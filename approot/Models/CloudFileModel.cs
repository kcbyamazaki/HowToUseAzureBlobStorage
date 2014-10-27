using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Blob;

namespace HowToUseAzureBlobStrage.Models
{
    public class CloudFileModel
    {
        public List<CloudFile> Files { get; set; }

        public CloudFileModel()
            : this(null)
        {
            Files = new List<CloudFile>();
        }

        public CloudFileModel(IEnumerable<IListBlobItem> fileList)
        {
            Files = new List<CloudFile>();
            if (fileList != null && fileList.Count() > 0)
            {
                Files = fileList
                    .Select(_ => CloudFile.CreateFrom(_))
                    .ToList();
            }
        }
    }
}