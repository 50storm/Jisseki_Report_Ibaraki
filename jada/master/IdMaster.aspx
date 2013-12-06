<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IdMaster.aspx.cs" Inherits="Jisseki_Report_Ibaraki.jada.master.IdMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="stylesheet" href="../../Css/input.css" type="text/css" />
<style type="text/css">
#txtCOCODE
{
	Width:60px;
	ime-mode:disabled;
}
#txtCONAME
{
    Width:180px;
    ime-mode:active;
}
#txtRepName
{
    Width:90px;	
    ime-mode:active;
}
#txtPostalCode
{
	 Width:60px;
	 ime-mode:disabled;
}
#txtAddress
{
	 Width:120px;
	 ime-mode:active;
}
#txtTel
{	
	Width:60px;	
	ime-mode:disabled;
}
#txtPassword
{
	Width:60px;
	ime-mode:disabled;
}
#txtMember
{
    Width:100px;
    ime-mode:disabled;
}
#txtMemberType
{
    Width:100px;
    ime-mode:disabled;
}
#txtshort_CONAME
{
    Width:180px;
    ime-mode:active;
}
</style>
    <title>会員マスターメンテナンス</title>
    <script type="text/javascript" src="../../Scripts/Utility.js" ></script>
    <script type="text/javascript">
        // ==============================
        //  カーソル制御処理
        // ==============================
    function setFocus() {
            document.getElementById('txtCOCODE').onkeydown
            = function () {
                if (event.keyCode == 13) {
                    document.getElementById('txtUID').focus();
                    document.getElementById('txtUID').select();
                    return false;
                }
            }

            document.getElementById('txtUID').onkeydown
            = function () {
                if (event.keyCode == 13) {
                    document.getElementById('txtCONAME').focus();
                    document.getElementById('txtCONAME').select();
                    return false;
                }
            }


            document.getElementById('txtCONAME').onkeydown
            = function () {
                if (event.keyCode == 13) {
                    document.getElementById('txtshort_CONAME').focus();
                    document.getElementById('txtshort_CONAME').select();
                    return false;
                }
            }

            document.getElementById('txtshort_CONAME').onkeydown
            = function () {
                if (event.keyCode == 13) {
                    document.getElementById('txtRepName').focus();
                    document.getElementById('txtRepName').select();
                    return false;
                }
            }

            document.getElementById('txtRepName').onkeydown
            = function () {
                if (event.keyCode == 13) {
                    document.getElementById('txtPostalCode').focus();
                    document.getElementById('txtPostalCode').select();
                    return false;
                }
            }

            document.getElementById('txtPostalCode').onkeydown
            = function () {
                if (event.keyCode == 13) {
                    document.getElementById('txtAddress').focus();
                    document.getElementById('txtAddress').select();
                    return false;
                }
            }

            document.getElementById('txtAddress').onkeydown
            = function () {
                if (event.keyCode == 13) {
                    document.getElementById('txtTel').focus();
                    document.getElementById('txtTel').select();
                    return false;
                }
            }

            document.getElementById('txtTel').onkeydown
            = function () {
                if (event.keyCode == 13) {
                    document.getElementById('txtPassword').focus();
                    document.getElementById('txtPassword').select();
                    return false;
                }
            }

            document.getElementById('txtPassword').onkeydown
            = function () {
                if (event.keyCode == 13) {
                    document.getElementById('txtMember').focus();
                    document.getElementById('txtMember').select();
                    return false;
                }
            }

            document.getElementById('txtMember').onkeydown
            = function () {
                if (event.keyCode == 13) {
                    document.getElementById('txtMemberType').focus();
                    document.getElementById('txtMemberType').select();
                    return false;
                }
            }

            document.getElementById('txtMemberType').onkeydown
            = function () {
                if (event.keyCode == 13) {
                    document.getElementById('txtPosition').focus();
                    document.getElementById('txtPosition').select();
                    return false;
                }
            }

            document.getElementById('txtPosition').onkeydown
            = function () {
                if (event.keyCode == 13) {
                    document.getElementById('txtisCanceled').focus();
                    document.getElementById('txtisCanceled').select();
                    return false;
                }
            }

            document.getElementById('txtisCanceled').onkeydown
            = function () {
                if (event.keyCode == 13) {
                    document.getElementById('btnInsert').focus();
                    document.getElementById('btnInsert').select();
                    return false;
                }
            }

            document.getElementById('btnInsert').onkeydown
            = function () {
                if (event.keyCode == 13) {
                    document.getElementById('btnUpdate').focus();
                    document.getElementById('btnUpdate').select();
                    return false;
                }
            }

            document.getElementById('btnUpdate').onkeydown
            = function () {
                if (event.keyCode == 13) {
                    document.getElementById('btnDelete').focus();
                    document.getElementById('btnDelete').select();
                    return false;
                }
            }

            document.getElementById('btnDelete').onkeydown
            = function () {
                if (event.keyCode == 13) {
                    document.getElementById('txtCOCODE').focus();
                    document.getElementById('txtCOCODE').select();
                    return false;
                }
            }
        }    


        //削除確認
        function confirmDeletion()
        { 
            if(!confirm("削除してよろしいですか？")) 
            {
                return false;
            }
        }

        //登録確認
        function confirmRegister() 
        {
            if (!isEmpty("txtCOCODE")) 
            {
                return false;
            }

            if (!isEmpty("txtUID")) {
                return false;
            }

            if (!isEmpty("txtMember")) 
            {
                return false;
            }
            if (!isEmpty("txtMemberType")) 
            {
                return false;
            }

            if (!isNumber("txtCOCODE"))
            {
                document.getElementById("txtCOCODE").focus();
                return false;
            }

            if (!isNumber("txtMember")) 
            {
                document.getElementById("txtMember").focus();
                return false;
            }

            var txtMember = document.getElementById("txtMember");
            if (parseInt(txtMember.value, 10) !== 0
               &&
                parseInt(txtMember.value, 10) !== 1
               )
            {
                document.getElementById("txtMember").focus();
                isError("txtMember");
                return false;
            } else {
                isOk("txtMember");
            }


            if (!isNumber("txtMemberType")) 
            {
                document.getElementById("txtMemberType").focus();
                return false;
            }

            var txtMemberType = document.getElementById("txtMemberType");
            if (parseInt(txtMemberType.value, 10) !== 0
               &&
                parseInt(txtMemberType.value, 10) !== 1
               ) {
                document.getElementById("txtMemberType").focus();
                isError("txtMemberType");
                return false;
            } else {
                isOk("txtMemberType");
            }

        }
    </script>
</head>
<body  onload="return setFocus();">
    <h1>会員マスター</h1>
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
    <div id="input" >
        <table border="1px">
        <tr style="background-color:#1C5E55;color:White;">
            <th>会社コード</th>
            <th>ログインID</th>
            <th>会社名</th>
            <th>略名</th>
            <th>担当者</th>
            <th>郵便番号</th>
            <th>住所</th>
            <th>電話番号</th>
            <th>パスワード</th>
            <th>会員フラグ<br/>(0:自販連/1:会員)</th>
            <th>会員種別<br/>(0:通常/1:賛助)</th>
            <th>ポジション</th>
            <th>退会フラグ</th>
               
        </tr>
        <tr>
            <td><asp:TextBox ID="txtCOCODE" runat="server"   MaxLength="4"></asp:TextBox></td>
            <td><asp:TextBox ID="txtUID" runat="server"   MaxLength="4"></asp:TextBox></td>
            <td><asp:TextBox ID="txtCONAME" runat="server"   MaxLength="40"></asp:TextBox></td>
            <td><asp:TextBox ID="txtshort_CONAME" runat="server" MaxLength="40"></asp:TextBox></td>
            <td><asp:TextBox ID="txtRepName" runat="server"   MaxLength="10" ></asp:TextBox></td>
            <td><asp:TextBox ID="txtPostalCode" runat="server" MaxLength="8" ></asp:TextBox></td>
            <td><asp:TextBox ID="txtAddress" runat="server"  MaxLength="50"></asp:TextBox></td>
            <td><asp:TextBox ID="txtTel" runat="server"   MaxLength="12"></asp:TextBox></td>
            <td><asp:TextBox ID="txtPassword" runat="server"   MaxLength="15"></asp:TextBox></td>
            <td><asp:TextBox ID="txtMember" runat="server"   MaxLength="1"></asp:TextBox></td>
            <td><asp:TextBox ID="txtMemberType" runat="server"   MaxLength="1"></asp:TextBox></td>
            <td><asp:TextBox ID="txtPosition" runat="server" MaxLength="3"></asp:TextBox></td>
            <td><asp:TextBox ID="txtisCanceled" runat="server" MaxLength="3"></asp:TextBox></td>
        </tr>
        </table>
    <!--改行-->
    <div>
        <br/><br/>
    </div>
        <asp:Button ID="btnInsert" runat="server" Text="登録" onclick="btnInsert_Click" 
            onclientclick="return confirmRegister();" />
        <asp:Button ID="btnUpdate" runat="server" Text="更新" onclick="btnUpdate_Click"  
            onclientclick="return confirmRegister();" />
        <asp:Button ID="btnDelete" runat="server" Text="削除" onclick="btnDelete_Click" 
            onclientclick="return confirmDeletion();" />
        <asp:Label ID="lblMsg" runat="server" BackColor="#FF66FF"></asp:Label>
    </div>
<asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False"  
        DataKeyNames="COCODE" onselectedindexchanged="GridView1_SelectedIndexChanged" 
        opageindexchanging="GridView1_PageIndexChanging" PageSize="500" 
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="COCODE" HeaderText="会員コード"   />
            <asp:BoundField DataField="UID" HeaderText="ログインID"   />
            <asp:BoundField DataField="CONAME" HeaderText="会員名"  />
            <asp:BoundField DataField="short_CONAME" HeaderText="略名"  />
            <asp:BoundField DataField="RepName" HeaderText="担当名"  />
            <asp:BoundField DataField="PostalCode" HeaderText="郵便番号"  />
            <asp:BoundField DataField="Address" HeaderText="住所" />
            <asp:BoundField DataField="Tel" HeaderText="電話番号"  />
            <asp:BoundField DataField="Password" HeaderText="パスワード" />
            <asp:BoundField DataField="Member" HeaderText="会員フラグ(0:自販連/1:会員)"  />
            <asp:BoundField DataField="MemberType" HeaderText="会員種別(0:通常/1:賛助)"  />
            <asp:BoundField DataField="Position" HeaderText="ポジション" />
            <asp:BoundField DataField="isCanceled" HeaderText="退会フラグ" />
        </Columns>
        <EditRowStyle BackColor="#7C6F57" />
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#E3EAEB" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F8FAFA" />
        <SortedAscendingHeaderStyle BackColor="#246B61" />
        <SortedDescendingCellStyle BackColor="#D4DFE1" />
        <SortedDescendingHeaderStyle BackColor="#15524A" />
</asp:GridView>
    <!--
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:JissekiConnectionString %>" 
        SelectCommand="SELECT DISTINCT * FROM [ID]"></asp:SqlDataSource>
        -->
    </form>
</body>
</html>
