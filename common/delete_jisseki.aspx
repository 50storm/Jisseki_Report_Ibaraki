<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="delete_jisseki.aspx.cs" Inherits=" Jisseki_Report_Ibaraki.common.delete_jisseki" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<script type="text/javascript" src="../Scripts/inputJisseki_Key.js"></script>
<meta charset="UTF-8">
<link rel="stylesheet" href="../Css/input.css" type="text/css" />
<!--[if lt IE 8  ]>
<link rel="stylesheet" href="../Css/input_ie6.css" type="text/css" />
<![endif]-->
    <title>新車登録台数報告書【削除】</title>
</head>
<body>
<div id="Wrapper">
<form id="form1" runat="server" onsubmit="" >
    <!--メニュー-->	
	<div id="Menu" >
		<div id="Menu_Link">
    		<asp:HyperLink ID="linkMenu" runat="server" NavigateUrl="~/member/menu/Dealer.aspx">メニュー</asp:HyperLink>
        </div>
        <div id="Menu_Btn">
            <asp:Button ID="btnLogOut" runat="server" Text="ログアウト"    onclick="btnLogOut_Click" />
        </div>
    </div>
    <!--メッセージ-->
    <div id="Message" >
        <asp:Label ID="lblMsg" runat="server" BackColor="#33CCFF"></asp:Label>
	</div >
	<!--元号-->
	<div id="Era">
		<p>
			登録日：
			<asp:Label ID="lblEra"  runat="server" Text="元号"></asp:Label>
            <asp:TextBox ID="txtYear" runat="server"  MaxLength="2" class="Era"></asp:TextBox>
			年
			<asp:TextBox  ID="txtMonth" runat="server"  MaxLength="2"   class="Era"></asp:TextBox>
			月
			<asp:TextBox  ID="txtDay" runat="server"  MaxLength="2"   class="Era"></asp:TextBox>
			日
		</p>
	</div>
	<!--宛先-->
    <div id="DivTo">
        <div id="DivSyaDanHouZin">
	        <div id="DivSyaDan">
	        	一般社団
	        </div>
	        <div id="DivHouZin">
	        	法人
	        </div>
        </div>
        <div id="To">
	        	日本自動車販売協会連合会茨城県支部長　殿
        </div>
    </div>
	<!--社名-->
	<div id="Coname">
		<p>社　名<asp:TextBox ID="txtSyamei" runat="server" MaxLength="40"></asp:TextBox></p>
	</div>
	<!--担当者-->
	<div id="Tantou">
	    <p>担当者　　氏　名<asp:TextBox ID="txtTantou" runat="server" MaxLength="50"></asp:TextBox></p>
	</div>
	<!--タイトル-->
	<div id="Title">
	    <p>
	    	<asp:Label ID="lblEraRep0" runat="server" Text="元号"></asp:Label>
	    	<asp:TextBox ID="txtYearRep0" runat="server" 
                onblur="setYear0ToYear1();" MaxLength="2" ></asp:TextBox>年
	    	<asp:TextBox ID="txtMonthRep0" runat="server" Text="99"  
                onblur="setMonth0ToMonth1();"  MaxLength="2"></asp:TextBox>
	    	月分新車新規登録台数報告書
	    </p>
	</div>
	<!--説明文1-->
	<div id="NarratvieText1">
	    <p id="p1" class="p1">
	    	当社の
	    	<asp:Label ID="lblEraRep1" runat="server" Text="元号"></asp:Label>
	    	<asp:TextBox ID="txtYearRep1" runat="server" 
                onblur="setYear1ToYear0();" MaxLength="2" ></asp:TextBox>年
	    	<span><asp:TextBox ID="txtMonthRep1" runat="server"  Text="99"    onblur="setMonth1ToMonth0();"  MaxLength="2"></asp:TextBox></span>
	    	月分新車新登録、届出台数は次のとおりであり
	    </p>
    </div>
    <!--説明文2-->
    <div id="NarratvieText2">
	    <p id="p2" class="p1">
	    	ますから報告します。
	    </p>
	</div>
	<!--次-->
	<div id="Next">
		<p>次</p>
	</div>
	<!--テーブル-->    
	<div id="TableDiv" >
		<table id="tblReport"  >
        <colgroup  id="" class="class1">
        </colgroup>
        <colgroup span="2" id="" class="class2">
            <col  style="width:120px;">
            <col>
        </colgroup>
        <colgroup span="9" id="" class="class3">
        </colgroup>
		<thead>
		<tr>
			<th class="col"></th><th>区　分</th><th colspan="2">水　戸</th><th colspan="2">土　浦</th><th colspan="2">つ　く　ば</th><th colspan="2">その他</th><th colspan="2">合計</th>
		</tr>
		</thead>
		<tbody>
		<!--貨物車-->
		<tr>
			<td rowspan="4"><p>貨</p><p>物</p><p>車</p></td>
			<td class="Category">7t以上</td>
			<td colspan="2"><asp:TextBox ID="txtMito_Kamotu1" runat="server"  class="txtTableType1"   MaxLength="3" ></asp:TextBox></td>
			<td colspan="2"><asp:TextBox ID="txtTuchiura_Kamotu1" runat="server" class="txtTableType1" MaxLength="3" ></asp:TextBox></td>
			<td colspan="2"><asp:TextBox ID="txtTukuba_Kamotu1" runat="server" class="txtTableType1"  MaxLength="3" ></asp:TextBox></td>
			<td colspan="2"><asp:TextBox ID="txtSonota_Kamotu1" runat="server" class="txtTableType1"  MaxLength="3" ></asp:TextBox></td>
			<td colspan="2"><asp:TextBox ID="txtGoukei_Kamotu1" runat="server" class="txtTableType1"  MaxLength="3" ></asp:TextBox></td>
		</tr>
		<tr>
			<td  class="Category">6.9t ～ 5t以上</td>
			<td colspan="2"><asp:TextBox ID="txtMito_Kamotu2" runat="server" class="txtTableType1"     MaxLength="3"  ></asp:TextBox></td>
			<td colspan="2"><asp:TextBox ID="txtTuchiura_Kamotu2" runat="server" class="txtTableType1" MaxLength="3"  ></asp:TextBox></td>
			<td colspan="2"><asp:TextBox ID="txtTukuba_Kamotu2" runat="server" class="txtTableType1"   MaxLength="3"  ></asp:TextBox></td>
			<td colspan="2"><asp:TextBox ID="txtSonota_Kamotu2" runat="server" class="txtTableType1"   MaxLength="3"  ></asp:TextBox></td>
			<td colspan="2"><asp:TextBox ID="txtGoukei_Kamotu2" runat="server" class="txtTableType1"   MaxLength="3"  ></asp:TextBox></td>
		</tr>
		<tr>
		<td  class="Category">4.9t ～ 3t以上</td>
			<td colspan="2"><asp:TextBox ID="txtMito_Kamotu3" runat="server" class="txtTableType1"      MaxLength="3"  ></asp:TextBox></td>
			<td colspan="2"><asp:TextBox ID="txtTuchiura_Kamotu3" runat="server" class="txtTableType1"  MaxLength="3"  ></asp:TextBox></td>
			<td colspan="2"><asp:TextBox ID="txtTukuba_Kamotu3" runat="server" class="txtTableType1"    MaxLength="3"  ></asp:TextBox></td>
			<td colspan="2"><asp:TextBox ID="txtSonota_Kamotu3" runat="server" class="txtTableType1"    MaxLength="3"  ></asp:TextBox></td>
			<td colspan="2"><asp:TextBox ID="txtGoukei_Kamotu3" runat="server" class="txtTableType1"    MaxLength="3"  ></asp:TextBox></td>
		
		</tr>
		<tr>
		<td class="Category">2.9t ～ 2.5t以上</td>
			<td colspan="2"><asp:TextBox ID="txtMito_Kamotu4" runat="server" class="txtTableType1"      MaxLength="3"  ></asp:TextBox></td>
			<td colspan="2"><asp:TextBox ID="txtTuchiura_Kamotu4" runat="server" class="txtTableType1"  MaxLength="3"  ></asp:TextBox></td>
			<td colspan="2"><asp:TextBox ID="txtTukuba_Kamotu4" runat="server" class="txtTableType1"    MaxLength="3"  ></asp:TextBox></td>
			<td colspan="2"><asp:TextBox ID="txtSonota_Kamotu4" runat="server" class="txtTableType1"    MaxLength="3"  ></asp:TextBox></td>
			<td colspan="2"><asp:TextBox ID="txtGoukei_Kamotu4" runat="server" class="txtTableType1"    MaxLength="3"  ></asp:TextBox></td>
		</tr>
		<!--バス-->
		<tr>
			<td rowspan="2"><p>バ</p><p>ス</p></td>
			<td  class="Category">定員30人以上</td>
			<td colspan="2"><asp:TextBox ID="txtMito_Bus1" runat="server" class="txtTableType1"      MaxLength="3"  ></asp:TextBox></td>
			<td colspan="2"><asp:TextBox ID="txtTuchiura_Bus1" runat="server" class="txtTableType1"  MaxLength="3"  ></asp:TextBox></td>
			<td colspan="2"><asp:TextBox ID="txtTukuba_Bus1" runat="server" class="txtTableType1"    MaxLength="3"  ></asp:TextBox></td>
			<td colspan="2"><asp:TextBox ID="txtSonota_Bus1" runat="server" class="txtTableType1"    MaxLength="3"  ></asp:TextBox></td>
			<td colspan="2"><asp:TextBox ID="txtGoukei_Bus1" runat="server" class="txtTableType1"    MaxLength="3"  ></asp:TextBox></td>
		</tr>
		<tr>
		<td  class="Category">定員29人以下<br/>(乗用車を除く)</td>
		<td colspan="2"><asp:TextBox ID="txtMito_Bus2" runat="server" class="txtTableType1"      MaxLength="3"  ></asp:TextBox></td>
		<td colspan="2"><asp:TextBox ID="txtTuchiura_Bus2" runat="server" class="txtTableType1"  MaxLength="3"  ></asp:TextBox></td>
		<td colspan="2"><asp:TextBox ID="txtTukuba_Bus2" runat="server" class="txtTableType1"    MaxLength="3"  ></asp:TextBox></td>
		<td colspan="2"><asp:TextBox ID="txtSonota_Bus2" runat="server" class="txtTableType1"    MaxLength="3"  ></asp:TextBox></td>
		<td colspan="2"><asp:TextBox ID="txtGoukei_Bus2" runat="server" class="txtTableType1"    MaxLength="3"  ></asp:TextBox></td>
		</tr>
		<!--乗用車及び貨物車-->
		<tr>
			<td rowspan="4" class="Row_JK">
                <p id="Row_JK_1" >乗&nbsp;&nbsp;貨</p>
                <p id="Row_JK_2" >用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
                <p id="Row_JK_3" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;物</p>
                <p id="Row_JK_4" >及&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
                <p id="Row_JK_5" >び&nbsp;&nbsp;車</p>
            </td>
             <td >&nbsp;</td><td>乗用</td><td>貨物</td><td>乗用</td><td>貨物</td><td>乗用</td><td>貨物</td><td>乗用</td><td>貨物</td><td>乗用</td><td>貨物</td>
		</tr>
		<tr>
			<td class="Category" ><p>2,100cc以上</p></td>
			<td><asp:TextBox ID="txtMito_JK_J1" runat="server"       MaxLength="3"  class="txtTableType2"  ></asp:TextBox></td>
			<td><asp:TextBox ID="txtMito_JK_K1" runat="server"       MaxLength="3"  class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtTuchiura_JK_J1" runat="server"   MaxLength="3"  class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtTuchiura_JK_K1" runat="server"   MaxLength="3"  class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtTukuba_JK_J1" runat="server"     MaxLength="3"  class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtTukuba_JK_K1" runat="server"     MaxLength="3"  class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtSonota_JK_J1" runat="server"     MaxLength="3"  class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtSonota_JK_K1" runat="server"     MaxLength="3"  class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtGoukei_JK_J1" runat="server"     MaxLength="3"  class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtGoukei_JK_K1" runat="server"     MaxLength="3"  class="txtTableType2" ></asp:TextBox></td>
		</tr>
		<tr style="height:10px;">
			<td  class="Category" ><p style="line-height:14px;">2,000cc&nbsp;&nbsp;&nbsp;<br/>&nbsp;&nbsp;&nbsp;～1,000cc</p></td>
			<td><asp:TextBox ID="txtMito_JK_J2" runat="server"    MaxLength="3"     class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtMito_JK_K2" runat="server"    MaxLength="3"     class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtTuchiura_JK_J2" runat="server"   MaxLength="3" class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtTuchiura_JK_K2" runat="server"   MaxLength="3" class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtTukuba_JK_J2" runat="server"  MaxLength="3"    class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtTukuba_JK_K2" runat="server"  MaxLength="3"    class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtSonota_JK_J2" runat="server"  MaxLength="3"    class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtSonota_JK_K2" runat="server"  MaxLength="3"    class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtGoukei_JK_J2" runat="server"  MaxLength="3"    class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtGoukei_JK_K2" runat="server"  MaxLength="3"    class="txtTableType2" ></asp:TextBox></td>
		</tr>
		<tr  style="height:10px;">
			<td class="Category" ><p>1,000cc未満</p></td>
			<td><asp:TextBox ID="txtMito_JK_J3" runat="server"   MaxLength="3"       class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtMito_JK_K3" runat="server"   MaxLength="3"       class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtTuchiura_JK_J3" runat="server"   MaxLength="3"   class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtTuchiura_JK_K3" runat="server"   MaxLength="3"   class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtTukuba_JK_J3" runat="server"  MaxLength="3"   class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtTukuba_JK_K3" runat="server"  MaxLength="3"   class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtSonota_JK_J3" runat="server"  MaxLength="3"   class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtSonota_JK_K3" runat="server"  MaxLength="3"   class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtGoukei_JK_J3" runat="server"  MaxLength="3"  class="txtTableType2" ></asp:TextBox></td>
			<td><asp:TextBox ID="txtGoukei_JK_K3" runat="server"  MaxLength="3"  class="txtTableType2" ></asp:TextBox></td>
		</tr>
		<!--小計-->
		<tr>
		<td colspan="2">小　計</td>
		<td colspan="2"><asp:TextBox ID="txtMito_SubTotal1" runat="server" class="txtTableType1"  MaxLength="3" ></asp:TextBox></td>
		<td colspan="2"><asp:TextBox ID="txtTuchiura_SubTotal1" runat="server" class="txtTableType1"  MaxLength="3" ></asp:TextBox></td>
		<td colspan="2"><asp:TextBox ID="txtTukuba_SubTotal1" runat="server" class="txtTableType1"   MaxLength="3" ></asp:TextBox></td>
		<td colspan="2"><asp:TextBox ID="txtSonota_SubTotal1" runat="server" class="txtTableType1"   MaxLength="3" ></asp:TextBox></td>
		<td colspan="2"><asp:TextBox ID="txtGoukei_SubTotal1" runat="server" class="txtTableType1"   MaxLength="3" ></asp:TextBox></td>
		</tr>
		<!--合計-->
		<tr>
		<td colspan="2">合　計</td>
		<td colspan="2"><asp:TextBox ID="txtMito_Total1" runat="server" class="txtTableType1"     MaxLength="3" ></asp:TextBox></td>
		<td colspan="2"><asp:TextBox ID="txtTuchiura_Total1" runat="server" class="txtTableType1"  MaxLength="3" ></asp:TextBox></td>
		<td colspan="2"><asp:TextBox ID="txtTukuba_Total1" runat="server" class="txtTableType1"   MaxLength="3" ></asp:TextBox></td>
		<td colspan="2"><asp:TextBox ID="txtSonota_Total1" runat="server" class="txtTableType1"   MaxLength="3" ></asp:TextBox></td>
		<td  colspan="2"><asp:TextBox ID="txtGoukei_Total1" runat="server" class="txtTableType1"   MaxLength="3" ></asp:TextBox></td>
		</tr>
		</tbody>
		</table>
	</div>
<!--フッター-->
<div id="footer">
	<p class="footer_p">※直納及び県外からの登録も含む。</p>
	<p class="footer_p" >※翌月5日までに必ず報告のこと。</p>
</div>
<!--ボタン-->
<div  id="footerButton">
    <asp:Button ID="btnSubmit" runat="server" Text="削除" onclick="btnSubmit_Click" class="FooterBtn" />
</div>
</form>
</div>
</body>
</html>