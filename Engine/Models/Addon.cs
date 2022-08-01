using System.Collections.Generic;

namespace Engine.Models
{
    internal class Addon
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? Version { get; set; }

        public bool IsActivated { get; set; }
        public bool IsInstalled { get; set; }
        public bool IsLibrary { get; set; }
        
        public List<string> DependsOn { get; set; }

        public Addon(string name)
        {
            Name = name;
            DependsOn = new List<string>();
        }
    }
}
