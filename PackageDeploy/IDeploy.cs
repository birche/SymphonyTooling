using System;

namespace PackageDeploy
{
    public interface IDeviceDeploy 
    {
        void Deploy();
        void Validate();
    }
}
