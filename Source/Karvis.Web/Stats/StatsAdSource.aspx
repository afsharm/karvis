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
    <asp:Chart ID="chartAdSource" runat="server" Width="412px" Height="296px" Palette="BrightPastel"
        BorderColor="181, 64, 1" BorderDashStyle="Solid" BackGradientStyle="TopBottom"
        BorderWidth="2" BackColor="#F3DFC1" ImageType="Png" ImageLocation="~\App_Data\TempImages\ChartPic_#SEQ(300,3)">
        <Titles>
            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3"
                Text="آگهی‌های ثبت شده به ازای هر منبع" Alignment="TopLeft" ForeColor="26, 59, 105">
            </asp:Title>
        </Titles>
        <Legends>
            <asp:Legend Enabled="False" IsTextAutoFit="False" Name="Default" BackColor="Transparent"
                Font="Trebuchet MS, 8.25pt, style=Bold">
            </asp:Legend>
        </Legends>
        <BorderSkin SkinStyle="Emboss"></BorderSkin>
        <Series>
            <asp:Series ChartArea="ChartArea1" Name="Series 1" BorderColor="180, 26, 59, 105"
                Color="224, 64, 10">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
                    WallWidth="0" IsClustered="False"></Area3DStyle>
                <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                    <MajorGrid LineColor="64, 64, 64, 64" />
                </AxisY>
                <AxisX LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" IsStaggered="True" />
                    <MajorGrid LineColor="64, 64, 64, 64" />
                </AxisX>
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
    <br />
    <br />
    تعداد کل کارهای ثبت شده:
    <asp:Label Text="" runat="server" ID="jobsCount" />
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
