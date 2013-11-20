<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jisseki.aspx.cs" Inherits="Jisseki_Report_Ibaraki.member.search.jisseki" %>
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
    <title>送信確認画面</title>
</head>
<body>
    <h1>送信確認画面</h1>
    <form id="form1" runat="server">
    <div>
        <asp:Label   ID="lblDateRep" runat="server" >新車登録台数報告年月：</asp:Label>        
        <asp:TextBox ID="txtYearRepFrom" runat="server" MaxLength="2" class="text2digit" ></asp:TextBox>
        <asp:Label   ID="lblYearRepFrom" runat="server" >年</asp:Label>
        <asp:TextBox ID="txtMonthRepFrom" runat="server" MaxLength="2"  class="text2digit" ></asp:TextBox>
        <asp:Label 　ID="lblMonthRepFrom" runat="server">月</asp:Label>
        ～
        <asp:TextBox ID="txtYearRepTo" runat="server" MaxLength="2"  class="text2digit"  ></asp:TextBox>
        <asp:Label   ID="lblYearRepTo" runat="server" >年</asp:Label>
        <asp:TextBox ID="txtMonthRepTo" runat="server" MaxLength="2"  class="text2digit" ></asp:TextBox>
        <asp:Label 　ID="lblMonthRepTo" runat="server">月</asp:Label>
        <asp:Button  ID="btnSearch" runat="server" Text="検索" onclick="btnSearch_Click" />
        <asp:Label ID="lblMsg" runat="server" ></asp:Label>
    </div>
    <div>
    <asp:gridview ID="Gridview1" runat="server" AutoGenerateColumns="False" 
            style="margin-right: 0px" 
           >
    <Columns>
        <asp:BoundField DataField="TANTOU" HeaderText="担当者" />
        <asp:BoundField HeaderText="送信日付" />
        
            
        <asp:BoundField HeaderText="報告年月" />
        
            
        <asp:HyperLinkField Text="削除" DataNavigateUrlFields="COCODE,YearRep,MonthRep" 
            DataNavigateUrlFormatString="~/common/delete_jisseki.aspx?COCODE={0}&amp;YearRep={1}&amp;MonthRep={2}" 
            Target="_blank" />
        
        <asp:HyperLinkField Text="修正" DataNavigateUrlFields="COCODE,YearRep,MonthRep" 
            DataNavigateUrlFormatString="~/common/alter_jisseki.aspx?COCODE={0}&amp;YearRep={1}&amp;MonthRep={2}" 
            Target="_blank" />
        
        <asp:BoundField DataField="Year" HeaderText="Year" />
        <asp:BoundField DataField="Month" HeaderText="Month" />
        <asp:BoundField DataField="Day" HeaderText="Day" />
        
        <asp:BoundField DataField="YearRep" HeaderText="YearRep" />
        <asp:BoundField DataField="MonthRep" HeaderText="MonthRep" />
        
            
    </Columns>
        </asp:gridview>    
    </div>
    </form>
</body>

</html>
