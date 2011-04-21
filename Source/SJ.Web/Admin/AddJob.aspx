<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddJob.aspx.cs" Inherits="SJ.Web.Admin.AddJob"
    MasterPageFile="~/MasterPages/MainMaster.Master" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    ثبت کار جدید
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainHolder">
    <asp:Label Text="" runat="server" ID="lblMessage" />
    <asp:FormView runat="server" ID="frmJob" DataSourceID="odsJob" DataKeyNames="ID">
        <EditItemTemplate>
            <table>
                <tr>
                    <td>
                        عنوان
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTitle" Text='<%# Bind("Title") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        شرح
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Text='<%# Bind("Description") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        لینک
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtURL" Text='<%# Bind("URL") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        تگ
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTag" Text='<%# Bind("Tag") %>' />
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
                    <td>
                        <%# Eval("Tag") %>
                    </td>
                </tr>
            </table>
            <asp:LinkButton ID="LinkButton3" Text="جدید" runat="server" CommandName="New" />
            <asp:LinkButton ID="LinkButton4" Text="ویرایش" runat="server" CommandName="Edit" />
        </ItemTemplate>
        <InsertItemTemplate>
            <table>
                <tr>
                    <td>
                        عنوان
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTitle" Text='<%# Bind("Title") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        شرح
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Text='<%# Bind("Description") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        لینک
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtURL" Text='<%# Bind("URL") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        تگ
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTag" Text='<%# Bind("Tag") %>' />
                    </td>
                </tr>
            </table>
            <asp:LinkButton ID="LinkButton1" Text="ثبت" runat="server" CommandName="Insert" />
            <asp:LinkButton ID="LinkButton2" Text="انصراف" runat="server" CommandName="Cancel" />
        </InsertItemTemplate>
    </asp:FormView>
    <asp:HiddenField runat="server" ID="hdnID" />
    <asp:ObjectDataSource runat="server" ID="odsJob" TypeName="SJ.Core.JobDao" InsertMethod="AddNewJob"
        SelectMethod="GetJob" OnInserted="odsJob_Inserted">
        <SelectParameters>
            <asp:ControlParameter Name="jobID" ControlID="hdnID" PropertyName="Value" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
