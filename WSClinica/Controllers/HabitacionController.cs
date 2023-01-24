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
    public class HabitacionController : ControllerBase
    {
        private readonly DbWSClinicaContext context;

        public HabitacionController(DbWSClinicaContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Habitacion>> Get()
        {
            return context.Habitaciones.ToList();
        }

        [HttpGet("{Id}")]
        public ActionResult<Habitacion> GetById(int ID)
        {
            Habitacion habitacion = (from hab in context.Habitaciones
                                     where hab.Id == ID
                                     select hab).SingleOrDefault();
            return habitacion;
        }
        [HttpPost]

        public ActionResult<Habitacion> Post(Habitacion habitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Habitaciones.Add(habitacion);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("{ID}")]
        public ActionResult<Habitacion> Put(int id, [FromBody] Habitacion habitacion)
        {
            if (id != habitacion.Id)
            {
                return BadRequest();
            }
            context.Entry(habitacion).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{ID}")]
        public ActionResult<Habitacion> Delete(int id)
        {
            var habitacion = (from h in context.Habitaciones
                                      where h.Id == id
                                      select h).SingleOrDefault();

            if (habitacion == null)
            {
                return NotFound();
            }
            context.Habitaciones.Remove(habitacion);
            context.SaveChanges();
            return habitacion;
        }

    }
}

