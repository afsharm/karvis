<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditJob.aspx.cs" Inherits="Karvis.Web.Admin.AddJob"
    MasterPageFile="~/MasterPages/MainMaster.Master" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    ثبت کار جدید
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainHolder">
    <asp:Label Text="" runat="server" ID="lblMessage" />
    <asp:FormView runat="server" ID="frmJob" DataSourceID="odsJob" DataKeyNames="ID">
        <InsertItemTemplate>
            <table>
                <tr>
                    <td>
                        عنوان
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTitle" Text='<%# Bind("Title") %>' Width="300" />
                    </td>
                </tr>
                <tr>
                    <td>
                        شرح
                    </td>
                    <td>
                        <CKEditor:CKEditorControl runat="server" ID="ckDescription" Text='<%# Bind("Description") %>'
                            Width="300" Height="100" Toolbar="Basic" DefaultLanguage="Fa" ContentsLangDirection="Rtl"
                            ContentsLanguage="Fa" Language="Fa" />
                    </td>
                </tr>
                <tr>
                    <td>
                        لینک
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtURL" Text='<%# Bind("URL") %>' CssClass="ltr"
                            Width="300" />
                    </td>
                </tr>
                <tr>
                    <td>
                        تگ
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTag" Text='<%# Bind("Tag") %>' CssClass="ltr"
                            Width="300" />
                    </td>
                </tr>
            </table>
            <asp:LinkButton ID="LinkButton1" Text="ثبت" runat="server" CommandName="Insert" />
            <asp:LinkButton ID="LinkButton2" Text="انصراف" runat="server" CommandName="Cancel" />
        </InsertItemTemplate>
        <EditItemTemplate>
            <table>
                <tr>
                    <td>
                        عنوان
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTitle" Text='<%# Bind("Title") %>' Width="300" />
                    </td>
                </tr>
                <tr>
                    <td>
                        شرح
                    </td>
                    <td>
                        <CKEditor:CKEditorControl runat="server" ID="ckDescription" Text='<%# Bind("Description") %>'
                            Width="300" Height="100" Toolbar="Basic" DefaultLanguage="Fa" ContentsLangDirection="Rtl"
                            ContentsLanguage="Fa" Language="Fa" />
                    </td>
                </tr>
                <tr>
                    <td>
                        لینک
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtURL" Text='<%# Bind("URL") %>' CssClass="ltr"
                            Width="300" />
                    </td>
                </tr>
                <tr>
                    <td>
                        تگ
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTag" Text='<%# Bind("Tag") %>' CssClass="ltr"
                            Width="300" />
                    </td>
                </tr>
            </table>
            <asp:LinkButton ID="LinkButton1" Text="ثبت" runat="server" CommandName="Update" />
            <asp:LinkButton ID="LinkButton2" Text="انصراف" runat="server" CommandName="Cancel" />
        </EditItemTemplate>
        <ItemTemplate>
            <h1>
                <%# Eval("ID") %></h1>
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
                        <asp:HyperLink NavigateUrl='<%# Eval("URL") %>' runat="server" Text="لینک" />
                    </td>
                </tr>
                <tr>
                    <td>
                        تگ
                    </td>
                    <td style="direction: ltr">
                        <%# Eval("Tag") %>
                    </td>
                </tr>
            </table>
            <asp:LinkButton ID="LinkButton3" Text="جدید" runat="server" CommandName="New" />
            <asp:LinkButton ID="LinkButton4" Text="ویرایش" runat="server" CommandName="Edit" />
        </ItemTemplate>
    </asp:FormView>
    <div style='text-align: left'>
        <asp:HyperLink ID="HyperLink1" Text="بازگشت به فهرست مشاغل" runat="server" NavigateUrl="~/Job/JobList.aspx" />
    </div>
    <asp:HiddenField runat="server" ID="hdnID" />
    <asp:ObjectDataSource runat="server" ID="odsJob" TypeName="Karvis.Core.JobModel" InsertMethod="AddNewJob"
        SelectMethod="GetJob" OnInserted="odsJob_Inserted" UpdateMethod="UpdateJob">
        <SelectParameters>
            <asp:ControlParameter Name="jobID" ControlID="hdnID" PropertyName="Value" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
