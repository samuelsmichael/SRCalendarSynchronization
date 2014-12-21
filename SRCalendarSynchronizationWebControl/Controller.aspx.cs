using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;

namespace SRCalendarSynchronizationWebControl {
    public partial class Controller : System.Web.UI.Page {
        Common.DBController mDBController;
        private string userId {
            get {
                return (String)Session["UserId"];
            }
            set {
                Session["UserId"] = value;
            }
        }
        private string password {
            get {
                return (String)Session["password"];
            }
            set {
                Session["password"] = value;
            }
        }
        private bool isLoggedIn {
            get {
                Object obj = Session["IsLoggedIn"];
                return obj == null ? false : ((bool)obj);
            }
            set {
                Session["IsLoggedIn"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e) {
            if (!isLoggedIn) {
                Server.Transfer("~/Default.aspx");
            } else {
                mDBController = new DBController(Session.SessionID);
                if (!IsPostBack) {
                    updateUI();
                }
            }
        }

        private void updateUI() {
            bool isSynchronizationEnabled;
            int periodicityInMinutes;
            mDBController.getSynchronizationParameters(out isSynchronizationEnabled, out periodicityInMinutes);
            lblStatus.Text =
                isSynchronizationEnabled ? "Enabled" : "Disabled";
            tbSynchronizationPeriodicity.Text = periodicityInMinutes.ToString();
            btnDisable.Enabled =
                isSynchronizationEnabled ? true : false;
            btnEnable.Enabled =
                isSynchronizationEnabled ? false : true;
            tbId.Text = userId;
            tbPassword.Text = password;
        }
        protected void btnEnable_Click(object sender, EventArgs e) {
            bool? isSynchronizationActive = true;
            int? synchronizationPeriodicityInMinutes = null;
            mDBController.setSynchronizationParameters(
                ref isSynchronizationActive,
                ref synchronizationPeriodicityInMinutes);
            updateUI();
        }

        protected void btnDisable_Click(object sender, EventArgs e) {
            bool? isSynchronizationActive = false;
            int? synchronizationPeriodicityInMinutes = null;
            mDBController.setSynchronizationParameters(
                ref isSynchronizationActive,
                ref synchronizationPeriodicityInMinutes);
            updateUI();

        }

        protected void btnUpdateSynchronizationPeriodicity_Click(object sender, EventArgs e) {
            int minutes = Convert.ToInt32(tbSynchronizationPeriodicity.Text);
            bool? isSynchronizationActive = null;
            int? synchronizationPeriodicityInMinutes = minutes;
            mDBController.setSynchronizationParameters(
                ref isSynchronizationActive,
                ref synchronizationPeriodicityInMinutes);
        }

        protected void btnUpdateCredentials_Click(object sender, EventArgs e) {
            string id = tbId.Text;
            string pwd = tbPassword.Text;
            lblPasswordMustNotBeBlank.Visible = false;
            if (pwd.Trim() == "") {
                lblPasswordMustNotBeBlank.Visible = true;
                return;
            }
            mDBController.setLoginParamenters(ref id, ref pwd);
            userId = id;
            password = pwd;
        }
    }
}
