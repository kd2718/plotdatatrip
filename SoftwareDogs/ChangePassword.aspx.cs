using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace SoftwareDogs
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Boolean)Session["LOGIN"] == false) Response.Redirect("./default.aspx");
            if ((Boolean)Session["Busy"] != true){
                Session["Busy"] = true;
                txtName.Text = (String)Session["LOGINNAME"];
                txtPassword.Text = (String)Session["PASSWORD"];
                txtConfirm.Text = (String)Session["PASSWORD"];
            }
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            String strConnection;
            String strCommand;
            SqlConnection conConnection;
            SqlCommand cmd;
            SqlDataReader dr;

            try
            {
                strConnection = "Data Source=RAYDAVIDSON-PC\\SQLEXPRESS;Initial Catalog=DogsLogin;Integrated Security=SSPI;";
                strCommand = "Execute pPasswordNew '" + ((String)Session["LOGINNAME"]).Replace("'", "''") + "', '" + ((String)Session["PASSWORD"]).Replace("'", "''") + "', '" + txtPassword.Text.Replace("'", "''") + "', '" + txtConfirm.Text.Replace("'", "''") + "', '" + txtName.Text.Replace("'", "''") + "'";
                conConnection = new SqlConnection(strConnection);
                conConnection.Open();
                cmd = new SqlCommand(strCommand, conConnection);
                dr = cmd.ExecuteReader();
                //conConnection.Close();
                //conConnection.Dispose();

                dr.Read();
                if (1 == dr.GetInt32(0))
                {
                    Session["LOGINNAME"] = txtName.Text;
                    Session["PASSWORD"] = txtPassword.Text;
                    Response.Redirect("./PasswordChanged.aspx", false);
                }
                else
                {
                    Response.Redirect("./PasswordUnchanged.aspx", false);
                }
            }
            catch (Exception ex)
            {
                ;
            }
            finally
            {
                Session["Busy"] = false;
            }
        }

        protected void btnAbort_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Menu.aspx", false);
        }
    }
}
