using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Dotnet_rpg.Data;
using Dotnet_rpg.Dtos.Character;
using Dotnet_rpg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_rpg.Services
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character{
                Id=1,
                Name="sam"
            }
        };
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
        }
      private int GetUserId()=> int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var ServiceResponse = new ServiceResponse<List<GetCharacterDto>>();

            Character character = _mapper.Map<Character>(newCharacter);
            character.User= await _context.Users.FirstOrDefaultAsync(c=>c.Id==GetUserId());
            _context.characters.Add(character);
            await _context.SaveChangesAsync();
            ServiceResponse.Data = await _context.characters
            .Where(c=>c.User.Id== GetUserId())
            .Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return ServiceResponse;

        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var ServiceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = await _context.characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id==GetUserId());
                if(character!=null)
                {
                _context.characters.Remove(character);
                await _context.SaveChangesAsync();
                ServiceResponse.Data = _context.characters
                .Where(c=>c.User.Id==GetUserId())
                .Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                } 
                else{
                      ServiceResponse.Success = false;
                ServiceResponse.Message = "Character not found.";

                }
            }
            catch (Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;

            }
            return ServiceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var ServiceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.characters
            .Include(c=>c.Weapon)
            .Include(c=>c.Skills)
            .Where(c => c.User.Id == GetUserId()).ToListAsync();
            ServiceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return ServiceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var ServiceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacters = await _context.characters
            .Include(c=>c.Weapon)
            .Include(c=>c.Skills)
            .FirstOrDefaultAsync(c => c.Id == id && c.User.Id==GetUserId());
            ServiceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacters);
            return ServiceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var ServiceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {

                Character character = await _context.characters
                .Include(c=>c.User)
                .FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);
                if(character.User.Id==GetUserId())
            {
                character.Name = updateCharacter.Name;
                character.HitPoints = updateCharacter.HitPoints;
                character.Strength = updateCharacter.Strength;
                character.Defence = updateCharacter.Defence;
                character.Intelligence = updateCharacter.Intelligence;
                character.Class = updateCharacter.Class;
                await _context.SaveChangesAsync();
                ServiceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            else
            {
                ServiceResponse.Success=false;
                ServiceResponse.Message="Character not found.";
            }
            }
            catch (Exception ex)
            {
                ServiceResponse.Data = null;
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;

            }
            return ServiceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            var response= new ServiceResponse<GetCharacterDto>();
            try
            {
                var character= await _context.characters
                .Include(c=>c.Weapon)
                .Include(c=>c.Skills)
                .FirstOrDefaultAsync(c=>c.Id== newCharacterSkill.CharacterId && c.User.Id== GetUserId());
                if(character ==null )
                {
                       response.Success=false;
                       response.Message="Character not Found";
                       return response;
                }
                var skill= await _context.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
                if(skill==null)
                {
                       response.Success=false;
                       response.Message="Skill not Found";
                       return response;
                }
              character.Skills.Add(skill);
              await _context.SaveChangesAsync();

              response.Data=_mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                  response.Success=false;
                  response.Message=ex.Message;
                  return response;
            }
           return response;
        }
    }
}