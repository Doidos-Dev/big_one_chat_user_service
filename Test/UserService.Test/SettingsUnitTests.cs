using UserService.Test.Application.Services.Implementations;
using UserService.Test.Domain.Entities;
using UserService.Test.Infraestructure.Data.Helper;
using UserService.Test.Infraestructure.Data.Persistence;
using UserService.Test.Infraestructure.Data.Repositories;
using UserService.Test.Application.Helper;
using UserService.Test.Application.DTOs.Input;


namespace UserService.Test
{
    public class SettingsUnitTests : IDisposable
    {
        private readonly SettingsRepository _settingsRepository;
        private readonly SettingsService _settingsService;
        private readonly static string _databaseName = "SettingsTestDatabase";
        private readonly DatabaseContext _databaseContext = DatabaseUtils.CreateDbContextInstance(_databaseName);

        public SettingsUnitTests()
        {
            _settingsRepository = new SettingsRepository(_databaseContext);
            _settingsService = new SettingsService(_settingsRepository);
        }

        public void Dispose()
        {
            DatabaseUtils.ClearDatabase(_databaseContext);
            _databaseContext.Dispose();
        }


        [Fact]
        public async void Get_Settings_By_Id_Success()
        {
            //Arrange
            var settings = new SettingsModel(Guid.NewGuid(), Guid.NewGuid(), true, true, true);

            await _databaseContext.Settings.AddAsync(settings);
            await _databaseContext.SaveChangesAsync();

            //Act
            var response = await _settingsService.Settings(settings.Id);

            //Assert
            Assert.True(response.IsOperationSuccess == true);
            Assert.Contains(response.Message, Operation.GET_SPECIFY);
            Assert.Equal(response.Response.Id, settings.Id);
        }

        [Fact]
        public async void Get_Settings_By_Id_The_Settings_Not_Exists_Fail()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            var settings = new SettingsModel(Guid.NewGuid(), Guid.NewGuid(), true, true, true);

            await _databaseContext.Settings.AddAsync(settings);
            await _databaseContext.SaveChangesAsync();

            //Act
            var response = await _settingsService.Settings(id);

            //Assert
            Assert.True(response.IsOperationSuccess == false);
            Assert.Contains(response.Message, Operation.GET_SPECIFY_SETTINGS_NOTFOUND);
            Assert.Null(response.Response);
        }

        [Fact]
        public async void Get_Settings_By_UserId_Success()
        {
            //Arrange
            var settings = new SettingsModel(Guid.NewGuid(), Guid.NewGuid(), true, true, true);

            await _databaseContext.Settings.AddAsync(settings);
            await _databaseContext.SaveChangesAsync();

            //Act
            var response = await _settingsService.SettingsByUserId(settings.UserId);

            //Assert
            Assert.True(response.IsOperationSuccess == true);
            Assert.Contains(response.Message, Operation.GET_SPECIFY);
            Assert.Equal(response.Response.UserId, settings.UserId);
        }

        [Fact]
        public async void Get_Settings_By_UserId_The_User_Not_Exists_Fail()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            var settings = new SettingsModel(Guid.NewGuid(), Guid.NewGuid(), true, true, true);

            await _databaseContext.Settings.AddAsync(settings);
            await _databaseContext.SaveChangesAsync();

            //Act
            var response = await _settingsService.SettingsByUserId(id);

            //Assert
            Assert.True(response.IsOperationSuccess == false);
            Assert.Contains(response.Message, Operation.GET_SPECIFY_NOTFOUND);
            Assert.Null(response.Response);
        }

        [Fact]
        public async void Update_Settings_Success()
        {
            //Arrange
            var settings = new SettingsModel(Guid.NewGuid(), Guid.NewGuid(), true, true, true);

            await _databaseContext.Settings.AddAsync(settings);
            await _databaseContext.SaveChangesAsync();

            var updateDTO = new SettingsUpdateDTO(settings.Id, settings.UserId, false, true, true);

            //Act
            var response = await _settingsService.UpdateSettings(updateDTO);
            var getUpdatedSettings = await _databaseContext.Settings.FindAsync(settings.Id);

            //Assert
            Assert.True(response.IsOperationSuccess == true);
            Assert.Contains(response.Message, Operation.UPDATE_RECORD);
            Assert.True(settings.IsVisibleStatus != getUpdatedSettings.IsVisibleStatus);
        }

        [Fact]
        public async void Update_Settings_The_Settings_Is_Not_Found_Fail()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            var settings = new SettingsModel(Guid.NewGuid(), Guid.NewGuid(), true, true, true);

            await _databaseContext.Settings.AddAsync(settings);
            await _databaseContext.SaveChangesAsync();

            var updateDTO = new SettingsUpdateDTO(id, settings.UserId, false, true, true);

            //Act
            var response = await _settingsService.UpdateSettings(updateDTO);
            var getUpdatedSettings = await _databaseContext.Settings.FindAsync(id);

            //Assert
            Assert.True(response.IsOperationSuccess == false);
            Assert.Contains(response.Message, Operation.GET_SPECIFY_SETTINGS_NOTFOUND);
            Assert.Null(response.Response);
            Assert.Null(getUpdatedSettings);
        }
    }
}
