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

            return addon;
        }

        private static string GetTitle(string txt)
        {
            var titleStart = txt.IndexOf("## Title:") + 9;
            var titleEnd = txt.Substring(titleStart).IndexOf("\n");
            var title = txt.Substring(titleStart, titleEnd).Trim();
            return title;
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
    }
}
