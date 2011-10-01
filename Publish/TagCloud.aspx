<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TagCloud.aspx.cs" Inherits="Karvis.Web.TagCloud"
    MasterPageFile="~/MasterPages/MainMaster.Master" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    تگ‌های استفاده شده
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainHolder">
    <div style="text-align: center;">
        <div id="divMain" runat="server" style="direction: ltr; text-align: center;" />
    </div>
</asp:Content>
