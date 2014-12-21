using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Configuration;
using System.Diagnostics;

namespace Common {
    public class Scheduler {
        #region Variables
        public EventLog windowsLog = new EventLog("SR Calendar Synchronization");
        private Timer mRefreshTimer = null;
        private int mPeriodicityInMinutes;
        private bool mIsSynchronizationEnabled;
        private DBController mDBController;
        private string mSessionId;
        private string mSource;
        private bool mWriteLogEntries;
        #endregion
        #region Properties

        public int PeriodicityInMinutes {
            get { return mPeriodicityInMinutes; }
            set { mPeriodicityInMinutes = value; }
        }

        public bool IsSynchronizationEnabled {
            get {
                return mIsSynchronizationEnabled;
            }
            set {
                mIsSynchronizationEnabled = value;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId">If comming from a web app, pass its SessionId string value,
        ///     otherwise, pass in any value.</param>
        /// <param name="source">Either the web app or the windows service</param>
        public Scheduler(string sessionId, string source) {
            mSessionId=sessionId;
            mSource = source;
            mDBController = new DBController(mSessionId);
            mWriteLogEntries =
                CommonRoutines.ObjectToBool(ConfigurationManager.AppSettings["WriteLogEntries"]);
        }


        #endregion

        #region Public Interface
        /// <summary>
        /// Perform startup functions
        /// </summary>
        public void Start() {
            // fetch the latest control values
            mDBController.getSynchronizationParameters(out mIsSynchronizationEnabled, out mPeriodicityInMinutes);
            startTimer();
        }
        /// <summary>
        /// Perform shutdown functions
        /// </summary>
        public void Stop() {
            stopTimer(true);
        }

        ///
        public void writeEventLogEntry(string entry) {
            if (mWriteLogEntries) {
                windowsLog.Source = mSource;
                try {
                    windowsLog.WriteEntry(entry);
                } catch { }
            }
        }

        #endregion

        #region Private Interface
        /// <summary>
        /// When starting the timer, set it so that it only pops once.  Then
        /// when it pops, start it again.
        /// </summary>
        private void startTimer() {
            stopTimer(false);
            double milliSeconds=mPeriodicityInMinutes*60*1000;
            writeEventLogEntry("Starting timer loop for every: "+mPeriodicityInMinutes+ " minutes.");
            /*The first time, we do it right away*/
            milliSeconds = 1000;
            mRefreshTimer = new System.Timers.Timer(milliSeconds);
            mRefreshTimer.AutoReset = false;
            mRefreshTimer.Elapsed += new System.Timers.ElapsedEventHandler(mRefreshTimer_Elapsed);
            mRefreshTimer.Start();
        }
        /// <summary>
        /// Timer has "popped".  
        /// Synchronize database, and then re-set the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mRefreshTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            if (mIsSynchronizationEnabled) {
                writeEventLogEntry("Timer popped; synchronizing data");
                mDBController.synchronizeData();
            }
            // update control values, in case they've changed
            mDBController.getSynchronizationParameters(out mIsSynchronizationEnabled, out mPeriodicityInMinutes);
            mRefreshTimer.Interval = mPeriodicityInMinutes * 60 * 1000;
            // reset the timer
            mRefreshTimer.Start();
        }
        /// <summary>
        /// Stop the timer, if it's running
        /// </summary>
        private void stopTimer(bool writeLog) {
            if (writeLog) {
                writeEventLogEntry("Stopping timer loop.");
            }
            if (mRefreshTimer != null) {
                try {
                    mRefreshTimer.Stop();
                    mRefreshTimer.Dispose();
                } catch (Exception e) {
                }
            }
            mRefreshTimer = null;
        }
        #endregion
    }
}
