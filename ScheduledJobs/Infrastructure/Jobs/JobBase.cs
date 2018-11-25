using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledJobs.Infrastructure.Jobs
{
    public abstract class JobBase : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                await DoWork();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public abstract Task DoWork();
    }
}
