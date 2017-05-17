using System;
using CliCommands.Meta;
using Microsoft.Extensions.DependencyModel;
using PackageDeploy;

namespace PackageDeploy.Deployment
{
    public abstract class TargetDeploy : IDeviceDeploy
    {
        public abstract void Deploy(DeploymentMetadata metadata);

        public virtual bool Validate()
        {
            return true;
        }
    }
}