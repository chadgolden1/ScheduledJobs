using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledJobs.Infrastructure.Jobs
{
    public class Job4 : JobBase
    {
        public override async Task DoWork()
        {
            await Console.Out.WriteLineAsync(GetType().ToString() + ": doing work...");
        }
    }
}
