using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using DataModel;
using TestToolWeb.Interfaces;
using Exception = System.Exception;

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
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/Project/GetProjectList", string.Empty));

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
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/Project/GetProject{id}", string.Empty));

        var dataResponse = new Projects
        {
            ProjectName = string.Empty,
        };

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<Projects>(content, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }

    public async Task<Projects> UpdateConfig(int id, Projects item)
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/Project/UpdateProject/{id}", string.Empty));

        var dataResponse = new Projects
        {
            ProjectName = string.Empty,
        };

        try
        {
            string json = JsonSerializer.Serialize(item, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<Projects>(responseContent, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }

    public async Task<Projects> AddConfig(Projects item)
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/Project/AddProject", string.Empty));

        var dataResponse = new Projects
        {
            ProjectName = string.Empty,
        };

        try
        {
            string json = JsonSerializer.Serialize(item, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<Projects>(responseContent, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }

    public async Task<Projects> DeleteConfig(int id)
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/Project/DeleteProject/{id}", string.Empty));

        var dataResponse = new Projects
        {
            ProjectName = string.Empty,
        };

        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<Projects>(responseContent, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }
}