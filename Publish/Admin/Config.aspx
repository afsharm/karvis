<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Config.aspx.cs" Inherits="Karvis.Web.Admin.Config"
    MasterPageFile="~/MasterPages/MainMaster.Master" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    تنظیمات
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainHolder">
    <asp:Label Text="" runat="server" ID="lblMessage" />
    <div runat="server" id="divLogin">
        Enter Password:<br />
        <asp:TextBox runat="server" ID="txtPassword" />
        <br />
        <asp:Button runat="server" ID="btnLogin" OnClick="btnLogin_Click" Text="Login" />
    </div>
    <div runat="server" id="divControlPanel" visible="false">
        Welcome to Karvis control panel!
        <br />
        <asp:TextBox runat="server" ID="txtDBPassword" />
        <asp:Button Text="Create Database" runat="server" ID="btnCreateDB" OnClick="btnCreateDB_Click" />
    </div>
</asp:Content>
