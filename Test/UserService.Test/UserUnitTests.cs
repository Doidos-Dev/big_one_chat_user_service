using UserService.Test.Application.DTOs.Input;
using UserService.Test.Application.Services.Implementations;
using UserService.Test.Infraestructure.Data.Persistence;
using UserService.Test.Infraestructure.Data.Repositories;
using UserService.Test.Application.Mappings;
using UserService.Test.Domain.Entities;
using UserService.Test.Infraestructure.Data.Helper;
using Microsoft.EntityFrameworkCore;

namespace UserService.Test
{
    public class UserUnitTests : IDisposable
    {
        private readonly UserRepository _userRepository;
        private readonly UserServiceTest _userService;
        private readonly DatabaseContext _databaseContext = DatabaseUtils.CreateDbContextInstance();

        public UserUnitTests()
        {
            _userRepository = new UserRepository(_databaseContext);
            _userService = new UserServiceTest(_userRepository);
        }

        public void Dispose()
        {
            DatabaseUtils.ClearDatabase(_databaseContext);
            _databaseContext.Dispose();
        }

        [Fact]
        public async void Register_User_Success()
        {
            //Arrange
            var createdDTO = new UserCreateDTO("TestName", "TestNickname", "TestPhotoUrl", "test@gmail.com", "TestPassword");

            //Act
            var response =  await _userService.CreateUser(createdDTO);

            //Assert
            Assert.True(response.IsOperationSuccess == true);
            
        }

        [Fact]
        public async void Get_All_Users_Success()
        {
            //Arrange
            var createdDTO = new UserCreateDTO("TestName", "TestNickname", "TestPhotoUrl", "test@gmail.com", "TestPassword");
            var createdDTO2 = new UserCreateDTO("TestName2", "TestNickname2", "TestPhotoUrl2", "test@gmail.com2", "TestPassword2");

            List<UserModel> modelList = [createdDTO.ToEntityInputInsert(), createdDTO2.ToEntityInputInsert()];

            await _databaseContext.Users.AddRangeAsync(modelList);
            await _databaseContext.SaveChangesAsync();

            //Act
        
            var response = await _userService.AllUsers();

            //Assert
            Assert.Equal(modelList.Count, response.ResponseList.Count());
            Assert.Contains(response.ResponseList, u => u.Name == "TestName");
            Assert.Contains(response.ResponseList, u => u.Name == "TestName2");
        }

        [Fact]
        public async void Get_User_ById_Success()
        {
            //Arrange
            var createdDTO = new UserCreateDTO("TestName", "TestNickname", "TestPhotoUrl", "test@gmail.com", "TestPassword");
            var model = createdDTO.ToEntityInputInsert();

            await _databaseContext.Users.AddAsync(model);
            await _databaseContext.SaveChangesAsync();

            //Act
            var response = await _userService.User(model.Id);

            //Assert
            Assert.Equal(model.Id, response.Response.Id);
            Assert.Equal(model.Name, response.Response.Name);

        }

        [Fact]
        public async void Update_User_Success()
        {
            //Arrange
            var createdDTO = new UserCreateDTO("TestName", "TestNickname", "TestPhotoUrl", "test@gmail.com", "TestPassword");
            var model = createdDTO.ToEntityInputInsert();

            await _databaseContext.Users.AddAsync(model);
            await _databaseContext.SaveChangesAsync();

            model = await _databaseContext.Users.FindAsync(model.Id);
            model.ChangeNickName("ChangedNickName");

            _databaseContext.Entry(model).State = EntityState.Detached;

            var updateDTO = model.ToUpdateDTO();

            //Act
            await _userService.UpdateUser(updateDTO);
            var updatedModel = await _databaseContext.Users.FindAsync(model.Id);

            //Assert
            Assert.Equal(model.Id, updatedModel.Id);
            Assert.True(createdDTO.NickName != updatedModel.Nickname);

        }

    }
}