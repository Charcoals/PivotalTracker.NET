msbuild /t:Rebuild /p:Configuration=Release ..\PivotalTrackerDotNet.sln
del /S /Q PivotalTrackerDotNet\lib\net40\*.*
mkdir PivotalTrackerDotNet\lib\net40
copy /Y ..\PivotalTrackerDotNet\bin\Release\PivotalTrackerDotNet.* PivotalTrackerDotNet\lib\net40
del PivotalTrackerDotNet.*.nupkg
NuGet.exe pack PivotalTrackerDotNet/PivotalTrackerDotNet.nuspec