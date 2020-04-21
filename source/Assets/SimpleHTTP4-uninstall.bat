@ECHO OFF
ECHO. 
ECHO. This service uninstaller must be run with Administrative privileges
ECHO. 
PAUSE
SET PROG="%~dp0\SimpleHTTP4.exe"
SET FIRSTPART=%WINDIR%"\Microsoft.NET\Framework\v"
SET SECONDPART="\InstallUtil.exe"
SET DOTNETVER=4.0.30319
IF EXIST %FIRSTPART%%DOTNETVER%%SECONDPART% GOTO install
GOTO fail
:install
ECHO. 
ECHO Found .NET Framework v%DOTNETVER%
ECHO Uninstalling service %PROG%
%FIRSTPART%%DOTNETVER%%SECONDPART% %PROG% -u
ECHO DONE !
ECHO. 
PAUSE
ECHO. Removing Uninstall Logs
Del SimpleHTTP4.InstallLog
Del InstallUtil.InstallLog
EXIT
:fail
ECHO. 
ECHO FAILURE: Could not find .NET Framework v4.0.30319
ECHO. 
PAUSE
EXIT