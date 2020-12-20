/* PROJECT: Kompressor (https://github.com/aprettycoolprogram/Kompressor)
 *    FILE: Kompressor.MainWindow.xaml.cs
 * UPDATED: 12-20-2020-3:07 PM
 * LICENSE: Apache v2 (https://apache.org/licenses/LICENSE-2.0)
 *          Copyright 2020 A Pretty Cool Program All rights reserved
 */

using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Du;
using Ookii.Dialogs.Wpf;

namespace Kompressor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            InitControls();
            ModifyKompressButtonStates();
        }

        /// <summary>
        /// Initialize the MainWindow controls.
        /// </summary>
        private void InitControls()
        {
            /*  Kompressor logo.
             */
            imgKompressorLogo.Source = DuBitmap.FromUri(Assembly.GetExecutingAssembly(), @"/Resources/Asset/Image/Logo/kompressor-logo-400x100.png");

            /*  Kompressor starts with "normal" compression as default.
             */
            rdoNormalCompression.IsChecked = true;

            /*  Eventually btnChooseFolderPath will have a background image, which will be set here.
             */
            //TODO: Move btnChooseFolderPath background image initilization out of MainWindow.xaml.

            /*  The Kompress button starts out disabled.
             */
            //TODO: Move btnKompress background image initilization out of MainWindow.xaml.
            btnKompress.IsEnabled = false;
        }

        /// <summary>
        /// Modify the Kompress button states.
        /// </summary>
        /// <remarks>
        /// * Enabled/disabled, depending on if the tbxFolderPath is a valid path.
        /// </remarks>
        public void ModifyKompressButtonStates()
        {
            btnKompress.IsEnabled = Directory.Exists(tbxFolderPath.Text);
        }

        /// <summary>
        /// User pressed the folder button.
        /// </summary>
        /// <remarks>
        /// * Uses OokiiDialogsWPF (https://github.com/augustoproiete/ookii-dialogs-wpf)
        /// </remarks>
        private void ChooseFolderPath()
        {
            var chooseFolderDialog = new VistaFolderBrowserDialog();

            if(chooseFolderDialog.ShowDialog() == true)
            {
                tbxFolderPath.Text = $"{chooseFolderDialog.SelectedPath}\\";
            }
        }

        /// <summary>
        /// User pressed the Kompress button.
        /// </summary>
        private void KompressButtonClicked()
        {
            var compressionLevel = GetCompressionLevel();
            SevenZip.Create(tbxFolderPath.Text, compressionLevel, lblProgress, (bool)cbxDeleteOriginal.IsChecked);
        }

        /// <summary>
        /// Determine the compression level.
        /// </summary>
        /// <returns></returns>
        private string GetCompressionLevel()
        {
            string compressionLevel;

            if((bool)rdoStoreCompression.IsChecked)
            {
                compressionLevel = "noCompression";
            }
            else if((bool)rdoFastestCompression.IsChecked)
            {
                compressionLevel = "fastestCompression";
            }
            else if((bool)rdoFastCompression.IsChecked)
            {
                compressionLevel = "fastCompression";
            }
            else if((bool)rdoNormalCompression.IsChecked)
            {
                compressionLevel = "normalCompression";
            }
            else if((bool)rdoMaximumCompression.IsChecked)
            {
                compressionLevel = "maximumOptimalCompression";
            }
            else if((bool)rdoUltraCompression.IsChecked)
            {
                compressionLevel = "ultraCompression";
            }
            else
            {
                compressionLevel = "noCompression";
            }

            return compressionLevel;
        }

        // EVENT HANDLERS
        private void btnChooseFolderPath_Click(object sender, RoutedEventArgs e) => ChooseFolderPath();
        private void tbxFolderPath_TextChanged(object sender, TextChangedEventArgs e) => ModifyKompressButtonStates();
        private void btnKompress_Click(object sender, RoutedEventArgs e) => KompressButtonClicked();
    }
}