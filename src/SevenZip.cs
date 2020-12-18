/* PROJECT: Kompressor (https://github.com/aprettycoolprogram/Kompressor)
 *    FILE: Kompressor.SevenZip.cs
 * UPDATED: 12-17-2020-4:59 PM
 * LICENSE: Apache v2 (https://apache.org/licenses/LICENSE-2.0)
 *          Copyright 2020 A Pretty Cool Program All rights reserved
 */

using System;
using System.IO;
using Du;

namespace Kompressor
{
    public class SevenZip
    {
        public static string GetExecutablePath()
        {
            return Get64BitExecutablePath() == null
                ? Get32BitExecutablePath()
                : Get64BitExecutablePath();
        }

        public static void Create(string folderPath, string compressLevel)
        {
            var subDirs= DuDirectory.GetSubDirectoryNames(folderPath);
            var compressionLevel = compressLevel switch
            {
                "fastestCompression"        => "-mx1",
                "fastCompression"           => "-mx3",
                "normalCompression"         => "-mx5",
                "maximumOptimalCompression" => "-mx7",
                "ultraCompression"          => "-mx9",
                _                           => "-mx0",
            };

            foreach(var subDir in subDirs)
            {
                var s = $"\"{folderPath}{subDir}\\*\"";
                var d = $"\"{folderPath}{subDir}.7z\"";

                DuCompression.SevenZip.CreateFromDirectory(s, d, compressionLevel);
            }
        }

        private static string Get32BitExecutablePath()
        {
            var sevenZip32BitPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)}\\7-Zip\\7z.exe";

            return File.Exists(sevenZip32BitPath)
                ? sevenZip32BitPath
                : null;
        }

        private static string Get64BitExecutablePath()
        {
            var sevenZip64BitPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\7-Zip\\7z.exe";

            return File.Exists(sevenZip64BitPath)
                ? sevenZip64BitPath
                : null;
        }
    }
}
