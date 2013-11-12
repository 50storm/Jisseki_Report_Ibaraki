<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="delete_jisseki.aspx.cs" Inherits=" Jisseki_Report_Ibaraki.common.delete_jisseki" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link rel="stylesheet" href="../Css/input.css" type="text/css" />
<link rel="stylesheet" href="../Css/print.css" type="text/css" media="print" />
<style type="text/css">
        #linkMenu
        {
        	color:#0000ff;
        	background:transparent;
        	font-size:18px;
        }
        #btnLogOut
        {
        	/*LinkButtonが動作しない*/
        	/*LinkButton風にＣＳＳを当てる*/
        	text-decoration:underline;
            color:#0000ff;
            border:none;
            background:transparent;
            font-size:18px;
        }
</style>

    <title>新車登録台数報告書【削除】</title>
</head>
<body>
<div id="Wrapper">
<form id="form1" runat="server" onsubmit="" >
<!--メニュー-->
<div style="float:left;">
    <asp:HyperLink ID="linkMenu" runat="server" NavigateUrl="~/member/menu/Dealer.aspx">メニュー</asp:HyperLink>
    <asp:Button ID="btnLogOut" runat="server" Text="ログアウト"  />    
</div>
<!--メッセージ-->
<div id="Message" >
    <asp:Label ID="lblMsg" runat="server" BackColor="#33CCFF"></asp:Label>
</div >
<!--元号-->
<div id="Era">
    <p>
        登録日：
		<asp:Label ID="lblEra"  runat="server" Text="元号">
		</asp:Label><asp:TextBox ID="txtYear" runat="server" Width="20px"></asp:TextBox>
		年
		<asp:TextBox  ID="txtMonth" runat="server" Width="20px"></asp:TextBox>
		月
		<asp:TextBox  ID="txtDay" runat="server" Width="20px"></asp:TextBox>
		日
    </p>
</div>
<!--宛先-->
<div id="To">
    <p><span>社団法人</span>日本自動車販売協会連合会茨城県支部長　殿</p>
</div>
<!--社名-->
<div id="Coname">
    <p>社　名<asp:TextBox ID="txtSyamei" runat="server"></asp:TextBox></p>
</div>
<!--担当者-->
<div id="Tantou">
<p>担当者　　氏　名<asp:TextBox ID="txtTantou" runat="server"></asp:TextBox></p>
</div>
<!--タイトル-->
<div id="Title">
<p>
	<asp:Label ID="lblEraRep0" runat="server" Text="元号"></asp:Label>
	<asp:TextBox ID="txtYearRep0" runat="server" Width="20px" onblur="setYear0ToYear1();" ></asp:TextBox>年
	<asp:TextBox ID="txtMonthRep0" runat="server" Text="99"  onblur="setMonth0ToMonth1();" Width="20px"></asp:TextBox>
	月分新車登録台数報告書
</p>
</div>
<!--説明文-->
<div id="NarratvieText">
<p id="p1" class="p1">
	当初の
	<asp:Label ID="lblEraRep1" runat="server" Text="元号"></asp:Label>
	<asp:TextBox ID="txtYearRep1" runat="server" Width="20px" onblur="setYear1ToYear0();" ></asp:TextBox>年
	<span><asp:TextBox ID="txtMonthRep1" runat="server"  Text="99"   onblur="setMonth1ToMonth0();" Width="20px"></asp:TextBox></span>
	月分新車新登録、届出台数は次のとおりであり
</p>
<p id="p2" class="p1">
	ますから報告します。
</p>
</div>
<!--次-->
<div id="Next">
    <p>次</p>
</div>
<div>
<table id="tblReport">
<thead>
<tr>
    <th></th><th>区　分</th><th colspan="2">水　戸</th><th colspan="2">土　浦</th><th colspan="2">つ　く　ば</th><th colspan="2">その他</th><th colspan="2">合計</th>
</tr>
</thead>
<tbody>
<!--貨物車-->
<tr>
    <td rowspan="4">貨物車</td>
    <td>7　t　以　上</td>
    <td colspan="2"><asp:TextBox ID="txtMito_Kamotu1" runat="server"></asp:TextBox></td>
    <td colspan="2"><asp:TextBox ID="txtTuchiura_Kamotu1" runat="server"></asp:TextBox></td>
    <td colspan="2"><asp:TextBox ID="txtTukuba_Kamotu1" runat="server"></asp:TextBox></td>
    <td colspan="2"><asp:TextBox ID="txtSonota_Kamotu1" runat="server"></asp:TextBox></td>
    <td  colspan="2"><asp:TextBox ID="txtGoukei_Kamotu1" runat="server"></asp:TextBox></td>
</tr>
<tr>
    <td>6.9t ～ 5t　以　上</td>
    <td colspan="2"><asp:TextBox ID="txtMito_Kamotu2" runat="server"></asp:TextBox></td>
    <td colspan="2"><asp:TextBox ID="txtTuchiura_Kamotu2" runat="server"></asp:TextBox></td>
    <td colspan="2"><asp:TextBox ID="txtTukuba_Kamotu2" runat="server"></asp:TextBox></td>
    <td colspan="2"><asp:TextBox ID="txtSonota_Kamotu2" runat="server"></asp:TextBox></td>
    <td colspan="2"><asp:TextBox ID="txtGoukei_Kamotu2" runat="server"></asp:TextBox></td>
</tr>
<tr>
   <td>4.9t ～ 3t　以　上</td>
    <td colspan="2"><asp:TextBox ID="txtMito_Kamotu3" runat="server"></asp:TextBox></td>
    <td colspan="2"><asp:TextBox ID="txtTuchiura_Kamotu3" runat="server"></asp:TextBox></td>
    <td colspan="2"><asp:TextBox ID="txtTukuba_Kamotu3" runat="server"></asp:TextBox></td>
    <td colspan="2"><asp:TextBox ID="txtSonota_Kamotu3" runat="server"></asp:TextBox></td>
    <td colspan="2"><asp:TextBox ID="txtGoukei_Kamotu3" runat="server"></asp:TextBox></td>

</tr>
<tr>
   <td>2.9t ～ 2.5t　以　上</td>
    <td colspan="2"><asp:TextBox ID="txtMito_Kamotu4" runat="server"></asp:TextBox></td>
    <td colspan="2"><asp:TextBox ID="txtTuchiura_Kamotu4" runat="server"></asp:TextBox></td>
    <td colspan="2"><asp:TextBox ID="txtTukuba_Kamotu4" runat="server"></asp:TextBox></td>
    <td colspan="2"><asp:TextBox ID="txtSonota_Kamotu4" runat="server"></asp:TextBox></td>
    <td colspan="2"><asp:TextBox ID="txtGoukei_Kamotu4" runat="server"></asp:TextBox></td>
</tr>
<!--バス-->
<tr>
    <td rowspan="2">バ　ス</td>
    <td>定員30人以上</td>
    <td colspan="2"><asp:TextBox ID="txtMito_Bus1" runat="server"></asp:TextBox></td>
    <td colspan="2"><asp:TextBox ID="txtTuchiura_Bus1" runat="server"></asp:TextBox></td>
    <td colspan="2"><asp:TextBox ID="txtTukuba_Bus1" runat="server"></asp:TextBox></td>
    <td colspan="2"><asp:TextBox ID="txtSonota_Bus1" runat="server"></asp:TextBox></td>
    <td colspan="2"><asp:TextBox ID="txtGoukei_Bus1" runat="server"></asp:TextBox></td>
</tr>
<tr>
   <td>定員29人以下(乗用車を除く)</td>
   <td colspan="2"><asp:TextBox ID="txtMito_Bus2" runat="server"></asp:TextBox></td>
   <td colspan="2"><asp:TextBox ID="txtTuchiura_Bus2" runat="server"></asp:TextBox></td>
   <td colspan="2"><asp:TextBox ID="txtTukuba_Bus2" runat="server"></asp:TextBox></td>
   <td colspan="2"><asp:TextBox ID="txtSonota_Bus2" runat="server"></asp:TextBox></td>
   <td colspan="2"><asp:TextBox ID="txtGoukei_Bus2" runat="server"></asp:TextBox></td>
</tr>
<!--乗用車及び貨物車-->
<tr>
    <td rowspan="4">乗用及び貨物車</td><td >&nbsp;</td><td>乗用</td><td>貨物</td><td>乗用</td><td>貨物</td><td>乗用</td><td>貨物</td><td>乗用</td><td>貨物</td><td>乗用</td><td>貨物</td>
</tr>
<tr>
    <td ></td>
    <td><asp:TextBox ID="txtMito_JK_J1" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtMito_JK_K1" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtTuchiura_JK_J1" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtTuchiura_JK_K1" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtTukuba_JK_J1" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtTukuba_JK_K1" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSonota_JK_J1" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSonota_JK_K1" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtGoukei_JK_J1" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtGoukei_JK_K1" runat="server" Width="50px"></asp:TextBox></td>
</tr>
<tr>
    <td ></td>
    <td><asp:TextBox ID="txtMito_JK_J2" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtMito_JK_K2" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtTuchiura_JK_J2" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtTuchiura_JK_K2" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtTukuba_JK_J2" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtTukuba_JK_K2" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSonota_JK_J2" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSonota_JK_K2" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtGoukei_JK_J2" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtGoukei_JK_K2" runat="server" Width="50px"></asp:TextBox></td>
</tr>
<tr>
    <td ></td>
    <td><asp:TextBox ID="txtMito_JK_J3" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtMito_JK_K3" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtTuchiura_JK_J3" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtTuchiura_JK_K3" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtTukuba_JK_J3" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtTukuba_JK_K3" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSonota_JK_J3" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSonota_JK_K3" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtGoukei_JK_J3" runat="server" Width="50px"></asp:TextBox></td>
    <td><asp:TextBox ID="txtGoukei_JK_K3" runat="server" Width="50px"></asp:TextBox></td>


</tr>
<!--小計-->
<tr>
   <td colspan="2">小　計</td>
   <td colspan="2"><asp:TextBox ID="txtMito_SubTotal1" runat="server"></asp:TextBox></td>
   <td colspan="2"><asp:TextBox ID="txtTuchiura_SubTotal1" runat="server"></asp:TextBox></td>
   <td colspan="2"><asp:TextBox ID="txtTukuba_SubTotal1" runat="server"></asp:TextBox></td>
   <td colspan="2"><asp:TextBox ID="txtSonota_SubTotal1" runat="server"></asp:TextBox></td>
   <td colspan="2"><asp:TextBox ID="txtGoukei_SubTotal1" runat="server"></asp:TextBox></td>
</tr>
<!--合計-->
<tr>
   <td colspan="2">合　計</td>
   <td colspan="2"><asp:TextBox ID="txtMito_Total1" runat="server"></asp:TextBox></td>
   <td colspan="2"><asp:TextBox ID="txtTuchiura_Total1" runat="server"></asp:TextBox></td>
   <td colspan="2"><asp:TextBox ID="txtTukuba_Total1" runat="server"></asp:TextBox></td>
   <td colspan="2"><asp:TextBox ID="txtSonota_Total1" runat="server"></asp:TextBox></td>
   <td  colspan="2"><asp:TextBox ID="txtGoukei_Total1" runat="server"></asp:TextBox></td>
</tr>
</tbody>
</table>
</div>
<div>
<p>※直納及び県外からの登録も含む。</p>
<p>※翌月5日までに必ず報告のこと。</p>
</div>
<div>
<asp:Button ID="btnSubmit" runat="server" Text="削除" onclick="btnSubmit_Click" />
</div>
</form>
</div>
</body>
</html>