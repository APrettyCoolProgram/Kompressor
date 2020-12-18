/* PROJECT: Kompressor (https://github.com/aprettycoolprogram/Kompressor)
 *    FILE: Kompressor.MainWindow.xaml.cs
 * UPDATED: 12-18-2020-12:34 PM
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

            InitLogo();
            InitKompressButton();
        }

        /// <summary>
        /// Initialize the Kompressor logo.
        /// </summary>
        private void InitLogo()
        {
            imgKompressorLogo.Source = DuBitmap.FromUri(Assembly.GetExecutingAssembly(), @"/Resources/Asset/Image/Logo/kompressor-logo.png");
        }

        /// <summary>
        /// Initialize the compression level.
        /// </summary>
        private void InitCompressionLevel()
        {
            rdoNormalCompression.IsChecked = true;
        }

        /// <summary>
        /// Initialize the Kompress button.
        /// </summary>
        public void InitKompressButton()
        {
            btnKompress.IsEnabled = Directory.Exists(tbxFolderPath.Text);
        }

        /// <summary>
        /// User pressed the folder button.
        /// </summary>
        private void ChooseFolderPath()
        {
            var ookiiDialog = new VistaFolderBrowserDialog();

            if(ookiiDialog.ShowDialog() == true)
            {
                tbxFolderPath.Text = $"{ookiiDialog.SelectedPath}\\";
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
        private void btnChooseFolder_Click(object sender, RoutedEventArgs e) => ChooseFolderPath();
        private void btnKompress_Click(object sender, RoutedEventArgs e) => KompressButtonClicked();
        private void tbxFolderPath_TextChanged(object sender, TextChangedEventArgs e) => InitKompressButton();
    }
}