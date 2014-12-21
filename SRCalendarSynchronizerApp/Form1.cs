using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using System.Web;

namespace SRCalendarSynchronizerApp {
    public partial class Form1 : Form {
        private Scheduler mScheduler;
        private DBController mDBController;
        public Form1() {
            InitializeComponent();
            CommonMethods.setCache(HttpRuntime.Cache);
            mDBController = new DBController(DateTime.Now.Millisecond.ToString());
            mScheduler = new Scheduler(DateTime.Now.Millisecond.ToString(),"Windows App");
            mScheduler.Start();
            mScheduler.writeEventLogEntry("Started Scheduler");
            deriveUI();
        }
        protected override void OnClosed(EventArgs e) {
            mScheduler.writeEventLogEntry("Stopping Scheduler");
            mScheduler.Stop();
            base.OnClosed(e);
        }

        private void btnEnable_Click(object sender, EventArgs e) {
            lblError.Text = "";
            bool? isSynchronizationActive = true;
            int? synchronizationPeriodicityInMinutes = null;
            mDBController.setSynchronizationParameters(
                ref isSynchronizationActive,
                ref synchronizationPeriodicityInMinutes);
            mScheduler.PeriodicityInMinutes = synchronizationPeriodicityInMinutes.Value;
            mScheduler.IsSynchronizationEnabled = isSynchronizationActive.Value;
            deriveUI();
        }

        private void btnDisable_Click(object sender, EventArgs e) {
            lblError.Text = "";
            bool? isSynchronizationActive = false;
            int? synchronizationPeriodicityInMinutes = null;
            mDBController.setSynchronizationParameters(
                ref isSynchronizationActive,
                ref synchronizationPeriodicityInMinutes);
            mScheduler.PeriodicityInMinutes = synchronizationPeriodicityInMinutes.Value;
            mScheduler.IsSynchronizationEnabled = isSynchronizationActive.Value;
            deriveUI();
        }

        private void btnUpdateSynchronizationPeriodicity_Click(object sender, EventArgs e) {
            lblError.Text = "";
            int minutes=-1;
            bool inError = false;
            try {
                minutes = Convert.ToInt32(tbSynchronizationPeriodicity.Text);
            } catch {
                inError = true;
            }
            if (!inError) {
                if (minutes <= 0) {
                    inError = true;
                }
            }
            if (inError) {
                lblError.Text = "Must contain an integer that is greater than 0";
                tbSynchronizationPeriodicity.Select();
                tbSynchronizationPeriodicity.Focus();
            } else {
                bool? isSynchronizationActive = null;
                int? synchronizationPeriodicityInMinutes=minutes;
                mDBController.setSynchronizationParameters(
                    ref isSynchronizationActive, 
                    ref synchronizationPeriodicityInMinutes);
                mScheduler.PeriodicityInMinutes = synchronizationPeriodicityInMinutes.Value;
                mScheduler.IsSynchronizationEnabled = isSynchronizationActive.Value;
                deriveUI();
            }
        }
        private void deriveUI() {
            tbSynchronizationPeriodicity.Text = mScheduler.PeriodicityInMinutes.ToString();
            if (mScheduler.IsSynchronizationEnabled) {
                btnDisable.Enabled = true;
                btnEnable.Enabled = false;
                lblStatus.Text = "Enabled";
            } else {
                btnDisable.Enabled = false;
                btnEnable.Enabled = true;
                lblStatus.Text = "Disabled";
            }
        }
    }
}
