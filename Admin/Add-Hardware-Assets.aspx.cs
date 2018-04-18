using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;

public partial class AddHardwareAssets : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        //ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        if (Session["SessionUserMailID"] != null && Session["SessionUserPosition"].ToString() == "Admin")
        {

            if (Session["SessionHWClick"].ToString() == "Asset Update" && Session["Sessiondefault"].ToString() == "yes")
            {
                updatingfun();
                updatebutton_div.Visible = true;
                Update.Visible = true;
                sw_Insertbutton_div.Visible = false;
                sw_Insert.Visible = false;
                Resetbutton_div.Visible = false;
                lbladdorupdate.Text = "Update";
            }
            else if (Session["SessionHWClick"].ToString() == "Asset Insert")
            {
                Resetbutton_div.Visible = true;
                Update.Visible = false;
                updatebutton_div.Visible = false;
                sw_Insert.Visible = true;
                sw_Insertbutton_div.Visible = true;
                lbladdorupdate.Text = "Add";
            }
            CalendarExtender1.StartDate = DateTime.Now.AddYears(-100);
            CalendarExtender2.StartDate = DateTime.Now.AddYears(-100);
            CalendarExtender1.EndDate = DateTime.Now;
            CalendarExtender2.EndDate = DateTime.Now;
            Session["AssetName"] = AMS_Asset_Name.Text;

            for (int i = 0; i <= 8; i++) { AMS_Asset_Locationn.Items[i].Attributes.Add("style", "color:black"); }
            for (int i = 0; i <= 2; i++) { selectyears.Items[i].Attributes.Add("style", "color:black"); }
            for (int i = 0; i <= 2; i++) { Warrenty_Status.Items[i].Attributes.Add("style", "color:black"); }
            YOMLabel.Visible = false;
            DOPLabel.Visible = false;
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
        //string userrole = Session["SessionUserRole"].ToString();
    }

    protected void AMS_Asset_Name_TextChanged(object sender, EventArgs e)
    {
        if (Page.IsPostBack == true && AMS_Asset_Name.Text != String.Empty.ToString())
        {
            Session["hwname"] = AMS_Asset_Name.Text;
            AMS_PH_Make.Focus();
        }
    }

    protected void AMS_PH_Make_TextChanged(object sender, EventArgs e)
    {
        if (Page.IsPostBack == true && AMS_Asset_Name.Text != String.Empty.ToString())
        {
            Session["hwmake"] = AMS_PH_Make.Text;
            AMS_PH_Model.Focus();
        }
    }

    protected void AMS_Date_of_Purchase_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == true)
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{4}$|^\d{4}-((0?\d)|(1[012]))-(((0?|[12])\d)|3[01])$");
                if (regex.IsMatch(AMS_Date_of_Purchase.Text))
                {
                    if (AMS_PH_yearofManufacturer.Text != string.Empty)
                    {
                        DateTime currentdate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                        DateTime manufacturedate = DateTime.Parse(AMS_PH_yearofManufacturer.Text, new CultureInfo("gu-IN", false));
                        DateTime purchasedate = DateTime.Parse(AMS_Date_of_Purchase.Text, new CultureInfo("gu-IN", false));
                        if (purchasedate > currentdate)
                        {

                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Date of purchase should not be greater than today');", true);
                            AMS_Date_of_Purchase.Text = null;
                            AMS_Date_of_Purchase.Focus();
                        }
                        else if (purchasedate < manufacturedate)
                        {

                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Date of purchase should not be Lesser than Year of Manufacturer.');", true);
                            AMS_Date_of_Purchase.Text = null;
                            AMS_Date_of_Purchase.Focus();
                        }
                        else
                        {
                            AMS_PH_Warranty.Focus();
                            DOPLabel.Visible = false;
                        }
                    }
                    else
                    {

                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('First you should fill Year of manufacture');", true);
                        //AMS_Date_of_Purchase.Text = String.Empty.ToString();
                        AMS_Date_of_Purchase.Text = null;
                        AMS_PH_yearofManufacturer.Focus();
                        DOPLabel.Visible = false;
                    }
                    DOPLabel.Visible = false;
                }
                else
                {

                    AMS_Date_of_Purchase.Text = "";
                    DOPLabel.Visible = true;
                }
                //Search_TextBox.Visible = false;
                //Search_LinkButton.Visible = false;
            }
            //Search_TextBox.Visible = false;
            //Search_LinkButton.Visible = false;
        }

        catch (Exception ex)
        {
			//string message = string.Format("Message: {0}\\n\\n", ex.Message);
			//message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
			//message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
			//message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
			//// ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
			//Session["eswar"] = message;

			//Session["pagename"] = "HardwareAssets.aspx";
			//Response.Redirect("Errorpage.aspx");
			throw ex;
        }
    }

    protected void Insert_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "master_hw_proc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AMS_AssetType", SqlDbType.NVarChar).Value = "Hardware Assets";
            cmd.Parameters.Add("@AMS_Asset_Location", SqlDbType.NVarChar).Value = AMS_Asset_Locationn.SelectedValue.Trim();
            cmd.Parameters.Add("@AMS_AssetName", SqlDbType.NVarChar).Value = AMS_Asset_Name.Text.Trim();
            cmd.Parameters.Add("@AMS_Asset_Size", SqlDbType.NVarChar).Value = "NA";
            cmd.Parameters.Add("@AMS_Asset_Cost", SqlDbType.Money).Value = Math.Round(float.Parse(AMS_Asset_Cost.Text.Trim()), 2, MidpointRounding.AwayFromZero);
            cmd.Parameters.Add("@AMS_Company_Location", SqlDbType.NVarChar).Value = AMS_Asset_Locationn.SelectedValue.Trim();
            cmd.Parameters.Add("@AMS_Date_of_Purchase", SqlDbType.NVarChar).Value = AMS_Date_of_Purchase.Text.Trim();
            cmd.Parameters.Add("@AMSHW_PH_Warranty", SqlDbType.NVarChar).Value = AMS_PH_Warranty.Text.Trim() + selectyears.SelectedValue;
            cmd.Parameters.Add("@AMSHW_PH_Characterstics", SqlDbType.NVarChar).Value = AMS_Characteristics.Text.Trim();
            //cmd.Parameters.Add("@AMSHW_PH_Allotted_Employee", SqlDbType.NVarChar).Value = AMSHW_PH_Allotted_Emp.Text.Trim();
            cmd.Parameters.Add("@AMSHW_PH_Allotted_Employee", SqlDbType.NVarChar).Value = "NA";
            cmd.Parameters.Add("@AMSHW_PH_Replacement_Date", SqlDbType.NVarChar).Value = "NA";
            cmd.Parameters.Add("@AMSHW_PH_Replacement_Reason", SqlDbType.NVarChar).Value = "NA";
            cmd.Parameters.Add("@AMSHW_PH_Depreciation_Cost", SqlDbType.Money).Value = 0;
            cmd.Parameters.Add("@AMSHW_PH_make", SqlDbType.NVarChar).Value = AMS_PH_Make.Text.Trim();
            cmd.Parameters.Add("@AMSHW_PH_Model", SqlDbType.NVarChar).Value = AMS_PH_Model.Text.Trim();
            cmd.Parameters.Add("@AMSHW_PH_Year_Of_Mfd", SqlDbType.NVarChar).Value = AMS_PH_yearofManufacturer.Text.Trim();

            if (AMS_Asset_Locationn.SelectedValue == "Vijayawada")
            {
                cmd.Parameters.Add("@SN_NAME", SqlDbType.NVarChar).Value = "SNVIJ";
            }
            else if (AMS_Asset_Locationn.SelectedValue == "Hyderabad")
            {
                cmd.Parameters.Add("@SN_NAME", SqlDbType.NVarChar).Value = "SNHYD";
            }
            else if (AMS_Asset_Locationn.SelectedValue == "Bengaluru")
            {
                cmd.Parameters.Add("@SN_NAME", SqlDbType.NVarChar).Value = "SNBEN";
            }
            else if (AMS_Asset_Locationn.SelectedValue == "Mumbai")
            {
                cmd.Parameters.Add("@SN_NAME", SqlDbType.NVarChar).Value = "SNMUM";
            }
            else
            {
                cmd.Parameters.Add("@SN_NAME", SqlDbType.NVarChar).Value = "NA";
            }

            if (Warrenty_Status.SelectedIndex == 0)
            {
                WarrentyStatusRequiredFieldValidator.IsValid = false;
            }
            else if (Warrenty_Status.SelectedIndex == 1 || Warrenty_Status.SelectedIndex == 2)
            {
                cmd.Parameters.Add("@HW_Warrenty_Status", SqlDbType.NVarChar).Value = Warrenty_Status.Text.Trim();
            }

            if (Remarks.Text == String.Empty.ToString())
            {
                cmd.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = "NA";
                cmd.Parameters.Add("@AMS_Remarks", SqlDbType.NVarChar).Value = "NA";
            }
            else if (Remarks.Text != String.Empty.ToString())
            {
                cmd.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = Remarks.Text.Trim();
                cmd.Parameters.Add("@AMS_Remarks", SqlDbType.NVarChar).Value = Remarks.Text.Trim();
            }

            if (selectyears.SelectedIndex == 1)
            {
                Int32 x = Convert.ToInt32(this.AMS_PH_Warranty.Text);
                DateTime EntryDate = Convert.ToDateTime(AMS_Date_of_Purchase.Text);
                string s = EntryDate.AddYears(x).ToShortDateString();
                cmd.Parameters.Add("@AMSHW_WarrantyExpireDate", SqlDbType.DateTime).Value = s;
                cmd.Parameters.Add("@AMSHW_Email", SqlDbType.NVarChar).Value = Session["SessionUserMailID"];
            }
            else if (selectyears.SelectedIndex == 2)
            {
                Int32 x = Convert.ToInt32(this.AMS_PH_Warranty.Text);
                DateTime EntryDate = Convert.ToDateTime(AMS_Date_of_Purchase.Text);
                string s = EntryDate.AddMonths(x).ToShortDateString();
                cmd.Parameters.Add("@AMSHW_WarrantyExpireDate", SqlDbType.DateTime).Value = s;
                cmd.Parameters.Add("@AMSHW_Email", SqlDbType.NVarChar).Value = Session["SessionUserMailID"];
            }


            //SqlConnection con1 = new SqlConnection(constr);
            //SqlCommand cmd1 = new SqlCommand();
            //DataTable dt1 = new DataTable();
            //SqlDataAdapter sd1 = new SqlDataAdapter("select s.AMSHW_PH_Serial_Number, a.AMS_AssetName from Asset_Master a join Hardware_Physical_Assets s on s.AMSHW_PH_AssetID = a.AMS_AssetID where s.AMSHW_PH_Serial_Number='" + AMS_PH_SerialNumber.Text + "'", con1);
            //sd1.Fill(dt1);
            DateTime manufacturedate = DateTime.Parse(AMS_PH_yearofManufacturer.Text, new CultureInfo("gu-IN", false));
            DateTime purchasedate = DateTime.Parse(AMS_Date_of_Purchase.Text, new CultureInfo("gu-IN", false));
            //if (dt1.Rows.Count >= 1)
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Asset already exists');", true);
            //    AMS_PH_SerialNumber.Text = string.Empty;
            //    AMS_PH_yearofManufacturer.Focus();
            //}
            if (purchasedate < manufacturedate)
            {

                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Year of Manufacturer should be Less than Date of purchase');", true);
                AMS_PH_yearofManufacturer.Text = null;
                AMS_PH_yearofManufacturer.Focus();
            }
            else
            {
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('Hardware Asset Inserted Successfully.');window.location.href='../Admin/Hardware-Assets.aspx';</script>");
            }
        }
        catch (Exception ex)
        {
            //string message = string.Format("Message: {0}\\n\\n", ex.Message);
            //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
            ////ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            //Session["eswar"] = message;

            //Session["pagename"] = "Add-Hardware-Assets.aspx";
            //Response.Redirect("Errorpage.aspx");
            throw ex;
        }
    }

    protected void AMS_PH_yearofManufacturer_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == true)
            {

                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{4}$|^\d{4}-((0?\d)|(1[012]))-(((0?|[12])\d)|3[01])$");
                if (regex.IsMatch(AMS_PH_yearofManufacturer.Text))
                {
                    if (AMS_PH_yearofManufacturer.Text != string.Empty && AMS_Date_of_Purchase.Text == string.Empty)
                    {

                        DateTime currentdate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                        DateTime manufacturedate = DateTime.Parse(AMS_PH_yearofManufacturer.Text, new CultureInfo("gu-IN", false));
                        if (currentdate < manufacturedate)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Year of Manufacturer should be Less than today.');", true);
                            AMS_Date_of_Purchase.Focus();
                        }
                        else
                        {
                            AMS_Characteristics.Focus();
                            //Label14.Visible = false;
                        }
                    }
                    else if (AMS_PH_yearofManufacturer.Text != string.Empty && AMS_Date_of_Purchase.Text != string.Empty)
                    {

                        DateTime currentdate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                        DateTime manufacturedate = DateTime.Parse(AMS_PH_yearofManufacturer.Text, new CultureInfo("gu-IN", false));
                        DateTime purchasedate = DateTime.Parse(AMS_Date_of_Purchase.Text, new CultureInfo("gu-IN", false));
                        if ((purchasedate < manufacturedate) && (currentdate <= manufacturedate))
                        {
                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Year of Manufacturer should be Less than today and Date of purchase.');", true);
                            AMS_PH_yearofManufacturer.Text = null;
                            AMS_PH_yearofManufacturer.Focus();

                        }
                        else
                        {
                            AMS_Characteristics.Focus();
                            //Label14.Visible = false;
                        }
                    }
                    else
                    {
                        AMS_Date_of_Purchase.Focus();
                        AMS_PH_yearofManufacturer.Focus();
                    }
                    YOMLabel.Visible = false;
                }
                else
                {

                    AMS_PH_yearofManufacturer.Text = "";
                    YOMLabel.Visible = true;
                }
            }
            //Search_TextBox.Visible = false;
            //Search_LinkButton.Visible = false;
        }
        catch (Exception ex)
        {
            //string message = string.Format("Message: {0}\\n\\n", ex.Message);
            //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
            //// ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            //Session["eswar"] = message;

            //Session["pagename"] = "HardwareAssets.aspx";
            //Response.Redirect("Errorpage.aspx");
        }
    }

    protected void Reset_Click(object sender, EventArgs e)
    {
        Resetfun();
    }

    protected void Resetfun()
    {
        try
        {
            AMS_Asset_Name.Text = string.Empty.ToString();
            AMS_PH_Make.Text = string.Empty.ToString();
            AMS_PH_Model.Text = string.Empty.ToString();
            AMS_PH_yearofManufacturer.Text = string.Empty.ToString();
            AMS_Characteristics.Text = string.Empty.ToString();

            AMS_Asset_Cost.Text = string.Empty.ToString();
            AMS_Asset_Locationn.Text = string.Empty.ToString();
            AMS_Date_of_Purchase.Text = string.Empty.ToString();
            AMS_PH_Warranty.Text = string.Empty.ToString();
            selectyears.Text = string.Empty.ToString();
            Warrenty_Status.Text = string.Empty.ToString();
            Remarks.Text = string.Empty.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void updatingfun()
    {
        Session["Sessiondefault"] = "no";
        try
        {
            SqlConnection updatefuncon = new SqlConnection(constr);
            SqlCommand updatefuncmd = new SqlCommand();
            updatefuncmd.CommandType = CommandType.StoredProcedure;
            updatefuncmd.CommandText = "master_hw_proc_select";
            updatefuncmd.Parameters.Add("@AMS_AssetID", SqlDbType.Int).Value = Session["SessionSWAssetID"];
            updatefuncmd.Connection = updatefuncon;
            updatefuncon.Open();
            SqlDataReader updatefundr = updatefuncmd.ExecuteReader();
            if (updatefundr.Read())
            {
                AMS_Asset_Name.ReadOnly = true;
                AMS_Asset_Name.Text = updatefundr["AMS_AssetName"].ToString();
                Session["hwcheck1"] = AMS_Asset_Name.Text;

                AMS_PH_Make.ReadOnly = true;
                AMS_PH_Make.Text = updatefundr["AMSHW_PH_make"].ToString();
                Session["hwcheck2"] = AMS_PH_Make.Text;

                AMS_PH_Model.ReadOnly = true;
                AMS_PH_Model.Text = updatefundr["AMSHW_PH_Model"].ToString();
                Session["hwcheck3"] = AMS_PH_Model.Text;

                AMS_PH_yearofManufacturer.Text = updatefundr["AMSHW_PH_Year_Of_Mfd"].ToString();
                Session["hwcheck4"] = AMS_PH_yearofManufacturer.Text;

                AMS_Characteristics.Text = updatefundr["AMSHW_PH_Characterstics"].ToString();
                Session["hwcheck5"] = AMS_Characteristics.Text;

                AMS_Asset_Cost.Text = Math.Round(float.Parse(updatefundr["AMS_Asset_Cost"].ToString()), 2, MidpointRounding.AwayFromZero).ToString("0.00");
                Session["hwcheck6"] = AMS_Asset_Cost.Text;

                AMS_Asset_Locationn.Enabled = false;
                AMS_Asset_Locationn.SelectedValue = updatefundr["AMS_Asset_Location"].ToString();
                Session["hwcheck7"] = AMS_Asset_Locationn.SelectedValue;
                AMS_Asset_Locationn.Attributes["Value"] = "this.value";

                AMS_Date_of_Purchase.Text = updatefundr["AMS_Date_of_Purchase"].ToString();
                Session["hwcheck8"] = AMS_Date_of_Purchase.Text;

                string warrenty = updatefundr["AMSHW_PH_Warranty"].ToString().ToLower();
                Session["hwcheck9"] = updatefundr["AMSHW_PH_Warranty"].ToString();

                int startingindex = 0, endindex = 0;
                if (warrenty.IndexOf("years") > 0)
                {
                    endindex = warrenty.IndexOf("years");
                    selectyears.SelectedIndex = 1;
                    selectyears.Attributes["Value"] = "this.value";
                }
                else if (warrenty.IndexOf("months") > 0)
                {
                    endindex = warrenty.IndexOf("months");
                    selectyears.SelectedIndex = 2;
                    selectyears.Attributes["Value"] = "this.value";
                }
                else if (warrenty.IndexOf("days") > 0)
                {
                    endindex = warrenty.IndexOf("days");
                    selectyears.SelectedIndex = 3;
                    selectyears.Attributes["Value"] = "this.value";
                }
                float years = float.Parse(warrenty.Substring(startingindex, endindex - startingindex));
                AMS_PH_Warranty.Text = years.ToString();

                string size = updatefundr["AMS_Asset_Size"].ToString().ToLower();

                Warrenty_Status.Text = updatefundr["HW_Warrenty_Status"].ToString();
                Session["hwcheck10"] = Warrenty_Status.Text;

                if (Warrenty_Status.SelectedIndex == 2)
                {
                    Warrenty_Status.Attributes["Value"] = "this.value";
                    Warrenty_Status.Enabled = false;
                }

                Remarks.Text = updatefundr["Remarks"].ToString();
                Session["hwcheck11"] = Remarks.Text;
            }
            updatefundr.Close();
            updatefuncon.Close();


            //catch (Exception ex)
            //{
            //    string message = string.Format("Message: {0}\\n\\n", ex.Message);
            //    message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
            //    message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
            //    message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            //}
        }

        catch (Exception ex)
        {
            //string message = string.Format("Message: {0}\\n\\n", ex.Message);
            //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
            ////ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            //Session["eswar"] = message;

            //Session["pagename"] = "HardwareAssets.aspx";
            //Response.Redirect("Errorpage.aspx");
        }
    }

    protected void Update_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection updatecon = new SqlConnection(constr);
            SqlCommand updatecmd = new SqlCommand();
            updatecmd.CommandType = CommandType.StoredProcedure;
            updatecmd.CommandText = "master_hw_proc_update";
            updatecmd.CommandType = CommandType.StoredProcedure;

            updatecmd.Parameters.Add("@AMS_AssetID", SqlDbType.Int).Value = Session["SessionSWAssetID"];
            updatecmd.Parameters.Add("@AMS_AssetName", SqlDbType.NVarChar).Value = AMS_Asset_Name.Text.Trim();
            updatecmd.Parameters.Add("@AMSHW_PH_make", SqlDbType.NVarChar).Value = AMS_PH_Make.Text.Trim();
            updatecmd.Parameters.Add("@AMSHW_PH_Model", SqlDbType.NVarChar).Value = AMS_PH_Model.Text.Trim();
            updatecmd.Parameters.Add("@AMSHW_PH_Year_Of_Mfd", SqlDbType.NVarChar).Value = AMS_PH_yearofManufacturer.Text.Trim();

            updatecmd.Parameters.Add("@AMSHW_PH_Characterstics", SqlDbType.NVarChar).Value = AMS_Characteristics.Text.Trim();
            updatecmd.Parameters.Add("@AMS_Asset_Cost", SqlDbType.Money).Value = AMS_Asset_Cost.Text.Trim();
            updatecmd.Parameters.Add("@AMS_Company_Location", SqlDbType.NVarChar).Value = AMS_Asset_Locationn.SelectedValue;
            updatecmd.Parameters.Add("@AMS_Asset_Location", SqlDbType.NVarChar).Value = AMS_Asset_Locationn.SelectedValue.Trim();
            updatecmd.Parameters.Add("@AMS_Date_of_Purchase", SqlDbType.NVarChar).Value = AMS_Date_of_Purchase.Text.Trim();
            updatecmd.Parameters.Add("@AMSHW_PH_Warranty", SqlDbType.NVarChar).Value = AMS_PH_Warranty.Text.Trim() + selectyears.SelectedValue;

            updatecmd.Parameters.Add("@HW_Warrenty_Status", SqlDbType.NVarChar).Value = Warrenty_Status.SelectedValue.Trim();

            if (Remarks.Text == "NA")
            {
                updatecmd.Parameters.Add("@AMS_Remarks", SqlDbType.NVarChar).Value = "NA";
                updatecmd.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = "NA";
            }
            else if (Remarks.Text != String.Empty.ToString())
            {
                updatecmd.Parameters.Add("@AMS_Remarks", SqlDbType.NVarChar).Value = Remarks.Text.Trim();
                updatecmd.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = Remarks.Text.Trim();
            }

            updatecmd.Parameters.Add("@AMS_Asset_Size", SqlDbType.NVarChar).Value = "NA";
            updatecmd.Parameters.Add("@AMSHW_PH_Allotted_Employee", SqlDbType.NVarChar).Value = "NA";
            updatecmd.Parameters.Add("@AMSHW_PH_Replacement_Date", SqlDbType.NVarChar).Value = "NA";
            updatecmd.Parameters.Add("@AMSHW_PH_Replacement_Reason", SqlDbType.NVarChar).Value = "NA";
            updatecmd.Parameters.Add("@AMSHW_PH_Depreciation_Cost", SqlDbType.NVarChar).Value = "00";
            updatecmd.Parameters.Add("@SN_NAME", SqlDbType.NVarChar).Value = "NA";

            updatecmd.Connection = updatecon;
            updatecon.Open();
            if ((AMS_Asset_Name.Text == Session["hwcheck1"].ToString()) &&
                (AMS_PH_Make.Text == Session["hwcheck2"].ToString()) &&
                (AMS_PH_Model.Text == Session["hwcheck3"].ToString()) &&
                (AMS_PH_yearofManufacturer.Text == Session["hwcheck4"].ToString()) &&
                (AMS_Characteristics.Text == Session["hwcheck5"].ToString()) &&
                (AMS_Asset_Cost.Text == Session["hwcheck6"].ToString()) &&
                (AMS_Asset_Locationn.SelectedValue == Session["hwcheck7"].ToString()) &&
                (AMS_Date_of_Purchase.Text == Session["hwcheck8"].ToString()) &&
                ((AMS_PH_Warranty.Text.Trim() + selectyears.SelectedValue) == Session["hwcheck9"].ToString()) &&
                (Warrenty_Status.Text == Session["hwcheck10"].ToString()) &&
                (Remarks.Text == Session["hwcheck11"].ToString()))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('Please do atleast one change to update.');</script>");
            }
            else
            {
                updatecmd.ExecuteNonQuery();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('Hardware Asset updated successfully.');window.location.href='../Admin/Hardware-Assets.aspx';</script>");
            }
            updatecon.Close();
        }
        catch (Exception ex)
        {
            //string message = string.Format("Message: {0}\\n\\n", ex.Message);
            //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
            //// ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            //Session["eswar"] = message;

            //Session["pagename"] = "HardwareAssets.aspx";
            //Response.Redirect("Errorpage.aspx");
            throw ex;
        }
    }
}