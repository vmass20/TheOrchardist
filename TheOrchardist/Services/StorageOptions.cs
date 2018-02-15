using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheOrchardist.Services
{
  public class StorageOptions
  {
    public String StorageConnectionString { get; set; }
    public String AccountName { get; set; }
    public String AccountKey { get; set; }
    public String DefaultEndpointsProtocol { get; set; }
    public String EndpointSuffix { get; set; }

    public StorageOptions() { }
  }
  public static class AzureStorageConstants
  {

    private static String TableAccount = "account name";
    private static String cloudEndPointFormat = "http://" + "massatstorage" + ".table.core.windows.net/";

    private static String cloudKey = "account key";
    private static string AzureStorage_SharedKeyAuthorizationScheme = "SharedKey";

    public static String Account
    {
      get { return TableAccount; }
    }

    public static string SharedKeyAuthorizationScheme
    {
      get { return AzureStorage_SharedKeyAuthorizationScheme; }
    }

    public static string Key
    {
      get { return cloudKey; }
    }

    public static String TableEndPoint
    {
      get { return cloudEndPointFormat; }
    }

  }
}
