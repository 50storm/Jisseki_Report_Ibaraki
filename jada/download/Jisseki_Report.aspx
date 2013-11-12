<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Jisseki_Report.aspx.cs" Inherits="Jisseki_Report_Ibaraki.jada.download.Jisseki_Report" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>実績報告書ダウンロード</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>
            <asp:Label ID="Label2" runat="server" Text="実績報告書ダウンロード：" ></asp:Label>
            <asp:Label ID="lblEra" runat="server" Text="元号"></asp:Label>
            <asp:TextBox ID="txtYearRep" runat="server" MaxLength="2" Width="20px"></asp:TextBox>
            <asp:Label ID="lblYearRep" runat="server" Text="年"></asp:Label>
            <asp:TextBox ID="txtMonthRep" runat="server" MaxLength="2" Width="20px"></asp:TextBox>
            <asp:Label ID="lblMonthRep" runat="server" Text="月"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label1" runat="server" Text="ファイル名："></asp:Label><asp:TextBox ID="txtFileName" runat="server"  Width="190px"></asp:TextBox>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </p>
        <asp:Button ID="btnDownload" runat="server" Text="ダウンロード" 
            onclick="btnDownload_Click" />

    </div>
    </form>
</body>
</html>
