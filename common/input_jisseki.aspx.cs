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
using Jisseki_Report_Ibaraki.Tools;
//TODO 報告年　報告日をKEYとして追加する(送信日と別にする)E
//TODO 1クラスを1テーブルにしたい

namespace Jisseki_Report_Ibaraki.common
{
    public partial class input_jisseki : System.Web.UI.Page
    {
        #region "メンバ 変数"

            //接続文字列
            private String strConn;
            //自販連か会員か
            private bool jadaUser;
            private String qCOCODE;
        #endregion

            #region "メソッド"
            /// <summary>
            /// InitializeForm
            /// </summary>
            private void initializeForm()
            {

                //初期表示
                JapaneseCalendar jCalender = new JapaneseCalendar();
                int iEra = jCalender.GetEra(DateTime.Now);
                switch (iEra)
                {
                    case 4://平成
                        lblEra.Text = "平成";
                        lblEraRep0.Text = "平成";
                        lblEraRep1.Text = "平成";
                        break;

                    case 3://昭和
                        lblEra.Text = "昭和";
                        lblEraRep0.Text = "昭和";
                        lblEraRep1.Text = "昭和";

                        break;

                    case 2://大正
                        lblEra.Text = "大正";
                        lblEraRep0.Text = "大正";
                        lblEraRep1.Text = "大正";

                        break;

                    case 1://明治
                        lblEra.Text = "明治";
                        lblEraRep0.Text = "明治";
                        lblEraRep1.Text = "明治";

                        break;

                }


                //送信日
                txtYear.Text = jCalender.GetYear(DateTime.Today).ToString();
                txtMonth.Text = jCalender.GetMonth(DateTime.Today).ToString();
                txtDay.Text = jCalender.GetDayOfMonth(DateTime.Today).ToString();

                //会社名
                txtSyamei.Text = Session["CONAME"].ToString();

                //報告日
                txtMonthRep0.Text = string.Empty;
                txtMonthRep1.Text = string.Empty;

                this.btnPrint.Visible = false;

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



            #endregion

            #region"自販連管理者が登録する場合"
            /// <summary>
            /// 自販連管理者が登録する場合
            /// </summary>
            private void setHeader()
            {
                //初期表示
                string SqlHeader =
                    " SELECT * FROM [Jisseki_Report_Ibaraki].[dbo].[ID]  "
                    + " WHERE COCODE=@COCODE ";
                using (SqlConnection Conn = new SqlConnection(strConn))
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand(SqlHeader, Conn))
                    {

                        cmd.Parameters.Add(new SqlParameter("@COCODE", qCOCODE));
                        using (SqlDataReader Reader = cmd.ExecuteReader())
                        {
                            Reader.Read();
                            this.txtSyamei.Text = Reader["CONAME"].ToString();
                        }
                    }
                    Conn.Close();
                }
            }

            #endregion

            #region"登録メソッド"
            private void insertHeader(SqlConnection Conn, SqlTransaction Tran)
            {

                //INSERT作成

                String InsertHeaderSql = "INSERT INTO [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Header] "
                               + "("
                               + "  [COCODE],[TANTOU],[Year],[Month],[Day],[YearRep],[MonthRep]"
                               + " ) "
                               + " VALUES "
                               + " ( @COCODE,@TANTOU,@Year,@Month,@Day,@YearRep,@MonthRep"
                               + "  ) ";


                //Sqlコネクション
                try
                {
                    SqlCommand cmd = new SqlCommand(InsertHeaderSql, Conn, Tran);
                    cmd.CommandText = InsertHeaderSql;
                    //Sqlインジェクション回避
                    if (jadaUser)
                    {
                        cmd.Parameters.Add(new SqlParameter("@COCODE", qCOCODE));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@COCODE", Session["COCODE"].ToString()));
                    }
                    cmd.Parameters.Add(new SqlParameter("@TANTOU", txtTantou.Text));
                    cmd.Parameters.Add(new SqlParameter("@Year", Utility.HeiseiToChristianEra(txtYear.Text)));
                    cmd.Parameters.Add(new SqlParameter("@Month", txtMonth.Text));
                    cmd.Parameters.Add(new SqlParameter("@Day", txtDay.Text));
                    cmd.Parameters.Add(new SqlParameter("@YearRep", Utility.HeiseiToChristianEra(txtYearRep0.Text)));
                    cmd.Parameters.Add(new SqlParameter("@MonthRep", txtMonthRep0.Text));


                    cmd.ExecuteNonQuery();

                }
                catch
                {
                    throw;

                }

            }

            /// <summary>
            /// Insert Mito's Data
            /// </summary>
            private void insertMito(SqlConnection Conn, SqlTransaction Tran)
            {

                //INSERT作成
                //SqlConnection sn = new ;
                String InsertMitoSql = "INSERT INTO [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Mito] "
                               + "("
                               + "  [COCODE],[Year],[Month],[Day],[YearRep],[MonthRep],"
                               + "  [Kamotu1],[Kamotu2],[Kamotu3],[Kamotu4],[Bus1],[Bus2], "
                               + "  [JK_J1],[JK_K1],[JK_J2],[JK_K2],[JK_J3],[JK_K3], "
                               + "  [SubTotal1],[Total1] "
                               + " ) "
                               + " VALUES "
                               + " ( @COCODE,@Year,@Month,@Day,@YearRep,@MonthRep,"
                               + "   @Kamotu1,@Kamotu2,@Kamotu3,@Kamotu4,@Bus1,@Bus2,"
                               + "   @JK_J1,@JK_K1,@JK_J2,@JK_K2,@JK_J3,@JK_K3,"
                               + "   @SubTotal1,@Total1 ) ";


                //Sqlコネクション
                try
                {
                    SqlCommand cmd = new SqlCommand(InsertMitoSql, Conn, Tran);
                    cmd.CommandText = InsertMitoSql;
                    //Sqlインジェクション回避
                    //キー項目
                    if (jadaUser)
                    {
                        cmd.Parameters.Add(new SqlParameter("@COCODE", qCOCODE));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@COCODE", Session["COCODE"].ToString()));
                    }
                    cmd.Parameters.Add(new SqlParameter("@Year", Utility.HeiseiToChristianEra(txtYear.Text)));
                    cmd.Parameters.Add(new SqlParameter("@Month", txtMonth.Text));
                    cmd.Parameters.Add(new SqlParameter("@Day", txtDay.Text));
                    cmd.Parameters.Add(new SqlParameter("@YearRep", Utility.HeiseiToChristianEra(txtYearRep0.Text)));
                    cmd.Parameters.Add(new SqlParameter("@MonthRep", txtMonthRep0.Text));

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
                    throw;

                }

            }

            /// <summary>
            /// Insert Tuchiura's Data
            /// </summary>
            private void insertTuchiura(SqlConnection Conn, SqlTransaction Tran)
            {

                //INSERT作成

                String InsertTuchiuraSql = "INSERT INTO [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Tuchiura] "
                               + "("
                               + "  [COCODE],[Year],[Month],[Day],[YearRep],[MonthRep],"
                               + "  [Kamotu1],[Kamotu2],[Kamotu3],[Kamotu4],[Bus1],[Bus2], "
                               + "  [JK_J1],[JK_K1],[JK_J2],[JK_K2],[JK_J3],[JK_K3], "
                               + "  [SubTotal1],[Total1] "
                               + " ) "
                               + " VALUES "
                               + " ( @COCODE,@Year,@Month,@Day,@YearRep,@MonthRep,"
                               + "   @Kamotu1,@Kamotu2,@Kamotu3,@Kamotu4,@Bus1,@Bus2,"
                               + "   @JK_J1,@JK_K1,@JK_J2,@JK_K2,@JK_J3,@JK_K3,"
                               + "   @SubTotal1,@Total1 ) ";


                //Sqlコネクション
                try
                {

                    SqlCommand cmd = new SqlCommand(InsertTuchiuraSql, Conn, Tran);
                    cmd.CommandText = InsertTuchiuraSql;
                    //Sqlインジェクション回避
                    //キー項目
                    if (jadaUser)
                    {
                        cmd.Parameters.Add(new SqlParameter("@COCODE", qCOCODE));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@COCODE", Session["COCODE"].ToString()));
                    }
                    cmd.Parameters.Add(new SqlParameter("@Year", Utility.HeiseiToChristianEra(txtYear.Text)));
                    cmd.Parameters.Add(new SqlParameter("@Month", txtMonth.Text));
                    cmd.Parameters.Add(new SqlParameter("@Day", txtDay.Text));
                    cmd.Parameters.Add(new SqlParameter("@YearRep", Utility.HeiseiToChristianEra(txtYearRep0.Text)));
                    cmd.Parameters.Add(new SqlParameter("@MonthRep", txtMonthRep0.Text));
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
            /// Insert Tukuba's Data
            /// </summary>
            private void insertTukuba(SqlConnection Conn, SqlTransaction Tran)
            {

                //INSERT作成
                //SqlConnection sn = new ;
                String InsertTukubaSql = "INSERT INTO [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Tukuba] "
                               + "("
                               + "  [COCODE],[Year],[Month],[Day],[YearRep],[MonthRep],"
                               + "  [Kamotu1],[Kamotu2],[Kamotu3],[Kamotu4],[Bus1],[Bus2], "
                               + "  [JK_J1],[JK_K1],[JK_J2],[JK_K2],[JK_J3],[JK_K3], "
                               + "  [SubTotal1],[Total1] "
                               + " ) "
                               + " VALUES "
                               + " ( @COCODE,@Year,@Month,@Day,@YearRep,@MonthRep,"
                               + "   @Kamotu1,@Kamotu2,@Kamotu3,@Kamotu4,@Bus1,@Bus2,"
                               + "   @JK_J1,@JK_K1,@JK_J2,@JK_K2,@JK_J3,@JK_K3,"
                               + "   @SubTotal1,@Total1 ) ";


                //Sqlコネクション
                try
                {


                    SqlCommand cmd = new SqlCommand(InsertTukubaSql, Conn, Tran);
                    cmd.CommandText = InsertTukubaSql;
                    //Sqlインジェクション回避
                    //キー項目
                    cmd.Parameters.Add(new SqlParameter("@COCODE", Session["COCODE"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@Year", Utility.HeiseiToChristianEra(txtYear.Text)));
                    cmd.Parameters.Add(new SqlParameter("@Month", txtMonth.Text));
                    cmd.Parameters.Add(new SqlParameter("@Day", txtDay.Text));
                    cmd.Parameters.Add(new SqlParameter("@YearRep", Utility.HeiseiToChristianEra(txtYearRep0.Text)));
                    cmd.Parameters.Add(new SqlParameter("@MonthRep", txtMonthRep0.Text));


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
            /// Insert Sonota's Data
            /// </summary>
            private void insertSonota(SqlConnection Conn, SqlTransaction Tran)
            {

                //INSERT作成
                //SqlConnection sn = new ;
                String InsertSonotaSql = "INSERT INTO [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Sonota] "
                               + "("
                               + "  [COCODE],[Year],[Month],[Day],[YearRep],[MonthRep],"
                               + "  [Kamotu1],[Kamotu2],[Kamotu3],[Kamotu4],[Bus1],[Bus2], "
                               + "  [JK_J1],[JK_K1],[JK_J2],[JK_K2],[JK_J3],[JK_K3], "
                               + "  [SubTotal1],[Total1] "
                               + " ) "
                               + " VALUES "
                               + " ( @COCODE,@Year,@Month,@Day,@YearRep,@MonthRep,"
                               + "   @Kamotu1,@Kamotu2,@Kamotu3,@Kamotu4,@Bus1,@Bus2,"
                               + "   @JK_J1,@JK_K1,@JK_J2,@JK_K2,@JK_J3,@JK_K3,"
                               + "   @SubTotal1,@Total1 ) ";


                //Sqlコネクション
                try
                {
                    SqlCommand cmd = new SqlCommand(InsertSonotaSql, Conn, Tran);
                    cmd.CommandText = InsertSonotaSql;
                    //Sqlインジェクション回避
                    //キー項目
                    if (jadaUser)
                    {
                        cmd.Parameters.Add(new SqlParameter("@COCODE", qCOCODE));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@COCODE", Session["COCODE"].ToString()));
                    }
                    cmd.Parameters.Add(new SqlParameter("@Year", Utility.HeiseiToChristianEra(txtYear.Text)));
                    cmd.Parameters.Add(new SqlParameter("@Month", txtMonth.Text));
                    cmd.Parameters.Add(new SqlParameter("@Day", txtDay.Text));
                    cmd.Parameters.Add(new SqlParameter("@YearRep", Utility.HeiseiToChristianEra(txtYearRep0.Text)));
                    cmd.Parameters.Add(new SqlParameter("@MonthRep", txtMonthRep0.Text));

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
            /// Insert Goukei's Data
            /// </summary>
            private void insertGoukei(SqlConnection Conn, SqlTransaction Tran)
            {

                //INSERT作成
                //SqlConnection sn = new ;
                String InsertGoukeiSql = "INSERT INTO [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Goukei] "
                               + "("
                               + "  [COCODE],[Year],[Month],[Day],[YearRep],[MonthRep],"
                               + "  [Kamotu1],[Kamotu2],[Kamotu3],[Kamotu4],[Bus1],[Bus2], "
                               + "  [JK_J1],[JK_K1],[JK_J2],[JK_K2],[JK_J3],[JK_K3], "
                               + "  [SubTotal1],[Total1] "
                               + " ) "
                               + " VALUES "
                               + " ( @COCODE,@Year,@Month,@Day,@YearRep,@MonthRep,"
                               + "   @Kamotu1,@Kamotu2,@Kamotu3,@Kamotu4,@Bus1,@Bus2,"
                               + "   @JK_J1,@JK_K1,@JK_J2,@JK_K2,@JK_J3,@JK_K3,"
                               + "   @SubTotal1,@Total1 ) ";


                //Sqlコネクション
                try
                {
                    SqlCommand cmd = new SqlCommand(InsertGoukeiSql, Conn, Tran);
                    cmd.CommandText = InsertGoukeiSql;
                    //Sqlインジェクション回避
                    //キー項目
                    if (jadaUser)
                    {
                        cmd.Parameters.Add(new SqlParameter("@COCODE", qCOCODE));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@COCODE", Session["COCODE"].ToString()));
                    }
                    cmd.Parameters.Add(new SqlParameter("@Year", Utility.HeiseiToChristianEra(txtYear.Text)));
                    cmd.Parameters.Add(new SqlParameter("@Month", txtMonth.Text));
                    cmd.Parameters.Add(new SqlParameter("@Day", txtDay.Text));
                    cmd.Parameters.Add(new SqlParameter("@YearRep", Utility.HeiseiToChristianEra(txtYearRep0.Text)));
                    cmd.Parameters.Add(new SqlParameter("@MonthRep", txtMonthRep0.Text));

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
            #endregion

            
        protected void Page_Load(object sender, EventArgs e)
        {
            //ログインしていなければ表示しない
            if (Session["COCODE"] == null) {
                Response.Redirect("loginJisseki.aspx");           
            }
            //接続文字列
            strConn = ConfigurationManager.ConnectionStrings["JissekiConnectionString"].ConnectionString;

            
            //Receive Keys by GET         
            if (this.Session["Member"].ToString() == "1")
            {
                //会員
                jadaUser = false;
            }
            else 
            {   
                //自販連
                jadaUser = true;
                qCOCODE = Page.Request.QueryString.Get("COCODE");

            }


            if (Page.IsPostBack)
            {

            }
            else 
            {
                if (jadaUser)
                {
                    initializeForm();
                    setHeader();
                }
                else 
                {
                    initializeForm();
                
                }               
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //入力チェック
            //サーバー再度でも念のため
            if (this.txtYear.Text == string.Empty) {
                this.lblMsg.Text = "送信日は必須入力です";
                return ;
               
            }

            if (this.txtMonth.Text == string.Empty)
            {
                this.lblMsg.Text = "送信日は必須入力です";
                return;

            }

            if (this.txtDay.Text == string.Empty)
            {
                this.lblMsg.Text = "送信日は必須入力です";
                return;

            }

            if(this.txtSyamei.Text == string.Empty)
            {
                this.lblMsg.Text = "会社名は必須入力です";
                return;
            }


            if (this.txtTantou.Text == string.Empty)
            {
                this.lblMsg.Text = "担当者は必須入力です";
                return;
            }

            if (this.txtYearRep0.Text == string.Empty)
            {
                this.lblMsg.Text = "報告日は必須入力です";
                return;

            }

            if (this.txtMonthRep0.Text == string.Empty)
            {
                this.lblMsg.Text = "報告日は必須入力です";
                return;

            }

            if (this.txtYearRep1.Text == string.Empty)
            {
                this.lblMsg.Text = "報告日は必須入力です";
                return;

            }

            if (this.txtMonthRep1.Text == string.Empty)
            {
                this.lblMsg.Text = "報告日は必須入力です";
                return;

            }

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
                            this.insertHeader(Conn, Tran);

                            //水戸
                            this.insertMito(Conn,Tran);

                            //土浦
                            this.insertTuchiura(Conn,Tran);

                            //つくば
                            this.insertTukuba(Conn,Tran);

                            //その他
                            this.insertSonota(Conn,Tran);

                            //合計
                            this.insertGoukei(Conn,Tran);

                            //Commit Transaction
                            Tran.Commit();
                            btnSubmit.Enabled = false;
                            this.lblMsg.Text = "登録しました";
                            btnPrint.Visible = true;
 

                        }
                        catch
                        {
                            //Rollback Transaction
                            Tran.Rollback();
                            throw;
                           
                        }
                     
                    }
                }

  
            }
            catch (SqlException SqlEx){
                if (SqlEx.Number == 2627)
                {
                   // Response.Write("<p style=background-color:red;>既に登録済です</p>");
                    this.lblMsg.Text = "既に登録されています。";
 
                }
                else {
                 //   Response.Write("<p style=background-color:red;>" + SqlEx.Message + "</p>");
                 //   Response.Write("<p style=background-color:red;>" + SqlEx.StackTrace + "</p>");
                    this.lblMsg.Text = SqlEx.Message;
 
                }
                
            
            }
            catch (Exception ex)
            {
                
               // Response.Write("<p style=background-color:red;>" + ex.Message + "</p>");
               // Response.Write("<p style=background-color:red;>" + ex.StackTrace + "</p>");
                this.lblMsg.Text = ex.Message;
 

            }

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

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect(URL.LOGIN_DEALER);
        }
        
    }
}