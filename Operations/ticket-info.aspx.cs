using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Operations_Default : System.Web.UI.Page
{
    public string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        errorDiv.Visible = false;
        Mailid.Text = Session["SessionUserMailID"].ToString();
        if (Session["TicketID"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        String str = Session["TicketID"].ToString();
        SqlConnection con = new SqlConnection(conString1);
        try
        {
            con.Open();
            //SqlCommand cmd1 = new SqlCommand("select TMSTicketID,TMSTicketSubject,TMSTicketDescription,TMSTicketPriority,TMSTicketDate,TMSHostName,TMSTicketStatus,EmployeeID,TMSTicketOwnership,TMSTicketLocation from TMSTicketMaster where[TMSTicketID] = '" + str + "'", con);
            DataTable dt = new DataTable();
            int i = 0;
            DataRow dr;
            SqlDataAdapter sda = new SqlDataAdapter("select TMSTicketID,TMSTicketSubject,TMSTicketDescription,TMSTicketPriority,TMSTicketDate,TMSHostName,TMSTicketStatus,EmployeeID,TMSTicketOwnership,TMSTicketLoaction from TMSTicketMaster where TMSTicketID = '" + str + "'", con);
            sda.Fill(dt);
            string tktstatus = dt.Rows[0]["TMSTicketStatus"].ToString();
            if (tktstatus == "Closed")
            {
                ReSubmit.Visible = true;
            }
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[i];
                // employee123 = Convert.ToString(dr25[0]);
                tckid.Text = Convert.ToString(dr[0]);
                sub.Text = Convert.ToString(dr[1]);
                Desc.Text = Convert.ToString(dr[2]);
                prt.Text = Convert.ToString(dr[3]);
                tckdate.Text = Convert.ToString(dr[4]);
                hstname.Text = Convert.ToString(dr[5]);
                Sts.Text = Convert.ToString(dr[6]);
                Empid.Text = Convert.ToString(dr[7]);
                Lct.Text = Convert.ToString(dr[9]);
                Onr.Text = Convert.ToString(dr[8]);
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
            errorDiv.Visible = true;
            errorMessage.Text = ex.Message;
        }
        this.Remarks();
        this.BindGrid();
    }

    private void Remarks()
    {
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
        SqlConnection con = new SqlConnection(conString1);
        using (con)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select TMSTicketRemarks from TMSTicketMaster where TMSTicketID= '" + Session["TicketID"].ToString() + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtRemarks.Text = reader["TMSTicketRemarks"].ToString();
                reader.Close();
                con.Close();
            }
        }
    }
        private void BindGrid()
    {
        //string ticketID1 = "SNPL-TMS-00001";
        string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                //string ticketID = "SNPL-TMS-00001";
                SqlCommand com = new SqlCommand("select ContentName from TMSTicketMaster where TMSTicketID='" + Session["TicketID"].ToString() + "'", con);
                com.Connection = con;
                com.Connection.Open();
                String body;
                body = com.ExecuteScalar().ToString();
                if (body == null || body == string.Empty)
                {
                    Label1.Text = "No Attachments Found ";
                    GridView2.Visible = false;
                }
                else
                {
                    // int i = 0;
                    DataTable dt3 = new DataTable();
                    SqlDataAdapter da3 = new SqlDataAdapter("select TMSAttachment,ContentName, ContentType from TMSTicketMaster where TMSTicketID='" + Session["TicketID"].ToString() + "'", con);
                    //con.Open();
                    da3.Fill(dt3);
                    GridView2.DataSource = dt3;
                    GridView2.DataBind();
                }
                con.Close();
            }
        }
    }

    protected void Comments_Click(object sender, EventArgs e)
    {
        string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
        SqlConnection con = new SqlConnection(conString1);
        //SqlConnection con = new SqlConnection("Data Source=HP-PC\\SQLEXPRESS;Integrated Security=true;Initial Catalog=TicketManagementSystemDB");
        con.Open();
        SqlCommand command = new SqlCommand("select TMSTicketRemarks from TMSTicketMaster where TMSTicketID='" + Session["TicketID"].ToString() + "'", con);
        string oldMessage = (string)command.ExecuteScalar();
        string newMessage = remarks.Text;
        string str = DateTime.Now.ToString("dd/%M/yyyy hh:mm:ss tt");
        TextBox4.Text = str;
        string message = oldMessage + "<br>  User: " + "<br>" + TextBox4.Text + "<br>" + newMessage + " <br> ";
        SqlCommand com = new SqlCommand("update TMSTicketMaster set TMSTicketRemarks=@remarks where TMSTicketID='" + Session["TicketID"].ToString() + "'", con);
        com.Parameters.AddWithValue("@remarks", message);
        com.ExecuteNonQuery();
        con.Close();
        Response.Redirect("ticket-info.aspx");
    }

    protected void MailToUser_Click(object sender, EventArgs e)
    {
        MailMessage Msg = new MailMessage();
        // Sender e-mail address.
        Msg.From = new MailAddress(Mailid.Text);
        // Recipient e-mail address.
        Msg.To.Add(Mailid.Text);
        Msg.Subject = subject.Text;
        Msg.Body = Description.Text;
        Msg.IsBodyHtml = true;
        // your remote SMTP server IP.
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.Port = 587;
        smtp.Credentials = new System.Net.NetworkCredential("helpdesk.snpl@gmail.com", "jdzydiyrrvfnrjbf");
        smtp.EnableSsl = true;
        smtp.Send(Msg);
        //Msg = null;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Message sent.')", true);
        //lbltxt.Text = "Your Password Details Sent to your mail";
        // Clear the textbox valuess
        subject.Text = "";
        Description.Text = "";
    }

    protected void lnkDownload_Click1(object sender, EventArgs e)
    {
        string id = ((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName, contentType;
        string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select TMSAttachment,ContentName, ContentType  from TMSTicketMaster where TMSTicketID='" + Session["TicketID"].ToString() + "'";
                //cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = con;
                con.Open();
                {
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["TMSAttachment"];
                        contentType = sdr["ContentType"].ToString();
                        fileName = sdr["ContentName"].ToString();
                    }
                    con.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ContentType);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
            Response.End();
        }
    }



    protected void ReSubmit_Click(object sender, EventArgs e)
    {
        string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
        SqlConnection con = new SqlConnection(conString1);
        con.Open();
        string query = "update TMSTicketMaster set TMSTicketStatus = 'New', TMSTicketDate = getdate(),TMSTicketRemarks=' ',FlagLevel2=0,FlagLevel3=0 where TMSTicketID = '" + Session["TicketID"] + "'";
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.CommandType = CommandType.Text;
        SqlDataReader dr = cmd.ExecuteReader();
        con.Close();
        Response.Redirect("ticket-info.aspx");
    }
}
