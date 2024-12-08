namespace OneIoT.Framework.IO.Files;

public interface IFileInfo
{
    public string FileName { get; set; }
    
    public string FilePath { get; set; }
    
    public string FileFormat { get; set; }
}