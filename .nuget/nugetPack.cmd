msbuild /t:Rebuild /p:Configuration=Release ..\PivotalTrackerDotNet.sln
del /S /Q PivotalTrackerDotNet\lib\net452\*.*
mkdir PivotalTrackerDotNet\lib\net452
copy /Y ..\PivotalTrackerDotNet\bin\Release\PivotalTrackerDotNet.* PivotalTrackerDotNet\lib\net452
del PivotalTrackerDotNet.*.nupkg
NuGet.exe pack PivotalTrackerDotNet/PivotalTrackerDotNet.nuspec