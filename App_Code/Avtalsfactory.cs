﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;

/// <summary>
/// Summary description for Avtalsfactory
/// </summary>
public static class Avtalsfactory
{
    // public const string Fields = "id, diarienummer, startdate, enddate, status, motpartstyp, SBKavtalsid, scan_url, orgnummer, enligt_avtal, internt_alias, kommentar";

    public static List<Person> GetPersons(NpgsqlDataReader reader)
    {
        var lst = new List<Person>();

        while (reader.Read())
        {
            lst.Add(new Person
            {
                id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Belagenhetsadress = reader.GetString(3),
                Postnummer = reader.GetString(4),
                Postort = reader.GetString(5),
                Telefonnummer = reader.GetString(6),
                epost = reader.GetString(7)
            });
        }
        return lst;
    }

    public static List<Person> GetNamesAndId(NpgsqlDataReader reader)
    {
        var lst = new List<Person>();

        while (reader.Read())
        {
            lst.Add(new Person
            {
                id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
            });
        }
        return lst;
    }

    public static List<Avtalsmodel> ParseAvtal(NpgsqlDataReader reader)
    {
        var lst = new List<Avtalsmodel>();

        while (reader.Read())
        {
            string diarienr;
            if (reader.GetValue(1) != DBNull.Value)
            {
                diarienr = reader.GetString(1);
            }
            else
            {
                diarienr = "";
            }

            DateTime? sd;
            if (reader.GetValue(2) != DBNull.Value)
            {
                sd = reader.GetDateTime(2);
            }
            else
            {
                sd = null;
            }

            DateTime? ed;
            if (reader.GetValue(3) != DBNull.Value)
            {
                ed = reader.GetDateTime(3);
            }
            else
            {
                ed = null;
            }

            //string mptyp;
            //if (reader.GetString(5) != DBNull.Value)
            //{
            //    mptyp = reader.GetString(5);
            //}
            //else
            //{
            //    mptyp = "";
            //}

            long dbid;
            if (reader.GetValue(0) != DBNull.Value)
            {
                dbid = reader.GetInt32(0);
            }
            else
            {
                dbid = -1;
            }

            lst.Add(new Avtalsmodel
            {
                id = dbid, // reader.GetInt32(0),
                diarienummer = diarienr,
                startdate = sd,
                enddate = ed,
                status = reader.GetString(4),
                motpartstyp = reader.GetString(5),
                sbkid = reader.GetInt32(6),
                scan_url = reader.GetString(7),
                orgnummer = reader.GetString(8),
                enligtAvtal = reader.GetString(9),
                interntAlias = reader.GetString(10),
                kommentar = reader.GetString(11)
            });
        }

        return lst;
    }
}