<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="JRICO.Content.list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Create New | Search 
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem value="ContractReference">Contract Reference</asp:ListItem>
            <asp:ListItem value="ContractTitle">Contract Title</asp:ListItem>
            <asp:ListItem value="LinkedContractReference">Associated Ref</asp:ListItem>
            <asp:ListItem value="RecordType">Record Type</asp:ListItem>
            <asp:ListItem value="ContractSystemPriceList">System Price List</asp:ListItem>
            <asp:ListItem value="ContactName">Name</asp:ListItem>
            <asp:ListItem value="ContactEmail">Email</asp:ListItem>
            <asp:ListItem value="ContactNo">Phone</asp:ListItem>
            <asp:ListItem value="ContractStatus">Contract Status</asp:ListItem>
            <asp:ListItem value="HospitalName">Hospital Name</asp:ListItem>
        </asp:DropDownList> for
        <asp:TextBox ID="TextSearch" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Go" />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>" 
            SelectCommand="sp_getContractList" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" Name="Column" 
                    PropertyName="SelectedValue" Type="String" DefaultValue="None" />
                <asp:FormParameter FormField="TextSearch" Name="TextSearch" Type="String" DefaultValue="%" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="Contract Reference" HeaderText="Contract Reference" 
                    SortExpression="Contract Reference" />
                <asp:BoundField DataField="Associated Ref" HeaderText="Associated Ref" 
                    SortExpression="Associated Ref" />
                <asp:BoundField DataField="Record Type" HeaderText="Record Type" 
                    SortExpression="Record Type" />
                <asp:BoundField DataField="System Price List" HeaderText="System Price List" 
                    SortExpression="System Price List" />
                <asp:BoundField DataField="Contract Title" HeaderText="Contract Title" 
                    SortExpression="Contract Title" />
                <asp:BoundField DataField="Name" HeaderText="Name" 
                    SortExpression="Name" />
                <asp:BoundField DataField="Email" HeaderText="Email" 
                    SortExpression="Email" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" 
                    SortExpression="Phone" />
                <asp:BoundField DataField="Start Date" HeaderText="Start Date" 
                    SortExpression="Start Date" />
                <asp:BoundField DataField="End Date" HeaderText="End Date" 
                    SortExpression="End Date" />
                <asp:BoundField DataField="Submission Date" HeaderText="Submission Date" 
                    SortExpression="Submission Date" />
                <asp:BoundField DataField="Contract Status" HeaderText="Contract Status" 
                    SortExpression="Contract Status" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
