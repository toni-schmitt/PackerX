namespace PackerX.Backend.TranscodingContexts;

public class DecodingContext : TranscodingContext
{

    public DecodingContext(BinaryReader binaryReader) : base(
        binaryReader
    ) {}

    public bool IsEncodedSection { get; private set; }

    public override TranscodingContextResult ReadSection()
    {
        FirstByte = SectionContextReader.ReadByte();

        // ReSharper disable once InvertIf - this is intended
        if (FirstByte is Constants.Marker)
        {
            IsEncodedSection = true;
            FirstByte = SectionContextReader.ReadByte();
            SectionLength = SectionContextReader.ReadInt32();
        }

        return SectionContextReader.BaseStream.Position
               == OriginalFileLength
            ? TranscodingContextResult.EndOfFile
            : TranscodingContextResult.EndOfSection;
    }
}
