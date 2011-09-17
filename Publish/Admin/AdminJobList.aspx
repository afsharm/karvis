<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminJobList.aspx.cs" Inherits="Karvis.Web.Admin.AdminJobList"
    MasterPageFile="~/MasterPages/MainMaster.Master" Title="کارویس - فهرست مشاغل - مدیریتی" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    فهرست مشاغل/جستجو مدیریتی
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
                <td>
                    تگ:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtTag" />
                </td>
            </tr>
            <tr>
                <td>
                    منبع:
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlAdSource">
                        <asp:ListItem Value="All" Text="مهم نیست" Selected="True" />
                        <asp:ListItem Value="DeveloperCenter" Text="Developer Center" />
                        <asp:ListItem Value="Email" Text="ایمیل" />
                        <asp:ListItem Value="Hamshahri" Text="نیازمندی‌های همشهری" />
                        <asp:ListItem Value="IranTalent" Text="Iran Talent" />
                        <asp:ListItem Value="Misc" Text="متفرقه" />
                    </asp:DropDownList>
                </td>
                <td>
                </td>
                <td>
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
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <asp:GridView runat="server" ID="grdJobList" DataSourceID="odsJobList" AutoGenerateColumns="false"
            AllowPaging="true" AllowSorting="true" DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:BoundField DataField="Title" HeaderText="عنوان" SortExpression="Title" />
                <asp:BoundField DataField="Tag" HeaderText="تگ" SortExpression="Tag" />
                <asp:BoundField DataField="DateAddedPersian" HeaderText="تاریخ ثبت" SortExpression="DateAdded" />
                <asp:BoundField DataField="VisitCount" HeaderText="تعداد مشاهده" SortExpression="VisitCount" />
                <asp:BoundField DataField="AdSource" HeaderText="منبع" SortExpression="AdSource" />
                <asp:TemplateField HeaderText="اطلاعات بیشتر">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink2" NavigateUrl='<%# MyGetJobUrl( Eval("Id"), Eval("Title")) %>'
                            Text="جزییات" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowDeleteButton="true" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource runat="server" ID="odsJobList" TypeName="Karvis.Core.JobModel"
            SelectMethod="FindAllNoneActive" EnablePaging="true" SelectCountMethod="FindAllCountNoneActive"
            SortParameterName="sortOrder" DeleteMethod="DeleteJob">
            <SelectParameters>
                <asp:ControlParameter PropertyName="Text" ControlID="txtTitle" Name="title" />
                <asp:ControlParameter PropertyName="Text" ControlID="txtTag" Name="tag" />
                <asp:ControlParameter PropertyName="SelectedValue" ControlID="ddlAdSource" Name="adSource" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>
