using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Jisseki_Report_Ibaraki.Tools;

namespace Jisseki_Report_Ibaraki.jada.master
{
    public partial class UnitPriceMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            //ログインしていなければ表示しない
            if (Session["COCODE"] == null)
            {
                Response.Redirect(URL.LOGIN_DEALER);
            }


            FormView1.FooterText = "仮請求書の単価を設定";
        }

        protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {

            try
            {

                foreach (DictionaryEntry entry in e.NewValues)
                {

                    string test = e.NewValues[entry.Key].ToString();
                    if (Utility.IsNotNumber(e.NewValues[entry.Key].ToString()))
                    {

                        e.Cancel = true;
                        FormView1.FooterText = "半角数値を入力してください。";
                        return;


                    }

                }
            }
            catch 
            {
                FormView1.FooterText = "更新エラー";
            }
        }

        protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            FormView1.FooterText = "更新されました";

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