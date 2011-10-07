<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Karvis.Web.Stats.Dashboard"
    MasterPageFile="~/MasterPages/MainMaster.Master" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    آمار
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainHolder">
    <br />
    <br />
    <br />
    <asp:Image ImageUrl="~/Images/stat.gif" runat="server" />
    <br />
    یکی از آمارهای زیر را انتخاب نمایید:
    <br />
    <br />
    <ul>
        <li>
            <asp:HyperLink Text="منابع آگهی" NavigateUrl="~/Stats/AdSource.aspx" runat="server" />
        </li>
    </ul>
    <br />
    <br />
</asp:Content>
