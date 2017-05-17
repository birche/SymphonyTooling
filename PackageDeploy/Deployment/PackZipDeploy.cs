using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text;
using CliCommands.Meta;
using Microsoft.Build.Utilities;
using Newtonsoft.Json.Bson;

namespace PackageDeploy.Deployment
{
    public class PackZipDeploy : TargetDeploy
    {
        private string CreateArchive(DeploymentMetadata metadata)
        {
            var filename = metadata.PackageFileName.Contains(".zip")
                ? metadata.PackageFileName
                : $"{metadata.PackageFileName}.zip";
            if (File.Exists(filename))
                File.Delete(filename);
            var tempFilename = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            try
            {
                ZipFile.CreateFromDirectory(metadata.PathToFiles, tempFilename);
                File.Copy(tempFilename, filename);
                return filename;
            }
            finally
            {
                File.Delete(tempFilename);
            }
        }

        private void TransferArchive(DeploymentMetadata metadata, string filename)
        {
            HttpContent streamContent = new StreamContent(File.OpenRead(filename));
            using (var client = new HttpClient())
            {
                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(streamContent, "files", Path.GetFileName(filename));
                    HttpResponseMessage response = client.PostAsync(new Uri(metadata.TargetFileUploadUriAddress), formData).Result;
                    if (!response.IsSuccessStatusCode)
                        Debug.WriteLine("Failed to send");

                }
            }
        }

        public override void Deploy(DeploymentMetadata metadata)
        {
            var packageFilename = CreateArchive(metadata);
            if (string.IsNullOrEmpty(packageFilename))
            {
                throw new FileNotFoundException("Failed to create zip archive from output");
            }
            TransferArchive(metadata, packageFilename);
        }
    }
}
