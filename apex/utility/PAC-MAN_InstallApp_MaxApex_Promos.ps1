function Debug($message) {
  #write-host $message -foregroundcolor "DarkGray" -backgroundcolor "Black"
}

function Info($message) {
  write-host
  write-host $message -foregroundcolor "Gray" -backgroundcolor "Black"
}

function Warn($message) {
  write-host
  write-warning $message
}

function Progress($message) {
  write-host 
  write-host $message
}

function InstallApexApp ( $CONNECTION ,$USER ,$PASSWORD ,$APP_ID ,$UTIL_DIR, $DBNAME, $SID, $PROMO )
{
 
  $ORACLE_DIR = "$UTIL_DIR\oracle"
  $APPS_DIR = "$UTIL_DIR\..\apps"

  Debug $UTIL_DIR
  Debug $CONNECTION
  Debug $USER
  Debug $PASSWORD
  Debug $APP_ID
  Debug $APPS_DIR
  Debug $ORACLE_DIR
  Debug $DBNAME
  Debug $SID
  Debug $PROMO
 
  $APP = "f$APP_ID"
  $APP_SQL = "f$APP_ID.sql"
  $SPLIT_SQL = "f$APP_ID.sql"

  
  ### DETERMINE APP_ID_NAME ###

  Debug "Extracting App Name from set_environment.sql"
  #Look for this line in the f200/application/set_environment.sql
  #"prompt APPLICATION 9221 - Marketing Reports"
  #Use details in the commit message.
  $env_filename = "$APPS_DIR/$APP/application/set_environment.sql"
  $search = "prompt APPLICATION"
  $pos = $search.length
  $matched_lines = Get-Content "$env_filename" | Select-String "$search" -SimpleMatch
  $matched_line = $matched_lines[0].line
  Debug $matched_line 

  $app_id_name = $matched_line.Substring($pos+1)
  Debug $app_id_name 

  ### DETERMINE NEW_APP_ID ###

  Debug "Extracting APP_ID_<PROMO> value from create_application.sql"
  #Look for this line in the file f200/application/create_application.sql
  #",p_substitution_string_13=>'APP_ID_DEV'"
  #Use the value to modify the APP_ID
  $vars_filename = "$APPS_DIR/$APP/application/create_application.sql"
  $search = "=>'APP_ID_$PROMO'"

  $matched_lines = Get-Content "$vars_filename" | Select-String "$search" -SimpleMatch -Context 0, 1
  $matched_line = $matched_lines[0].line
  Debug $matched_line

  $subs_var_num = $matched_line.Substring(23,2) #assumes 2 digit var numbers
  Debug $subs_var_num

  #Then search for by the subs_var_num for ,p_substitution_value_13=>'200'
  $search = ",p_substitution_value_$subs_var_num=>"
  $pos = $search.length
  $matched_lines = Get-Content "$vars_filename" | Select-String "$search" -SimpleMatch
  $matched_line = $matched_lines[0].line
  Debug $matched_line
  $new_app_id = $matched_line.Substring($pos+1,3) #assumes 3digit app_id
  Debug $NEW_APP_ID

 
  ### DETERMINE NEW_APP_ALIAS ###

  Debug "Extracting APP_ID_<PROMO> value from create_application.sql"
  #Look for this line in the file f200/application/create_application.sql
  #",p_substitution_string_10=>'ALIAS_DEV'"
  #Use the value to modify the ALIAS
  $vars_filename = "$APPS_DIR/$APP/application/create_application.sql"
  $search = "=>'ALIAS_$PROMO'"

  $matched_lines = Get-Content "$vars_filename" | Select-String "$search" -SimpleMatch -Context 0, 1
  $matched_line = $matched_lines[0].line
  Debug $matched_line

  $subs_var_num = $matched_line.Substring(23,2) #assumes 2 digit var numbers
  Debug $subs_var_num

  #Then search for by the subs_var_num for ,p_substitution_value_10=>'PACMAN_DEV'
  $search = ",p_substitution_value_$subs_var_num=>"
  $pos = $search.length
  $matched_lines = Get-Content "$vars_filename" | Select-String "$search" -SimpleMatch
  $matched_line = $matched_lines[0].line
  Debug $matched_line
  $NEW_APP_ALIAS = $matched_line.Substring($pos) # Assume we want it wrapped in ''
  Debug $NEW_APP_ALIAS

 

  Info "Importing $app_id_name into $dbname"
  Info "Promoted to $PROMO as AppId $NEW_APP_ID with Alias $NEW_APP_ALIAS"
$Host.UI.RawUI.WindowTitle     = "Install Apex Apps on MaxApex WITH new AppId $NEW_APP_ID and Alias $NEW_APP_ALIAS"
 
#  NO LONGER ASK - DERIVED FROM SUBS VARS 
#  $NEW_APP_ID = Read-Host "Import $app_id_name into $dbname - New Apex App No? "
 
  If (Test-Path "$APPS_DIR\$APP"){
    If (Test-Path "$APPS_DIR\$APP\install.sql"){
        Info "Ready to import $APP into $DBNAME"
        Warn "YOU ARE ABOUT TO IMPORT AND OVERWRITE APP $NEW_APP_ID IN $DBNAME"
        write-host
        $confirmation = Read-Host "Are you Sure You Want To Proceed"
        if ($confirmation -eq 'y') {
           Progress "Ok - Importing ..."
           write-host

           $Host.UI.RawUI.BackgroundColor = "Black"
           $Host.UI.RawUI.ForegroundColor = "DarkGray"

           Set-Location "$APPS_DIR\$APP"

$install_mod = @"
declare 
 v_workspace_id NUMBER; 
begin 
 select workspace_id into v_workspace_id 
 from apex_workspaces where workspace = '$USER'; 
 apex_application_install.set_workspace_id (v_workspace_id); 
 apex_util.set_security_group_id 
 (p_security_group_id => apex_application_install.get_workspace_id); 
 apex_application_install.set_schema('$USER'); 
 apex_application_install.set_application_id($NEW_APP_ID); 
 apex_application_install.generate_offset; 
 apex_application_install.set_application_alias($NEW_APP_ALIAS); 
end; 
/
@install.sql
"@

$install_mod | Set-Content 'install_mod.sql' 
 
           #Using the call operator: &
           #Pipe exit to sqlplus to make the script finish.
           Debug "echo exit; | & sqlplus.exe $USER/$PASSWORD@$SID @install_mod.sql"
           echo "exit;" | & "sqlplus.exe" "$USER/$PASSWORD@$SID" "@install_mod.sql"
 
        } else {
          Info "User cancelled import."
        }
      } else { 
        Warn "Could not find $APPS_DIR\$APP\install.sql"
      }
  } else { 
    Warn "Could not find App dir $APPS_DIR\$APP"
  }
 
}
 
# --------------------------------------------------------------------------------------
Debug "PAC-MAN_InstallApp_MaxApex_Promos.ps1"
# --------------------------------------------------------------------------------------

$Host.UI.RawUI.WindowTitle     = "Install Apex Apps on MaxApex WITH new AppId and Alias"
$Host.UI.RawUI.BackgroundColor = "darkMagenta"
$Host.UI.RawUI.ForegroundColor = "Gray"
 
Progress "Import Apex App into DB from local GIT repo"



#Get the current dir
$scriptpath = $MyInvocation.MyCommand.Path
$util_dir = Split-Path $scriptpath 
Debug $util_dir

$caption = "Choose Database";
$message = "Which promotion level?";
$VM_12C = new-Object System.Management.Automation.Host.ChoiceDescription   "&VM_12C","DEV 12C EE VM";
$PAC_DEV  = new-Object System.Management.Automation.Host.ChoiceDescription "PAC_&DEV" ,"DEV  11G XE Cloud";
$PAC_TEST = new-Object System.Management.Automation.Host.ChoiceDescription "PAC_&TEST","TEST 11G XE Cloud";
$PAC_PROD = new-Object System.Management.Automation.Host.ChoiceDescription "PAC_&PROD","PROD 11G XE Cloud";

$choicesLevel = [System.Management.Automation.Host.ChoiceDescription[]]($VM_12C,$PAC_DEV,$PAC_TEST,$PAC_PROD);
write-host
$level = $host.ui.PromptForChoice($caption,$message,$choicesLevel,2)

       switch ($level){
           0 { Debug "VM_12C"

       $Host.UI.RawUI.WindowTitle  = "Install Apex Apps WITH new AppId - VM 12C"
       $Host.PrivateData.WarningBackgroundColor = "yellow"
       $Host.PrivateData.WarningForegroundColor = "black"
       Info "You have chosen the Virtual database."
       $PromoBackColor = "Black"
       $PromoForeColor = "DarkMagenta"
       
              $connection = "192.168.56.102:1521/ORCL";
              $sid    = "VM_12C";
              $dbname = "VM_12C";
              $user   = "PACMAN";
              $pword  = "pacman";
              $promo  = "VM";
              break}

           1 { Debug "PAC_DEV" 

       $Host.UI.RawUI.WindowTitle  = "Install Apex Apps WITH new AppId - PAC_DEV"
       $Host.PrivateData.WarningBackgroundColor = "DarkYellow"
       $Host.PrivateData.WarningForegroundColor = "black"
       Info "You have chosen the Development database."
       $PromoBackColor = "Black"
       $PromoForeColor = "Yellow"

              $connection = "indigo.maxapex.net:1555/XE";
              $sid    = "PAC_DEV";
              $dbname = "PAC_DEV";
              $user   = "A171872B";
              $pword  = "Donkey01";
              $promo  = "DEV";
              break}

           2 { Debug "PAC_TEST" 

       $Host.UI.RawUI.WindowTitle  = "Install Apex Apps WITH new AppId - PAC_TEST"
       $Host.PrivateData.WarningBackgroundColor = "red"
       $Host.PrivateData.WarningForegroundColor = "white"
       Warn "You have chosen the Test database."
       $PromoBackColor = "Black"
       $PromoForeColor = "Magenta"

              $connection = "indigo.maxapex.net:1555/XE";
              $sid    = "PAC_TEST";
              $dbname = "PAC_TEST";
              $user   = "A171872C";
              $pword  = "Tiger01";
              $promo  = "TEST";
              break}

           3 { Debug "PAC_PROD" 

       $Host.UI.RawUI.WindowTitle  = "Install Apex Apps - PAC_PROD"
       $Host.PrivateData.WarningBackgroundColor = "red"
       $Host.PrivateData.WarningForegroundColor = "white"
       Warn "You have chosen the Prod database."
       $PromoBackColor = "Black"
       $PromoForeColor = "Magenta"

       $connection = "indigo.maxapex.net:1555/XE";
       $sid    = "PAC_PROD";
       $dbname = "PAC_PROD";
       $user   = "A171872";
       $pword  = "Monkey01";
       $promo  = "PROD";
       break}

       };

Debug $connection
Debug $dbname
Debug $user
Debug $pword
Debug $sid
Debug $promo
 

write-host 
$Host.UI.RawUI.BackgroundColor = $PromoBackColor
$Host.UI.RawUI.ForegroundColor = $PromoForeColor
$appno = Read-Host "Import into $dbname - Apex App No? (null to quit) "
While ($appno -And $appno -ne 'n') {
  Debug $appno
  InstallApexApp "$connection" "$user" "$pword" "$appno" "$util_dir" "$dbname" "$sid" "$promo"
  write-host   
  $Host.UI.RawUI.BackgroundColor = $PromoBackColor
  $Host.UI.RawUI.ForegroundColor = $PromoForeColor
  $appno = Read-Host "Import into $dbname - another Apex App No? (null to quit) "
}  
 
#read-host "Hit return to close."