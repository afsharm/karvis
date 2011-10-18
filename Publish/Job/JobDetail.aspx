<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobDetail.aspx.cs" Inherits="Karvis.Web.JobDetail"
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
                <asp:Label runat="server" ID="lblTitle" />
            </td>
        </tr>
        <tr>
            <td>
                شرح:
            </td>
            <td>
                <asp:Label runat="server" ID="lblDescription" />
            </td>
        </tr>
        <tr>
            <td>
                لینک:
            </td>
            <td>
                <asp:HyperLink runat="server" Text="Link" ID="lnkUrl" />
            </td>
        </tr>
        <tr>
            <td>
                تگ:
            </td>
            <td>
                <asp:Label runat="server" ID="lblTag" Style='direction: ltr' />
            </td>
        </tr>
        <tr>
            <td>
                منبع:
            </td>
            <td>
                <asp:Label runat="server" ID="lblAdSource" />
            </td>
        </tr>
        <tr>
            <td>
                تاریخ ثبت:
            </td>
            <td>
                <asp:Label runat="server" ID="lblDateAddedPersian" />
            </td>
        </tr>
        <tr>
            <td>
                تعداد مشاهده:
            </td>
            <td>
                <asp:Label runat="server" ID="lblVisitCount" />
            </td>
        </tr>
        <tr>
            <td>
                مشاهده از طریق فید:
            </td>
            <td>
                <asp:Label runat="server" ID="lblFeedCount" />
            </td>
        </tr>
    </table>
    <div style='text-align: left'>
        <asp:HyperLink Text="بازگشت به فهرست مشاغل" runat="server" NavigateUrl="~/Job/JobList.aspx" />
    </div>
</asp:Content>
