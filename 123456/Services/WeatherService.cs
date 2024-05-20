

public class WeatherService
{
    private string url = "https://opendata.cwa.gov.tw/api";
    private string key = "CWB-19B07FE3-446E-4B33-8D6C-AB17B097292E";
     
    public async Task<List<WeatherModel>> List()
    {
        List<WeatherModel> result = new List<WeatherModel>();
         
        try
        {
            string path = url + "/v1/rest/datastore/F-D0047-073?Authorization=" + key;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<List<WeatherModel>>();
            }
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp);
            throw;
        }
         
        return result;
    }
}
