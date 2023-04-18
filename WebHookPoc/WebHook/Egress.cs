namespace WebHookPoc.WebHook
{
    public class Egress : IEgress
    {
        public async Task<HttpResponseMessage> SendAnalyticDataAsync(Dictionary<string, string> analyticData, HttpClient httpClient)
        {
              string data = Newtonsoft.Json.JsonConvert.SerializeObject(analyticData);
              var content = new StringContent(data);
              var responce = await httpClient.PostAsync("", content);
              return responce;
        }
    }
}
