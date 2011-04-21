<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobDetail.aspx.cs" Inherits="SJ.Web.JobDetail"
    MasterPageFile="~/MasterPages/MainMaster.Master" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    جزییات دقیق‌تر
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainHolder">
    <asp:FormView runat="server" ID="frmJob" DataSourceID="odsJob" EnableViewState="false">
        <ItemTemplate>
            <table>
                <tr>
                    <td>
                        عنوان:
                    </td>
                    <td>
                        <%# Eval("Title") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        شرح:
                    </td>
                    <td>
                        <%# Eval("Description") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        لینک:
                    </td>
                    <td>
                        <asp:HyperLink runat="server" Text="Link" NavigateUrl='<%# Eval("URL") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        تگ:
                    </td>
                    <td>
                        <%# Eval("Tag") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        تاریخ ثبت:
                    </td>
                    <td>
                        <%# Eval("DateAdded") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        تعداد مشاهده:
                    </td>
                    <td>
                        <%# Eval("VisitCount") %>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:FormView>
    <div style='text-align: left'>
        <asp:HyperLink Text="بازگشت به فهرست مشاغل" runat="server" NavigateUrl="~/Job/JobList.aspx" />
    </div>
    <asp:ObjectDataSource runat="server" ID="odsJob" TypeName="SJ.Core.JobDao" SelectMethod="GetJob">
        <SelectParameters>
            <asp:QueryStringParameter Name="jobId" QueryStringField="ID" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
