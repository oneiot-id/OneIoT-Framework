namespace OneIoT.Framework.IO.Files;

public class FilesInfo : IFileInfo
{
    public FilesInfo(string fileName, string filePath, string fileFormat)
    {
        FileName = fileName;
        FilePath = filePath;
        FileFormat = fileFormat;
    }

    public FilesInfo(string path)
    {
        FileName = Path.GetFileNameWithoutExtension(path);
        FilePath = path;
        FileFormat = Path.GetExtension(path);
    }

    public string FileName { get; set; }
    
    public string FilePath { get; set; }
    
    public string FileFormat { get; set; }
}