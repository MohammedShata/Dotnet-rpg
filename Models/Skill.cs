using System.Collections.Generic;
using Dotnet_rpg.Models;

namespace Dotnet_rpg.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage {get;set;}
        public List<Character> characters { get; set; }
    }
}