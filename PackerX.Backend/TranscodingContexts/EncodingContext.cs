namespace PackerX.Backend.TranscodingContexts;

public class EncodingContext : TranscodingContext
{
    public EncodingContext(BinaryReader binaryReader) : base(
        binaryReader
    ) {}

    public bool CanBeEncoded => SectionLength >= 3;

    public override TranscodingContextResult ReadSection()
    {
        FirstByte = SectionContextReader.ReadByte();

        if (SectionContextReader.BaseStream.Position == OriginalFileLength)
        {
            return TranscodingContextResult.EndOfFile;
        }

        while (SectionContextReader.BaseStream.Position
               != OriginalFileLength
               && SectionLength < byte.MaxValue)
        {
            byte readByte = SectionContextReader.ReadByte();

            if (readByte != FirstByte)
            {
                SectionContextReader.BaseStream.Position -= 1;
                break;
            }

            SectionLength += 1;
        }

        return TranscodingContextResult.EndOfSection;
    }

    public byte[] ToEncodedByteArray(byte marker)
    {
        byte[] markerAndFirstByteAsByteArray =
        {
            marker,
            FirstByte
        };

        byte[] sectionLengthAsByteArray = SectionLengthToByteArray();

        return markerAndFirstByteAsByteArray.Concat(
                sectionLengthAsByteArray
            )
            .ToArray();
    }
}
