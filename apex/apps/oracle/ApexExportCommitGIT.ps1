function ApexExportCommit ( $CONNECTION ,$USER ,$PASSWORD ,$APP_ID ,$CHECKOUT_PATH, $DIR )
{
  write-host $CONNECTION
  write-host $USER
  write-host $PASSWORD
  write-host $APP_ID
  write-host $CHECKOUT_PATH
  write-host $DIR

  #write-host "APEX file export and commit - uses oracle.apex.APEXExport.class and java oracle.apex.APEXExportSplitter.class"
  #Does this need to perform a pull from the master ??
  #TortoiseGitProc.exe /command:"update" /path:"$CHECKOUT_PATH" | Out-Null
  #add ojdbc5.jar to the CLASSPATH, in this case its on the checkout path
  $CLASSPATH = $Env:CLASSPATH
  $env:CLASSPATH = "$CLASSPATH;$DIR\oracle\jdbc\lib\ojdbc6.jar" 
  write-host $Env:CLASSPATH
  $APP_SQL = "f$APP_ID.sql"
  #NB Not exporting application comments
  java oracle.apex.APEXExport -db $CONNECTION -user $USER -password $PASSWORD -applicationid $APP_ID -expPubReports -skipExportDate
  #write-host "Don't bother converting to DOS, slow and unnecessary."
  #UNIX to DOS (So we can run the scripts in DOS)
  #write-host "Converting $APP_SQL to DOS - this takes a while, depending on size of export..."
  #$content = Get-Content $APP_SQL
  #$content | Set-Content $APP_SQL
  
  write-host "Remove the application directory $CHECKOUT_PATH\f$APP_ID" 
  Remove-Item -Recurse -Force -ErrorAction 0 @("$CHECKOUT_PATH\f$APP_ID")
 
  write-host "Splitting $APP_SQL into its composite files"
  java oracle.apex.APEXExportSplitter $APP_SQL 
   
  ##write-host "Adding new files to GIT"
  ##TortoiseGitProc.exe /command:"add" /path:"$CHECKOUT_PATH"  | Out-Null
  ##
  ##write-host "Committing changed files to GIT"
  ##TortoiseGitProc.exe /command:"commit" /path:"$CHECKOUT_PATH" /logmsg:"""App $APP_ID has been exported and split""" /closeonend:1  | Out-Null
}
 