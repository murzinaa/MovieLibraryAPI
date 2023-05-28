var target = Argument("target", "Publish");
var configuration = Argument("configuration", "Release");

var solutionPath = "./";
var outputDir = "FilmsLibrary\\bin\\Release\\net6.0\\publish";

Task("Restore")
    .Does(() => {
        DotNetRestore(solutionPath);
    });

Task("Build")
    .Does(() => {
        DotNetBuild(solutionPath, new DotNetBuildSettings
        {
            Configuration = configuration
        });
    });


Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
        DotNetTest(solutionPath, new DotNetTestSettings
        {
            Configuration = configuration,
            NoBuild = true,
        });
    });

Task("Publish")
    .IsDependentOn("Test")
    .Does(() => {
        DotNetPublish(solutionPath, new DotNetPublishSettings{
            NoRestore = true,
            NoBuild = true,
            Configuration = configuration,
            OutputDirectory = outputDir
        });
    });

RunTarget(target)