using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace TheOrchardist.Controllers
{
  public class BlobsController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
    private CloudBlobContainer GetCloudBlobContainer()
    {
      CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
              Startup.Configuration["AppSettings:massatstorage_AzureStorageConnectionString"]);
      CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
      CloudBlobContainer container = blobClient.GetContainerReference("test-blob-container");
      return container;

    }
    public ActionResult CreateBlobContainer()
    {
      CloudBlobContainer container = GetCloudBlobContainer();
      ViewBag.Success = container.CreateIfNotExistsAsync();
      ViewBag.BlobContainerName = container.Name;
      return View();
    }
  }
}