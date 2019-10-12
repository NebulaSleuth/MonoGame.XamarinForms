using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Microsoft.Xna.Framework.Utilities
{
    public class PackFileEntry
    {
        public string Name { get; set; }
        public long Offset { get; set; }
        public long Length { get; set; }
    }

    public static class FilePacker
    {
        public static void PackFolder(string folder, string outpath, bool output)
        {
            var files = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories);
            if (File.Exists(outpath)) File.Delete(outpath);

            var outfile = File.Create(outpath, 10240, FileOptions.WriteThrough);
            outfile.WriteByte((byte)'B');
            outfile.WriteByte((byte)'P');
            outfile.WriteByte((byte)'A');
            outfile.WriteByte((byte)'K');

            WriteInt(outfile, files.Count());

            Dictionary<string, long> seekOffsets = new Dictionary<string, long>();
            Dictionary<string, long> seekOffsets2 = new Dictionary<string, long>();
            if (output) Console.WriteLine("Creating Directory");
            foreach (var file in files)
            {

                WriteString(outfile, GetSubPath(folder, file));

                seekOffsets.Add(file, outfile.Position);
                WriteLong(outfile, 0);  // Don't Know the offset yet

                long fileLength = GetFileLength(folder, file);
                WriteLong(outfile, fileLength);
            }

            if (output) Console.WriteLine("Packing Files...");
            foreach (var file in files)
            {
                if (output) Console.WriteLine(GetSubPath(folder, file));
                seekOffsets2.Add(file, outfile.Position);

                var inFile = File.OpenRead(file);
                inFile.CopyTo(outfile);
                inFile.Close();
            }

            // Now set the file offsets
            if (output) Console.WriteLine("Adjusting Offsets...");
            foreach (var file in files)
            {
                long offset = seekOffsets[file];
                outfile.Seek(offset, SeekOrigin.Begin);
                WriteLong(outfile, seekOffsets2[file]);
            }
            if (output) Console.WriteLine("Done.");
        }
        static string GetSubPath(string folder, string file)
        {
            if (file.StartsWith(folder))
            {
                file = file.Substring(folder.Length);
                while (file.StartsWith("/") || file.StartsWith("\\"))
                {
                    file = file.Substring(1);
                }
            }
            return file;
        }

        static long GetFileLength(string folder, string file)
        {
            string fullpath = Path.Combine(folder, file);
            FileInfo info = new FileInfo(fullpath);
            return (int)info.Length;
        }

        static void WriteInt(FileStream file, int value)
        {
            file.Write(BitConverter.GetBytes(value), 0, sizeof(int));
        }

        static void WriteLong(FileStream file, long value)
        {
            file.Write(BitConverter.GetBytes(value), 0, sizeof(long));
        }

        static void WriteString(FileStream file, string value)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(value);
            WriteInt(file, bytes.Count());
            file.Write(bytes, 0, bytes.Count());
        }

        static byte[] buffer = new byte[1024];

        static bool CheckHeader(Stream infile)
        {
            lock (buffer)
            {
                if (infile.CanSeek)
                    infile.Seek(0, SeekOrigin.Begin);
                infile.Read(buffer, 0, 4);
                if (buffer[0] == 'B' && buffer[1] == 'P' && buffer[2] == 'A' && buffer[3] == 'K') return true;
                return false;
            }
        }

        static int ReadInt(Stream infile)
        {
            lock (buffer)
            {
                if (infile.Read(buffer, 0, sizeof(int)) == sizeof(int))
                {
                    return BitConverter.ToInt32(buffer, 0);
                }
            }
            throw new EndOfStreamException();
        }
        static long ReadLong(Stream infile)
        {
            lock (buffer)
            {
                if (infile.Read(buffer, 0, sizeof(long)) == sizeof(long))
                {
                    return BitConverter.ToInt64(buffer, 0);
                }
            }
            throw new EndOfStreamException();
        }

        static string ReadString(Stream infile)
        {
            int len = 0;
            lock (buffer)
            {
                len = ReadInt(infile);
            }

            if (len + 1 > buffer.Length) buffer = new byte[len + 1];

            lock (buffer)
            {
                if (infile.Read(buffer, 0, len) == len)
                {
                    buffer[len] = 0;
                    return Encoding.ASCII.GetString(buffer, 0, len);
                }
            }
            throw new EndOfStreamException();
        }
        static List<PackFileEntry> ReadDirectory(Stream infile, out int dirLen)
        {
            List<PackFileEntry> dir = new List<PackFileEntry>();
            if (infile.CanSeek) infile.Seek(4, SeekOrigin.Begin);
            int cnt = ReadInt(infile);
            dirLen = sizeof(int);
            for (int i = 0; i < cnt; i++)
            {
                PackFileEntry e = new PackFileEntry();
                e.Name = ReadString(infile);
                dirLen += e.Name.Length + sizeof(int);
                e.Offset = ReadLong(infile);
                dirLen += sizeof(long);
                e.Length = ReadLong(infile);
                dirLen += sizeof(long);

                dir.Add(e);
            }
            return dir;
        }

        public static long GetAssetPosition(string pakfile, string filename, out long len)
        {
            Stream inFile = File.OpenRead(pakfile);
            if (CheckHeader(inFile))
            {
                var dir = ReadDirectory(inFile, out int dirLen);

                filename = filename.Replace("/", "\\"); // Because the packer is windows
                PackFileEntry entry = dir.FirstOrDefault(e => e.Name.ToLower() == filename.ToLower());
                if (entry != null)
                {
                    len = entry.Length;
                    return entry.Offset;
                }
            }
            len = 0;
            return -1;
        }

        public static long GetAssetLength(string pakfile, string filename)
        {
            Stream inFile = File.OpenRead(pakfile);
            if (CheckHeader(inFile))
            {
                var dir = ReadDirectory(inFile, out int dirLen);

                filename = filename.Replace("/", "\\"); // Because the packer is windows
                PackFileEntry entry = dir.FirstOrDefault(e => e.Name.ToLower() == filename.ToLower());
                if (entry != null)
                {
                    return entry.Length;
                }
            }
            return -1;
        }
        public static Stream GetFileStream(string pakfile, string filename)
        {
            Stream inFile = File.OpenRead(pakfile);
            if (CheckHeader(inFile))
            {
                var dir = ReadDirectory(inFile, out int dirLen);

                filename = filename.Replace("/", "\\"); // Because the packer is windows
                PackFileEntry entry = dir.FirstOrDefault(e => e.Name.ToLower() == filename.ToLower());
                if (entry != null)
                {
                    return new SubStream(inFile, entry.Offset, entry.Length, 4 + dirLen);
                }
            }
            return null;
        }
        public static Stream GetFileStream(Stream inFile, string filename)
        {
            if (CheckHeader(inFile))
            {
                var dir = ReadDirectory(inFile, out int dirLen);

                filename = filename.Replace("/", "\\"); // Because the packer is windows
                PackFileEntry entry = dir.FirstOrDefault(e => e.Name.ToLower() == filename.ToLower());
                if (entry != null)
                {
                    return new SubStream(inFile, entry.Offset, entry.Length, 4 + dirLen);
                }
            }
            return null;
        }

        public static string CreateTempFile(string safeName)
        {
            if (safeName.ToLower().Contains(".pak"))
            {
                // Pull from the .zip files
                int idx = safeName.ToLower().IndexOf(".pak") + 4;
                string pakName = Path.Combine(TitleContainer.Location, safeName.Substring(0, idx));
                string filename = safeName.Substring(idx);
                while (filename.StartsWith("/") || filename.StartsWith("\\"))
                {
                    filename = filename.Substring(1);
                }

                try
                {
                    if (File.Exists(pakName))
                    {
                        string tempFile = Path.Combine(TitleContainer.TempLocation, "Temp", filename);
                        if (!File.Exists(tempFile))
                        {
                            try
                            {
                                if (!Directory.Exists(Path.Combine(TitleContainer.TempLocation, "Temp")))
                                {
                                    Directory.CreateDirectory(Path.Combine(TitleContainer.TempLocation, "Temp"));
                                }
                                var stream = FilePacker.GetFileStream(pakName, filename);
                                FileStream outFile = File.Create(tempFile);
                                stream.CopyTo(outFile);
                                stream.Close();

                                outFile.Close();
                                return tempFile;
                            }
                            catch { }
                        }
                        return tempFile;
                    }
                }
                catch
                {
                }
                return null;
            }
            if (File.Exists(safeName)) return safeName;

            var absolutePath = Path.Combine(TitleContainer.Location, safeName);
            return absolutePath;
        }
    }
}
