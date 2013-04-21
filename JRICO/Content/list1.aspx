<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list1.aspx.cs" Inherits="JRICO.Content.list1" MasterPageFile="~/Site.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <a href="">Create New</a> &nbsp;&nbsp;|&nbsp;&nbsp; <a href="">Hospital Admin</a> 
     &nbsp;&nbsp;|&nbsp;&nbsp; Search &nbsp;&nbsp;
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
        </asp:DropDownList> &nbsp;&nbsp;for&nbsp;&nbsp;
        <asp:TextBox ID="TextSearch" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Go" onclick="Button1_Click" />
        <asp:GridView ID="GridView1" runat="server" OnSorting="SortRecords" 
            AllowSorting="True" DataKeyNames="ContractID" 
         onrowdatabound="GridView_RowDataBound" BackColor="White" BorderColor="#999999" 
         BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
         AlternatingRowStyles-CssClass="alt" CssClass="mGrid"  
    PagerStyle-CssClass="pgr" ForeColor="Black">
            <AlternatingRowStyle BackColor="#e2e2e2" />
            <Columns>
                <asp:hyperlinkfield text="Details" datanavigateurlfields="ContractID" 
         datanavigateurlformatstring="/Content/Details.aspx?Id={0}" />
            </Columns>           
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />

<PagerStyle CssClass="pgr" BackColor="#999999" ForeColor="Black" HorizontalAlign="Center"></PagerStyle>
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
</asp:Content>