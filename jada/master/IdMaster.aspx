﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IdMaster.aspx.cs" Inherits="Jisseki_Report_Ibaraki.jada.master.IdMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会員マスターメンテナンス</title>
    <script type="text/javascript" src="../../Script/Utility.js" ></script>
    <script type="text/javascript">
        function confirmDeletion()
        { 
            if(!confirm("削除してよろしいですか？")) 
            {
                return false;
            }
        }
        function confirmRegister()
        {
            if (!isNumber("txtCOCODE"))
            {
                document.getElementById("txtCOCODE").focus();
                return false;
            }
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="1px">
        <tr>
            <th>会社コード</th>
            <th>会社名</th>
            <th>代表者名</th>
            <th>郵便番号</th>
            <th>住所</th>
            <th>電話番号</th>
            <th>パスワード</th>
            <th>会員フラグ<br/>(1:会員/0:自販連)</th>
            <th>略名</th>
        </tr>
        <tr>
            <td><asp:TextBox ID="txtCOCODE" runat="server"  Width="60px" MaxLength="4"></asp:TextBox></td>
            <td><asp:TextBox ID="txtCONAME" runat="server"  Width="180px" MaxLength="40"></asp:TextBox></td>
            <td><asp:TextBox ID="txtRepName" runat="server"  Width="90px" MaxLength="10" ></asp:TextBox></td>
            <td><asp:TextBox ID="txtPostalCode" runat="server"  Width="60px"> </asp:TextBox></td>
            <td><asp:TextBox ID="txtAddress" runat="server"  Width="120px" MaxLength="50"></asp:TextBox></td>
            <td><asp:TextBox ID="txtTel" runat="server"  Width="60px" MaxLength="12"></asp:TextBox></td>
            <td><asp:TextBox ID="txtPassword" runat="server"  Width="60px" MaxLength="15"></asp:TextBox></td>
            <td><asp:TextBox ID="txtMember" runat="server"  Width="100px" MaxLength="1"></asp:TextBox></td>
            <td><asp:TextBox ID="txtshort_CONAME" runat="server"  Width="180px" MaxLength="40"></asp:TextBox></td>
        </tr>
        </table>
        <asp:Button ID="btnInsert" runat="server" Text="登録" onclick="btnInsert_Click" 
            onclientclick="return confirmRegister();" />
        <asp:Button ID="btnUpdate" runat="server" Text="更新" onclick="btnUpdate_Click" 
            style="height: 21px" />
        <asp:Button ID="btnDelete" runat="server" Text="削除" onclick="btnDelete_Click" 
            onclientclick="return confirmDeletion();" />
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
    <asp:GridView ID="GridView1" runat="server" 
        AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
        DataKeyNames="COCODE" 
        onselectedindexchanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="COCODE" HeaderText="会員コード"   />
            <asp:BoundField DataField="CONAME" HeaderText="会員名"  />
            <asp:BoundField DataField="RepName" HeaderText="代表者名"  />
            <asp:BoundField DataField="PostalCode" HeaderText="郵便番号"  />
            <asp:BoundField DataField="Address" HeaderText="住所" />
            <asp:BoundField DataField="Tel" HeaderText="電話番号"  />
            <asp:BoundField DataField="Password" HeaderText="パスワード" />
            <asp:BoundField DataField="Member" HeaderText="会員フラグ"  />
            <asp:BoundField DataField="short_CONAME" HeaderText="略名"  />
        </Columns>
    </asp:GridView>
    <!--
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:JissekiConnectionString %>" 
        SelectCommand="SELECT DISTINCT * FROM [ID]"></asp:SqlDataSource>
        -->
    </form>
</body>
</html>
