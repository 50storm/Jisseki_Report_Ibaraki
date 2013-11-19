<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitPriceMaster.aspx.cs" Inherits="Jisseki_Report_Ibaraki.jada.master.UnitPriceMaster" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        #Wrapper
        {
	        width: 400px;  
	        height:auto;
	        margin: 100px auto;
        }
        #Content
        {
        	
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
            if (!isNumber("FormView1_BigSizeTextBox")) {
                
                flag.BigSize=false;
            }else{
                flag.BigSize=true;

            }

            if (!isNumber("FormView1_MediumSmallTextBox")) {
                flag.MediumSmall=false;
            }else{
                flag.MediumSmall=true;
            }

            if (!isNumber("FormView1_AverageTextBox")) {
                flag.Average=false;
            }else{
                flag.Average=true;
            }


            if (!isNumber("FormView1_Kamotu7tTextBox")) {
                flag.Kamotu7t=false;
            }else{
                flag.Kamotu7t=true;
            }


            if (!isNumber("FormView1_Kamotu6DP9_5tTextBox")) {
                flag.Kamotu6DP9_5t=false;
            }else{
                flag.Kamotu6DP9_5t=true;
            }

            if (!isNumber("FormView1_Kamotu4DP9_3tTextBox")) {
                flag.Kamotu4DP9_3t=false;
            }else{
                flag.Kamotu4DP9_3t=true;
            }

            if (!isNumber("FormView1_Kamotu2DP9_2DP5tTextBox")) {
                flag.Kamotu2DP9_2DP5t=false;
            }else{
                flag.Kamotu2DP9_2DP5t=true;
            }

            if (!isNumber("FormView1_Over2001ccTextBox")) {
                flag.Over2001cc=false;
            }else{
                flag.Over2001cc=true;
            }

            if (!isNumber("FormView1_To2000From1000ccTextBox")) {
                flag.To2000From1000cc=false;
            }else{
                flag.To2000From1000cc=true;
            }

            if (!isNumber("FormView1_Over30TextBox")) {
                flag.Over30=false;
            }else{
                flag.Over30=true;
            }

            if (!isNumber("FormView1_LessThan29TextBox")) {
                flag.LessThan29=false;
            }else{
                flag.LessThan29=true;
            }

            if (!isNumber("FormView1_MemberFeeTextBox")) {
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
    <div id="Wrapper">

        <asp:FormView ID="FormView1" runat="server" DataKeyNames="Code,COCODE" 
            DataSourceID="SqlDataSource_UnitPriceMaster" BackColor="#DEBA84" 
            BorderColor="#DEBA84" BorderStyle="Solid" BorderWidth="3px" CellPadding="3" 
            CellSpacing="2" DefaultMode="Edit" GridLines="Both" 
            onitemupdating="FormView1_ItemUpdating" HeaderText="単価マスター" 
            FooterText="仮請求書の単価を設定" onitemupdated="FormView1_ItemUpdated" 
            EmptyDataText="単価マスターに設定がありません。" HorizontalAlign="Center">
            <EditItemTemplate>
                台数割会費　大型:
                <asp:TextBox ID="BigSizeTextBox" runat="server" Text='<%# Bind("BigSize") %>' />
                <br />
                台数割会費　中・小型:
                <asp:TextBox ID="MediumSmallTextBox" runat="server" 
                    Text='<%# Bind("MediumSmall") %>' />
                <br />
                均等割会費:
                <asp:TextBox ID="AverageTextBox" runat="server" Text='<%# Bind("Average") %>' />
                <br />
                ７ｔ以上:
                <asp:TextBox ID="Kamotu7tTextBox" runat="server" 
                    Text='<%# Bind("Kamotu7t") %>' />
                <br />
                ６．９ｔ～５ｔ:
                <asp:TextBox ID="Kamotu6DP9_5tTextBox" runat="server" 
                    Text='<%# Bind("Kamotu6DP9_5t") %>' />
                <br />
                ４．９ｔ～３ｔ:
                <asp:TextBox ID="Kamotu4DP9_3tTextBox" runat="server" 
                    Text='<%# Bind("Kamotu4DP9_3t") %>' />
                <br />
                ２．９ｔ～２．５ｔ:
                <asp:TextBox ID="Kamotu2DP9_2DP5tTextBox" runat="server" 
                    Text='<%# Bind("Kamotu2DP9_2DP5t") %>' />
                <br />
                ２，００１ｃｃ以上:
                <asp:TextBox ID="Over2001ccTextBox" runat="server" 
                    Text='<%# Bind("Over2001cc") %>' />
                <br />
                ２，０００ｃｃ～１，０００ｃｃ:
                <asp:TextBox ID="To2000From1000ccTextBox" runat="server" 
                    Text='<%# Bind("To2000From1000cc") %>' />
                <br />
                乗合定員３０人以上:
                <asp:TextBox ID="Over30TextBox" runat="server" Text='<%# Bind("Over30") %>' />
                <br />
                乗合定員２９人以下:
                <asp:TextBox ID="LessThan29TextBox" runat="server" 
                    Text='<%# Bind("LessThan29") %>' />
                <br />
                会費:
                <asp:TextBox ID="MemberFeeTextBox" runat="server"  Text='<%# Bind("MemberFee") %>' />
                <br />
                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True"  CommandName="Update" Text="更新" OnClientClick="return checkForm();" />
                &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                    CausesValidation="False" CommandName="Cancel" Text="キャンセル"  />
            </EditItemTemplate>
            <EditRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" BorderStyle="Solid" 
                BorderWidth="3px" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" Wrap="True" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" BorderStyle="Dashed" 
                BorderWidth="1px" />
        </asp:FormView>
        <asp:SqlDataSource ID="SqlDataSource_UnitPriceMaster" runat="server" 
            ConnectionString="<%$ ConnectionStrings:JissekiConnectionString %>" SelectCommand="SELECT TOP 1 *
  FROM [Jisseki_Report_Ibaraki].[dbo].[UnitPrice]
where 
Code='01'" 
            UpdateCommand="UPDATE UnitPrice SET BigSize = @BigSize, MediumSmall = @MediumSmall, Average = @Average, Kamotu7t = @Kamotu7t, Kamotu6DP9_5t = @Kamotu6DP9_5t, Kamotu4DP9_3t = @Kamotu4DP9_3t, Kamotu2DP9_2DP5t = @Kamotu2DP9_2DP5t, Over2001cc = @Over2001cc, To2000From1000cc = @To2000From1000cc, MemberFee = @MemberFee, LessThan29 = @LessThan29, Over30 = @Over30 WHERE (Code = '01')">
            <UpdateParameters>
                <asp:Parameter Name="BigSize" />
                <asp:Parameter Name="MediumSmall" />
                <asp:Parameter Name="Average" />
                <asp:Parameter Name="Kamotu7t" />
                <asp:Parameter Name="Kamotu6DP9_5t" />
                <asp:Parameter Name="Kamotu4DP9_3t" />
                <asp:Parameter Name="Kamotu2DP9_2DP5t" />
                <asp:Parameter Name="Over2001cc" />
                <asp:Parameter Name="To2000From1000cc" />
                <asp:Parameter Name="MemberFee" />
                <asp:Parameter Name="LessThan29" />
                <asp:Parameter Name="Over30" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
