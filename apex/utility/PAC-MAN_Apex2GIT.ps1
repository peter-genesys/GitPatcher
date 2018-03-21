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

function ApexExportCommit ( $CONNECTION ,$USER ,$PASSWORD ,$APP_ID ,$UTIL_DIR, $DBNAME, $SID ) {
           $Host.UI.RawUI.BackgroundColor = "Black"
           $Host.UI.RawUI.ForegroundColor = "DarkGray"

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
 
  Debug "APEX file export and commit - uses oracle.apex.APEXExport.class and java oracle.apex.APEXExportSplitter.class" 

  #add ojdbc6.jar to the CLASSPATH, in this case its on the checkout path
  $CLASSPATH = $Env:CLASSPATH
  $env:CLASSPATH = "$CLASSPATH;$ORACLE_DIR\jdbc\lib\ojdbc6.jar" 

  Debug $Env:CLASSPATH 

  $APP = "f$APP_ID"
  $APP_SQL = "f$APP_ID.sql"
  $SPLIT_SQL = "f$APP_ID.sql"

  #Remove files that may be left-over from a previous failure.
  If (Test-Path "$UTIL_DIR\$APP"){
    Remove-Item -Recurse -Force -ErrorAction 0 @("$UTIL_DIR\$APP")
  }
  If (Test-Path "$UTIL_DIR\$APP_SQL"){
    Remove-Item "$UTIL_DIR\$APP_SQL"
  }
 
  #NB Not exporting application comments
  java oracle.apex.APEXExport -db $CONNECTION -user $USER -password $PASSWORD -applicationid $APP_ID -expPubReports -skipExportDate
  
  #No need to rename at BMW.
  #Remove-Item -Recurse -Force -ErrorAction 0 @("$APPS_DIR\$APP_SQL")
  #Rename-Item -Path "$APPS_DIR\$SPLIT_SQL" -NewName "$APP_SQL"

 
  Info "Splitting $APP_SQL into its composite files" 
  java oracle.apex.APEXExportSplitter $APP_SQL 
   
  Info "Replace existing split application with new version." 
  Debug "Remove the application directory $APPS_DIR\$APP" 

  If (Test-Path "$APPS_DIR\$APP"){
    Remove-Item -Recurse -Force -ErrorAction 0 @("$APPS_DIR\$APP")
  }

  If (Test-Path "$APPS_DIR\$APP_SQL"){
    Remove-Item "$APPS_DIR\$APP_SQL"
  }
  
  Debug "Move raw and split apex app to $APPS_DIR"
  #Move split files to apps dir
  Move-Item "$UTIL_DIR\$APP" "$APPS_DIR"
  #Move full export to apps dir
  Move-Item "$UTIL_DIR\$APP_SQL" "$APPS_DIR\$APP_SQL"
 
  Debug "Extracting App Name from set_environment.sql"
  #Look for this line in the f101/application/set_environment.sql
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

  Info "Adding new files to GIT" 
  TortoiseGitProc.exe /command:"add" /path:"$APPS_DIR/$APP"  | Out-Null
   
  Info "Committing changed files to GIT" 
  TortoiseGitProc.exe /command:"commit" /path:"$APPS_DIR/$APP" /logmsg:"""Apex2GIT App $app_id_name - $DBNAME""" /closeonend:1  | Out-Null
  
  Info "Reverting remaining changes from the GIT checkout" 
  TortoiseGitProc.exe /command:"revert" /path:"$APPS_DIR/$APP"

}
 
# -------------------------------------------------------------------------------------- 
# COUNTRY CONFIG
# This is the only section that differs au to nz versions
# --------------------------------------------------------------------------------------
Debug "PAC-MAN_Apex2GIT.ps1"
# --------------------------------------------------------------------------------------

$Host.UI.RawUI.WindowTitle     = "Export Apex Apps"
$Host.UI.RawUI.BackgroundColor = "darkgreen"
$Host.UI.RawUI.ForegroundColor = "Gray"

Progress "Commit changes from Apex App to local GIT repo"

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
       $PromoForeColor = "DarkGreen"

       $connection = "192.168.56.102:1521/ORCL";
       $sid    = "VM_12C";
       $dbname = "VM_12C";
       $user   = "PACMAN";
       $pword  = "pacman";
       break}

    1 { Debug "PAC_DEV" 

       $Host.UI.RawUI.WindowTitle  = "Export Apex Apps - PAC_DEV"
       $Host.PrivateData.WarningBackgroundColor = "yellow"
       $Host.PrivateData.WarningForegroundColor = "black"
       Info "You have chosen the Development database."
       $PromoBackColor = "Black"
       $PromoForeColor = "Green"

       $connection = "indigo.maxapex.net:1555/XE";
       $sid    = "PAC_DEV";
       $dbname = "PAC_DEV";
       $user   = "A171872B";
       $pword  = "Donkey01";
       break}

    2 { Debug "PAC_TEST" 
       $Host.UI.RawUI.WindowTitle  = "Export Apex Apps - PAC_TEST"
       $Host.PrivateData.WarningBackgroundColor = "red"
       $Host.PrivateData.WarningForegroundColor = "white"
       Warn "You have chosen the Test database."
       $PromoBackColor = "Black"
       $PromoForeColor = "Cyan"

       $connection = "indigo.maxapex.net:1555/XE";
       $sid    = "PAC_TEST";
       $dbname = "PAC_TEST";
       $user   = "A171872C";
       $pword  = "Tiger01";
       break}

    3 { Debug "PAC_PROD" 
       $Host.UI.RawUI.WindowTitle  = "Export Apex Apps - PAC_PROD"
       $Host.PrivateData.WarningBackgroundColor = "red"
       $Host.PrivateData.WarningForegroundColor = "white"
       Warn "You have chosen the Test database."
       $PromoBackColor = "Black"
       $PromoForeColor = "Cyan"

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
$appno = Read-Host "Export from $dbname - Apex App No? (null to quit) "
While ($appno -And $appno -ne 'n') {
  Debug $appno
  ApexExportCommit "$connection"  "$user"  "$pword" "$appno" "$util_dir" "$dbname" "$sid"
  write-host
  $Host.UI.RawUI.BackgroundColor = $PromoBackColor
  $Host.UI.RawUI.ForegroundColor = $PromoForeColor
  $appno = Read-Host "Export from $dbname - another Apex App No? (null to quit) "
}  
 
#read-host "Hit return to close."