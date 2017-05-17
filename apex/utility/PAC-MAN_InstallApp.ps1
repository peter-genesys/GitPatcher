function Debug($message) {
  #write-host $message
}

function Info($message) {
  #write-host $message
}

function Warn($message) {
  write-host $message
}

function Progress($message) {
  write-host 
  write-host $message
}

function InstallApexApp ( $CONNECTION ,$USER ,$PASSWORD ,$APP_ID ,$UTIL_DIR, $DBNAME, $SID )
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
 
  $APP = "f$APP_ID"
  $APP_SQL = "f$APP_ID.sql"
  $SPLIT_SQL = "f$APP_ID.sql"

  $NEW_APP_ID = Read-Host "Import $APP into $dbname - New Apex App No? "

  If (Test-Path "$APPS_DIR\$APP"){
    If (Test-Path "$APPS_DIR\$APP\install.sql"){
        Warn
        Warn "Ready to import $APP into $DBNAME"
        Warn 
        Warn "**** YOU ARE ABOUT TO IMPORT AND OVERWRITE THE APP IN $DBNAME ****"
        Warn 
        $confirmation = Read-Host "Are you Sure You Want To Proceed"
        if ($confirmation -eq 'y') {
           Progress
           Progress "Ok - Importing ..."
           write-host
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
 apex_application_install.set_application_alias('F$NEW_APP_ID'); 
end; 
/
@install.sql
"@

$install_mod | Set-Content 'install_mod.sql' 
 
           #Using the call operator: &
           #Pipe exit to sqlplus to make the script finish.
           Debug "echo exit; | & sqlplus.exe $USER/$PASSWORD@$SID @install_mod.sql"
           echo "exit;" | & "sqlplus.exe" "$USER/$PASSWORD@$SID" "@install_mod.sql"
 
        }
      } else { 
        Warn "Could not find $APPS_DIR\$APP\install.sql"
      }
  } else { 
    Warn "Could not find App dir $APPS_DIR\$APP"
  }
 
}
 
# -------------------------------------------------------------------------------------- 
# COUNTRY CONFIG
# This is the only section that differs au to nz versions
# --------------------------------------------------------------------------------------
Debug "CRMauInstallApp.ps1"
$countryName = "Australia"
$country     = 0
# --------------------------------------------------------------------------------------

write-host "Import Apex App into DB CRM $countryName from local GIT repo"


#Get the current dir
$scriptpath = $MyInvocation.MyCommand.Path
$util_dir = Split-Path $scriptpath 
Debug $util_dir


#$caption = "Choose Database";
#$message = "Which country?";
#$AU = new-Object System.Management.Automation.Host.ChoiceDescription "&AU","Australia";
#$NZ = new-Object System.Management.Automation.Host.ChoiceDescription "&NZ","New Zealand";
#$choicesCountry = [System.Management.Automation.Host.ChoiceDescription[]]($AU,$NZ);
#$country = $host.ui.PromptForChoice($caption,$message,$choicesCountry,0)
 
$caption = "Choose Database";
$message = "Which promotion level?";
$VM_12C = new-Object System.Management.Automation.Host.ChoiceDescription   "&VM_12C","DEV 12C EE VM";
$PAC_TEST = new-Object System.Management.Automation.Host.ChoiceDescription "PAC_&TEST","TEST 11G XE Cloud";
$PAC_DEV  = new-Object System.Management.Automation.Host.ChoiceDescription "PAC_&DEV" ,"DEV  11G XE Cloud";
$choicesLevel = [System.Management.Automation.Host.ChoiceDescription[]]($VM_12C,$PAC_DEV,$PAC_TEST);
write-host
$level = $host.ui.PromptForChoice($caption,$message,$choicesLevel,0)

switch ($country){
    0 { Debug "Australia" 
       switch ($level){
           0 { Debug "VM_12C"
              $connection = "192.168.56.102:1521/ORCL";
              $sid    = "VM_12C";
              $dbname = "VM_12C";
              $user   = "PACMAN";
              $pword  = "pacman";
              break}
           1 { Debug "PAC_DEV" 
              $connection = "indigo.maxapex.net:1555/XE";
              $sid    = "PAC_DEV";
              $dbname = "PAC_DEV";
              $user   = "A171872B";
              $pword  = "Donkey01";
              break}
           2 { Debug "PAC_TEST" 
              $connection = "indigo.maxapex.net:1555/XE";
              $sid    = "PAC_TEST";
              $dbname = "PAC_TEST";
              $user   = "A171872";
              $pword  = "Monkey01";
              break}
       };
       break}
    1 { Debug "New Zealand"  
       switch ($level){
           0 { Debug "DEV"; 
              $connection = "ssau0107.asiapacific.bmw.corp:1521/DEVNZ";
              $sid    = "DEVNZ.WORLD"; 
              $dbname = "DEV NZ";
              $user   = "BROCK";
              $pword  = "mf8501";
              break}
           1 { Debug "LIVE"; 
              $connection = "bmwzqora:1521/ORCLNZ";
              $sid    = "ORCLNZ.WORLD"; 
              $dbname = "LIVE NZ";
              $user   = "BROCK";
              $pword  = "mf8501";
              break}
       };
       break}
}

Debug $connection
Debug $dbname
Debug $user
Debug $pword
Debug $sid
 

write-host 
$appno = Read-Host "Import into $dbname - Apex App No? (null to quit) "
While ($appno) {
  Debug $appno
  InstallApexApp "$connection"  "$user"  "$pword" "$appno" "$util_dir" "$dbname" "$sid"
  write-host   
  $appno = Read-Host "Import into $dbname - another Apex App No? (null to quit) "
}  
 
#read-host "Hit return to close."