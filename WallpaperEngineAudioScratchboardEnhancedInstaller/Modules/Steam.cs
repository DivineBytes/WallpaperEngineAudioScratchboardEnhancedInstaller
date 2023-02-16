using Microsoft.Win32;
using System;
using System.IO;

namespace WallpaperEngineAudioScratchboardEnhancedInstaller.Modules
{
    internal class Steam
    {
        public const string SteamApp = "steam.exe";
        public static Uri DirectDownload = new Uri("https://cdn.akamai.steamstatic.com/client/installer/SteamSetup.exe");
        public static Uri Website = new Uri("https://store.steampowered.com/about/download");

        public static bool Exists()
        {
            return AppInfo().Exists;
        }

        public static FileInfo AppInfo()
        {
            FileInfo fileInfo = null;

            try
            {
                fileInfo = new FileInfo(Path.Combine(AppDirectory().FullName, SteamApp));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return fileInfo;
        }

        public static string GetRegistryInstallPath()
        {
            string installPath = string.Empty;

            try
            {
                RegistryKey steamRegistry = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Valve\\Steam", false);

                if (steamRegistry != null)
                {
                    installPath = Convert.ToString(steamRegistry.GetValue("InstallPath", string.Empty));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return installPath;
        }

        public static DirectoryInfo AppDirectory()
        {
            DirectoryInfo directoryInfo = null;

            try
            {
                string registryInstallPath = GetRegistryInstallPath();
                if (!string.IsNullOrEmpty(registryInstallPath))
                {
                    directoryInfo = new DirectoryInfo(registryInstallPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return directoryInfo;
        }

        public static bool RegistryPathExists()
        {
            try
            {
                string registryInstallPath = GetRegistryInstallPath();
                if (!string.IsNullOrEmpty(registryInstallPath) && Directory.Exists(registryInstallPath))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }
    }
}
