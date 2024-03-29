﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="details.aspx.cs" Inherits="JRICO.Content.details" %>

<%@ Register Assembly="JRICO" TagPrefix="Custom" Namespace="JRICO.CodeArea" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.10.2.custom.js" type="text/javascript"></script>
    <link href="../Styles/jquery-ui-1.10.2.custom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Initial
        {
            display: block;
            padding: 4px 18px 4px 18px;
            float: left;
            background: url("../Images/InitialImage1.png") no-repeat right top;
            color: Black;
            font-weight: bold;
        }
        .Initial:hover
        {
            color: White;
            background: url("../Images/SelectedButton1.png") no-repeat right top;
        }
        .Clicked
        {
            float: left;
            display: block;
            background: url("../Images/SelectedButton1.png") no-repeat right top;
            padding: 4px 18px 4px 18px;
            color: Black;
            font-weight: bold;
            color: White;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" width="100%">
        <tr>
            <td>
                &nbsp;<a href="contractList.aspx">Contract List</a>
            </td>
            <td align="center">
                <asp:Label ID="lblContractReferenceLabel" runat="server" Text="&nbsp;&nbsp; Contract Reference: " />
                <asp:Label ID="lblContractReferenceHeading" runat="server" />
                <asp:Label ID="lblContractTitleLabel" runat="server" Text="&nbsp;&nbsp;|&nbsp;&nbsp; Contract Title: " />
                <asp:Label ID="lblContractTitleHeading" runat="server" />
                <asp:HyperLink ID="hlMessage" runat="server" />
            </td>
            <td align="right">
                <a href="hospitalList.aspx">Hospital Admin</a>
            </td>
        </tr>
    </table>
    <table width="100%" align="center">
        <tr>
            <td>
                <asp:Button Text="Record Overview" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server"
                    OnClick="Tab1_Click" />
                <asp:Button Text="Associated Documentation" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server"
                    OnClick="Tab2_Click" />
                <asp:Button Text="Email Alerts & Notes" BorderStyle="None" ID="Tab3" CssClass="Initial" runat="server"
                    OnClick="Tab3_Click" />
                <asp:Button Text="System Prices" BorderStyle="None" ID="Tab4" CssClass="Initial" runat="server"
                    OnClick="Tab4_Click" />
                <asp:Button Text="Hospitals" BorderStyle="None" ID="Tab5" CssClass="Initial" runat="server"
                    OnClick="Tab5_Click" />
                <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">
                        <asp:Table ID="Table1" runat="server" Style="width: 100%; border-width: 1px; border-color: #666;
                            border-style: solid; vertical-align: middle;">
                            <asp:TableRow>
                                <asp:TableCell Text="Contract Reference"></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblContractReference" runat="server"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Text="Record Type"></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblRecordType" runat="server"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Text="System Price List"></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblSystemPriceList" runat="server"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Text="Contract Title"></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblContractTitle" runat="server"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Text="Contract Status"></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblContractStatus" runat="server"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell Text="Associated Reference"></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblAssociatedReference" runat="server"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Text="Name"></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblName" runat="server"></asp:Label></asp:TableCell>
                                <asp:TableCell Text="Email"></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblEmail" runat="server"></asp:Label></asp:TableCell>
                                <asp:TableCell Text="Phone"></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblPhone" runat="server"></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Text="Start Date"></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblStartDate" runat="server"></asp:Label></asp:TableCell>
                                <asp:TableCell Text="End Date"></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblEndDate" runat="server"></asp:Label></asp:TableCell>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Text="Uploaded By"></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblUploadedBy" runat="server"></asp:Label></asp:TableCell>
                                <asp:TableCell Text="Date Uploaded"></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblDateUploaded" runat="server"></asp:Label></asp:TableCell>
                                <asp:TableCell></asp:TableCell>
                                <asp:TableCell></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <asp:Table ID="Table2" runat="server" Style="width: 100%; border-width: 1px; border-color: #666;
                            border-style: solid; vertical-align: middle;">
                            <asp:TableRow>
                                <asp:TableCell>
                                    &nbsp;&nbsp; Search &nbsp;&nbsp;
                                    <asp:DropDownList ID="DropDownListAttachment" runat="server">
                                        <asp:ListItem Value="AttachmentTitle">Title</asp:ListItem>
                                        <asp:ListItem Value="AttachmentName">Attachment Name</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;for&nbsp;&nbsp;
                                    <asp:TextBox ID="txtAttachmentSearch" runat="server"></asp:TextBox>
                                    <asp:Button ID="Button3" runat="server" Text="Go" OnClick="Button_Attachment" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                        <Custom:GridViewAlwaysShow ID="GridView2" runat="server" AllowSorting="True" OnSorting="SortRecordsAttachment"
                            AutoGenerateColumns="False" ShowFooter="True" BackColor="White" BorderColor="#999999"
                            DataKeyNames="AttachmentID" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                            GridLines="Vertical" AlternatingRowStyles-CssClass="alt" CssClass="mGrid" PagerStyle-CssClass="pgr"
                            ForeColor="Black" Width="100%" OnRowDeleting="deleteAttachmentRow" OnRowDataBound="GridView2_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="AttachmentID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAttachmentID" runat="server" Text='<%# Bind("[AttachmentID]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Attachment Title" SortExpression="AttachmentTitle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAttachmentTitle" runat="server" Text='<%# Bind("[AttachmentTitle]") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAttachmentTitleInsert" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="insertAttachment" ID="rfcInsertAttachmentTitle"
                                            runat="server" ErrorMessage="Title is a required field!" ControlToValidate="txtAttachmentTitleInsert"
                                            Text="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Attachment Name" SortExpression="AttachmentName">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbAttachmentName" runat="server" Text='<%# Bind("[AttachmentName]") %>'
                                            OnCommand="DownloadApplicationFile" CommandArgument='<%# Bind("[AttachmentName]") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:FileUpload ID="fAttachmentNameInsert" runat="server" />
                                        <asp:RequiredFieldValidator ValidationGroup="insertAttachment" ID="rfcInsertAttachmentName"
                                            runat="server" ErrorMessage="Please select a file!" ControlToValidate="fAttachmentNameInsert"
                                            Text="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Uploaded" SortExpression="Attachment Date Uploaded">
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("[Attachment Date Uploaded]", "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                        <FooterTemplate>
                                        <asp:LinkButton ID="lbInsert" runat="server" ValidationGroup="insertAttachment" OnClick="lbInsert_Attachment">Insert</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Uploaded By" SortExpression="Attachment Uploaded By">
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("[Attachment Uploaded By]") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="DeleteAttachmentButton" runat="server" 
                                            CommandName="Delete" Text="Delete"  OnClientClick="return confirm('Are you sure you want to delete this attachment?');"></asp:LinkButton>
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
                        </Custom:GridViewAlwaysShow>
                        <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="insertAttachment"
                            runat="server" ForeColor="Red" />
                    </asp:View>
                    <asp:View ID="View3" runat="server">
                        <asp:Table ID="Table3" runat="server" Style="width: 100%; border-width: 1px; border-color: #666;
                            border-style: solid; vertical-align: middle;">
                            <asp:TableRow>
                                <asp:TableCell Text="Email Reminder" ColumnSpan="4" Font-Bold="true">
                                    <asp:Label ID="lblEmailID" runat="server" Visible="false"></asp:Label></asp:TableCell><asp:TableCell
                                        HorizontalAlign="right">
                                        <asp:LinkButton ID="lbEditEmail" runat="server" OnClick="lbEdit_Email">Edit</asp:LinkButton>
                                        <asp:LinkButton ID="lbUpdateEmail" runat="server" OnClick="lbUpdate_Email">Update</asp:LinkButton>&nbsp;&nbsp;
                                        <asp:LinkButton ID="lbCancelEmail" runat="server" OnClick="lbCancel_Email">Cancel</asp:LinkButton>
                                    </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="lblEmailToHeading" runat="server" Text="To" /></asp:TableCell><asp:TableCell ColumnSpan="2">
                                        <asp:Label ID="lblEmailTo" runat="server" /><asp:TextBox ID="txtEmailTo" runat="server"
                                            Width="550" type="email" /><asp:Label ID="lblEmailToRFC" runat="server" Width="15"/></asp:TableCell><asp:TableCell>
                                                <asp:Label ID="lblEmailDateHeading" runat="server" Text="Email Trigger Date" /></asp:TableCell><asp:TableCell>
                                                    <asp:Label ID="lblEmailDate" runat="server" /><asp:TextBox ID="txtEmailDate" runat="server"
                                                        Width="75" CssClass="txtEmailDate" /><asp:Label ID="lblEmailDateRFC" runat="server" Width="15"/></asp:TableCell></asp:TableRow><asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="lblEmailSubjectHeading" runat="server" Text="Subject" /></asp:TableCell><asp:TableCell ColumnSpan="2">
                                    <asp:Label ID="lblEmailSubject" runat="server" /><asp:TextBox ID="txtEmailSubject"
                                        runat="server" Width="550" /><asp:Label ID="lblEmailSubjectRFC" runat="server" Width="15"/></asp:TableCell><asp:TableCell>
                                         <asp:Label ID="lblEmailStatusHeading" runat="server" Text="STATUS" /></asp:TableCell><asp:TableCell>
                                                    <asp:Label ID="lblEmailStatus" runat="server" /></asp:TableCell></asp:TableRow><asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="lblEmailContentHeading" runat="server" Text="Content" /></asp:TableCell><asp:TableCell
                                        ColumnSpan="4">
                                        <asp:Label ID="lblEmailContent" runat="server" /><asp:TextBox ID="txtEmailContent"
                                            runat="server" Width="550" /><asp:Label ID="lblEmailContentRFC" runat="server" Width="15"/></asp:TableCell></asp:TableRow><asp:TableRow>
                                <asp:TableCell CssClass="5"></asp:TableCell></asp:TableRow></asp:Table><br /><asp:Table ID="Table33" runat="server" Style="width: 100%; border-width: 1px; border-color: #666;
                            border-style: solid; vertical-align: middle;">
                            <asp:TableRow>
                                <asp:TableCell>
                                    &nbsp;&nbsp; Search &nbsp;&nbsp;
                                    <asp:DropDownList ID="DropDownListNote" runat="server">
                                        <asp:ListItem Value="NoteTitle">Title</asp:ListItem>
                                        <asp:ListItem Value="NoteDescription">Description</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;for&nbsp;&nbsp;
                                    <asp:TextBox ID="txtNoteSearch" runat="server"></asp:TextBox>
                                    <asp:Button ID="Button4" runat="server" Text="Go" OnClick="Button_Note" />
                                </asp:TableCell></asp:TableRow></asp:Table><Custom:GridViewAlwaysShow ID="GridView3" runat="server" AllowSorting="True" OnSorting="SortRecordsNote"
                            AutoGenerateColumns="False" ShowFooter="True" BackColor="White" BorderColor="#999999"
                            DataKeyNames="NoteID" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                            AlternatingRowStyles-CssClass="alt" CssClass="mGrid" PagerStyle-CssClass="pgr"
                            ForeColor="Black" Width="100%" OnMustAddARow="GridView3_MustAddARow" EmptyDataText="There are no data records to display."
                             OnRowDeleting="deleteNoteRow" OnRowDataBound="GridView3_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="NoteID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LblNoteID" runat="server" Text='<%# Bind("[NoteID]") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Note Title" SortExpression="NoteTitle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("[NoteTitle]") %>'></asp:Label></ItemTemplate><FooterTemplate>
                                        <asp:TextBox ID="txtNoteTitleInsert" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                            ValidationGroup="insertNote" ID="rfcInsertTitle" runat="server" ErrorMessage="Title is a required field!"
                                            ControlToValidate="txtNoteTitleInsert" Text="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator></FooterTemplate><ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Note Description" SortExpression="NoteDescription">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoteDescription" runat="server" Text='<%# Bind("[NoteDescription]") %>'></asp:Label></ItemTemplate><FooterTemplate>
                                        <asp:TextBox ID="txtNoteDescriptionInsert" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                            ValidationGroup="insertNote" ID="rfcInsertNoteDescription" runat="server" ErrorMessage="Description is a required field!"
                                            ControlToValidate="txtNoteDescriptionInsert" Text="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator></FooterTemplate><ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Uploaded" SortExpression="Note Date Uploaded">
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("[Note Date Uploaded]", "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate><FooterTemplate>
                                        <asp:LinkButton ID="lbInsertNote" runat="server" ValidationGroup="insertNote" OnClick="lbInsert_Note">Insert</asp:LinkButton></FooterTemplate></asp:TemplateField><asp:TemplateField HeaderText="Uploaded By" SortExpression="Note Uploaded By">
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("[Note Uploaded By]") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="DeleteNoteButton" runat="server" 
                                            CommandName="Delete" Text="Delete"  OnClientClick="return confirm('Are you sure you want to delete this note?');"></asp:LinkButton></ItemTemplate></asp:TemplateField></Columns><AlternatingRowStyle BackColor="#e2e2e2" />
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pgr" BackColor="#999999" ForeColor="Black" HorizontalAlign="Center">
                            </PagerStyle>
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#808080" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#383838" />
                        </Custom:GridViewAlwaysShow>
                        <asp:ValidationSummary ID="ValidationSummary3" ValidationGroup="insertNote" runat="server"
                            ForeColor="Red" />
                    </asp:View>
                    <asp:View ID="View4" runat="server">
                        <asp:Table ID="Table4" runat="server" Style="width: 100%; border-width: 1px; border-color: #666;
                            border-style: solid; vertical-align: middle;">
                            <asp:TableRow>
                                <asp:TableCell>
                                    &nbsp;&nbsp; Search &nbsp;&nbsp;
                                    <asp:DropDownList ID="DropDownListPrice" runat="server">
                                        <asp:ListItem Value="Subject">Subject</asp:ListItem>
                                        <asp:ListItem Value="Description">Description</asp:ListItem>
                                        <asp:ListItem Value="Price">Price</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;for&nbsp;&nbsp;
                                    <asp:TextBox ID="txtPriceSearch" runat="server"></asp:TextBox>
                                    <asp:Button ID="Button1" runat="server" Text="Go" OnClick="Button_Price" />
                                </asp:TableCell></asp:TableRow></asp:Table><Custom:GridViewAlwaysShow ID="GridView4" runat="server" AllowSorting="True" OnSorting="SortRecordsPrice"
                            AutoGenerateColumns="False" ShowFooter="True" BackColor="White" BorderColor="#999999"
                            DataKeyNames="PriceID" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                            GridLines="Vertical" AlternatingRowStyles-CssClass="alt" CssClass="mGrid" PagerStyle-CssClass="pgr"
                            ForeColor="Black" OnRowEditing="RowEditPrice" OnRowUpdating="RowUpdatePrice"
                            OnRowCancelingEdit="RowEditCancelPrice" Width="100%" OnRowDeleting="deletePriceRow" OnRowDataBound="GridView4_RowDataBound">
                            <Columns>
                                <asp:CommandField ShowEditButton="True" ShowCancelButton="true" />
                                <asp:TemplateField HeaderText="PriceID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPriceID" runat="server" Text='<%# Bind("[PriceID]") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Price Subject" SortExpression="Subject">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSubject" runat="server" Text='<%# Bind("[Subject]") %>'></asp:TextBox></EditItemTemplate><ItemTemplate>
                                        <asp:Label ID="lblSubject" runat="server" Text='<%# Bind("[Subject]") %>'></asp:Label></ItemTemplate><FooterTemplate>
                                        <asp:TextBox ID="txtSubjectInsert" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                            ValidationGroup="insertPrice" ID="rfcInsertSubject" runat="server" ErrorMessage="Subject is a required field!"
                                            ControlToValidate="txtSubjectInsert" Text="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator></FooterTemplate><ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price Description" SortExpression="Description">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("[Description]") %>'></asp:TextBox></EditItemTemplate><ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("[Description]") %>'></asp:Label></ItemTemplate><FooterTemplate>
                                        <asp:TextBox ID="txtDescriptionInsert" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                            ValidationGroup="insertPrice" ID="rfcInsertDescription" runat="server" ErrorMessage="Description is a required field!"
                                            ControlToValidate="txtDescriptionInsert" Text="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator></FooterTemplate><ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price" SortExpression="Price">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPrice" runat="server" Text='<%# Bind("[Price]") %>'></asp:TextBox><asp:RegularExpressionValidator ID="revUpdatePrice" runat="server" ErrorMessage="Only Decimal Value Allowed"
                                            ValidationExpression="^\d*[0-9](|.\d*[0-9]|,\d*[0-9])?$" ControlToValidate="txtPrice"
                                            Text="*" ForeColor="Red">
                                        </asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="rfcUpdatePrice"
                                            runat="server" ErrorMessage="Price is a required field!" ControlToValidate="txtPrice"
                                            Text="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator></EditItemTemplate><ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("[Price]") %>'></asp:Label></ItemTemplate><FooterTemplate>
                                        <asp:TextBox ID="txtPriceInsert" runat="server" Width="60"></asp:TextBox><asp:RegularExpressionValidator
                                            ValidationGroup="insertPrice" ID="revInsertPrice" runat="server" ErrorMessage="Only Decimal Value Allowed"
                                            ValidationExpression="^\d*[0-9](|.\d*[0-9]|,\d*[0-9])?$" ControlToValidate="txtPriceInsert"
                                            Text="*" ForeColor="Red">
                                        </asp:RegularExpressionValidator><asp:RequiredFieldValidator ValidationGroup="insertPrice"
                                            ID="rfcInsertPrice" runat="server" ErrorMessage="Price is a required field!"
                                            ControlToValidate="txtPriceInsert" Text="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator></FooterTemplate><ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Uploaded" SortExpression="Date Uploaded">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("[Date Uploaded]", "{0:dd/MM/yyyy}") %>'
                                            Visible="false"></asp:TextBox></EditItemTemplate><ItemTemplate>
                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("[Date Uploaded]", "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate><FooterTemplate>
                                        <asp:LinkButton ID="lbInsert" runat="server" ValidationGroup="insertPrice" OnClick="lbInsert_Price">Insert</asp:LinkButton></FooterTemplate></asp:TemplateField><asp:TemplateField HeaderText="Uploaded By" SortExpression="Uploaded By">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox6" Visible="false" runat="server" Text='<%# Bind("[Uploaded By]") %>'></asp:TextBox></EditItemTemplate><ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("[Uploaded By]") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="DeletePriceButton" runat="server" CommandName="Delete" Text="Delete"
                                            OnClientClick="return confirm('Are you sure you want to delete this price?');"></asp:LinkButton></ItemTemplate></asp:TemplateField></Columns><AlternatingRowStyle BackColor="#e2e2e2" />
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pgr" BackColor="#999999" ForeColor="Black" HorizontalAlign="Center">
                            </PagerStyle>
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#808080" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#383838" />
                        </Custom:GridViewAlwaysShow>
                        <asp:ValidationSummary ID="ValidationSummary41" ValidationGroup="insertPrice" runat="server"
                            ForeColor="Red" />
                        <asp:ValidationSummary ID="ValidationSummary42" runat="server" ForeColor="Red" />
                    </asp:View>
                    <asp:View ID="View5" runat="server">
                        <asp:Table ID="Table5" runat="server" Style="width: 100%; border-width: 1px; border-color: #666;
                            border-style: solid; vertical-align: middle;">
                            <asp:TableRow>
                                <asp:TableCell>
                                    &nbsp;&nbsp; Search &nbsp;&nbsp;
                                    <asp:DropDownList ID="DropDownListAccountNumber" runat="server">
                                        <asp:ListItem Value="HospitalName">Hospital Name</asp:ListItem>
                                        <asp:ListItem Value="Address1">Address</asp:ListItem>
                                        <asp:ListItem Value="Postcode">Postcode</asp:ListItem>
                                        <asp:ListItem Value="AccountNumber">Account Number</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;for&nbsp;&nbsp;
                                    <asp:TextBox ID="txtAccountNumberSearch" runat="server"></asp:TextBox>
                                    <asp:Button ID="Button2" runat="server" Text="Go" OnClick="Button_AccountNumber" />
                                </asp:TableCell></asp:TableRow></asp:Table><asp:SqlDataSource ID="sdsHospitalName" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>"
                            SelectCommand="SELECT [HospitalID], [HospitalName], [AccountNumber] FROM [Hospitals]">
                        </asp:SqlDataSource>
                        <Custom:GridViewAlwaysShow ID="GridView5" runat="server" AllowSorting="True" OnSorting="SortRecordsAccountNumber"
                            AutoGenerateColumns="False" ShowFooter="True" BackColor="White" BorderColor="#999999"
                            DataKeyNames="HospitalID" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                            GridLines="Vertical" AlternatingRowStyles-CssClass="alt" CssClass="mGrid" PagerStyle-CssClass="pgr"
                            ForeColor="Black" Width="100%" OnRowDeleting="deleteHospitalRow" OnRowDataBound="GridView5_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="ContractHospitalID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContractHospitalID" runat="server" Text='<%# Bind("[ContractHospitalID]") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Hospital Name" SortExpression="Hospital Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHospitalName" runat="server" Text='<%# Bind("[Hospital Name]") %>'></asp:Label></ItemTemplate><FooterTemplate>
                                        <asp:DropDownList ID="ddlHospitalName" runat="server" AutoPostBack="True" DataTextField="HospitalName"
                                            DataValueField="HospitalID" DataSourceID="sdsHospitalName" OnSelectedIndexChanged="ddlHospitalName_Selected">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address" SortExpression="Address">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("[Address]") %>'></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Postcode" SortExpression="Postcode">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("[Postcode]") %>'></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Account Number" SortExpression="Account Number">
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("[Account Number]") %>'></asp:Label></ItemTemplate><FooterTemplate>
                                        <asp:DropDownList ID="ddlAccountNumber" runat="server" AutoPostBack="True" DataTextField="AccountNumber"
                                            DataValueField="HospitalID" DataSourceID="sdsHospitalName" OnSelectedIndexChanged="ddlAccountNumber_Selected">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Uploaded" SortExpression="ContractHospital Date Uploaded">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDateUploaded" runat="server" Text='<%# Bind("[ContractHospital Date Uploaded]", "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate><FooterTemplate>
                                        <asp:LinkButton ID="lbInsert" runat="server" ValidationGroup="insert" OnClick="lbInsert_AccountNumber">Insert</asp:LinkButton></FooterTemplate></asp:TemplateField><asp:TemplateField HeaderText="Uploaded By" SortExpression="ContractHospital Uploaded By">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUploadedBy" runat="server" Text='<%# Bind("[ContractHospital Uploaded By]") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="DeleteHospitalButton" runat="server" 
                                            CommandName="Delete" Text="Delete"  OnClientClick="return confirm('Are you sure you want to delete this entry?');"></asp:LinkButton></ItemTemplate></asp:TemplateField></Columns><AlternatingRowStyle BackColor="#e2e2e2" />
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pgr" BackColor="#999999" ForeColor="Black" HorizontalAlign="Center">
                            </PagerStyle>
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#808080" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#383838" />
                        </Custom:GridViewAlwaysShow>
                        <asp:ValidationSummary ID="ValidationSummary5" ValidationGroup="insertAccountNumber"
                            runat="server" ForeColor="Red" />
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">
        $(function () {
            $(".txtEmailDate").datepicker({ dateFormat: 'dd/mm/yy', minDate: '0' }); ;
        });
    </script>
</asp:Content>
