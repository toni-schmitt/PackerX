namespace PackerX.Backend.TranscodingContexts;

public class DecodingContext : TranscodingContext
{

    public DecodingContext(BinaryReader binaryReader) : base(
        binaryReader
    ) {}

    public bool IsEncodedSection { get; private set; } = false;

    public override TranscodingContextResult ReadSection()
    {
        FirstByte = SectionContextReader.ReadByte();

        SectionLength = FirstByte switch
        {
            Constants.Marker => SectionContextReader.ReadInt32(),
            _ => 1
        };

        return SectionContextReader.BaseStream.Position == OriginalFileLength
            ? TranscodingContextResult.EndOfFile
            : TranscodingContextResult.EndOfSection;
    }
}
