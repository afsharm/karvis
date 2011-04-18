<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddJob.aspx.cs" Inherits="SJ.Web.Admin.AddJob" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label Text="" runat="server" ID="lblMessage" />
    <div>
        <table>
            <tr>
                <td>
                    Title
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtTitle" />
                </td>
            </tr>
            <tr>
                <td>
                    Description
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" />
                </td>
            </tr>
            <tr>
                <td>
                    URL
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtURL" />
                </td>
            </tr>
            <tr>
                <td>
                    Tag
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtTag" />
                </td>
            </tr>
        </table>
        <asp:Button Text="Save" runat="server" ID="btnSave" OnClick="btnSave_Click" />
    </div>
    </form>
</body>
</html>
