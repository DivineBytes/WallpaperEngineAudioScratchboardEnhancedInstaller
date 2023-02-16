using System;
using System.IO;

namespace WallpaperEngineAudioScratchboardEnhancedInstaller.Modules
{
    internal class AudioScratchboardEnhanced
    {
        public const string AddonName = "AudioScratchboardEnhanced";

        public static DirectoryInfo AddonDirectory()
        {
            DirectoryInfo directoryInfo = null;

            try
            {
                if (WallpaperEngine.ProjectsDirectory().Exists)
                {
                    string path = Path.Combine(WallpaperEngine.ProjectsDirectory().FullName, AddonName);
                    directoryInfo = new DirectoryInfo(path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return directoryInfo;
        }

        public static bool Exists()
        {
            return AddonDirectory().Exists;
        }
    }
}
