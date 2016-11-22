@ECHO OFF

SET configuration=%1
IF "%configuration%" == "" SET configuration=Debug

pushd "%~dp0/.."
IF NOT EXIST TestResults mkdir TestResults
whoLetTheGoatsOut\NUnit\nunit-console\nunit3-console.exe /config:%configuration% whoLetTheGoatsOut\goat.nunit /result:"TestResults\results.xml;format=nunit2" /noheader
popd
EXIT /B %ERRORLEVEL%
