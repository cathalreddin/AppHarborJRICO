<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="addContract.aspx.cs" Inherits="JRICO.Content.addContract" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.10.2.custom.js" type="text/javascript"></script>
    <link href="../Styles/jquery-ui-1.10.2.custom.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="Table1" runat="server" Width="90%" Height="100%" HorizontalAlign="Center" BorderStyle="Solid" GridLines="Horizontal">
        <asp:TableRow>
            <asp:TableCell Text="Contract Reference"></asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="txtContractReference" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Text="Record Type"></asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList ID="ddlRecordType" runat="server" DataTextField="Name" DataValueField="RecordTypeID"
                    DataSourceID="sdsRecordTypes">
                </asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell Text="System Price List"></asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="txtSystemPriceList" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Text="Contract Title"></asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="txtContractTitle" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Text="Contract Status"></asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList ID="ddlContractStatus" runat="server" DataTextField="Name" DataValueField="ContractStatusID"
                    DataSourceID="sdsContractStatus">
                </asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell Text="Associated Reference"></asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList ID="ddlAssociatedReference" runat="server" DataTextField="Name"
                    DataValueField="AssociatedContractReferenceID" DataSourceID="sdsContracts" >
                </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Text="Name"></asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell Text="Email"></asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell Text="Phone"></asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Text="Start Date"></asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="datepickersd"></asp:TextBox></asp:TableCell>
            <asp:TableCell Text="End Date"></asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="txtEndDate" runat="server" CssClass="datepickered"></asp:TextBox></asp:TableCell>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell HorizontalAlign="Right">
                <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Cancel" />
                <asp:Button ID="Button1" runat="server" Text="Add" OnClick="AddContract" /></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:SqlDataSource ID="SdsRecordTypes" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>" 
        SelectCommand="SELECT [RecordTypeID], [Name] FROM [RecordTypes] ORDER BY [Name]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SdsContractStatus" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>" 
        SelectCommand="SELECT [ContractStatusID], [Name] FROM [ContractStatus] ORDER BY [Name]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SdsContracts" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>" 
        SelectCommand="SELECT [AssociatedContractReferenceID], [Name] FROM [AssociatedContractReference] ORDER BY [Name]">
    </asp:SqlDataSource>
    <script language="javascript" type="text/javascript">
           $(function () {
               $(".datepickered").datepicker({ dateFormat: 'dd/mm/yy' }); ;
           });

           $(function () {
               $(".datepickersd").datepicker({ dateFormat: 'dd/mm/yy' }); ;
           });
    </script>
</asp:Content>
