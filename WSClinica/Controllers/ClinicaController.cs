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
    public class ClinicaController : ControllerBase
    {
        private readonly DbWSClinicaContext context;

        public ClinicaController(DbWSClinicaContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Clinica>> Get()
        {
            return context.Clinicas.ToList();
        }

        [HttpGet("{ID}")]
        public ActionResult<Clinica> GetById(int ID)
        {
            Clinica clinica = (from clini in context.Clinicas
                               where clini.ID == ID
                               select clini).SingleOrDefault();
            return clinica;
        }
        [HttpPost]

        public ActionResult<Clinica> Post(Clinica clinica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Clinicas.Add(clinica);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("{ID}")]
        public ActionResult<Clinica> Put(int id, [FromBody] Clinica clinica)
        {
            if (id != clinica.ID)
            {
                return BadRequest();
            }
            context.Entry(clinica).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{ID}")]
        public ActionResult<Clinica> Delete(int id)
        {
            var clinicaOriginal = (from c in context.Clinicas
                                   where c.ID == id
                                   select c).SingleOrDefault();

            if (clinicaOriginal == null)
            {
                return NotFound();
            }
            context.Clinicas.Remove(clinicaOriginal);
            context.SaveChanges();
            return clinicaOriginal;

        }

    }
}

