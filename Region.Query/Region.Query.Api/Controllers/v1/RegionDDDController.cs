using Microsoft.AspNetCore.Mvc;
using Region.Query.Api.Filters;
using Region.Query.Application.UseCase.DDD.Recover;
using Region.Query.Communication.Request.Enum;
using Region.Query.Communication.Response;
using System.ComponentModel.DataAnnotations;

namespace Region.Query.Api.Controllers.v1;

[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class RegionDDDController : TechChallengeController
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Result<ResponseRegionDDDJson>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RecoverAll(
        [FromServices] IRecoverRegionDDDUseCase useCase)
    {
        var result = await useCase.Execute();

        if (result.IsSuccess)
            return Ok(result);

        return NoContent();
    }

    [HttpGet]
    [Route("DDD/by-region")]
    [ProducesResponseType(typeof(IEnumerable<ResponseRegionDDDJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RecoverByRegion(
        [FromQuery][Required] RegionRequestEnum region,
        [FromServices] IRecoverRegionDDDUseCase useCase)
    {
        var result = await useCase.Execute(region);

        if (result.IsSuccess)
            return Ok(result);

        return NoContent();
    }
}
