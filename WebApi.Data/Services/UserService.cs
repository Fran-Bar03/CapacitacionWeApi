
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

        Dictionary<int, List<TareaModel>> userTasks = [];
        try
        {
            await database.OpenAsync();

            IEnumerable<UserModel> result = await database.QueryAsync<UserModel, TareaModel, UserModel>(
                sql: sqlQuery,
                map: (user, task) =>
                {

                    List<TareaModel> currentTasks = [];
                    userTasks.TryGetValue(user.idUsuario, out currentTasks);

                    currentTasks ??= [];

                    if (currentTasks.Count == 0) currentTasks = [task];

                    else currentTasks.Add(task);

                    userTasks[user.idUsuario] = currentTasks;

                    return user;
                },
                splitOn: "idTarea"
            );

            await database.CloseAsync();

            IEnumerable<UserModel> users = result.Distinct().ToList().Select(user =>
            {
                user.Tareas = userTasks[user.idUsuario];
                return user;
            });
            

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
    