<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobList.aspx.cs" Inherits="Karvis.Web.JobList"
    MasterPageFile="~/MasterPages/MainMaster.Master" Title="کارویس - فهرست مشاغل" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    فهرست مشاغل/جستجو
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainHolder">
    <div>
        <asp:Label Text="" runat="server" ID="lblMessage" />
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
                        <asp:ListItem Value="All" Text="همه" Selected="True" />
                        <asp:ListItem Value="Hamshahri" Text="همشهری" />
                        <asp:ListItem Value="DeveloperCenter" Text="DeveloperCenter" />
                        <asp:ListItem Value="IranTalent" Text="IranTalent" />
                        <asp:ListItem Value="Karvis" Text="کارویس" />
                        <asp:ListItem Value="Email" Text="ایمیل" />
                        <asp:ListItem Value="Misc" Text="متفرقه" />
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button Text="جستجو" runat="server" ID="btnSearch" OnClick="btnSearch_Click" />
                    <asp:Button Text="از نو" runat="server" ID="btnReset" OnClick="btnReset_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    وضعیت فعال:
                </td>
                <td>
                    <asp:RadioButtonList runat="server" ID="rblIsActive">
                        <asp:ListItem Text="مهم نیست" Value="All" Selected="True" />
                        <asp:ListItem Text="فعال" Value="Active" />
                        <asp:ListItem Text="غیر فعال" Value="NotActive" />
                    </asp:RadioButtonList>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:HiddenField runat="server" ID="hdnSortExpression" />
        <asp:DataGrid runat="server" ID="dgJobList" AutoGenerateColumns="false" AllowPaging="true"
            AllowSorting="true" AllowCustomPaging="true" OnPageIndexChanged="dgJobList_PageIndexChanged"
            OnSortCommand="dgJobList_SortCommand" DataKeyField="Id" OnDeleteCommand="dgJobList_DeleteCommand">
            <Columns>
                <asp:BoundColumn DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:BoundColumn DataField="Title" HeaderText="عنوان" SortExpression="Title" />
                <asp:BoundColumn DataField="Tag" HeaderText="تگ" SortExpression="Tag" />
                <asp:BoundColumn DataField="DateAddedPersian" HeaderText="تاریخ ثبت" SortExpression="DateAdded" />
                <asp:BoundColumn DataField="VisitCount" HeaderText="تعداد مشاهده" SortExpression="VisitCount" />
                <asp:BoundColumn DataField="AdSourceDescription" HeaderText="منبع" SortExpression="AdSource" />
                <asp:BoundColumn DataField="IsActive" HeaderText="فعال" SortExpression="IsActive" />
                <asp:TemplateColumn HeaderText="ویرایش">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink3" NavigateUrl='<%# MyGetJobUrlModify( Eval("Id")) %>'
                            Text="ویرایش" runat="server" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="حذف">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" CommandArgument='<%# Eval("Id") %>'
                            Text="حذف" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="اطلاعات بیشتر">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink2" NavigateUrl='<%# MyGetJobUrl( Eval("Id"), Eval("Title")) %>'
                            Text="جزییات" runat="server" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle Mode="NumericPages" PageButtonCount="20" HorizontalAlign="Center" />
        </asp:DataGrid>
    </div>
</asp:Content>
