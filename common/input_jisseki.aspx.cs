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


            #region "チェックメソッド"

            private bool HeaderIsValid()
            {
                if (this.txtYear.Text == string.Empty)
                {
                    this.lblMsg.Text = "送信日は必須入力です";
                    this.txtYear.Focus();
                    return false;

                }

                if (this.txtMonth.Text == string.Empty)
                {
                    this.lblMsg.Text = "送信日は必須入力です";
                    this.txtMonth.Focus();
                    return false;

                }


                if (this.txtDay.Text == string.Empty)
                {
                    this.lblMsg.Text = "送信日は必須入力です";
                    this.txtDay.Focus();
                    return false;

                }

                if (this.txtSyamei.Text == string.Empty)
                {
                    this.lblMsg.Text = "会社名は必須入力です";
                    this.txtSyamei.Focus();
                    return false;
                }

                if (this.txtTantou.Text == string.Empty)
                {
                    this.lblMsg.Text = "担当者は必須入力です";
                    this.txtTantou.Focus();
                    return false;
                }


                if (this.txtYearRep0.Text == string.Empty)
                {
                    this.lblMsg.Text = "報告日は必須入力です";
                    this.txtYearRep0.Focus();
                    return false;

                }

                if (this.txtMonthRep0.Text == string.Empty)
                {
                    this.lblMsg.Text = "報告日は必須入力です";
                    this.txtMonthRep0.Focus();
                    return false;

                }

                if (this.txtYearRep1.Text == string.Empty)
                {
                    this.lblMsg.Text = "報告日は必須入力です";
                    this.txtYearRep1.Focus();
                    return false;

                }

                if (this.txtMonthRep1.Text == string.Empty)
                {
                    this.lblMsg.Text = "報告日は必須入力です";
                    this.txtMonthRep1.Focus();
                    return false;

                }


                //数値
                if (Utility.IsNotNumber(this.txtYear.Text))
                {
                    this.lblMsg.Text = "送信日は半角数値を入れてください";
                    this.txtYear.Focus();
                    return false;
                }

                if (Utility.IsNotNumber(this.txtMonth.Text))
                {
                    this.lblMsg.Text = "送信日は半角数値を入れてください";
                    this.txtMonth.Focus();
                    return false;

                }

                if (Utility.IsNotNumber(this.txtDay.Text))
                {
                    this.lblMsg.Text = "送信日は半角数値を入れてください";
                    this.txtDay.Focus();
                    return false;
                }


                if (Utility.IsNotNumber(this.txtYearRep0.Text))
                {
                    this.lblMsg.Text = "報告日は半角数値を入れてください";
                    this.txtYearRep0.Focus();
                    return false;
                }

                if (Utility.IsNotNumber(this.txtMonthRep0.Text))
                {
                    this.lblMsg.Text = "報告日は半角数値を入れてください";
                    this.txtMonthRep0.Focus();
                    return false;
                }

                if (Utility.IsNotNumber(this.txtYearRep1.Text))
                {
                    this.lblMsg.Text = "報告日は半角数値を入れてください";
                    this.txtYearRep1.Focus();
                    return false;
                }

                if (Utility.IsNotNumber(this.txtMonthRep1.Text))
                {
                    this.lblMsg.Text = "報告日は半角数値を入れてください";
                    this.txtMonthRep1.Focus();
                    return false;
                }

                //報告日０と１が同じかどうか
                if (!this.txtYearRep0.Text.ToString().Equals(this.txtYearRep1.Text))
                {
                    this.lblMsg.Text = "報告日に矛盾があります";
                    this.txtYearRep0.Focus();
                    return false;
                }
                if (!this.txtMonthRep0.Text.ToString().Equals(this.txtMonthRep1.Text))
                {
                    this.lblMsg.Text = "報告日に矛盾があります";
                    this.txtMonthRep0.Focus();
                    return false;
                }




                //年のチェック
                if (int.Parse(this.txtYear.Text) < 1)
                {
                    this.txtYear.Focus();
                    return false;

                }
                if (int.Parse(this.txtYearRep0.Text) < 1)
                {
                    this.txtYearRep0.Focus();
                    return false;

                }
                if (int.Parse(this.txtYearRep1.Text) < 1)
                {
                    this.txtYearRep1.Focus();
                    return false;

                }

                //月のチェック
                if (int.Parse(this.txtMonth.Text) > 12 || int.Parse(this.txtMonth.Text) < 1)
                {
                    this.txtMonth.Focus();
                    return false;
                }

                if (int.Parse(this.txtMonthRep0.Text) > 12 || int.Parse(this.txtMonthRep0.Text) < 1)
                {
                    this.txtMonthRep0.Focus();
                    return false;
                }

                if (int.Parse(this.txtMonthRep1.Text) > 12 || int.Parse(this.txtMonthRep1.Text) < 1)
                {
                    this.txtMonthRep1.Focus();
                    return false;
                }

                //日のチェック
                if (int.Parse(this.txtDay.Text) > 31 || int.Parse(this.txtDay.Text) < 1)
                {
                    this.txtDay.Focus();
                    return false;
                }



                return true;

            }

            /// <summary>
            /// 水戸入力チェック
            /// </summary>
            /// <returns></returns>
            private bool MitoIsValid()
            {
                //貨物
                if (Utility.IsNotNumber(this.txtMito_Kamotu1.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtMito_Kamotu2.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtMito_Kamotu3.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtMito_Kamotu4.Text))
                {
                    return false;
                }

                //バス
                if (Utility.IsNotNumber(this.txtMito_Bus1.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtMito_Bus2.Text))
                {
                    return false;
                }

                //乗用及び貨物車
                if (Utility.IsNotNumber(this.txtMito_JK_J1.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtMito_JK_K1.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtMito_JK_J2.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtMito_JK_K2.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtMito_JK_J3.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtMito_JK_K3.Text))
                {
                    return false;
                }

                //小計
                if (Utility.IsNotNumber(this.txtMito_SubTotal1.Text))
                {
                    return false;
                }

                //合計
                if (Utility.IsNotNumber(this.txtMito_Total1.Text))
                {
                    return false;
                }


                return true;
            }

            private bool TuchiuraIsValid()
            {
                //貨物
                if (Utility.IsNotNumber(this.txtTuchiura_Kamotu1.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtTuchiura_Kamotu2.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtTuchiura_Kamotu3.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtTuchiura_Kamotu4.Text))
                {
                    return false;
                }

                //バス
                if (Utility.IsNotNumber(this.txtTuchiura_Bus1.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtTuchiura_Bus2.Text))
                {
                    return false;
                }

                //乗用及び貨物車
                if (Utility.IsNotNumber(this.txtTuchiura_JK_J1.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtTuchiura_JK_K1.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtTuchiura_JK_J2.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtTuchiura_JK_K2.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtTuchiura_JK_J3.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtTuchiura_JK_K3.Text))
                {
                    return false;
                }

                //小計
                if (Utility.IsNotNumber(this.txtTuchiura_SubTotal1.Text))
                {
                    return false;
                }

                //合計
                if (Utility.IsNotNumber(this.txtTuchiura_Total1.Text))
                {
                    return false;
                }


                return true;
            }

            private bool TukubaIsValid()
            {
                //貨物
                if (Utility.IsNotNumber(this.txtTukuba_Kamotu1.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtTukuba_Kamotu2.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtTukuba_Kamotu3.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtTukuba_Kamotu4.Text))
                {
                    return false;
                }

                //バス
                if (Utility.IsNotNumber(this.txtTukuba_Bus1.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtTukuba_Bus2.Text))
                {
                    return false;
                }

                //乗用及び貨物車
                if (Utility.IsNotNumber(this.txtTukuba_JK_J1.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtTukuba_JK_K1.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtTukuba_JK_J2.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtTukuba_JK_K2.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtTukuba_JK_J3.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtTukuba_JK_K3.Text))
                {
                    return false;
                }

                //小計
                if (Utility.IsNotNumber(this.txtTukuba_SubTotal1.Text))
                {
                    return false;
                }

                //合計
                if (Utility.IsNotNumber(this.txtTukuba_Total1.Text))
                {
                    return false;
                }


                return true;
            }

            private bool SonotaIsValid()
            {
                //貨物
                if (Utility.IsNotNumber(this.txtSonota_Kamotu1.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtSonota_Kamotu2.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtSonota_Kamotu3.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtSonota_Kamotu4.Text))
                {
                    return false;
                }

                //バス
                if (Utility.IsNotNumber(this.txtSonota_Bus1.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtSonota_Bus2.Text))
                {
                    return false;
                }

                //乗用及び貨物車
                if (Utility.IsNotNumber(this.txtSonota_JK_J1.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtSonota_JK_K1.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtSonota_JK_J2.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtSonota_JK_K2.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtSonota_JK_J3.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtSonota_JK_K3.Text))
                {
                    return false;
                }

                //小計
                if (Utility.IsNotNumber(this.txtSonota_SubTotal1.Text))
                {
                    return false;
                }

                //合計
                if (Utility.IsNotNumber(this.txtSonota_Total1.Text))
                {
                    return false;
                }


                return true;
            }

            private bool GoukeiIsValid()
            {
                //貨物
                if (Utility.IsNotNumber(this.txtGoukei_Kamotu1.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtGoukei_Kamotu2.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtGoukei_Kamotu3.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtGoukei_Kamotu4.Text))
                {
                    return false;
                }

                //バス
                if (Utility.IsNotNumber(this.txtGoukei_Bus1.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtGoukei_Bus2.Text))
                {
                    return false;
                }

                //乗用及び貨物車
                if (Utility.IsNotNumber(this.txtGoukei_JK_J1.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtGoukei_JK_K1.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtGoukei_JK_J2.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtGoukei_JK_K2.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtGoukei_JK_J3.Text))
                {
                    return false;
                }
                if (Utility.IsNotNumber(this.txtGoukei_JK_K3.Text))
                {
                    return false;
                }

                //小計
                if (Utility.IsNotNumber(this.txtGoukei_SubTotal1.Text))
                {
                    return false;
                }

                //合計
                if (Utility.IsNotNumber(this.txtGoukei_Total1.Text))
                {
                    return false;
                }


                return true;
            }


            #endregion
        /*
            #region "コンバートメソッド"

            private void Header_ConvertNothingTo0()
            {

                this.txtYear.Text = Utility.ToHankaku(this.txtYear.Text.Trim());

                this.txtMonth.Text = Utility.ToHankaku(this.txtMonth.Text.Trim());

                this.txtDay.Text = Utility.ToHankaku(this.txtDay.Text.Trim());

                this.txtYearRep0.Text = Utility.ToHankaku(this.txtYearRep0.Text.Trim());

                this.txtYearRep1.Text = Utility.ToHankaku(this.txtYearRep1.Text.Trim());

                this.txtMonthRep0.Text = Utility.ToHankaku(this.txtMonthRep0.Text.Trim());

                this.txtMonthRep1.Text = Utility.ToHankaku(this.txtMonthRep1.Text.Trim());


            }


            /// <summary>
            /// 何も入力されてないときは、半角の0に強制変換
            /// </summary>
            private void Mito_ConvertNothingTo0()
            {
                //貨物
                this.txtMito_Kamotu1.Text = Utility.ToHankaku(this.txtMito_Kamotu1.Text.Trim());
                if (this.txtMito_Kamotu1.Text == string.Empty)
                {
                    this.txtMito_Kamotu1.Text = "0";

                }

                this.txtMito_Kamotu2.Text = Utility.ToHankaku(this.txtMito_Kamotu2.Text.Trim());
                if (this.txtMito_Kamotu2.Text == string.Empty)
                {
                    this.txtMito_Kamotu2.Text = "0";

                }

                this.txtMito_Kamotu3.Text = Utility.ToHankaku(this.txtMito_Kamotu3.Text.Trim());
                if (this.txtMito_Kamotu3.Text == string.Empty)
                {
                    this.txtMito_Kamotu3.Text = "0";

                }

                this.txtMito_Kamotu4.Text = Utility.ToHankaku(this.txtMito_Kamotu4.Text.Trim());
                if (this.txtMito_Kamotu4.Text == string.Empty)
                {
                    this.txtMito_Kamotu4.Text = "0";

                }


                //バス
                this.txtMito_Bus1.Text = Utility.ToHankaku(this.txtMito_Bus1.Text.Trim());
                if (this.txtMito_Bus1.Text == string.Empty)
                {
                    this.txtMito_Bus1.Text = "0";

                }

                this.txtMito_Bus2.Text = Utility.ToHankaku(this.txtMito_Bus2.Text.Trim());
                if (this.txtMito_Bus2.Text == string.Empty)
                {
                    this.txtMito_Bus2.Text = "0";

                }


                //乗用及び貨物車                
                this.txtMito_JK_J1.Text = Utility.ToHankaku(this.txtMito_JK_J1.Text.Trim());
                if (this.txtMito_JK_J1.Text == string.Empty)
                {
                    this.txtMito_JK_J1.Text = "0";

                }

                this.txtMito_JK_K1.Text = Utility.ToHankaku(this.txtMito_JK_K1.Text.Trim());
                if (this.txtMito_JK_K1.Text == string.Empty)
                {
                    this.txtMito_JK_K1.Text = "0";

                }

                this.txtMito_JK_J2.Text = Utility.ToHankaku(this.txtMito_JK_J2.Text.Trim());
                if (this.txtMito_JK_J2.Text == string.Empty)
                {
                    this.txtMito_JK_J2.Text = "0";

                }

                this.txtMito_JK_K2.Text = Utility.ToHankaku(this.txtMito_JK_K2.Text.Trim());
                if (this.txtMito_JK_K2.Text == string.Empty)
                {
                    this.txtMito_JK_K2.Text = "0";

                }

                this.txtMito_JK_J3.Text = Utility.ToHankaku(this.txtMito_JK_J3.Text.Trim());
                if (this.txtMito_JK_J3.Text == string.Empty)
                {
                    this.txtMito_JK_J3.Text = "0";

                }

                this.txtMito_JK_K3.Text = Utility.ToHankaku(this.txtMito_JK_K3.Text.Trim());
                if (this.txtMito_JK_K3.Text == string.Empty)
                {
                    this.txtMito_JK_K3.Text = "0";

                }

                //小計
                this.txtMito_SubTotal1.Text = Utility.ToHankaku(this.txtMito_SubTotal1.Text.Trim());
                if (this.txtMito_SubTotal1.Text == string.Empty)
                {
                    this.txtMito_SubTotal1.Text = "0";

                }

                //合計
                this.txtMito_Total1.Text = Utility.ToHankaku(this.txtMito_Total1.Text.Trim());
                if (this.txtMito_Total1.Text == string.Empty)
                {
                    this.txtMito_Total1.Text = "0";

                }

            }

            /// <summary>
            /// 何も入力されてないときは、半角の0に強制変換
            /// </summary>
            private void Tuchiura_ConvertNothingTo0()
            {
                //貨物
                this.txtTuchiura_Kamotu1.Text = Utility.ToHankaku(this.txtTuchiura_Kamotu1.Text.Trim());
                if (this.txtTuchiura_Kamotu1.Text == string.Empty)
                {
                    this.txtTuchiura_Kamotu1.Text = "0";

                }

                this.txtTuchiura_Kamotu2.Text = Utility.ToHankaku(this.txtTuchiura_Kamotu2.Text.Trim());
                if (this.txtTuchiura_Kamotu2.Text == string.Empty)
                {
                    this.txtTuchiura_Kamotu2.Text = "0";

                }

                this.txtTuchiura_Kamotu3.Text = Utility.ToHankaku(this.txtTuchiura_Kamotu3.Text.Trim());
                if (this.txtTuchiura_Kamotu3.Text == string.Empty)
                {
                    this.txtTuchiura_Kamotu3.Text = "0";

                }

                this.txtTuchiura_Kamotu4.Text = Utility.ToHankaku(this.txtTuchiura_Kamotu4.Text.Trim());
                if (this.txtTuchiura_Kamotu4.Text == string.Empty)
                {
                    this.txtTuchiura_Kamotu4.Text = "0";

                }


                //バス
                this.txtTuchiura_Bus1.Text = Utility.ToHankaku(this.txtTuchiura_Bus1.Text.Trim());
                if (this.txtTuchiura_Bus1.Text == string.Empty)
                {
                    this.txtTuchiura_Bus1.Text = "0";

                }

                this.txtTuchiura_Bus2.Text = Utility.ToHankaku(this.txtTuchiura_Bus2.Text.Trim());
                if (this.txtTuchiura_Bus2.Text == string.Empty)
                {
                    this.txtTuchiura_Bus2.Text = "0";

                }


                //乗用及び貨物車                
                this.txtTuchiura_JK_J1.Text = Utility.ToHankaku(this.txtTuchiura_JK_J1.Text.Trim());
                if (this.txtTuchiura_JK_J1.Text == string.Empty)
                {
                    this.txtTuchiura_JK_J1.Text = "0";

                }

                this.txtTuchiura_JK_K1.Text = Utility.ToHankaku(this.txtTuchiura_JK_K1.Text.Trim());
                if (this.txtTuchiura_JK_K1.Text == string.Empty)
                {
                    this.txtTuchiura_JK_K1.Text = "0";

                }

                this.txtTuchiura_JK_J2.Text = Utility.ToHankaku(this.txtTuchiura_JK_J2.Text.Trim());
                if (this.txtTuchiura_JK_J2.Text == string.Empty)
                {
                    this.txtTuchiura_JK_J2.Text = "0";

                }

                this.txtTuchiura_JK_K2.Text = Utility.ToHankaku(this.txtTuchiura_JK_K2.Text.Trim());
                if (this.txtTuchiura_JK_K2.Text == string.Empty)
                {
                    this.txtTuchiura_JK_K2.Text = "0";

                }

                this.txtTuchiura_JK_J3.Text = Utility.ToHankaku(this.txtTuchiura_JK_J3.Text.Trim());
                if (this.txtTuchiura_JK_J3.Text == string.Empty)
                {
                    this.txtTuchiura_JK_J3.Text = "0";

                }

                this.txtTuchiura_JK_K3.Text = Utility.ToHankaku(this.txtTuchiura_JK_K3.Text.Trim());
                if (this.txtTuchiura_JK_K3.Text == string.Empty)
                {
                    this.txtTuchiura_JK_K3.Text = "0";

                }

                //小計
                this.txtTuchiura_SubTotal1.Text = Utility.ToHankaku(this.txtTuchiura_SubTotal1.Text.Trim());
                if (this.txtTuchiura_SubTotal1.Text == string.Empty)
                {
                    this.txtTuchiura_SubTotal1.Text = "0";

                }

                //合計
                this.txtTuchiura_Total1.Text = Utility.ToHankaku(this.txtTuchiura_Total1.Text.Trim());
                if (this.txtTuchiura_Total1.Text == string.Empty)
                {
                    this.txtTuchiura_Total1.Text = "0";

                }

            }

            /// <summary>
            /// 何も入力されてないときは、半角の0に強制変換
            /// </summary>
            private void Tukuba_ConvertNothingTo0()
            {
                //貨物
                this.txtTukuba_Kamotu1.Text = Utility.ToHankaku(this.txtTukuba_Kamotu1.Text.Trim());
                if (this.txtTukuba_Kamotu1.Text == string.Empty)
                {
                    this.txtTukuba_Kamotu1.Text = "0";

                }

                this.txtTukuba_Kamotu2.Text = Utility.ToHankaku(this.txtTukuba_Kamotu2.Text.Trim());
                if (this.txtTukuba_Kamotu2.Text == string.Empty)
                {
                    this.txtTukuba_Kamotu2.Text = "0";

                }

                this.txtTukuba_Kamotu3.Text = Utility.ToHankaku(this.txtTukuba_Kamotu3.Text.Trim());
                if (this.txtTukuba_Kamotu3.Text == string.Empty)
                {
                    this.txtTukuba_Kamotu3.Text = "0";

                }

                this.txtTukuba_Kamotu4.Text = Utility.ToHankaku(this.txtTukuba_Kamotu4.Text.Trim());
                if (this.txtTukuba_Kamotu4.Text == string.Empty)
                {
                    this.txtTukuba_Kamotu4.Text = "0";

                }


                //バス
                this.txtTukuba_Bus1.Text = Utility.ToHankaku(this.txtTukuba_Bus1.Text.Trim());
                if (this.txtTukuba_Bus1.Text == string.Empty)
                {
                    this.txtTukuba_Bus1.Text = "0";

                }

                this.txtTukuba_Bus2.Text = Utility.ToHankaku(this.txtTukuba_Bus2.Text.Trim());
                if (this.txtTukuba_Bus2.Text == string.Empty)
                {
                    this.txtTukuba_Bus2.Text = "0";

                }


                //乗用及び貨物車                
                this.txtTukuba_JK_J1.Text = Utility.ToHankaku(this.txtTukuba_JK_J1.Text.Trim());
                if (this.txtTukuba_JK_J1.Text == string.Empty)
                {
                    this.txtTukuba_JK_J1.Text = "0";

                }

                this.txtTukuba_JK_K1.Text = Utility.ToHankaku(this.txtTukuba_JK_K1.Text.Trim());
                if (this.txtTukuba_JK_K1.Text == string.Empty)
                {
                    this.txtTukuba_JK_K1.Text = "0";

                }

                this.txtTukuba_JK_J2.Text = Utility.ToHankaku(this.txtTukuba_JK_J2.Text.Trim());
                if (this.txtTukuba_JK_J2.Text == string.Empty)
                {
                    this.txtTukuba_JK_J2.Text = "0";

                }

                this.txtTukuba_JK_K2.Text = Utility.ToHankaku(this.txtTukuba_JK_K2.Text.Trim());
                if (this.txtTukuba_JK_K2.Text == string.Empty)
                {
                    this.txtTukuba_JK_K2.Text = "0";

                }

                this.txtTukuba_JK_J3.Text = Utility.ToHankaku(this.txtTukuba_JK_J3.Text.Trim());
                if (this.txtTukuba_JK_J3.Text == string.Empty)
                {
                    this.txtTukuba_JK_J3.Text = "0";

                }

                this.txtTukuba_JK_K3.Text = Utility.ToHankaku(this.txtTukuba_JK_K3.Text.Trim());
                if (this.txtTukuba_JK_K3.Text == string.Empty)
                {
                    this.txtTukuba_JK_K3.Text = "0";

                }

                //小計
                this.txtTukuba_SubTotal1.Text = Utility.ToHankaku(this.txtTukuba_SubTotal1.Text.Trim());
                if (this.txtTukuba_SubTotal1.Text == string.Empty)
                {
                    this.txtTukuba_SubTotal1.Text = "0";

                }

                //合計
                this.txtTukuba_Total1.Text = Utility.ToHankaku(this.txtTukuba_Total1.Text.Trim());
                if (this.txtTukuba_Total1.Text == string.Empty)
                {
                    this.txtTukuba_Total1.Text = "0";

                }

            }

            /// <summary>
            /// 何も入力されてないときは、半角の0に強制変換
            /// </summary>
            private void Sonota_ConvertNothingTo0()
            {
                //貨物
                this.txtSonota_Kamotu1.Text = Utility.ToHankaku(this.txtSonota_Kamotu1.Text.Trim());
                if (this.txtSonota_Kamotu1.Text == string.Empty)
                {
                    this.txtSonota_Kamotu1.Text = "0";

                }

                this.txtSonota_Kamotu2.Text = Utility.ToHankaku(this.txtSonota_Kamotu2.Text.Trim());
                if (this.txtSonota_Kamotu2.Text == string.Empty)
                {
                    this.txtSonota_Kamotu2.Text = "0";

                }

                this.txtSonota_Kamotu3.Text = Utility.ToHankaku(this.txtSonota_Kamotu3.Text.Trim());
                if (this.txtSonota_Kamotu3.Text == string.Empty)
                {
                    this.txtSonota_Kamotu3.Text = "0";

                }

                this.txtSonota_Kamotu4.Text = Utility.ToHankaku(this.txtSonota_Kamotu4.Text.Trim());
                if (this.txtSonota_Kamotu4.Text == string.Empty)
                {
                    this.txtSonota_Kamotu4.Text = "0";

                }


                //バス
                this.txtSonota_Bus1.Text = Utility.ToHankaku(this.txtSonota_Bus1.Text.Trim());
                if (this.txtSonota_Bus1.Text == string.Empty)
                {
                    this.txtSonota_Bus1.Text = "0";

                }

                this.txtSonota_Bus2.Text = Utility.ToHankaku(this.txtSonota_Bus2.Text.Trim());
                if (this.txtSonota_Bus2.Text == string.Empty)
                {
                    this.txtSonota_Bus2.Text = "0";

                }


                //乗用及び貨物車                
                this.txtSonota_JK_J1.Text = Utility.ToHankaku(this.txtSonota_JK_J1.Text.Trim());
                if (this.txtSonota_JK_J1.Text == string.Empty)
                {
                    this.txtSonota_JK_J1.Text = "0";

                }

                this.txtSonota_JK_K1.Text = Utility.ToHankaku(this.txtSonota_JK_K1.Text.Trim());
                if (this.txtSonota_JK_K1.Text == string.Empty)
                {
                    this.txtSonota_JK_K1.Text = "0";

                }

                this.txtSonota_JK_J2.Text = Utility.ToHankaku(this.txtSonota_JK_J2.Text.Trim());
                if (this.txtSonota_JK_J2.Text == string.Empty)
                {
                    this.txtSonota_JK_J2.Text = "0";

                }

                this.txtSonota_JK_K2.Text = Utility.ToHankaku(this.txtSonota_JK_K2.Text.Trim());
                if (this.txtSonota_JK_K2.Text == string.Empty)
                {
                    this.txtSonota_JK_K2.Text = "0";

                }

                this.txtSonota_JK_J3.Text = Utility.ToHankaku(this.txtSonota_JK_J3.Text.Trim());
                if (this.txtSonota_JK_J3.Text == string.Empty)
                {
                    this.txtSonota_JK_J3.Text = "0";

                }

                this.txtSonota_JK_K3.Text = Utility.ToHankaku(this.txtSonota_JK_K3.Text.Trim());
                if (this.txtSonota_JK_K3.Text == string.Empty)
                {
                    this.txtSonota_JK_K3.Text = "0";

                }

                //小計
                this.txtSonota_SubTotal1.Text = Utility.ToHankaku(this.txtSonota_SubTotal1.Text.Trim());
                if (this.txtSonota_SubTotal1.Text == string.Empty)
                {
                    this.txtSonota_SubTotal1.Text = "0";

                }

                //合計
                this.txtSonota_Total1.Text = Utility.ToHankaku(this.txtSonota_Total1.Text.Trim());
                if (this.txtSonota_Total1.Text == string.Empty)
                {
                    this.txtSonota_Total1.Text = "0";

                }

            }

            /// <summary>
            /// 何も入力されてないときは、半角の0に強制変換
            /// </summary>
            private void Goukei_ConvertNothingTo0()
            {
                //貨物
                this.txtGoukei_Kamotu1.Text = Utility.ToHankaku(this.txtGoukei_Kamotu1.Text.Trim());
                if (this.txtGoukei_Kamotu1.Text == string.Empty)
                {
                    this.txtGoukei_Kamotu1.Text = "0";

                }

                this.txtGoukei_Kamotu2.Text = Utility.ToHankaku(this.txtGoukei_Kamotu2.Text.Trim());
                if (this.txtGoukei_Kamotu2.Text == string.Empty)
                {
                    this.txtGoukei_Kamotu2.Text = "0";

                }

                this.txtGoukei_Kamotu3.Text = Utility.ToHankaku(this.txtGoukei_Kamotu3.Text.Trim());
                if (this.txtGoukei_Kamotu3.Text == string.Empty)
                {
                    this.txtGoukei_Kamotu3.Text = "0";

                }

                this.txtGoukei_Kamotu4.Text = Utility.ToHankaku(this.txtGoukei_Kamotu4.Text.Trim());
                if (this.txtGoukei_Kamotu4.Text == string.Empty)
                {
                    this.txtGoukei_Kamotu4.Text = "0";

                }


                //バス
                this.txtGoukei_Bus1.Text = Utility.ToHankaku(this.txtGoukei_Bus1.Text.Trim());
                if (this.txtGoukei_Bus1.Text == string.Empty)
                {
                    this.txtGoukei_Bus1.Text = "0";

                }

                this.txtGoukei_Bus2.Text = Utility.ToHankaku(this.txtGoukei_Bus2.Text.Trim());
                if (this.txtGoukei_Bus2.Text == string.Empty)
                {
                    this.txtGoukei_Bus2.Text = "0";

                }


                //乗用及び貨物車                
                this.txtGoukei_JK_J1.Text = Utility.ToHankaku(this.txtGoukei_JK_J1.Text.Trim());
                if (this.txtGoukei_JK_J1.Text == string.Empty)
                {
                    this.txtGoukei_JK_J1.Text = "0";

                }

                this.txtGoukei_JK_K1.Text = Utility.ToHankaku(this.txtGoukei_JK_K1.Text.Trim());
                if (this.txtGoukei_JK_K1.Text == string.Empty)
                {
                    this.txtGoukei_JK_K1.Text = "0";

                }

                this.txtGoukei_JK_J2.Text = Utility.ToHankaku(this.txtGoukei_JK_J2.Text.Trim());
                if (this.txtGoukei_JK_J2.Text == string.Empty)
                {
                    this.txtGoukei_JK_J2.Text = "0";

                }

                this.txtGoukei_JK_K2.Text = Utility.ToHankaku(this.txtGoukei_JK_K2.Text.Trim());
                if (this.txtGoukei_JK_K2.Text == string.Empty)
                {
                    this.txtGoukei_JK_K2.Text = "0";

                }

                this.txtGoukei_JK_J3.Text = Utility.ToHankaku(this.txtGoukei_JK_J3.Text.Trim());
                if (this.txtGoukei_JK_J3.Text == string.Empty)
                {
                    this.txtGoukei_JK_J3.Text = "0";

                }

                this.txtGoukei_JK_K3.Text = Utility.ToHankaku(this.txtGoukei_JK_K3.Text.Trim());
                if (this.txtGoukei_JK_K3.Text == string.Empty)
                {
                    this.txtGoukei_JK_K3.Text = "0";

                }

                //小計
                this.txtGoukei_SubTotal1.Text = Utility.ToHankaku(this.txtGoukei_SubTotal1.Text.Trim());
                if (this.txtGoukei_SubTotal1.Text == string.Empty)
                {
                    this.txtGoukei_SubTotal1.Text = "0";

                }

                //合計
                this.txtGoukei_Total1.Text = Utility.ToHankaku(this.txtGoukei_Total1.Text.Trim());
                if (this.txtGoukei_Total1.Text == string.Empty)
                {
                    this.txtGoukei_Total1.Text = "0";

                }

            }

            #endregion

        */
            #region "初期化メソッド"
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
                this.btnKariInvoice.Visible = false;

                ////メニューを会員、自販連で分ける
                //if (jadaUser)
                //{
                //    linkMenu.NavigateUrl = URL.MENU_JADA;
                //}
                //else
                //{
                //    linkMenu.NavigateUrl = URL.MENU_DEALER;
                //}
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
            if (this.Session["Member"].ToString().Trim() == "1")
            {
                //会員
                jadaUser = false;
            }
            else 
            {   
                //自販連
                jadaUser = true;
                qCOCODE = Page.Request.QueryString.Get("COCODE");
                //年月ももらってセット


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
            //入力チェックTODO サーバーサイドのチェック
            //サーバー再度でも念のため

            //データを整える
            //何もないときは0に変換する
            ////ヘッダー
            //this.Header_ConvertNothingTo0();
            ////水戸
            //this.Mito_ConvertNothingTo0();
            ////土浦
            //this.Tuchiura_ConvertNothingTo0();
            ////つくば
            //this.Tukuba_ConvertNothingTo0();
            ////その他
            //this.Sonota_ConvertNothingTo0();
            ////合計
            //this.Goukei_ConvertNothingTo0();


            /**********/
            //チェック//
            /**********/
            //ヘッダー
            if (!this.HeaderIsValid())
            {
                return;
            }

            //水戸
            if (!this.MitoIsValid())
            {
                this.lblMsg.Text = "水戸の欄に数字以外の項目が入力されています。";
                return;
            }
            //土浦
            if (!this.TuchiuraIsValid())
            {
                this.lblMsg.Text = "土浦の欄に数字以外の項目が入力されています。";
                return;
            }
            //つくば
            if (!this.TukubaIsValid())
            {
                this.lblMsg.Text = "つくばの欄に数字以外の項目が入力されています。";
                return;
            }
            //その他
            if (!this.SonotaIsValid())
            {
                this.lblMsg.Text = "その他の欄に数字以外の項目が入力されています。";
                return;
            }
            //合計
            if (!this.GoukeiIsValid())
            {
                this.lblMsg.Text = "合計の欄に数字以外の項目が入力されています。";
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
                            this.btnPrint.Visible = true;
                            this.btnKariInvoice.Visible = true;

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

            
            string js = "";
            js += "<script language='JavaScript'>";
            js += "window.open('" + URL.REPORT_JISSEKI_REPORT_JS + "')";
            js += "</script>";


            Page.ClientScript.RegisterStartupScript(this.GetType(), "startup", js);


        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect(URL.LOGIN_DEALER);
        }

        protected void btnKariInvoice_Click(object sender, EventArgs e)
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

            string js = "";
            js += "<script language='JavaScript'>";
            js += "window.open('" + URL.REPORT_KARI_INVOICE_REPORT_JS + "')";
            js += "</script>";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "startup", js);

        }

        protected void btnlinkMenu_Click(object sender, EventArgs e)
        {

            try
            {
                if (Session["Member"].ToString().Trim() == "1")
                {
                    //会員
                    Response.Redirect(URL.MENU_DEALER);
                }
                else
                {
                    //自販連
                    Response.Redirect(URL.MENU_JADA);

                }
            }
            catch
            {
            }

        }
        
    }
}