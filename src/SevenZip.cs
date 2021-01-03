/* PROJECT: Kompressor (https://github.com/aprettycoolprogram/Kompressor)
 *    FILE: Kompressor.SevenZip.cs
 * UPDATED: 12-20-2020-3:07 PM
 * LICENSE: Apache v2 (https://apache.org/licenses/LICENSE-2.0)
 *          Copyright 2020 A Pretty Cool Program All rights reserved
 */

using System;
using System.Windows.Controls;
using System.Windows.Threading;
using Du;

namespace Kompressor
{
    public class SevenZip
    {

        private static readonly Action EmptyDelegate = delegate { };

        /// <summary>
        /// Refresh the progress label.
        /// </summary>
        /// <param name="label">The progress label.</param>
        public static void Refresh(Label label)
        {
            label.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }

        /// <summary>
        /// Create an archive.
        /// </summary>
        /// <param name="folderPath">Path to the folder to archive.</param>
        /// <param name="compressLevel">The compression level.</param>
        /// <param name="lblProgress">The progress indicator</param>
        /// <param name="deleteOriginalFolder">Determines if the original folder should be deleted after archiving.</param>
        public static void Create(string folderPath, string compressLevel, Label lblProgress, bool deleteOriginalFolder)
        {
            var subDirectories= DuDirectory.GetSubDirectoryNames(folderPath);

            var compressionLevel = compressLevel switch
            {
                "fastestCompression"        => "-mx1",
                "fastCompression"           => "-mx3",
                "normalCompression"         => "-mx5",
                "maximumOptimalCompression" => "-mx7",
                "ultraCompression"          => "-mx9",
                _                           => "-mx0",
            };

            var sevenZipExecutablePath = DuOperatingSystem.MSWindows.Is64Bit()
                ? @"./Resources/Bin/7Zip/64bit/7za.exe"
                : @"./Resources/Bin/7Zip/32bit/7za.exe";

            var numberOfSubDirectories = subDirectories.Count;
            var currSubDirectoryCount = 1;

            foreach(var subDirectory in subDirectories)
            {
                var sourceDirectory = $"{folderPath}{subDirectory}\\";
                var archiveFilePath = $"{folderPath}{subDirectory}.7z";

                var command = $"a -t7z {compressionLevel} \"{archiveFilePath}\" \"{sourceDirectory}*\"";

                lblProgress.Content = $"PROGRESS: File {currSubDirectoryCount} of {numberOfSubDirectories}: {subDirectory}";
                Refresh(lblProgress);

                DuCompression.SevenZip.CreateFromDirectory(sevenZipExecutablePath, archiveFilePath, command);

                if(deleteOriginalFolder)
                {
                    DuDirectory.Delete(sourceDirectory);
                }

                currSubDirectoryCount++;
            }

            lblProgress.Content = $"PROGRESS: COMPLETE!";
        }
    }
}