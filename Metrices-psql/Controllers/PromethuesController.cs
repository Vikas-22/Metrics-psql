using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Metrices_API.Models;
using Prometheus;
using System.Diagnostics;
using Metrices_API.PrometheusQueryServices;

namespace Metrices_API.Controllers
{
    [Route("Metrices")]

    [ApiController]

    public class PromethuesController : Controller
    {
        private readonly IPrometheusQueryServices _prometheusQueryServices;
       
       public PromethuesController(IPrometheusQueryServices prometheusQueryServices)
        {
            _prometheusQueryServices = prometheusQueryServices;
        }

        //[HttpGet("CustomQuery")]
        //public async Task<IActionResult> CustomQuery(string customquery)
        //{
        //    try
        //    {
        //        var result = await _prometheusQueryServices.CustomQueryPromethues(customquery);

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error querying Promethues: {ex.Message}");
        //    }
        //}

        [HttpGet("TotalEmployees")]

        public async Task<IActionResult> TotalEmployees() 
        {
            try
            {
                var result = await _prometheusQueryServices.TotalEmployees();
                return Ok(result);


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error querying Promethues: {ex.Message}");
            }
        }

        [HttpGet("TotalIndexReached")]
        public async Task<IActionResult> TotalIndexReached()
        {
            try
            {
                var result = await _prometheusQueryServices.TotalIndexReached();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error querying Promethues: {ex.Message}");
            }
        }

        //[HttpGet("TotalEmployesByDepartment")]
        //public async Task<IActionResult> TotalEmployesByDepartment()
        //{
        //    try
        //    {
        //        var result = await _prometheusQueryServices.TotalEmployesByDepartment();
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error querying Promethues: {ex.Message}");
        //    }
        //}

        [HttpGet("EmployeeGetDuration")]
        public async Task<IActionResult> EmployeeGetDuration()
        {
            try
            {

                var result = await _prometheusQueryServices.EmployeeGetDuration();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error querying Promethues: {ex.Message}");
            }

        }

        }
}
