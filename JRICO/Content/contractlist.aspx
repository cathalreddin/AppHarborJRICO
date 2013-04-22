<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="contractlist.aspx.cs" Inherits="JRICO.Content.contractlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<a href="hospitalList1.aspx">Hospital Admin</a> 
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

          <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>"
            SelectCommand="sp_getContractList" SelectCommandType="StoredProcedure">

            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" Name="Column" 
                    PropertyName="SelectedValue" Type="String" DefaultValue="None" />
                <asp:FormParameter FormField="TextSearch" Name="TextSearch" Type="String" DefaultValue="%" />
            </SelectParameters>

        </asp:SqlDataSource>

    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        DataSourceID="SqlDataSource1" ShowFooter="True" BackColor="White" BorderColor="#999999" 
         BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
         AlternatingRowStyles-CssClass="alt" CssClass="mGrid"  
    PagerStyle-CssClass="pgr" ForeColor="Black">

            <Columns>
                <asp:TemplateField HeaderText="ContractID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" 
                            Text='<%# Bind("[ContractID]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contract Reference" 
                    SortExpression="Contract Reference">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" 
                            Text='<%# Bind("[Contract Reference]") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" 
                            Text='<%# Bind("[Contract Reference]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Associated Ref" SortExpression="Associated Ref">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" 
                            Text='<%# Bind("[Associated Ref]") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("[Associated Ref]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Record Type" SortExpression="Record Type">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("[Record Type]") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("[Record Type]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="System Price List" 
                    SortExpression="System Price List">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" 
                            Text='<%# Bind("[System Price List]") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("[System Price List]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contract Title" SortExpression="Contract Title">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" 
                            Text='<%# Bind("[Contract Title]") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("[Contract Title]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email" SortExpression="Email">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Phone" SortExpression="Phone">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("Phone") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("Phone") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Start Date" SortExpression="Start Date">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("[Start Date]") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("[Start Date]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="End Date" SortExpression="End Date">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("[End Date]") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("[End Date]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Submission Date" 
                    SortExpression="Submission Date">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox11" runat="server" 
                            Text='<%# Bind("[Submission Date]") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("[Submission Date]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contract Status" 
                    SortExpression="Contract Status">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox12" runat="server" 
                            Text='<%# Bind("[Contract Status]") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("[Contract Status]") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

             
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
