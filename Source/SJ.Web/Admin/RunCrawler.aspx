<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RunCrawler.aspx.cs" Inherits="SJ.Web.Admin.RunCrawler"
    MasterPageFile="~/MasterPages/MainMaster.Master" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    Crawler
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainHolder">
    <asp:Button Text="Run Crawler" runat="server" ID="bntRunCrawler" OnClick="btnRunCrawler_Click" />
</asp:Content>
