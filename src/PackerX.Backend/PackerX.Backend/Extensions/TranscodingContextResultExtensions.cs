// ReSharper disable once CheckNamespace - this namespace is intended
namespace PackerX.Backend.TranscodingContexts;

public static class TranscodingContextResultExtensions
{
    public static bool IsEndOfFile(
        this TranscodingContextResult transcodingContextResult
    )
        => transcodingContextResult == TranscodingContextResult.EndOfFile;
}
