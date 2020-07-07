using System.Collections.Generic;
using System.IO;

namespace Definitiva.Shared.Infra.Support.Helpers
{
    public static class DirectoryHelper
    {
        public static string RemoveBackSlash(this string directory)
        {
            return (!directory.IsNullOrWhiteSpace() && directory.Trim().EndsWith(@"\")) ? directory.Remove(directory.Trim().Length -1) : directory;
        }

        public static void CreateDirectory(this string directory)
        {
            Directory.CreateDirectory(directory);
        }

        public static bool DirectoryExists(this string directory)
        {
            return Directory.Exists(directory);
        }

        public static string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        public static string GetDirectory(this string fullFileName)
        {
            return Path.GetDirectoryName(fullFileName);
        }

        public static IEnumerable<string> GetDirectories(this string path)
        {
            return Directory.GetDirectories(path);
        }
    }
}
