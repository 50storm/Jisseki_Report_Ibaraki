<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitPriceMaster.aspx.cs" Inherits="Jisseki_Report_Ibaraki.jada.master.UnitPriceMaster" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="stylesheet" href="../../Css/input.css" type="text/css" />
    <style type="text/css">
<%--        #Wrapper
        {
	        width: 400px;  
	        height:auto;
	        margin: 100px auto;
        }--%>
        #UnitPriceTable
        {
            	
        }
        #Content
        {
        	
        	}
        	.txtUnitPrice
        	{
        		width:130px;
        		text-align:center;
        	}

    </style>
    <script type="text/javascript" src="../../Scripts/Utility.js"></script>
    <script type="text/javascript">
        var flag =
        {
            BigSize: false,
            MediumSmall: false,
            Average: false,
            Kamotu7t: false,
            Kamotu6DP9_5t: false,
            Kamotu4DP9_3t: false,
            Kamotu2DP9_2DP5t: false,
            Over2001cc: false,
            To2000From1000cc: false,
            Over30: false,
            LessThan29: false
        
        };
        function checkForm() {
            if (!isNumber("txtBigSize")) {
                
                flag.BigSize=false;
            }else{
                flag.BigSize=true;

            }

            if (!isNumber("txtMediumSmall")) {
                flag.MediumSmall=false;
            }else{
                flag.MediumSmall=true;
            }

            if (!isNumber("txtAverage")) {
                flag.Average=false;
            }else{
                flag.Average=true;
            }


            if (!isNumber("txtKamotu7t")) {
                flag.Kamotu7t=false;
            }else{
                flag.Kamotu7t=true;
            }


            if (!isNumber("txtKamotu6DP9_5t")) {
                flag.Kamotu6DP9_5t=false;
            }else{
                flag.Kamotu6DP9_5t=true;
            }

            if (!isNumber("txtKamotu4DP9_3t")) {
                flag.Kamotu4DP9_3t=false;
            }else{
                flag.Kamotu4DP9_3t=true;
            }

            if (!isNumber("txtKamotu2DP9_2DP5t")) {
                flag.Kamotu2DP9_2DP5t=false;
            }else{
                flag.Kamotu2DP9_2DP5t=true;
            }

            if (!isNumber("txtOver2001cc")) {
                flag.Over2001cc=false;
            }else{
                flag.Over2001cc=true;
            }

            if (!isNumber("txtTo2000From1000cc")) {
                flag.To2000From1000cc=false;
            }else{
                flag.To2000From1000cc=true;
            }

            if (!isNumber("txtOver30")) {
                flag.Over30=false;
            }else{
                flag.Over30=true;
            }

            if (!isNumber("txtLessThan29")) {
                flag.LessThan29=false;
            }else{
                flag.LessThan29=true;
            }

            if (!isNumber("txtMemberFee")) {
                flag.MemberFee=false;
            }else{
                flag.MemberFee=true;
            }
            if(
                flag.BigSize
                && flag.MediumSmall
                && flag.Average
                && flag.Kamotu7t
                && flag.Kamotu6DP9_5t
                && flag.Kamotu4DP9_3t
                && flag.Kamotu2DP9_2DP5t
                && flag.Over2001cc
                && flag.To2000From1000cc
                && flag.Over30
                && flag.LessThan29
                && flag.MemberFee
            ){
               
            }else{
                return false;

            }
            return true;

        }

</script>
    <title>単価マスター【一括修正】</title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>単価マスター</h1>
    <div id="Wrapper">
    <!--メニュー-->	
	<div id="Menu" >
        <table id="MenuTable" cellpadding="1" cellspacing="5" style="border-collapse: separate;">
            <tr >            
                <td >
                   <asp:Button ID="btnlinkMenu" runat="server" Text="メニュー"  onclick="btnlinkMenu_Click" class="BtnMenu" />
                </td>
                <td >
                    <asp:Button ID="btnLogOut" runat="server" Text="ログアウト"    onclick="btnLogOut_Click" />
                </td>
            </tr>
        </table>
    </div>
    <!--改行-->
    <div>
        <br/><br/><br/>
    </div>
    <div>
        <asp:Label ID="lblMsg" runat="server" BackColor="#FF66FF"></asp:Label>
    </div>
    <div>
        <table id="UnitPriceTable" style="width:360px;" border="1">
        <colgroup span="1" style="background-color:#1C5E55;color:White;"></colgroup>
        <colgroup span="1"></colgroup>
            <tr>
                <td>台数割会費　大型:</td>
                <td><asp:TextBox ID="txtBigSize" runat="server" class="txtUnitPrice"></asp:TextBox></td>
            </tr>
            <tr>
                <td>台数割会費　中・小型:</td>
                <td><asp:TextBox ID="txtMediumSmall" runat="server" class="txtUnitPrice"></asp:TextBox></td>
            </tr>
            <tr>
                <td>均等割会費: </td>
                <td><asp:TextBox ID="txtAverage" runat="server" class="txtUnitPrice"></asp:TextBox></td>
            </tr>
            <tr>
                <td>７ｔ以上: </td>
                <td><asp:TextBox ID="txtKamotu7t" runat="server" class="txtUnitPrice"></asp:TextBox></td>
            </tr>
            <tr>
                <td>６．９ｔ～５ｔ:</td>
                <td><asp:TextBox ID="txtKamotu6DP9_5t" runat="server" class="txtUnitPrice"></asp:TextBox></td>
            </tr>
            <tr>
                <td>４．９ｔ～３ｔ: </td>
                <td><asp:TextBox ID="txtKamotu4DP9_3t" runat="server" class="txtUnitPrice"></asp:TextBox></td>
            </tr>
            <tr>
                <td>２．９ｔ～２．５ｔ:</td>
                <td><asp:TextBox ID="txtKamotu2DP9_2DP5t" runat="server" class="txtUnitPrice"></asp:TextBox></td>
            </tr>
            <tr>
                <td>２，００１ｃｃ以上: </td>
                <td><asp:TextBox ID="txtOver2001cc" runat="server" class="txtUnitPrice"></asp:TextBox></td>
            </tr>
            <tr>
                <td>２，０００ｃｃ～１，０００ｃｃ: </td>
                <td><asp:TextBox ID="txtTo2000From1000cc" runat="server" class="txtUnitPrice"></asp:TextBox></td>
            </tr>
            <tr>
                <td>乗合定員３０人以上:</td>
                <td><asp:TextBox ID="txtOver30" runat="server" class="txtUnitPrice"></asp:TextBox></td>
            </tr>
            <tr>
                <td>乗合定員２９人以下:</td>
                <td><asp:TextBox ID="txtLessThan29" runat="server" class="txtUnitPrice"></asp:TextBox></td>
            </tr>
            <tr>
                <td>会費:</td>
                <td><asp:TextBox ID="txtMemberFee" runat="server" class="txtUnitPrice"></asp:TextBox></td>
            </tr>
            
        </table>
        <br/>
        <div>
            <asp:Button ID="btnUpdate" runat="server" Text="更新"  onclick="btnUpdate_Click1" OnClientClick="return checkForm();" />
            <input id="Reset1" type="reset" value="リセット" />
        </div>
        <br/>
      </div>
    </div>
    </form>
</body>
</html>
