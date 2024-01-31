ECHO OFF

del *.nupkg

dotnet pack -o . 

dotnet nuget push *.nupkg --source="github"


PAUSE