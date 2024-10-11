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
        public Task<TareaModel> Create(CreateTareaDTO createTareaDTO);
        
    }
}
