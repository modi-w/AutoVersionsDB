@ECHO OFF
SETLOCAL ENABLEEXTENSIONS ENABLEDELAYEDEXPANSION

:: script global variables
SET me=%~n0
SET parent=%~dp0
SET logFile=automationLogs\%me%_%DATE:~-4,4%-%DATE:~-7,2%-%DATE:~0,2%-%time:~0,2%-%time:~3,2%-%time:~6,2%.log
SET errorsLogFile=automationLogs\%me%_%DATE:~-4,4%-%DATE:~-7,2%-%DATE:~0,2%-%time:~0,2%-%time:~3,2%-%time:~6,2%.errorsLog.log

SET testsLogFileName=%parent%\automationLogs\%me%_%DATE:~-4,4%-%DATE:~-7,2%-%DATE:~0,2%-%time:~0,2%-%time:~3,2%-%time:~6,2%.results.trx
SET testLogParams=trx;LogFileName=%testsLogFileName%


:: init log file with datetime and input parameters
ECHO %DATE:~-4,4%-%DATE:~-7,2%-%DATE:~0,2% %time:~0,2%:%time:~3,2%:%time:~6,2% -- Start -- %1 %2 %3 %4 %5 %6 %7 %8 %9  > "%logFile%"


:: run tests for: AutoVersionsDB.Core.IntegrationTests.csproj
CALL :echoExtend start run tests for: AutoVersionsDB.Core.IntegrationTests.csproj
::dotnet test src\AutoVersionsDB\AutoVersionsDB.Core.IntegrationTests\AutoVersionsDB.Core.IntegrationTests.csproj -l "console;verbosity=detailed" 2>> "%errorsLogFile%"
dotnet test src\AutoVersionsDB\AutoVersionsDB.Core.IntegrationTests\AutoVersionsDB.Core.IntegrationTests.csproj -l:%testLogParams% 2>> "%errorsLogFile%"
CALL :checkError "%errorsLogFile%"
CALL :echoExtend complete run tests for: AutoVersionsDB.Core.IntegrationTests.csproj



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
ECHO %DATE:~-4,4%-%DATE:~-7,2%-%DATE:~0,2% %time:~0,2%:%time:~3,2%:%time:~6,2% -- Cmplete  >> "%logFile%"
IF EXIST "%errorsLogFile%" DEL "%errorsLogFile%"
ENDLOCAL
EXIT /B %ERRORLEVEL%

