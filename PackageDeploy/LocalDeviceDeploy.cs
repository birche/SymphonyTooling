using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using CliCommands.Meta;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using PackageDeploy.Deployment;

namespace PackageDeploy
{
    public class LocalDeviceDeploy : Microsoft.Build.Utilities.Task
    {
        public string Format { get; set; }
        public string PublishedOutput { get; set; }
        public string PackageName { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationVersion { get; set; }
        public string Rid { get; set; }
        public string MetadataFile { get; set; }
        public ITaskItem AdditionalMetadata { get; set; }
        public bool IsPortable => string.IsNullOrEmpty(Rid);

        public override bool Execute()
        {
            Debugger.Launch();
            var deployer = new PackZipDeploy();
            var metadata = GetMetadata();
            if (!deployer.Validate())
            {
                Log.LogError($"Not valid IDeviceDeploy instance");
                return false;
            }

            Log.LogMessage(MessageImportance.Normal, $"Chosen deployer: {Format}; published output is {PublishedOutput}");

            try
            {
                deployer.Deploy(metadata);
                Log.LogMessage(MessageImportance.High, $"Deployment complete!");
                return true;

            }
            catch (Exception e)
            {
                Log.LogError($"An error happened: {e.Message}");

                return false;
            }

        }

        private DeploymentMetadata GetMetadata()
        {
            var result = new DeploymentMetadata();
            result.DeploymentType = IsPortable ? DeploymentType.FrameworkDependent : DeploymentType.SelfContained;
            result.ApplicationName = ApplicationName;
            result.ApplicationVersion = ApplicationVersion;
            if (string.IsNullOrEmpty(PackageName))
            {
                result.PackageFileName = Path.Combine(PublishedOutput,
                    $"{result.ApplicationName}-{result.ApplicationVersion}");
            }
            else
            {
                result.PackageFileName = PackageName;
            }
            result.PathToFiles = PublishedOutput;

            return result;
        }

    }
}
