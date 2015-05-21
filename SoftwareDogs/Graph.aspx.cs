using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Support;
using System.Text;

using System.IO;

namespace SoftwareDogs
{
    public partial class Graph : System.Web.UI.Page
    {
        //static int[] aiKey;
        SqlConnection conConnection;
        DataSet dsType;
        DataSet dsUnit;
        string strConnection;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Boolean)Session["LOGIN"] == false) Response.Redirect("./default.aspx");
            System.Diagnostics.Debug.WriteLine("Page_Load");

            try
            {
                if (null == Session["dsType"])   //then this is a postback.
                {
                    //The Type:
                    CINIFile obj = new CINIFile();
                    string strConnection = obj.Read(MapPath("Support/Support.ini"), "SQLServerData");
                    //Get the data from the Server.
                    string strCommand = "SELECT acName, iTypeKey FROM vType ORDER BY acName DESC";
                    conConnection = new SqlConnection(strConnection);
                    conConnection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(strCommand, conConnection);
                    dsType = new DataSet();
                    adapter.Fill(dsType);
                    Session["dsType"] = dsType;  //Save the data for use after the page loses this dataset.
                    if (dsType.Tables.Count > 0)
                    {
                        cmbType.Items.Clear();
                        for (int i = dsType.Tables[0].Rows.Count - 1; i >= 0; i--)
                        {
                            string sText = dsType.Tables[0].Rows[i][0].ToString();
                            cmbType.Items.Add(new ListItem(sText, sText));
                        }
                    }

                    //The Unit:
                    strConnection = obj.Read(MapPath("Support/Support.ini"), "SQLServerData");
                    //Get the data from the Server.
                    strCommand = "SELECT acName, iUnitKey FROM vUnit ORDER BY acName DESC";
                    conConnection = new SqlConnection(strConnection);
                    conConnection.Open();
                    adapter = new SqlDataAdapter(strCommand, conConnection);
                    dsUnit = new DataSet();
                    adapter.Fill(dsUnit);
                    Session["dsUnit"] = dsUnit;  //Save the data for use after the page loses this dataset.
                    if (dsUnit.Tables.Count > 0)
                    {
                        cmbUnit.Items.Clear();
                        for (int i = dsUnit.Tables[0].Rows.Count - 1; i >= 0; i--)
                        {
                            string sText = dsUnit.Tables[0].Rows[i][0].ToString();
                            cmbUnit.Items.Add(new ListItem(sText, sText));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ;
            }
        }

        string myMonth(string sMonth)
        {
            sMonth = sMonth.ToUpper();
            if (sMonth == "JANUARY") return "01";
            if (sMonth == "FEBRUARY") return "02";
            if (sMonth == "MARCH") return "03";
            if (sMonth == "APRIL") return "04";
            if (sMonth == "MAY") return "05";
            if (sMonth == "JUNE") return "06";
            if (sMonth == "JULY") return "07";
            if (sMonth == "AUGUST") return "08";
            if (sMonth == "SEPTEMBER") return "09";
            if (sMonth == "OCTOBER") return "10";
            if (sMonth == "NOVEMBER") return "11";
            if (sMonth == "DECEMBER") return "12";
            return "00";
        }

        static bool bBusy = false;
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (bBusy == false)
            {
                bBusy = true;
                string sUserKey = Session["USERKEY"].ToString();
                CFileWrite fw = new CFileWrite();

                //CREATE PARAMETER CSV FILE.
                string sParameter = "";
                sParameter += "rate,1\r\n";
                sParameter += "xscale,20\r\n";
                sParameter += "ymax,1100\r\n";
                sParameter += "ymin,-1\r\n";
                sParameter += "title," + txtGraph.Text.Trim() + "\r\n";
                sParameter += "xlabel," + txtXAxis.Text.Trim() + "\r\n";
                sParameter += "ylabel," + txtYAxis.Text.Trim() + "\r\n";
                sParameter += "color," + cmbColor.Text.Trim() + "\r\n";
                sParameter += "output_location,C:\\Graph\\out\\\r\n";
                sParameter += "output_file,Graph" + sUserKey + ".png\r\n";
                sParameter += "data_in,C:\\Graph\\Out\\Data" + sUserKey + ".CSV\r\n";
                fw.Write("c:\\Graph\\out\\Parameter" + sUserKey + ".CSV", sParameter);

                //CREATE THE DATA.CSV FILE.
                CINIFile obj = new CINIFile();
                string strConnection = obj.Read(MapPath("Support/Support.ini"), "SQLServerData");
                //Get the data from the Server.
                string sWhere = "";
                sWhere += "datDateTime >= '" + cmbYearStart.Text + "-" + myMonth(cmbMonthStart.Text) + "-" + cmbDayStart.Text + " " + cmbHourStart.Text + ":" + cmbMinuteStart.Text + ":" + cmbSecondStart.Text + "' ";
                sWhere += "AND datDateTime <= '" + cmbYearEnd.Text + "-" + myMonth(cmbMonthEnd.Text) + "-" + cmbDayEnd.Text + " " + cmbHourEnd.Text + ":" + cmbMinuteEnd.Text + ":" + cmbSecondEnd.Text + "' ";
                sWhere += "AND acUnit = '" + cmbUnit.Text + "' ";
                sWhere += "AND acType = '" + cmbType.Text + "' ";
                //string strCommand = "SELECT TOP " + cmbPoints.Text + " convert(nvarchar(24), datDateTime, 121), dValue FROM vGraph WHERE " + sWhere + " ORDER BY datDateTime";
                //'SELECT top 200 [datDateTime], [dValue]  FROM [DogsData].[dbo].[tReading] Where [iTypeNumber] = 20 AND [iUnitNumber] = 15 GROUP BY [datDateTime], [dValue] ORDER BY max([datDateTime]) DESC'
                string strCommand = "SELECT top 200 convert(nvarchar(24), datDateTime, 121), dValue  FROM vGraph Where " + sWhere + "  GROUP BY datDateTime, dValue ORDER BY max(datDateTime) DESC"; // KDD
                conConnection = new SqlConnection(strConnection);
                conConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(strCommand, conConnection);
                dsType = new DataSet();
                adapter.Fill(dsType);
                if (dsType.Tables.Count > 0)
                {
                    int iCount = dsType.Tables[0].Rows.Count;
                    string sData = "";
                    for (int i = 0; i < iCount; i++)
                    {
                        sData += dsType.Tables[0].Rows[i][0].ToString().Trim() + "," + dsType.Tables[0].Rows[i][1].ToString().Trim().Trim()+ "\r\n";
                    }
                    fw.Write("c:\\Graph\\out\\Data" + sUserKey + ".CSV", sData);
                }

                //CREATE THE GRAPH.
                System.Diagnostics.Process proc;
                System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo(); 

                procInfo.UseShellExecute = true; // If this is false, only .exe's can be run.
                procInfo.WorkingDirectory = "C:\\Graph"; 
                procInfo.FileName = "c:\\Graph\\Quick_Plots.exe"; // simple batch file which has command line instructions, dir *.* etc
                procInfo.Arguments = " out\\Parameter" + sUserKey + ".CSV";
                procInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; 

                proc = System.Diagnostics.Process.Start(procInfo); 
                proc.WaitForExit(); // Waits for the process to end. 

                if(!proc.HasExited)
                {
                proc.Kill();
                }

                //DISPLAY THE GRAPH.
                try
                {
                    File.Delete("pixes/Graph" + sUserKey + ".png");
                }
                catch (Exception ex){
                    Console.WriteLine(ex);
                    ;
                }
                File.Copy("C:\\Graph\\out\\Graph" + sUserKey + ".png", MapPath("/Pixes") + "\\Graph" + sUserKey + ".png", true);
                imgHeader.ImageUrl = "pixes/Graph" + sUserKey + ".png";
                bBusy = false;
            }
        }

        protected void btnDone_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Menu.aspx", false);
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void timRegraph_Tick(object sender, EventArgs e)
        {
            btnAdd_Click(sender, e);
        }

        protected void chkTimer_CheckedChanged(object sender, EventArgs e)
        {
            //timRegraph.Enabled = chkTimer.Checked;
        }

        protected void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
