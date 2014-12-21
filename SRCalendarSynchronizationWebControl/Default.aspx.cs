using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;

namespace SRCalendarSynchronizationWebControl {
    public partial class _Default : System.Web.UI.Page {
        private Common.DBController mDBController;
        private bool isLoggedIn {
            get {
                Object obj = Session["IsLoggedIn"];
                return obj == null ? false : ((bool)obj);
            }
            set {
                Session["IsLoggedIn"] = value;
            }
        }
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
        protected void Page_Load(object sender, EventArgs e) {
            mDBController = new DBController(Session.SessionID);
            if (isLoggedIn) {
                Server.Transfer("~/Controller.aspx");
            } else {
                tbId.Focus();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e) {
            string password = null;
            string id = null;
            lblError.Text = "";
            lblError.Visible = false;
            mDBController.getLoginParameters(out id,out password);
            if (
                id.ToLower().Equals(tbId.Text.ToLower()) &&
                password.Equals(tbPassword.Text)
                ) {
                isLoggedIn = true;
                userId = id;
                password = password;
                Server.Transfer("~/Controller.aspx");
            } else {
                lblError.Text = "Invalid Credentials";
                lblError.Visible = true;
                tbPassword.Focus();
            }
        }
    }
}
