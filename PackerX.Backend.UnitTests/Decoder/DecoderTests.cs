using PackerX.Backend.UnitTests.Encoder;
using Xunit;
using Xunit.Categories;

namespace PackerX.Backend.UnitTests.Decoder;

public class DecoderTests
{
    [Fact]
    [TestCase]
    public void Decode_()
    {
        // Arrange
        Backend.Decoder.Decoder decoder = Backend.Decoder.Decoder.Create(
            new FileInfo(
                TestData.EncodedTestData.ExpectedEncodedFileName
            ),
            new FileInfo(
                TestData.DecodedTestData.ActualDecodedFileName
            )
        );

        // Act
        decoder.Decode();

        decoder.Dispose();

        // Assert
        byte[] expected = File.ReadAllBytes(
            TestData.OriginalFileName
        );

        byte[] actual = File.ReadAllBytes(
            TestData.DecodedTestData.ActualDecodedFileName
        );

        Assert.Equal(
            expected,
            actual
        );
    }
}
