using UserService.Test.Application.DTOs.Input;
using UserService.Test.Application.Services.Implementations;
using UserService.Test.Infraestructure.Data.Persistence;
using UserService.Test.Infraestructure.Data.Repositories;
using UserService.Test.Application.Mappings;
using UserService.Test.Domain.Entities;
using UserService.Test.Infraestructure.Data.Helper;
using UserService.Test.Application.Helper;
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
            var response = await _userService.CreateUser(createdDTO);

            //Assert
            Assert.True(response.IsOperationSuccess == true);
            Assert.Contains(response.Message, Operation.CREATE_RECORD);

        }

        [Fact]
        public async void Register_User_With_Settings_Success()
        {
            //Arrange
            var createdDTO = new UserCreateDTO("TestName", "TestNickname", "TestPhotoUrl", "test@gmail.com", "TestPassword");

            //Act
            var response =  await _userService.CreateUser(createdDTO);
            var model = await _databaseContext.Users.FirstAsync();

            //Assert
            Assert.True(response.IsOperationSuccess == true);
            Assert.Contains(response.Message, Operation.CREATE_RECORD);
            Assert.NotNull(model.Settings);

        }

        [Fact]
        public async void Register_User_The_User_Already_Exists_Fail()
        {
            //Arrange
            var createdDTO = new UserCreateDTO("TestName", "TestNickname", "TestPhotoUrl", "test@gmail.com", "TestPassword");
            var model = createdDTO.ToEntityInputInsert();

            var createSameNicknameDTO = new UserCreateDTO("TestName2", "TestNickname", "TestPhotoUrl2", "test@gmail.com2", "TestPassword2");

            await _databaseContext.AddAsync(model);
            await _databaseContext.SaveChangesAsync();
            //Act
            var response =  await _userService.CreateUser(createSameNicknameDTO);

            //Assert
            Assert.True(response.IsOperationSuccess == false);
            Assert.Contains(response.Message, Operation.USER_EXISTS);
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
            Assert.Contains(response.Message, Operation.GET_ALL);

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
            Assert.Contains(response.Message, Operation.GET_SPECIFY);

        }

        [Fact]
        public async void Get_User_ById_The_User_Not_Exist_Fail()
        {
            //Arrange
            Guid id = new Guid();
            var createdDTO = new UserCreateDTO("TestName", "TestNickname", "TestPhotoUrl", "test@gmail.com", "TestPassword");
            var model = createdDTO.ToEntityInputInsert();

            await _databaseContext.Users.AddAsync(model);
            await _databaseContext.SaveChangesAsync();

            //Act
            var response = await _userService.User(id);

            //Assert
            Assert.Null(response.Response);
            Assert.Contains(response.Message, Operation.GET_SPECIFY_NOTFOUND);
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
            var response = await _userService.UpdateUser(updateDTO);
            var updatedModel = await _databaseContext.Users.FindAsync(model.Id);

            //Assert
            Assert.Equal(model.Id, updatedModel.Id);
            Assert.True(createdDTO.NickName != updatedModel.Nickname);
            Assert.Contains(response.Message, Operation.UPDATE_RECORD);

        }

        [Fact]
        public async void Delete_User_Success()
        {
            //Arrange
            var createdDTO = new UserCreateDTO("TestName", "TestNickname", "TestPhotoUrl", "test@gmail.com", "TestPassword");
            var deleteDTO = new UserDeleteDTO("TestNickname", "TestPassword");
            var model = createdDTO.ToEntityInputInsert();

            model.EncryptPasswordEntity(HashPassword.CreatePasswordHash(model.Password));

            await _databaseContext.Users.AddAsync(model);
            await _databaseContext.SaveChangesAsync();

            _databaseContext.Entry(model).State = EntityState.Detached;

            //Act
            var response =  await _userService.RemoveUser(deleteDTO);
            var getUserById = await _databaseContext.Users.FindAsync(model.Id);

            //Assert
            Assert.True(response.IsOperationSuccess == true);
            Assert.Null(getUserById);
            Assert.Contains(response.Message, Operation.DELETE_RECORD);
        }

        [Fact]
        public async void Delete_User_The_User_Not_Exists_Fail()
        {
            var createdDTO = new UserCreateDTO("TestName", "TestNickname", "TestPhotoUrl", "test@gmail.com", "TestPassword");
            var deleteDTO = new UserDeleteDTO("TestNickname2", "TestPassword");
            var model = createdDTO.ToEntityInputInsert();

            await _databaseContext.Users.AddAsync(model);
            await _databaseContext.SaveChangesAsync();

            _databaseContext.Entry(model).State = EntityState.Detached;

            //Act
            var response = await _userService.RemoveUser(deleteDTO);

            //Assert
            Assert.True(response.IsOperationSuccess == false);
            Assert.Contains(response.Message, Operation.GET_SPECIFY_NOTFOUND);
        }

        [Fact]
        public async void Delete_User_The_Password_Is_Not_Valid_Fail()
        {
            //Arrange
            var createdDTO = new UserCreateDTO("TestName", "TestNickname", "TestPhotoUrl", "test@gmail.com", "TestPassword");
            var deleteDTO = new UserDeleteDTO("TestNickname", "TestPassword2");
            var model = createdDTO.ToEntityInputInsert();

            model.EncryptPasswordEntity(HashPassword.CreatePasswordHash(model.Password));

            await _databaseContext.Users.AddAsync(model);
            await _databaseContext.SaveChangesAsync();

            _databaseContext.Entry(model).State = EntityState.Detached;
            //Act
            var response = await _userService.RemoveUser(deleteDTO);

            //Assert
            Assert.True(response.IsOperationSuccess == false);
            Assert.Contains(response.Message, Operation.DELETE_FAILED);
        }

        [Fact]
        public async void User_Password_IsEncrypted_Success()
        {
            //Arrange
            var createdDTO = new UserCreateDTO("TestName", "TestNickname", "TestPhotoUrl", "test@gmail.com", "TestPassword");
            var createResponse = await _userService.CreateUser(createdDTO);

            //Act
            var response = await _userService.AllUsers();

            //Assert
            Assert.True(createResponse.IsOperationSuccess == true);
            Assert.NotEqual(createdDTO.Password, response.ResponseList.First().Password);
        }

    }
}