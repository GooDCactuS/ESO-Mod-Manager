using Engine.Models;
using System;

namespace Engine.ViewModels
{
    public class ModManager
    {
        private AddonsFolder _addonsFolder;

        public ModManager(string? addonsFolderPath)
        {
            _addonsFolder = new AddonsFolder(addonsFolderPath);
        }

        public string GetAddonsFolderPath() => _addonsFolder.Path;
    }
}
