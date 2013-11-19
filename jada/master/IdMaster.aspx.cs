using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Jisseki_Report_Ibaraki.Tools;

namespace Jisseki_Report_Ibaraki.jada.master
{
    public partial class IdMaster : System.Web.UI.Page
    {
        private string strConn;

        private void setGridView() {
            string Sql = " SELECT * FROM [Jisseki_Report_Ibaraki].dbo.ID ";


            using (SqlConnection Conn = new SqlConnection(strConn))
            {
                Conn.Open();
                using (SqlCommand cmd = new SqlCommand(Sql, Conn))
                {
                    /*
                    cmd.Parameters.Add(new SqlParameter("@COCODE", qCOCODE));
                    cmd.Parameters.Add(new SqlParameter("@YearRep", qYearRep));
                    cmd.Parameters.Add(new SqlParameter("@MonthRep", qMonthRep));
                    */
                    //using (SqlDataReader Reader = cmd.ExecuteReader())
                    //{
                    using (SqlDataAdapter Adapter = new SqlDataAdapter(Sql, Conn))
                    {
                        Adapter.SelectCommand = cmd;
                        DataTable ID = new DataTable("IDマスタ");
                        Adapter.Fill(ID);
                        if (ID.Rows.Count == 0)
                        {
                            GridView1.EmptyDataText = "IDマスタに登録がありません。";
                        }
                        else
                        {

                            GridView1.DataSource = ID;
                            GridView1.DataBind();
                        }
                    }
                    //}
                }
                Conn.Close();
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
            setGridView();

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = GridView1.SelectedIndex;
            String Sql = " SELECT * FROM Jisseki_Report_Ibaraki.dbo.ID "
                       + " WHERE "
                       + " COCODE = @COCODE "  ;


            using (SqlConnection Conn = new SqlConnection(strConn))
            {
                Conn.Open();
                using (SqlCommand cmd = new SqlCommand(Sql, Conn))
                {

                    cmd.Parameters.Add(new SqlParameter("@COCODE", GridView1.Rows[i].Cells[1].Text));

                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            txtCOCODE.Text = reader["COCODE"].ToString();
                            txtCONAME.Text = reader["CONAME"].ToString();
                            txtRepName.Text = reader["RepName"].ToString();
                            txtAddress.Text = reader["Address"].ToString();
                            txtTel.Text = reader["Tel"].ToString();
                            txtPassword.Text = reader["Password"].ToString();
                            txtMember.Text = reader["Member"].ToString();
                            txtshort_CONAME.Text = reader["short_CONAME"].ToString();
                            txtPosition.Text = reader["Position"].ToString();

                        }
                        else {
                            return;
                        }
               
                    }

                   

                }
                Conn.Close();
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //Update
            string Sql= " UPDATE [Jisseki_Report_Ibaraki].[dbo].[ID] "
                      + " SET "
                      + "   [COCODE]       =  @COCODE       "
                      + "  ,[CONAME]       =  @CONAME       "
                      + "  ,[RepName]      =  @RepName      "
                      + "  ,[PostalCode]   =  @PostalCode   "
                      + "  ,[Address]      =  @Address      "
                      + "  ,[Tel]          =  @Tel          "
                      + "  ,[Password]     =  @Password     "
                      + "  ,[Member]       =  @Memeber      "
                      + "  ,[short_CONAME] =  @short_CONAME "
                      + "  ,[Position]     =  @Position     "
                      + " WHERE   "
                            //Key
                      + " COCODE = @Key"
                      ;

            
            using (SqlConnection Conn = new SqlConnection(strConn))
            {
                Conn.Open();
                using (SqlTransaction Tran = Conn.BeginTransaction())
                {
                    try
                    {
                        
                        using (SqlCommand cmd = new SqlCommand(Sql, Conn, Tran))
                        {
                            cmd.Parameters.Add(new SqlParameter("@COCODE", this.txtCOCODE.Text));
                            cmd.Parameters.Add(new SqlParameter("@CONAME", this.txtCONAME.Text));
                            cmd.Parameters.Add(new SqlParameter("@RepName", this.txtRepName.Text));
                            cmd.Parameters.Add(new SqlParameter("@PostalCode", this.txtPostalCode.Text));
                            cmd.Parameters.Add(new SqlParameter("@Address", this.txtAddress.Text));
                            cmd.Parameters.Add(new SqlParameter("@Tel", this.txtTel.Text));
                            cmd.Parameters.Add(new SqlParameter("@Password", this.txtPassword.Text));
                            cmd.Parameters.Add(new SqlParameter("@Memeber", this.txtMember.Text));
                            cmd.Parameters.Add(new SqlParameter("@short_CONAME", this.txtshort_CONAME.Text));
                            cmd.Parameters.Add(new SqlParameter("@Position", this.txtPosition.Text));
                            cmd.Parameters.Add(new SqlParameter("@Key", this.txtCOCODE.Text));
                            cmd.ExecuteNonQuery();


                        }

                        Tran.Commit();
                        setGridView();
                    }
                    catch (Exception ex)
                    {
                        this.lblMsg.Text = ex.Message;
                        Tran.Rollback();

                    }
                }

            }

            //GridViewでバインド

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

            //削除確認
            //OnClient_Clickで


            //削除
            string Sql = " DELETE FROM  [Jisseki_Report_Ibaraki].dbo.ID "
                       + " WHERE COCODE = @Key ";
                       
            using (SqlConnection Conn = new SqlConnection(strConn))
            {
                Conn.Open();
                using (SqlCommand cmd = new SqlCommand(Sql, Conn))
                {   
                    cmd.Parameters.Add(new SqlParameter("@Key", this.txtCOCODE.Text));
                    cmd.ExecuteNonQuery();

                    setGridView();

                }
            }          
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {

            if (txtCOCODE.Text == string.Empty) {
                lblMsg.Text = "会員コードは必須です";
            }

            string Sql = " INSERT INTO [Jisseki_Report_Ibaraki].[dbo].[ID] "
                         + "(" 
                         + " [COCODE] "
                         + ",[CONAME] "
                         + ",[RepName] "
                         + ",[PostalCode] "
                         + ",[Address] "
                         + ",[Tel] "
                         + ",[Password] "
                         + ",[Member] "
                         + ",[short_CONAME] "
                         + ",[Position] " 
                         + ")"
                         + " VALUES "         
                         + "("
                         + " @COCODE "
                         + ",@CONAME "
                         + ",@RepName "
                         + ",@PostalCode "
                         + ",@Address "
                         + ",@Tel "
                         + ",@Password "
                         + ",@Member "
                         + ",@short_CONAME "
                         + ",@Position "
                         + ")";



            //SqlConnection Conn = new SqlConnection(strConn);
            //Conn.Open();
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = Conn;
            //cmd.CommandText = Sql;
            //cmd.Parameters.Add(new SqlParameter("@COCODE", this.txtCOCODE.Text));
            //cmd.Parameters.Add(new SqlParameter("@CONAME", this.txtCONAME.Text));
            //cmd.Parameters.Add(new SqlParameter("@RepName", this.txtRepName.Text));
            //cmd.Parameters.Add(new SqlParameter("@PostalCode", this.txtPostalCode.Text));
            //cmd.Parameters.Add(new SqlParameter("@Address", this.txtAddress.Text));
            //cmd.Parameters.Add(new SqlParameter("@Tel", this.txtTel.Text));
            //cmd.Parameters.Add(new SqlParameter("@Password", this.txtPassword.Text));
            //cmd.Parameters.Add(new SqlParameter("@Member", this.txtMember.Text));
            //cmd.Parameters.Add(new SqlParameter("@short_CONAME", this.txtshort_CONAME.Text));
            //cmd.ExecuteNonQuery();
            //this.setGridView();





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

                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = Conn;
                            cmd.CommandText = Sql;
                            cmd.Transaction = Tran;
                            cmd.Parameters.Add(new SqlParameter("@COCODE", this.txtCOCODE.Text));
                            cmd.Parameters.Add(new SqlParameter("@CONAME", this.txtCONAME.Text));
                            cmd.Parameters.Add(new SqlParameter("@RepName", this.txtRepName.Text));
                            cmd.Parameters.Add(new SqlParameter("@PostalCode", this.txtPostalCode.Text));
                            cmd.Parameters.Add(new SqlParameter("@Address", this.txtAddress.Text));
                            cmd.Parameters.Add(new SqlParameter("@Tel", this.txtTel.Text));
                            cmd.Parameters.Add(new SqlParameter("@Password", this.txtPassword.Text));
                            cmd.Parameters.Add(new SqlParameter("@Member", this.txtMember.Text));
                            cmd.Parameters.Add(new SqlParameter("@short_CONAME", this.txtshort_CONAME.Text));
                            cmd.Parameters.Add(new SqlParameter("@Position", this.txtPosition.Text));
                            cmd.ExecuteNonQuery();
                            //Commit Transaction
                            Tran.Commit();
                            this.setGridView();

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
            catch (SqlException SqlEx)
            {
                if (SqlEx.Number == 2627)
                {                    
                    lblMsg.Text="既に登録済です";
                    //Response.Write("<p style=background-color:red;>既に登録済です</p>");
                }
                else
                {
                    //Response.Write("<p style=background-color:red;>" + SqlEx.Message + "</p>");
                    //Response.Write("<p style=background-color:red;>" + SqlEx.StackTrace + "</p>");
                    lblMsg.Text = "SQLエラー";
                 
                }


            }
            catch (Exception ex)
            {

                //Response.Write("<p style=background-color:red;>" + ex.Message + "</p>");
                //Response.Write("<p style=background-color:red;>" + ex.StackTrace + "</p>");
                lblMsg.Text = "エラー";                

            }

        }

       
     
    }
}