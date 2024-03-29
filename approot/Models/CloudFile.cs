﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Blob;

namespace HowToUseAzureBlobStrage.Models
{
    public class CloudFile
    {
        public string FileName { get; set; }

        public string URL { get; set; }

        public long Size { get; set; }

        public static CloudFile CreateFrom(IListBlobItem item)
        {
            if (item is CloudBlockBlob)
            {
                var blob = (CloudBlockBlob)item;
                return new CloudFile
                {
                    FileName = blob.Name,
                    URL = blob.Uri.ToString(),
                    Size = blob.Properties.Length
                };
            }
            return null;
        }
    }
}