<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dealer.aspx.cs" Inherits="Jisseki_Report_Ibaraki.member.menu.Dealer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>会員メニュー</h1>
    <div style=" width:400px;height:300px;">
        <form id="form1" runat="server">
        <div>
            <p><asp:Button ID="btnInputJisseki" runat="server" Text="新車台数実績入力"   onclick="btnInputJisseki_Click" /></p>
            <p><asp:Button ID="btnSerachJisseki" runat="server" Text="新車台数実績検索"  onclick="btnSearchJisseki_Click" /></p>
        </div>
        <asp:LinkButton ID="LinkButtonLogOut" runat="server"  onclick="LinkButton1_Click">ログアウト</asp:LinkButton>
        </form>
    </div>
</body>
</html>
