using System.Threading.Tasks;
using Dotnet_rpg.Dtos.Character;
using Dotnet_rpg.Dtos.Weapon;
using Dotnet_rpg.Models;
using Dotnet_rpg.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_rpg.Controllers
{    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponServices;
        public WeaponController(IWeaponService weaponServices)
        {
            _weaponServices = weaponServices;
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddWeapon(AddWeaponDto newWeapon)
        {
          return Ok(await _weaponServices.AddWeapon(newWeapon));
        }
    }
}