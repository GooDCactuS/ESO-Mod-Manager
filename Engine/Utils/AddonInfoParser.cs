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

            var dependsOnStart = txt.IndexOf("## DependsOn:");
            if (dependsOnStart == -1)
            {
                return addons;
            }

            dependsOnStart += + 13;
            var dependsOnEnd = txt.Substring(dependsOnStart).IndexOf("\n");
            var dependsOnString = txt.Substring(dependsOnStart, dependsOnEnd).Trim();
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

            var dependsOnStart = txt.IndexOf("## OptionalDependsOn:");
            if (dependsOnStart == -1)
            {
                return addons;
            }

            dependsOnStart += 21;
            var dependsOnEnd = txt.Substring(dependsOnStart).IndexOf("\n");
            var dependsOnString = txt.Substring(dependsOnStart, dependsOnEnd).Trim();
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
            var isLibraryStart = txt.IndexOf("## IsLibrary:");
            if (isLibraryStart == -1)
            {
                return false;
            }

            isLibraryStart += 13;
            var isLibraryEnd = txt.Substring(isLibraryStart).IndexOf("\n");
            var isLibraryString = txt.Substring(isLibraryStart, isLibraryEnd).Trim();
            if (bool.TryParse(isLibraryString, out var result))
            {
                return result;
            }

            return false;
        }
    }
}
