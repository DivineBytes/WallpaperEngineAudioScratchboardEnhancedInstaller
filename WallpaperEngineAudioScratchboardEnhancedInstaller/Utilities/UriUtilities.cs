using System;
using System.Diagnostics;

namespace WallpaperEngineAudioScratchboardEnhancedInstaller.Utilities
{
    public static class UriUtilities
    {
        public static bool OpenUri(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri), "Cannot be null.");
            }

            try
            {
                Process.Start(uri.OriginalString); 
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }
    }
}
