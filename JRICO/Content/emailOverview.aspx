<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="emailOverview.aspx.cs" Inherits="JRICO.Content.emailOverview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<table border="0" width="100%">
        <tr>
          <td>
              <a href="contractList.aspx">Contract List</a> &nbsp;&nbsp;|&nbsp;&nbsp; Search &nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Value="Contract Reference">Contract Reference</asp:ListItem>
                    <asp:ListItem Value="Email Subject">Email Subject</asp:ListItem>
                    <asp:ListItem Value="Email Content">Email Content</asp:ListItem>
                    <asp:ListItem Value="Message Status">Message Status</asp:ListItem>
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
        BackColor="White" BorderColor="#999999" DataKeyNames="EmailID"
        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AlternatingRowStyles-CssClass="alt"
        CssClass="mGrid" PagerStyle-CssClass="pgr" ForeColor="Black" Width="100%"> 
        <Columns>
                      
            <asp:TemplateField HeaderText="EmailID" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("[EmailID]") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contract Reference" SortExpression="Contract Reference">
                <ItemTemplate>
                   <asp:Label ID="Label2" runat="server" Text='<%# Bind("[Contract Reference]") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email Subject" SortExpression="Email Subject">
               <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("[Email Subject]") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Email Content" SortExpression="Email Content">
                <ItemTemplate>
                   <asp:Label ID="Label2" runat="server" Text='<%# Bind("[Email Content]") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Message Status" SortExpression="Message Status">
                <ItemTemplate>
                   <asp:Label ID="Label2" runat="server" Text='<%# Bind("[Message Status]") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Message Status Date" SortExpression="Message Status Date">
                <ItemTemplate>
                   <asp:Label ID="Label2" runat="server" Text='<%# Bind("[Message Status Date]", "{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Trigger Date" SortExpression="Trigger Date">
                <ItemTemplate>
                   <asp:Label ID="Label2" runat="server" Text='<%# Bind("[Trigger Date]", "{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Date Uploaded" SortExpression="Date Uploaded">
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("[Date Uploaded]", "{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Uploaded By" SortExpression="Uploaded By">
                <ItemTemplate>
                    <asp:Label ID="lblUploadedBy" runat="server" Text='<%# Bind("[Uploaded By]") %>'></asp:Label>
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
