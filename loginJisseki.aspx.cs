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
using Jisseki_Report_Ibaraki.Tools;

namespace Jisseki_Report_Ibaraki
{
    public partial class loginJisseki : System.Web.UI.Page
    {
        //接続文字列
        private String strConn;
 
        protected void Page_Load(object sender, EventArgs e)
        {
            //PageLoad and PostBack
            //接続文字列
            strConn = ConfigurationManager.ConnectionStrings["JissekiConnectionString"].ConnectionString;            

            //Only PostBack
            if (!IsPostBack)
            {
                //クッキーにあれば自動セット

                if (Request.Cookies["COCODE"] != null)
                {
                    txtCOCODE.Text = Server.HtmlEncode(Request.Cookies["COCODE"].Value);
                }

                if (Request.Cookies["Password"] != null)
                {
                   txtPassword.Text = Server.HtmlEncode(Request.Cookies["Password"].Value);
                }    
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection Conn = new SqlConnection(strConn)) {
                Conn.Open();
                string strSQL= "SELECT * FROM Jisseki_Report_Ibaraki.dbo.ID WHERE COCODE = @COCODE AND Password = @Password ";

                SqlCommand cmd = new SqlCommand(strSQL,Conn);
                cmd.Parameters.Add(new SqlParameter("@COCODE", txtCOCODE.Text));
                cmd.Parameters.Add(new SqlParameter("@Password", txtPassword.Text));
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    //ログインOK

                    //クッキーに登録
                    HttpCookie cookieCOCODE = new HttpCookie("COCODE");
                    cookieCOCODE.Value = txtCOCODE.Text;             
                    cookieCOCODE.Expires = DateTime.Now.AddDays(30); 
                    Response.Cookies.Add(cookieCOCODE);

                    HttpCookie cookiePassword = new HttpCookie("Password");
                    cookiePassword.Value = txtPassword.Text;         
                    cookiePassword.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(cookiePassword);

                    //セッションに情報入力
                    reader.Read();
                    Session["COCODE"] = reader["COCODE"];
                    Session["CONAME"] = reader["CONAME"];
                    Session["Member"] = reader["Member"];

                    if (reader["Member"].ToString().Equals("1"))
                    {
                        //会員メニュー
                        Response.Redirect(URL.MENU_DEALER);
                    }
                    else
                    {
                        //自販連メニュー
                        Response.Redirect(URL.MENU_JADA);
                    }
                }
                else { 
                    //ログインNG           
                    this.lblMsg.Text = "ログインIDまたはパスワードが違います。";
                }              
            }
        }
    }
}