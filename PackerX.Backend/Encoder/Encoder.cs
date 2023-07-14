using Dawn;
using PackerX.Backend.Extensions;
using PackerX.Backend.TranscodingContexts;

namespace PackerX.Backend.Encoder;

public class Encoder : IEncoder
{
    private readonly BinaryWriter _encodedBinaryWriter;
    private readonly FileStream _encodedFileStream;
    private readonly BinaryReader _originalBinaryReader;
    private readonly FileStream _originalFileStream;

    private Encoder(
        FileSystemInfo originalFileInfo,
        FileSystemInfo encodedFileInfo
    )
    {
        _originalFileStream = File.OpenRead(
            originalFileInfo.FullName
        );

        _originalBinaryReader = new BinaryReader(
            _originalFileStream
        );

        _encodedFileStream = File.OpenWrite(
            encodedFileInfo.FullName
        );

        _encodedBinaryWriter = new BinaryWriter(
            _encodedFileStream
        );

    }

    // private byte GetLeastUsedByte()
    // {
    //     Dictionary<byte, int> bytesByCount = new();
    //
    //     while (_originalFileStream.IsEndOfFile() is false)
    //     {
    //         byte readByte = _originalBinaryReader.ReadByte();
    //
    //         if (bytesByCount.TryAdd(
    //                 readByte,
    //                 1
    //             ) is false)
    //         {
    //             bytesByCount[readByte] += 1;
    //         }
    //     }
    //
    //     _originalFileStream.ResetPosition();
    //
    //     return bytesByCount.MinBy(
    //             pair => pair.Value
    //         )
    //         .Key;
    // }

    public void Encode()
    {
        WriteMagicHeader(
            Constants.MagicHeader
        );

        while (_originalFileStream.IsEndOfFile() is false)
        {
            EncodingContext sameByteContext = new(
                _originalBinaryReader
            )
            {
                OriginalFileLength = _originalFileStream.Length
            };

            TranscodingContextResult transcodingContextResult =
                sameByteContext.ReadSection();

            if (transcodingContextResult.IsEndOfFile())
            {
                _encodedBinaryWriter.Write(
                    sameByteContext.FirstByte
                );

                return;
            }

            WriteSection(
                sameByteContext
            );
        }
    }

    public void Dispose()
    {
        _originalBinaryReader.Dispose();
        _originalFileStream.Dispose();
        _encodedBinaryWriter.Dispose();
        _encodedFileStream.Dispose();

        GC.SuppressFinalize(
            this
        );
    }

    public static Encoder Create(
        FileSystemInfo originalFileInfo,
        FileSystemInfo encodedFileInfo
    )
    {
        Guard.Argument(
                originalFileInfo
            )
            .NotNull()
            .FileExists();

        Guard.Argument(
                encodedFileInfo
            )
            .NotNull()
            .ValidPath();

        return new Encoder(
            originalFileInfo,
            encodedFileInfo.AddEncodingExtension()
        );
    }

    private void WriteMagicHeader(byte[] magicHeader)
        => _encodedBinaryWriter.Write(
            magicHeader
        );

    private void WriteSection(EncodingContext encodingContext)
    {
        if (encodingContext.CanBeEncoded)
        {
            _encodedBinaryWriter.Write(
                Constants.Marker
            );

            _encodedBinaryWriter.Write(
                encodingContext.FirstByte
            );

            _encodedBinaryWriter.Write(
                encodingContext.SectionLength
            );

            return;
        }

        _encodedBinaryWriter.Write(
            encodingContext.ToByteArray()
        );
    }
}
