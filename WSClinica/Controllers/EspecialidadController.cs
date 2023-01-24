using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WSClinica.Data;
using WSClinica.Models;

namespace WSClinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {
        private readonly DbWSClinicaContext context;

        public EspecialidadController(DbWSClinicaContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Especialidad>> Get()
        {
            return context.Especialidades.ToList();
        }

        [HttpGet("{Id}")]
        public ActionResult<Especialidad> GetById(int ID)
        {
            Especialidad especialidad = (from e in context.Especialidades
                                         where e.Id == ID
                                         select e).SingleOrDefault();
            return especialidad;
        }

        [HttpPost]

        public ActionResult<Especialidad> Post(Especialidad especialidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Especialidades.Add(especialidad);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("{ID}")]
        public ActionResult<Especialidad> Put(int id, [FromBody] Especialidad especialidad)
        {
            if (id != especialidad.Id)
            {
                return BadRequest();
            }
            context.Entry(especialidad).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{ID}")]
        public ActionResult<Especialidad> Delete(int id)
        {
            var especialidad = (from e in context.Especialidades
                                        where e.Id == id
                                        select e).SingleOrDefault();

            if (especialidad == null)
            {
                return NotFound();
            }
            context.Especialidades.Remove(especialidad);
            context.SaveChanges();
            return especialidad;
        }
    }
}
