<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Config.aspx.cs" Inherits="SJ.Web.Admin.Config" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    SJ<br />
    <asp:Label Text="" runat="server" ID="lblMessage" />
    <div runat="server" id="divLogin">
        Enter Password:<br />
        <asp:TextBox runat="server" ID="txtPassword" />
        <br />
        <asp:Button runat="server" ID="btnLogin" OnClick="btnLogin_Click" Text="Login" />
    </div>
    <div runat="server" id="divControlPanel" visible="false">
        Welcome to SJ control panel!
        <br />
        <asp:TextBox runat="server" ID="txtDBPassword" />
        <asp:Button Text="Create Database" runat="server" ID="btnCreateDB" OnClick="btnCreateDB_Click" />
    </div>
    </form>
</body>
</html>
