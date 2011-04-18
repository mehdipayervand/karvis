﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobDetail.aspx.cs" Inherits="SJ.Web.JobDetail" %>

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
                <td>
                    Title
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtTitle" />
                </td>
            </tr>
            <tr>
                <td>
                    Description
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" />
                </td>
            </tr>
            <tr>
                <td>
                    URL
                </td>
                <td>
                    <asp:HyperLink runat="server" ID="hlkURL" Text="Link" />
                </td>
            </tr>
            <tr>
                <td>
                    Tag
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtTag" />
                </td>
            </tr>
            <tr>
                <td>
                    Date Added:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtDateAdded" />
                </td>
            </tr>
        </table>
        <asp:HyperLink Text="Return to Job List" runat="server" NavigateUrl="~/JobList.aspx" />
    </div>
    </form>
</body>
</html>
