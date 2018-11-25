using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledJobs.Infrastructure
{
    public interface ISchedulerRunner
    {
        Task Run();
    }
}
