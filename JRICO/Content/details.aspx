<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="details.aspx.cs" Inherits="JRICO.Content.details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
        .Initial
        {
            display: block;
            padding: 4px 18px 4px 18px;
            float: left;
            background: url("../Images/InitialImage.png") no-repeat right top;
            color: Black;
            font-weight: bold;
        }
        .Initial:hover
        {
            color: White;
            background: url("../Images/SelectedButton.png") no-repeat right top;
        }
        .Clicked
        {
            float: left;
            display: block;
            background: url("../Images/SelectedButton.png") no-repeat right top;
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
        <a href="contractList1.aspx">Contract List</a> &nbsp;&nbsp;|&nbsp;&nbsp; Search &nbsp;&nbsp;
    </td>
    <td align="right">
        <a href="hospitalList.aspx">Hospital Admin</a>
    </td>
</tr>
</table>
<table width="100%" align="center">
        <tr>
            <td>
                <br />
                <br />
                <br />
                <br />
            </td>
        </tr>
    <tr>
        <td>
            <asp:Button Text="Tab 1" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server"
                OnClick="Tab1_Click" />
            <asp:Button Text="Tab 2" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server"
                OnClick="Tab2_Click" />
            <asp:Button Text="Tab 3" BorderStyle="None" ID="Tab3" CssClass="Initial" runat="server"
                OnClick="Tab3_Click" />
            <asp:Button Text="Tab 4" BorderStyle="None" ID="Tab4" CssClass="Initial" runat="server"
                OnClick="Tab4_Click" />
            <asp:Button Text="Tab 5" BorderStyle="None" ID="Tab5" CssClass="Initial" runat="server"
                OnClick="Tab5_Click" />
            <asp:MultiView ID="MainView" runat="server">
                <asp:View ID="View1" runat="server">
                    <asp:Table ID="Table1" runat="server" style="width: 90%; border-width: 1px; border-color: #666; border-style: solid; vertical-align: middle;">
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
                    <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                        <tr>
                            <td>
                                <br />
                                <br />
                                <h3>
                                    View 2
                                </h3>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View3" runat="server">
                        <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                            <tr>
                                <td>
                                    <br />
                                    <br />
                                    <h3>
                                        View 3
                                    </h3>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View4" runat="server">
                        <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                            <tr>
                                <td>
                                    <br />
                                    <br />
                                    <h3>
                                        View 4
                                    </h3>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View5" runat="server">
                        <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                            <tr>
                                <td>
                                    <br />
                                    <br />
                                    <h3>
                                        View 5
                                    </h3>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
    </table>
</asp:Content>
