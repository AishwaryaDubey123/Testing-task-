using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using rpgAPI.Controller;
using rpgAPI.Model;
using rpgAPI.Service;

namespace rpgAPITest
{
    public class UnitTest1
    {
        [Fact]
        public void GetCharacter_ReturnsListOfCharacters()
        {
            // Arrange
            var mockCharacterService = new Mock<ICharacterService>();
            mockCharacterService.Setup(service => service.GetAllCharacter())
                                 .Returns(new ServiceResponse<List<Character>> { Data = new List<Character>() });

            var controller = new CharacterController(mockCharacterService.Object);

            // Act
            var result = controller.GetCharacter();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var serviceResponse = Assert.IsType<ServiceResponse<List<Character>>>(okResult.Value);
            Assert.NotNull(serviceResponse.Data);
        }

        [Fact]
        public void GetId_ReturnsCharacterById()
        {
            // Arrange
            var id = 1;
            var mockCharacterService = new Mock<ICharacterService>();
            mockCharacterService.Setup(service => service.GetCharacterById(id))
                                 .Returns(new ServiceResponse<Character> { Data = new Character() });

            var controller = new CharacterController(mockCharacterService.Object);

            // Act
            var result = controller.GetId(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var serviceResponse = Assert.IsType<ServiceResponse<Character>>(okResult.Value);
            Assert.NotNull(serviceResponse.Data);
        }

        [Fact]
        public void PostCharacter_ReturnsNewCharacterList()
        {
            // Arrange
            var mockCharacterService = new Mock<ICharacterService>();
            mockCharacterService.Setup(service => service.AddCharacter(It.IsAny<Character>()))
                                 .Returns<Character>(character => new ServiceResponse<List<Character>> { Data = new List<Character> { character } });

            var controller = new CharacterController(mockCharacterService.Object);

            // Act
            var result = controller.PostCharacter(new Character());

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var serviceResponse = Assert.IsType<ServiceResponse<List<Character>>>(okResult.Value);
            Assert.NotNull(serviceResponse.Data);
            Assert.NotEmpty(serviceResponse.Data);
        }

        [Fact]
        public void UpdateCharacter_ReturnsUpdatedCharacter()
        {
            // Arrange
            var id = 1;
            var mockCharacterService = new Mock<ICharacterService>();
            mockCharacterService.Setup(service => service.UpdateCharacter(id, It.IsAny<Character>()))
                                 .Returns<int, Character>((_, character) => new ServiceResponse<Character> { Data = character });

            var controller = new CharacterController(mockCharacterService.Object);

            // Act
            var result = controller.UpdateCharacter(id, new Character());

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var serviceResponse = Assert.IsType<ServiceResponse<Character>>(okResult.Value);
            Assert.NotNull(serviceResponse.Data);
        }

        [Fact]
        public void DeleteCharacter_ReturnsUpdatedCharacterList()
        {
            // Arrange
            var id = 1;
            var mockCharacterService = new Mock<ICharacterService>();
            mockCharacterService.Setup(service => service.DeleteCharacter(id))
                                 .Returns(new ServiceResponse<List<Character>> { Data = new List<Character>() });

            var controller = new CharacterController(mockCharacterService.Object);

            // Act
            var result = controller.DeleteCharacter(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var serviceResponse = Assert.IsType<ServiceResponse<List<Character>>>(okResult.Value);
            Assert.NotNull(serviceResponse.Data);
        }
    }
}
