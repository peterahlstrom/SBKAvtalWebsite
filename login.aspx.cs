using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;
using Npgsql;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        

        string connstr = ConfigurationManager.ConnectionStrings["postgres connection"].ConnectionString;
        bool valid;
        using (var conn = new NpgsqlConnection(connstr))
        {
            conn.Open();
            var personinsertquery = "select sbk_avtal.validatelogin(@user, @pwd);";
            using (var cmd = new NpgsqlCommand(personinsertquery, conn))
            {
                cmd.Parameters.Add(new NpgsqlParameter("user", Login1.UserName));
                cmd.Parameters.Add(new NpgsqlParameter("pwd", Login1.Password));

                valid = (bool)cmd.ExecuteScalar();
                e.Authenticated = valid;
                Session["loggedin"] = valid;
                
                if (valid)
                {
                    Session["user"] = Login1.UserName;
                }
                else
                {
                    Session["user"] = null;
                }
                

                //Response.Redirect(Request.UrlReferrer.ToString());
                //cmd.ExecuteNonQuery();
            }
        }


        debug.Text = (Session["loggedin"].ToString() == "True") ? "Inloggad!" : "Inte inloggad!";

    }
}