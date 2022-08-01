using Engine.Models;
using System;

namespace Engine.ViewModels
{
    public class ModManager
    {
        private AddonsFolder _addonsFolder;
        private InstalledModManager _installedModManager;

        public ModManager(string? addonsFolderPath)
        {
            _addonsFolder = new AddonsFolder(addonsFolderPath);
            _installedModManager = new InstalledModManager(_addonsFolder.Path);
        }

        public string GetAddonsFolderPath() => _addonsFolder.Path;
    }
}
