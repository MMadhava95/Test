using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default : System.Web.UI.Page
{
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
    protected void btnSignIn_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
        Page.Validate("Group1");
        if (Page.IsValid)
        {
            if (Page.IsPostBack == true)
            {
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmdd = new SqlCommand("select * from CreateCourseTable  where CourseName='" + Tbcoursename.Text + "'", con);
                int count = Convert.ToInt32(cmdd.ExecuteScalar());
                if (count > 0)
                {
                    lblmessage.Text = "This course is already exist";
                }
                else
                {
                    string coursetitle = Tbcoursename.Text;
                    //String constr = "Data Source=HP-PC\\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
                    //SqlConnection con = new SqlConnection(constr);
                    FUimageupload.SaveAs(Server.MapPath("~/images/") + Path.GetFileName(FUimageupload.FileName));
                    string link1 = "../images/" + Path.GetFileName(FUimageupload.FileName);
                    string imglink =
                        //"<" +"div"+" " +"class="+"w3"+"-"+"container"+" "+"style="+"padding"+":"+"60px"+" " +"16px"+" " +"id="+"Courses"+">"
                        //+"<" +"div" +" " +"class=" +"w3"+"-"+"row"+"-"+"padding"+" " +"w3"+"-"+"grayscale"+">"
                        "<" + "div" + " " + "class=" + "w3" + "-" + "col" + " " + "l3" + " " + "m6" + " " + "w3" + "-" + "margin" + "-" + "bottom" + ">" + "<br/>"
                    + "<" + "div" + " " + "class=" + "w3" + "-" + "card" + " " + "w3" + "-" + "light" + "-" + "gray" + ">" + "<br/>"
                    + "<" + "img src = " + link1 + "  " + "style=" + "width:" + "100%;" + "min-height:150px;" + "max-height:160px;" + ">" + "<br/>"
                    + "<" + "div" + " " + "class=" + "w3" + "-" + "container" + ">" + "<br/>"
                    + "<h3>" + Tbcoursename.Text + "</h3>" + "<br/>"
                    + "<h5>" + tbcouseduration.Text + "</h5>" + "<br/>"
                    + "<h5>" + txtcont.Text + "</h5>" + "<br/>"
                    + "<h5>" + tbCourseContent.Text + "</h5>" + "<br/>"
                    //+ "</div>" + "<br/>"
                    + "</div>" + "<br/>"
                    + "</div>" + "<br/>"
                    + "</div>";
                    if (DDcoursetype.SelectedItem.Value == "Select Course Type")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Select Course Type ');", true);
                        DDcoursetype.Focus();

                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_LMSCourses";
                        cmd.Parameters.Add("@CourseName", SqlDbType.NVarChar).Value = Tbcoursename.Text.Trim();
                        cmd.Parameters.Add("@CourseDuration", SqlDbType.NVarChar).Value = tbcouseduration.Text.Trim();
                        cmd.Parameters.Add("@CourseFaculty", SqlDbType.NVarChar).Value = tbCourseFaculty.Text.Trim();
                        cmd.Parameters.Add("@CourseType", SqlDbType.NVarChar).Value = DDcoursetype.SelectedValue.Trim();
                        cmd.Parameters.Add("@CourseContent", SqlDbType.NVarChar).Value = tbCourseContent.Text.Trim();
                        cmd.Parameters.Add("@HrefLink", SqlDbType.NVarChar).Value = imglink;
                        cmd.Parameters.Add("@PreRequisites", SqlDbType.NVarChar).Value = CoursePrerequisites.Text.Trim();
                        cmd.Parameters.Add("@ImagePath", SqlDbType.NVarChar).Value = link1;
                        cmd.Parameters.Add("@Learncontent", SqlDbType.NVarChar).Value = txtcont.Text.Trim();
                       
                        SqlParameter para = new SqlParameter()
                        {
                            ParameterName = "@CourseID",
                            Value = -1,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(para);
                        cmd.Connection = con;
                        //con.Open();
                        cmd.ExecuteNonQuery();
                        Session["CourseID"] = cmd.Parameters["@CourseID"].Value.ToString();
                        Session["LmsCourseName"] = Tbcoursename.Text;
                        con.Close();
                        con.Dispose();
                        lblmessage.Text = "Course created successfully";
                        lblNote.Visible = false;
                        Tbcoursename.Text = " ";
                        tbcouseduration.Text = "";
                        tbCourseFaculty.Text = "";
                        tbCourseContent.Text = "";
                        txtcont.Text = "";
                        CoursePrerequisites.Text = "";
                    }
                }
            }

        }
    }
    protected void ValidateFileSize(object sender, ServerValidateEventArgs e)
    {
        System.Drawing.Image img = System.Drawing.Image.FromStream(FUimageupload.PostedFile.InputStream);
        int height = img.Height;
        int width = img.Width;
        decimal size = Math.Round(((decimal)FUimageupload.PostedFile.ContentLength / (decimal)1024), 2);
        if (size > 200)
        {
            CustomValidator1.ErrorMessage = "File size must not exceed 200 KB.";
            e.IsValid = false;
        }
        if (height > 300 || width > 300)
        {
            CustomValidator1.ErrorMessage = "Height and Width must not exceed 300px.";
            e.IsValid = false;
        }
    }
}
