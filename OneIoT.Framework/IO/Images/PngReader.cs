using System.Diagnostics;
using System.IO.Compression;
using System.Net.Mime;
using System.Text;
using OneIoT.Framework.Graphics.Color;
using OneIoT.Framework.Graphics.VisualElements;
using OneIoT.Framework.IO.Files;
using OpenTK.Mathematics;

namespace OneIoT.Framework.IO.Images;

public class PngReader
{
    public FilesInfo FileInfo;
    
    private BinaryReader _dataStream;
    
    public Size ImageSize = new Size();

    public Dictionary<Vector2, Colour> ImageData = new Dictionary<Vector2, Colour>();
    
    public PngReader(string path)
    {
        FileInfo = new Files.FilesInfo(path);
        
        var file = File.Open(path, FileMode.Open);
        _dataStream = new BinaryReader(file, Encoding.UTF8, false);
        
        Decode();
    }

    public void Decode()
    {
        //Decode signature
        SignatureCheck();
        
        List<byte> allIdatData = new List<byte>();  // Store the data from all IDAT chunks

        while (_dataStream.BaseStream.CanRead)
        {
            (string type, byte[] data) = ReadChunk();

            if (type == "IHDR")
            {
                ImageSize.Width = GetIntFromByte(data, 4, 0);
                ImageSize.Height = GetIntFromByte(data, 4, 4);
            }

            if (type == "IEND")
                break;

            if (type == "IDAT")
            {
                allIdatData.AddRange(data);
            }
        }
        DecompressIdatData(allIdatData);
        GetPngData();
    }

    private byte[] _decompressedData;
    
    public void DecompressIdatData(List<byte> idatData)
    {
        try
        {
            // Remove the Zlib header (first 2 bytes)
            byte[] zlibData = idatData.Skip(2).ToArray(); 

            using (var compressedStream = new MemoryStream(zlibData))
            using (var deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
            using (var decompressedStream = new MemoryStream())
            {
                deflateStream.CopyTo(decompressedStream);
                _decompressedData = decompressedStream.ToArray();
            }
        }
        catch (InvalidDataException ex)
        {
            Logger.Log(LoggerType.Warning, "The file data is corrupted...");
        }
    }

    private void GetPngData()
    {
        int offset = 0;

        for (int i = 0; i < ImageSize.Height; i++)
        {
            offset++;
            
            for(int j = 0; j < ImageSize.Width; j++)
            {
                var r = _decompressedData[offset];
                var g = _decompressedData[offset + 1];
                var b = _decompressedData[offset + 2];
                var a = _decompressedData[offset + 3];
                
                ImageData.Add(new Vector2(j, i), new Colour(r, g, b, a));
                offset += 4;
            }
        }
    }
        
    private bool SignatureCheck()
    {
        var signature = new byte[] {0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A,  0x1A,  0x0A };
        var data = _dataStream.ReadBytes(8);

        if (data.Length != signature.Length)
            return false;

        return data.ToArray().Select((d, i) => (d, i)).All(tuple => tuple.d == signature[tuple.i]);
    }

    private (string, byte[]) ReadChunk()
    {
        //Read length data (4 bytes)
        var length = GetIntFromByte(4);

        //Read the type (4 bytes)
        string type = Encoding.ASCII.GetString(_dataStream.ReadBytes(4));

        //Read the data
        byte[] data = _dataStream.ReadBytes(length);

        //Checksum
        byte[] crc = _dataStream.ReadBytes(4);

        return (type, data);
    }

    private int GetIntFromByte(byte[] data, int length, int offset)
    {
        int result = 0;
        int pow = length - 1;

        for (int i = offset; i < length + offset; i++)
        {
            result = data[i] << pow;
            pow--;
        }

        return result;
    }
    
    private int GetIntFromByte(int length)
    {
        int result = 0;
        int b = length -1 ;
        
        for (int i = 0; i < length; i++)
        {
            result = _dataStream.ReadByte() << b;
            b--;
        }

        return result;
    }

}