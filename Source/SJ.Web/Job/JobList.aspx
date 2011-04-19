<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobList.aspx.cs" Inherits="SJ.Web.JobList"
    MasterPageFile="~/MasterPages/MainMaster.Master" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    فهرست مشاغل
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="MainHolder">
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
</asp:Content>
