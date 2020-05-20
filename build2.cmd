@ECHO OFF
SETLOCAL ENABLEEXTENSIONS ENABLEDELAYEDEXPANSION

:: script global variables
SET me=%~n0
SET parent=%~dp0
SET logFile=automationLogs\"%me%"_log_%DATE:~-4%-%DATE:~4,2%-%DATE:~7,2%-%time:~0,2%-%time:~3,2%-%time:~6,2%.log
SET errorsLogFile=automationLogs\"%me%"_errorsLog_%DATE:~-4%-%DATE:~4,2%-%DATE:~7,2%-%time:~0,2%-%time:~3,2%-%time:~6,2%.log

:: init log file with datetime and input parameters
ECHO %DATE:~-4%-%DATE:~4,2%-%DATE:~7,2% %time:~0,2%:%time:~3,2%:%time:~6,2% -- Start -- %1 %2 %3 %4 %5 %6 %7 %8 %9  

:: build AutoVersionsDB.Core project
CALL :echoExtend start build: AutoVersionsDB.Core
dotnet build src\AutoVersionsDB\AutoVersionsDB.Core\AutoVersionsDB.Core.csproj -c debug 
CALL :checkError "%errorsLogFile%"
CALL :echoExtend complete build: AutoVersionsDB.Core



:: build AutoVersionsDB.WinApp project
CALL :echoExtend start build: AutoVersionsDB.WinApp
dotnet build src\AutoVersionsDB\AutoVersionsDB.WinApp\AutoVersionsDB.WinApp.csproj -c debug
CALL :checkError "%errorsLogFile%"
CALL :echoExtend complete build: AutoVersionsDB.WinApp




goto :exitProcess 








:echoExtend
ECHO %DATE:~-4%-%DATE:~4,2%-%DATE:~7,2% %time:~0,2%:%time:~3,2%:%time:~6,2% -- %* >> "%logFile%"
ECHO %*
EXIT /B %ERRORLEVEL%




:checkError
if [%ERRORLEVEL%] NEQ [0] IF [%~z1] NEQ [0] (
	ECHO -------  Error Message -------  
	ECHO Error Number: %ERRORLEVEL%
	TYPE "%errorsLogFile%" 
	ECHO ------------------------------  

	ECHO %DATE:~-4%-%DATE:~4,2%-%DATE:~7,2% %time:~0,2%:%time:~3,2%:%time:~6,2% --  Error -------  >> "%logFile%"
	ECHO Error Number: %ERRORLEVEL% >> "%logFile%"
	TYPE "%errorsLogFile%" >> "%logFile%"
	ECHO ------------------------------  >> "%logFile%"	
	
	goto :exitProcess 
) 
EXIT /B %ERRORLEVEL%



:exitProcess
ECHO %DATE:~-4%-%DATE:~4,2%-%DATE:~7,2% %time:~0,2%:%time:~3,2%:%time:~6,2% -- Cmplete  >> "%logFile%"
IF EXIST "%errorsLogFile%" DEL "%errorsLogFile%"
ENDLOCAL
EXIT /B %ERRORLEVEL%

