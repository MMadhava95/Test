using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddSoftwareAssets : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        //ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        if (Session["SessionUserMailID"] != null && Session["SessionUserPosition"].ToString() == "Admin")
        {
            //Resetfun();
            if (Session["SessionSWClick"].ToString() == "Asset Update" && Session["Sessiondefault"].ToString() == "yes")
            {
                updatingfun();
                updatebutton_div.Visible = true;
                Update.Visible = true;
                sw_Insertbutton_div.Visible = false;
                sw_Insert.Visible = false;
                Resetbutton_div.Visible = false;
                lbladdorupdate.Text = "Update";
            }
            else if (Session["SessionSWClick"].ToString() == "Asset Insert")
            {
                Resetbutton_div.Visible = true;
                Update.Visible = false;
                updatebutton_div.Visible = false;
                sw_Insert.Visible = true;
                sw_Insertbutton_div.Visible = true;
                lbladdorupdate.Text = "Add";
            }
            CalendarExtender1.StartDate = DateTime.Now.AddYears(-100);
            CalendarExtender1.EndDate = DateTime.Now;
            CalendarExtender2.StartDate = DateTime.Now.AddYears(-100);
            CalendarExtender2.EndDate = DateTime.Now;
            CalendarExtender3.StartDate = DateTime.Now.AddYears(-100);
            CalendarExtender3.EndDate = DateTime.Now;

            for (int i = 0; i <= 3; i++)
            {
                AMSCategory.Items[i].Attributes.Add("style", "color:black");
                AMS_SW_License_Type.Items[i].Attributes.Add("style", "color:black");
                TypeofCloud.Items[i].Attributes.Add("style", "color:black");
            }
            for (int i = 0; i <= 2; i++)
            {
                Cloudlicense.Items[i].Attributes.Add("style", "color:black");
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void AMS_Asset_Name_TextChanged(object sender, EventArgs e)
    {

        Session["SWVersion"] = AMS_Asset_Name.Text;
        if (Page.IsPostBack == true && AMS_Asset_Name.Text != String.Empty.ToString())
        {
            Session["AssetName"] = AMS_Asset_Name.Text;
            try
            {
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand assetnamecmd = new SqlCommand("Select SWAssetName from SoftwareDbTable where SWAssetName = '" + AMS_Asset_Name.Text + "'", con);
                SqlCommand assetvendorcmd = new SqlCommand("Select SWVendor from SoftwareDbTable where SWAssetName = '" + AMS_Asset_Name.Text + "'", con);
                SqlDataReader assetnamedr = (assetnamecmd.ExecuteReader());
                assetnamedr.Read();

                if (assetnamedr.HasRows)
                {
                    assetnamedr.Close();
                    AMS_SW_Vendor.Text = assetvendorcmd.ExecuteScalar().ToString();
                    AMS_SW_Vendor.ReadOnly = true;
                    AMS_SW_Asset_Version.Focus();
                    
                }
                else if (!assetnamedr.HasRows || AMS_Asset_Name.Text == String.Empty.ToString())
                {
                    assetnamedr.Close();
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Enter Valid Data');", true);
                    AMS_Asset_Name.Text = String.Empty.ToString();
                    AMS_SW_Vendor.Text = String.Empty.ToString();
                    AMS_Asset_Name.Focus();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public void Clear_Asset()
    {
        try
        {
            AMS_SW_Vendor.Text = AMS_SW_Asset_Version.Text = LicenseNumber.Text = LicenseServerDetails.Text = VendorContact.Text = VendorMail.Text =
                Asset_Remarks.Text = AMS_Asset_Cost.Text = AMS_Asset_Name.Text = String.Empty.ToString();
            AMS_Date_of_Purchase.Text = "";
            AMS_SW_NumofdaysforTrial.Text = "00";
            AMSCategory.SelectedIndex = AMS_SW_License_Type.SelectedIndex = 0;
            AMS_Asset_Name.Focus();

            //Response.Redirect("SoftwareAssets.aspx");
        }

        catch (Exception ex)
        {
            //string message = string.Format("Message: {0}\\n\\n", ex.Message);
            //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
            ////ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            //Session["eswar"] = message;

            //Session["pagename"] = "SoftwareAssets.aspx";
            //Response.Redirect("Errorpage.aspx");
        }
    }

    protected void sw_Insert_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection insertcon = new SqlConnection(constr);
            SqlCommand insertcmd = new SqlCommand();
            insertcmd.CommandType = CommandType.StoredProcedure;
            insertcmd.CommandText = "master_sw_proc";
            insertcmd.CommandType = CommandType.StoredProcedure;

            insertcmd.Parameters.Add("@AMS_AssetName", SqlDbType.NVarChar).Value = AMS_Asset_Name.Text.Trim();
            insertcmd.Parameters.Add("@AMSSW_Vendor", SqlDbType.NVarChar).Value = AMS_SW_Vendor.Text.Trim();
            insertcmd.Parameters.Add("@AMSSW_Version", SqlDbType.NVarChar).Value = AMS_SW_Asset_Version.Text.Trim();

            //SW Category Start
            insertcmd.Parameters.Add("@AMSSW_Category", SqlDbType.NVarChar).Value = AMSCategory.SelectedValue.Trim();
            if (AMSCategory.SelectedIndex == 0)
            {
                CategoryRequiredFieldValidator.IsValid = false;
            }
            else if (AMSCategory.SelectedIndex == 1 || AMSCategory.SelectedIndex == 3)
            {
                //cloud type
                insertcmd.Parameters.Add("@AMSSW_CloudType", SqlDbType.NVarChar).Value = "NA";
                //cloud License type
                insertcmd.Parameters.Add("@AMSSW_CloudLicensetype", SqlDbType.NVarChar).Value = "NA";
                //Cloud Trial Days
                insertcmd.Parameters.Add("@AMSSW_Trialdays", SqlDbType.NVarChar).Value = "00";
                //cloud purchase details
                insertcmd.Parameters.Add("@AMSSW_Cloud_Cost", SqlDbType.NVarChar).Value = "00";
                //Cloud strat time
                insertcmd.Parameters.Add("@AMSSW_Cloud_StartTime", SqlDbType.NVarChar).Value = "NA";
                //Cloud billing details
                insertcmd.Parameters.Add("@AMSSW_Cloud_BillingTime", SqlDbType.NVarChar).Value = "NA";
            }
            else if (AMSCategory.SelectedIndex == 2)
            {
                //Cloud Type
                if (TypeofCloud.SelectedIndex == 0)
                {
                    cloudtypeRequiredFieldValidator.IsValid = false;
                }
                else if (TypeofCloud.SelectedIndex == 1 || TypeofCloud.SelectedIndex == 2 || TypeofCloud.SelectedIndex == 3)
                {
                    insertcmd.Parameters.Add("@AMSSW_CloudType", SqlDbType.NVarChar).Value = TypeofCloud.SelectedValue.Trim();
                }

                //Cloud License Type
                insertcmd.Parameters.Add("@AMSSW_CloudLicensetype", SqlDbType.NVarChar).Value = Cloudlicense.SelectedValue.Trim();

                if (Cloudlicense.SelectedIndex == 0)
                {
                    CloudlicenseRequiredFieldValidator.IsValid = false;
                }
                else if (Cloudlicense.SelectedIndex == 1)
                {
                    //cloud Trail details
                    insertcmd.Parameters.Add("@AMSSW_Trialdays", SqlDbType.NVarChar).Value = TrailDays.Text.Trim();
                    //cloud Purchased details
                    insertcmd.Parameters.Add("@AMSSW_Cloud_Cost", SqlDbType.NVarChar).Value = "00";
                }
                else if (Cloudlicense.SelectedIndex == 2)
                {
                    //cloud Trail details
                    insertcmd.Parameters.Add("@AMSSW_Trialdays", SqlDbType.NVarChar).Value = "00";
                    //cloud Purchased details
                    insertcmd.Parameters.Add("@AMSSW_Cloud_Cost", SqlDbType.NVarChar).Value = Purchasedcosts.Text.Trim();
                }
                insertcmd.Parameters.Add("@AMSSW_Cloud_StartTime", SqlDbType.NVarChar).Value = cloudstarttime.Text.Trim();
                insertcmd.Parameters.Add("@AMSSW_Cloud_BillingTime", SqlDbType.NVarChar).Value = Billingtime.Text.Trim();
            }
            //SW Category End

            //SW License Type start
            insertcmd.Parameters.Add("@AMSSW_LicensedType", SqlDbType.NVarChar).Value = AMS_SW_License_Type.SelectedValue.Trim();
            if (AMS_SW_License_Type.SelectedIndex == 0)
            {
                LicenseTypeRequiredFieldValidator.IsValid = false;
            }
            else if (AMS_SW_License_Type.SelectedIndex == 1)
            {
                //SW License Trial Days
                insertcmd.Parameters.Add("@AMSSW_Num_Of_Days", SqlDbType.NVarChar).Value = "00";
                //Licensed Fields
                AMS_Asset_Cost.Text = "0.0000";
                insertcmd.Parameters.Add("@AMS_Asset_Cost", SqlDbType.Money).Value = Math.Round(float.Parse(AMS_Asset_Cost.Text.Trim()), 2, MidpointRounding.AwayFromZero);
                insertcmd.Parameters.Add("@AMSSW_License_Number", SqlDbType.NVarChar).Value = "NA";
                insertcmd.Parameters.Add("@AMSSW_LicenseServerDetails", SqlDbType.NVarChar).Value = "NA";
            }
            else if (AMS_SW_License_Type.SelectedIndex == 2)
            {
                //SW License Trial Days
                insertcmd.Parameters.Add("@AMSSW_Num_Of_Days", SqlDbType.NVarChar).Value = AMS_SW_NumofdaysforTrial.Text.Trim();
                //Licensed Fields
                AMS_Asset_Cost.Text = "0.0000";
                insertcmd.Parameters.Add("@AMS_Asset_Cost", SqlDbType.Money).Value = Math.Round(float.Parse(AMS_Asset_Cost.Text.Trim()), 2, MidpointRounding.AwayFromZero);
                insertcmd.Parameters.Add("@AMSSW_License_Number", SqlDbType.NVarChar).Value = "NA";
                insertcmd.Parameters.Add("@AMSSW_LicenseServerDetails", SqlDbType.NVarChar).Value = "NA";
            }
            else if (AMS_SW_License_Type.SelectedIndex == 3)
            {
                //SW License Trial Days
                insertcmd.Parameters.Add("@AMSSW_Num_Of_Days", SqlDbType.NVarChar).Value = "00";
                //Licensed Fields
                insertcmd.Parameters.Add("@AMS_Asset_Cost", SqlDbType.Money).Value = Math.Round(float.Parse(AMS_Asset_Cost.Text.Trim()), 2, MidpointRounding.AwayFromZero);
                insertcmd.Parameters.Add("@AMSSW_License_Number", SqlDbType.NVarChar).Value = LicenseNumber.Text.Trim();
                insertcmd.Parameters.Add("@AMSSW_LicenseServerDetails", SqlDbType.NVarChar).Value = LicenseServerDetails.Text.Trim();
            }
            //SW License Type end

            insertcmd.Parameters.Add("@AMS_Date_of_Purchase", SqlDbType.NVarChar).Value = AMS_Date_of_Purchase.Text.Trim();

            //optional fields start
            //Vendor Mail
            if (VendorMail.Text == String.Empty.ToString())
            {
                insertcmd.Parameters.Add("@AMSSW_VendorMail", SqlDbType.NVarChar).Value = "NA";
            }
            else if (VendorMail.Text != String.Empty.ToString())
            {
                insertcmd.Parameters.Add("@AMSSW_VendorMail", SqlDbType.NVarChar).Value = VendorMail.Text.Trim();
            }

            //Vendor Contact
            if (VendorContact.Text == String.Empty.ToString())
            {
                insertcmd.Parameters.Add("@AMSSW_VendorContact", SqlDbType.NVarChar).Value = "NA";
            }
            else if (VendorContact.Text != String.Empty.ToString())
            {
                insertcmd.Parameters.Add("@AMSSW_VendorContact", SqlDbType.NVarChar).Value = VendorContact.Text.Trim();
            }

            //Remarks
            if (Asset_Remarks.Text == String.Empty.ToString())
            {
                insertcmd.Parameters.Add("@AMS_Remarks", SqlDbType.NVarChar).Value = "NA";
            }
            else if (Asset_Remarks.Text != String.Empty.ToString())
            {
                insertcmd.Parameters.Add("@AMS_Remarks", SqlDbType.NVarChar).Value = Asset_Remarks.Text.Trim();
            }
            //optional fields end

            insertcmd.Parameters.Add("@AMS_AssetType", SqlDbType.NVarChar).Value = "Software Assets";
            insertcmd.Parameters.Add("@AMSSW_AssetType", SqlDbType.NVarChar).Value = "Software Assets";
            insertcmd.Parameters.Add("@AMS_Company_Location", SqlDbType.NVarChar).Value = "Vijayawada";
            insertcmd.Parameters.Add("@AMS_Num_of_Assets", SqlDbType.Int).Value = 1;
            insertcmd.Parameters.Add("@AMS_Asset_Size", SqlDbType.NVarChar).Value = "NA";
            insertcmd.Parameters.Add("@AMS_Asset_Location", SqlDbType.NVarChar).Value = "NA";
            insertcmd.Parameters.Add("@AMSSW_Software_Requirements", SqlDbType.NVarChar).Value = "NA";
            insertcmd.Parameters.Add("@AMSSW_OperatingSytem", SqlDbType.NVarChar).Value = "NA";

            //LicenseExpire
            Int32 x = Convert.ToInt32(this.AMS_SW_NumofdaysforTrial.Text);
            DateTime EntryDate = Convert.ToDateTime(AMS_Date_of_Purchase.Text);
            string s = EntryDate.AddDays(x).ToShortDateString();

            insertcmd.Parameters.Add("@AMSSW_LicenseExpireDate", SqlDbType.DateTime).Value = s;
            insertcmd.Parameters.Add("@AMSSW_EmailID", SqlDbType.NVarChar).Value = Session["SessionUserMailID"];

            //SqlConnection con1 = new SqlConnection(constr);
            //SqlCommand cmd1 = new SqlCommand();
            //DataTable dt1 = new DataTable();
            //SqlDataAdapter sd1 = new SqlDataAdapter("select s.AMSSW_Version, a.AMS_AssetName from Asset_Master a join Software_Assets s on s.AMSSW_AssetID = a.AMS_AssetID where ((a.AMS_AssetName='" + AMS_Asset_Name.Text + "' or s.AMSSW_Vendor='" + AMS_SW_Vendor.Text + "')or not(a.AMS_AssetName='" + AMS_Asset_Name.Text + "' or s.AMSSW_Vendor='" + AMS_SW_Vendor.Text + "')) and s.AMSSW_Version='" + AMS_SW_Asset_Version.Text + "'", con1);
            //sd1.Fill(dt1);
            //if (dt1.Rows.Count >= 1)
            //{
            //    //Alertmessage.Visible = true;
            //    //alertmsg.Text = "Asset already exists";
            //    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Asset already exists');", true);
            //    //AMS_PH_SerialNumber.Text = string.Empty;
            //    //AMS_PH_SerialNumber.Focus();
            //}
            //else
            //{
            insertcmd.Connection = insertcon;
            insertcon.Open();
            insertcmd.ExecuteNonQuery();
            Clear_Asset();
            //Alertmessage.Visible = true;
            //alertmsg.Text = "Software Asset inserted successfully";
            //Alertmessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('Software Asset Inserted Successfully');window.location.href='../Admin/Software-Assets.aspx';</script>");
            //When Inserting the data textboxes will be cleared by calling the reset function...
            Resetfun();
            insertcon.Close();
            insertcon.Dispose();
            //}
        }
        catch (Exception ex)
        {
            //string message = string.Format("Message: {0}\\n\\n", ex.Message);
            //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
            //// ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            //Session["eswar"] = message;

            //Session["pagename"] = "SoftwareAssets.aspx";
            //Response.Redirect("Errorpage.aspx");
            throw ex;
        }
    }

    protected void License_Type_function()
    {
        try
        {
            //lbldescription1.Text = "Inserting Software Asset(s)";
            //panelgrid.Visible = false;

            if (AMS_SW_License_Type.SelectedIndex == 1)
            {
                //SWTrial.Visible = false;
                //AMS_Asset_Cost.Visible = false;
                //LicenseNumber.Visible = false;
                //LicenseServerDetails.Visible = false;
                AMS_SW_NumofdaysforTrial.Text = "00";
                AMS_Asset_Cost.Text = "0.00";
                LicensedPanel.Visible = false;
                AMSCategory.Focus();
            }
            else if (AMS_SW_License_Type.SelectedIndex == 2)
            {
                //SWTrial.Visible = true;
                //AMS_Asset_Cost.Visible = false;
                //LicenseNumber.Visible = false;
                //LicenseServerDetails.Visible = false;
                LicensedPanel.Visible = false;
                AMS_Asset_Cost.Text = "0.00";
                AMS_SW_NumofdaysforTrial.Focus();
            }
            else if (AMS_SW_License_Type.SelectedIndex == 3)
            {
                //AMS_Asset_Cost.Visible = false;
                //LicenseNumber.Visible = false;
                //LicenseServerDetails.Visible = false;
                //SWTrial.Visible = false;

                LicensedPanel.Visible = true;

                AMS_SW_NumofdaysforTrial.Text = "00";
                AMS_Asset_Cost.Focus();
            }
        }

        catch (Exception ex)
        {
            //string message = string.Format("Message: {0}\\n\\n", ex.Message);
            //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
            //// ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            //Session["eswar"] = message;

            //Session["pagename"] = "SoftwareAssets.aspx";
            //Response.Redirect("Errorpage.aspx");
            throw ex;
        }
    }

    protected void AMSCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        AMSCategory.Attributes["value"] = "this.value";
        if (Page.IsPostBack == true)
        {
            //Categoryfun();
            if (AMSCategory.SelectedIndex == 0)
            {
                CloudPanel.Visible = false;
                CategoryRequiredFieldValidator.IsValid = false;
            }
            else if (AMSCategory.SelectedIndex == 1)
            {
                CloudPanel.Visible = false;
                AMSCategory.Attributes["value"] = "this.value";
            }
            else if (AMSCategory.SelectedIndex == 2)
            {
                CloudPanel.Visible = true;
                AMSCategory.Attributes["value"] = "this.value";
            }
            else if (AMSCategory.SelectedIndex == 3)
            {
                CloudPanel.Visible = false;
                AMSCategory.Attributes["value"] = "this.value";
            }
        }
    }

    protected void Categoryfun()
    {
        if (AMSCategory.SelectedIndex == 1)
        {
            CloudPanel.Visible = false;
            AMSCategory.Attributes["value"] = "this.value";
        }
        else if (AMSCategory.SelectedIndex == 2)
        {
            CloudPanel.Visible = true;
            AMSCategory.Attributes["value"] = "this.value";
        }
        else if (AMSCategory.SelectedIndex == 3)
        {
            CloudPanel.Visible = false;
            AMSCategory.Attributes["value"] = "this.value";
        }
    }

    protected void Cloudlicense_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cloudlicense.Attributes["value"] = "this.value";
        if (Page.IsPostBack == true)
        {
            if (Cloudlicense.SelectedIndex == 0)
            {
                CloudlicenseRequiredFieldValidator.IsValid = false;
                cloudtrialdayspanel.Visible = false;
                cloudcostpanel.Visible = false;
            }
            else if (Cloudlicense.SelectedIndex == 1)
            {
                cloudtrialdayspanel.Visible = true;
                cloudcostpanel.Visible = false;
            }
            else if (Cloudlicense.SelectedIndex == 2)
            {
                cloudtrialdayspanel.Visible = false;
                cloudcostpanel.Visible = true;
            }
        }
    }

    protected void CloudLicensefun()
    {
        if (Cloudlicense.SelectedIndex == 0)
        {
            CloudlicenseRequiredFieldValidator.IsValid = false;
            cloudtrialdayspanel.Visible = false;
            cloudcostpanel.Visible = false;
        }
        else if (Cloudlicense.SelectedIndex == 1)
        {
            cloudtrialdayspanel.Visible = true;
            cloudcostpanel.Visible = false;
        }
        else if (Cloudlicense.SelectedIndex == 2)
        {
            cloudtrialdayspanel.Visible = false;
            cloudcostpanel.Visible = true;
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
                    License_Type_function();

                    DateTime currentdate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                    DateTime value = DateTime.Parse(AMS_Date_of_Purchase.Text, new CultureInfo("gu-IN", false));
                    if (value > currentdate)
                    {
                        //Alertmessage.Visible = true;
                        //alertmsg.Text = "Date of purchase should not be Later than Today";
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Date of purchase should not be Later than Today');", true);
                        AMS_Date_of_Purchase.Text = "";
                        //Label15.Visible = true;
                    }
                    else
                    {
                        //Label5.Visible = false;

                        VendorContact.Visible = true;

                        VendorMail.Focus();
                    }
                }
                else
                {

                    AMS_Date_of_Purchase.Text = "";
                    //Label5.Visible = true;
                }
            }
            //Label15.Visible = false;
            VendorContact.Visible = true;

        }

        catch (Exception ex)
        {
            //string message = string.Format("Message: {0}\\n\\n", ex.Message);
            //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
            ////ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            //Session["eswar"] = message;

            //Session["pagename"] = "SoftwareAssets.aspx";
            //Response.Redirect("Errorpage.aspx");
            throw ex;
        }
    }

    protected void AMS_SW_License_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        AMS_SW_License_Type.Attributes["value"] = "this.value";
        if (Page.IsPostBack == true)
        {
            if (AMS_SW_License_Type.SelectedIndex == 0)
            {
                TrailVersionPanel.Visible = false;
                LicensedPanel.Visible = false;
                LicenseTypeRequiredFieldValidator.IsValid = false;
            }
            else if (AMS_SW_License_Type.SelectedIndex == 1)
            {
                TrailVersionPanel.Visible = false;
                LicensedPanel.Visible = false;
            }
            else if (AMS_SW_License_Type.SelectedIndex == 2)
            {
                TrailVersionPanel.Visible = true;
                LicensedPanel.Visible = false;
            }
            else if (AMS_SW_License_Type.SelectedIndex == 3)
            {
                TrailVersionPanel.Visible = false;
                LicensedPanel.Visible = true;
            }
        }
    }

    protected void LicenseTypefun()
    {
        if (AMS_SW_License_Type.SelectedIndex == 0)
        {
            TrailVersionPanel.Visible = false;
            LicensedPanel.Visible = false;
            LicenseTypeRequiredFieldValidator.IsValid = false;
        }
        else if (AMS_SW_License_Type.SelectedIndex == 1)
        {
            TrailVersionPanel.Visible = false;
            LicensedPanel.Visible = false;
        }
        else if (AMS_SW_License_Type.SelectedIndex == 2)
        {
            TrailVersionPanel.Visible = true;
            LicensedPanel.Visible = false;
        }
        else if (AMS_SW_License_Type.SelectedIndex == 3)
        {
            TrailVersionPanel.Visible = false;
            LicensedPanel.Visible = true;
            AMS_SW_License_Type.Enabled = false;
            AMS_Asset_Cost.ReadOnly = true;
            LicenseNumber.ReadOnly = true;
            LicenseServerDetails.ReadOnly = true;
        }
    }

    protected void TypeofCloud_SelectedIndexChanged(object sender, EventArgs e)
    {
        TypeofCloud.Attributes["value"] = "this.value";
    }

    protected void Reset_Click1(object sender, EventArgs e)
    {
        Resetfun();
    }

    protected void Resetfun()
    {
        try
        {
            AMS_Asset_Name.Text = string.Empty.ToString();
            AMS_SW_Vendor.Text = string.Empty.ToString();
            AMS_SW_Asset_Version.Text = string.Empty.ToString();
            AMSCategory.SelectedIndex = 0;
            Categoryfun();
            TypeofCloud.SelectedIndex = 0;

            Cloudlicense.SelectedIndex = 0;
            TrailDays.Text = string.Empty.ToString();
            Purchasedcosts.Text = string.Empty.ToString();
            cloudstarttime.Text = string.Empty.ToString();
            Billingtime.Text = string.Empty.ToString();

            AMS_SW_License_Type.SelectedIndex = 0;
            AMS_SW_NumofdaysforTrial.Text = string.Empty.ToString();
            LicenseNumber.Text = string.Empty.ToString();
            LicenseServerDetails.Text = string.Empty.ToString();
            AMS_Date_of_Purchase.Text = string.Empty.ToString();

            VendorMail.Text = string.Empty.ToString();
            VendorContact.Text = string.Empty.ToString();
            Asset_Remarks.Text = string.Empty.ToString();
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
            SqlConnection upfuncon = new SqlConnection(constr);
            SqlCommand upfuncmd = new SqlCommand();
            upfuncmd.CommandType = CommandType.StoredProcedure;
            upfuncmd.CommandText = "master_sw_proc_select";
            upfuncmd.Parameters.Add("@AMS_AssetID", SqlDbType.Int).Value = Session["SessionSWAssetID"];
            upfuncmd.Connection = upfuncon;
            upfuncon.Open();
            SqlDataReader upfundr = upfuncmd.ExecuteReader();
            if (upfundr.Read())
            {
                AMS_Asset_Name.ReadOnly = true;
                AMS_Asset_Name.Text = upfundr["AMS_AssetName"].ToString();
                Session["swcheck1"] = AMS_Asset_Name.Text;

                AMS_SW_Vendor.ReadOnly = true;
                AMS_SW_Vendor.Text = upfundr["AMSSW_Vendor"].ToString();
                Session["swcheck2"] = AMS_SW_Vendor.Text;

                AMS_SW_Asset_Version.ReadOnly = true;
                AMS_SW_Asset_Version.Text = upfundr["AMSSW_Version"].ToString();
                Session["swcheck3"] = AMS_SW_Asset_Version.Text;

                AMSCategory.SelectedValue = upfundr["AMSSW_Category"].ToString();
                Session["swcheck4"] = AMSCategory.SelectedValue;
                AMSCategory.Attributes["value"] = "this.value";
                Categoryfun();

                TypeofCloud.SelectedValue = upfundr["AMSSW_CloudType"].ToString();
                Session["swcheck5"] = TypeofCloud.SelectedValue;
                TypeofCloud.Attributes["value"] = "this.value";

                Cloudlicense.SelectedValue = upfundr["AMSSW_CloudLicensetype"].ToString();
                Session["swcheck6"] = Cloudlicense.SelectedValue;
                Cloudlicense.Attributes["value"] = "this.value";
                CloudLicensefun();

                TrailDays.Text = upfundr["AMSSW_Trialdays"].ToString();
                Session["swcheck7"] = TrailDays.Text;

                Purchasedcosts.Text = upfundr["AMSSW_Cloud_Cost"].ToString();
                Session["swcheck8"] = Purchasedcosts.Text;

                cloudstarttime.Text = upfundr["AMSSW_Cloud_StartTime"].ToString();
                Session["swcheck9"] = cloudstarttime.Text;

                Billingtime.Text = upfundr["AMSSW_Cloud_BillingTime"].ToString();
                Session["swcheck10"] = Billingtime.Text;

                AMS_SW_License_Type.SelectedValue = upfundr["AMSSW_LicensedType"].ToString();
                Session["swcheck11"] = AMS_SW_License_Type.SelectedValue;
                LicenseTypefun();
                AMS_SW_License_Type.Attributes["value"] = "this.value";

                AMS_SW_NumofdaysforTrial.Text = upfundr["AMSSW_Num_Of_Days"].ToString();
                Session["swcheck12"] = AMS_SW_NumofdaysforTrial.Text;

                AMS_Asset_Cost.Text = Math.Round(float.Parse(upfundr["AMS_Asset_Cost"].ToString()), 2, MidpointRounding.AwayFromZero).ToString("0.00");
                Session["swcheck13"] = AMS_Asset_Cost.Text;

                LicenseNumber.Text = upfundr["AMSSW_License_Number"].ToString();
                Session["swcheck14"] = LicenseNumber.Text;

                LicenseServerDetails.Text = upfundr["AMSSW_LicenseServerDetails"].ToString();
                Session["swcheck15"] = LicenseServerDetails.Text;

                AMS_Date_of_Purchase.Text = upfundr["AMS_Date_of_Purchase"].ToString();
                Session["swcheck16"] = AMS_Date_of_Purchase.Text;

                VendorMail.Text = upfundr["AMSSW_VendorMail"].ToString();
                Session["swcheck17"] = VendorMail.Text;

                VendorContact.Text = upfundr["AMSSW_VendorContact"].ToString();
                Session["swcheck18"] = VendorContact.Text;

                Asset_Remarks.Text = upfundr["AMS_Remarks"].ToString();
                Session["swcheck19"] = Asset_Remarks.Text;
            }
            upfundr.Close();
            upfuncon.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Update_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection updatecon = new SqlConnection(constr);
            SqlCommand updatecmd = new SqlCommand();
            updatecmd.CommandType = CommandType.StoredProcedure;
            updatecmd.CommandText = "master_sw_proc_update";

            updatecmd.Parameters.Add("@AMS_AssetID", SqlDbType.Int).Value = Session["SessionSWAssetID"];
            updatecmd.Parameters.Add("@AMS_AssetName", SqlDbType.NVarChar).Value = AMS_Asset_Name.Text.Trim();
            updatecmd.Parameters.Add("@AMSSW_Version", SqlDbType.NVarChar).Value = AMS_SW_Asset_Version.Text.Trim();
            updatecmd.Parameters.Add("@AMSSW_Vendor", SqlDbType.NVarChar).Value = AMS_SW_Vendor.Text.Trim();

            //SW Category Start
            updatecmd.Parameters.Add("@AMSSW_Category", SqlDbType.NVarChar).Value = AMSCategory.SelectedValue.Trim();
            if (AMSCategory.SelectedIndex == 0)
            {
                CategoryRequiredFieldValidator.IsValid = false;
            }
            else if (AMSCategory.SelectedIndex == 1 || AMSCategory.SelectedIndex == 3)
            {
                //cloud type
                updatecmd.Parameters.Add("@AMSSW_CloudType", SqlDbType.NVarChar).Value = "NA";
                //cloud License type
                updatecmd.Parameters.Add("@AMSSW_CloudLicensetype", SqlDbType.NVarChar).Value = "NA";
                //Cloud Trial Days
                updatecmd.Parameters.Add("@AMSSW_Trialdays", SqlDbType.NVarChar).Value = "00";
                //cloud purchase details
                updatecmd.Parameters.Add("@AMSSW_Cloud_Cost", SqlDbType.NVarChar).Value = "00";
                //Cloud strat time
                updatecmd.Parameters.Add("@AMSSW_Cloud_StartTime", SqlDbType.NVarChar).Value = "NA";
                //Cloud billing details
                updatecmd.Parameters.Add("@AMSSW_Cloud_BillingTime", SqlDbType.NVarChar).Value = "NA";
            }
            else if (AMSCategory.SelectedIndex == 2)
            {
                //Cloud Type
                if (TypeofCloud.SelectedIndex == 0)
                {
                    cloudtypeRequiredFieldValidator.IsValid = false;
                }
                else if (TypeofCloud.SelectedIndex == 1 || TypeofCloud.SelectedIndex == 2 || TypeofCloud.SelectedIndex == 3)
                {
                    updatecmd.Parameters.Add("@AMSSW_CloudType", SqlDbType.NVarChar).Value = TypeofCloud.SelectedValue.Trim();
                }

                //Cloud License Type
                updatecmd.Parameters.Add("@AMSSW_CloudLicensetype", SqlDbType.NVarChar).Value = Cloudlicense.SelectedValue.Trim();

                if (Cloudlicense.SelectedIndex == 0)
                {
                    CloudlicenseRequiredFieldValidator.IsValid = false;
                }
                else if (Cloudlicense.SelectedIndex == 1)
                {
                    //cloud Trail details
                    updatecmd.Parameters.Add("@AMSSW_Trialdays", SqlDbType.NVarChar).Value = TrailDays.Text.Trim();
                    //cloud Purchased details
                    updatecmd.Parameters.Add("@AMSSW_Cloud_Cost", SqlDbType.NVarChar).Value = "00";
                }
                else if (Cloudlicense.SelectedIndex == 2)
                {
                    //cloud Trail details
                    updatecmd.Parameters.Add("@AMSSW_Trialdays", SqlDbType.NVarChar).Value = "00";
                    //cloud Purchased details
                    updatecmd.Parameters.Add("@AMSSW_Cloud_Cost", SqlDbType.NVarChar).Value = Purchasedcosts.Text.Trim();
                }
                updatecmd.Parameters.Add("@AMSSW_Cloud_StartTime", SqlDbType.NVarChar).Value = cloudstarttime.Text.Trim();
                updatecmd.Parameters.Add("@AMSSW_Cloud_BillingTime", SqlDbType.NVarChar).Value = Billingtime.Text.Trim();
            }
            //SW Category End

            //SW License Type start
            updatecmd.Parameters.Add("@AMSSW_LicensedType", SqlDbType.NVarChar).Value = AMS_SW_License_Type.SelectedValue.Trim();
            if (AMS_SW_License_Type.SelectedIndex == 0)
            {
                LicenseTypeRequiredFieldValidator.IsValid = false;
            }
            else if (AMS_SW_License_Type.SelectedIndex == 1)
            {
                //SW License Trial Days
                updatecmd.Parameters.Add("@AMSSW_Num_Of_Days", SqlDbType.NVarChar).Value = "00";
                //Licensed Fields
                AMS_Asset_Cost.Text = "0.0000";
                updatecmd.Parameters.Add("@AMS_Asset_Cost", SqlDbType.Money).Value = Math.Round(float.Parse(AMS_Asset_Cost.Text.Trim()), 2, MidpointRounding.AwayFromZero);
                updatecmd.Parameters.Add("@AMSSW_License_Number", SqlDbType.NVarChar).Value = "NA";
                updatecmd.Parameters.Add("@AMSSW_LicenseServerDetails", SqlDbType.NVarChar).Value = "NA";
            }
            else if (AMS_SW_License_Type.SelectedIndex == 2)
            {
                //SW License Trial Days
                updatecmd.Parameters.Add("@AMSSW_Num_Of_Days", SqlDbType.NVarChar).Value = AMS_SW_NumofdaysforTrial.Text.Trim();
                //Licensed Fields
                AMS_Asset_Cost.Text = "0.0000";
                updatecmd.Parameters.Add("@AMS_Asset_Cost", SqlDbType.Money).Value = Math.Round(float.Parse(AMS_Asset_Cost.Text.Trim()), 2, MidpointRounding.AwayFromZero);
                updatecmd.Parameters.Add("@AMSSW_License_Number", SqlDbType.NVarChar).Value = "NA";
                updatecmd.Parameters.Add("@AMSSW_LicenseServerDetails", SqlDbType.NVarChar).Value = "NA";
            }
            else if (AMS_SW_License_Type.SelectedIndex == 3)
            {
                //SW License Trial Days
                updatecmd.Parameters.Add("@AMSSW_Num_Of_Days", SqlDbType.NVarChar).Value = "00";
                //Licensed Fields
                updatecmd.Parameters.Add("@AMS_Asset_Cost", SqlDbType.Money).Value = Math.Round(float.Parse(AMS_Asset_Cost.Text.Trim()), 2, MidpointRounding.AwayFromZero);
                updatecmd.Parameters.Add("@AMSSW_License_Number", SqlDbType.NVarChar).Value = LicenseNumber.Text.Trim();
                updatecmd.Parameters.Add("@AMSSW_LicenseServerDetails", SqlDbType.NVarChar).Value = LicenseServerDetails.Text.Trim();
            }
            //SW License Type end

            updatecmd.Parameters.Add("@AMS_Date_of_Purchase", SqlDbType.NVarChar).Value = AMS_Date_of_Purchase.Text.Trim();

            //optional fields start
            //Vendor Mail
            if (VendorMail.Text != String.Empty.ToString() || VendorMail.Text != "NA")
            {
                updatecmd.Parameters.Add("@AMSSW_VendorMail", SqlDbType.NVarChar).Value = VendorMail.Text.Trim();
            }
            else
            {
                updatecmd.Parameters.Add("@AMSSW_VendorMail", SqlDbType.NVarChar).Value = "NA";
            }

            //Vendor Contact
            if (VendorContact.Text != String.Empty.ToString() || VendorContact.Text != "NA")
            {
                updatecmd.Parameters.Add("@AMSSW_VendorContact", SqlDbType.NVarChar).Value = VendorContact.Text.Trim();
            }
            else
            {
                updatecmd.Parameters.Add("@AMSSW_VendorContact", SqlDbType.NVarChar).Value = "NA";
            }

            //Remarks
            if (Asset_Remarks.Text != String.Empty.ToString() || Asset_Remarks.Text != "NA")
            {
                updatecmd.Parameters.Add("@AMS_Remarks", SqlDbType.NVarChar).Value = Asset_Remarks.Text.Trim();
            }
            else
            {
                updatecmd.Parameters.Add("@AMS_Remarks", SqlDbType.NVarChar).Value = "NA";
            }
            //optional fields end

            updatecmd.Parameters.Add("@AMS_Asset_Size", SqlDbType.NVarChar).Value = "NA";
            updatecmd.Parameters.Add("@AMS_Asset_Location", SqlDbType.NVarChar).Value = "NA";
            updatecmd.Parameters.Add("@AMSSW_Software_Requirements", SqlDbType.NVarChar).Value = "NA";
            updatecmd.Parameters.Add("@AMSSW_OperatingSytem", SqlDbType.NVarChar).Value = "NA";
            updatecmd.Parameters.Add("@AMS_Num_of_Assets", SqlDbType.Int).Value = 1;

            updatecmd.Connection = updatecon;
            updatecon.Open();
            if ((AMS_Asset_Name.Text == Session["swcheck1"].ToString()) &&
                (AMS_SW_Vendor.Text == Session["swcheck2"].ToString()) &&
                (AMS_SW_Asset_Version.Text == Session["swcheck3"].ToString()) &&
                (AMSCategory.SelectedValue == Session["swcheck4"].ToString()) &&
                (TypeofCloud.SelectedValue == Session["swcheck5"].ToString()) &&

                (Cloudlicense.Text == Session["swcheck6"].ToString() &&
                (TrailDays.Text == Session["swcheck7"].ToString()) &&
                (Purchasedcosts.Text == Session["swcheck8"].ToString()) &&
                (cloudstarttime.Text == Session["swcheck9"].ToString()) &&
                (Billingtime.Text == Session["swcheck10"].ToString()) &&

                (AMS_SW_License_Type.SelectedValue == Session["swcheck11"].ToString()) &&
                (AMS_SW_NumofdaysforTrial.Text == Session["swcheck12"].ToString()) &&
                (AMS_Asset_Cost.Text == Session["swcheck13"].ToString()) &&
                (LicenseNumber.Text == Session["swcheck14"].ToString()) &&
                (LicenseServerDetails.Text == Session["swcheck15"].ToString()) &&

                (AMS_Date_of_Purchase.Text == Session["swcheck16"].ToString()) &&
                (VendorMail.Text == Session["swcheck17"].ToString()) &&
                (VendorContact.Text == Session["swcheck18"].ToString()) &&
                (Asset_Remarks.Text == Session["swcheck19"].ToString())))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('please do atleast one change to Update Assets');</script>");
            }
            else
            {
                updatecmd.ExecuteNonQuery();
                //Alertmessage.Visible = true;
                //alertmsg.Text = "Software Asset updated successfully";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('Software Asset Updated successfully');window.location.href='../Admin/Software-Assets.aspx';</script>");
            }
            updatecon.Close();
            updatecon.Dispose();
        }
        catch (Exception ex)
        {
            //string message = string.Format("Message: {0}\\n\\n", ex.Message);
            //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
            //// ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            //Session["eswar"] = message;

            //Session["pagename"] = "SoftwareAssets.aspx";
            //Response.Redirect("Errorpage.aspx");
            throw ex;
        }
    }
}