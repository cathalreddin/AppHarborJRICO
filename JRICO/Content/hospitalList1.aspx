<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="hospitalList1.aspx.cs" Inherits="JRICO.Content.hospitalList1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <a href="contractList.aspx">Contract List</a>
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>"
        SelectCommand="SELECT DISTINCT [HospitalID], [HospitalName], [Address1], [Postcode], [AccountNumber], [DateUploaded], [UserID] FROM [Hospitals]"
        UpdateCommand="UPDATE [hospitals] Set [HospitalName] = @HospitalName, [Address1] = @Address1, [Postcode] = @Postcode, [AccountNumber] = @AccountNumber, [DateUploaded] = @DateUploaded, [UserID] = @User Where [HospitalID] = @HospitalID"
        InsertCommand="INSERT INTO [hospitals] ([HospitalName], [Address1], [Postcode], [AccountNumber], [DateUploaded], [UserID]) VALUES (@HospitalName, @Address1, @Postcode, @AccountNumber, @DateUploaded, @User)">
        <UpdateParameters>
            <asp:Parameter Type="String" Name="HospitalName"></asp:Parameter>
            <asp:Parameter Type="String" Name="Address1"></asp:Parameter>
            <asp:Parameter Type="String" Name="Postcode"></asp:Parameter>
            <asp:Parameter Type="String" Name="AccountNumber"></asp:Parameter>
            <asp:Parameter Type="DateTime" Name="DateUploaded"></asp:Parameter>
            <asp:Parameter Type="String" Name="User"></asp:Parameter>
        </UpdateParameters>

        <InsertParameters>
            <asp:Parameter Type="String" Name="HospitalName"></asp:Parameter>
            <asp:Parameter Type="String" Name="Address1"></asp:Parameter>
            <asp:Parameter Type="String" Name="Postcode"></asp:Parameter>
            <asp:Parameter Type="String" Name="AccountNumber"></asp:Parameter>
            <asp:Parameter Type="DateTime" Name="DateUploaded"></asp:Parameter>
            <asp:Parameter Type="String" Name="User"></asp:Parameter>
        </InsertParameters>
    </asp:SqlDataSource>

    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        DataSourceID="SqlDataSource1" ShowFooter="True" DataKeyNames="HospitalID" BackColor="White" BorderColor="#999999" 
         BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
         AlternatingRowStyles-CssClass="alt" CssClass="mGrid"  
    PagerStyle-CssClass="pgr" ForeColor="Black">

        <Columns>
            <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Wrap="false">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                        CommandName="Update" Text="Update"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                        CommandName="Edit" Text="Edit"></asp:LinkButton>
                </ItemTemplate>
                <footertemplate>
                    <asp:LinkButton ID="lbInsert" runat="server" ValidationGroup="insert" OnClick="lbInsert_Click">Insert</asp:LinkButton>
                </footertemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="HospitalID" SortExpression="HospitalID" Visible="false">
                <EditItemTemplate>
                    <asp:label ID="LblHospitalID" runat="server" Text='<%# Eval("HospitalID") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LblHospitalID" runat="server" Text='<%# Bind("HospitalID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="HospitalName" SortExpression="HospitalName" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("HospitalName") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfcEditName" runat="server" ErrorMessage="Hospital Name is a required field!"
                        ControlToValidate="TextBox1" Text="*" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("HospitalName") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtHospitalName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="insert" ID="rfcInsertName" runat="server" ErrorMessage="Hospital Name is a required field!"
                        ControlToValidate="txtHospitalName" Text="*" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Address1" SortExpression="Address1" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Address1") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfcEditAddress" runat="server" ErrorMessage="Address is a required field!"
                        ControlToValidate="TextBox2" Text="*" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Address1") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtAddress1" runat="server"></asp:TextBox>
                     <asp:RequiredFieldValidator ValidationGroup="insert" ID="rfcInsertAddress" runat="server" ErrorMessage="Address is a required field!"
                        ControlToValidate="txtAddress1" Text="*" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Postcode" SortExpression="Postcode" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Postcode") %>' Width="65"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="rfcEditPostcode" runat="server" ErrorMessage="Postcode is a required field!"
                        ControlToValidate="TextBox3" Text="*" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Postcode") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtPostcode" runat="server" Width="65"></asp:TextBox>
                     <asp:RequiredFieldValidator ValidationGroup="insert" ID="rfcInsertPostcode" runat="server" ErrorMessage="Postcode is a required field!"
                        ControlToValidate="txtPostcode" Text="*" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="AccountNumber" SortExpression="AccountNumber" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("AccountNumber") %>' Width="75"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="rfcEditAccountNumber" runat="server" ErrorMessage="Account number is a required field!"
                        ControlToValidate="TextBox4" Text="*" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("AccountNumber") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtAccountNumber" runat="server" Width="75"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="insert" ID="rfcInsertAccountNumber" runat="server" ErrorMessage="Account Number is a required field!"
                        ControlToValidate="txtAccountNumber" Text="*" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="DateUploaded" SortExpression="DateUploaded" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">
                <EditItemTemplate>
                    <asp:Label ID="lblEditDateUploaded" runat="server" Text='<%# Bind("DateUploaded") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("DateUploaded") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UserID" SortExpression="UserID" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">
                <EditItemTemplate>
                    <asp:Label ID="lblEditUser" runat="server" Text='<%# Bind("UserID") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("UserID") %>'></asp:Label>
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
    <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="insert" runat="server" ForeColor="Red" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" />
</asp:Content>
