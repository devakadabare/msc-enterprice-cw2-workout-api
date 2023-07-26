using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutApi.Services;
using WorkoutApi.DTO;
using WorkoutApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WorkoutApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        //private readonly ILogger<UserController> _logger;
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            // _logger = logger;
            _reportService = reportService;
        }

        //get report by user id
        [HttpGet("api/Report/user/{userId}")]
        public async Task<List<ReportDTO>> Get(int userId)
        {
            //get all reports from the database
            return await _reportService.GetReportByUserIdAsync(userId);
        }
    }
}