#region Namespace

using System.Collections;
using System.Windows.Forms.Design;

#endregion

namespace WallpaperEngineAudioScratchboardEnhancedInstaller.Controls
{
    internal class SeparatorDesigner : ControlDesigner
    {
        #region Methods

        protected override void PreFilterProperties(IDictionary properties)
        {
            properties.Remove("ImeMode");
            properties.Remove("Padding");
            properties.Remove("FlatAppearance");
            properties.Remove("FlatStyle");
            properties.Remove("AutoEllipsis");
            properties.Remove("UseCompatibleTextRendering");
            properties.Remove("Image");
            properties.Remove("ImageAlign");
            properties.Remove("ImageIndex");
            properties.Remove("ImageKey");
            properties.Remove("ImageList");
            properties.Remove("TextImageRelation");
            properties.Remove("BackgroundImageLayout");
            properties.Remove("UseVisualStyleBackColor");
            properties.Remove("RightToLeft");

            base.PreFilterProperties(properties);
        }

        #endregion
    }
}