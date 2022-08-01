using Engine.ViewModels;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ModManager _modManager;

        public MainWindow()
        {
            InitializeComponent();
            Properties.Settings.Default.AddonsFolderPath = String.Empty;
            Properties.Settings.Default.Save();

            _modManager = InitializeModManager();

            DataContext = _modManager;
        }

        private ModManager InitializeModManager()
        {
            var folderPath = GetAddonsFolderPathFromSettings();

            var modManager = new ModManager(folderPath);

            if (string.IsNullOrWhiteSpace(folderPath))
            {
                ChooseAddonsFolder(modManager);
            }

            return modManager;
        }

        private void ChooseAddonsFolder(ModManager modManager)
        {
            var folderPath = modManager.GetAddonsFolderPath();
            if (Directory.Exists(folderPath))
            {
                Properties.Settings.Default.AddonsFolderPath = folderPath;

                if (MessageBox.Show($"Default add-ons folder is \"{folderPath}\". Would you like to change it?",
                    "Default add-on folder",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                    dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    dialog.IsFolderPicker = true;
                    if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        Properties.Settings.Default.AddonsFolderPath = dialog.FileName;
                    }
                }
                Properties.Settings.Default.Save();
            }
        }

        private string GetAddonsFolderPathFromSettings() => Properties.Settings.Default.AddonsFolderPath;
    }
}
