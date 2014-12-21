using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Common;

namespace SRCalSynchWindowsService {
    public partial class Service1 : ServiceBase {
        private Scheduler mScheduler;

        public Service1() {
            InitializeComponent();
        }

        protected override void OnStart(string[] args) {
            mScheduler = new Scheduler(DateTime.Now.Millisecond.ToString(),"Windows Service");
            mScheduler.Start();
            mScheduler.writeEventLogEntry("Started Scheduler");
        }

        protected override void OnStop() {
            mScheduler.writeEventLogEntry("Stopping Scheduler");
            mScheduler.Stop();
        }
    }
}
