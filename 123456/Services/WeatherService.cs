

public class WeatherService
{
    private string url = "https://opendata.cwa.gov.tw/api";
    private string key = "CWB-19B07FE3-446E-4B33-8D6C-AB17B097292E";
     
    public async Task<WeatherModel> List()
    {
        WeatherModel result = new WeatherModel();
         
        try
        {
            string path = url + "/v1/rest/datastore/F-D0047-073?Authorization=" + key+"&locationName=%E8%A5%BF%E5%B1%AF%E5%8D%80&elementName=PoP12h";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<WeatherModel>();
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
