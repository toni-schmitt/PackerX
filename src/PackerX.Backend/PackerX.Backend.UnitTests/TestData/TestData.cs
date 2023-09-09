using System.Reflection;

namespace PackerX.Backend.UnitTests.TestData;

public static class TestData
{

    private static readonly string? s_baseDirectory = GetBaseDirectory();

    private static readonly string s_testFilesDirectory =
        Path.Join(
            s_baseDirectory,
            "TestData\\TestFiles"
        );

    public static readonly string OriginalFileName = Path.Join(
        s_testFilesDirectory,
        "org-file"
    );

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
            Path.Join(
                s_testFilesDirectory,
                "Encoder"
            );

        public static readonly string ActualEncodedFileName =
            Path.Join(
                s_base,
                $"actual-enc-file{Constants.EncodingFileExtension}"
            );

        public static readonly string ExpectedEncodedFileName =
            Path.Join(
                s_base,
                "expected-enc-file.ttpack"
            );
    }

    public static class DecodedTestData
    {
        private static readonly string
            s_base = Path.Join(
                s_testFilesDirectory,
                "Decoder"
            );

        public static readonly string EncodedFileName =
            Path.Join(
                s_base,
                "enc-file.ttpack"
            );

        public static readonly string ActualDecodedFileName =
            Path.Join(
                s_base,
                "actual-decoded-file"
            );
    }

    public static class ErrorTestData
    {

        public const string InvalidFileName = "SL:KDJFSDF:LKSDJFSDF";

        public const string NonExistentFileName =
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
