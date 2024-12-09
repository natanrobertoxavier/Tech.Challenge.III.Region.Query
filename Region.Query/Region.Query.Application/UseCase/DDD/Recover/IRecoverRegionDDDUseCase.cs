using Region.Query.Communication.Request.Enum;
using Region.Query.Communication.Response;

namespace Region.Query.Application.UseCase.DDD.Recover;
public interface IRecoverRegionDDDUseCase
{
    Task<Result<IEnumerable<ResponseRegionDDDJson>>> Execute();
    Task<Result<IEnumerable<ResponseRegionDDDJson>>> Execute(RegionRequestEnum request);
}
