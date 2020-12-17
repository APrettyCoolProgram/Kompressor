/* PROJECT: Kompressor (https://github.com/aprettycoolprogram/Kompressor)
 *    FILE: Kompressor.MainWindow.xaml.cs
 * UPDATED: 12-17-2020-4:59 PM
 * LICENSE: Apache v2 (https://apache.org/licenses/LICENSE-2.0)
 *          Copyright 2020 A Pretty Cool Program All rights reserved
 */

using System.IO;
using System.Windows;
using System.Windows.Controls;
using Ookii.Dialogs.Wpf;
using Du;

namespace Kompressor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SetKompressButton();
            SetupZip();
            SetupSevenZip();

        }

        private void SetupZip()
        {
            ZipCompressionMethodChosen();
        }

        private void SetupSevenZip()
        {
            rdoSevenZipCompression.Tag = SevenZip.GetExecutablePath(); //= null
            rdoSevenZipCompression.IsEnabled = rdoSevenZipCompression.Tag != null;
        }

        private void ChooseFolder()
        {
            var ookiiDialog = new VistaFolderBrowserDialog();

            if(ookiiDialog.ShowDialog() == true)
            {
                tbxFolderPath.Text = $"{ookiiDialog.SelectedPath}\\";
            }
        }

        public void SetKompressButton()
        {
            btnKompress.IsEnabled = Directory.Exists(tbxFolderPath.Text);
        }

        private string GetCompressionLevel()
        {
            string compressionLevel;

            if((bool)rdoNoCompression.IsChecked)
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
            else if((bool)rdoMaximumOptimalCompression.IsChecked)
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

        private void KompressButtonClicked()
        {
            if((bool)rdoZipCompression.IsChecked)
            {
                var compressionLevel = GetCompressionLevel();
                Zip.Create(tbxFolderPath.Text, compressionLevel);
            }
            else if((bool)rdoSevenZipCompression.IsChecked)
            {
                var compressionLevel = GetCompressionLevel();
                SevenZip.Create(tbxFolderPath.Text, compressionLevel);
            }

            MessageBoxResult compressionCompleteMsg = MessageBox.Show("All folders have been compressed", "Compression complete!");
        }

        private void ZipCompressionMethodChosen()
        {
            rdoZipCompression.IsChecked = true;

            rdoNoCompression.IsEnabled = true;
            rdoFastestCompression.IsEnabled = true;
            rdoFastCompression.IsEnabled = false;
            rdoNormalCompression.IsEnabled = false;
            rdoMaximumOptimalCompression.IsEnabled = true;
            rdoUltraCompression.IsEnabled = false;

            rdoMaximumOptimalCompression.IsChecked = true;
        }

        private void SevenZipCompressionMethodChosen()
        {
            rdoSevenZipCompression.IsChecked = true;

            rdoNoCompression.IsEnabled = true;
            rdoFastestCompression.IsEnabled = true;
            rdoFastCompression.IsEnabled = true;
            rdoNormalCompression.IsEnabled = true;
            rdoMaximumOptimalCompression.IsEnabled = true;
            rdoUltraCompression.IsEnabled = true;

            rdoNormalCompression.IsChecked = true;
        }

        // EVENT HANDLERS
        private void rdoZipCompression_Checked(object sender, RoutedEventArgs e) => ZipCompressionMethodChosen();
        private void rdoSevenZipCompression_Checked(object sender, RoutedEventArgs e) => SevenZipCompressionMethodChosen();
        private void btnChooseFolder_Click(object sender, RoutedEventArgs e) => ChooseFolder();
        private void btnKompress_Click(object sender, RoutedEventArgs e) => KompressButtonClicked();
        private void tbxFolderPath_TextChanged(object sender, TextChangedEventArgs e) => SetKompressButton();
    }
}