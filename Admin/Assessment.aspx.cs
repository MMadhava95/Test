using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    int LmsCourseId = 0;
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
    protected void getQu_no()
    {
        SqlConnection Qno_con = new SqlConnection(constr);
        SqlCommand Qno_cmd = new SqlCommand("select [coursename] from [Question] where [coursename]='" + ddlCourseName.SelectedValue.ToString() + "'");

        SqlDataAdapter ap = new SqlDataAdapter(Qno_cmd.CommandText, Qno_con);
        DataSet ds = new DataSet();

        Qno_con.Open();
        ap.Fill(ds);
        int n = (ds.Tables[0].Rows.Count) + 1;
        if (ddlCourseName.SelectedValue == "0")
        {
            //QuestionNO.Text = " ";
            //Label1.Text = "Please Select a Course Name!!";
            //Label1.ForeColor = System.Drawing.Color.Red;
        }
        else
            tbQn_no.Text = n.ToString();


        Qno_con.Close();
        tbQuestion.Focus();
    }

   

    

    protected void btnSubmitQn_Click2(object sender, EventArgs e)
    {
        try
        {
            Page.Validate("Group1");
            if (Page.IsValid)
            {
                if (Page.IsPostBack == true)
                {

                    SqlConnection con1 = new SqlConnection(constr);
                    SqlCommand command = new SqlCommand("select * from CreateCourseTable where CourseName='" + ddlCourseName.SelectedValue.ToString() + "'", con1);
                    con1.Open();
                    SqlDataReader drCourse = command.ExecuteReader();
                    while (drCourse.Read())
                    {
                        LmsCourseId = (Convert.ToInt32(drCourse["CourseID"]));


                    }
                    con1.Close();


                    SqlConnection con = new SqlConnection(constr);

                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_insertQuestions";

                    cmd.Parameters.Add("@CourseId", SqlDbType.NVarChar).Value = LmsCourseId.ToString().Trim();
                    //cmd.Parameters.Add("@CourseId", SqlDbType.Int).Value =LmsCourseId;
                    cmd.Parameters.Add("@QuestionNo", SqlDbType.NVarChar).Value = tbQn_no.Text.Trim();
                    cmd.Parameters.Add("@Question", SqlDbType.NVarChar).Value = tbQuestion.Text.Trim();

                    cmd.Parameters.Add("@ans1", SqlDbType.NVarChar).Value = tbOption1.Text.Trim();
                    cmd.Parameters.Add("@ans2", SqlDbType.NVarChar).Value = tbOption2.Text.Trim();
                    cmd.Parameters.Add("@ans3", SqlDbType.NVarChar).Value = tbOption3.Text.Trim();
                    cmd.Parameters.Add("@ans4", SqlDbType.NVarChar).Value = tbOption4.Text.Trim();
                    cmd.Parameters.Add("@correct_ans", SqlDbType.NVarChar).Value = tb_answar.Text.Trim();
                    cmd.Parameters.Add("@course_name", SqlDbType.NVarChar).Value = ddlCourseName.SelectedValue.ToString();

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    tb_answar.Text = "";
                    tbOption1.Text = "";
                    tbOption2.Text = "";
                    tbOption3.Text = "";
                    tbOption4.Text = "";
                    tbQuestion.Text = "";
                    lblmessage.Text = "Question Inserted Successfully!!";
                    //QuestionNO  count here
                    getQu_no();
                    //--------------------------------

                }
            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void ddlCourseName_SelectedIndexChanged(object sender, EventArgs e)
    {
        getQu_no();
    }
}