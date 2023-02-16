using System;
using System.IO;

namespace WallpaperEngineAudioScratchboardEnhancedInstaller.Modules
{
    internal class WallpaperEngine
    {
        public const string WallpaperEngineApp = "launcher.exe";
        public static Uri SteamStore = new Uri("https://store.steampowered.com/app/431960/Wallpaper_Engine/");
        public static string SteamStoreApp = "steam://openurl/" + SteamStore.OriginalString;
        public static Uri Website = new Uri("https://www.wallpaperengine.io/en");

        public static DirectoryInfo AppDirectory()
        {
            DirectoryInfo appDirectory = null;

            try
            {
                string registryInstallPath = Steam.AppDirectory().FullName;
                string installPath = string.Empty;

                if (!string.IsNullOrEmpty(registryInstallPath))
                {
                    installPath = Path.Combine(registryInstallPath, "steamapps\\common\\wallpaper_engine");
                }

                appDirectory = new DirectoryInfo(installPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return appDirectory;
        }

        public static FileInfo AppInfo()
        {
            FileInfo appInfo = null;

            try
            {
                appInfo = new FileInfo(Path.Combine(AppDirectory().FullName, WallpaperEngineApp));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return appInfo;
        }

        public static bool Exists()
        {
            return AppInfo().Exists;
        }     

        public static DirectoryInfo ProjectsDirectory()
        {
            DirectoryInfo directoryInfo = null;

            try
            {
                if (AppDirectory().Exists)
                {
                    string projectsPath = Path.Combine(AppDirectory().FullName, "projects\\myprojects");
                    directoryInfo = new DirectoryInfo(projectsPath);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return directoryInfo;
        }
    }
}
