﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Igarashi
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Net;
//Igarashi
using Jisseki_Report_Ibaraki.Tools;


namespace Jisseki_Report_Ibaraki.member.search
{
    public partial class jisseki : System.Web.UI.Page
    {
        //接続文字列
        private String strConn;
        //Index for Gridview
        private const int GV_INDEX_担当 = 0;
        private const int GV_INDEX_日付 = 1;
        private const int GV_INDEX_YEAR = 4;
        private const int GV_INDEX_MONTH = 5;
        private const int GV_INDEX_DAY = 6;
        private const int GV_INDEX_YEAR_REP = 7;
        private const int GV_INDEX_MONTH_REP= 8;

        //検索クエリー用
       // private string qCOCODE,qYearRep,qMonthRep;

        private void searchReportData(string qCOCODE, string qYearRepFrom, string qMonthRepFrom, string qYearRepTo, string qMonthRepTo)
        {
            //初期表示
            string Sql =
                " SELECT * FROM [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Header]  "
                + " WHERE "
                + " COCODE=@COCODE  "
                /*
                + " AND YearRep = @YearRep "
                + " AND MonthRep = @MonthRep ";
                */

                +" AND ( YearRep >= @YearRepFrom AND MonthRep >= @MonthRepFrom) "
                +" AND ( YearRep <= @YearRepTo AND MonthRep <= @MonthRepTo) ";


            using (SqlConnection Conn = new SqlConnection(strConn))
            {
                Conn.Open();
                using (SqlCommand cmd = new SqlCommand(Sql, Conn))
                {

                    cmd.Parameters.Add(new SqlParameter("@COCODE", qCOCODE));
                    cmd.Parameters.Add(new SqlParameter("@YearRepFrom", qYearRepFrom));
                    cmd.Parameters.Add(new SqlParameter("@MonthRepFrom", qMonthRepFrom));
                    cmd.Parameters.Add(new SqlParameter("@YearRepTo", qYearRepTo));
                    cmd.Parameters.Add(new SqlParameter("@MonthRepTo", qMonthRepTo));

                    //送信日
                    Gridview1.Columns[GV_INDEX_YEAR].Visible = true;
                    Gridview1.Columns[GV_INDEX_MONTH].Visible = true;
                    Gridview1.Columns[GV_INDEX_DAY].Visible = true;
                    //報告台数提出日
                    Gridview1.Columns[GV_INDEX_YEAR_REP].Visible = true;
                    Gridview1.Columns[GV_INDEX_MONTH_REP].Visible = true;


                    using (SqlDataAdapter Adapter = new SqlDataAdapter(Sql, Conn))
                    {
                        Adapter.SelectCommand = cmd;
                        DataTable header = new DataTable("新車台数ヘッダー");
                        Adapter.Fill(header);

                        if (header.Rows.Count == 0)
                        {
                            //Gridview1.EmptyDataText = "検索データはありません。";
                            Gridview1.DataSource = header;
                            Gridview1.DataBind();
                            lblMsg.Text = "検索データはありません。";
                        }
                        else
                        {
                            Gridview1.DataSource = header;
                            Gridview1.DataBind();
                        }
                    }

                    
                    string wEra;
                    string wYear;
                    string wDate;
                    JapaneseCalendar jCalender = new JapaneseCalendar();
                    for (int i = 0; i < Gridview1.Rows.Count; i++)
                    {
                        //Covert Christian Era To Japanese Era
                        DateTime JapaneseDate = DateTime.Parse(Gridview1.Rows[i].Cells[GV_INDEX_YEAR].Text + "/" + Gridview1.Rows[i].Cells[GV_INDEX_MONTH].Text + "/" + Gridview1.Rows[i].Cells[GV_INDEX_DAY].Text);

                        wEra = Utility.getJapaneseEra(jCalender.GetEra(JapaneseDate));
                        wYear = jCalender.GetYear(JapaneseDate).ToString();
                        wDate = wEra + wYear + "年" + Gridview1.Rows[i].Cells[GV_INDEX_MONTH].Text + "月" + Gridview1.Rows[i].Cells[GV_INDEX_DAY].Text + "日";
                        Gridview1.Rows[i].Cells[GV_INDEX_日付].Text = wDate;


                    }
                    //送信日
                    Gridview1.Columns[GV_INDEX_YEAR].Visible = false;
                    Gridview1.Columns[GV_INDEX_MONTH].Visible = false;
                    Gridview1.Columns[GV_INDEX_DAY].Visible = false;
                    //報告台数提出日
                    Gridview1.Columns[GV_INDEX_YEAR_REP].Visible = false;
                    Gridview1.Columns[GV_INDEX_MONTH_REP].Visible = false;
                }
                Conn.Close();
            }



        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            //接続文字列
            strConn = ConfigurationManager.ConnectionStrings["JissekiConnectionString"].ConnectionString;
            
            if(Page.IsPostBack){
            
            }
            else
            {
                //初期表示は報告台数の報告年
                string strSql =
                    " SELECT *  FROM [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Header] "
                    + "WHERE COCODE=@COCODE AND YearRep = @YearRep ";

                using (SqlConnection Conn = new SqlConnection(strConn))
                {
                    Conn.Open();
                    SqlCommand cmd = new SqlCommand(strSql, Conn);
                    cmd.Parameters.Add(new SqlParameter("@COCODE", Session["COCODE"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@YearRep", DateTime.Today.Year.ToString()));

                    using (SqlDataAdapter Adapter = new SqlDataAdapter(strSql, Conn))
                    {
                        Adapter.SelectCommand = cmd;
                        DataTable header = new DataTable("新車台数ヘッダー");
                        Adapter.Fill(header);


                        Gridview1.DataSource = header;
                        Gridview1.DataBind();


                        string wEra;
                        string wYear;
                        string wDate;
                        JapaneseCalendar jCalender = new JapaneseCalendar();
                        for (int i = 0; i < Gridview1.Rows.Count; i++)
                        {
                            //Covert Christian Era To Japanese Era
                            DateTime JapaneseDate = DateTime.Parse(Gridview1.Rows[i].Cells[GV_INDEX_YEAR].Text + "/" + Gridview1.Rows[i].Cells[GV_INDEX_MONTH].Text + "/" + Gridview1.Rows[i].Cells[GV_INDEX_DAY].Text);

                            wEra = Utility.getJapaneseEra(jCalender.GetEra(JapaneseDate));
                            wYear = jCalender.GetYear(JapaneseDate).ToString();
                            wDate = wEra + wYear + "年" + Gridview1.Rows[i].Cells[GV_INDEX_MONTH].Text + "月" + Gridview1.Rows[i].Cells[GV_INDEX_DAY].Text + "日";
                            Gridview1.Rows[i].Cells[GV_INDEX_日付].Text = wDate;


                        }
                        //送信日
                        Gridview1.Columns[GV_INDEX_YEAR].Visible = false;
                        Gridview1.Columns[GV_INDEX_MONTH].Visible = false;
                        Gridview1.Columns[GV_INDEX_DAY].Visible = false;
                        //報告台数提出日
                        Gridview1.Columns[GV_INDEX_YEAR_REP].Visible = false;
                        Gridview1.Columns[GV_INDEX_MONTH_REP].Visible = false;

                    }
            }
           
            }
        }

       
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.searchReportData(Session["COCODE"].ToString(),
                Utility.HeiseiToChristianEra(this.txtYearRepFrom.Text), this.txtMonthRepFrom.Text,
                Utility.HeiseiToChristianEra(this.txtYearRepTo.Text), this.txtMonthRepTo.Text);
        }

        protected void Gridview1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int row = Gridview1.SelectedIndex;



        }

    }
}