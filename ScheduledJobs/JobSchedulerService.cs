using ScheduledJobs.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledJobs
{
    public partial class JobSchedulerService : ServiceBase
    {

        private static ISchedulerRunner _runner;

        public JobSchedulerService(ISchedulerRunner runner)
        {
            InitializeComponent();
            _runner = runner;
        }

        protected override void OnStart(string[] args)
        {
            _runner.Run();
        }

        protected override void OnStop()
        {
            Console.WriteLine("Service stopped");
        }

        /// <summary>
        /// For debugging purposes.
        /// </summary>
        public void OnDebug()
        {
            OnStart(null);
        }
    }
}
