using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public partial class testing : System.Web.UI.Page
{
    string today = DateTime.Today.ToString("dd-MM-yyyy");
    DateTime Fourday;
    DateTime Fiveday;
    string Empid;
    public string Empmailid;
    public string ApproverMailid;
    public string NextApproverMailID;
    public string ClaimID;
    public string level;
    public string subject;
    public string body;
    public string AppName;

    protected void Page_Load(object sender, EventArgs e)
    {

       
   
        try
        {

            SqlConnection con = new SqlConnection(@"Data Source=snpls;Database =NEWTESTING; User ID=sa;Password=snpl123@;MultipleActiveResultSets=true;");
            con.Open();
            DataTable dt = new DataTable();
            SqlCommand cmd1 = new SqlCommand("select FourDaysAlert,ERSClaimID,FiveDaysAlert,ERSClaim.ERSEmployeeID,EmployeeMailID,ERSApproverMailID,ERSApproverName,ERSClaimStatus From ERSClaim left join Employee on ERSClaim.ERSEmployeeID=Employee.EmployeeID left join ERSApprover on ERSClaim.ERSApproverID=ERSApprover.ERSApproverID where ERSClaimStatus='Pending'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            da.Fill(dt);
            SqlDataReader Myreader = cmd1.ExecuteReader();
            Myreader.Read();
            //int CountRow = dt.Rows.Count;
            foreach (DataRow row in dt.Rows)
            {


                Fourday = Convert.ToDateTime(row["FourDaysAlert"]);
                Fiveday = Convert.ToDateTime(row["FiveDaysAlert"]);
                Empid = Convert.ToString(row["ERSEmployeeID"]);
                Empmailid = Convert.ToString(row["EmployeeMailID"]);
                ApproverMailid = Convert.ToString(row["ERSApproverMailID"]);
                AppName = Convert.ToString(row["ERSApproverName"]);
                ClaimID = Convert.ToString(row["ERSClaimID"]);
                string four = Fourday.ToShortDateString();
                string five = Fiveday.ToShortDateString();

                if (today == four)
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(ApproverMailid);
                    mail.From = new MailAddress("snplhelpdesk@gmail.com");
                    mail.Subject = "Claim process delay ";
                    mail.Body = string.Format("Dear " + AppName + " ,\n\n<br /><br />  The claim with claimID :" + ClaimID + " has not been processed from 3 days.After 24 hours the claim will be forwarded to next level of approver. <br /><br />Thanks And Regards\n<br />ERS Team\n<br />SNPL");
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Credentials = new System.Net.NetworkCredential("snplhelpdesk@gmail.com", "cmtoovnzlmtoiwja");
                    smtp.EnableSsl = true;
                    smtp.Port = 587;
                    smtp.Send(mail);
                }


                else if (today == five)
                {
                    ForwardClaim();
                    NextApprover();
                    MailMessage mail = new MailMessage();
                    mail.To.Add(ApproverMailid);
                    mail.To.Add(NextApproverMailID);
                    mail.From = new MailAddress("snplhelpdesk@gmail.com");
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Credentials = new System.Net.NetworkCredential("snplhelpdesk@gmail.com", "cmtoovnzlmtoiwja");
                    smtp.EnableSsl = true;
                    smtp.Port = 587;
                    smtp.Send(mail);
                }
            }
            Myreader.Close();
         
            con.Close();
        }


        catch (Exception ex)
        {
          
        }

    }

   

    public void NextApprover()
    {

        SqlConnection con2 = new SqlConnection(@"Data Source=snpls;Database =NEWTESTING;User ID=sa;Password=snpl123@;MultipleActiveResultSets=true;");
        con2.Open();
        SqlCommand cmd2 = new SqlCommand("select ERSApproverLevel,ERSApproverName from ERSApprover where ERSApproverMailID='" + ApproverMailid + "'", con2);
        SqlDataReader Myreader4 = null;
        Myreader4 = cmd2.ExecuteReader();
        Myreader4.Read();
        level = Convert.ToString(Myreader4["ERSApproverLevel"]);
        string name = Convert.ToString(Myreader4["ERSApproverName"]);
        Myreader4.Close();
        if (level == "level3")
        {
            SqlCommand cmd7 = new SqlCommand("select ERSApproverMailID from ERSApprover where ERSApproverLevel='level2 '", con2);
            NextApproverMailID = cmd7.ExecuteScalar().ToString();
            subject = "Claim process delay ";
            body = "Dear Approver ,\n\n<br /><br /> The claim with claimID :" + ClaimID + " has not been processed by " + name + " so the Claim has been forwarded to next level. <br /><br />Thanks And Regards\n<br />ERS Team\n<br />SNPL";
           
        }
        else if (level == "level2")
        {
            SqlCommand cmd8 = new SqlCommand("select ERSApproverMailID from ERSApprover where ERSApproverLevel='level1 '", con2);
            NextApproverMailID = cmd8.ExecuteScalar().ToString();
            subject = "Claim process delay ";
            body = "Dear Approver ,\n\n<br /><br /> The claim with claimID :" + ClaimID + " has not been processed by " + name + " so the Claim has been forwarded to next level. <br /><br />Thanks And Regards\n<br />ERS Team\n<br />SNPL";
           
        }
        else
        {
            NextApproverMailID = "jhansi.gundapaneni@supremesoft.net";
            subject = "Claim process delay ";
            body = "Dear Approver ,\n\n<br /><br /> The claim with claimID :" + ClaimID + " has not been processed. <br /><br />Thanks And Regards\n<br />ERS Team\n<br />SNPL";
           
        }


        cmd2.ExecuteNonQuery();
        con2.Close();
    }
    public void ForwardClaim()
    {
        string Appid;
        SqlConnection con4 = new SqlConnection(@"Data Source=snpls;Database =NEWTESTING;User ID=sa;Password=snpl123@;MultipleActiveResultSets=true;");
        con4.Open();
        SqlCommand cmd3 = new SqlCommand("select ERSApproverID From ERSApprover where ERSApproverMailID='" + ApproverMailid + "'", con4);
        SqlDataReader Myreader4 = null;
        Myreader4 = cmd3.ExecuteReader();
        Myreader4.Read();
        Appid = Convert.ToString(Myreader4["ERSApproverID"]);
        Myreader4.Close();
        if (Appid == "SNPL-HYD-2048")
        {
            SqlCommand cmd5 = new SqlCommand("Update ERSClaim set ERSApproverID='SNPL-HYD-2016' where ERSClaimID='" + ClaimID + "'", con4);
            cmd5.ExecuteNonQuery();
           
        }
        else if (Appid == "SNPL-HYD-2016")
        {
            SqlCommand cmd6 = new SqlCommand("Update ERSClaim set ERSApproverID='SNPL-HYD-1107' where ERSClaimID='" + ClaimID + "'", con4);
            cmd6.ExecuteNonQuery();
           
        }
        con4.Close();
    }


    private void WriteToFile(string text)
    {
        string path = "C:\\ServiceLog.txt";
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
            writer.Close();
        }
    }

}

