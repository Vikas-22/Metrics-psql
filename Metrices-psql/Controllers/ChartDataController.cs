using Metrices_psql.datalayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Metrices_psql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartDataController : ControllerBase
    {
        private readonly IPrometheusQueryServices _prometheusQueryServices;

        public ChartDataController(IPrometheusQueryServices prometheusQueryServices)
        {
            _prometheusQueryServices = prometheusQueryServices;
        }


        [HttpGet("ChartTotalEmployesByDepartment")]
        public async Task<IActionResult> ChartTotalEmployesByDepartment()
        {
            try
            {
                var result = await _prometheusQueryServices.ChartDataTotalEmployeesByDepartment();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error querying Promethues: {ex.Message}");
            }
        }

        [HttpGet("ChartDataTotalEmployes")]
        public async Task<IActionResult> ChartDataTotalEmployes()
        {
            try
            {
                var result = await _prometheusQueryServices.ChartDataTotalEmployes();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error querying Promethues: {ex.Message}");
            }
        }


        [HttpGet("ChartDataIndexCount")]
        public async Task<IActionResult> ChartDataIndexCount()
        {
            try
            {
                var result = await _prometheusQueryServices.ChartDataIndexCount();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error querying Promethues: {ex.Message}");
            }
        }
    }
}
