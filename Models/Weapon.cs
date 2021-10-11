using Dotnet_rpg.Models;

namespace Dotnet_rpg.Models
{
    public class Weapon
    {
        public int Id {get;set;}
        public string Name { get; set; }
        public int Damage { get; set; } 
        public Character Character { get; set; }

        public int CharacterId { get; set; }
    }
}