using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    internal class AddonsFolder
    {
        public string Path { get; set; }
        public bool IsDefault { get; set; }

        internal AddonsFolder(string? path)
        {
            if (string.IsNullOrEmpty(path))
            {
                Path = FindAddonsFolder();
                IsDefault = true;
            }
            else
            {
                Path = path;
                IsDefault = false;
            }
        }


        public static string FindAddonsFolder()
        {
            var folder = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Elder Scrolls Online\\live\\AddOns";
            return folder;
        }
    }
}
