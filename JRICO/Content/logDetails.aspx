<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="logDetails.aspx.cs" Inherits="JRICO.Content.logDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" width="100%">
        <tr>
          <td>
              <a href="contractList.aspx">Contract List</a> &nbsp;&nbsp;|&nbsp;&nbsp; Search &nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Value="LogData">Log Detail</asp:ListItem>
                    <asp:ListItem Value="LogUser">User</asp:ListItem>
                    <asp:ListItem Value="LogDate">Date</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;for&nbsp;&nbsp;
                <asp:TextBox ID="TextSearch" runat="server"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="Go" OnClick="Button1_Click" />
           </td>
           <td align="right">
               <a href="hospitalList.aspx">Hospital Admin</a>
           </td>
        </tr>
    </table>
<asp:GridView ID="GridView1" runat="server" AllowSorting="True" OnSorting="SortRecords" AutoGenerateColumns="False"
        BackColor="White" BorderColor="#999999" DataKeyNames="LogID"
        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AlternatingRowStyles-CssClass="alt"
        CssClass="mGrid" PagerStyle-CssClass="pgr" ForeColor="Black" Width="100%"> 
        <Columns>           
            <asp:TemplateField HeaderText="LogID" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("[LogID]") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Log Detail" SortExpression="LogData">
                <ItemTemplate>
                   <asp:Label ID="lblHospitalName" runat="server" Text='<%# Bind("[LogData]") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Timestamp" SortExpression="LogDate">
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("[LogDate]") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="User" SortExpression="LogUser">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("[LogUser]") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle BackColor="#e2e2e2" />
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle CssClass="pgr" BackColor="#999999" ForeColor="Black" HorizontalAlign="Center">
        </PagerStyle>
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
</asp:Content>
