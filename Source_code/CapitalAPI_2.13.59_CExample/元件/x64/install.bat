@echo off

SET Dir=%~dp0

regsvr32.exe "%Dir%SKCOM.dll"
