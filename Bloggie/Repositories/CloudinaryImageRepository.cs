using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Bloggie.Repositories
{
    public class CloudinaryImageRepository: IImageRepository
    {
        private readonly IConfiguration _configuration;
        private readonly Account account;

        public CloudinaryImageRepository(IConfiguration configuration)

        {
            _configuration = configuration;
            account = new Account(
                _configuration["Cloudinary:CloudName"],
                _configuration["Cloudinary:ApiKey"],
                _configuration["Cloudinary:ApiSecret"]
                );
        }

        public async Task<string> UploadAsync(IFormFile formFile)
        {
            var client = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(formFile.FileName, formFile.OpenReadStream()),
                DisplayName = formFile.FileName
            };

            var uploadResult = await client.UploadAsync(uploadParams);

            if (uploadResult != null && uploadResult.StatusCode == HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }

            return null;
        }
    }
}
