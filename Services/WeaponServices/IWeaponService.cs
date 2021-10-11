using System.Threading.Tasks;
using Dotnet_rpg.Dtos.Character;
using Dotnet_rpg.Dtos.Weapon;
using Dotnet_rpg.Models;

namespace Dotnet_rpg.Services
{
    public interface IWeaponService
    {
         Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}