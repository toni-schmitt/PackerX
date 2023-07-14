namespace PackerX.Backend.Extensions;

public static class FileSystemInfoExtensions
{
    public static FileSystemInfo AddEncodingExtension(
        this FileSystemInfo fileSystemInfo,
        string encodingExtension = ".ttpack"
    )
    {
        if (fileSystemInfo.Extension == encodingExtension)
        {
            return fileSystemInfo;
        }

        string fullFilePathWithoutExtension = string.IsNullOrEmpty(
                fileSystemInfo.Extension
            ) switch
            {
                false => fileSystemInfo.FullName.Remove(
                    fileSystemInfo.FullName.LastIndexOf(
                        '.'
                    )
                ),
                _ => fileSystemInfo.FullName
            };

        return new FileInfo(
            $"{fullFilePathWithoutExtension}{encodingExtension}"
        );
    }
}
