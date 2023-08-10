using System.Reflection;

namespace PackerX.Backend.UnitTests;

public static class TestData
{

    private static readonly string? s_baseDirectory = GetBaseDirectory();

    private static readonly string s_testFilesDirectory =
        @$"{s_baseDirectory}\TestFiles";

    public static readonly string OriginalFileName =
        @$"{s_testFilesDirectory}\org-file";

    private static string GetBaseDirectory()
    {
        string? assemblyDirectory = Assembly.GetAssembly(
                typeof(TestData)
            )
            ?.Location;

        if (assemblyDirectory is null)
        {
            throw new InvalidOperationException(
                "Assembly cannot be null"
            );
        }

        return assemblyDirectory[..assemblyDirectory.LastIndexOf(
            "bin",
            StringComparison.Ordinal
        )];
    }

    public static class EncodedTestData
    {
        private static readonly string s_base =
            $@"{s_testFilesDirectory}\Encoder";

        public static readonly string ActualEncodedFileName =
            $@"{s_base}\actual-enc-file{Constants.EncodingFileExtension}";

        public static readonly string ExpectedEncodedFileName =
            $@"{s_base}\expected-enc-file{Constants.EncodingFileExtension}";
    }

    public static class DecodedTestData
    {
        private static readonly string
            s_base = $@"{s_testFilesDirectory}\Decoder";

        public static readonly string EncodedFileName =
            $@"{s_base}\enc-file.ttpack";

        public static readonly string ActualDecodedFileName =
            $@"{s_base}\actual-decoded-file";
    }

    public static class ErrorTestData
    {

        public const string InvalidFileName = @"SL:KDJFSDF:LKSDJFSDF";

        public static readonly string NonExistentFileName =
            @"X:\this\path\does\not\exists";

        public static IEnumerable<object[]> InvalidPathsTestData => new[]
        {
            new object[]
            {
                NonExistentFileName,
                OriginalFileName
            },
            new object[]
            {
                OriginalFileName,
                InvalidFileName
            }
        };
    }
}
