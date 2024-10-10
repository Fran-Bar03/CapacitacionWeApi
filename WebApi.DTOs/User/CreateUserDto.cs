using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.DTOs.User
{
    public class CreateUserDto
    {

        public string Names { get; set; }
        public string Username { get; set; }

        public string Password { get; set; } 
    }
}
