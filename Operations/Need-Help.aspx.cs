using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NeedHelp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionUserMailID"] != null && Session["SessionUserPosition"].ToString() == "Employee")
        {
            UserName.Text = Session["SessionEmployeeName"].ToString();
            UserEmail.Text = Session["SessionUserMailID"].ToString();
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected String UserHelpMailContent()
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Mail Content Pages/Mail Content User Need Help.html")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("[UserName]", UserName.Text);
        body = body.Replace("[UserEmail]", UserEmail.Text);
        body = body.Replace("[UserSubject]", UserSubject.Text);
        body = body.Replace("[UserMessage]", UserMessage.Text);
        return body;
    }

    protected void UserSubmitButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (UserSubject.Text != String.Empty.ToString() && UserMessage.Text != String.Empty.ToString())
            {
                using (MailMessage mm = new MailMessage("helpdesk.snpl@gmail.com", "helpdesk.snpl@gmail.com"))
                {
                    mm.Subject = "Need Help! SNPL";
                    mm.Body = UserHelpMailContent();
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.UseDefaultCredentials = true;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = true;
                    System.Net.NetworkCredential NetworkCred = new NetworkCredential("helpdesk.snpl@gmail.com", "jdzydiyrrvfnrjbf");
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                    UserName.Text = UserEmail.Text = UserSubject.Text = UserMessage.Text = string.Empty;
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Your Message was sent to SNPL-Helpdesk. They will be respond in 24 hours.');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please fill Required Fields.');", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}