using BlobStorageDemo.API.DTOs;

namespace BlobStorageDemo.API.Services.Interfaces
{
    public interface IAzureStorage
    {
        Task<BlobResponseDto> UploadAsync(IFormFile file);
        Task<BlobDto> DownloadAsync(string blobFilename);
        Task<BlobResponseDto> DeleteAsync(string blobFilename);
        Task<List<BlobDto>> ListAsync();
    }
}