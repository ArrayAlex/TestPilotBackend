using Microsoft.AspNetCore.Mvc;
using TestPilot.Helpers;
using TestPilot.DTOs;

[ApiController]
[Route("[controller]")]
public class RequestController : ControllerBase
{
    private readonly HttpClient _httpClient;

    private readonly HelperMethods _helperMethods;


    public RequestController(IHttpClientFactory httpClientFactory, HelperMethods helperMethods)
    {
        _httpClient = httpClientFactory.CreateClient();
        _helperMethods = helperMethods;
    }

    [HttpPost("call")]
    public async Task<IActionResult> ForwardRequest([FromBody] ForwardRequestDto requestDto)
    {
        if (string.IsNullOrWhiteSpace(requestDto.Url) || !_helperMethods.TryGetHttpMethod(requestDto.Method, out var httpMethod) || requestDto == null)
        {
            return BadRequest("Invalid URL or HTTP method.");
        }

        var requestMessage = new HttpRequestMessage(httpMethod, requestDto.Url)
        {
            Content = httpMethod == HttpMethod.Get ? null : new StringContent(requestDto.Body ?? string.Empty, System.Text.Encoding.UTF8, "application/json")
        };

        try
        {
            var response = await _httpClient.SendAsync(requestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            return Content(responseContent, response.Content.Headers.ContentType?.ToString() ?? "text/plain");
        }
        catch (TaskCanceledException ex)
        {
            return StatusCode(408, $"Request timeout: {ex.Message}");
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Request failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }

    
}

