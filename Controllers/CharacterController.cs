using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dotnet_rpg.Dtos.Character;
using Dotnet_rpg.Models;
using Dotnet_rpg.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class CharacterController : ControllerBase
    {

        private readonly ICharacterService _CharacterService;
        public CharacterController(ICharacterService CharacterService)
        {
            _CharacterService = CharacterService;

        }
       
        [HttpGet("GetAll")]

        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {      
               return Ok (await _CharacterService.GetAllCharacters());
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {
              return Ok( await _CharacterService.GetCharacterById(id));
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {
           return Ok( await _CharacterService.AddCharacter(newCharacter));
        }
        [HttpPut]

        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter (UpdateCharacterDto updateCharacter)
        {
            var response= await _CharacterService.UpdateCharacter(updateCharacter);
            if(response.Data==null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpDelete("{id}")]

        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Delete(int id)
        {
            var response= await _CharacterService.DeleteCharacter(id);
            if(response.Data==null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
          [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddCharacter(AddCharacterSkillDto newCharacterSkill)
        {
           return Ok( await _CharacterService.AddCharacterSkill(newCharacterSkill));
        }
    }
}