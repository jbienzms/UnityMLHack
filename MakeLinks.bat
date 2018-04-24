@ECHO OFF

ECHO.
ECHO ===============================================================================
ECHO =                                 Make Links                                  =
ECHO ===============================================================================
ECHO.
ECHO This batch file creates symbolic links for  Unity 
ECHO source-based libraries that are used by this project.
ECHO.
ECHO The libraries used by this project are:
ECHO.
ECHO * Unity ML Agents
ECHO.
ECHO All libraries should be downloaded and extracted before running this batch file. 
ECHO If you continue you will be prompted for the full path of each of the above 
ECHO libraries. 
ECHO.
ECHO Are you ready to continue?
ECHO.
CHOICE /C:YN
IF ERRORLEVEL == 2 GOTO End


:MLAgent

SET /p MLAgentSource=Unity ML Agent Path? 
IF NOT EXIST "%MLAgentSource%\python\learn.py" (
ECHO.
ECHO Unity ML Agents not found at %MLAgentSource%
ECHO.
GOTO MLAgent
)
ECHO Unity ML Agents FOUND
ECHO.


ECHO.
ECHO ===============================================================================
ECHO =                               Linking ML Agents                             =
ECHO ===============================================================================
ECHO.
mklink /J "Assets\ML-Agents" "%MLAgentSource%\unity-environment\Assets\ML-Agents"
ECHO.

PAUSE

:End