<%@ Page Language="C#" MasterPageFile="~/MailGenerator.Master" AutoEventWireup="true" CodeBehind="Newsletters.aspx.cs" Inherits="MailGenerator.Newsletters" EnableEventValidation="False" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <title><asp:Literal runat="server" Text="<%$ Resources:resource, Newsletters%>" /></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <div class="container">
        <h2><asp:Literal runat="server" Text="<%$ Resources:resource, Overview%>" /></h2>
        <asp:GridView runat="server" ID="gdvNewsletters"
                      CssClass="table table-hover table-striped"
                      UseAccessibleHeader="true"
                      ItemType="MailGenerator.DAL.Newsletter" DataKeyNames="Id" 
                      SelectMethod="gdvNewsletters_GetData"
                      AutoGenerateColumns="false"
                      OnRowCommand="gdvNewsletters_OnRowCommand"
                      OnRowDataBound="gdvNewsletters_OnRowDataBound"
                      AllowPaging="True" AllowSorting="True" PageSize="10">
            <Columns>
                <asp:DynamicField DataField="Id" HeaderText="<%$ Resources:resource, Id %>" Visible="False" />
                <asp:DynamicField DataField="DateCreated" HeaderText="<%$ Resources:resource, DateCreated %>" />
                <asp:DynamicField DataField="Name" HeaderText="<%$ Resources:resource, Name %>" />      
                <asp:TemplateField HeaderText="<%$ Resources:resource, CreatedBy %>">
                    <ItemTemplate>
                        <asp:Label Text="<%# Item.Customer.Name %>" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" ItemStyle-Width="20%">
                    <ItemTemplate>
                        <%--<asp:Button ID="btnEditNewsletter" runat="server" CausesValidation="false" CommandName="Edit" Text="<%$ Resources:resource, Edit %>" CommandArgument='<%# Eval("Id") %>' />--%>
                        <asp:LinkButton runat="server" CssClass="btn btn-primary fa fa-pencil tinyButton" CausesValidation="False" CommandName="Edit" CommandArgument='<%# Eval("Id") %>' ToolTip="<%$ Resources:resource, Edit%>"></asp:LinkButton>
                        <asp:LinkButton runat="server" CssClass="btn btn-primary fa fa-code tinyButton" CausesValidation="False" CommandName="Export" CommandArgument='<%# Eval("Id") %>' ToolTip="<%$ Resources:resource, ExportHtml%>"></asp:LinkButton>
                        <asp:LinkButton runat="server" CssClass="btn btn-primary fa fa-file-o tinyButton" CausesValidation="False" CommandName="Save" CommandArgument='<%# Eval("Id") %>' ToolTip="<%$ Resources:resource, SaveHtml%>"></asp:LinkButton>
                        <asp:LinkButton runat="server" CssClass="btn btn-info fa fa-newspaper-o tinyButton" CausesValidation="False" CommandName="Preview" CommandArgument='<%# Eval("Id") %>' ToolTip="<%$ Resources:resource, Preview%>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle CssClass="pagination-ys" HorizontalAlign="Right" Wrap="True" />
        </asp:GridView>
        <asp:LinkButton ID="btnCreateNewsletter" runat="server" CssClass="btn btn-lg btn-primary btn-block" OnClick="btnCreateNewsletter_Click"><asp:Literal runat="server" Text="<%$ Resources:resource, CreateNew%>" /></asp:LinkButton>
    </div>
</asp:Content>