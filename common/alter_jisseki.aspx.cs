/*
 *会員、自販連　 共通
 *
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Igarashi
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Globalization;
using System.Net;
using Jisseki_Report_Ibaraki.Tools;

namespace Jisseki_Report_Ibaraki.common
{
    public partial class alter_jisseki : System.Web.UI.Page
    {
        //メンバ
        //接続文字列
        private String strConn;

        private bool jadaUser;


        private String qCOCODE;
        private String qYearRep;
        private String qMonthRep;


        private void setHeaderForJada() {
            string SqlHeader =
                " SELECT H.COCODE AS COCODE, "
                + "      H.Year AS Year ,"
                + "      H.Month AS Month ,"
                + "      H.Day AS Day ,"
                + "      H.YearRep AS YearRep ,"
                + "      H.MonthRep AS MonthRep ,"
                + "      H.TANTOU AS TANTOU  ,"
                + "      I.CONAME AS CONAME  "
                + " FROM [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Header] H "
                + " INNER JOIN  [Jisseki_Report_Ibaraki].[dbo].[ID] I"
                + " ON H.COCODE = I.COCODE "
                + " WHERE H.COCODE=@COCODE AND H.YearRep = @YearRep AND H.MonthRep = @MonthRep ";//TODO

            using (SqlConnection Conn = new SqlConnection(strConn))
            {
                Conn.Open();
                using (SqlCommand cmd = new SqlCommand(SqlHeader, Conn))
                {

                    cmd.Parameters.Add(new SqlParameter("@COCODE", qCOCODE));
                    cmd.Parameters.Add(new SqlParameter("@YearRep", qYearRep));
                    cmd.Parameters.Add(new SqlParameter("@MonthRep", qMonthRep));

                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        Reader.Read();
                        //Convert Christian Era To  Japanese Era
                        DateTime JapaneseDate = DateTime.Parse(
                                                Reader["Year"].ToString()
                                                + "/" + Reader["Month"].ToString()
                                                + "/" + Reader["Day"].ToString()
                                               );
                        JapaneseCalendar jCalender = new JapaneseCalendar();
                        this.lblEra.Text = Utility.getJapaneseEra(jCalender.GetEra(JapaneseDate));
                        this.lblEraRep0.Text = Utility.getJapaneseEra(jCalender.GetEra(JapaneseDate));
                        this.lblEraRep1.Text = Utility.getJapaneseEra(jCalender.GetEra(JapaneseDate));
                        this.txtYear.Text = jCalender.GetYear(JapaneseDate).ToString();
                        this.txtYearRep0.Text = jCalender.GetYear(JapaneseDate).ToString();
                        this.txtYearRep1.Text = jCalender.GetYear(JapaneseDate).ToString();

                        this.txtMonth.Text = Reader["Month"].ToString();
                        this.txtMonthRep0.Text = Reader["MonthRep"].ToString();
                        this.txtMonthRep1.Text = Reader["MonthRep"].ToString();

                        this.txtDay.Text = Reader["Day"].ToString();
                        this.txtSyamei.Text = Reader["CONAME"].ToString();
                        this.txtTantou.Text = Reader["TANTOU"].ToString();

                    }
                }
                Conn.Close();
            }

        }

            
        /// <summary>
        /// setHeader
        /// </summary>
        private void setHeader(){
            //初期表示


            string SqlHeader =
                " SELECT * FROM [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Header]  "
                +" WHERE COCODE=@COCODE AND YearRep = @YearRep AND MonthRep = @MonthRep ";//TODO

            using (SqlConnection Conn = new SqlConnection(strConn))
            {
                Conn.Open();
                using (SqlCommand cmd = new SqlCommand(SqlHeader, Conn))
                {

                    cmd.Parameters.Add(new SqlParameter("@COCODE", qCOCODE));
                    cmd.Parameters.Add(new SqlParameter("@YearRep", qYearRep));
                    cmd.Parameters.Add(new SqlParameter("@MonthRep", qMonthRep));

                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        Reader.Read();
                        //Convert Christian Era To  Japanese Era
                        DateTime JapaneseDate = DateTime.Parse(
                                                Reader["Year"].ToString()
                                                + "/" + Reader["Month"].ToString()
                                                + "/" + Reader["Day"].ToString()
                                               );
                        JapaneseCalendar jCalender = new JapaneseCalendar();
                        this.lblEra.Text = Utility.getJapaneseEra(jCalender.GetEra(JapaneseDate));
                        this.lblEraRep0.Text = Utility.getJapaneseEra(jCalender.GetEra(JapaneseDate));
                        this.lblEraRep1.Text = Utility.getJapaneseEra(jCalender.GetEra(JapaneseDate));
                        this.txtYear.Text = jCalender.GetYear(JapaneseDate).ToString();
                        this.txtYearRep0.Text = jCalender.GetYear(JapaneseDate).ToString();
                        this.txtYearRep1.Text = jCalender.GetYear(JapaneseDate).ToString();
                        
                        this.txtMonth.Text = Reader["Month"].ToString();
                        this.txtMonthRep0.Text = Reader["MonthRep"].ToString();
                        this.txtMonthRep1.Text = Reader["MonthRep"].ToString();

                        this.txtDay.Text = Reader["Day"].ToString();
                      　this.txtSyamei.Text = Session["CONAME"].ToString();//TODO 自販連からきたときと別
                        

                        this.txtTantou.Text = Reader["TANTOU"].ToString();

                    }
                }
                Conn.Close();
            }
        }



        /// <summary>
        /// setMitoData
        /// </summary>
        private void setMito() {
            string SqlMito =
                    " SELECT * FROM [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Mito]  "
                    //+ " WHERE COCODE=@COCODE AND Year = @Year AND Month = @Month AND Day = @Day";
                    +" WHERE COCODE=@COCODE AND YearRep = @YearRep AND MonthRep = @MonthRep ";
            using (SqlConnection Conn = new SqlConnection(strConn))
            {
                Conn.Open();
                using (SqlCommand cmd = new SqlCommand(SqlMito, Conn))
                {
                    
                    cmd.Parameters.Add(new SqlParameter("@COCODE", qCOCODE));
                    cmd.Parameters.Add(new SqlParameter("@YearRep", qYearRep));
                    cmd.Parameters.Add(new SqlParameter("@MonthRep", qMonthRep));
                    
                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        Reader.Read();
                        this.txtMito_Kamotu1.Text   = Utility.zeroToSpace(Reader["Kamotu1"].ToString());
                        this.txtMito_Kamotu2.Text   = Utility.zeroToSpace(Reader["Kamotu2"].ToString());
                        this.txtMito_Kamotu3.Text   = Utility.zeroToSpace(Reader["Kamotu3"].ToString());
                        this.txtMito_Kamotu4.Text   = Utility.zeroToSpace(Reader["Kamotu4"].ToString());
                        this.txtMito_Bus1.Text      = Utility.zeroToSpace(Reader["Bus1"].ToString());
                        this.txtMito_Bus2.Text      = Utility.zeroToSpace(Reader["Bus2"].ToString());
                        this.txtMito_JK_J1.Text     = Utility.zeroToSpace(Reader["JK_J1"].ToString());
                        this.txtMito_JK_K1.Text     = Utility.zeroToSpace(Reader["JK_K1"].ToString());
                        this.txtMito_JK_J2.Text     = Utility.zeroToSpace(Reader["JK_J2"].ToString());
                        this.txtMito_JK_K2.Text     = Utility.zeroToSpace(Reader["JK_K2"].ToString());
                        this.txtMito_JK_J3.Text     = Utility.zeroToSpace(Reader["JK_J3"].ToString());
                        this.txtMito_JK_K3.Text     = Utility.zeroToSpace(Reader["JK_K3"].ToString());
                        this.txtMito_SubTotal1.Text = Utility.zeroToSpace(Reader["SubTotal1"].ToString());
                        this.txtMito_Total1.Text    = Utility.zeroToSpace(Reader["Total1"].ToString());


                    }
                }
                Conn.Close();
            }
        
        }

        /// <summary>
        /// setTuchiuraData
        /// </summary>
        private void setTuchiura()
        {
            string SqlTuchiura =
                    " SELECT * FROM [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Tuchiura]  "
                    + " WHERE COCODE=@COCODE AND YearRep = @YearRep AND MonthRep = @MonthRep ";
            using (SqlConnection Conn = new SqlConnection(strConn))
            {
                Conn.Open();
                using (SqlCommand cmd = new SqlCommand(SqlTuchiura, Conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@COCODE", qCOCODE));
                    cmd.Parameters.Add(new SqlParameter("@YearRep", qYearRep));
                    cmd.Parameters.Add(new SqlParameter("@MonthRep", qMonthRep));

                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        Reader.Read();
                        this.txtTuchiura_Kamotu1.Text = Utility.zeroToSpace(Reader["Kamotu1"].ToString());
                        this.txtTuchiura_Kamotu2.Text = Utility.zeroToSpace(Reader["Kamotu2"].ToString());
                        this.txtTuchiura_Kamotu3.Text = Utility.zeroToSpace(Reader["Kamotu3"].ToString());
                        this.txtTuchiura_Kamotu4.Text = Utility.zeroToSpace(Reader["Kamotu4"].ToString());
                        this.txtTuchiura_Bus1.Text = Utility.zeroToSpace(Reader["Bus1"].ToString());
                        this.txtTuchiura_Bus2.Text = Utility.zeroToSpace(Reader["Bus2"].ToString());
                        this.txtTuchiura_JK_J1.Text = Utility.zeroToSpace(Reader["JK_J1"].ToString());
                        this.txtTuchiura_JK_K1.Text = Utility.zeroToSpace(Reader["JK_K1"].ToString());
                        this.txtTuchiura_JK_J2.Text = Utility.zeroToSpace(Reader["JK_J2"].ToString());
                        this.txtTuchiura_JK_K2.Text = Utility.zeroToSpace(Reader["JK_K2"].ToString());
                        this.txtTuchiura_JK_J3.Text = Utility.zeroToSpace(Reader["JK_J3"].ToString());
                        this.txtTuchiura_JK_K3.Text = Utility.zeroToSpace(Reader["JK_K3"].ToString());
                        this.txtTuchiura_SubTotal1.Text = Utility.zeroToSpace(Reader["SubTotal1"].ToString());
                        this.txtTuchiura_Total1.Text = Utility.zeroToSpace(Reader["Total1"].ToString());


                    }
                }
                Conn.Close();
            }

        }

        /// <summary>
        /// setTukubaData
        /// </summary>
        private void setTukuba()
        {
            string SqlTukuba =
                    " SELECT * FROM [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Tukuba]  "
                    +" WHERE COCODE=@COCODE AND YearRep = @YearRep AND MonthRep = @MonthRep ";
            using (SqlConnection Conn = new SqlConnection(strConn))
            {
                Conn.Open();
                using (SqlCommand cmd = new SqlCommand(SqlTukuba, Conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@COCODE", qCOCODE));
                    cmd.Parameters.Add(new SqlParameter("@YearRep", qYearRep));
                    cmd.Parameters.Add(new SqlParameter("@MonthRep", qMonthRep));

                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        Reader.Read();
                        this.txtTukuba_Kamotu1.Text = Utility.zeroToSpace(Reader["Kamotu1"].ToString());
                        this.txtTukuba_Kamotu2.Text = Utility.zeroToSpace(Reader["Kamotu2"].ToString());
                        this.txtTukuba_Kamotu3.Text = Utility.zeroToSpace(Reader["Kamotu3"].ToString());
                        this.txtTukuba_Kamotu4.Text = Utility.zeroToSpace(Reader["Kamotu4"].ToString());
                        this.txtTukuba_Bus1.Text = Utility.zeroToSpace(Reader["Bus1"].ToString());
                        this.txtTukuba_Bus2.Text = Utility.zeroToSpace(Reader["Bus2"].ToString());
                        this.txtTukuba_JK_J1.Text = Utility.zeroToSpace(Reader["JK_J1"].ToString());
                        this.txtTukuba_JK_K1.Text = Utility.zeroToSpace(Reader["JK_K1"].ToString());
                        this.txtTukuba_JK_J2.Text = Utility.zeroToSpace(Reader["JK_J2"].ToString());
                        this.txtTukuba_JK_K2.Text = Utility.zeroToSpace(Reader["JK_K2"].ToString());
                        this.txtTukuba_JK_J3.Text = Utility.zeroToSpace(Reader["JK_J3"].ToString());
                        this.txtTukuba_JK_K3.Text = Utility.zeroToSpace(Reader["JK_K3"].ToString());
                        this.txtTukuba_SubTotal1.Text = Utility.zeroToSpace(Reader["SubTotal1"].ToString());
                        this.txtTukuba_Total1.Text = Utility.zeroToSpace(Reader["Total1"].ToString());


                    }
                }
                Conn.Close();
            }

        }

        /// <summary>
        /// setSonotaData
        /// </summary>
        private void setSonota()
        {
            string SqlSonota =
                    " SELECT * FROM [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Sonota]  "
                    +" WHERE COCODE=@COCODE AND YearRep = @YearRep AND MonthRep = @MonthRep ";
            using (SqlConnection Conn = new SqlConnection(strConn))
            {
                Conn.Open();
                using (SqlCommand cmd = new SqlCommand(SqlSonota, Conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@COCODE", qCOCODE));
                    cmd.Parameters.Add(new SqlParameter("@YearRep", qYearRep));
                    cmd.Parameters.Add(new SqlParameter("@MonthRep", qMonthRep));

                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        Reader.Read();
                        this.txtSonota_Kamotu1.Text = Utility.zeroToSpace(Reader["Kamotu1"].ToString());
                        this.txtSonota_Kamotu2.Text = Utility.zeroToSpace(Reader["Kamotu2"].ToString());
                        this.txtSonota_Kamotu3.Text = Utility.zeroToSpace(Reader["Kamotu3"].ToString());
                        this.txtSonota_Kamotu4.Text = Utility.zeroToSpace(Reader["Kamotu4"].ToString());
                        this.txtSonota_Bus1.Text = Utility.zeroToSpace(Reader["Bus1"].ToString());
                        this.txtSonota_Bus2.Text = Utility.zeroToSpace(Reader["Bus2"].ToString());
                        this.txtSonota_JK_J1.Text = Utility.zeroToSpace(Reader["JK_J1"].ToString());
                        this.txtSonota_JK_K1.Text = Utility.zeroToSpace(Reader["JK_K1"].ToString());
                        this.txtSonota_JK_J2.Text = Utility.zeroToSpace(Reader["JK_J2"].ToString());
                        this.txtSonota_JK_K2.Text = Utility.zeroToSpace(Reader["JK_K2"].ToString());
                        this.txtSonota_JK_J3.Text = Utility.zeroToSpace(Reader["JK_J3"].ToString());
                        this.txtSonota_JK_K3.Text = Utility.zeroToSpace(Reader["JK_K3"].ToString());
                        this.txtSonota_SubTotal1.Text = Utility.zeroToSpace(Reader["SubTotal1"].ToString());
                        this.txtSonota_Total1.Text = Utility.zeroToSpace(Reader["Total1"].ToString());


                    }
                }
                Conn.Close();
            }

        }

        /// <summary>
        /// setGoukeiData
        /// </summary>
        private void setGoukei()
        {
            string SqlGoukei =
                    " SELECT * FROM [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Goukei]  "
                    +" WHERE COCODE=@COCODE AND YearRep = @YearRep AND MonthRep = @MonthRep ";
            using (SqlConnection Conn = new SqlConnection(strConn))
            {
                Conn.Open();
                using (SqlCommand cmd = new SqlCommand(SqlGoukei, Conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@COCODE", qCOCODE));
                    cmd.Parameters.Add(new SqlParameter("@YearRep", qYearRep));
                    cmd.Parameters.Add(new SqlParameter("@MonthRep", qMonthRep));
                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        Reader.Read();
                        this.txtGoukei_Kamotu1.Text     = Utility.zeroToSpace(Reader["Kamotu1"].ToString());
                        this.txtGoukei_Kamotu2.Text     = Utility.zeroToSpace(Reader["Kamotu2"].ToString());
                        this.txtGoukei_Kamotu3.Text     = Utility.zeroToSpace(Reader["Kamotu3"].ToString());
                        this.txtGoukei_Kamotu4.Text     = Utility.zeroToSpace(Reader["Kamotu4"].ToString());
                        this.txtGoukei_Bus1.Text        = Utility.zeroToSpace(Reader["Bus1"].ToString());
                        this.txtGoukei_Bus2.Text        = Utility.zeroToSpace(Reader["Bus2"].ToString());
                        this.txtGoukei_JK_J1.Text       = Utility.zeroToSpace(Reader["JK_J1"].ToString());
                        this.txtGoukei_JK_K1.Text       = Utility.zeroToSpace(Reader["JK_K1"].ToString());
                        this.txtGoukei_JK_J2.Text       = Utility.zeroToSpace(Reader["JK_J2"].ToString());
                        this.txtGoukei_JK_K2.Text       = Utility.zeroToSpace(Reader["JK_K2"].ToString());
                        this.txtGoukei_JK_J3.Text       = Utility.zeroToSpace(Reader["JK_J3"].ToString());
                        this.txtGoukei_JK_K3.Text = Utility.zeroToSpace(Reader["JK_K3"].ToString());
                        this.txtGoukei_SubTotal1.Text   = Utility.zeroToSpace(Reader["SubTotal1"].ToString());
                        this.txtGoukei_Total1.Text      = Utility.zeroToSpace(Reader["Total1"].ToString());


                    }
                }
                Conn.Close();
            }

        }

        /// <summary>
        /// InitializeForm
        /// </summary>
        private void initializeForm() {
            if (jadaUser)
            {
                this.setHeaderForJada();
            }
            else
            {
                this.setHeader();
            }
            this.setMito();
            this.setTuchiura();
            this.setTukuba();
            this.setSonota();
            this.setGoukei();

            //Key項目
            //EnableをFalse
            this.txtYearRep0.Enabled = false;
            this.txtYearRep1.Enabled = false;
            this.txtMonthRep0.Enabled = false;
            this.txtMonthRep1.Enabled = false;
            //メニューを会員、自販連で分ける
            if (jadaUser)
            {
                linkMenu.NavigateUrl = URL.MENU_JADA;
            }
            else
            {
                linkMenu.NavigateUrl = URL.MENU_DEALER;
            }

        }


        private void updateHeader(SqlConnection Conn, SqlTransaction Tran)
        {

            String UpdateMitoSql = "UPDATE [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Header] "
                                    + " SET "
                                    + "    [Year]   = @Year"
                                    + "   ,[Month]  = @Month "
                                    + "   ,[Day]    = @Day "
                                    + "   ,[COCODE] = @COCODE "
                                    + "   ,[TANTOU] = @TANTOU "
                                    + " WHERE COCODE = @COCODE AND Year = @Year AND Month = @Month AND Day = @Day";


            //Sqlコネクション
            try
            {
                SqlCommand cmd = new SqlCommand(UpdateMitoSql, Conn, Tran);
                cmd.CommandText = UpdateMitoSql;
                //Sqlインジェクション回避
                //キー項目
                cmd.Parameters.Add(new SqlParameter("@COCODE", Session["COCODE"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@Year", Utility.HeiseiToChristianEra(txtYear.Text)));
                cmd.Parameters.Add(new SqlParameter("@Month", txtMonth.Text));
                cmd.Parameters.Add(new SqlParameter("@Day", txtDay.Text));
                cmd.Parameters.Add(new SqlParameter("@TANTOU", txtTantou.Text));
                cmd.ExecuteNonQuery();

            }
            catch
            {
                throw;

            }

        }

        /// <summary>
        /// Update Mito's Data
        /// </summary>
        private void updateMito(SqlConnection Conn,SqlTransaction Tran){
            
            //INSERT作成
            //SqlConnection sn = new ;
            String updateMitoSql = "UPDATE [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Mito] "
                                 + " SET"
                                 + "  [Year]      = @Year "
                                 + " ,[Month]     = @Month "
                                 + " ,[Day]       = @Day "
                                 + " ,[COCODE]    = @COCODE "
                                 + " ,[Kamotu1]   = @Kamotu1 "
                                 + " ,[Kamotu2]   = @Kamotu2 "
                                 + " ,[Kamotu3]   = @Kamotu3 "
                                 + " ,[Kamotu4]   = @Kamotu4 "
                                 + " ,[Bus1]      = @Bus1  "
                                 + " ,[Bus2]      = @Bus2 "
                                 + " ,[JK_J1]     = @JK_J1 "
                                 + " ,[JK_K1]     = @JK_K1 "
                                 + " ,[JK_J2]     = @JK_J2 "
                                 + " ,[JK_K2]     = @JK_K2 "
                                 + " ,[JK_J3]     = @JK_J3 "
                                 + " ,[JK_K3]     = @JK_K3 "
                                 + " ,[SubTotal1] = @SubTotal1 "
                                 + " ,[Total1]    = @Total1 "
                                 + " WHERE COCODE = @COCODE AND Year = @Year AND Month = @Month AND Day = @Day";
                                 
                           
            //Sqlコネクション
            try
            {
                SqlCommand cmd = new SqlCommand(updateMitoSql, Conn, Tran);
                cmd.CommandText = updateMitoSql;
                //Sqlインジェクション回避
                //キー項目
                cmd.Parameters.Add(new SqlParameter("@COCODE", Session["COCODE"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@Year", Utility.HeiseiToChristianEra(txtYear.Text)));
                cmd.Parameters.Add(new SqlParameter("@Month", txtMonth.Text));
                cmd.Parameters.Add(new SqlParameter("@Day", txtDay.Text));


                //貨物
                cmd.Parameters.Add(new SqlParameter("@Kamotu1", txtMito_Kamotu1.Text));
                cmd.Parameters.Add(new SqlParameter("@Kamotu2", txtMito_Kamotu2.Text));
                cmd.Parameters.Add(new SqlParameter("@Kamotu3", txtMito_Kamotu3.Text));
                cmd.Parameters.Add(new SqlParameter("@Kamotu4", txtMito_Kamotu4.Text));
                //バス
                cmd.Parameters.Add(new SqlParameter("@Bus1", txtMito_Bus1.Text));
                cmd.Parameters.Add(new SqlParameter("@Bus2", txtMito_Bus2.Text));
                //乗用　貨物
                cmd.Parameters.Add(new SqlParameter("@JK_J1", txtMito_JK_J1.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_K1", txtMito_JK_K1.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_J2", txtMito_JK_J2.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_K2", txtMito_JK_K2.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_J3", txtMito_JK_J3.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_K3", txtMito_JK_K3.Text));
                cmd.Parameters.Add(new SqlParameter("@SubTotal1", txtMito_SubTotal1.Text));
                cmd.Parameters.Add(new SqlParameter("@Total1", txtMito_Total1.Text));
                cmd.ExecuteNonQuery();

            }
            catch 
            {
                throw ;

            }
        
        }


        /// <summary>
        /// Update Tuchiura's Data
        /// </summary>
        private void updateTuchiura(SqlConnection Conn, SqlTransaction Tran)
        {

            //INSERT作成
            //SqlConnection sn = new ;
            String updateTuchiuraSql = "UPDATE [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Tuchiura] "
                                 + " SET"
                                 + "  [Year]      = @Year "
                                 + " ,[Month]     = @Month "
                                 + " ,[Day]       = @Day "
                                 + " ,[COCODE]    = @COCODE "
                                 + " ,[Kamotu1]   = @Kamotu1 "
                                 + " ,[Kamotu2]   = @Kamotu2 "
                                 + " ,[Kamotu3]   = @Kamotu3 "
                                 + " ,[Kamotu4]   = @Kamotu4 "
                                 + " ,[Bus1]      = @Bus1  "
                                 + " ,[Bus2]      = @Bus2 "
                                 + " ,[JK_J1]     = @JK_J1 "
                                 + " ,[JK_K1]     = @JK_K1 "
                                 + " ,[JK_J2]     = @JK_J2 "
                                 + " ,[JK_K2]     = @JK_K2 "
                                 + " ,[JK_J3]     = @JK_J3 "
                                 + " ,[JK_K3]     = @JK_K3 "
                                 + " ,[SubTotal1] = @SubTotal1 "
                                 + " ,[Total1]    = @Total1 "
                                 + " WHERE COCODE = @COCODE AND Year = @Year AND Month = @Month AND Day = @Day";


            //Sqlコネクション
            try
            {
                SqlCommand cmd = new SqlCommand(updateTuchiuraSql, Conn, Tran);
                cmd.CommandText = updateTuchiuraSql;
                //Sqlインジェクション回避
                //キー項目
                cmd.Parameters.Add(new SqlParameter("@COCODE", Session["COCODE"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@Year", Utility.HeiseiToChristianEra(txtYear.Text)));
                cmd.Parameters.Add(new SqlParameter("@Month", txtMonth.Text));
                cmd.Parameters.Add(new SqlParameter("@Day", txtDay.Text));


                //貨物
                cmd.Parameters.Add(new SqlParameter("@Kamotu1", txtTuchiura_Kamotu1.Text));
                cmd.Parameters.Add(new SqlParameter("@Kamotu2", txtTuchiura_Kamotu2.Text));
                cmd.Parameters.Add(new SqlParameter("@Kamotu3", txtTuchiura_Kamotu3.Text));
                cmd.Parameters.Add(new SqlParameter("@Kamotu4", txtTuchiura_Kamotu4.Text));
                //バス
                cmd.Parameters.Add(new SqlParameter("@Bus1", txtTuchiura_Bus1.Text));
                cmd.Parameters.Add(new SqlParameter("@Bus2", txtTuchiura_Bus2.Text));
                //乗用　貨物
                cmd.Parameters.Add(new SqlParameter("@JK_J1", txtTuchiura_JK_J1.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_K1", txtTuchiura_JK_K1.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_J2", txtTuchiura_JK_J2.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_K2", txtTuchiura_JK_K2.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_J3", txtTuchiura_JK_J3.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_K3", txtTuchiura_JK_K3.Text));
                cmd.Parameters.Add(new SqlParameter("@SubTotal1", txtTuchiura_SubTotal1.Text));
                cmd.Parameters.Add(new SqlParameter("@Total1", txtTuchiura_Total1.Text));
                cmd.ExecuteNonQuery();

            }
            catch
            {
                throw;

            }

        }


        /// <summary>
        /// Update Tukuba's Data
        /// </summary>
        private void updateTukuba(SqlConnection Conn, SqlTransaction Tran)
        {

            //INSERT作成
            //SqlConnection sn = new ;
            String updateTukubaSql = "UPDATE [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Tukuba] "
                                 + " SET"
                                 + "  [Year]      = @Year "
                                 + " ,[Month]     = @Month "
                                 + " ,[Day]       = @Day "
                                 + " ,[COCODE]    = @COCODE "
                                 + " ,[Kamotu1]   = @Kamotu1 "
                                 + " ,[Kamotu2]   = @Kamotu2 "
                                 + " ,[Kamotu3]   = @Kamotu3 "
                                 + " ,[Kamotu4]   = @Kamotu4 "
                                 + " ,[Bus1]      = @Bus1  "
                                 + " ,[Bus2]      = @Bus2 "
                                 + " ,[JK_J1]     = @JK_J1 "
                                 + " ,[JK_K1]     = @JK_K1 "
                                 + " ,[JK_J2]     = @JK_J2 "
                                 + " ,[JK_K2]     = @JK_K2 "
                                 + " ,[JK_J3]     = @JK_J3 "
                                 + " ,[JK_K3]     = @JK_K3 "
                                 + " ,[SubTotal1] = @SubTotal1 "
                                 + " ,[Total1]    = @Total1 "
                                 + " WHERE COCODE = @COCODE AND Year = @Year AND Month = @Month AND Day = @Day";


            //Sqlコネクション
            try
            {
                SqlCommand cmd = new SqlCommand(updateTukubaSql, Conn, Tran);
                cmd.CommandText = updateTukubaSql;
                //Sqlインジェクション回避
                //キー項目
                cmd.Parameters.Add(new SqlParameter("@COCODE", Session["COCODE"].ToString()));

                cmd.Parameters.Add(new SqlParameter("@Year", Utility.HeiseiToChristianEra(txtYear.Text)));
                cmd.Parameters.Add(new SqlParameter("@Month", txtMonth.Text));
                cmd.Parameters.Add(new SqlParameter("@Day", txtDay.Text));


                //貨物
                cmd.Parameters.Add(new SqlParameter("@Kamotu1", txtTukuba_Kamotu1.Text));
                cmd.Parameters.Add(new SqlParameter("@Kamotu2", txtTukuba_Kamotu2.Text));
                cmd.Parameters.Add(new SqlParameter("@Kamotu3", txtTukuba_Kamotu3.Text));
                cmd.Parameters.Add(new SqlParameter("@Kamotu4", txtTukuba_Kamotu4.Text));
                //バス
                cmd.Parameters.Add(new SqlParameter("@Bus1", txtTukuba_Bus1.Text));
                cmd.Parameters.Add(new SqlParameter("@Bus2", txtTukuba_Bus2.Text));
                //乗用　貨物
                cmd.Parameters.Add(new SqlParameter("@JK_J1", txtTukuba_JK_J1.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_K1", txtTukuba_JK_K1.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_J2", txtTukuba_JK_J2.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_K2", txtTukuba_JK_K2.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_J3", txtTukuba_JK_J3.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_K3", txtTukuba_JK_K3.Text));
                cmd.Parameters.Add(new SqlParameter("@SubTotal1", txtTukuba_SubTotal1.Text));
                cmd.Parameters.Add(new SqlParameter("@Total1", txtTukuba_Total1.Text));
                cmd.ExecuteNonQuery();

            }
            catch
            {
                throw;

            }

        }


        /// <summary>
        /// Update Sonota's Data
        /// </summary>
        private void updateSonota(SqlConnection Conn, SqlTransaction Tran)
        {

            //INSERT作成
            //SqlConnection sn = new ;
            String updateSonotaSql = "UPDATE [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Sonota] "
                                 + " SET"
                                 + "  [Year]      = @Year "
                                 + " ,[Month]     = @Month "
                                 + " ,[Day]       = @Day "
                                 + " ,[COCODE]    = @COCODE "
                                 + " ,[Kamotu1]   = @Kamotu1 "
                                 + " ,[Kamotu2]   = @Kamotu2 "
                                 + " ,[Kamotu3]   = @Kamotu3 "
                                 + " ,[Kamotu4]   = @Kamotu4 "
                                 + " ,[Bus1]      = @Bus1  "
                                 + " ,[Bus2]      = @Bus2 "
                                 + " ,[JK_J1]     = @JK_J1 "
                                 + " ,[JK_K1]     = @JK_K1 "
                                 + " ,[JK_J2]     = @JK_J2 "
                                 + " ,[JK_K2]     = @JK_K2 "
                                 + " ,[JK_J3]     = @JK_J3 "
                                 + " ,[JK_K3]     = @JK_K3 "
                                 + " ,[SubTotal1] = @SubTotal1 "
                                 + " ,[Total1]    = @Total1 "
                                 + " WHERE COCODE = @COCODE AND Year = @Year AND Month = @Month AND Day = @Day";


            //Sqlコネクション
            try
            {
                SqlCommand cmd = new SqlCommand(updateSonotaSql, Conn, Tran);
                cmd.CommandText = updateSonotaSql;
                //Sqlインジェクション回避
                //キー項目
                cmd.Parameters.Add(new SqlParameter("@COCODE", Session["COCODE"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@Year", Utility.HeiseiToChristianEra(txtYear.Text)));
                cmd.Parameters.Add(new SqlParameter("@Month", txtMonth.Text));
                cmd.Parameters.Add(new SqlParameter("@Day", txtDay.Text));


                //貨物
                cmd.Parameters.Add(new SqlParameter("@Kamotu1", txtSonota_Kamotu1.Text));
                cmd.Parameters.Add(new SqlParameter("@Kamotu2", txtSonota_Kamotu2.Text));
                cmd.Parameters.Add(new SqlParameter("@Kamotu3", txtSonota_Kamotu3.Text));
                cmd.Parameters.Add(new SqlParameter("@Kamotu4", txtSonota_Kamotu4.Text));
                //バス
                cmd.Parameters.Add(new SqlParameter("@Bus1", txtSonota_Bus1.Text));
                cmd.Parameters.Add(new SqlParameter("@Bus2", txtSonota_Bus2.Text));
                //乗用　貨物
                cmd.Parameters.Add(new SqlParameter("@JK_J1", txtSonota_JK_J1.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_K1", txtSonota_JK_K1.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_J2", txtSonota_JK_J2.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_K2", txtSonota_JK_K2.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_J3", txtSonota_JK_J3.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_K3", txtSonota_JK_K3.Text));
                cmd.Parameters.Add(new SqlParameter("@SubTotal1", txtSonota_SubTotal1.Text));
                cmd.Parameters.Add(new SqlParameter("@Total1", txtSonota_Total1.Text));
                cmd.ExecuteNonQuery();

            }
            catch
            {
                throw;

            }

        }


        /// <summary>
        /// Update Goukei's Data
        /// </summary>
        private void updateGoukei(SqlConnection Conn, SqlTransaction Tran)
        {

            //INSERT作成
            //SqlConnection sn = new ;
            String updateGoukeiSql = "UPDATE [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Goukei] "
                                 + " SET"
                                 + "  [Year]      = @Year "
                                 + " ,[Month]     = @Month "
                                 + " ,[Day]       = @Day "
                                 + " ,[COCODE]    = @COCODE "
                                 + " ,[Kamotu1]   = @Kamotu1 "
                                 + " ,[Kamotu2]   = @Kamotu2 "
                                 + " ,[Kamotu3]   = @Kamotu3 "
                                 + " ,[Kamotu4]   = @Kamotu4 "
                                 + " ,[Bus1]      = @Bus1  "
                                 + " ,[Bus2]      = @Bus2 "
                                 + " ,[JK_J1]     = @JK_J1 "
                                 + " ,[JK_K1]     = @JK_K1 "
                                 + " ,[JK_J2]     = @JK_J2 "
                                 + " ,[JK_K2]     = @JK_K2 "
                                 + " ,[JK_J3]     = @JK_J3 "
                                 + " ,[JK_K3]     = @JK_K3 "
                                 + " ,[SubTotal1] = @SubTotal1 "
                                 + " ,[Total1]    = @Total1 "
                                 + " WHERE COCODE = @COCODE AND Year = @Year AND Month = @Month AND Day = @Day";


            //Sqlコネクション
            try
            {
                SqlCommand cmd = new SqlCommand(updateGoukeiSql, Conn, Tran);
                cmd.CommandText = updateGoukeiSql;
                //Sqlインジェクション回避
                //キー項目
                cmd.Parameters.Add(new SqlParameter("@COCODE", Session["COCODE"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@Year", Utility.HeiseiToChristianEra(txtYear.Text)));
                cmd.Parameters.Add(new SqlParameter("@Month", txtMonth.Text));
                cmd.Parameters.Add(new SqlParameter("@Day", txtDay.Text));


                //貨物
                cmd.Parameters.Add(new SqlParameter("@Kamotu1", txtGoukei_Kamotu1.Text));
                cmd.Parameters.Add(new SqlParameter("@Kamotu2", txtGoukei_Kamotu2.Text));
                cmd.Parameters.Add(new SqlParameter("@Kamotu3", txtGoukei_Kamotu3.Text));
                cmd.Parameters.Add(new SqlParameter("@Kamotu4", txtGoukei_Kamotu4.Text));
                //バス
                cmd.Parameters.Add(new SqlParameter("@Bus1", txtGoukei_Bus1.Text));
                cmd.Parameters.Add(new SqlParameter("@Bus2", txtGoukei_Bus2.Text));
                //乗用　貨物
                cmd.Parameters.Add(new SqlParameter("@JK_J1", txtGoukei_JK_J1.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_K1", txtGoukei_JK_K1.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_J2", txtGoukei_JK_J2.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_K2", txtGoukei_JK_K2.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_J3", txtGoukei_JK_J3.Text));
                cmd.Parameters.Add(new SqlParameter("@JK_K3", txtGoukei_JK_K3.Text));
                cmd.Parameters.Add(new SqlParameter("@SubTotal1", txtGoukei_SubTotal1.Text));
                cmd.Parameters.Add(new SqlParameter("@Total1", txtGoukei_Total1.Text));
                cmd.ExecuteNonQuery();

            }
            catch
            {
                throw;

            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //try{
                //TODO
                 //ログインしていなければ表示しない
                 if (Session["COCODE"] == null) {
                     Response.Redirect("loginJisseki.aspx");           
                 }

                 if (Session["Member"].ToString() == "1")
                 { //TODO自販連ユーザーと振り分け
                     //会員
                     jadaUser = false;
                 }
                 else 
                 {
                    //自販連
                     jadaUser = true;
                 
                 }

                 //接続文字列
                 strConn = ConfigurationManager.ConnectionStrings["JissekiConnectionString"].ConnectionString;

                 //Receive Keys by GET
                 qCOCODE = Page.Request.QueryString.Get("COCODE");
                 qYearRep = Page.Request.QueryString.Get("YearRep");
                 qMonthRep = Page.Request.QueryString.Get("MonthRep");
            
            //     Response.Write(qCOCODE);
            //     Response.Write(qYearRep);
            //     Response.Write(qMonthRep);
            //

                 if (!Page.IsPostBack)
                 {
                     initializeForm();
                 }   
           //}catch{
           
           //}
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //入力チェック
            try
            {
                using (SqlConnection Conn = new SqlConnection(strConn))
                {
                    Conn.Open();
                    using (SqlTransaction Tran = Conn.BeginTransaction())
                    {
                        try
                        {
                            //ヘッダー
                            this.updateHeader(Conn, Tran);

                            //水戸
                            this.updateMito(Conn,Tran);

                            //土浦
                            this.updateTuchiura(Conn, Tran);

                            //つくば
                            this.updateTukuba(Conn, Tran);

                            //その他
                            this.updateSonota(Conn, Tran);

                            //合計
                            this.updateGoukei(Conn, Tran);

                            //Commit Transaction
                            Tran.Commit();
                            btnSubmit.Enabled = false;
                            this.lblMsg.Text = "修正しました";
                        }
                        catch
                        {
                            //Rollback Transaction
                            Tran.Rollback();
                            throw ;
                           
                        }
                     
                    }
                }

  
            }
            catch (Exception ex)
            {
                this.lblMsg.Text = ex.Message;
                //Response.Write("<p style=background-color:red;>" + ex.Message + "</p>");
                //Response.Write("<p style=background-color:red;>" + ex.StackTrace + "</p>");
            }           
                        
       }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect(URL.LOGIN_DEALER);

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

           //セッションで渡す
            if (jadaUser)
            {

                //自販連ユーザー
                this.Session["Jisseki_Report_COCODE"] = qCOCODE;
                this.Session["Jisseki_Report_YearRep"] = Utility.HeiseiToChristianEra(this.txtYearRep0.Text);
                this.Session["Jisseki_Report_MonthRep"] = this.txtMonthRep0.Text;
       
                
            }
            else 
            {
                //会員
                this.Session["Jisseki_Report_COCODE"] = this.Session["COCODE"];
                this.Session["Jisseki_Report_YearRep"] = Utility.HeiseiToChristianEra(this.txtYearRep0.Text);
                this.Session["Jisseki_Report_MonthRep"] = this.txtMonthRep0.Text;
            }
            
            string REPORT_JISSEKI_REPORT = "../Report/Jisseki_Report_View.aspx";            
            string js = "";
            js += "<script language='JavaScript'>";
            js += "window.open('" + REPORT_JISSEKI_REPORT + "')";
            js += "</script>";

           
            Page.ClientScript.RegisterStartupScript(this.GetType(), "startup", js);



        }

    }
}