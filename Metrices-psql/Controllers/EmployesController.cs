using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Metrices_psql;
using Metrices_psql.Models;
using Prometheus;

namespace Metrices_psql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployesController(ApplicationDbContext context)
        {
            _context = context;
        }
        private static readonly Counter EmployeeCreateCounter = Metrics.CreateCounter(
        "employee_create_total",
        "Total number of employees created");


        private static readonly Histogram RequestDuration = Metrics.CreateHistogram(
    "http_request_duration_seconds",
    "Duration of HTTP requests in seconds",
    new HistogramConfiguration
    {
        Buckets = Histogram.LinearBuckets(start: 0.1, width: 0.2, count: 5) // Configure buckets as needed
    }
    );

        // GET: api/Employes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employes>>> GetEmployes()
        {
            var timer = RequestDuration.NewTimer();

            try
            {
                if (_context.Employes == null)
                {
                    return NotFound();
                }
                
                return await _context.Employes.ToListAsync();
            }
            finally
            {
                timer.ObserveDuration();
            }


         
        }

        // GET: api/Employes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employes>> GetEmployes(int id)
        {
          if (_context.Employes == null)
          {
              return NotFound();
          }
            var employes = await _context.Employes.FindAsync(id);

            if (employes == null)
            {
                return NotFound();
            }

            return employes;
        }

        // PUT: api/Employes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployes(int id, Employes employes)
        {
            if (id != employes.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Employes>> PostEmployes(Employes employes)
        //{
        //  if (_context.Employes == null)
        //  {
        //      return Problem("Entity set 'ApplicationDbContext.Employes'  is null.");
        //  }
        //    _context.Employes.Add(employes);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetEmployes", new { id = employes.EmployeeId }, employes);
        //}

        [HttpPost]
        public async Task<ActionResult<Employes>> PostEmployees(Employes employee)
        {
            try
            {
                if (_context.Employes == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Employees' is null.");
                }

                _context.Employes.Add(employee);
                await _context.SaveChangesAsync();

                // Use CreatedAtAction with the appropriate route name and route values
                return CreatedAtAction("GetEmployes", new { id = employee.EmployeeId }, employee);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        // DELETE: api/Employes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployes(int id)
        {
            if (_context.Employes == null)
            {
                return NotFound();
            }
            var employes = await _context.Employes.FindAsync(id);
            if (employes == null)
            {
                return NotFound();
            }

            _context.Employes.Remove(employes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployesExists(int id)
        {
            return (_context.Employes?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
