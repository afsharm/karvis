<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobDetail.aspx.cs" Inherits="SJ.Web.JobDetail"
    MasterPageFile="~/MasterPages/MainMaster.Master" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    جزییات دقیق‌تر
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainHolder">
    <table>
        <tr>
            <td>
                عنوان:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtTitle" />
            </td>
        </tr>
        <tr>
            <td>
                شرح:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
            <td>
                لینک:
            </td>
            <td>
                <asp:HyperLink runat="server" ID="hlkURL" Text="Link" />
            </td>
        </tr>
        <tr>
            <td>
                تگ:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtTag" />
            </td>
        </tr>
        <tr>
            <td>
                تاریخ ثبت:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtDateAdded" />
            </td>
        </tr>
    </table>
    <asp:HyperLink Text="بازگشت به فهرست مشاغل" runat="server" NavigateUrl="~/Job/JobList.aspx" />
</asp:Content>
