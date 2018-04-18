using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    string LmsCourseId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionUserMailID"] != null)
        {

        }
        else
        {
            Response.Redirect("../Login.aspx");
        }

    }
    protected void submit_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(constr);
        //code To get Course id from Create course table and store it in a variable to insert into videos table as foreing key
        SqlCommand command = new SqlCommand("select * from CreateCourseTable where CourseName='" + DdlcourseName.SelectedValue.ToString() + "'", con);
        con.Open();
        SqlDataReader drCourse = command.ExecuteReader();
        while (drCourse.Read())
        {
            LmsCourseId = (drCourse["CourseID"].ToString());
        }
        con.Close();
        if (DdlcourseType.SelectedItem.Value == "Select CourseType")
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Select Course Type ');", true);
            DdlcourseType.Focus();
        }
        else
        {
            if (DdlcourseName.SelectedItem.Value == "Select Course Name")
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Select Course Name');", true);
                DdlcourseName.Focus();
            }
            else
            {
                string coursetitle = TextBox1.Text;
                FileUpload1.SaveAs(Server.MapPath("~/CourseVideos/") + Path.GetFileName(FileUpload1.FileName));
                string link1 = "CourseVideos/" + Path.GetFileName(FileUpload1.FileName);
                link1 = Regex.Replace(link1, @"\s+", string.Empty);
               // string link = "<video width=700px height=500px controls><Source src=" + link1 + " " + "type=video/mp4></video>";
                string hrf = "<tr>" + "<li>" + "<a href=" + link1 + " " + "style =" + "text" + "-" + "decoration" + ":none;" + " " + " target =vid_frame > " + coursetitle + " </a>" + "</li>" + "</tr>";
                String query = "insert into [videostableLms] values('" + LmsCourseId.ToString() + "','" + TextBox1.Text + "','" + TextBox1.Text + "','" + link1 + "','" + hrf + "','" + DdlcourseName.SelectedValue.ToString() + "','" + DdlcourseType.SelectedValue.ToString() + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                //
                string Document_title = TextBox2.Text;
                //PdfUpload.SaveAs(Server.MapPath("~/CourseDocuments/") + Path.GetFileName(PdfUpload.FileName));
                string documentlink = "CourseDocuments/" + Path.GetFileName(PdfUpload.FileName);
                //string documentlink1 = "<video width=700px height=500px controls><Source src=" + link1 + " " + "type=video/mp4></video>";
                string hrf1 = "<tr>" + "<li>" + "<a href=" + documentlink + " " + "style =" + "text" + "-" + "decoration" + ":none;" + " " + " target =_blank > " + Document_title + " </a>" + "</li>" + "</tr>";
                String query1 = "insert into [LMSDocumenttable] values('" + LmsCourseId.ToString() + "','" + TextBox2.Text + "','" + documentlink + "','" + hrf1 + "','" + DdlcourseName.SelectedValue.ToString() + "','" + DdlcourseType.SelectedValue.ToString() + "')";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                con.Open();
                cmd.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                con.Close();
            }
        }
        string fileName = Path.GetFileName(PdfUpload.PostedFile.FileName);
     //  PdfUpload.PostedFile.SaveAs(Server.MapPath("~/CourseDocuments/") + fileName);
        Label1.Text = "Files uploaded Successfully!";
        Label1.ForeColor = System.Drawing.Color.Green;
        lblNote.Visible = false;
        TextBox1.Text = "";
        TextBox2.Text = "";
    }
    protected void DdlcourseName_DataBound(object sender, EventArgs e)
    {
        DdlcourseName.Items.Insert(0, new ListItem("Select Course Name", ""));
    }

}