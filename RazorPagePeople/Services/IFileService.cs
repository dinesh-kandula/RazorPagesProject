namespace RazorPagePeople.Services
{
    public interface IFileService
    {
        Tuple<int, string> SaveImage(IFormFile imageFile);

        bool DeleteImage(string imageFileName);

    }
}
