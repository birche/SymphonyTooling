using System.Collections.Generic;

namespace CliCommands.Meta
{
    public class DeploymentMetadata 
    {
        public string PackageFileName { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationVersion { get; set; }
        public DeploymentType DeploymentType { get; set; }
        public Dictionary<string, string> CustomMetadata { get; set; }
    }
}