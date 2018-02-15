using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TheOrchardist.Controllers
{
  public class CaptchaResponse
  {
    public static bool Validate(string EncodedResponse)
    {
      var client = new System.Net.WebClient();

      string PrivateKey = Startup.Configuration["AppSettings:GoogleCaptchaSecretKey"];
     // string PrivateKey = Startup.Configuration["AppSettings:GoogleCaptchaSecretKey"];
      var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse));

      var captchaResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<CaptchaResponse>(GoogleReply);

      return captchaResponse.Success;
    }

    [JsonProperty("success")]
    public bool Success
    {
      get { return m_Success; }
      set { m_Success = value; }
    }

    private bool m_Success;
    [JsonProperty("error-codes")]
    public List<string> ErrorCodes
    {
      get { return m_ErrorCodes; }
      set { m_ErrorCodes = value; }
    }


    private List<string> m_ErrorCodes;


  }
}
