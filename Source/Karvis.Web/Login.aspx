<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Karvis.Web.Login"
    MasterPageFile="~/MasterPages/MainMaster.Master" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    ورود به سیستم
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainHolder">
    <hr />
    <div style='direction: ltr; text-align: left;'>
        <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/Job/JobList.aspx" VisibleWhenLoggedIn="false"
            OnLoggedIn="Login1_LoggedIn">
        </asp:Login>
    </div>
</asp:Content>
