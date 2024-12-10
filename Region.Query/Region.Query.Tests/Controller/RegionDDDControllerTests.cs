using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Region.Query.Api.Controllers.v1;
using Region.Query.Application.UseCase.DDD.Recover;
using Region.Query.Application.UseCase.DDD;
using Region.Query.Communication;
using Region.Query.Communication.Request;
using Region.Query.Communication.Request.Enum;
using Region.Query.Communication.Response;
using Region.Query.Communication.Response.Enum;
using Region.Query.Exceptions.ExceptionBase;
using Region.Query.Domain.ResultServices;

namespace Region.Query.Tests.Controller;
public class RegionDDDControllerTests
{
    [Fact]
    public async Task RecoverAll_ReturnsOkResult_WhenDataExists()
    {
        // Arrange
        var mockUseCase = new Mock<IRecoverRegionDDDUseCase>();

        var mockResponse = new ResponseListRegionDDDJson(new List<ResponseRegionDDDJson>
        {
            new ResponseRegionDDDJson(11, RegionResponseEnum.Sudeste.GetDescription()),
            new ResponseRegionDDDJson(21, RegionResponseEnum.Sudeste.GetDescription())
        });

        var response = new Communication.Response.Result<ResponseListRegionDDDJson>(
            mockResponse, true, string.Empty);

        mockUseCase.Setup(useCase => useCase.RecoverAllAsync())
                   .ReturnsAsync(response);

        var controller = new RegionDDDController();

        // Act
        var result = await controller.RecoverAll(mockUseCase.Object) as OkObjectResult;

        // Assert
        var actualResult = Assert.IsType<Communication.Response.Result<ResponseListRegionDDDJson>>(result.Value);

        Assert.NotNull(result);
        Assert.True(actualResult.IsSuccess);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }

    [Fact]
    public async Task RecoverAll_ReturnsNoContentResult_WhenNoDataExists()
    {
        // Arrange
        var mockUseCase = new Mock<IRecoverRegionDDDUseCase>();

        mockUseCase.Setup(useCase => useCase.RecoverAllAsync())
                   .ReturnsAsync(new Communication.Response.Result<ResponseListRegionDDDJson>(
            new ResponseListRegionDDDJson(null), true, string.Empty));

        var controller = new RegionDDDController();

        // Act
        var result = await controller.RecoverAll(mockUseCase.Object) as OkObjectResult;

        // Assert
        var actualResult = Assert.IsType<Communication.Response.Result<ResponseListRegionDDDJson>>(result.Value);

        Assert.NotNull(result);
        Assert.True(actualResult.IsSuccess);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }

    [Fact]
    public async Task RecoverAll_ReturnsUnauthorizedResult_WhenUserIsNotAuthorized()
    {
        // Arrange
        var mockUseCase = new Mock<IRecoverRegionDDDUseCase>();

        mockUseCase.Setup(useCase => useCase.RecoverAllAsync())
                   .ThrowsAsync(new UnauthorizedAccessException("Usuário sem permissão"));

        var controller = new RegionDDDController();

        // Act
        var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            controller.RecoverAll(mockUseCase.Object));

        // Assert
        Assert.Equal("Usuário sem permissão", exception.Message);
    }

    [Fact]
    public async Task RecoverByRegion_ReturnsOkResult_WhenDataExists()
    {
        // Arrange
        var mockUseCase = new Mock<IRecoverRegionDDDUseCase>();

        var region = RegionRequestEnum.Sudeste;

        var mockResponse = new ResponseListRegionDDDJson(new List<ResponseRegionDDDJson>
        {
            new ResponseRegionDDDJson(11, RegionResponseEnum.Sudeste.GetDescription()),
            new ResponseRegionDDDJson(21, RegionResponseEnum.Sudeste.GetDescription())
        });

        var response = new Communication.Response.Result<ResponseListRegionDDDJson>(
            mockResponse, true, string.Empty);

        mockUseCase.Setup(useCase => useCase.RecoverListDDDByRegionAsync(region))
                   .ReturnsAsync(response);

        var controller = new RegionDDDController();

        // Act
        var result = await controller.RecoverByRegion(region, mockUseCase.Object) as OkObjectResult;

        // Assert
        var actualResult = Assert.IsType<Communication.Response.Result<ResponseListRegionDDDJson>>(result.Value);

        Assert.NotNull(result);
        Assert.True(actualResult.IsSuccess);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }

    [Fact]
    public async Task RecoverByRegion_ReturnsNoContentResult_WhenNoDataExists()
    {
        // Arrange
        var mockUseCase = new Mock<IRecoverRegionDDDUseCase>();

        var region = RegionRequestEnum.Sudeste;

        mockUseCase.Setup(useCase => useCase.RecoverListDDDByRegionAsync(region))
                   .ReturnsAsync(new Communication.Response.Result<ResponseListRegionDDDJson>(
            new ResponseListRegionDDDJson(null), true, string.Empty));

        var controller = new RegionDDDController();

        // Act
        var result = await controller.RecoverByRegion(region, mockUseCase.Object) as OkObjectResult;

        // Assert
        var actualResult = Assert.IsType<Communication.Response.Result<ResponseListRegionDDDJson>>(result.Value);

        Assert.NotNull(result);
        Assert.True(actualResult.IsSuccess);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }

    [Fact]
    public async Task RecoverByRegion_ReturnsUnauthorizedResult_WhenUserIsNotAuthorized()
    {
        // Arrange
        var mockUseCase = new Mock<IRecoverRegionDDDUseCase>();

        var region = RegionRequestEnum.Sudeste;

        mockUseCase.Setup(useCase => useCase.RecoverListDDDByRegionAsync(region))
                   .ThrowsAsync(new UnauthorizedAccessException("Usuário sem permissão"));

        var controller = new RegionDDDController();

        // Act
        var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            controller.RecoverByRegion(region, mockUseCase.Object));

        // Assert
        Assert.Equal("Usuário sem permissão", exception.Message);
    }
}
