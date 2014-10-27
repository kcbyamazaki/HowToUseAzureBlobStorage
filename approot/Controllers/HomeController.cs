using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using HowToUseAzureBlobStrage.Models;

namespace HowToUseAzureBlobStrage.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            // 1. Connect to the Strage Account using the Connection String
            var strageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));
            // 2. Create a Storage Client
            var strageClient = strageAccount.CreateCloudBlobClient();
            // 3. Create Storage Container from the Storage Client
            var strageContainer = strageClient.GetContainerReference(
                ConfigurationManager.AppSettings.Get("CloudStorageContainerReference"));
            strageContainer.CreateIfNotExists();
            // 4. Create a CloudFilesModel by passing the ListBlobs in the Container 
            var blobsList = new CloudFileModel(strageContainer.ListBlobs(useFlatBlobListing: true));
            return View(blobsList);
        }

        public ActionResult UploadFile()
        {
            if (Request.Files.Count > 0)
            {
                var storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionString"));
                var storageClient = storageAccount.CreateCloudBlobClient();
                var storageContainer = storageClient.GetContainerReference(
                   ConfigurationManager.AppSettings.Get("CloudStorageContainerReference"));
                storageContainer.CreateIfNotExists();
                for (var i = 0; i < Request.Files.Count; i++)
                {
                    var fileName = default(string);
                    if (Request.Files[i] != null && Request.Files[i].ContentLength > 0)
                    {
                        fileName = Path.GetFileName(Request.Files[i].FileName);
                        var azureBlockBlob = storageContainer.GetBlockBlobReference(fileName);
                        azureBlockBlob.UploadFromStream(Request.Files[i].InputStream);
                    }
                }
                return RedirectToAction("Index");
            }
            return View("UploadFile");
        }

    }
}
