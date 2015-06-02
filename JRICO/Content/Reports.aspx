<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="JRICO.Content.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="Button1" runat="server" Text="Report 1 - click here to download the following fields to excel : Hospital/Contract Reference/Contract Title/Record Type/ Start date/ End Date/Contract Status" OnClick="Button1_Click" />
</asp:Content>
