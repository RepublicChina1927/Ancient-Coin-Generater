using Ancient_Coin_Generater.Data;
using Microsoft.EntityFrameworkCore;
using Ancient_Coin_Generater.Pages;

namespace Ancient_Coin_Generater.Models_for_DTO
{
    public class ImageService : IImageService
    {
        private readonly HttpClient _httpClient;

        
        public string TemperatureSelection { get; set; } = "";
        public string ColorOfRustSelection { get; set; } = "";
        public string StoringConditionSelection { get; set; } = "";
        public async Task<string> RegenerateImage()
        {
            var prompt = $"Generate an ancient coin that met following requirements: temperature {TemperatureSelection}, color of rust {ColorOfRustSelection}, storing condition {StoringConditionSelection}.";

            var imageResponse = await CreateImageAsync(new CreateImageRequestDTO { prompt = prompt, Size = "512x512" });

            var imageUrl = imageResponse.Data.FirstOrDefault()?.Url;

            return imageUrl;
        }
        // Example implementation of CreateImageAsync
        public async Task<ImageResponseDTO> CreateImageAsync(CreateImageRequestDTO request)
        {

            // Use the injected HttpClient to send a POST request
            
            var response = await _httpClient.PostAsJsonAsync("/v1/images/generations", request);

            // Process the response
            if (response.IsSuccessStatusCode)
            {
                var imageResponse = await response.Content.ReadFromJsonAsync<ImageResponseDTO>();

                return imageResponse;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API call failed with status code: {response.StatusCode}, Content: {errorContent}");
            }

        }

            public ImageService(HttpClient httpClient)
            {
                _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

                            }

        public async Task<byte[]> GetImageBytesFromUrl(string imageUrl)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(imageUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
                else
                {
                    throw new InvalidOperationException("Unable to fetch image from URL.");
                }
            }
        }
       

       
	}
}

