using Autodesk.Windows;
using System.IO;

namespace Jacobian.Civil3D.Utilities
{
    /// <summary>
    /// Factory helpers for building ribbon buttons programmatically.
    /// Keeps ribbon setup code out of Application.cs.
    /// </summary>
    public static class RibbonUtils
    {
        /// <summary>
        /// Creates a large vertical ribbon button, adds it to the given panel,
        /// and wires it to the supplied ICommand.
        /// </summary>
        public static void CreateButton(
            RibbonPanelSource panelSource,
            System.Windows.Input.ICommand command,
            string buttonText,
            string toolTip,
            string dir)
        {
            Uri uri = new Uri(Path.Combine(dir, "JacobianWhite.ico"));

            RibbonButton ribbonButton = new RibbonButton
            {
                Text = buttonText,
                ToolTip = toolTip,
                ShowText = true,
                ShowImage = true,
                Orientation = System.Windows.Controls.Orientation.Vertical,
                LargeImage = new System.Windows.Media.Imaging.BitmapImage(uri),
                Size = RibbonItemSize.Large,
                CommandHandler = command,
            };

            panelSource.Items.Add(ribbonButton);
        }
    }
}
