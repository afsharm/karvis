<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubmitJob.aspx.cs" Inherits="Karvis.Web.SubmitJob"
    MasterPageFile="~/MasterPages/MainMaster.Master" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    ثبت کار جدید
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainHolder">
    <asp:Label Text="" runat="server" ID="lblMessage" />
    <table>
        <tr>
            <td>
                Id:
            </td>
            <td>
                <asp:Label runat="server" ID="lblId" />
            </td>
        </tr>
        <tr>
            <td>
                عنوان:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtTitle" Width="700" />
            </td>
        </tr>
        <tr>
            <td>
                شرح:
            </td>
            <td>
                <CKEditor:CKEditorControl runat="server" ID="ckDescription" Width="700" Height="300"
                    Toolbar="Basic" DefaultLanguage="Fa" ContentsLangDirection="Rtl" ContentsLanguage="Fa"
                    Language="Fa" />
            </td>
        </tr>
        <tr>
            <td>
                لینک:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtURL" CssClass="ltr" Width="700" />
            </td>
        </tr>
        <tr>
            <td>
                تگ:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtTag" CssClass="ltr" Width="700" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label Text="منبع:" runat="server" ID="lblAdSource" />
            </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlAdSource">
                    <asp:ListItem Value="rahnama_com" Text="نیازمندی‌های ‌همشهری" />
                    <asp:ListItem Value="irantalent_com" Text="IranTalent.com" />
                    <asp:ListItem Value="Email" Text="ایمیل" />
                    <asp:ListItem Value="developercenter_ir" Text="DeveloperCenter.ir" />
                    <asp:ListItem Value="karvis_ir" Text="سایت کارویس" />
                    <asp:ListItem Value="itjobs_ir" Text="ITJobs.ir" />
                    <asp:ListItem Value="agahi_ir" Text="Agahi.ir" />
                    <asp:ListItem Value="istgah_com" Text="Istgah.com" />
                    <asp:ListItem Value="nofaـir" Text="Nofa.ir" />
                    <asp:ListItem Value="unp_ir" Text="UNP.ir" />
                    <asp:ListItem Value="banki_ir" Text="Banki.ir" />
                    <asp:ListItem Value="Misc" Text="متفرقه" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label Text="فعال:" runat="server" ID="lblActive" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkIsActive" CssClass="ltr" />
            </td>
        </tr>
    </table>
    <asp:Button ID="btnSaveUpdate" Text="ثبت" runat="server" OnClick="btnSaveUpdate_Click" />
    <asp:Button ID="btnCancel" Text="انصراف" runat="server" OnClick="btnCancel_Click" />
    <asp:Button ID="btnNew" Text="جدید" runat="server" OnClick="btnNew_Click" />
    <asp:Button ID="btnEdit" Text="ویرایش" runat="server" OnClick="btnEdit_Click" />
    <asp:HiddenField ID="hdnJobId" runat="server" />
    <hr />
    تگ باید به انگلیسی باشد
</asp:Content>
