using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DTOs.Tareas;
using WebApi.Models;

namespace WebApi.Data.Interfaces
{
    public interface ITareaService
    {
        public Task<TareaModel?> Create(CreateTareaDTO createTareaDTO);

        public Task<TareaModel?> Update(int idtask, UpdateTareaDTO updateTareaDTO);

        public Task<IEnumerable<TareaModel?>> FindAll(int userId);

        public Task<TareaModel?> Remove(int idtask);

        public Task<TareaModel?> togglestatus(int idtask);




    }
}
