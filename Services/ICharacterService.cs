using System.Collections.Generic;
using System.Threading.Tasks;
using Dotnet_rpg.Dtos.Character;
using Dotnet_rpg.Model;
using Dotnet_rpg.Models;

namespace Dotnet_rpg.Services
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
         Task<ServiceResponse< GetCharacterDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<AddCharacterDto>>> AddCharacter(Character newCharacter);
    }
}