@echo off

SET Dir=%~dp0

regsvr32.exe /u "%Dir%SKCOM.dll"