@ECHO OFF
ECHO. 
ECHO. This service installer must be run with Administrative privileges
ECHO. 
ECHO. NTFS permissions must be applied to the application working directory:
ECHO.  Administrators = Full Control
ECHO.  LOCAL SERVICE = Full Control
ECHO.  SYSTEM = Full Control
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
ECHO Installing service %PROG%
%FIRSTPART%%DOTNETVER%%SECONDPART% %PROG%
ECHO DONE !
ECHO. 
ECHO. Go to Start, Run, services.msc
ECHO. Pick SimpleHTTP4
ECHO. Set Startup type: Automatic
ECHO. Then Start the service
ECHO. 
PAUSE
ECHO. Removing Install Logs
Del SimpleHTTP4.InstallLog
Del SimpleHTTP4.InstallState
Del InstallUtil.InstallLog
EXIT
:fail
ECHO. 
ECHO FAILURE: Could not find .NET Framework v4.0.30319
ECHO. 
PAUSE
EXIT