<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Jisseki_Report.aspx.cs" Inherits="Jisseki_Report_Ibaraki.jada.download.Jisseki_Report" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="stylesheet" href="../../Css/input.css" type="text/css" />
<style type="text/css">
.text2digit
{
	width:20px;
	height:20px;
	text-align:center;
	ime-mode:disabled;
	
}
</style>
<script type="text/javascript" src="../../Scripts/Utility.js"></script>
<script type="text/javascript">
    function validateForm() {
        if (!isEmpty("txtYearRep")) {
            return false;
        }

        if (!isEmpty("txtMonthRep")) {
            return false;
        }


        if (!isNumber("txtYearRep")) {
            return false;
        }

        if (!isNumber("txtMonthRep")) {
            return false;
        }

        if (!isEmpty("txtFileName")) {
            document.getElementById("lblMsg").value = "ファイル名を入力してください。";
            return false;
        } else {
            document.getElementById("lblMsg").value = "";
        }

    }
    // ==============================
    //  カーソル制御処理
    // ==============================
    function setFocus() {
        document.getElementById('txtYearRep').onkeydown
        = function () {
            if (event.keyCode == 13) {
                document.getElementById('txtMonthRep').focus();
                document.getElementById('txtMonthRep').select();
                return false;
            }
        }

        document.getElementById('txtMonthRep').onkeydown
        = function () {
            if (event.keyCode == 13) {
                document.getElementById('btnDownload').focus();
                document.getElementById('btnDownload').select();
                return false;
            }
        }
    }
</script>
    <title>実績報告書ダウンロード</title>
</head>
<body  onload="return setFocus();">
<!--外枠-->	
<div id="Wrapper">
    <form id="form1" runat="server">
    <!--メニュー-->	
	<div id="Menu" >
        <div id="Menu_Link">
            <asp:Button ID="btnlinkMenu" runat="server" Text="メニュー"  onclick="btnlinkMenu_Click" class="BtnMenu" />
        </div>
        <div id="Menu_Btn">
            <asp:Button ID="btnLogOut" runat="server" Text="ログアウト"    onclick="btnLogOut_Click" />
        </div>
    </div>
    <!--改行-->
    <div>
        <br/><br/><br/>
    </div>
    <div>
        <p>
            <asp:Label ID="Label2" runat="server" Text="実績報告書ダウンロード：" ></asp:Label>
            <asp:Label ID="lblEra" runat="server" Text="元号"></asp:Label>
            <asp:TextBox ID="txtYearRep" runat="server" MaxLength="2" class="text2digit"  onFocus="select();"  ></asp:TextBox>
            <asp:Label ID="lblYearRep" runat="server" Text="年"></asp:Label>
            <asp:TextBox ID="txtMonthRep" runat="server" MaxLength="2"  class="text2digit"  onFocus="select();" ></asp:TextBox>
            <asp:Label ID="lblMonthRep" runat="server" Text="月"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label1" runat="server" Text="ファイル名："></asp:Label><asp:TextBox ID="txtFileName" runat="server"  Width="190px"  onFocus="select();"  ></asp:TextBox>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </p>
        <asp:Button ID="btnDownload" runat="server" Text="ダウンロード" onclientclick="return validateForm();"
            onclick="btnDownload_Click" />

    </div>
    </form>
</div>
</body>
</html>
