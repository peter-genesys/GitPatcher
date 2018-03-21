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


  If (Test-Path "$APPS_DIR\$APP"){
    If (Test-Path "$APPS_DIR\$APP\install.sql"){
        Info "Ready to import $APP into $DBNAME"
        Warn "YOU ARE ABOUT TO IMPORT AND OVERWRITE APP $APP IN $DBNAME"
        write-host
        $confirmation = Read-Host "Are you Sure You Want To Proceed"
        if ($confirmation -eq 'y') {
           Progress "Ok - Importing ..."
           write-host
           Set-Location "$APPS_DIR\$APP"

           $Host.UI.RawUI.BackgroundColor = "Black"
           $Host.UI.RawUI.ForegroundColor = "DarkGray"
           #Using the call operator: &
           #Pipe exit to sqlplus to make the script finish.
           echo "exit;" | & "sqlplus.exe" "$USER/$PASSWORD@$SID" "@install.sql"
 
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
# COUNTRY CONFIG
# This is the only section that differs au to nz versions
# --------------------------------------------------------------------------------------
Debug "PAC-MAN_InstallApp.ps1"
# --------------------------------------------------------------------------------------

$Host.UI.RawUI.WindowTitle     = "Install Apex Apps"
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
$level = $host.ui.PromptForChoice($caption,$message,$choicesLevel,1)

switch ($level){
    0 { Debug "VM_12C"

       $Host.UI.RawUI.WindowTitle  = "Install Apex Apps - VM 12C"
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
       break}
    1 { Debug "PAC_DEV" 

       $Host.UI.RawUI.WindowTitle  = "Install Apex Apps - PAC_DEV"
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
       break}
    2 { Debug "PAC_TEST" 

       $Host.UI.RawUI.WindowTitle  = "Install Apex Apps - PAC_TEST"
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
       break}

};

Debug $connection
Debug $dbname
Debug $user
Debug $pword
Debug $sid
 

write-host 
$Host.UI.RawUI.BackgroundColor = $PromoBackColor
$Host.UI.RawUI.ForegroundColor = $PromoForeColor
$appno = Read-Host "Import into $dbname - Apex App No? (null to quit) "
While ($appno -And $appno -ne 'n') {
  Debug $appno
  InstallApexApp "$connection"  "$user"  "$pword" "$appno" "$util_dir" "$dbname" "$sid"
  write-host   
  $Host.UI.RawUI.BackgroundColor = $PromoBackColor
  $Host.UI.RawUI.ForegroundColor = $PromoForeColor
  $appno = Read-Host "Import into $dbname - another Apex App No? (null to quit) "
}  
 
#read-host "Hit return to close."