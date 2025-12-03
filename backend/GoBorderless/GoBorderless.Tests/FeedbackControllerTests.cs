using System.Net;
using System.Text;
using GoBorderless.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace GoBorderless.Tests;

public class FeedbackControllerTests : IAsyncLifetime
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;

    public Task InitializeAsync()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        _factory.Dispose();
        _client.Dispose();
        return Task.CompletedTask;
    }
    
    [Fact]
    public async Task PostFeedback_ReturnsSuccess()
    {
        var request = new FeedbackRequest
        {
            Rating = 2,
            Comment = "This is a comment",
        };
        
        var requestBody = JsonConvert.SerializeObject(request);
        var stringContent = new StringContent(requestBody, Encoding.UTF8, "application/json");
        var result = await _client.PostAsync("/api/Feedback", stringContent);
        
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }
    
    [Fact]
    public async Task PostInvalidFeedback_ReturnsValidationProblem()
    {
        var request = new FeedbackRequest
        {
            Rating = 6,
            Comment = null,
        };
        
        var requestBody = JsonConvert.SerializeObject(request);
        var stringContent = new StringContent(requestBody, Encoding.UTF8, "application/json");
        var result = await _client.PostAsync("/api/Feedback", stringContent);
        
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}