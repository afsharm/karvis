<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobList.aspx.cs" Inherits="SJ.Web.JobList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView runat="server" ID="grdJobList" DataSourceID="odsJobList" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" />
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="DateAdded" HeaderText="DateAdded" />
                <asp:TemplateField HeaderText="Detail">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink2" NavigateUrl='<%# "JobDetail.aspx?ID=" + Eval("ID") %>'
                            Text="Deatils" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource runat="server" ID="odsJobList" TypeName="SJ.Core.JobDao" SelectMethod="FindAll">
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
