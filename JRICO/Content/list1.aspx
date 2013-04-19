<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list1.aspx.cs" Inherits="JRICO.Content.list1" %>

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
        <asp:Button ID="Button1" runat="server" Text="Go" onclick="Button1_Click" />
        <asp:GridView ID="GridView1" runat="server" OnSorting="SortRecords" 
            AllowSorting="True" DataKeyNames="ContractID" onrowdatabound="GridView_RowDataBound" >
            <Columns>
                <asp:hyperlinkfield text="Details" datanavigateurlfields="ContractID" 
         datanavigateurlformatstring="/Content/Details.aspx?Id={0}" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
