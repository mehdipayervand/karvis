﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Monitor.aspx.cs" Inherits="SJ.Web.Admin.Monitor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnRefresh" runat="server" Click="btnRefresh_Click" Text="Refresh" />
        <asp:Label Text="" runat="server" ID="lblFeedInfo" />
    </div>
    </form>
</body>
</html>
