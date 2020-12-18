/* PROJECT: Kompressor (https://github.com/aprettycoolprogram/Kompressor)
 *    FILE: Kompressor.Zip.cs
 * UPDATED: 12-17-2020-4:59 PM
 * LICENSE: Apache v2 (https://apache.org/licenses/LICENSE-2.0)
 *          Copyright 2020 A Pretty Cool Program All rights reserved
 */

using Du;

namespace Kompressor
{
    public class Zip
    {
        public static void Create(string folderPath, string compressLevel)
        {
            var subDirs= DuDirectory.GetSubDirectoryNames(folderPath);

            System.IO.Compression.CompressionLevel compressionLevel = System.IO.Compression.CompressionLevel.NoCompression;

            compressionLevel = compressLevel switch
            {
                "fastestCompression" => System.IO.Compression.CompressionLevel.Fastest,
                "maximumOptimalCompression" => System.IO.Compression.CompressionLevel.Optimal,
                _ => System.IO.Compression.CompressionLevel.NoCompression,
            };
            foreach(var subDir in subDirs)
            {
                var s = folderPath + subDir + @"\";
                var d = folderPath + subDir + ".zip";

                DuCompression.Zip.CreateFromDirectory(s, d, compressionLevel, false);
            }
        }
    }
}
