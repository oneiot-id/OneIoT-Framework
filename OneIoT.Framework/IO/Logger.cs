namespace OneIoT.Framework.IO;

public enum LoggerType
{
    Warning,
    Ok,
    Danger,
    Info
}

public static class Logger
{
    public static void Log(LoggerType type, string message)
    {
        switch (type)
        {
            case LoggerType.Warning:
                Console.WriteLine($"[WARNING] : {message}");
                break;
            case LoggerType.Info:
                Console.WriteLine($"[INFO] : {message}");
                break;
        }
    }
}