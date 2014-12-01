write-host "ExtractVM_f123.ps1"
write-host "Commit changes from the Apex 123 application on the VM to local GIT repo"

#Get the current dir
$scriptpath = $MyInvocation.MyCommand.Path
$dir = Split-Path $scriptpath 
#write-host $dir
 
#include the ApexExportCommitGIT function  
. ".\oracle\ApexExportCommitGIT.ps1" 
 
ApexExportCommit "10.10.10.22:1521/PDBORCL"  "patch_admin"  "patch_admin" "123" "$dir" "$dir"
 
read-host "Finished."