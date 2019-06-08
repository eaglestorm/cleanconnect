//#tool "nuget:?package=xunit.runner.console"
#addin "nuget:?package=Cake.Incubator&version=3.0.0"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

var framework = Argument("framework", "netcoreapp2.1");

var solutionFile = "./CleanConnectWeb.sln";
var config = "Debug";

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./CleanConnect.Web/bin/Debug/netcoreapp2.1") + Directory(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")    
    .Does(() =>
{
    DotNetCoreRestore("CleanConnectWeb.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    var settings = new DotNetCoreBuildSettings
    {
        Framework = framework,
        Configuration = config,
        NoDependencies = false,
        OutputDirectory = "./CleanConnect.Web/bin/" + config + "/" + framework + "/"
    };
 
        DotNetCoreBuild(solutionFile, settings);
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetCoreTest("./CleanConnect.Core.Test/CleanConnect.Core.Test.csproj");
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Run-Unit-Tests");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
