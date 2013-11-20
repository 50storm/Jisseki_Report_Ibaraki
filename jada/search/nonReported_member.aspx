﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nonReported_member.aspx.cs" Inherits="Jisseki_Report_Ibaraki.jada.search.nonReported_member" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<style type="text/css">
.text2digit
{
	width:20px;
	height:20px;
	text-align:center;
}
</style>
    <title>未受信データ会員</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="Query">
        <asp:Label   ID="lblDateRep" runat="server" >新車登録台数報告年月：</asp:Label>        
        <asp:TextBox ID="txtYearRep" runat="server" MaxLength="2"  class="text2digit" ></asp:TextBox>
        <asp:Label   ID="lblYearRep" runat="server" >年</asp:Label>
        <asp:TextBox ID="txtMonthRep" runat="server" MaxLength="2"   class="text2digit" ></asp:TextBox>
        <asp:Label 　ID="lblMonthRep" runat="server">月</asp:Label>
        <asp:Button  ID="btnSearch" runat="server" Text="検索" onclick="btnSearch_Click" />
        <asp:Label ID="lblMsg" runat="server" ></asp:Label>
    </div>
    <div>
        <asp:gridview ID="Gridview1" runat="server" AutoGenerateColumns="False"  Caption="未受信データ会員" >
            <Columns>
                <asp:BoundField DataField="COCODE" HeaderText="会員コード" />
                <asp:BoundField DataField="CONAME" HeaderText="会員名" />
                <asp:BoundField DataField="RepName" HeaderText="代表者" />
                <asp:BoundField DataField="Tel" HeaderText="電話番号" />
                <asp:HyperLinkField DataNavigateUrlFields="COCODE"  
                    DataNavigateUrlFormatString="~/common/input_jisseki.aspx?COCODE={0}"  Text="登録" 
                    Target="_blank" />
            </Columns>
        </asp:gridview>    
    </div>
    </form>
</body>
</html>
