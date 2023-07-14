using PackerX.Backend.TranscodingContexts;

namespace PackerX.Backend.Encoder;

public class Encoder : IEncoder
{
    private readonly BinaryWriter _encodedBinaryWriter;
    private readonly FileStream _encodedFileStream;
    private readonly BinaryReader _originalBinaryReader;
    private readonly FileStream _originalFileStream;

    public Encoder(
        FileSystemInfo originalFileInfo,
        FileSystemInfo encodedFileInfo
    )
    {
        _originalFileStream = File.Open(
            originalFileInfo.FullName,
            FileMode.Open
        );

        _originalBinaryReader = new BinaryReader(
            _originalFileStream
        );

        _encodedFileStream = File.Open(
            encodedFileInfo.FullName,
            FileMode.Create
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
        _originalFileStream.Dispose();
        _originalBinaryReader.Dispose();
        _encodedFileStream.Dispose();
        _encodedBinaryWriter.Dispose();

        GC.SuppressFinalize(
            this
        );
    }

    private void WriteMagicHeader(byte[] magicHeader)
        => _encodedBinaryWriter.Write(
            magicHeader
        );

    private void WriteSection(EncodingContext encodingContext)
    {
        byte[] toWrite = encodingContext.CanBeEncoded switch
        {
            true => encodingContext.ToEncodedByteArray(
                Constants.Marker
            ),
            false => encodingContext.ToByteArray()
        };

        _encodedBinaryWriter.Write(
            toWrite
        );
    }
}
