// ReSharper disable once CheckNamespace - this namespace is intended
namespace System.IO;

public static class FileStreamExtensions
{
    public static bool IsEndOfFile(this FileStream fileStream)
        => fileStream.Position == fileStream.Length;

    public static void ResetPosition(this FileStream fileStream)
        => fileStream.Position = 0;
}
