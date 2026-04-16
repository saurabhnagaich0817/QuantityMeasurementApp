using QuantityService.Core.DTOs;

namespace QuantityService.Core.Interfaces
{
    public interface IQuantityBusinessService
    {
        Task<QuantityResponse> AddAsync(AddRequest request);
        Task<QuantityResponse> SubtractAsync(SubtractRequest request);
        Task<QuantityResponse> MultiplyAsync(MultiplyRequest request);
        Task<QuantityResponse> DivideAsync(DivideRequest request);
        Task<QuantityResponse> ConvertAsync(ConvertRequest request);
        Task<QuantityResponse> CompareAsync(CompareRequest request);
    }
}