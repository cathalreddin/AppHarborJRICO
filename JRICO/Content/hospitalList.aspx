<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="hospitalList.aspx.cs" Inherits="JRICO.Content.hospitalList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <%--<asp:ScriptManager ID="ScriptManager1" runat="server"> 
    </asp:ScriptManager>--%>
<table border="0" width="100%">
        <tr>
          <td>
              <a href="contractList.aspx">Contract List</a> &nbsp;&nbsp;|&nbsp;&nbsp; Search &nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Value="HospitalName">Hospital Name</asp:ListItem>
                    <asp:ListItem Value="Address1">Address</asp:ListItem>
                    <asp:ListItem Value="Postcode">Postcode</asp:ListItem>
                    <asp:ListItem Value="AccountNumber">Account Number</asp:ListItem>
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
        ShowFooter="True" BackColor="White" BorderColor="#999999" DataKeyNames="HospitalID"
        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AlternatingRowStyles-CssClass="alt"
        CssClass="mGrid" PagerStyle-CssClass="pgr" ForeColor="Black"  
        OnRowEditing="RowEdit" OnRowUpdating="RowUpdate" OnRowCancelingEdit="RowEditCancel" Width="100%" > 
        <Columns>
           <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Wrap="false">
                <footertemplate>
                    <asp:LinkButton ID="lbInsert" runat="server" ValidationGroup="insert" OnClick="lbInsert_Click">Insert</asp:LinkButton>
                </footertemplate>
              <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False"></ItemStyle>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" ShowCancelButton="true" />                  
            <asp:TemplateField HeaderText="HospitalID" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("[HospitalID]") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="lblHospitalID" runat="server" Text='<%# Eval("[HospitalID]") %>'></asp:Label>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hospital Name" SortExpression="Hospital Name">
                <EditItemTemplate>
                    <asp:TextBox ID="txtHospitalName" runat="server" Text='<%# Bind("[Hospital Name]") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                   <asp:HyperLink ID="hlHospitalName" runat="server" NavigateUrl='<%# String.Format("map.aspx?lat={0}&lon={1}", Eval("lat"), Eval("lon")) %>' Text='<%# Eval("[Hospital Name]") %>' Target="_blank"></asp:HyperLink>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtHospitalNameInsert" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="insert" ID="rfcInsertHospitalName" runat="server" ErrorMessage="Hospital Name is a required field!"
                        ControlToValidate="txtHospitalNameInsert" Text="*" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </FooterTemplate>
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Address" SortExpression="Address">
                <EditItemTemplate>
                    <asp:TextBox ID="txtAddress1" runat="server" Text='<%# Bind("[Address]") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("[Address]") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtAddress1Insert" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="insert" ID="rfcInsertAddress1" runat="server" ErrorMessage="Address is a required field!"
                        ControlToValidate="txtAddress1Insert" Text="*" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </FooterTemplate>
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Postcode" SortExpression="Postcode">
                <EditItemTemplate>
                    <asp:TextBox ID="txtpostcode" runat="server" Text='<%# Bind("[Postcode]") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("[Postcode]") %>'></asp:Label>
                    <%--<ajax:PopupControlExtender id="PopupControlExtender1" runat="server" popupcontrolid="Panel1"
                        targetcontrolid="Label5" dynamiccontextkey='<%# Eval("Postcode") %>' dynamiccontrolid="Panel1"
                        dynamicservicemethod="GetDynamicContent" position="Bottom"> 
            </ajax:PopupControlExtender>--%>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtPostcodeInsert" runat="server" Width="60"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="insert" ID="rfcInsertPostcode" runat="server" ErrorMessage="Postcode is a required field!"
                        ControlToValidate="txtPostcodeInsert" Text="*" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </FooterTemplate>
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Account Number" SortExpression="Account Number">
                <EditItemTemplate>
                    <asp:TextBox ID="txtAccountNumber" runat="server" Text='<%# Bind("[Account Number]") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblAccountNumber" runat="server" Text='<%# Bind("[Account Number]") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtAccountNumberInsert" runat="server" Width="60"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="insert" ID="rfcInsertAccountNumber" runat="server" ErrorMessage="Account Number is a required field!"
                        ControlToValidate="txtAccountNumberInsert" Text="*" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </FooterTemplate>
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date Uploaded" SortExpression="Date Uploaded">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("[Date Uploaded]", "{0:dd/MM/yyyy}") %>' Visible="false"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("[Date Uploaded]", "{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Uploaded By" SortExpression="Uploaded By">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox6" visible="false" runat="server" Text='<%# Bind("[Uploaded By]") %>'></asp:TextBox>
                </EditItemTemplate>
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
   <%-- <asp:Panel ID="Panel1" runat="server"> </asp:Panel> --%>
    <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="insert" runat="server" ForeColor="Red" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" />
</asp:Content>
