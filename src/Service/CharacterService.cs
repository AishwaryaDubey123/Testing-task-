using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rpgAPI.Service
{
    public class CharacterService : ICharacterService
    {


        private static List<Character> _characterList = new List<Character>()
        {
            new Character(),
            new Character(){Name = "Gollum", Id = 1},
        };


        public ServiceResponse<List<Character>> GetAllCharacter()
        {
            var serviceResponse = new ServiceResponse<List<Character>>()
            {
                Data = _characterList
            };
            return serviceResponse;
        }

        public ServiceResponse<List<Character>> AddCharacter(Character newCharacter)
        { 
             _characterList.Add(newCharacter);
            
            var serviceResponse = new ServiceResponse<List<Character>>()
            {
                Data = _characterList
            };
            return serviceResponse;

        }

        public ServiceResponse<Character> GetCharacterById(int id)
        {
            var character = _characterList.FirstOrDefault(c=>c.Id==id);

            var serviceResponse = new ServiceResponse<Character>();

            if (character == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Id Doesn't Exist";

                return serviceResponse;
            }

            serviceResponse.Data = character;

            return serviceResponse;
        }


        public ServiceResponse<Character> UpdateCharacter(int id, Character updatedCharacter)
        {
            var existingCharacter = _characterList.FirstOrDefault(c => c.Id == id);

            if (existingCharacter == null)
            {
                return new ServiceResponse<Character>()
                {
                    Success = false,
                    Message = "Character not found."
                };
            }

            // Update the character's properties
            existingCharacter.Name = updatedCharacter.Name;
            existingCharacter.CharacterClass = updatedCharacter.CharacterClass; // Use CharacterClass property

            return new ServiceResponse<Character>()
            {
                Data = existingCharacter
            };
        }



         public ServiceResponse<List<Character>> DeleteCharacter(int id)
        {
            var characterToRemove = _characterList.FirstOrDefault(c => c.Id == id);

            if (characterToRemove == null)
            {
                return new ServiceResponse<List<Character>>()
                {
                    Success = false,
                    Message = "Character not found."
                };
            }

            _characterList.Remove(characterToRemove);
            return new ServiceResponse<List<Character>>()
            {
                Data = _characterList
            };
        }
        
        
    }
}