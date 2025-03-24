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

    #region Project

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
                var dataResponse2 = JsonSerializer.Deserialize<IEnumerable<Projects>>(content, _serializerOptions);
                dataResponse = dataResponse2.ToList();
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
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/Project/GetProject/{id}", string.Empty));

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

    public async Task<Projects> UpdateProject(int id, Projects item)
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

    public async Task<Projects> AddProject(Projects item)
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

    public async Task<Projects> DeleteProject(int id)
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

    #endregion

    #region Suites

    public async Task<List<TestSuites>> GetSuiteList()
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/Suite/GetSuiteList", string.Empty));

        var dataResponse = new List<TestSuites>();

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<List<TestSuites>>(content, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }

    public async Task<List<TestSuites>>GetSuitesListByProject(int projectId)
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/Suite/GetSuitesListByProject/{projectId}", string.Empty));

        var dataResponse = new List<TestSuites>();

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<List<TestSuites>>(content, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }
    public async Task<TestSuites> GetSuite(int id)
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/Suite/GetSuite/{id}", string.Empty));

        
        TestSuites dataResponse = new TestSuites
        {
            Project = null
        };

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<TestSuites>(content, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }
    public async Task<TestSuites> AddSuite(TestSuites suite)
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/Suite/AddSuite", string.Empty));

        var dataResponse = new TestSuites
        {
            Project = null
        };

        try
        {
            string json = JsonSerializer.Serialize(suite, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<TestSuites>(responseContent, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }
    public async Task<TestSuites> UpdateSuite(int id, TestSuites item)
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/Suite/UpdateSuite/{id}", string.Empty));

        var dataResponse = new TestSuites()
        {
            Project = null
        };

        try
        {
            string json = JsonSerializer.Serialize(item, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<TestSuites>(responseContent, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }
    public async Task<TestSuites> DeleteSuite(int id)
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/Suite/DeleteSuite/{id}", string.Empty));

        var dataResponse = new TestSuites()
        {
            Project = null
        };

        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<TestSuites>(responseContent, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }
    
    #endregion
    
    #region TestCases
    
    public async Task<List<TestCases>> GetTestCaseList()
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/Cases/GetTestCaseList", string.Empty));

        var dataResponse = new List<TestCases>();

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<List<TestCases>>(content, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }
    
    public async Task<List<TestCases>> GetTestCaseListBySuite(int suiteId)
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/Cases/GetTestCaseListBySuite/{suiteId}", string.Empty));

        var dataResponse = new List<TestCases>();

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<List<TestCases>>(content, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    } 
    
    public async Task<List<TestCases>> GetTestCaseListByProject(int projectId)
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/Cases/GetCaseListByProject/{projectId}", string.Empty));

        var dataResponse = new List<TestCases>();

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<List<TestCases>>(content, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }
    public async Task<TestCases> GetTestCase(int id)
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/Cases/GetTestCase/{id}", string.Empty));

        var dataResponse = new TestCases
        {
            TestCaseName = string.Empty,
            TestSuite = null,
        };

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<TestCases>(content, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }
    
    public async Task<TestCases> UpdateTestCase(int id, TestCases item)
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/Cases/UpdateTestCase/{id}", string.Empty));

        var dataResponse = new TestCases
        {
            TestCaseName = string.Empty,
            TestSuite = null,
        };

        try
        {
            string json = JsonSerializer.Serialize(item, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<TestCases>(responseContent, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }
    
    public async Task<TestCases> AddTestCase(TestCases item)
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/Cases/AddCase", string.Empty));

        var dataResponse = new TestCases
        {
            TestCaseName = string.Empty,
            TestSuite = null,
        };

        try
        {
            string json = JsonSerializer.Serialize(item, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<TestCases>(responseContent, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }
    
    public async Task<TestCases> DeleteTestCase(int id)
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/Cases/DeleteTestCase/{id}", string.Empty));

        var dataResponse = new TestCases
        {
            TestCaseName = string.Empty,
            TestSuite = null,
        };

        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<TestCases>(responseContent, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }
    
    #endregion
    
    #region TestScripts
    
    public async Task<List<TestScripts>> GetTestScriptList()
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/TestScript/GetTestScriptList", string.Empty));

        var dataResponse = new List<TestScripts>();

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<List<TestScripts>>(content, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }
    
    public async Task<List<TestScripts>> GetTestScriptListByCase(int caseId)
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/TestScript/GetTestScriptListByCase/{caseId}", string.Empty));

        var dataResponse = new List<TestScripts>();

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<List<TestScripts>>(content, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }
    
    public async Task<TestScripts> GetTestScript(int id)
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/TestScript/GetTestScript/{id}", string.Empty));

        var dataResponse = new TestScripts
        {
            TestCase = null,
        };

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<TestScripts>(content, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }
    
    public async Task<TestScripts> UpdateTestScript(int id, TestScripts item)
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/TestScript/UpdateTestScript/{id}", string.Empty));

        var dataResponse = new TestScripts
        {
            TestCase = null,
        };

        try
        {
            string json = JsonSerializer.Serialize(item, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<TestScripts>(responseContent, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }
    
    public async Task<TestScripts> AddTestScript(TestScripts item)
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/TestScript/AddTestScript", string.Empty));

        var dataResponse = new TestScripts
        {
            TestCase = null,
        };

        try
        {
            string json = JsonSerializer.Serialize(item, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<TestScripts>(responseContent, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }
    
    public async Task<TestScripts> DeleteTestScript(int id)
    {
        Uri uri = new Uri(string.Format(GetRestUrl() + $"api/TestScript/DeleteTestScript/{id}", string.Empty));

        var dataResponse = new TestScripts
        {
            TestCase = null,
        };

        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                dataResponse = JsonSerializer.Deserialize<TestScripts>(responseContent, _serializerOptions);
            }

            return dataResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(@"\tERROR {0}", ex.Message);
            return null;
        }
    }
    
    #endregion
}