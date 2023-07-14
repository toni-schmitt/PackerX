using System.Security;
using Dawn;

namespace PackerX.Backend.Extensions;

public static class DawnGuardExtensions
{
    public static ref readonly Guard.ArgumentInfo<FileSystemInfo> FileExists(
        in this Guard.ArgumentInfo<FileSystemInfo> argument,
        string? message = null
    )
    {
        if (argument.Value.Exists is false)
        {
            throw Guard.Fail(
                new FileNotFoundException(
                    message
                )
            );
        }

        return ref argument;
    }

    public static ref readonly Guard.ArgumentInfo<FileSystemInfo> ValidPath(
        in this Guard.ArgumentInfo<FileSystemInfo> argument,
        string? message = null
    )
    {
        try
        {
            Path.GetFullPath(
                argument.Value.FullName
            );
        }
        catch (ArgumentException exception)
        {
            throw Guard.Fail(
                new InvalidPathException(
                    message,
                    exception
                )
            );
        }
        catch (SecurityException exception)
        {
            throw Guard.Fail(
                new InvalidPathException(
                    message,
                    exception
                )
            );
        }
        catch (NotSupportedException exception)
        {
            throw Guard.Fail(
                new InvalidPathException(
                    message,
                    exception
                )
            );
        }
        catch (PathTooLongException exception)
        {
            throw Guard.Fail(
                new InvalidCastException(
                    message,
                    exception
                )
            );
        }

        return ref argument;
    }

    public class InvalidPathException : InvalidOperationException
    {
        public InvalidPathException(
            string? message = null,
            Exception? innerException = null
        ) : base(
            message,
            innerException
        ) {}
    }
}
