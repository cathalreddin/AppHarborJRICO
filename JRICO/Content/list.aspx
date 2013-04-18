<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="JRICO.Content.list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        * <asp:TextBox ID="TextBox1" runat="server">        
        </asp:TextBox> *
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
   <%-- <table>
        <tr>
            <th> 
                Contract Reference
            </th>
            <th>
                Associated Ref 
            </th>
            <th>
                Record Type
            </th>
            <th>
                System Price List
            </th>        
            <th>
                Contract Title
            </th>
            <th>
                Name
            </th>
            <th>
                Email
            </th>
            <th>
                Phone
            </th>
            <th>
                Start Date
            </th>
            <th>
                End Date
            </th>
            <th>
                Submission Date
            </th>
            <th>
                Contract Status
            </th>
            <th></th>
        </tr>

        <tr>
            <td>
                ContractReference
            </td>
            <td>
                LinkedContractReference
            </td>
            <td>
                RecordType.Name
            </td>
            <td>
                ContractSystemPriceList
            </td>
            <td>
                ContractTitle
            </td>
            <td>
                ContactName
            </td>
            <td>
                ContactEmail
            </td>
            <td>
                ContactNo
            </td>
            <td>
                StartDate
            </td>
            <td>
                EndDate
            </td>
            <td>
                DateUploaded
            </td>
            <td>
                ContractStatus.Name
            </td>
            <td>
                @*@Html.ActionLink("Edit", "Edit", new { id=item.ContractID }) |*@
                @Html.ActionLink("Details", "One", new { id=item.ContractID }) @*|
                @Html.ActionLink("Delete", "Delete", new { id=item.ContractID })*@
            </td>
        </tr>
    </table>--%>
    </div>
    </form>
</body>
</html>
