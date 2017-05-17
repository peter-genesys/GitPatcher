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

function ApexExportCommit ( $CONNECTION ,$USER ,$PASSWORD ,$APP_ID ,$UTIL_DIR, $DBNAME ) {

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
 
  Info "APEX file export and commit - uses oracle.apex.APEXExport.class and java oracle.apex.APEXExportSplitter.class" 

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
  Info "Remove the application directory $APPS_DIR\$APP" 

  If (Test-Path "$APPS_DIR\$APP"){
    Remove-Item -Recurse -Force -ErrorAction 0 @("$APPS_DIR\$APP")
  }

  If (Test-Path "$APPS_DIR\$APP_SQL"){
    Remove-Item "$APPS_DIR\$APP_SQL"
  }
  
  Info "Move raw and split apex app to $APPS_DIR"
  #Move split files to apps dir
  Move-Item "$UTIL_DIR\$APP" "$APPS_DIR"
  #Move full export to apps dir
  Move-Item "$UTIL_DIR\$APP_SQL" "$APPS_DIR\$APP_SQL"

  #MY VERSION OF POWERSHELL DOES NOT SUPPORT THIS OPERATION
  #NEED TO UPGRADE POWERSHELL FIRST
  #Info "Extracting App Name from set_environment.sql"
  ##Look for this line in the f101/application/set_environment.sql
  ##"prompt APPLICATION 9221 - Marketing Reports"
  ##Use details in the commit message.
  #$env_filename = "$APPS_DIR/$APP/application/set_environment.sql"
  #$search = "prompt APPLICATION"
  #$pos = $search.length
  #$matched_lines = Get-Content "$env_filename" | Select-String "$search" -SimpleMatch
  #$matched_line = $matched_lines[0].line
  #Debug $matched_line 

  #$app_id_name = $matched_line.Substring($pos+1)
  $app_id_name = "PAC_MAN"
  Debug $app_id_name 

 
  Progress "Adding new files to GIT" 
  TortoiseGitProc.exe /command:"add" /path:"$APPS_DIR/$APP"  | Out-Null
   
  Progress "Committing changed files to GIT" 
  TortoiseGitProc.exe /command:"commit" /path:"$APPS_DIR/$APP" /logmsg:"""Apex2GIT App $app_id_name - $DBNAME""" /closeonend:1  | Out-Null
  Progress "Reverting remaining changes from the GIT checkout" 
  TortoiseGitProc.exe /command:"revert" /path:"$APPS_DIR/$APP"

}
 
# -------------------------------------------------------------------------------------- 
# COUNTRY CONFIG
# This is the only section that differs au to nz versions
# --------------------------------------------------------------------------------------
Debug "CRMauApex2GIT.ps1"
$countryName = "Australia"
$country     = 0
# --------------------------------------------------------------------------------------

Progress "Commit changes from an apex app from DB CRM $countryName to local GIT repo"

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
$PAC_DEV  = new-Object System.Management.Automation.Host.ChoiceDescription "PAC_&DEV" ,"DEV  11G XE Cloud";
$PAC_TEST = new-Object System.Management.Automation.Host.ChoiceDescription "PAC_&TEST","TEST 11G XE Cloud";

$choicesLevel = [System.Management.Automation.Host.ChoiceDescription[]]($VM_12C,$PAC_DEV,$PAC_TEST);
write-host
$level = $host.ui.PromptForChoice($caption,$message,$choicesLevel,0)

switch ($country){
    0 { Debug "Australia" 
       switch ($level){
           0 { Debug "VM_12C"
              $connection = "192.168.56.102:1521/ORCL";
              $dbname = "VM_12C";
              $user   = "PACMAN";
              $pword  = "pacman";
              break}
           1 { Debug "PAC_DEV" 
              $connection = "indigo.maxapex.net:1555/XE";
              $dbname = "PAC_DEV";
              $user   = "A171872B";
              $pword  = "Donkey01";
              break}
           2 { Debug "PAC_TEST" 
              $connection = "indigo.maxapex.net:1555/XE";
              $dbname = "PAC_TEST";
              $user   = "A171872";
              $pword  = "Monkey01";
              break}

       };
       break}
    1 { Debug "New Zealand"  
       switch ($level){
           0 { Debug "VM_12C"; 
              $connection = "ssau0107.asiapacific.bmw.corp:1521/VM_12CNZ";
              $dbname = "VM_12C NZ";
              $user   = "BROCK";
              $pword  = "mf8501";
              break}
           1 { Debug "PAC_TEST"; 
              $connection = "bmwzqora:1521/ORCLNZ";
              $dbname = "PAC_TEST NZ";
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
 
write-host 
$appno = Read-Host "Export from $dbname - Apex App No? (null to quit) "
While ($appno) {
  Debug $appno
  ApexExportCommit "$connection"  "$user"  "$pword" "$appno" "$util_dir" "$dbname"
  write-host
  $appno = Read-Host "Export from $dbname - another Apex App No? (null to quit) "
}  
 
#read-host "Hit return to close."