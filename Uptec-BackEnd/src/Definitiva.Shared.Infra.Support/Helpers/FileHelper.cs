using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Definitiva.Shared.Infra.Support.Helpers
{
    public static class FileHelper
    {
        public static bool FileExists(this string fullFileName)
        {
            return File.Exists(fullFileName);
        }

        public static IEnumerable<string> GetFiles(this string directory, string fileType = "*", string fileName = "*")
        {
            var dir = directory.AddEndChar("\\");
            var extension = fileType.Replace(".", "");

            if (dir.IsNullOrWhiteSpace() || !dir.AddEndChar("\\").DirectoryExists())
                return new List<string>();

            return Directory.GetFiles($@"{dir}", $"{fileName}.{extension}");
        }

        public static IEnumerable<string> GetFileList(string directory, string fileType = "*", string fileName = "*")
        {
            return directory.GetFiles(fileType, fileName);
        }

        public static string GetFileName(this string fullFileName)
        {
            return Path.GetFileName(fullFileName);
        }

        public static void MoveFileTo(this string fullFileName, string toDirectory)
        {
            if (!toDirectory.DirectoryExists())
                toDirectory.CreateDirectory();

            File.Move(fullFileName, toDirectory.AddEndChar("\\") + Path.GetFileName(fullFileName));
        }

        public static List<string> ReadTextFile(this string fullFileName)
        {
            return File.ReadAllLines(fullFileName).ToList();
        }

        public static void SaveTextFile(this string fileContent, string fullFileName)
        {
            using (var writer = new StreamWriter(fullFileName, false))
            {
                writer.Write(fileContent);
            }
        }

        public static byte[] ToBytes(this string fullFileName, Encoding encoding = null)
        {
            var encode = encoding ?? Encoding.UTF8;

            byte[] file;
            using (var stream = new FileStream(fullFileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream, encode))
                {
                    file = reader.ReadBytes((int)stream.Length);
                }
            }

            return file;
        }

        public static string ToString(this byte[] fileContent, Encoding encoding = null)
        {
            var encode = encoding ?? Encoding.UTF8;

            return encode.GetString(fileContent);
        }

        public static IEnumerable<string> ToStringList(this byte[] byteOutput)
        {
            var stringOutput = Encoding.ASCII.GetString(byteOutput);

            return stringOutput.Split(Environment.NewLine.ToCharArray()).ToList().Where(l => !l.IsNullOrWhiteSpace());
        }
    }
}
