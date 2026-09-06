@echo off
setlocal EnableExtensions EnableDelayedExpansion

set "PROJECT_DIR=%~dp0..\src\ERP.SharedKernel"
set "PROJECT_FILE=%PROJECT_DIR%\ERP.SharedKernel.csproj"
set "VERSION_FILE=%~dp0..\version.txt"
set "BASE_VERSION=1.0.0"
if exist "%VERSION_FILE%" (
    for /f "usebackq delims=" %%I in ("%VERSION_FILE%") do set "BASE_VERSION=%%I"
)
set "OUTPUT_DIR=%~dp0..\..\..\..\artifacts\nuget"
if not exist "%OUTPUT_DIR%" mkdir "%OUTPUT_DIR%"
set "BUILD_NUMBER=1"

:find_next_version
if exist "%OUTPUT_DIR%\ERP.SharedKernel.%BASE_VERSION%-dev.%BUILD_NUMBER%.nupkg" (
    set /a BUILD_NUMBER+=1
    goto find_next_version
)

set "PACKAGE_VERSION=%BASE_VERSION%-dev.%BUILD_NUMBER%"

echo Packing %PACKAGE_VERSION%
dotnet pack "%PROJECT_FILE%" -c Release -o "%OUTPUT_DIR%" -p:PackageVersion=%PACKAGE_VERSION%
if errorlevel 1 exit /b %errorlevel%
echo Packed %PACKAGE_VERSION%
