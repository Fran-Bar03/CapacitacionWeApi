using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class TareaModel
    {
        public int idTarea {  get; set; }

        public string Tarea { get; set; }

        public string Descripcion { get; set; }

        public bool Completada { get; set; }

        public int IdUsuario { get; set; }

        public UserModel Usuarioo { get; set; }

    }
}
