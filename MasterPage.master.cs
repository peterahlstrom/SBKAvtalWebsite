using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string user = (Session["user"] != null) ? Session["user"].ToString() : "inte inloggad";
        Session["user"] = user;
        //debug.Text = Session["user"].ToString();
        DateTime today = DateTime.Today;
        //debug.Text = today.AddDays(16).ToShortDateString();

        SetEditMenu();
    }

    protected void SetEditMenu()
    {
        if (Session["user"] == "inte inloggad")
        {
            editMenu.Attributes.Add("class", "disabled");
            editMenuToggle.Attributes.Add("class", "disabled");
            loginLabel.InnerHtml = "Logga in";

        }
        else
        {
            editMenu.Attributes.Remove("disabled");
            editMenuToggle.Attributes.Remove("disabled");
            loginLabel.InnerHtml = "Logga ut";
            //loginButton.
        }
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        if (Session["user"] == "inte inloggad")
        {
            Response.Redirect("./Login.aspx");
        }
        else
        {
            Session["user"] = "inte inloggad";
        }

        SetEditMenu();
    }

}
