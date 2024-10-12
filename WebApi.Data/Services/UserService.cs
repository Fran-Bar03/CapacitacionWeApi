
using Dapper;
using Npgsql;
using WebApi.Data.Interfaces;
using WebApi.DTOs.User;
using WebApi.Models;

namespace WebApi.Data.Services;
public class UserService : IUserService
{
    private PostgresqlConfiguration _postgresqlConfiguration { get; set; }

    public UserService(PostgresqlConfiguration postgresqlConfiguration) =>  _postgresqlConfiguration = postgresqlConfiguration;

    public NpgsqlConnection CreateConnection() => new NpgsqlConnection(_postgresqlConfiguration.Connection);
    

    #region Create
    public Task<UserModel> Create(CreateUserDto createUserDto)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region FindAll
    public async Task<IEnumerable<UserModel>> FindAll()
    {
        string sqlQuery = "select* from view_usuario";

        using NpgsqlConnection database = CreateConnection();

        try
        {
            await database.OpenAsync();
            IEnumerable<UserModel> users = await database.QueryAsync<UserModel>(sqlQuery);

            await database.CloseAsync();

            return users;
        }

        catch (Exception ex)
        {
            return [];
        }
    }
        #endregion

    #region FindOne
        public Task<UserModel>? FindOne(int userId)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Remove
    public Task<UserModel> Remove(int userId)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Update
    public Task<UserModel>? Update(UpdateUserDto updateUserDto)
    {
        throw new NotImplementedException();
    }
    #endregion
}
    