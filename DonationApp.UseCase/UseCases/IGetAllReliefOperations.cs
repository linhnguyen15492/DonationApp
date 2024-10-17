using DonationApp.UseCase.Dtos;

namespace DonationApp.UseCase.UseCases
{
    public interface IGetAllReliefOperations
    {
        Task<IEnumerable<ReliefOperationDto>> GetAllReliefOperations();
    }
}
