using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Operations_Default : System.Web.UI.Page
{

    public Byte[] bytes;
    protected void Page_Load(object sender, EventArgs e)
    {
        subject.Focus();
       if( Session["SessionEmployeeID"]==null)
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void submit_Click(object sender, EventArgs e)
    {
        // Read the file and convert it to Byte Array
        string filePath = FileUpload1.PostedFile.FileName;
        string filename = Path.GetFileName(filePath);
        string ext = Path.GetExtension(filename);
        string contenttype = String.Empty;

        if (ext == ".exe")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(Please Upload Valid file..);", true);
        }

        //Set the contenttype based on File Extension
        switch (ext)
        {
            case ".doc":
                contenttype = "application/vnd.ms-word";
                break;

            case ".docx":
                contenttype = "application/vnd.ms-word";
                break;

            case ".xls":
                contenttype = "application/vnd.ms-excel";
                break;

            case ".xlsx":
                contenttype = "application/vnd.ms-excel";
                break;

            case ".jpg":
                contenttype = "image/jpg";
                break;

            case ".png":
                contenttype = "image/png";
                break;

            case ".gif":
                contenttype = "image/gif";
                break;

            case ".pdf":
                contenttype = "application/pdf";
                break;
        }

        if (priority.SelectedValue == "Critical")
        {
            slaid.Text = "1";
            slastatus.Text = "Before 30 mins";
            level2.Text = DateTime.Now.AddHours(2).ToString();
            level3.Text = DateTime.Now.AddHours(2).ToString();
        }

        else if (priority.SelectedValue == "High")
        {
            slaid.Text = "2";
            slastatus.Text = "Before 2 hours";
            level2.Text = DateTime.Now.AddHours(8).ToString();
            level3.Text = DateTime.Now.AddHours(12).ToString();

        }

        else if (priority.SelectedValue == "Moderate")
        {
            slaid.Text = "3";
            slastatus.Text = "Before 12 hours";
            level2.Text = DateTime.Now.AddHours(12).ToString();
            level3.Text = DateTime.Now.AddHours(24).ToString();
        }

        else
        {
            slaid.Text = "4";
            slastatus.Text = "Before 24 hours";
            level2.Text = DateTime.Now.AddHours(24).ToString();
            level3.Text = DateTime.Now.AddHours(48).ToString();
        }

        Stream fs = FileUpload1.PostedFile.InputStream;
        BinaryReader br = new BinaryReader(fs);
        Byte[] bytes = br.ReadBytes((Int32)fs.Length);

        //insert the file into database

        string strQuery = "insert into TMSTicketMaster(TMSTicketSubject,TMSTicketDepartment,TMSTicketDescription,TMSTicketPriority,TMSTicketLoaction,TMSSLAID,TMSSLAStatus,TMSHostName,TMSAttachment,EmployeeID,Level2Escalation,Level3Escalation,ContentName,ContentType) values (@var1,@var2,@var3,@var4,@var5,@var6,@var7,@var8,@var9,@var10,@var11,@var12,@ContentName,@ContentType)";
        SqlCommand cmd = new SqlCommand(strQuery);
        cmd.Parameters.AddWithValue("@var1", subject.Text);
        cmd.Parameters.AddWithValue("@var2", department.SelectedValue);
        cmd.Parameters.AddWithValue("@var3", description.Text);
        cmd.Parameters.AddWithValue("@var4", priority.SelectedValue);
        cmd.Parameters.AddWithValue("@var5", Location.SelectedValue);
        cmd.Parameters.AddWithValue("@var6", slaid.Text);
        cmd.Parameters.AddWithValue("@var7", slastatus.Text);
        cmd.Parameters.AddWithValue("@var8", hostName.Text);
        cmd.Parameters.AddWithValue("@var10", Session["SessionEmployeeID"]);
        cmd.Parameters.AddWithValue("@var11", level2.Text);
        cmd.Parameters.AddWithValue("@var12", level3.Text);
        cmd.Parameters.Add("@ContentName", SqlDbType.VarChar).Value = filename;
        cmd.Parameters.Add("@ContentType", SqlDbType.VarChar).Value = contenttype;
        cmd.Parameters.Add("@var9", SqlDbType.Binary).Value = bytes;


        //InsertUpdateData(cmd);


        String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
        SqlConnection con = new SqlConnection(strConnString);
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            Response.Redirect("history.aspx");
            //return true;
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You will get a conformation mail.')", true);
        }

        catch (Exception ex)
        {
            Response.Write(ex.Message);
            //throw ex;
            //return false;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }



        try
        {
            DataRow dr4;
            DataTable dt4 = new DataTable();
            string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
            SqlConnection conn = new SqlConnection(conString1);
            SqlDataAdapter da4 = new SqlDataAdapter("select Max(TMSTicketID) from TMSTicketMaster where EmployeeID = '" + Session["SessionEmployeeID"] + "' and TMSTicketSubject='" + subject.Text + "'", conn);
            da4.Fill(dt4);
            int i = 0;
            dr4 = dt4.Rows[i];
            string ticketID = Convert.ToString(dr4[0]);
            SendMail(ticketID);
        }
        catch
        {

        }
        Response.Redirect("history.aspx");
    }

    protected void SendMail(string tktId)
    {
        string ticket = tktId;
        string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
        SqlConnection con2 = new SqlConnection(conString1);
        if (Session["employeeID"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        empID.Text =Session["EmployeeID"].ToString();
        int i = 0;
        DataTable dt2 = new DataTable();
        DataRow dr2;
        SqlDataAdapter da2 = new SqlDataAdapter("Select EmployeeName from Employee where EmployeeID='" + Session["SessionEmployeeID"] + "'", con2);
        da2.Fill(dt2);
        dr2 = dt2.Rows[i];
        userName.Text = Convert.ToString(dr2[0]);
        DataTable dt3 = new DataTable();
        DataRow dr3;
        SqlDataAdapter da3 = new SqlDataAdapter("Select EmployeeMailID from Employee where EmployeeID='" + Session["SessionEmployeeID"] + "'", con2);
        da3.Fill(dt3);
        dr3 = dt3.Rows[i];
        mailId.Text = Convert.ToString(dr3[0]);

        using (MailMessage mm = new MailMessage("helpdesk.snpl@gmail.com", Session["SessionUserMailID"].ToString()))
        {

            mm.Subject = "TMS - New ticket details";
            mm.Body = "Hello " + userName.Text + " <br/>" + " We have received your Ticket." + "<br/>" + "<br/>" + " Your Ticket ID is " + ticket + "<br/>" + "\nSubject:\t" + subject.Text + "<br/>" + "\nDepartment:\t" + department.SelectedValue +
                        "<br/>" + "\nDescription:\t" + description.Text + "<br/>" + "\nPriority:\t" + priority.SelectedValue + "<br/>" + "\nLocation:\t" + Location.Text + "<br/>" +
                              " <br/> " + "Thanks & Regards" + "<br>" + "TMS Team";


            mm.IsBodyHtml = true;

            string str = @"<html><body>" + "<br /><img src=\"cid:logo\"></body></html>";
            AlternateView av = AlternateView.CreateAlternateViewFromString(str, null, System.Net.Mime.MediaTypeNames.Text.Html);
            LinkedResource lr = new LinkedResource("images/logo.png", MediaTypeNames.Image.Jpeg);
            lr.ContentId = "image1";
            av.LinkedResources.Add(lr);
            mm.AlternateViews.Add(av);
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("helpdesk.snpl@gmail.com", "jdzydiyrrvfnrjbf");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            LinkedResource LinkedImage = new LinkedResource(@"Images\logo.png");
            LinkedImage.ContentId = "MyPic";
            //Added the patch for Thunderbird as suggested by Jorge
            LinkedImage.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mm.Body + "<br>" + " <img src=cid:MyPic>", null, "text/html");

            htmlView.LinkedResources.Add(LinkedImage);
            mm.AlternateViews.Add(htmlView);
            smtp.Send(mm);
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent Successfull.');", true);


        }

    }
}

