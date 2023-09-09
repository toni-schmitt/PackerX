using Xunit;
using Xunit.Categories;

namespace PackerX.Backend.UnitTests.UnitTests.Encoder;

[UnitTest]
public class EncoderTests
{


    [Theory]
    [TestCase]
    [MemberData(
        nameof(TestData.TestData.ErrorTestData.InvalidPathsTestData),
        MemberType = typeof(TestData.TestData.ErrorTestData)
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
        Backend.Encoder.Encoder encoder = Backend.Encoder.Encoder.Create(
            new FileInfo(
                TestData.TestData.OriginalFileName
            ),
            new FileInfo(
                TestData.TestData.EncodedTestData.ActualEncodedFileName
            )
        );

        // Act
        encoder.Encode();

        encoder.Dispose();

        // Assert
        byte[] expectedFileOutput = File.ReadAllBytes(
            TestData.TestData.EncodedTestData.ExpectedEncodedFileName
        );

        byte[] actualFileOutput = File.ReadAllBytes(
            TestData.TestData.EncodedTestData.ActualEncodedFileName
        );

        Assert.Equal(
            expectedFileOutput,
            actualFileOutput
        );

    }
}
