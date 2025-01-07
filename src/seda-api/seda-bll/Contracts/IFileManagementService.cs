namespace seda_bll.Contracts;

public interface IFileManagementService
{
    void CheckFileType();
    Task UploadFile(string fileName, string contentType, Stream documentStream);
    Task DownloadFile();
}