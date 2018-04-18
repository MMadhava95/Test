using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Operations_Default : System.Web.UI.Page
{
    int LmsCourseId = 0, CourseName = 0, EmployeeID = 0;


    SqlConnection con;
    SqlCommand cmd;
    int correct_asn = 0;
    int wrong_ans = 0;
    int select_no = 0;
    int count = 0;
    public Operations_Default()
    {
        con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ToString();

        cmd = new SqlCommand();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            loadgrid();
        }
        lblmail.Visible = false;
        lblmail.Text = "raj.maddala@supremesoft.net";
        lblcoursename.Text =  "Assessment";
        //CourseName = Session["CourseName"].ToString();
        int i = 0;

        DataTable dt2 = new DataTable();
        DataRow dr2;
        SqlDataAdapter da2 = new SqlDataAdapter("Select [EmployeeName] from [Employee] where [EmployeeMailID]='" + lblmail.Text + "'", con);
        da2.Fill(dt2);
        dr2 = dt2.Rows[i];
        UserName.Text = " " + Convert.ToString(dr2[0]) + " ";

    }
    private void loadgrid()
    {
        con.Open();
        try
        {
            cmd.CommandText = "select * from [dbo].[Question] where CourseName='Csharp'";
            cmd.Connection = con;
            SqlDataReader dr = cmd.ExecuteReader();
            GridView1.DataSource = dr;
            GridView1.DataBind();
        }
        catch
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alertMessage",
    "<script type='text/javascript'>alert('Some thing went wrong try again !');window.location.replace('..//NewVideosPage.aspx');</script>");
        }
    }
    protected void btnsubmitAss_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            Label l1 = row.FindControl("QuestionId") as Label;
            RadioButton r1 = row.FindControl("Rbans1") as RadioButton;
            RadioButton r2 = row.FindControl("Rbans2") as RadioButton;
            RadioButton r3 = row.FindControl("Rbans3") as RadioButton;
            RadioButton r4 = row.FindControl("Rbans4") as RadioButton;

            if (r1.Checked)
            {
                select_no = 1;
            }
            else if (r2.Checked)
            {
                select_no = 2;
            }
            else if (r3.Checked)
            {
                select_no = 3;
            }
            else if (r4.Checked)
            {
                select_no = 4;
            }

            con.Close();
            //my duplicate code

            //check_number(l1.Text);
            cmd.CommandText = "select * from Question where [coursename]='" + Session["CourseName"].ToString() + "' AND [Question_ID]=@qid" + count;
            cmd.Parameters.AddWithValue("@qid" + count, l1.Text);
            cmd.Connection = con;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (select_no == Convert.ToInt32(dr["correct_ans"]))
                {
                    correct_asn = correct_asn + 1;
                }
                else
                {
                    wrong_ans = wrong_ans + 1;

                }
            }
            count++;
            con.Close();
        }

        //=====================***********************************==================//
        Page.Validate("Group1");
        if (Page.IsValid)
        {
            if (Page.IsPostBack == true)
            {
                SqlConnection con1 = new SqlConnection(con.ConnectionString);
                SqlCommand command = new SqlCommand("select * from CreateCourseTable where CourseName='" + Session["CourseName"].ToString() + "'", con1);
                con1.Open();
                SqlDataReader drCourse = command.ExecuteReader();

                while (drCourse.Read())
                {
                    LmsCourseId = (Convert.ToInt32(drCourse["CourseID"]));


                }
                con1.Close();

                //Code to get employee Id

                SqlCommand emplyeeIdget = new SqlCommand("Select * from registration where employeeMailId='" + lblmail.Text.ToString() + "'", con);

                con.Open();


                SqlDataReader dremp = emplyeeIdget.ExecuteReader();

                while (dremp.Read())

                {
                    EmployeeID = (Convert.ToInt32(dremp["EmployeeID"]));
                }
                con.Close();
            }


            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_insertResult";

            cmd.Parameters.Add("@UserMailID", SqlDbType.NVarChar).Value = lblmail.Text.Trim();
            cmd.Parameters.Add("@EmployeeId", SqlDbType.NVarChar).Value = EmployeeID.ToString();
            cmd.Parameters.Add("@CourseId", SqlDbType.NVarChar).Value = LmsCourseId.ToString();
            cmd.Parameters.Add("@Result", SqlDbType.NVarChar).Value = correct_asn.ToString();
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            //lblmessage.Text = "You Total Score Is  : " + correct_asn.ToString();
            //Session["result"] = lblmessage.Text;
            //Response.Redirect("resultpage.aspx");

            ClientScript.RegisterStartupScript(typeof(Page), "alertMessage",
"<script type='text/javascript'>alert('Result: " + correct_asn + "');window.location.replace('..//NewVideosPage.aspx');</script>");

        }
    }


}

