using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Metrices_psql.Models;
using Prometheus;
using Metrices_psql.datalayer;
using System.Diagnostics;

namespace Metrices_psql.Controllers
{
    public class PromethuesController : Controller
    {
        private readonly IPrometheusQueryServices _prometheusQueryServices;
       
       public PromethuesController(IPrometheusQueryServices prometheusQueryServices)
        {
            _prometheusQueryServices = prometheusQueryServices;
        }

        [HttpGet("CustomQuery")]
        public async Task<IActionResult> CustomQuery(string customquery)
        {
            try
            {
                var result = await _prometheusQueryServices.CustomQueryPromethues(customquery);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error querying Promethues: {ex.Message}");
            }
        }

        [HttpGet("TotalEmployeeinSystemPromethues")]

        public async Task<IActionResult> TotalEmployeeinSystemPromethues()
        {
            try
            {
                var result = await _prometheusQueryServices.TotalEmployesOverallSystemPromethues();
                return Ok(result);


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error querying Promethues: {ex.Message}");
            }
        }

        [HttpGet("TotalIndexReachedPromethues")]
        public async Task<IActionResult> TotalIndexReachedPromethues()
        {
            try
            {
                var result = await _prometheusQueryServices.TotalIndexReachedPromethues();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error querying Promethues: {ex.Message}");
            }
        }

        [HttpGet("TotalEmployesByDepartment")]
        public async Task<IActionResult> TotalEmployesByDepartment()
        {
            try
            {
                var result = await _prometheusQueryServices.TotalEmployesByDepartment();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error querying Promethues: {ex.Message}");
            }
        }



    }
}
