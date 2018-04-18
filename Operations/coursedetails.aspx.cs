using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class coursedetails : System.Web.UI.Page
{ 
    string LMSEmployeeId, user, name, Employeemail, Employeename, LmsCourseId;

    public string divMenu;
    StringBuilder sbMenu = new StringBuilder();
    string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
     public coursedetails()
    {
        SqlConnection con = new SqlConnection(constr);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionUserMailID"] != null)
        {
            Session["urlid"] = Request.QueryString["id"].ToString();
            LMSEmployeeId = Session["SessionEmployeeID"].ToString();
            user = Session["SessionUserMailID"].ToString();
            VideosBindGrid();
            vidbind();
            DocumentBindGrid();
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand comand = new SqlCommand("select * from CreateCourseTable where [CourseID]='" + Request.QueryString["id"].ToString() + "'", con);
            SqlDataReader dr = comand.ExecuteReader();
            while (dr.Read())
            {
                name = (dr["CourseName"].ToString());

            }
            con.Close();
            h3Coursename.InnerText = "' " + name.ToString() + " ' Course Details ";
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }

    }
    void vidbind()
    {
        
        int count = 0;
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cmd1 = new SqlCommand("select * from LMSCourseregistration where EmployeeMailID = '"+user+ "' and [CourseID]='"+ Session["urlid"]+ "' ", con);
        
        con.Open();
        count = Convert.ToInt32(cmd1.ExecuteScalar());
        
        con.Close();
        if (count== 0)
        {
            msglbl.Visible = true;
            msglbl.Text = "Please register for the course before proceeding to watch videos";
            //vid_frame.Visible = false;
        }
        else
        {

            SqlCommand cmd = new SqlCommand("select * from videostablelms where courseid='" + Request.QueryString["id"] + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable videodt = new DataTable();
            videodt.Load(sdr);
            DataList2.DataSource = videodt;
            DataList2.DataBind();
            con.Close();
       }
        
    }
    


    void VideosBindGrid()
    {
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        //SqlCommand cmd = new SqlCommand("select * from CreateCourseTable where CourseID=" + Label1.Text + "", con);
        SqlCommand cmd = new SqlCommand("select * from CreateCourseTable where CourseID='" + Request.QueryString["id"] + "'", con);
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        DataTable videodt = new DataTable();
        videodt.Load(dr);
        dlCourseList.DataSource = videodt;
        dlCourseList.DataBind();
        con.Close();
    }
    void DocumentBindGrid()
    {

        SqlConnection con = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand("select * from LMSDocumenttable where courseid='" + Request.QueryString["id"] + "'", con);
        con.Open();
        cmd.ExecuteNonQuery();
        SqlDataReader sdr = cmd.ExecuteReader();
        DataTable videodt = new DataTable();
        videodt.Load(sdr);
        gvContentList.DataSource = videodt;
        gvContentList.DataBind();
        con.Close();

    }

    protected void BtnAddCourse_Click(object sender, EventArgs e)
    {
       
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand cmdd = new SqlCommand("select * from LMSCourseregistration where LmsCourseName='" + name + "' and EmployeeMailID='" + user + "'", con);
        int count = Convert.ToInt32(cmdd.ExecuteScalar());
        if (count > 0)
        {
            Response.Write("<script>alert('Course Already added to your courses');</script>");
            con.Close();
        }
        else
        {
            con.Close();
            con.Open();
            SqlCommand comand = new SqlCommand("select * from Employee where [EmployeeMailID]='" + user + "'", con);
            SqlDataReader dr = comand.ExecuteReader();
            while (dr.Read())
            {
                Employeemail = (dr["EmployeeMailID"].ToString());
                Employeename = (dr["EmployeeName"].ToString());
                LMSEmployeeId = (dr["EmployeeID"].ToString());
            }
            con.Close();
            SqlCommand command = new SqlCommand("select * from CreateCourseTable where CourseName='" + name + "'", con);
            con.Open();
            SqlDataReader drCourse = command.ExecuteReader();
            while (drCourse.Read())
            {
                LmsCourseId = (drCourse["CourseID"].ToString());

            }
            con.Close();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_insertCourseRegistration";
            con.Open();
            //EmployeeId = Label1.Text;
            string a1 = LMSEmployeeId.ToString();
            cmd.Parameters.Add("@CourseID", SqlDbType.NVarChar).Value = Request.QueryString["id"].ToString();
            cmd.Parameters.Add("@CourseName", SqlDbType.NVarChar).Value = "" + name + "";
            cmd.Parameters.Add("@EmployeeID", SqlDbType.NVarChar).Value = a1;
            cmd.Parameters.Add("@EmployeeMailID", SqlDbType.NVarChar).Value = "" + user + "";
            cmd.Parameters.Add("@RegisteredCourse", SqlDbType.NVarChar).Value = "" + name + "";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Course added successfully');", true);
            string subject = "Course registration acknowledgement ";
            string body = "Dear \t" + " " + Employeename + ",<br /><br /> You have successfully registered for "+name+ " course\n\n<br /><br /> Thanks and regards,<br />\nLMS,<br />\nSNPL.";
            sendMailToEmployee(subject, body, Employeemail);
            msglbl.Visible = false;
            vidbind();
        }
    }
    protected void sendMailToEmployee(string subject, string body, string mail)
    {
        using (MailMessage mm = new MailMessage("snplhelpdesk@gmail.com", mail))

        {
            mm.Subject = subject;
            string str = @"<html><body>" + body + "<br /><img src=\"cid:image1\"></body></html>";
            AlternateView av = AlternateView.CreateAlternateViewFromString(str, null, System.Net.Mime.MediaTypeNames.Text.Html);
            LinkedResource lr = new LinkedResource("images/logo1.png", MediaTypeNames.Image.Jpeg);
            lr.ContentId = "image1";
            av.LinkedResources.Add(lr);
            mm.AlternateViews.Add(av);
            MailAddress copy = new MailAddress("chakri_snpl@yahoo.com");
            mm.Bcc.Add(copy);
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("lmshelpdesk.snpl@gmail.com", "zadharzyhmivkxxc");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
        }
    }
}
