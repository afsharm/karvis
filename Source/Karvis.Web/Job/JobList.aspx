<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobList.aspx.cs" Inherits="Karvis.Web.JobList"
    MasterPageFile="~/MasterPages/MainMaster.Master" Title="کارویس - فهرست مشاغل" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    فهرست مشاغل
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainHolder">
    <div>
        <table border="0" cellpadding="0" cellspacing="0">
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
                    تگ:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtTag" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button Text="جستجو" runat="server" ID="btnSearch" OnClick="btnSearch_Click" />
                    <asp:Button Text="از نو" runat="server" ID="btnReset" OnClick="btnReset_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView runat="server" ID="grdJobList" DataSourceID="odsJobList" AutoGenerateColumns="false"
            AllowPaging="true" AllowSorting="true">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                <asp:BoundField DataField="Title" HeaderText="عنوان" SortExpression="Title" />
                <asp:BoundField DataField="Tag" HeaderText="تگ" SortExpression="Tag" />
                <asp:BoundField DataField="DateAddedPersian" HeaderText="تاریخ ثبت" SortExpression="DateAdded" />
                <asp:BoundField DataField="VisitCount" HeaderText="تعداد مشاهده" SortExpression="VisitCount" />
                <asp:TemplateField HeaderText="اطلاعات بیشتر">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink2" NavigateUrl='<%# MyGetJobUrl( Eval("ID"), Eval("Title")) %>'
                            Text="جزییات" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource runat="server" ID="odsJobList" TypeName="Karvis.Core.JobModel"
            SelectMethod="FindAll" EnablePaging="true" SelectCountMethod="FindAllCount" SortParameterName="sortOrder">
            <SelectParameters>
                <asp:ControlParameter PropertyName="Text" ControlID="txtTitle" Name="title" />
                <asp:ControlParameter PropertyName="Text" ControlID="txtTag" Name="tag" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>
