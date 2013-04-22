<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="hospitalList.aspx.cs" Inherits="JRICO.Content.hospitalList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <a href="list1.aspx">Contract List</a>
    <asp:GridView ID="GridView1" onrowdatabound="GridView_RowDataBound" runat="server" OnSorting="SortRecords" 
            AllowSorting="True" DataKeyNames="HospitalID" BackColor="White" BorderColor="#999999" 
         BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
         AlternatingRowStyles-CssClass="alt" CssClass="mGrid"  
    PagerStyle-CssClass="pgr" ForeColor="Black">
            <AlternatingRowStyle BackColor="#e2e2e2" />
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
