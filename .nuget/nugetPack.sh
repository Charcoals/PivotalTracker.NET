rm PivotalTrackerDotNet.*.nupkg
NuGet.exe pack PivotalTrackerDotNet/PivotalTrackerDotNet.nuspec
nuget.exe push PivotalTrackerDotNet.*.nupkg