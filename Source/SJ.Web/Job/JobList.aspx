<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobList.aspx.cs" Inherits="SJ.Web.JobList"
    MasterPageFile="~/MasterPages/MainMaster.Master" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    فهرست مشاغل
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainHolder">
    <div>
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    Title:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtTitle" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button Text="جستجو" runat="server" ID="btnSearch" OnLoad="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView runat="server" ID="grdJobList" DataSourceID="odsJobList" AutoGenerateColumns="false"
            AllowPaging="true" AllowSorting="true">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                <asp:BoundField DataField="Title" HeaderText="عنوان" SortExpression="Title" />
                <asp:BoundField DataField="DateAddedPersian" HeaderText="تاریخ ثبت" SortExpression="DateAdded" />
                <asp:TemplateField HeaderText="اطلاعات بیشتر">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink2" NavigateUrl='<%# "JobDetail.aspx?ID=" + Eval("ID") %>'
                            Text="جزییات" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource runat="server" ID="odsJobList" TypeName="SJ.Core.JobDao" SelectMethod="FindAll"
            EnablePaging="true" SelectCountMethod="FindAllCount" SortParameterName="sortOrder">
            <SelectParameters>
                <asp:ControlParameter PropertyName="Text" ControlID="txtTitle" Name="title" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>
