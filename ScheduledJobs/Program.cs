using ScheduledJobs.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledJobs
{
    static class Program
    {

        private static readonly ISchedulerRunner _runner = new SchedulerRunner();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            #if DEBUG

                JobSchedulerService service = new JobSchedulerService(_runner);
                service.OnDebug();
                System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);

            #else

                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new JobSchedulerService()
                };
                ServiceBase.Run(ServicesToRun);

            #endif
        }
    }
}
