﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RunCrawler.aspx.cs" Inherits="SJ.Web.Admin.RunCrawler"
    MasterPageFile="~/MasterPages/MainMaster.Master" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    Crawler
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainHolder">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                url:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtUrl" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button Text="Run Crawler" runat="server" ID="bntRunCrawler" OnClick="btnRunCrawler_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView runat="server" ID="grdEmails" AutoGenerateColumns="true" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
