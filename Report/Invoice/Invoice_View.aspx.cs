﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Jisseki_Report_Ibaraki.Tools;

using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Export.Pdf;


namespace Jisseki_Report_Ibaraki.Report
{
    public partial class Invoice_View : System.Web.UI.Page
    {
        private string strConn;

        private string getSql()
        {

        string Sql = " SELECT "
                    //{乗(大) +  貨(大)　+ バス(大) }*216
                    + "I.CONAME as CONAME, H.MonthRep as MonthRep, "
                    //乗(中小」) +  貨(中小)　+ バス(中小)
                    + "(" 
                    + "M.JK_J1 + TC.JK_J1 +  TK.JK_J1  + S.JK_J1 + G.JK_J1 "
                    + " + M.Kamotu1 + M.Kamotu2 + M.Kamotu3 + TK.Kamotu1 + TK.Kamotu2 +  TK.Kamotu3 + TC.Kamotu1 + TC.Kamotu2 + TC.Kamotu3    + S.Kamotu1  +  S.Kamotu2 +  S.Kamotu3 + G.Kamotu1  + G.Kamotu2 +  G.Kamotu3 "
                    + " + M.Bus1 + TC.Bus1 +  TK.Bus1 + S.Bus1 + G.Bus1"
                    + " ) as Num_BigSize ,"
                    //単価
                    + " U.BigSize as U_BigSize ,"
                    + "(" 
                    + "M.JK_J1 + TC.JK_J1 +  TK.JK_J1  + S.JK_J1 + G.JK_J1 "
                    + " + M.Kamotu1 + M.Kamotu2 + M.Kamotu3 + TK.Kamotu1 + TK.Kamotu2 +  TK.Kamotu3 + TC.Kamotu1 + TC.Kamotu2 + TC.Kamotu3    + S.Kamotu1  +  S.Kamotu2 +  S.Kamotu3 + G.Kamotu1  + G.Kamotu2 +  G.Kamotu3 "
                    + " + M.Bus1 + TC.Bus1 +  TK.Bus1 + S.Bus1 + G.Bus1"
                    + " )*U.BigSize as Sum_BigSize, "


                    //{乗(中小」) +  貨(中小)　+ バス(中小) }
                    +
                    "("
                    + "M.JK_J2+M.JK_J3    + 	TK.JK_J2+TK.JK_J3    +	TC.JK_J2+TC.JK_J3   +	S.JK_J2+S.JK_J3   +	G.JK_J2+G.JK_J3 "
                    + "+ M.Kamotu4+M.JK_K1+M.JK_K2+M.JK_K3     +	TC.Kamotu4+TC.JK_K1+TC.JK_K2+TC.JK_K3     +	TK.Kamotu4+TK.JK_K1+TK.JK_K2+TK.JK_K3     +	S.Kamotu4+S.JK_K1+S.JK_K2+S.JK_K3     +	G.Kamotu4+G.JK_K1+G.JK_K2+G.JK_K3 "
                    + "+ M.Bus2 + TC.Bus2 +  TK.Bus2 + S.Bus2 + G.Bus2 "
                    + " ) as Num_MediumSmall ,"
                    //単価
                    + " U.MediumSmall as U_MediumSmall ,"                  
                    //{乗(中小」) +  貨(中小)　+ バス(中小) }*120
                    +
                    "("
                    + "M.JK_J2+M.JK_J3    + 	TK.JK_J2+TK.JK_J3    +	TC.JK_J2+TC.JK_J3   +	S.JK_J2+S.JK_J3   +	G.JK_J2+G.JK_J3 "
                    + "+ M.Kamotu4+M.JK_K1+M.JK_K2+M.JK_K3     +	TC.Kamotu4+TC.JK_K1+TC.JK_K2+TC.JK_K3     +	TK.Kamotu4+TK.JK_K1+TK.JK_K2+TK.JK_K3     +	S.Kamotu4+S.JK_K1+S.JK_K2+S.JK_K3     +	G.Kamotu4+G.JK_K1+G.JK_K2+G.JK_K3 "
                    + "+ M.Bus2 + TC.Bus2 +  TK.Bus2 + S.Bus2 + G.Bus2 "
                    + " )*U.MediumSmall as Sum_MediumSmall ,"
                    +



                    //均等割会費
                    " U.Average as U_Average,"
                    
                    //7t以上
                    +
                    "(M.Kamotu1 + TK.Kamotu1 + TC.Kamotu1 + S.Kamotu1 + G.Kamotu1) as Num_Kamotu7t ,"
                    +
                    "U.Kamotu7t as U_Kamotu7t,"
                    +
                    "(M.Kamotu1 + TK.Kamotu1 + TC.Kamotu1 + S.Kamotu1 + G.Kamotu1)*U.Kamotu7t as  Sum_Kamotu7t     , "
                    +
                    //6.9t~5t
                    "(M.Kamotu2 + TK.Kamotu2 + TC.Kamotu2 + S.Kamotu2 + G.Kamotu2) as  Num_Kamotu6DP9_5t  ,"
                    +
                    "U.[Kamotu6DP9_5t]  as U_Kamotu6DP9_5t , "
                    +
                    "(M.Kamotu2 + TK.Kamotu2 + TC.Kamotu2 + S.Kamotu2 + G.Kamotu2)*U.[Kamotu6DP9_5t] as Sum_Kamotu6DP9_5t  ,"


                    //4.9t~3t
                    +
                    " (M.Kamotu3 + TK.Kamotu3 + TC.Kamotu3 + S.Kamotu3 + G.Kamotu3) as Num_Kamotu4DP9_3t  ,"
                    +
                    " U.[Kamotu4DP9_3t] as U_Kamotu4DP9_3t  ,"
                    +
                    "(M.Kamotu3 + TK.Kamotu3 + TC.Kamotu3 + S.Kamotu3 + G.Kamotu3)*U.[Kamotu4DP9_3t] as Sum_Kamotu4DP9_3t, "


                    //2.9t~2.5t
                    +
                    "(M.Kamotu4 + TK.Kamotu4 + TC.Kamotu4 + S.Kamotu4 + G.Kamotu4) as Num_Kamotu2DP9_2DP5t  ,"
                    +
                    " U.[Kamotu2DP9_2DP5t] as U_Kamotu2DP9_2DP5t  ,"
                    +
                    "(M.Kamotu4 + TK.Kamotu4 + TC.Kamotu4 + S.Kamotu4 + G.Kamotu4)*U.[Kamotu2DP9_2DP5t] as Sum_Kamotu2DP9_2DP5t  ,"
                    

                    //2,001cc
                    +
                    "(M.JK_J1 + TC.JK_J1   + TK.JK_J1   + S.JK_J1   +  G.JK_J1) as Num_Over2001cc , "
                    +
                    "U.Over2001cc as U_Over2001cc , "
                    +
                    "(M.JK_J1 + TC.JK_J1   + TK.JK_J1   + S.JK_J1   +  G.JK_J1)*U.Over2001cc as Sum_Over2001cc , "
                  
                    //2,000cc~1000cc
                    +
                    "(M.JK_J2 + TC.JK_J2 + TK.JK_J2 + S.JK_J2 + G.JK_J2 + M.JK_J3+ TC.JK_J3 + TK.JK_J3 + S.JK_J3 + G.JK_J3 + M.JK_K1+ TC.JK_K1 + TK.JK_K1 + S.JK_K1	+ G.JK_K1 + M.JK_K2 + TC.JK_K2 + TK.JK_K2 + S.JK_K2 + G.JK_K2 + M.JK_K3	+ TC.JK_K3 + TK.JK_K3 + S.JK_K3	+ G.JK_K3) as Num_To2000From1000cc ,"
                    +
                    "U.[To2000From1000cc] as U_To2000From1000cc ,"
                    +
                    "(M.JK_J2 + TC.JK_J2 + TK.JK_J2 + S.JK_J2 + G.JK_J2 + M.JK_J3+ TC.JK_J3 + TK.JK_J3 + S.JK_J3	+ G.JK_J3 + M.JK_K1	+ TC.JK_K1 + TK.JK_K1 + S.JK_K1	+ G.JK_K1 + M.JK_K2 + TC.JK_K2 + TK.JK_K2 + S.JK_K2 + G.JK_K2 + M.JK_K3	+ TC.JK_K3 + TK.JK_K3 + S.JK_K3	+ G.JK_K3) * U.[To2000From1000cc] as Sum_To2000From1000cc ,"
                    
                    //30
                    +
                    "(M.Bus1 + TC.Bus1 +  TK.Bus1 + S.Bus1 + G.Bus1) as Num_Over30 ,"
                    +
                    "U.Over30 as U_Over30 ,"
                    +
                    "(M.Bus1 + TC.Bus1 +  TK.Bus1 + S.Bus1 + G.Bus1)*U.Over30 as Sum_Over30 ,"
                    
                    //20
                    +
                    "(M.Bus2 + TC.Bus2 +  TK.Bus2 + S.Bus2 + G.Bus2 ) as Num_LessThan29 , "
                    +
                    "U.LessThan29 as U_LessThan29 , "
                    +
                    "(M.Bus2 + TC.Bus2 +  TK.Bus2 + S.Bus2 + G.Bus2 )*U.LessThan29 as Sum_LessThan29 , "
                    +
                    "U.MemberFee  as U_MemberFee "
                       + " FROM "
                       + "  [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Header]  H "
                       + " INNER JOIN  "
                       + "   [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Mito]   M "
                       + " ON H.COCODE = M.COCODE AND H.YearRep = M.YearRep AND H.MonthRep = M.MonthRep "
                       + " INNER JOIN "
                       + "  [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Tuchiura]   TC "
                       + " ON H.COCODE = TC.COCODE AND H.YearRep = TC.YearRep AND H.MonthRep = TC.MonthRep "
                       + "  INNER JOIN "
                       + "   [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Tukuba]   TK "
                       + " ON H.COCODE = TK.COCODE AND H.YearRep = TK.YearRep AND H.MonthRep = TK.MonthRep "
                       + "  INNER JOIN "
                       + "   [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Sonota]   S "
                       + " ON H.COCODE = S.COCODE AND H.YearRep = S.YearRep AND H.MonthRep = S.MonthRep "
                       + "  INNER JOIN "
                       + "   [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Goukei]   G "
                       + " ON H.COCODE = G.COCODE AND H.YearRep = G.YearRep AND H.MonthRep = G.MonthRep "
                       + "  INNER JOIN "
                       + "   [Jisseki_Report_Ibaraki].[dbo].[ID]   I "
                       + " ON H.COCODE = I.COCODE "
                       + " INNER JOIN [Jisseki_Report_Ibaraki].[dbo].UnitPrice U "
                       + " ON H.COCODE =  U.COCODE "
                       + " WHERE "
                       + " H.COCODE = @COCODE "
                       + " AND "
                       + " H.YearRep = @YearRep "
                       + " AND "
                       + " H.MonthRep = @MonthRep " ;

            return Sql;

        }

        /// <summary>
        /// 賛助会員かどうか
        /// </summary>
        /// <returns></returns>
        private Boolean IsSanjyo()
        {
            SqlConnection Conn = new SqlConnection(strConn);
            Conn.Open();
            SqlCommand cmd = new SqlCommand();
            
            cmd.CommandText = "SELECT * FROM  [Jisseki_Report_Ibaraki].[dbo].[ID] WHERE COCODE = @COCODE ";
            cmd.Connection = Conn;
  
            //セッションで渡すとき
            cmd.Parameters.Add(new SqlParameter("@COCODE", this.Session["Jisseki_Report_COCODE"].ToString()));

            SqlDataReader Readr = cmd.ExecuteReader();
                Readr.Read();
                if (Readr["MemberType"] == null)
                {
                    return false;
                }
                if (Readr["MemberType"].ToString() == "0")
                {
                    return false;
                }
                if (Readr["MemberType"].ToString() == "1")
                {
                    return true;
                }
                return false;
            
     
        }
        
        /// <summary>
        /// 通常会員
        /// </summary>
        private void runInvoice(){

            ActiveReport rpt = new Invoice();

            // レポートを作成します。
            try
            {

                using (SqlConnection Conn = new SqlConnection(strConn))
                {
                    Conn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = getSql();
                        cmd.Connection = Conn;

                        //セッションで渡すとき
                        cmd.Parameters.Add(new SqlParameter("@COCODE", this.Session["Jisseki_Report_COCODE"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@YearRep", this.Session["Jisseki_Report_YearRep"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@MonthRep", this.Session["Jisseki_Report_MonthRep"].ToString()));


                        using (SqlDataAdapter Adapter = new SqlDataAdapter())
                        {

                            Adapter.SelectCommand = cmd;
                            DataTable dt = new DataTable();
                            Adapter.Fill(dt);
                            rpt.DataSource = dt;
                            rpt.Run(false);

                        }
                    }
                }

            }
            catch (DataDynamics.ActiveReports.ReportException eRunReport)
            {
                // レポートの作成に失敗した場合、クライアントにエラーメッセージを表示します。
                Response.Clear();
                Response.Write("<h1>レポート生成時にエラーが発生しました。</h1>");
                Response.Write("<font face=\"MS UI Gothic\">" + eRunReport.ToString() + "</font>");
                return;
            }

            //以下はサンプルコードをそのまま流用。

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();

            //  ブラウザに対してPDFドキュメントの適切なビューワを使用するように指定します。
            Response.ContentType = "application/pdf";

            Response.AddHeader("content-disposition", "inline; filename=MyPDF.PDF");
            // 次のコードに置き換えると新しいウィンドウを開きます：
            // Response.AddHeader("content-disposition","attachment; filename=MyPDF.PDF");

            // PDFエクスポートクラスのインスタンスを作成します。
            PdfExport pdf = new PdfExport();

            // PDFの出力用のメモリストリームを作成します。
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();

            // メモリストリームにPDFエクスポートを行います。
            pdf.Export(rpt.Document, memStream);

            // 出力ストリームにPDFのストリームを出力します。
            Response.BinaryWrite(memStream.ToArray());

            // バッファリングされているすべての内容をクライアントへ送信します。
            Response.End();
        
        }


        private void runInvoiceSanjyo(){
        
            ActiveReport rpt = new InvoiceSanjyo();

            // レポートを作成します。
            try
            {

                using (SqlConnection Conn = new SqlConnection(strConn))
                {
                    Conn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = getSql();
                        cmd.Connection = Conn;

                        //セッションで渡すとき
                        cmd.Parameters.Add(new SqlParameter("@COCODE", this.Session["Jisseki_Report_COCODE"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@YearRep", this.Session["Jisseki_Report_YearRep"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@MonthRep", this.Session["Jisseki_Report_MonthRep"].ToString()));


                        using (SqlDataAdapter Adapter = new SqlDataAdapter())
                        {

                            Adapter.SelectCommand = cmd;
                            DataTable dt = new DataTable();
                            Adapter.Fill(dt);
                            rpt.DataSource = dt;
                            rpt.Run(false);

                        }
                    }
                }

            }
            catch (DataDynamics.ActiveReports.ReportException eRunReport)
            {
                // レポートの作成に失敗した場合、クライアントにエラーメッセージを表示します。
                Response.Clear();
                Response.Write("<h1>レポート生成時にエラーが発生しました。</h1>");
                Response.Write("<font face=\"MS UI Gothic\">" + eRunReport.ToString() + "</font>");
                return;
            }

            //以下はサンプルコードをそのまま流用。

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();

            //  ブラウザに対してPDFドキュメントの適切なビューワを使用するように指定します。
            Response.ContentType = "application/pdf";

            Response.AddHeader("content-disposition", "inline; filename=MyPDF.PDF");
            // 次のコードに置き換えると新しいウィンドウを開きます：
            // Response.AddHeader("content-disposition","attachment; filename=MyPDF.PDF");

            // PDFエクスポートクラスのインスタンスを作成します。
            PdfExport pdf = new PdfExport();

            // PDFの出力用のメモリストリームを作成します。
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();

            // メモリストリームにPDFエクスポートを行います。
            pdf.Export(rpt.Document, memStream);

            // 出力ストリームにPDFのストリームを出力します。
            Response.BinaryWrite(memStream.ToArray());

            // バッファリングされているすべての内容をクライアントへ送信します。
            Response.End();
        
        
        }

        protected void Page_Load(object sender, EventArgs e)
        {
         try { 
            //ログインしていなければ表示しない
            if (Session["COCODE"] == null)
            {
                Response.Redirect(URL.LOGIN_DEALER);
            }
            //接続文字列
            strConn = ConfigurationManager.ConnectionStrings["JissekiConnectionString"].ConnectionString;
            if (IsSanjyo())
            {
                 //賛助会員
                 this.runInvoiceSanjyo();
            }
            else
            {
                 //通常会員
                this.runInvoice();
            }


         }
         catch
         {

         }

      }
    }
}