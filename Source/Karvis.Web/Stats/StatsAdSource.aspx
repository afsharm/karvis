<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatsAdSource.aspx.cs"
    Inherits="Karvis.Web.Stats.StatsAdSource" MasterPageFile="~/MasterPages/MainMaster.Master" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    منابع آگهی
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainHolder">
    <br />
    <br />
    منابع آگهی:
    <br />
    <br />
    تعداد کل کارهای ثبت شده:
    <asp:Label Text="" runat="server" ID="jobsCount" />
    <br />
    <br />
    <br />
    <br />
    تعداد آگهی‌های ثبت شده به ازای هر منبع آگهی:
    <br />
    <asp:Repeater runat="server" ID="rptJobAdSource">
        <SeparatorTemplate>
            <br />
        </SeparatorTemplate>
        <ItemTemplate>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        منبع آگهی:
                    </td>
                    <td>
                        <%# Eval("SiteSourceDescription")%>
                    </td>
                </tr>
                <tr>
                    <td>
                        تعداد:
                    </td>
                    <td>
                        <%# Eval("Count")%>
                    </td>
                </tr>
                <tr>
                    <td>
                        درصد:
                    </td>
                    <td>
                        <%# Eval("Percent")%>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
