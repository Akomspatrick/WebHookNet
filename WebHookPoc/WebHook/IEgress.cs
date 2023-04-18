namespace WebHookPoc.WebHook
{
    public interface IEgress
    {
      public  Task<HttpResponseMessage> SendAnalyticDataAsync(Dictionary<string, string> analyticData , HttpClient httpClient);
    }
}
