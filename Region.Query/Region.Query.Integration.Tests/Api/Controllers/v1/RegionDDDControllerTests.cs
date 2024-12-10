using FluentAssertions;
using System.Net;

namespace Region.Query.Integration.Tests.Api.Controllers.v1;
public class RegionDDDControllerTests() : BaseTestClient("")
{
    private const string URI_REGION_DDD = "/api/v1/regionddd";
    private const string VALID_EMAIL = "natan@email.com";

    [Fact]
    public async Task RegionDDDController_OK_WhenDDDNotFound()
    {
        // Arrange
        var token = GetValidToken(VALID_EMAIL);

        // Act
        var response = await GetRequest(URI_REGION_DDD, token);

        // Assert
        var result = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNullOrEmpty();
        result.Contains("DDD");
        result.Contains("Region");
    }

    [Fact]
    public async Task RegionDDDController_Unauthorized_WhenInvalidToken()
    {
        // Arrange
        var token = string.Empty;

        // Act
        var response = await GetRequest(URI_REGION_DDD, token);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
