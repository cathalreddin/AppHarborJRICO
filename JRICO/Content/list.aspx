<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="JRICO.Content.list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <th>
                ContractReference
            </th>
            <th>
                Associated Ref 
            </th>
            <th>
                RecordType
            </th>
            <th>
                ContractSystemPriceList
            </th>        
            <th>
                ContractTitle
            </th>
            <th>
                ContactName
            </th>
            <th>
                ContactEmail
            </th>
            <th>
                ContactNo
            </th>
            <th>
                StartDate
            </th>
            <th>
                EndDate
            </th>
            <th>
                DateUploaded
            </th>
            <th>
                ContractStatus
            </th>
            <th></th>
        </tr>

        <tr>
            <td>
                @Html.DisplayFor(modelItem => item.ContractReference)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.LinkedContractReference)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.RecordType.Name)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.ContractSystemPriceList)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.ContractTitle)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.ContactName)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.ContactEmail)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.ContactNo)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.StartDate)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.EndDate)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.DateUploaded)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.ContractStatus.Name)
            </td>
            <td>
                @*@Html.ActionLink("Edit", "Edit", new { id=item.ContractID }) |*@
                @Html.ActionLink("Details", "One", new { id=item.ContractID }) @*|
                @Html.ActionLink("Delete", "Delete", new { id=item.ContractID })*@
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
