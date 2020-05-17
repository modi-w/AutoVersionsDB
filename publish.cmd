@ECHO OFF
SETLOCAL ENABLEEXTENSIONS ENABLEDELAYEDEXPANSION

:: script global variables
SET me=%~n0
SET parent=%~dp0
SET logFile=automationLogs\"%me%"_log_%DATE:~-4%-%DATE:~4,2%-%DATE:~7,2%-%time:~0,2%-%time:~3,2%-%time:~6,2%.log
SET errorsLogFile=automationLogs\"%me%"_errorsLog_%DATE:~-4%-%DATE:~4,2%-%DATE:~7,2%-%time:~0,2%-%time:~3,2%-%time:~6,2%.log

:: init log file with datetime and input parameters
ECHO %DATE:~-4%-%DATE:~4,2%-%DATE:~7,2% %time:~0,2%:%time:~3,2%:%time:~6,2% -- Start -- %1 %2 %3 %4 %5 %6 %7 %8 %9  > "%logFile%"

::find msbuild Path
CALL :echoExtend start find MSBuild Path
"%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -latest -products * -requires Microsoft.Component.MSBuild -property installationPath > tempOutput.txt  2>> "%errorsLogFile%"
set /p vsPath1= < tempOutput.txt
del tempOutput.txt
set msBuildPath=%vsPath1%\MSBuild\Current\Bin\msbuild
CALL :checkError "%errorsLogFile%"
CALL :echoExtend complete find MSBuild Path


:: create package for x64
CALL :echoExtend start create package for x64
"%msBuildPath%" src\AutoVersionsDB\AutoVersionsDB.Setup\AutoVersionsDB.Setup.wixproj /p:Configuration=Debug /p:Platform=x64 2>> "%errorsLogFile%"
CALL :checkError "%errorsLogFile%"
CALL :echoExtend complete create package for x64


:: create package for x86
CALL :echoExtend start create package for x86
"%msBuildPath%" src\AutoVersionsDB\AutoVersionsDB.Setup\AutoVersionsDB.Setup.wixproj /p:Configuration=Debug /p:Platform=x86 2>> "%errorsLogFile%"
CALL :checkError "%errorsLogFile%"
CALL :echoExtend complete create package for x86






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

