using PackerX.Backend.TranscodingContexts;

namespace PackerX.Backend.Decoder;

public class Decoder : IDecoder
{
    private readonly BinaryWriter _decodedBinaryWriter;
    private readonly FileStream _decodedFileStream;
    private readonly BinaryReader _originalBinaryReader;
    private readonly FileStream _originalFileStream;

    public Decoder(
        FileSystemInfo originalFileInfo,
        FileSystemInfo decodedFileInfo
    )
    {
        _originalFileStream = File.Open(
            originalFileInfo.FullName,
            FileMode.Open
        );

        _originalBinaryReader = new BinaryReader(
            _originalFileStream
        );

        _decodedFileStream = File.Open(
            decodedFileInfo.FullName,
            FileMode.Create
        );

        _decodedBinaryWriter = new BinaryWriter(
            _decodedFileStream
        );
    }

    public void Decode()
    {
        if (HasMagicHeader(
                Constants.MagicHeader
            ) is false)
        {
            throw new InvalidOperationException(
                "Cannot decode an invalid file"
            );
        }

        while (_originalFileStream.IsEndOfFile() is false)
        {
            DecodingContext sameByteContext = new(
                _originalBinaryReader
            )
            {
                OriginalFileLength = _originalFileStream.Length
            };

            _ = sameByteContext.ReadSection();

            WriteSection(
                sameByteContext
            );
        }
    }

    public void Dispose()
    {
        _originalFileStream.Dispose();
        _originalBinaryReader.Dispose();
        _decodedFileStream.Dispose();
        _decodedBinaryWriter.Dispose();

        GC.SuppressFinalize(
            this
        );
    }

    private void WriteSection(DecodingContext sameByteContext)
    {
        byte[] toWrite = sameByteContext.IsEncodedSection switch
        {
            true => Enumerable.Repeat(
                    sameByteContext.FirstByte,
                    sameByteContext.SectionLength
                )
                .ToArray(),
            false => new[]
            {
                sameByteContext.FirstByte
            }
        };

        _decodedBinaryWriter.Write(
            toWrite
        );
    }

    private bool HasMagicHeader(IReadOnlyCollection<byte> expectedMagicHeader)
    {
        byte[] actualMagicHeader = _originalBinaryReader.ReadBytes(
            expectedMagicHeader.Count
        );

        return expectedMagicHeader.SequenceEqual(
            actualMagicHeader
        );
    }
}
