using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class UserModel
    {

       public int idUsuario { get; set; }

        public string Nombre { get; set; }

        public string Usuario { get; set; }

        public string Contrasena { get; set; }
    }
}
