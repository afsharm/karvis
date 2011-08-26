<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RunCrawler.aspx.cs" Inherits="Karvis.Web.Admin.RunCrawler"
    MasterPageFile="~/MasterPages/MainMaster.Master" ValidateRequest="false" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    Crawler
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainHolder">
    <table border="0" cellpadding="0" cellspacing="0" style="direction: ltr; text-align: left">
        <tr>
            <td>
            </td>
            <td>
                <asp:Button Text="Extract jobs" runat="server" ID="btnExtractJobs" OnClick="btnExtractJobs_Click" />
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
