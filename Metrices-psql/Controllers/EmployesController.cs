using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Metrices_API.Models;
using Prometheus;
using Metrices_API.datalayer;
using System.Diagnostics;
using Metrices_API.PrometheusQueryServices;

namespace Metrices_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployesController : ControllerBase
    {
        private readonly IEmployesRepository employesRepository;
        private readonly IPrometheusQueryServices prometheusQueryService;

        public EmployesController(IEmployesRepository _employesRepository, IPrometheusQueryServices _prometheusQueryService)
        {
            employesRepository = _employesRepository;
            prometheusQueryService = _prometheusQueryService;
        }



        //[HttpGet("TotalEmployeeinSystemPromethues")]

        //public async Task<IActionResult> TotalEmployeeinSystemPromethues()
        //{
        //    try
        //    {
        //        var result = await _prometheusQueryService.TotalEmployesinSystemPromethues();
        //        return Ok(result);


        //    }
        //    catch(Exception ex)
        //    {
        //        return StatusCode(500, $"Error querying Promethues: {ex.Message}");
        //    }
        //}

        //[HttpGet("TotalIndexReachedPromethues")]
        //public async Task<IActionResult> TotalIndexReachedPromethues()
        //{
        //    try
        //    {
        //        var result = await _prometheusQueryService.TotalIndexReachedPromethues();
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error querying Promethues: {ex.Message}");
        //    }
        //}

        [HttpGet("SetIndexCount")]
        public ActionResult<string> Indexcount()
        {
            prometheusQueryService.TotalIndexInc();
            return ("");

        }

        [HttpGet("Getallemployee")]
        public ActionResult<IEnumerable<Employes>> GetallEmployes()
        {
            var stopwatch = Stopwatch.StartNew();
            {
                var employes = employesRepository.GetallEmployes();

                if (employes == null)
                {
                    stopwatch.Stop();
                    prometheusQueryService.RecordEmployeeFindDuration(stopwatch.Elapsed);
                    return NotFound();
                }
                stopwatch.Stop();
                prometheusQueryService.RecordEmployeeFindDuration(stopwatch.Elapsed);
                return Ok(employes);
            }
        }

        [HttpPost("CreateEmployee")]
        public ActionResult<Employes> PostEmployees(Employes employee)
        {
            try
            {
                var createdEmployee = employesRepository.PostEmployees(employee);
                //prometheusQueryService.TotalEmployesIncBYDepartment(employee);
                prometheusQueryService.TotalEmployesInSystemIncandDec(1);
                return createdEmployee;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployes(int id)
        {
            var emplo = employesRepository.GetEmployes(id);
            var deleted = employesRepository.DeleteEmployes(id);
           // prometheusQueryService.TotalEmployesDecByDepartment(emplo);
            prometheusQueryService.TotalEmployesInSystemIncandDec(-1);

            if (!deleted)
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutEmployes(int id, Employes employes)
        {
            if (id != employes.EmployeeId)
            {
                return BadRequest();
            }

            var updatedEmployee = employesRepository.PutEmployes(id, employes);

            if (updatedEmployee == null)
            {
                return NotFound();
            }

            return Ok(updatedEmployee);
        }

        [HttpGet("{id}")]
        public ActionResult<Employes> GetEmployes(int id)
        {
            var employes = employesRepository.GetEmployes(id);
            return employes;
        }
    }



}

