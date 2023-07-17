namespace PackerX.Backend;

public static class Constants
{
    public const byte Marker = (byte)'>';
    public const string EncodingFileExtension = ".ttpack";
    public static readonly byte[] MagicHeader = "ttpack"u8.ToArray();
}
