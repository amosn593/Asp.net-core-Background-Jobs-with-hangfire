using BackGroundJobsWithHangfire.Jobs;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackGroundJobsWithHangfire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        // GET: api/<JobsController>
        [HttpPost("CreateBackgroundJob")]
        public ActionResult CreateBackgroundJob()
        {
            //Create a fire and forget job
            BackgroundJob.Enqueue<JobsWithDI>(x => x.WriteLogMessage("Background Job"));
            return Ok();
        }

        // GET api/<JobsController>/5
        [HttpPost("CreateScheduledJob")]
        public ActionResult CreateScheduledJob()
        {
            var TimeToFire = DateTime.UtcNow.AddSeconds(5);
            var TimeOffset = new DateTimeOffset(TimeToFire);

            
            BackgroundJob.Schedule<JobsWithDI>(x  => x.WriteLogMessage("Scheduled Job Triggered"), TimeOffset);

            return Ok();
        }
        // POST api/<JobsController>
        [HttpPost("CreateContinuationJob")]
        public ActionResult CreateContinuationJob()
        {
            var TimeToFire = DateTime.UtcNow.AddSeconds(5);
            var TimeOffset = new DateTimeOffset(TimeToFire);


            var JobId = BackgroundJob.Schedule<JobsWithDI>(x => x.WriteLogMessage("Scheduled Job Triggered"), TimeOffset);
            var JobId1 = BackgroundJob.ContinueJobWith<JobsWithDI>(JobId, x => x.WriteLogMessage("Continuation Job Triggered 1"));
            var JobId2 = BackgroundJob.ContinueJobWith<JobsWithDI>(JobId1, x => x.WriteLogMessage("Continuation Job Triggered 2"));
            return Ok();
        }

        // PUT api/<JobsController>/5
        [HttpPost("CreateRecurringJob")]
        public ActionResult CreateRecurringJob()
        {
            //Create a fire and forget job
            RecurringJob.AddOrUpdate<JobsWithDI>("JobId1", x => x.WriteLogMessage("Recurring Job"), "* * * * *");
            return Ok();
        }


       
    }
}
