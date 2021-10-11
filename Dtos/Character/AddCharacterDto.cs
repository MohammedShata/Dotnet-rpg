using Dotnet_rpg.Dtos.Weapon;
using Dotnet_rpg.Models;

namespace Dotnet_rpg.Dtos.Character
{
    public class AddCharacterDto
    {
        
        public string Name { get; set; }="frodo";
        public  int HitPoints { get; set; }=100;

        public int Strength { get; set; }=10;

        public int Defence { get; set; }=10;
        public int  Intelligence { get; set; }=10;

         public RpgClass Class { get; set; }=RpgClass.Knight;

         public GetWeaponDto Weapon {get;set;} 


    }
}