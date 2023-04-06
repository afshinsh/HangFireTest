using Hangfire;
using HangFireTest.Models;
using HangFireTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangFireTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly ILogger<ApiController> _logger;

        public ApiController(IJobService jobTestService, ILogger<ApiController> logger)
        {
            _jobService = jobTestService;
            _logger = logger;
        }

        [HttpPost("DelayedJob")]
        public IActionResult DelayedJob(RequestJobModel model)
        {
            var localTime = model.Time.ToLocalTime();
            var identifier = BackgroundJob.Schedule(() => _jobService.DelayedJob(model.Path),
                TimeSpan.FromMinutes(localTime.Subtract(DateTime.Now).TotalMinutes));
            
            return Ok(new ApiResponseModel { Data = Convert.ToInt32(identifier)
                , Message = "job with identifier " + identifier + " scheduled!", Success = true});
        }


        [HttpPost("EditDelayedJob/{jobId}")]
        public IActionResult EditDelayedJob(string jobId, RequestJobModel model)
        {
            BackgroundJob.Delete(jobId);
            var localTime = model.Time.ToLocalTime();
            var identifier = BackgroundJob.Schedule(() => _jobService.DelayedJob(model.Path),
                TimeSpan.FromMinutes(localTime.Subtract(DateTime.Now).TotalMinutes));

            return Ok(new ApiResponseModel
            {
                Data = Convert.ToInt32(identifier),
                Message = "job with identifier " + identifier + " scheduled!",
                Success = true
            });
        }

        [HttpPost("DailyReccuringJob")]
        public IActionResult ReccuringJob(RequestJobModel model)
        {
            
            RecurringJob.AddOrUpdate(() => _jobService.ReccuringJob(model.Path)
            , Cron.Daily(model.Time.Hour, model.Time.Minute), TimeZoneInfo.Local);
            return Ok(new ApiResponseModel { Message = "daily job scheduled", Success = true});
        }

    }
}
