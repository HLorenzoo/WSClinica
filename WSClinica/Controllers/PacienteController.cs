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
    public class PacienteController : ControllerBase
    {
        private readonly DbWSClinicaContext context;

        public PacienteController(DbWSClinicaContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Paciente>> Get()
        {
            return context.Pacientes.ToList();
        }

        [HttpGet("{Id}")]
        public ActionResult<Paciente> GetById(int ID)
        {
            Paciente paciente = (from p in context.Pacientes
                                 where p.Id == ID
                                 select p).SingleOrDefault();
            return paciente;
        }

        [HttpPost]
        public ActionResult<Paciente> Post(Paciente paciente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Pacientes.Add(paciente);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("{Id}")]
        public ActionResult<Paciente> Put(int id, [FromBody] Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return BadRequest();
            }
            context.Entry(paciente).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{Id}")]
        public ActionResult<Paciente> Delete(int id)
        {
            var paciente = (from p in context.Pacientes
                                    where p.Id == id
                                    select p).SingleOrDefault();

            if (paciente == null)
            {
                return NotFound();
            }
            context.Pacientes.Remove(paciente);
            context.SaveChanges();
            return paciente;
        }
    }
}
