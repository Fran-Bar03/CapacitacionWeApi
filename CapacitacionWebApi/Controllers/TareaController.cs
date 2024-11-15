using Microsoft.AspNetCore.Mvc;
using WebApi.Data.Interfaces;
using WebApi.DTOs.Tareas;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CapacitacionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {

        ITareaService _service;
        public TareaController(ITareaService service) => _service = service;


        // GET: api/<TareaController>
        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            IEnumerable<TareaModel> task = await _service.FindAll();

            if (task.Count() == 0)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPut("togglestatus/{idtask}")] //togglestatus
        public async Task<IActionResult> togglestatus(int idtask)
        {
            TareaModel? status = await _service.togglestatus(idtask);
            return Ok(status);
        }
        

        // POST api/<TareaController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]  CreateTareaDTO createTareaDTO) 
        {
            TareaModel? task = await _service.Create(createTareaDTO);

            if (task == null) return NotFound();

            return Ok(task);
            
        }

        // PUT api/<TareaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTareaDTO updateTareaDTO)
        {
            TareaModel? task = await _service.Update(id,updateTareaDTO);
            return Ok(task);
        }
        
        // DELETE api/<TareaController>/5
        [HttpDelete("{idtask}")]
        public async Task<IActionResult> Remove(int idtask)
        {
            TareaModel? task = await _service.Remove(idtask);
            if (task == null) return NotFound();
            return Ok(task);
        }
    }
}
