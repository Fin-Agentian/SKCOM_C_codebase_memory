@echo off

SET Dir=%~dp0

ver | findstr /i "5\.1\."
if %ERRORLEVEL% EQU 0 (
::echo OS = Windows XP x86
regsvr32.exe /u "%Dir%SKCOM.dll"
goto end
)

%systemroot%\SysWoW64\regsvr32.exe /u "%Dir%SKCOM.dll"
