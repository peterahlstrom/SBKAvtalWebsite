using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //create Roles


        string user = (Session["user"] != null) ? Session["user"].ToString() :  "inte inloggad";
        Session["user"] = user;
        debug.Text = Session["user"].ToString();
    }
}