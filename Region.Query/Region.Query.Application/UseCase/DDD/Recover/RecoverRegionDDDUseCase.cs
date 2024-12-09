using Region.Query.Communication;
using Region.Query.Communication.Request.Enum;
using Region.Query.Communication.Response;
using Region.Query.Domain.Repositories;

namespace Region.Query.Application.UseCase.DDD.Recover;
public class RecoverRegionDDDUseCase(
    IRegionDDDReadOnlyRepository regionDDDReadOnlyRepository) : IRecoverRegionDDDUseCase
{
    private readonly IRegionDDDReadOnlyRepository _regionDDDReadOnlyRepository = regionDDDReadOnlyRepository;

    public async Task<Result<IEnumerable<ResponseRegionDDDJson>>> Execute()
    {
        var result = await _regionDDDReadOnlyRepository.RecoverAllAsync();

        //return _mapper.Map< Result<IEnumerable<ResponseRegionDDDJson>>>(result);
        return new Result<IEnumerable<ResponseRegionDDDJson>>();
    }

    public async Task<Result<IEnumerable<ResponseRegionDDDJson>>> Execute(RegionRequestEnum request)
    {
        var result = await _regionDDDReadOnlyRepository.RecoverListDDDByRegionAsync(request.GetDescription());

        //return _mapper.Map< Result<IEnumerable<ResponseRegionDDDJson>>>(result);
        return new Result<IEnumerable<ResponseRegionDDDJson>>();
    }
}
