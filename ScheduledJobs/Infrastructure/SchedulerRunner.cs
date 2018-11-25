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
                await SetupScheduler();
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
        }

        private async Task SetupScheduler()
        {
            var tenants = new List<string>() {
                { "T0" },
                { "T1" },
                { "T2" },
                { "T3" },
                { "T4" },
                { "T5" },
                { "T6" },
                { "T7" },
                { "T8" },
                { "T9" }
            };

            var classList = new List<string>() {
                { "Job0" },
                { "Job1" },
                { "Job2" },
                { "Job3" },
                { "Job4" }
            };

            // Get scheduler instance
            var factory = new StdSchedulerFactory(Config());
            var scheduler = await factory.GetScheduler();

            // Start it up
            await scheduler.Start();

            foreach (var tenant in tenants)
            {
                foreach (var className in classList)
                {
                    // Setup jobs, triggers, and schedule jobs
                    var type = Type.GetType("ScheduledJobs.Infrastructure.Jobs." + className);

                    var job = JobBuilder.Create(type)
                    .WithIdentity("job" + tenant + className, "group" + tenant + className)
                    .Build();

                    var trigger = TriggerBuilder.Create()
                        .WithIdentity("trigger" + tenant + className, "group" + tenant + className)
                        .StartNow()
                        .WithSimpleSchedule(x => x
                            .WithIntervalInSeconds(5)
                            .RepeatForever())
                        .Build();

                    // Schedule the job using the trigger
                    await scheduler.ScheduleJob(job, trigger);
                }
            }
        }

        private NameValueCollection Config()
        {
            return new NameValueCollection
            {
                { "quartz.serializer.type", "binary" },
                { "quartz.threadPool.threadCount", "100" }
            };
        }
    }
}
