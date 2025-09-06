namespace Bloggie.Repositories
{
    public class CloudinaryImageRepository: IImageRepository
    {
        public Task<string> UploadAsync(IFormFile formFile)
        {
            throw new NotImplementedException();
        }
    }
}
