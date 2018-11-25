using Quartz;
using Quartz.Impl;
using ScheduledJobs.Infrastructure.Jobs;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledJobs.Infrastructure
{
    public class SchedulerRunner : ISchedulerRunner
    {
        public async Task Run()
        {
            try
            {
                // Get scheduler instance
                var factory = new StdSchedulerFactory(Config());
                var scheduler = await factory.GetScheduler();

                // Start it up
                await scheduler.Start();

                // Setup job and trigger
                var job = JobBuilder.Create<Job1>()
                .WithIdentity("job1", "group1")
                .Build();

                var trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(10)
                        .RepeatForever())
                    .Build();

                // Schedule the job using the trigger
                await scheduler.ScheduleJob(job, trigger);

                // Delay to show what's going on
                //await Task.Delay(TimeSpan.FromSeconds(30));

                // Shut her down
                //await scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
        }

        private NameValueCollection Config()
        {
            return new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
        }
    }
}
