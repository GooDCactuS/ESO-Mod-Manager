using Engine.Utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Engine.Models
{
    internal class InstalledModManager
    {
        public string FolderPath { get; set; }
        public List<Addon> Addons { get; set; }

        internal InstalledModManager(string addonFolderPath)
        {
            FolderPath = addonFolderPath;
            Addons = new List<Addon>();
            LoadAddons();
        }

        private void LoadAddons()
        {
            var parentDirectory = new DirectoryInfo(FolderPath);
            var addonsFolders = parentDirectory.GetDirectories();
            foreach (var folder in addonsFolders)
            {
                var title = folder.Name;
                var addonInfoFilePath = folder.GetFiles($"{title}.txt");
                if (addonInfoFilePath.Any())
                {
                    var addon = GetAddonInfoFromTxt(title, addonInfoFilePath[0].FullName);
                    Addons.Add(addon);
                }
            }
        }

        private Addon GetAddonInfoFromTxt(string title, string path)
        {
            var addonInfo = File.ReadAllText(path);
            return AddonInfoParser.ParseTxtIntoAddon(title, addonInfo);
        }
    }
}
