FROM mono:4.0.0-onbuild

RUN nuget install NUnit.Runners.lite
RUN ls
RUN mono NUnit.Runners.lite.2.6.4.20150512/nunit-console.exe PivotalTrackerDotNet.Tests.dll
