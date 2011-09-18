<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sand.aspx.cs" Inherits="Karvis.Web.sand" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DataGrid AllowPaging="true" ID="dgSand" runat="server" AllowSorting="true" OnPageIndexChanged="dgSand_PageIndexChanged"
            AllowCustomPaging="true" PageSize="5" ShowFooter="True" AutoGenerateColumns="false"
            OnSortCommand="dgSand_SortCommand">
            <Columns>
                <asp:TemplateColumn HeaderText="Id" SortExpression="Id">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="title" SortExpression="title">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="DateAdded" SortExpression="DateAdded">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("DateAdded") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle Mode="NumericPages" PageButtonCount="20" />
        </asp:DataGrid>
        <asp:HiddenField runat="server" ID="hdnSortExpression" />
    </div>
    </form>
</body>
</html>
