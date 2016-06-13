using System;
using System.Collections.Generic;
using System.Windows;
using CodaForte.IO.Files;

namespace CodaForte.FileExtensionFixer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnFixExtensions_OnClick(object sender, RoutedEventArgs e)
        {
            if (txtDirectoryPath.Text.Trim() == string.Empty)
                MessageBox.Show("Please enter a directory to begin with in order to continue.");

            var directoryInfo = new System.IO.DirectoryInfo(txtDirectoryPath.Text);
            if (!directoryInfo.Exists)
                MessageBox.Show("The selected directory does not exist.  Please select a new directory and try again.");

            txtOutput.Text = string.Empty;

            var filesUpdated = new List<string>();
            FileExtensionExtractor.FixFilesInDirectory(directoryInfo, cbIncludeSubDirectories.IsChecked ?? false, ref filesUpdated);

            txtOutput.Text += $"Fixed {filesUpdated.Count} files.";

            foreach (var file in filesUpdated)
                txtOutput.Text += $"{Environment.NewLine}{file}";
        }

        private void BtnBrowse_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog(this) ?? false)
                txtDirectoryPath.Text = dialog.SelectedPath;
        }
    }
}
