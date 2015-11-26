@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)
 
set version=0.1.0
if not "%PackageVersion%" == "" (
   set version=%PackageVersion%
)

set nuget=
if "%nuget%" == "" (
	set nuget=nuget
)

REM Restoring NuGet packages
call nuget restore src\YAWL.Common.Portable\packages.config -OutputDirectory %cd%\src\packages -NonInteractive

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild src\YAWL.Common.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false

mkdir Build
mkdir Build\lib

%nuget% pack "src\YAWL.Common.Portable\YAWL.Common.Portable.csproj" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"
%nuget% pack "src\YAWL.Common.UWP\YAWL.Common.UWP.csproj" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"