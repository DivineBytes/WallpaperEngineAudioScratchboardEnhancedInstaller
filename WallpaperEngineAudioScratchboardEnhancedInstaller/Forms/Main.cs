using System;
using System.IO;
using System.Windows.Forms;
using WallpaperAudioScratchboardEnhancedInstaller.Modules;
using WallpaperAudioScratchboardEnhancedInstaller.Properties;
using WallpaperAudioScratchboardEnhancedInstaller.Utilities;

namespace WallpaperAudioScratchboardEnhancedInstaller
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        public void ValidateInstallChecks()
        {
            try
            {
                // Check if steam is installed in registry
                if (Steam.RegistryPathExists())
                {
                    PB_SteamRegistry.Image = Resources.check;
                }
                else
                {
                    PB_SteamRegistry.Image = Resources.x;
                }

                // Check if steam installed in folder
                if (Steam.Exists())
                {
                    PB_SteamFolder.Image = Resources.check;
                    BTN_OpenFolderSteam.Enabled = true;
                    BTN_OpenSteam.Enabled = true;
                }
                else
                {
                    PB_SteamFolder.Image = Resources.x;
                    BTN_OpenFolderSteam.Enabled = false;
                    BTN_OpenSteam.Enabled = false;
                }

                // Check if wallpaper engine is installed
                if (WallpaperEngine.Exists())
                {
                    PB_WallpaperEngineFolder.Image = Resources.check;
                    BTN_OpenFolderWallpaperEngine.Enabled = true;
                    BTN_OpenWallpaperEngine.Enabled = true;
                    BTN_OpenWallpaperEngineSteam.Enabled = true;
                }
                else
                {
                    PB_WallpaperEngineFolder.Image = Resources.x;
                    BTN_OpenFolderWallpaperEngine.Enabled = false;
                    BTN_OpenWallpaperEngine.Enabled = false;
                    BTN_OpenWallpaperEngineSteam.Enabled = false;
                }

                // Check if audio scratchboard is installed
                if (AudioScratchboardEnhanced.Exists())
                {
                    PB_AsbeExists.Image = Resources.check;
                    BTN_OpenFolderAudioscratchboardEnhanced.Enabled = true;
                }
                else
                {
                    PB_AsbeExists.Image = Resources.x;
                    BTN_OpenFolderAudioscratchboardEnhanced.Enabled = false;
                }

                // Check if environment is configured to allow addon install
                if (Steam.RegistryPathExists() && Steam.Exists() && WallpaperEngine.Exists())
                {
                    BTN_Install.Enabled = true;
                }
                else
                {
                    BTN_Install.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void BTN_SteamDownload_Click(object sender, EventArgs e)
        {
            UriUtilities.OpenUri(Steam.DirectDownload);
        }

        private void BTN_SteamWebsite_Click(object sender, EventArgs e)
        {
            UriUtilities.OpenUri(Steam.Website);
        }

        private void BTN_WallpaperEngineDownload_Click(object sender, EventArgs e)
        {
            UriUtilities.OpenUri(WallpaperEngine.SteamStore);
        }

        private void BTN_WallpaperEngineSteam_Click(object sender, EventArgs e)
        {
            UriUtilities.OpenUri(WallpaperEngine.Website);
        }

        private void BTN_OpenWallpaperEngineSteam_Click(object sender, EventArgs e)
        {
            RunDialog.SetDefault(WallpaperEngine.SteamStoreApp);
            RunDialog.Open();
        }

        private void BTN_OpenFolderSteam_Click(object sender, EventArgs e)
        {
            if(Steam.Exists())
            {
                FileExplorerUtilities.Show(Steam.AppInfo(), true);
            }
        }

        private void BTN_OpenFolderWallpaperEngine_Click(object sender, EventArgs e)
        {
            if (WallpaperEngine.Exists())
            {
                FileExplorerUtilities.Show(WallpaperEngine.AppInfo(), true);
            }
        }

        private void BTN_OpenSteam_Click(object sender, EventArgs e)
        {
            if (Steam.Exists())
            {
                ProcessUtilities.StartProcess(Steam.AppInfo().FullName, string.Empty, false, false, false);
            }
        }

        private void BTN_OpenWallpaperEngine_Click(object sender, EventArgs e)
        {
            ProcessUtilities.StartProcess(WallpaperEngine.AppInfo().FullName, string.Empty, false, false, false);
        }

        private void BTN_OpenFolderAudioscratchboardEnhanced_Click(object sender, EventArgs e)
        {
            if (AudioScratchboardEnhanced.AddonDirectory().Exists)
            {
                FileExplorerUtilities.Show(AudioScratchboardEnhanced.AddonDirectory());
            }
        }

        private void BTN_Install_Click(object sender, EventArgs e)
        {
            // Disable button while installing
            BTN_Install.Enabled = false;

            try
            {
                DirectoryInfo appDirectory = new DirectoryInfo(Path.GetDirectoryName(Application.ExecutablePath));

                FileInfo[] files = appDirectory.GetFiles();
                foreach (FileInfo fileInfo in files)
                {
                    string fileName = fileInfo.Name;
                    string destinationFileName = Path.Combine(AudioScratchboardEnhanced.AddonDirectory().FullName, fileName);
                    
                    File.Copy(fileInfo.FullName, destinationFileName, true);
                }

                string installDoneMessage =
                    "The installation has completed!" +
                    "\n\n" +
                    "Destination: " + AudioScratchboardEnhanced.AddonDirectory().FullName +
                    "\n\n" +
                    "Installed: " + files.Length + " file/s" +
                    "\n\n" +
                    "Would you like to open the destination directory?";

                DialogResult dialogResult = MessageBox.Show(this, installDoneMessage, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            
                if (dialogResult == DialogResult.Yes)
                {
                    BTN_OpenFolderAudioscratchboardEnhanced.PerformClick();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                string installErrorMessage =
                    "The installation has failed!" +
                    "\n\n" +
                    ex.Message;

                MessageBox.Show(this, installErrorMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void T_Main_Tick(object sender, EventArgs e)
        {
            ValidateInstallChecks();
        }
    }
}
