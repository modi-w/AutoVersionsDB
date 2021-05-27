@ECHO OFF
SETLOCAL ENABLEEXTENSIONS ENABLEDELAYEDEXPANSION

:: script global variables
SET me=%~n0
SET parent=%~dp0
SET logFile=automationLogs\%me%_log_%DATE:~-4,4%-%DATE:~-7,2%-%DATE:~0,2%-%time:~0,2%-%time:~3,2%-%time:~6,2%.log
::SET logFile=automationLogs\%me%_log.log
SET errorsLogFile=automationLogs\%me%_errorsLog_%DATE:~-4,4%-%DATE:~-7,2%-%DATE:~0,2%-%time:~0,2%-%time:~3,2%-%time:~6,2%.log
::SET errorsLogFile=automationLogs\%me%_errorsLog.log


if not exist automationLogs mkdir automationLogs

:: init log file with datetime and input parameters
ECHO %DATE:~-4,4%-%DATE:~-7,2%-%DATE:~0,2% %time:~0,2%:%time:~3,2%:%time:~6,2% -- Start Process -- %1 %2 %3 %4 %5 %6 %7 %8 %9  > "%logFile%"

::Pay Attention:
::the dotenet build command write the errors to stdout (instead of stderr) so we write >> "%errorsLogFile%" instead of 2>> "%errorsLogFile%"

:: build AutoVersionsDB.Core project
CALL :echoExtend start build: AutoVersionsDB.Core
dotnet build src\AutoVersionsDB\AutoVersionsDB.Core\AutoVersionsDB.Core.csproj -c debug >> "%errorsLogFile%"
CALL :checkError "%errorsLogFile%"
CALL :echoExtend complete build: AutoVersionsDB.Core

:: build AutoVersionsDB.ConsoleApp project
CALL :echoExtend start build: AutoVersionsDB.ConsoleApp
dotnet build src\AutoVersionsDB\AutoVersionsDB.ConsoleApp\AutoVersionsDB.ConsoleApp.csproj -c debug >> "%errorsLogFile%"
CALL :checkError "%errorsLogFile%"
CALL :echoExtend complete build: AutoVersionsDB.ConsoleApp


:: build AutoVersionsDB.WinApp project
CALL :echoExtend start build: AutoVersionsDB.WinApp
dotnet build src\AutoVersionsDB\AutoVersionsDB.WinApp\AutoVersionsDB.WinApp.csproj -c debug  >> "%errorsLogFile%"
CALL :checkError "%errorsLogFile%"
CALL :echoExtend complete build: AutoVersionsDB.WinApp




goto :exitProcess 








:echoExtend
ECHO %DATE:~-4,4%-%DATE:~-7,2%-%DATE:~0,2% %time:~0,2%:%time:~3,2%:%time:~6,2% -- %* >> "%logFile%"
ECHO %*
EXIT /B %ERRORLEVEL%




:checkError
if [%ERRORLEVEL%] NEQ [0] IF [%~z1] NEQ [0] (

	ECHO -------  Error Message -------  
	ECHO Error Number: %ERRORLEVEL%
	TYPE "%errorsLogFile%" 
	ECHO ------------------------------  

	ECHO %DATE:~-4,4%-%DATE:~-7,2%-%DATE:~0,2% %time:~0,2%:%time:~3,2%:%time:~6,2% --  Error -------  >> "%logFile%"
	ECHO Error Number: %ERRORLEVEL% >> "%logFile%"
	TYPE "%errorsLogFile%" >> "%logFile%"
	ECHO ------------------------------  >> "%logFile%"	
	
	goto :exitProcess 
) 
EXIT /B %ERRORLEVEL%



:exitProcess
ECHO %DATE:~-4,4%-%DATE:~-7,2%-%DATE:~0,2% %time:~0,2%:%time:~3,2%:%time:~6,2% -- Process Complete --  >> "%logFile%"
IF EXIST "%errorsLogFile%" DEL "%errorsLogFile%"
ENDLOCAL
EXIT %ERRORLEVEL%


