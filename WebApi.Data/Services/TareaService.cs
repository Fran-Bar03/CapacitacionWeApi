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
                        tarea.Usuarioo = usuario;

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

        public async Task<IEnumerable<TareaModel?>> FindAll()
        {
            using NpgsqlConnection database = CreateConnection();
            string sqlQuery = "Select * from view_tarea";

            try
            {
                await database.OpenAsync();

                var result = await database.QueryAsync<TareaModel, UserModel, TareaModel>(
                    sqlQuery,
                 map: (task, user) =>
                 {
                     task.Usuarioo = user;
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


        #region Findone

        public async Task<TareaModel?> togglestatus(int idtask)
        {
            using NpgsqlConnection database = CreateConnection();

            string sqlQuery = "Select * from fun_task_togglestatus(" +
                "p_idTarea := @idtarea)";

            try
            {
                await database.OpenAsync();
                TareaModel? result = await database.QueryFirstOrDefaultAsync<TareaModel>(
                    sqlQuery,
                    param: new
                    {
                        idTarea = idtask
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
        public async Task<TareaModel?> Remove(int idtask)
        {
            using NpgsqlConnection database = CreateConnection();

            string SqlQuery = "Select * from fun_task_remove("+
                "p_idTarea := @idtarea)";
            
            try
            {
                await database.OpenAsync();

                TareaModel? result = await database.QueryFirstOrDefaultAsync<TareaModel>(
                    SqlQuery,
                    param: new
                    {
                        idTarea = idtask
                    }
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

        public async Task<TareaModel?> Update( int idtask, UpdateTareaDTO updateTareaDTO)
        {
            using NpgsqlConnection database = CreateConnection();

            string sqlQuery = "Select * from fun_task_update(" +
                "p_idTarea := @idtarea," +
                "p_tarea := @tarea," +
                "p_descripcion := @descripcion)";
            
            try
            {
                await database.OpenAsync();

                var result = await database.QueryFirstOrDefaultAsync<TareaModel>(
                    sqlQuery,
                    param: new
                    {
                        idTarea = idtask,
                        tarea = updateTareaDTO.Tarea,
                        descripcion = updateTareaDTO.Descripcion
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
    }
}
