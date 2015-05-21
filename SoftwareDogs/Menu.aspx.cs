using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoftwareDogs
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Boolean)Session["LOGIN"] == false) Response.Redirect("./default.aspx");
        }

        //protected void btnLogin_Click(object sender, EventArgs e)
        //{
        //}

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session["Busy"] = false;
            Session["LOGIN"] = false;
            Response.Redirect("./default.aspx");
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            Session["Busy"] = false;
            Response.Redirect("./ChangePassword.aspx");
        }

        protected void btnSensor_Click(object sender, EventArgs e)
        {
            Session["Busy"] = false;
            Response.Redirect("./Sensor.aspx");
        }

        protected void btnUnit_Click(object sender, EventArgs e)
        {
            Session["Busy"] = false;
            Response.Redirect("./Unit.aspx");
        }

        protected void btnDepartment_Click(object sender, EventArgs e)
        {
            Session["Busy"] = false;
            Response.Redirect("./Department.aspx");
        }

        protected void btnData_Click(object sender, EventArgs e)
        {
            Session["Busy"] = false;
            Response.Redirect("./Reading.aspx");
        }

        protected void btnGraph_Click(object sender, EventArgs e)
        {
            Session["Busy"] = false;
            Response.Redirect("./Graph.aspx");
        }
    }
}
