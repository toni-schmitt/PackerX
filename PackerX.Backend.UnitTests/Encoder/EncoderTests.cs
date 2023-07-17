using Xunit;
using Xunit.Categories;

namespace PackerX.Backend.UnitTests.Encoder;

[UnitTest]
public class EncoderTests
{
    [Theory]
    [TestCase]
    [InlineData(
        TestData.ErrorTestData.NonExistentFileName,
        "x"
    )]
    [InlineData(
        TestData.OriginalFileName,
        TestData.ErrorTestData.InvalidFileName
    )]
    public void Create_Must_Throw_On_Invalid_Paths(
            string originalFilePath,
            string encodedFilePath
        )
        // Assert
        => Assert.ThrowsAny<Exception>(
            () => Backend.Encoder.Encoder.Create(
                new FileInfo(
                    originalFilePath
                ),
                new FileInfo(
                    encodedFilePath
                )
            )
        );

    [Fact]
    [TestCase]
    public void Encode_Produces_Valid_File()
    {
        // Arrange
        using Backend.Encoder.Encoder encoder = Backend.Encoder.Encoder.Create(
            new FileInfo(
                TestData.OriginalFileName
            ),
            new FileInfo(
                TestData.EncodedTestData.ActualEncodedFileName
            )
        );

        // Act
        encoder.Encode();

        encoder.Dispose();

        // Assert
        byte[] expectedFileOutput = File.ReadAllBytes(
            TestData.EncodedTestData.ExpectedEncodedFileName
        );

        byte[] actualFileOutput = File.ReadAllBytes(
            TestData.EncodedTestData.ActualEncodedFileName
        );

        Assert.Equal(
            expectedFileOutput,
            actualFileOutput
        );

    }
}
