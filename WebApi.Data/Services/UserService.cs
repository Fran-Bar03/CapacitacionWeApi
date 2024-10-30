
using Dapper;
using Npgsql;
using System.Data;
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
    public async Task<UserModel?> Create(CreateUserDto createUserDto)
    {
        using NpgsqlConnection database = CreateConnection();

        string sqlQuery = "select * from fun_user_create(" +
            "p_nombre := @nombre," +
            "p_usuario := @usuario," +
            "p_contrasena := @contrasena)";
        
        
        try
        {
            await database.OpenAsync();
            IEnumerable<UserModel?> result = await database.QueryAsync<UserModel>(
                sqlQuery,
                param: new
                {
                    nombre = createUserDto.Names,
                    usuario = createUserDto.Username,
                    contrasena = createUserDto.Password
                });
            await database.CloseAsync();
            return result.FirstOrDefault();
        }

        catch ( Exception ex )
        {
            return null;
        }
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
        public async Task<UserModel?> FindOne(int userId)
    {
        using NpgsqlConnection database = CreateConnection();
        string sqlQuery = "select * from usuario where idusuario = @idusuario"; 
        
        try
        {
            await database.OpenAsync();
            UserModel? result = await database.QueryFirstOrDefaultAsync<UserModel>(
                sqlQuery,
                param: new
                {
                    idusuario = userId
                });
            await database.CloseAsync();
            return result;
        }

        catch (Exception ex)
        {
            return null;
        }
        
    }
    #endregion

    #region Remove
    public async Task<UserModel> Remove(int userId)
    {
        NpgsqlConnection database = CreateConnection();
        string sqlQuery = "Select * from fun_user_remove(" +
            "p_idUsuario := @idusuario)";
        

        try
        {
            await database.OpenAsync();
            UserModel? result = await database.QueryFirstOrDefaultAsync<UserModel>(
                sqlQuery,
                param: new
                {
                    idusuario = userId
                });
            
            await database.CloseAsync();
            return result;
        }
        catch (Exception ex)
        {
            return null;
        }

        

    }
    #endregion
    
    #region Update
    public async Task<UserModel?> Update(int userid, UpdateUserDto updateUserDto)
    {
        NpgsqlConnection database = CreateConnection();
        string sqlQuery = "Select * from fun_user_update(" +
            "p_idUsuario := @idusuario," +
            "p_nombre := @nombre," +
            "p_usuario := @usuario," +
            "p_contrasena := @contrasena)";
        
        try
        {
            await database.OpenAsync();
            var result = await database.QueryAsync<UserModel>(
                sqlQuery,
                param: new
                {
                    idusuario = userid,
                    nombre = updateUserDto.Names,
                    usuario = updateUserDto.Username,
                    contrasena = updateUserDto.Password
                });
            await database.CloseAsync();
            return result.FirstOrDefault();
        }
        catch (Exception ex)
        {
            return null;
        } 
    }
    #endregion
}
    