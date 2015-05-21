using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Support;

namespace SoftwareDogs
{
    public partial class Reading : System.Web.UI.Page
    {
        static int[] aiKey;
        SqlConnection conConnection;
        DataSet ds;
        string strConnection;

        string sVal(string sValue)
        {
            try
            {
                return double.Parse(sValue).ToString();
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        int iVal(string sValue)
        {
            try
            {
                return int.Parse(sValue);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Boolean)Session["LOGIN"] == false) Response.Redirect("./default.aspx");
        
            System.Diagnostics.Debug.WriteLine("Page_Load");
            //Page.EnableViewState=true;
            //gData.EnableViewState = true;

            try
            {
                //this.Visible = true;

                if (null == Session["myDataSet"])
                {
                    CINIFile obj2 = new CINIFile();
                    strConnection = obj2.Read(MapPath("Support/Support.ini"), "SQLServer");
                    //Get the data from the Server.
                    string strCommand = "SELECT iReadingsKey, datDateTime, dValue, iUnitKey, iTypeKey FROM vReading ORDER BY datDateTime";
                    conConnection = new SqlConnection(strConnection);
                    conConnection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(strCommand, conConnection);
                    ds = new DataSet();
                    adapter.Fill(ds);
                    Session["myDataSet"] = ds;  //Save the data for use after the page loses this dataset.

                    if (ds.Tables.Count > 0)
                    {
                        gData.DataSource = ds;
                        gData.DataBind();

                        //Save the primary key as the datagrid refuses to keep it.
                        aiKey = new int[gData.Rows.Count];
                        for (int i = gData.Rows.Count - 1; i >= 0; i--)
                        {
                            //System.Diagnostics.Debug.WriteLine(ds.Tables[0].Rows[i][0].ToString());
                            aiKey[i] = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                        }
                        Session["KEY"] = aiKey;
                        //System.Diagnostics.Debug.WriteLine("Style " + gData.Columns[4].ControlStyle.GetType(). .ToString());
                        ;
                    }
                }
                else
                {
                    if (Session["EDITINDEX"] != null)
                    {
                        int iRow = iVal(Session["EDITINDEX"].ToString());
                        Session["acName"] = ((TextBox)gData.Rows[iRow].Cells[2].Controls[0]).Text;
                        Session["iReading"] = iVal(((TextBox)gData.Rows[iRow].Cells[3].Controls[0]).Text);
                        Session["iDepartmentKey"] = 123; //radrad
                    }

                    if (ds == null) ds = (DataSet)Session["myDataSet"];

                    if (gData.DataSource == null)
                    {
                        gData.DataSource = ds;
                        gData.DataBind();
                    }

                    if (!Page.IsPostBack)
                    {
                        System.Diagnostics.Debug.WriteLine("NOT IsPostback");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("IS IsPostback");
                    }
                }
            }
            catch (Exception ex)
            {
                ;
            }
            gData.Columns[0].Visible = false;
            gData.Columns[0].Visible = false;
        }

        protected void btnDone_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("btnAbort_Click");
            Session["myDataSet"] = null;
            Response.Redirect("./Menu.aspx", false);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("btnSave_Click");
            ds = (DataSet)Session["myDataSet"];
            ds.Tables[0].Rows.Add();
            Session["myDataSet"] = ds;
            gData.DataSource = ds;
            gData.DataBind();
        }

        protected void gData_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_SelectedIndexChanged");
        }

        protected void gData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_RowDeleting");

            aiKey = (int[])Session["KEY"];
            conConnection = new SqlConnection(strConnection);
            conConnection.Open();

            SqlCommand com = new SqlCommand("DELETE FROM tReading WHERE iReadingsKey = " + aiKey[e.RowIndex], conConnection);
            //gData.Columns[1].Visible = false;
            try
            {
                com.ExecuteNonQuery();
                Session["myDataSet"] = null;
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Oops " + ex.Message);
                lblMessage.Text = "That entry is attached to data and cannot be removed. Delete the data first.";
            }
        }

        protected void gData_RowEditing(object sender, GridViewEditEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_RowEditing");

            try
            {
                ds = (DataSet)Session["myDataSet"];
                gData.DataSource = ds;
                gData.EditIndex = e.NewEditIndex;
                gData.DataBind();
                Session["EDITINDEX"] = e.NewEditIndex;
                btnAdd.Enabled = false;
                btnDone.Enabled = false;
            }
            catch (Exception ex)
            {
                ;
            }
        }

        protected void gData_Sorting(object sender, GridViewSortEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_Sorting");
            ;
        }

        protected void gData_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_RowUpdating");

            int iRow = gData.EditIndex;
            int iReading = iVal(Session["iReading"].ToString());
            string sName = Session["acName"].ToString();
            int iDepartmentKey = iVal(Session["iDepartmentKey"].ToString());

            //Database design indicates iReading > 0 and unique.
            if (iReading > 0)
            {
                int i;
                int iBound = gData.Rows.Count - 1;

                ds = (DataSet)Session["myDataSet"];
                for (i = iBound; i >= 0; i--)
                {
                    if (i != iRow)
                    {
                        if (iVal(ds.Tables[0].Rows[iRow][3].ToString()) == iReading) break;
                    }
                }
                if (i < 0)
                {
                    aiKey = (int[])Session["KEY"];
                    conConnection = new SqlConnection(strConnection);
                    conConnection.Open();
                    string sSQL;
                    if (iRow > aiKey.Length - 1)
                    {
                        sSQL = "INSERT INTO tReading (iDepartmentKey, acName, iReadingNumber) VALUES (" + iDepartmentKey.ToString() + ", '" + sName.Replace("'", "''") + " ', " + iReading.ToString() + ")";
                    }
                    else
                    {
                        sSQL = "UPDATE tReading SET acName='" + sName.Replace("'", "''") + "',iReadingNumber=" + iReading.ToString() + " WHERE iReadingsKey=" + aiKey[iRow];
                    }
                    SqlCommand com = new SqlCommand(sSQL, conConnection);

                    try
                    {
                        com.ExecuteNonQuery();
                        Session["myDataSet"] = null;
                        Session["KEY"] = null;
                        Response.Redirect(Request.RawUrl);
                    }
                    catch (SqlException ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Oops " + ex.Message);
                        lblMessage.Text = "That entry is attached to data and cannot be changed.";
                    }
                }
                else
                {
                    lblMessage.Text = "That Reading Number is a duplicate, please choose another.";
                }
            }
            else
            {
                lblMessage.Text = "That Reading Number must be greater than zero.";
            }


            gData.DataBind();
            Session["myDataSet"] = null;
            btnDone.Enabled = true;
            btnAdd.Enabled = true;

            gData.EditIndex = -1;
        }

        protected void gData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_RowCancelingEdit");
            Session["myDataSet"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void gData_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_RowDeleted");
        }

        protected void gData_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_RowUpdated");
        }

        protected void gData_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_SelectedIndexChanging");
        }

        protected void gData_Sorted(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_Sorted");
        }

        protected void gData_Unload(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_Unload");
        }

        protected void RalphIt(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("RalphIt");
        }

        protected void gData_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_Load");
        }

        protected void gData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_RowDataBound");
        }

        protected void gData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_RowCommand");
        }

        protected void gData_DataBinding(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_DataBinding");
        }

        protected void gData_DataBound(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_DataBound");
        }

        protected void gData_Disposed(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_Disposed");
        }

        protected void gData_Init(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_Init");
        }

        protected void gData_PageIndexChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_PageIndexChanged");
        }

        protected void gData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_PageIndexChanging");
        }

        protected void gData_RowCreated(object sender, GridViewRowEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gData_RowCreated");
        }
    }
}
