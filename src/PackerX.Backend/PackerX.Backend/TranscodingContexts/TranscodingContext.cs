namespace PackerX.Backend.TranscodingContexts;

public abstract class TranscodingContext
{
    protected readonly BinaryReader SectionContextReader;

    protected TranscodingContext(BinaryReader sectionContextReader)
        => SectionContextReader = sectionContextReader;

    public byte FirstByte { get; protected set; }
    public int SectionLength { get; protected set; } = 1;
    public required long OriginalFileLength { get; init; }

    public abstract TranscodingContextResult ReadSection();

    public byte[] ToByteArray() => Enumerable.Repeat(
            FirstByte,
            SectionLength
        )
        .ToArray();

    // ReSharper disable once ReturnTypeCanBeEnumerable.Global - this return type is intended
    public byte[] SectionLengthToByteArray()
    {

        byte[] sectionLengthAsByteArray = BitConverter.GetBytes(
            SectionLength
        );

        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(
                sectionLengthAsByteArray
            );
        }

        return sectionLengthAsByteArray;
    }
}
