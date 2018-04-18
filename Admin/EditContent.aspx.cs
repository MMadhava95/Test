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
    string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    SqlConnection con = new SqlConnection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionUserMailID"] != null)
        {

            //ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (!Page.IsPostBack)
            {
                VideosBindGrid();
                DocumentsBindGrid();
            }
            if (gvContentList.Rows.Count > 0)
            {
                gvContentList.CssClass = "table table-bordered table-striped  table-inverse nomargin";
                gvContentList.UseAccessibleHeader = true;
                gvContentList.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvContentList.FooterRow.TableSection = TableRowSection.TableFooter;
            }



            if (gvVideoList.Rows.Count > 0)
            {
                gvVideoList.CssClass = "table table-bordered table-striped  table-inverse nomargin";
                gvVideoList.UseAccessibleHeader = true;
                gvVideoList.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvVideoList.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }
    //Method to bind grid from database 
    void VideosBindGrid()
    {
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT * FROM videostablelms ", con);
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        DataTable videodt = new DataTable();
        videodt.Load(dr);
        gvVideoList.DataSource = videodt;
        gvVideoList.DataBind();

        con.Close();
    }
    void DocumentsBindGrid()
    {
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT * FROM LMSDocumenttable ", con);
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        DataTable Docdt = new DataTable();
        Docdt.Load(dr);
        gvContentList.DataSource = Docdt;
        gvContentList.DataBind();

        con.Close();
    }

    protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvVideoList, "Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";
        }
    }
    protected void OnRowDataBound2(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvContentList, "Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";
        }
    }

    protected void gvVideoList_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection connection = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand("select * from videostablelms where Id ='" + gvVideoList.SelectedRow.Cells[0].Text + "'", connection);
        connection.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        Label6.Text = gvVideoList.SelectedRow.Cells[0].Text;
        videotitle.Text = gvVideoList.SelectedRow.Cells[4].Text;
       // Label8.Text = gvVideoList.SelectedRow.Cells[5].Text;
        // Label4.Text = gvVideoList.SelectedRow.Cells[2].Text;
        //Label3.Text = gvVideoList.SelectedRow.Cells[3].Text;
        ModalPopupExtender1.Show();
    }

    protected void gvContentList_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand("select * from LMSDocumenttable where Id='" + gvContentList.SelectedRow.Cells[0].Text + "'", con);
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        Label9.Text = gvContentList.SelectedRow.Cells[0].Text;
        documenttitle.Text = gvContentList.SelectedRow.Cells[4].Text;
        documentextender.Show();

    }

    protected void videoedit_Click(object sender, EventArgs e)
    {
        String query, link, hrf;
        int i = 0;
        SqlConnection con = new SqlConnection(constr);
        try
        {
            if (Videofile.HasFile == false)
            {
                SqlDataAdapter filefetch = new SqlDataAdapter("select VideoPath,TitleLink from videostableLms where Id='" + Label6.Text + "'", con);
                DataTable dt = new DataTable();
                DataRow dr3;
                filefetch.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dr3 = dt.Rows[i];
                    link = Convert.ToString(dr3[0]);
                    hrf = Convert.ToString(dr3[1]);
                    query = "update [videostableLms] set [TitleLink] ='" + hrf + "' , [VideoPath]= '" + link + "' ,[VideoTitle] = '" + videotitle.Text + "'  where Id= '" + Label6.Text + "'";

                    SqlCommand cmd2 = new SqlCommand(query, con);
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    Alert.Text = "Course details updated successfully";
                    //DdlcourseType.ClearSelection();
                    //DdlcourseName.ClearSelection();
                    gvVideoList.DataBind();

                }
            }
            else
            {
                //code ends here ...........
                string coursetitle = videotitle.Text;
                //String constr = "Data Source=HP-PC;Initial Catalog=LMSDB;Integrated Security=True";

                Videofile.SaveAs(Server.MapPath("~/CourseVideos/") + Path.GetFileName(Videofile.FileName));
                string link1 = "CourseVideos/" + Path.GetFileName(Videofile.FileName);


                //link = "<video width=700px height=500px controls><Source src=" + link1 + " " + "type=video/mp4></video>";
                hrf = "<tr>" + "<li>" + "<a href=" + link1 + " " + "style =" + "text" + "-" + "decoration" + ":none;" + " " + " target =iframe_a > " + coursetitle + " </a>" + "</li>" + "</tr>";

                query = "update [videostableLms] set [TitleLink] ='" + hrf + "' , [VideoPath]= '" + link1 + "' ,[VideoTitle] = '" + videotitle.Text + "'  where Id= '" + Label6.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //Response.Write("Video Uploaded");
                Alert.Text = "Updated Successfully";
                //Session["alert"] = "Updated Successfully";
                //Session["bindval"] = DdlcourseName.SelectedItem.Value;
                //Response.Redirect("EditDeleteCourseVideos.aspx");
                gvVideoList.DataBind();

            }
            gvVideoList.DataBind();
            Response.Redirect("~/Admin/EditContent.aspx");
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
            //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Claim  Details  Updated ');", true);
            Console.Write(ex.Message);
        }
    }

    protected void documentedit_Click(object sender, EventArgs e)
    {
        String query, link, hrf;
        int i = 0;
        SqlConnection con = new SqlConnection(constr);
        try
        {
            if (FileUpload1.HasFile == false)
            {
                SqlDataAdapter filefetch = new SqlDataAdapter("select  ContentName,Data from PDFDocumentTable1 where  PDFId='" + Label9.Text + "'", con);
                DataTable dt = new DataTable();
                DataRow dr3;
                filefetch.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dr3 = dt.Rows[i];
                    link = Convert.ToString(dr3[0]);
                    hrf = Convert.ToString(dr3[1]);
                    query = "update [PDFDocumentTable1] set [ContentName] ='" + hrf + "' , [Data]= '" + link + "' ,[ContentTitle] = '" + documenttitle.Text + "'  where Id= '" + Label9.Text + "'";

                    SqlCommand cmd2 = new SqlCommand(query, con);
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    Alert.Text = "Course details updated successfully";
                    //DdlcourseType.ClearSelection();
                    //DdlcourseName.ClearSelection();
                    gvContentList.DataBind();

                }
            }
            else
            {
                //code ends here ...........
                string Document_title = documenttitle.Text;
                FileUpload1.SaveAs(Server.MapPath("~/CourseDocuments/") + Path.GetFileName(FileUpload1.FileName));
                string documentlink = "CourseDocuments/" + Path.GetFileName(FileUpload1.FileName);
                //string documentlink1 = "<video width=700px height=500px controls><Source src=" + link1 + " " + "type=video/mp4></video>";
                string hrf1 = "<tr>" + "<li>" + "<a href=" + documentlink + " " + "style =" + "text" + "-" + "decoration" + ":none;" + " " + " target =_blank > " + Document_title + " </a>" + "</li>" + "</tr>";

                query = "update [PDFDocumentTable1] set [DocumentTitle] ='" + Document_title + "' , [DocumentPath]= '" + documentlink + "' ,[DocumentTitleLink]= '" + hrf1 + "'  where Id= '" + Label9.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //Response.Write("Video Uploaded");
                Alert.Text = "Updated Successfully";
                //Session["alert"] = "Updated Successfully";
                //Session["bindval"] = DdlcourseName.SelectedItem.Value;
                //Response.Redirect("EditDeleteCourseVideos.aspx");
                gvContentList.DataBind();

            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
            //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Claim  Details  Updated ');", true);
            Console.Write(ex.Message);
        }

    }
}
