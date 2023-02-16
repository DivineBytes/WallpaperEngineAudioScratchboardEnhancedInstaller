using System;
using System.IO;

namespace WallpaperEngineAudioScratchboardEnhancedInstaller.Utilities
{
    /// <summary>
    ///     The <see cref="FileUtilities" /> class.
    /// </summary>
    public class FileUtilities
    {
        public static void CopyDirectory(string sourcePath, string destPath)
        {
            if (string.IsNullOrEmpty(sourcePath))
            {
                throw new ArgumentNullException(nameof(sourcePath), "Cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(destPath))
            {
                throw new ArgumentNullException(nameof(destPath), "Cannot be null or empty.");
            }

            if (!Directory.Exists(sourcePath))
            {
                throw new ArgumentNullException(nameof(sourcePath), "The directory does not exist.");
            }

            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }

            foreach (string file in Directory.GetFiles(sourcePath))
            {
                string dest = Path.Combine(destPath, Path.GetFileName(file));
                File.Copy(file, dest, true);
            }

            foreach (string folder in Directory.GetDirectories(sourcePath))
            {
                string dest = Path.Combine(destPath, Path.GetFileName(folder));
                CopyDirectory(folder, dest);
            }
        }
    }
}
