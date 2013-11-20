using System;
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
using Jisseki_Report_Ibaraki.Tools;

namespace Jisseki_Report_Ibaraki.jada.search
{
    public partial class nonReported_member : System.Web.UI.Page
    {
        //接続文字列
        private String strConn;

        private const int GV_INDEX_会員コード = 0;
        private const int GV_INDEX_会員名 = 1;
        private const int GV_INDEX_代表者 = 2;
        private const int GV_INDEX_電話番号 = 3;
        private const int GV_INDEX_登録 = 4;

        private void showData() {
            //初期表示は事務処理年月
            string strSql = "SELECT * FROM [Jisseki_Report_Ibaraki].[dbo].ID WHERE "
                            + " COCODE NOT IN "
                            + "( "
                            + " SELECT I.COCODE FROM  [Jisseki_Report_Ibaraki].[dbo].[ID] I "
                            + " INNER JOIN  "
                            + " [Jisseki_Report_Ibaraki].[dbo].[Jisseki_Header] H "
                            + " ON I.COCODE = H.COCODE "
                            + " WHERE "
                            + " H.Year = @Year AND H.Month = @Month "
                            + " AND I.Member = '1' "
                            + " ) "
                            + " AND Member = '1' ";


            using (SqlConnection Conn = new SqlConnection(strConn))
            {
                Conn.Open();
                SqlCommand cmd = new SqlCommand(strSql, Conn);

                cmd.Parameters.Add(new SqlParameter("@Year",Utility.HeiseiToChristianEra(this.txtYearRep.Text)));
                cmd.Parameters.Add(new SqlParameter("@Month", this.txtMonthRep.Text));


                using (SqlDataAdapter Adapter = new SqlDataAdapter(strSql, Conn))
                {
                    Adapter.SelectCommand = cmd;
                    DataTable header = new DataTable("新車台数ヘッダー");
                    Adapter.Fill(header);

                    Gridview1.DataSource = header;
                    Gridview1.DataBind();

                }
            } 
        
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            
            //ログインしていなければ表示しない
            if (Session["COCODE"] == null)
            {
                Response.Redirect(URL.LOGIN_DEALER);
            }
            //接続文字列
            strConn = ConfigurationManager.ConnectionStrings["JissekiConnectionString"].ConnectionString;

            if (Page.IsPostBack) {
            
            } 
            else 
            {
                JapaneseCalendar jCalender = new JapaneseCalendar();

                this.txtYearRep.Text = jCalender.GetYear(DateTime.Today).ToString();
                this.txtMonthRep.Text = DateTime.Today.Month.ToString();
            }
            

       
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.showData();
        }

    }
}