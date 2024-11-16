using Microsoft.AspNetCore.Mvc;
using WebApi.Data.Interfaces;
using WebApi.DTOs.User;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CapacitacionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserService _service;

        public UserController(IUserService service) => _service = service;


        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            IEnumerable<UserModel> users = await _service.FindAll();

            return Ok(users);

            /*
   
            200 Ok
            201 Creted
            400 Bad request
            404 Not Found
            403 Forbidden
            401 Unauthorized
            500 Interal Server Error
            418 I'm a teapot

            */

        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> FindOne(int userid)
        {
            UserModel? user = await _service.FindOne(userid);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
        {
            UserModel? user = await _service.Create(createUserDto);

            if (user == null) return NotFound();
            return Ok(user);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int iduser, [FromBody] UpdateUserDto updateUserDto)
        {
            UserModel? task = await _service.Update(iduser, updateUserDto);
            return Ok(task);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{iduser}")]
        public async Task<IActionResult> Remove(int iduser)
        {
            UserModel? user = await _service.Remove(iduser);
           if (user == null) return NotFound();
            return Ok(user);
        }
    }
}
