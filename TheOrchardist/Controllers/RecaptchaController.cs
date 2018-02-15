using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TheOrchardist.Data;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;


namespace TheOrchardist.Controllers
{
  public class RecaptchaController : Controller
  {
    public ActionResult Index()

    {

      return View();

    }
    [HttpPost]

    public bool IsValidCaptcha()
    {
      var encodedResponse = Request.Form["g-Recaptcha-Response"];
      using (var client = new WebClient())
      {
        var response = client.DownloadString($"https://www.google.com/recaptcha/api/siteverify?secret={ Startup.Configuration["AppSettings:GoogleCaptchaSiteKey"] }&response={ encodedResponse }");
        return JsonConvert.DeserializeObject<CaptchaResponse>(response).Success;
      }

      
    }
  }
}