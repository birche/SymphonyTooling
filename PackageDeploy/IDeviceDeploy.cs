using System;
using CliCommands.Meta;

namespace PackageDeploy
{
    public interface IDeviceDeploy 
    {
        void Deploy(DeploymentMetadata metadata);
        bool Validate();
    }
}
