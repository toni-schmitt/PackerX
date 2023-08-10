namespace PackerX.Backend.UnitTests.Encoder;

public static class TestData
{
    private const string TestFilesDirectory =
        @"C:\Dev\PackerX\PackerX.Backend.UnitTests\TestFiles";

    public const string OriginalFileName = @$"{TestFilesDirectory}\org-file";

    public static class EncodedTestData
    {
        public const string ActualEncodedFileName =
            $@"{TestFilesDirectory}\actual-enc-file{Constants.EncodingFileExtension}";

        public const string ExpectedEncodedFileName =
            $@"{TestFilesDirectory}\expected-enc-file{Constants.EncodingFileExtension}";
    }

    public static class DecodedTestData
    {
        public const string ActualDecodedFileName =
            $@"{TestFilesDirectory}\actual-decoded-file";
    }

    public static class ErrorTestData
    {
        public const string NonExistentFileName =
            @"X:\this\path\does\not\exists";

        public const string InvalidFileName = @"SL:KDJFSDF:LKSDJFSDF";
    }
}
