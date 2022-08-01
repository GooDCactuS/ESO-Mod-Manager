using Engine.Models;
using System.Collections.Generic;

namespace Engine.Utils
{
    internal static class AddonInfoParser
    {
        internal static Addon ParseTxtIntoAddon(string title, string txt)
        {
            var addon = new Addon(title);
            addon.DependsOn = GetDependsOn(txt);
            addon.IsLibrary = IsLibrary(txt);
            addon.Description = GetDescription(txt);
            addon.Version = GetVersion(txt);

            return addon;
        }

        private static List<string> GetDependsOn(string txt)
        {
            var addons = new List<string>();

            addons.AddRange(GetRequiredDependsOn(txt));
            addons.AddRange(GetOptionalDependsOn(txt));

            return addons;
        }

        private static List<string> GetRequiredDependsOn(string txt)
        {
            var addons = new List<string>();
            var name = "## DependsOn:";

            var dependsOnString = GetSpecificString(txt, name);
            var dependsOnList = dependsOnString.Split(' ');
            foreach (var item in dependsOnList)
            {
                var values = item.Split(">=");
                addons.Add(values[0]);
            }

            return addons;
        }

        private static List<string> GetOptionalDependsOn(string txt)
        {
            var addons = new List<string>();
            var name = "## OptionalDependsOn:";

            var dependsOnString = GetSpecificString(txt, name);
            var dependsOnList = dependsOnString.Split(' ');
            foreach (var item in dependsOnList)
            {
                var values = item.Split(">=");
                addons.Add(values[0]);
            }

            return addons;
        }

        private static bool IsLibrary(string txt)
        {
            var name = "## IsLibrary:";
            var isLibraryString = GetSpecificString(txt, name);
            if (bool.TryParse(isLibraryString, out var result))
            {
                return result;
            }

            return false;
        }

        private static string GetDescription(string txt)
        {
            var name = "## Description:";
            var description = GetSpecificString(txt, name);

            return description;
        }

        private static string GetVersion(string txt)
        {
            var name = "## Version:";
            var version = GetSpecificString(txt, name);

            return version;
        }

        private static string GetSpecificString(string txt, string stringName)
        {
            var stringStart = txt.IndexOf(stringName);
            if (stringStart == -1)
            {
                return string.Empty;
            }

            stringStart += stringName.Length;
            var stringEnd = txt.Substring(stringStart).IndexOf("\n");
            var specificString = txt.Substring(stringStart, stringEnd).Trim();

            return specificString;
        }
    }
}
