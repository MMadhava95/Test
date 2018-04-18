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
	public string cs = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
	protected void Page_Load(object sender, EventArgs e)
	{
		alert.Visible = false;
	}



	protected void submit_Click(object sender, EventArgs e)
	{
		SqlConnection con = new SqlConnection(cs);
		con.Open();
		HttpPostedFile postedFile = pic.PostedFile;
		string FileName = Path.GetFileName(postedFile.FileName);
		string FileExtension = Path.GetExtension(FileName);
		Stream Stream = postedFile.InputStream;
		BinaryReader binaryReader = new System.IO.BinaryReader(Stream);
		Byte[] picbytes = binaryReader.ReadBytes((int)Stream.Length);

		SqlCommand select2 = new SqlCommand
		("insert into Addemployee(EmployeeID,FirstName,MiddleName,LastName,Gender,DateofJoin,PersonalEmail,DateofBirth,Mobile,BloodGroup,EmergencyContact,AadharNumber,PancardNumber,Address,Landmark,Country,state,city,QualificationName,Yearofpassing,Marks,University,CollegeName,CompanyName,Designation,Location,Fromdate,Todate,LastDrawnSalary,ReportManager,Department,Role,Zone,Branch,CurrentSalary,Profilepic,PermenentAddress,PermenentLandmark,PermenentCountry,Permenentstate,Permenentcity) " +
  "Values" + "(@var1,@var2,@var3,@var4,@var5,@var6,@var7,@var8,@var9,@var10,@var11,@var12,@var13,@var14,@var15,@var16,@var17,@var18,@var19,@var20,@var21,@var22,@var23,@var24,@var25,@var26,@var27,@var28,@var29,@var30,@var31,@var32,@var33,@var34,@var35,@var36,@var37,@var38,@var39,@var40,@var41)", con);
		select2.Parameters.AddWithValue("@var1", empid.Text);
		select2.Parameters.AddWithValue("@var2", empfname.Text);
		select2.Parameters.AddWithValue("@var3", empmname.Text);
		select2.Parameters.AddWithValue("@var4", emplname.Text);
		select2.Parameters.AddWithValue("@var5", gender.SelectedValue);
		select2.Parameters.AddWithValue("@var6", doj.Text);
		select2.Parameters.AddWithValue("@var7", emppmailid.Text);
		select2.Parameters.AddWithValue("@var8", dob.Text);
		select2.Parameters.AddWithValue("@var9", emphone.Text);
		select2.Parameters.AddWithValue("@var10", "1");
		select2.Parameters.AddWithValue("@var11", emobile.Text);
		select2.Parameters.AddWithValue("@var12", Adharno.Text);
		select2.Parameters.AddWithValue("@var13", Panno.Text);
		select2.Parameters.AddWithValue("@var14",Address.Text );
		select2.Parameters.AddWithValue("@var15", Landmark.Text);
		select2.Parameters.AddWithValue("@var16", Country.Text);
		select2.Parameters.AddWithValue("@var17", state.Text);
		select2.Parameters.AddWithValue("@var18", City.Text);
		select2.Parameters.AddWithValue("@var19",Qname.Text );
		select2.Parameters.AddWithValue("@var20",year.Text );
		select2.Parameters.AddWithValue("@var21",marks.Text );
		select2.Parameters.AddWithValue("@var22",uni.Text );
		select2.Parameters.AddWithValue("@var23", Clz.Text);
		select2.Parameters.AddWithValue("@var24", Company.Text);
		select2.Parameters.AddWithValue("@var25", designation.Text);
		select2.Parameters.AddWithValue("@var26", location.Text);
		select2.Parameters.AddWithValue("@var27", from.Text);
		select2.Parameters.AddWithValue("@var28", to.Text);
		select2.Parameters.AddWithValue("@var29", lsalary.Text);
		select2.Parameters.AddWithValue("@var30", RM.Text);
		select2.Parameters.AddWithValue("@var31",dept.Text );
		select2.Parameters.AddWithValue("@var32", role.Text);
		select2.Parameters.AddWithValue("@var33", zone.Text);
		select2.Parameters.AddWithValue("@var34", branch.Text);
		select2.Parameters.AddWithValue("@var35", csalary.Text);
		select2.Parameters.Add("@var36", SqlDbType.VarBinary).Value = picbytes;
		select2.Parameters.AddWithValue("@var37", padd.Text );
		select2.Parameters.AddWithValue("@var38", pland.Text);
		select2.Parameters.AddWithValue("@var39", pcountry.Text);
		select2.Parameters.AddWithValue("@var40", pstate.Text);
		select2.Parameters.AddWithValue("@var41", pcity.Text);
		select2.ExecuteNonQuery();
		alertmod.Style.Add("background-color", "#d7ecc6");
		alert.Style.Add("background-color", "#d7ecc6");
		Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
		Label6.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
		Label5.Text = "Success!";
		Label6.Text = "New employee added";
		alert.Visible = true;
		con.Close();
		ClearControls();

	}
	protected void ClearControls()
	{
		empid.Text = string.Empty;
		empfname.Text = string.Empty;
		empmname.Text = string.Empty;
		emplname.Text = string.Empty;
		gender.Items.Clear();
		Bgroup.Items.Clear();
		doj.Text = string.Empty;
		dob.Text = string.Empty;
		emppmailid.Text = string.Empty;
		emphone.Text = string.Empty;
		emobile.Text = string.Empty;
		Adharno.Text = string.Empty;
		Panno.Text = string.Empty;
		Address.Text = string.Empty;
		Landmark.Text = String.Empty;
		Country.Text = string.Empty;
		state.Text = string.Empty;
		City.Text = string.Empty;
		padd.Text= string.Empty;
		pland.Text= string.Empty;
		pcountry.Text= string.Empty;
		pcity.Text= string.Empty;
		pstate.Text= string.Empty;
		Qname.Text= string.Empty;
		year.Text= string.Empty;
		marks.Text= string.Empty;
		uni.Text= string.Empty;
		Clz.Text= string.Empty;
		Company.Text= string.Empty;
		designation.Text= string.Empty;
		location.Text= string.Empty;
		from.Text= string.Empty; ;
		to.Text= string.Empty;
		lsalary.Text= string.Empty; 
		RM.Text= string.Empty; 
		dept.Text= string.Empty;
		role.Text= string.Empty; 
		zone.Text= string.Empty;
		branch.Text= string.Empty;
		csalary.Text= string.Empty;
		same.Checked = false;
	}

	protected void same_CheckedChanged(object sender, EventArgs e)
	{
		if (same.Checked == true)
		{
			  padd.Text= Address.Text;
			 pland.Text= Landmark.Text ;
			 pcountry.Text= Country.Text ;
			pstate.Text= state.Text ;
			 pcity.Text= City.Text ;
		}
		
	}

	protected void clear_Click(object sender, EventArgs e)
	{
		ClearControls();
	}

	protected void back_Click(object sender, EventArgs e)
	{
		Response.Redirect("ersEmployee.aspx");
	}
}