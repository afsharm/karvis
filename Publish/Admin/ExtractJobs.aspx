<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExtractJobs.aspx.cs" Inherits="Karvis.Web.Admin.ExtractJobs"
    MasterPageFile="~/MasterPages/MainMaster.Master" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    Extract Jobs
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainHolder">
    <asp:Label runat="server" ID="lblMessage" />
    <br />
    <asp:DropDownList runat="server" ID="ddlSiteSource">
        <asp:ListItem Value="rahnama_com" Text="نیازمندی‌های ‌همشهری" Selected="True" />
        <asp:ListItem Value="agahi_ir" Text="Agahi.ir" />
        <asp:ListItem Value="irantalent_com" Text="IranTalent.com" />
        <asp:ListItem Value="developercenter_ir" Text="DeveloperCenter.ir" />
        <asp:ListItem Value="itjobs_ir" Text="ITJobs.ir" />
        <asp:ListItem Value="istgah_com" Text="Istgah.com" />
        <asp:ListItem Value="nofaـir" Text="Nofa.ir" />
        <asp:ListItem Value="unp_ir" Text="UNP.ir" />
    </asp:DropDownList>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" style="direction: ltr; text-align: left">
        <tr>
            <td>
                <asp:Button Text="اعمال" runat="server" ID="btnApplyJobs" OnClick="btnApplyJobs_Click" />
            </td>
            <td>
                <asp:Button Text="ثبت موقت" runat="server" ID="btnTempSave" OnClick="btnTempSave_Click" />
            </td>
            <td>
                <asp:Button Text="استخراج" runat="server" ID="btnExtractJobs" OnClick="btnExtractJobs_Click" />
            </td>
        </tr>
    </table>
    <asp:Repeater runat="server" ID="rptPreJob" DataMember="Karvis.Job.Core">
        <SeparatorTemplate>
            <br />
            <br />
        </SeparatorTemplate>
        <ItemTemplate>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        (<%# Container.ItemIndex + 1 %>) - اعمال شود؟
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="chkApply" Checked="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        آی دی:
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblId" Text='<%# Eval("Id") %>' Font-Names="Tahoma" />
                    </td>
                </tr>
                <tr>
                    <td>
                        عنوان:
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTitle" Text='<%# Eval("Title") %>' Width="600px"
                            Font-Names="Tahoma" />
                    </td>
                </tr>
                <tr>
                    <td>
                        شرح:
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtDescription" Text='<%# Eval("Description") %>'
                            Width="600px" TextMode="MultiLine" Rows="6" Font-Names="Tahoma" />
                    </td>
                </tr>
                <tr>
                    <td>
                        تگ:
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTag" Text='<%# Eval("Tag") %>' Width="600px" Font-Names="Tahoma"
                            Style='direction: ltr; text-align: left' />
                    </td>
                </tr>
                <tr>
                    <td>
                        لینک:
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtUrl" Text='<%# Eval("Url") %>' Width="600px" Style='direction: ltr;
                            text-align: left' Font-Names="Tahoma" />
                        <asp:HyperLink NavigateUrl='<%# Eval("Url") %>' runat="server" Text="go" />
                    </td>
                </tr>
                <tr>
                    <td>
                        ایمیل:
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtEmails" Text='<%# Eval("Emails") %>' Width="600px"
                            Style='direction: ltr; text-align: left' Font-Names="Tahoma" />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
