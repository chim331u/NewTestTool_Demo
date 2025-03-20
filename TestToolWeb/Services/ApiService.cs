using System.Text.Json;
using System.Text.Json.Serialization;
using DataModel;
using TestToolWeb.Interfaces;

namespace TestToolWeb.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly IConfiguration _config;
    
    public ApiService(IConfiguration config)
    {
        _httpClient = new HttpClient();

        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            NumberHandling =
                JsonNumberHandling.AllowReadingFromString |
                JsonNumberHandling.WriteAsString,
            ReadCommentHandling = JsonCommentHandling.Skip
        };

        _config = config;
    }

    /// <summary>
    /// Ottiene l'URL di base per le richieste REST.
    /// </summary>
    /// <returns>Una stringa con l'URL di base.</returns>
    public string GetRestUrl()
    {
        var uri = _config.GetSection("Uri").Value;
        return uri;
    }
    
    public async Task<List<Projects>> GetProjectList()
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/GetProjectList", string.Empty));

        var dataResponse = new List<Projects>();

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<List<Projects>>(content, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }

    public async Task<Projects> GetProject(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Projects> UpdateConfig(Projects item)
    {
        throw new NotImplementedException();
    }

    public async Task<Projects> AddConfig(Projects item)
    {
        throw new NotImplementedException();
    }

    public async Task<Projects> DeleteConfig(int id)
    {
        throw new NotImplementedException();
    }
}