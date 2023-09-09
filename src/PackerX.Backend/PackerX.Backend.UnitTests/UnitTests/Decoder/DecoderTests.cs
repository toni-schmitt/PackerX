using Xunit;
using Xunit.Categories;

namespace PackerX.Backend.UnitTests.UnitTests.Decoder;

public class DecoderTests
{
    [Fact]
    [TestCase]
    public void Decode_()
    {
        // Arrange
        Backend.Decoder.Decoder decoder = Backend.Decoder.Decoder.Create(
            new FileInfo(
                TestData.TestData.DecodedTestData.EncodedFileName
            ),
            new FileInfo(
                TestData.TestData.DecodedTestData.ActualDecodedFileName
            )
        );

        // Act
        decoder.Decode();

        decoder.Dispose();

        // Assert
        byte[] expected = File.ReadAllBytes(
            TestData.TestData.OriginalFileName
        );

        byte[] actual = File.ReadAllBytes(
            TestData.TestData.DecodedTestData.ActualDecodedFileName
        );

        Assert.Equal(
            expected,
            actual
        );
    }
}
