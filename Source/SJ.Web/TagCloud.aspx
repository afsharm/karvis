<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TagCloud.aspx.cs" Inherits="SJ.Web.TagCloud"
    MasterPageFile="~/MasterPages/MainMaster.Master" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    تگ‌های استفاده شده
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainHolder">
    <div style="text-align: center; width: 400px">
        <div id="divMain" runat="server" style="direction: rtl; text-align: right; width: 200px" />
    </div>
</asp:Content>
