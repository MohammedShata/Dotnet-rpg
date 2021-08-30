using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet_rpg.Dtos.Character;
using Dotnet_rpg.Model;
using Dotnet_rpg.Models;
using Dotnet_rpg.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CharacterController : ControllerBase
    {

        private  static List<Character> Characters = new List<Character>{
            new Character(),
            new Character{
                Id=1,
                Name="sam"
            }
        };
        private readonly ICharacterService _ICharacterServices;
        public CharacterController(ICharacterService ICharacterServices)
        {
            _ICharacterServices = ICharacterServices;

        }
        [HttpGet("GetAll")]

        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
               return Ok (await _ICharacterServices.GetAllCharacters());
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {
              return Ok( await _ICharacterServices.GetCharacterById(id));
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {
 return Ok( await _ICharacterServices.AddCharacter(newCharacter));
        }
    }
}