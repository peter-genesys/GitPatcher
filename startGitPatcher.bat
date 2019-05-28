set HOME=%USERPROFILE%
powershell scripts\killGitPatcher.ps1
git pull 
start GitPatcher\bin\Release\GitPatcher.exe