﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Npgsql;
using System.Data;

public partial class avtalsgrid : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var ds = new DataSet();
        var dt = new DataTable();
        
        string connstr = ConfigurationManager.ConnectionStrings["postgres connection"].ConnectionString;
        using (var conn = new NpgsqlConnection(connstr))
        {
            var sqlquery = "select id, enligt_avtal, diarienummer, startdate, enddate, datakontakt from sbk_avtal.avtal;";
            using (var da = new NpgsqlDataAdapter(sqlquery, conn))
            {
                da.Fill(ds);
            }

        }
        dt = ds.Tables[0];

      

        AvtalTable.DataSource = dt;

        //AvtalTable.DataKeys = "ID";

        AvtalTable.DataBind();

        Session.Add("AvtalTableDataSource", dt);
    }

    protected void AvtalTable_DataBound(object sender, GridViewRowEventArgs e) 
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var id = DataBinder.Eval(e.Row.DataItem, "id");
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.ToolTip = "Redigera avtal";
            e.Row.Attributes["onClick"] = "location.href='./avtal_detail.aspx?id=" + id.ToString() + "'";  //String.Format("location.href={0}'", link);
        }
    }

    protected void AvtalTable_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = Session["AvtalTableDataSource"] as DataTable;

        if (dt != null)
        {
            var dv = dt.DefaultView;
            dv.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
            // dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
            // AvtalTable.DataSource = Session["AvtalTableDataSource"];
            var newTable = dv.ToTable();
            AvtalTable.DataSource = newTable;
            Session.Add("AvtalTableDataSource", newTable);
            
            AvtalTable.DataBind();
                
        }
    }

    private string GetSortDirection(string column)
    {

        // By default, set the sort direction to ascending.
        string sortDirection = "ASC";

        // Retrieve the last column that was sorted.
        string sortExpression = ViewState["SortExpression"] as string;

        if (sortExpression != null)
        {
            // Check if the same column is being sorted.
            // Otherwise, the default value can be returned.
            if (sortExpression == column)
            {
                string lastDirection = ViewState["SortDirection"] as string;
                if ((lastDirection != null) && (lastDirection == "ASC"))
                {
                    sortDirection = "DESC";
                }
            }
        }

        // Save new values in ViewState.
        ViewState["SortDirection"] = sortDirection;
        ViewState["SortExpression"] = column;

        return sortDirection;
    }
}