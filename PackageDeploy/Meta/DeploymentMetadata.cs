using System.Collections.Generic;

namespace CliCommands.Meta
{
    public class DeploymentMetadata 
    {
        public string PackageFileName { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationVersion { get; set; }
        public string PathToFiles { get; set; }
        public DeploymentType DeploymentType { get; set; }
        public Dictionary<string, string> CustomMetadata { get; set; }
        public string TargetIpAddress { get; set; }
        public string TargetSocketPort { get; set; }
        public string TargetFileUploadUriAddress { get; set; }

    }
}