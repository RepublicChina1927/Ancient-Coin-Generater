namespace Ancient_Coin_Generater.Models_for_DTO
{
    public interface IImageService
    {
       

        Task<ImageResponseDTO> CreateImageAsync(CreateImageRequestDTO request);
    }
}
