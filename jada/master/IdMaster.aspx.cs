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
            try
            {
                string Sql = " SELECT * FROM [Jisseki_Report_Ibaraki].dbo.ID ";
                using (SqlConnection Conn = new SqlConnection(strConn))
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand(Sql, Conn))
                    {
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
                    }
                    Conn.Close();
                }
            }
            catch
            { 
            
            }       
        
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try{

                //ログインしていなければ表示しない
                if (Session["COCODE"] == null)
                {
                    Response.Redirect(URL.LOGIN_DEALER);
                }


                //接続文字列
                strConn = ConfigurationManager.ConnectionStrings["JissekiConnectionString"].ConnectionString;
                setGridView();
            }catch{
            }

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int i = GridView1.SelectedIndex;
                String Sql = " SELECT * FROM Jisseki_Report_Ibaraki.dbo.ID "
                           + " WHERE "
                           + " COCODE = @COCODE ";


                using (SqlConnection Conn = new SqlConnection(strConn))
                {
                    Conn.Open();
                    using (SqlCommand cmd = new SqlCommand(Sql, Conn))
                    {

                        cmd.Parameters.Add(new SqlParameter("@COCODE", GridView1.Rows[i].Cells[1].Text));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                txtCOCODE.Text = reader["COCODE"].ToString();
                                txtUID.Text = reader["UID"].ToString();
                                txtCONAME.Text = reader["CONAME"].ToString();
                                txtRepName.Text = reader["RepName"].ToString();
                                txtPostalCode.Text = reader["PostalCode"].ToString();
                                txtAddress.Text = reader["Address"].ToString();
                                txtTel.Text = reader["Tel"].ToString();
                                txtPassword.Text = reader["Password"].ToString();
                                txtMember.Text = reader["Member"].ToString();
                                txtMemberType.Text = reader["MemberType"].ToString();
                                txtshort_CONAME.Text = reader["short_CONAME"].ToString();
                                txtPosition.Text = reader["Position"].ToString();

                            }
                            else
                            {
                                return;
                            }

                        }



                    }
                    Conn.Close();
                }
            }
            catch { 
            
            
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //Update
            string Sql= " UPDATE [Jisseki_Report_Ibaraki].[dbo].[ID] "
                      + " SET "
                      + "   [COCODE]       =  @COCODE       "
                      + "  ,[UID]          =  @UID          "
                      + "  ,[CONAME]       =  @CONAME       "
                      + "  ,[RepName]      =  @RepName      "
                      + "  ,[PostalCode]   =  @PostalCode   "
                      + "  ,[Address]      =  @Address      "
                      + "  ,[Tel]          =  @Tel          "
                      + "  ,[Password]     =  @Password     "
                      + "  ,[Member]       =  @Memeber      "
                      + "  ,[MemberType]   =  @MemeberType  "
                      + "  ,[short_CONAME] =  @short_CONAME "
                      + "  ,[Position]     =  @Position     "
                      + "  ,[isCanceled]   =  @isCanceled     "
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
                            cmd.Parameters.Add(new SqlParameter("@UID", this.txtUID.Text));
                            cmd.Parameters.Add(new SqlParameter("@CONAME", this.txtCONAME.Text));
                            cmd.Parameters.Add(new SqlParameter("@RepName", this.txtRepName.Text));
                            cmd.Parameters.Add(new SqlParameter("@PostalCode", this.txtPostalCode.Text));
                            cmd.Parameters.Add(new SqlParameter("@Address", this.txtAddress.Text));
                            cmd.Parameters.Add(new SqlParameter("@Tel", this.txtTel.Text));
                            cmd.Parameters.Add(new SqlParameter("@Password", this.txtPassword.Text));
                            cmd.Parameters.Add(new SqlParameter("@Memeber", this.txtMember.Text));
                            cmd.Parameters.Add(new SqlParameter("@MemeberType", this.txtMemberType.Text));
                            cmd.Parameters.Add(new SqlParameter("@short_CONAME", this.txtshort_CONAME.Text));
                            cmd.Parameters.Add(new SqlParameter("@Position", this.txtPosition.Text));
                            cmd.Parameters.Add(new SqlParameter("@isCanceled", this.txtisCanceled.Text));
                            cmd.Parameters.Add(new SqlParameter("@Key", this.txtCOCODE.Text));
                            cmd.ExecuteNonQuery();


                        }

                        Tran.Commit();
                        setGridView();
                        lblMsg.Text = "更新しました";
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

            if (txtCOCODE.Text.Trim() == string.Empty)
            {
                lblMsg.Text = "会員コードは必須です";
                txtCOCODE.BackColor = System.Drawing.Color.Purple;
                txtCOCODE.Focus();
                return;

            }
            else 
            {
                txtCOCODE.BackColor = System.Drawing.Color.White;
            }

            if (txtUID.Text.Trim() == string.Empty)
            {
                lblMsg.Text = "ログインIDは必須です";
                txtUID.BackColor = System.Drawing.Color.Purple;
                txtUID.Focus();
                return;

            }
            else
            {
                txtUID.BackColor = System.Drawing.Color.White;
            }


            if (txtMember.Text.Trim() == string.Empty)
            {
                lblMsg.Text = "会員フラグは必須です";
                txtMember.BackColor = System.Drawing.Color.Purple;
                txtMember.Focus();
                return;

            }
            else 
            {
                lblMsg.Text = "";
                txtMember.BackColor = System.Drawing.Color.White;
            }

            if (txtMember.Text.Trim() != "0" && txtMember.Text.Trim() != "1")
            {
                lblMsg.Text = "会員フラグは0か1を入力してください";
                txtMember.BackColor = System.Drawing.Color.Purple;
                txtMember.Focus();
                return;
            }
            else 
            {
                lblMsg.Text = "";
                txtMember.BackColor = System.Drawing.Color.White;
            }

            if (txtMemberType.Text.Trim() == string.Empty)
            {
                lblMsg.Text = "会員種別は必須です";
                txtMemberType.BackColor = System.Drawing.Color.Purple;
                txtMemberType.Focus();
                return;
            }
            else
            {
                lblMsg.Text = "";
                txtMemberType.BackColor = System.Drawing.Color.White;
            }

            if (txtMemberType.Text.Trim() != "0" && txtMemberType.Text.Trim() != "1")
            {
                lblMsg.Text = "会員種別は0か1を入力してください";
                txtMemberType.BackColor = System.Drawing.Color.Purple;
                txtMemberType.Focus();
                return;
            }
            else 
            {
                lblMsg.Text = "";
                txtMemberType.BackColor = System.Drawing.Color.White;
            }



            string Sql = " INSERT INTO [Jisseki_Report_Ibaraki].[dbo].[ID] "
                         + "(" 
                         + " [COCODE] "
                         + ",[UID] "
                         + ",[CONAME] "
                         + ",[RepName] "
                         + ",[PostalCode] "
                         + ",[Address] "
                         + ",[Tel] "
                         + ",[Password] "
                         + ",[Member] "
                         + ",[MemberType] "
                         + ",[short_CONAME] "
                         + ",[Position] "
                         //+ ",[isCanceled] " 
                         + ")"
                         + " VALUES "         
                         + "("
                         + " @COCODE "
                         + ",@UID    "
                         + ",@CONAME "
                         + ",@RepName "
                         + ",@PostalCode "
                         + ",@Address "
                         + ",@Tel "
                         + ",@Password "
                         + ",@Member "
                         + ",@MemberType "
                         + ",@short_CONAME "
                         + ",@Position "
                         //+ ",@isCanceled"
                         + ")";

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
                            cmd.Parameters.Add(new SqlParameter("@UID", this.txtUID.Text));
                            cmd.Parameters.Add(new SqlParameter("@CONAME", this.txtCONAME.Text));
                            cmd.Parameters.Add(new SqlParameter("@RepName", this.txtRepName.Text));
                            cmd.Parameters.Add(new SqlParameter("@PostalCode", this.txtPostalCode.Text));
                            cmd.Parameters.Add(new SqlParameter("@Address", this.txtAddress.Text));
                            cmd.Parameters.Add(new SqlParameter("@Tel", this.txtTel.Text));
                            cmd.Parameters.Add(new SqlParameter("@Password", this.txtPassword.Text));
                            cmd.Parameters.Add(new SqlParameter("@Member", this.txtMember.Text));
                            cmd.Parameters.Add(new SqlParameter("@MemberType", this.txtMemberType.Text));
                            cmd.Parameters.Add(new SqlParameter("@short_CONAME", this.txtshort_CONAME.Text));
                            cmd.Parameters.Add(new SqlParameter("@Position", this.txtPosition.Text));
                            //cmd.Parameters.Add(new SqlParameter("@isCanceled", this.txtPosition.Text));
                            cmd.ExecuteNonQuery();
                            //Commit Transaction
                            Tran.Commit();
                            this.setGridView();
                            this.lblMsg.Text="登録しました";

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
            catch 
            {

                //Response.Write("<p style=background-color:red;>" + ex.Message + "</p>");
                //Response.Write("<p style=background-color:red;>" + ex.StackTrace + "</p>");
                lblMsg.Text = "エラー";                

            }

        }


        protected void btnlinkMenu_Click(object sender, EventArgs e)
        {
            try
            {
                //自販連
                Response.Redirect(URL.MENU_JADA);
            }
            catch
            {

            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            try
            {
                this.Session.Abandon();
                Response.Redirect(URL.LOGIN_DEALER);

            }
            catch
            {

            }
        }
    }
}