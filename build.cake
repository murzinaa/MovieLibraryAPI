var target = Argument("target", "Test");
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

RunTarget(target)