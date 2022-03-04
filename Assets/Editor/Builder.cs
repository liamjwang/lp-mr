﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityBuilderAction.Input;
using UnityBuilderAction.Reporting;
using UnityBuilderAction.Versioning;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace UnityBuilderAction
{
  static class Builder
  {
    public static void BuildProject()
    {
      // Gather values from args
      var options = ArgumentsParser.GetValidatedOptions();

      // Gather values from project
      var scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(s => s.path).ToArray();

      // Define BuildPlayer Options
      var buildOptions = new BuildPlayerOptions {
        scenes = scenes,
        locationPathName = options["customBuildPath"],
        target = (BuildTarget) Enum.Parse(typeof(BuildTarget), options["buildTarget"]),
      };

      // Set version for this build
      VersionApplicator.SetVersion(options["buildVersion"]);
      VersionApplicator.SetAndroidVersionCode(options["androidVersionCode"]);
      
      // Apply Android settings
      if (buildOptions.target == BuildTarget.Android)
        AndroidSettings.Apply(options);
      
      if (buildOptions.target == BuildTarget.WSAPlayer)
      { 
        EditorUserBuildSettings.SetPlatformSettings("WindowsStoreApps", "CopyReferences", "true");
        Console.WriteLine("Windows Store App CopyReferences set to true");
      }

      // Perform build
      BuildReport buildReport = BuildPipeline.BuildPlayer(buildOptions);

      // Summary
      BuildSummary summary = buildReport.summary;
      StdOutReporter.ReportSummary(summary);

      // Result
      BuildResult result = summary.result;
      StdOutReporter.ExitWithResult(result);
    }
  }
}