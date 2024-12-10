namespace DAL.Abstract
{
    public interface IFileRepository
    {
        public string UploadFile(string fileName, Stream fileContent);
        public Stream GetFile(string fileName);
    }
}
