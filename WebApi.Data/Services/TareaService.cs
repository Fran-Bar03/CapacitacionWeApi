using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Data.Interfaces;
using WebApi.DTOs.Tareas;
using WebApi.Models;
using Dapper;

namespace WebApi.Data.Services
{
    public class TareaService : ITareaService
    {
        private PostgresqlConfiguration _connection;

        public TareaService (PostgresqlConfiguration connection) => _connection = connection;

        private NpgsqlConnection CreateConnection() => new(_connection.Connection);


        #region Create

        public async Task<TareaModel?> Create(CreateTareaDTO createTareaDTO) 
        {
            using NpgsqlConnection database = CreateConnection();

            string sqlQuery = "select * from fun_task_create (" +
             "p_tarea := @task," +
             "p_descripcion := @descripcion," +
             "p_idUsuario := @userId" +
             ");";
            
            
            try
            {
                await database.OpenAsync();

                var result = await database.QueryAsync<TareaModel, UserModel, TareaModel>(
                    sqlQuery,
                    param: new

                    {
                        Task = createTareaDTO.Tarea,
                        descripcion = createTareaDTO.Descripcion,
                        userId = createTareaDTO.IdUsuario
                    },
                    map: (tarea, usuario) =>
                    {
                        tarea.Usuario = usuario;

                        return tarea;
                    },

                    splitOn: "UsuarioId"
                    );

                await database.CloseAsync();

                return result.FirstOrDefault();
                

            }
            
            catch (Exception ex) 
            {
                return null;
            }

        }
        
        #endregion

        #region FindAll

        public async Task<IEnumerable<TareaModel?>> FindAll(int userId)
        {
            using NpgsqlConnection database = CreateConnection();
            string sqlQuery = "Select * from view_tarea where idUsuario = @userId";

            try
            {
                await database.OpenAsync();

                var result = await database.QueryAsync<TareaModel, UserModel, TareaModel>(
                    sqlQuery,
                    param: new
                    {
                        userId
                    },
                 map: (task, user) =>
                 {
                     task.Usuario = user;
                     return task;
                 },
                 splitOn: "usuarioId"

                 );
                

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


        #endregion
    }
}
