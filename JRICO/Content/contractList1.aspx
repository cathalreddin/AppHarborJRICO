<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
CodeBehind="contractList1.aspx.cs" Inherits="JRICO.Content.contractList1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.10.2.custom.js" type="text/javascript"></script>
    <link href="../Styles/jquery-ui-1.10.2.custom.css" rel="stylesheet" type="text/css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" width="100%">
        <tr>
            <td>
                <a href="addContract.aspx">Add New Contract</a> &nbsp;&nbsp;|&nbsp;&nbsp; Search &nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Value="ContractReference">Contract Reference</asp:ListItem>
                    <asp:ListItem Value="ContractTitle">Contract Title</asp:ListItem>
                    <asp:ListItem Value="AssociatedContractReference">Associated Ref</asp:ListItem>
                    <asp:ListItem Value="RecordType">Record Type</asp:ListItem>
                    <asp:ListItem Value="ContractSystemPriceList">System Price List</asp:ListItem>
                    <asp:ListItem Value="ContactName">Name</asp:ListItem>
                    <asp:ListItem Value="ContactEmail">Email</asp:ListItem>
                    <asp:ListItem Value="ContactNo">Phone</asp:ListItem>
                    <asp:ListItem Value="ContractStatus">Contract Status</asp:ListItem>
                    <asp:ListItem Value="HospitalName">Hospital Name</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;for&nbsp;&nbsp;
                <asp:TextBox ID="TextSearch" runat="server"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="Go" OnClick="Button1_Click" />
           </td>
           <td align="right">
               <a href="hospitalList1.aspx">Hospital Admin</a>
           </td>
        </tr>

    </table>
    
    <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>"
        SelectCommand="sp_getContractList" SelectCommandType="StoredProcedure" UpdateCommand="sp_updateContractList"
        UpdateCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" Name="Column" PropertyName="SelectedValue"
                Type="String" DefaultValue="None" />
            <asp:FormParameter FormField="TextSearch" Name="TextSearch" Type="String" DefaultValue="%" />
        </SelectParameters>
    </asp:SqlDataSource>--%>
    <asp:SqlDataSource ID="sdsRecordType" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>" 
        SelectCommand="SELECT [RecordTypeID], [Name] FROM [RecordTypes]"></asp:SqlDataSource>

     <asp:SqlDataSource ID="sdsContractStatus" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>" 
        SelectCommand="SELECT [ContractStatusID], [Name] FROM [ContractStatus]"></asp:SqlDataSource>

    <asp:SqlDataSource ID="sdsAssociatedRef" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>" 
        SelectCommand="SELECT [AssociatedContractReferenceID], [Name] FROM [AssociatedContractReference] ORDER BY [Name]"></asp:SqlDataSource>

    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" OnSorting="SortRecords" AutoGenerateColumns="False"
        ShowFooter="True" BackColor="White" BorderColor="#999999" DataKeyNames="ContractID"
        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AlternatingRowStyles-CssClass="alt"
        CssClass="mGrid" PagerStyle-CssClass="pgr" ForeColor="Black"  
        OnRowEditing="RowEdit" OnRowUpdating="RowUpdate" OnRowCancelingEdit="RowEditCancel"> 
        <Columns>
            <asp:CommandField ShowEditButton="True" ShowCancelButton="true" />                  
            <asp:TemplateField HeaderText="ContractID" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("[ContractID]") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="lblContractID" runat="server" Text='<%# Eval("[ContractID]") %>'></asp:Label>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contract Reference" SortExpression="Contract Reference">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("[Contract Reference]") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                   <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("[ContractID]", "details.aspx?id={0}") %>' Text='<%# Eval("[Contract Reference]") %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Associated Ref" SortExpression="Associated Ref">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlAssociatedRef" runat="server"
                    DataTextField="Name" 
                    DataValueField="AssociatedContractReferenceID"
                    DataSourceID="sdsAssociatedRef"
                    SelectedValue='<%# Bind("[AssociatedContractReferenceID]") %>'>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("[Associated Ref]") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Record Type" SortExpression="Record Type">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlRecordType" runat="server"
                    DataTextField="Name" 
                    DataValueField="RecordTypeID"
                    DataSourceID="sdsRecordType"
                    SelectedValue='<%# Bind("[RecordTypeID]") %>'>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("[Record Type]") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="System Price List" SortExpression="System Price List">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("[System Price List]") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("[System Price List]") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contract Title" SortExpression="Contract Title">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("[Contract Title]") %>'></asp:TextBox>
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
                  <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("[Start Date]", "{0:dd/MM/yyyy}") %>' CssClass="datepickersd"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("[Start Date]", "{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="End Date" SortExpression="End Date">
                <EditItemTemplate>
                   <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("[End Date]", "{0:dd/MM/yyyy}") %>' CssClass="datepickered"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("[End Date]", "{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Submission Date" SortExpression="Submission Date">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("[Submission Date]", "{0:dd/MM/yyyy}") %>' Visible="false"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("[Submission Date]", "{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contract Status" SortExpression="Contract Status">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlContractStatus" runat="server"
                    DataTextField="Name" 
                    DataValueField="ContractStatusID"
                    DataSourceID="sdsContractStatus"
                    SelectedValue='<%# Bind("[ContractStatusID]") %>'>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("[Contract Status]") %>'></asp:Label>
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

    <script language="javascript" type="text/javascript">
        $(function () {
            $(".datepickered").datepicker({ dateFormat: 'dd/mm/yy' }); ;
        });

        $(function () {
            $(".datepickersd").datepicker({ dateFormat: 'dd/mm/yy' }); ;
        });
    </script>
   
</asp:Content>


