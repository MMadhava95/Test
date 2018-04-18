using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(ReportAssetType.SelectedIndex == 0)
        {
            ReportBy.Enabled = false;
            ReportContent.Enabled = false;
        }
        else if(ReportBy.SelectedIndex == 0)
        {

        }
    }

    protected void ReportAssetType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(Page.IsPostBack == true)
        {
            if (ReportAssetType.SelectedIndex!= 0)
            {
                ReportBy.Enabled = true;
                ReportContent.Enabled = true;
                if(ReportBy.SelectedIndex == 0)
                {
                    ReportContent.Enabled = false;
                }
            }
        }
    }
}