<%@ Page Language="C#" MasterPageFile="~/MailGenerator.Master" AutoEventWireup="true" CodeBehind="CreateNewsletter.aspx.cs" Inherits="MailGenerator.CreateNewsletter" validateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <title><asp:Literal runat="server" Text="<%$ Resources:resource, CreateNewsletter%>" /></title>
    <script type="text/javascript" src="scripts/CreateNewsletter.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <div class="container" id="accordion">
            <h2><asp:Literal runat="server" Text="<%$ Resources:resource, CreateNewsletter%>" /></h2>
            <div class="row top-buffer">
                <div class="col-md-1">
                    <asp:Label runat="server" Text="<%$ Resources:resource, Name %>"></asp:Label>
                </div>
                <div class="col-md-11">
                    <asp:TextBox runat="server" id="txtName"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="panel panel-default">
                <div class="panel-heading" data-parent="#accordion" data-toggle="collapse" data-target="#collapseHeader">
                    <asp:Literal runat="server" Text="<%$ Resources:resource, Header%>" />
                </div>
            <div id="collapseHeader" class="panel-collapse collapse in">
                <div class="panel-body">
                   <div class="row top-buffer">
                       <div class="col-md-2">
                           <asp:Label runat="server" Text="<%$ Resources:resource, Image %>"></asp:Label>
                       </div>
                       <div class="col-md-10">
                           <asp:Image ID="imgHeaderImage" runat="server" CssClass="img-preview" />
                           <asp:FileUpload ID="fupHeaderImage" runat="server" accept="image/*" multiple="false" onChange="previewHeaderImage(this)" />
                       </div>
                   </div>
                    <div class="row top-buffer">
                        <div class="col-md-2">
                            <asp:Label runat="server" Text="<%$ Resources:resource, ImageAlt %>"></asp:Label>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" id="txtHeaderImageAlt"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row top-buffer">
                        <div class="col-md-2">
                            <asp:Label runat="server" Text="<%$ Resources:resource, ImageLink %>"></asp:Label>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" id="txtHeaderImageLink"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row top-buffer">
                        <div class="col-md-2">
                            <asp:Label runat="server" Text="<%$ Resources:resource, HeaderTitle %>"></asp:Label>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" id="txtHeaderTitle"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row top-buffer">
                        <div class="col-md-2">
                            <asp:Label runat="server" Text="<%$ Resources:resource, HeaderSubtitle %>"></asp:Label>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" id="txtHeaderSubTitle"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
                </div>
            <div class="panel panel-default">
                <div class="panel-heading collapsed" data-parent="#accordion" data-toggle="collapse" data-target="#collapseIntroduction">
                    <asp:Literal runat="server" Text="<%$ Resources:resource, Introduction%>" />
                </div>
                <div id="collapseIntroduction" class="panel-collapse collapse">
                    <div class="panel-body">
                        <div class="row top-buffer">
                            <div class="col-md-2">
                                <asp:Label runat="server" Text="<%$ Resources:resource, Introduction %>"></asp:Label>
                            </div>
                            <div class="col-md-10">
                                <textarea runat="server" id="txtIntroduction" class="editable"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading collapsed" data-parent="#accordion" data-toggle="collapse" data-target="#collapseArticles">
                    <asp:Literal runat="server" Text="<%$ Resources:resource, Articles%>" />
                </div>
                <div id="collapseArticles" class="panel-collapse collapse">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Literal runat="server" Text="<%$ Resources:resource, Max3CellsPerRow%>" />
                            </div>
                        </div>
                        <div class="row top-buffer">
                            <div class="col-md-12">
                                <table class="table table-bordered order-list" id="tableAddRow" runat="server" style="float: left; width: 70%; table-layout: fixed">
                                </table>
                                <table id="tableButtons" runat="server" style="float: left; width: 25%; table-layout: fixed; margin-left: 25px">
                                </table>
                                <input type="button" runat="server" class="btn btn-lg btn-block" id="addrow" value="<%$ Resources:resource, AddRow%>" onclick="addNewRow()" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading collapsed" data-parent="#accordion" data-toggle="collapse" data-target="#collapseFooter">
                    <asp:Literal runat="server" Text="<%$ Resources:resource, Footer%>" />
                </div>
                <div id="collapseFooter" class="panel-collapse collapse">
                    <div class="panel-body">
                   <div class="row top-buffer">
                       <div class="col-md-2">
                           <asp:Label runat="server" Text="<%$ Resources:resource, Image %>"></asp:Label>
                       </div>
                       <div class="col-md-10">
                           <asp:Image ID="imgFooterImage" runat="server" CssClass="img-preview" />
                           <asp:FileUpload ID="fupFooterImage" runat="server" accept="image/*" multiple="false" onChange="previewFooterImage(this)" />
                       </div>
                   </div>
                    <div class="row top-buffer">
                        <div class="col-md-2">
                            <asp:Label runat="server" Text="<%$ Resources:resource, ImageAlt %>"></asp:Label>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" id="txtFooterImageAlt"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row top-buffer">
                        <div class="col-md-2">
                            <asp:Label runat="server" Text="<%$ Resources:resource, FooterText %>"></asp:Label>
                        </div>
                        <div class="col-md-10">
                            <textarea runat="server" id="txtFooterText" class="editable"></textarea>
                        </div>
                    </div>
                </div>
                </div>
            </div>
            <div class="row top-buffer">
                <div class="col-md-6">
                    <asp:LinkButton runat="server" ID="btnSaveNewsletter" CssClass="btn btn-lg btn-success btn-block" OnClick="btnSaveNewsletter_Click"><asp:Literal runat="server" /><i class="fa fa-save fa-fw"></i><asp:Literal runat="server" Text="<%$ Resources:resource, SaveNewsletter%>"></asp:Literal></asp:LinkButton>
                </div>
                <div class="col-md-3">
                    <asp:LinkButton runat="server" ID="btnExportHTML" CssClass="btn btn-lg btn-primary btn-block" OnClick="btnExportHTML_Click"><asp:Literal runat="server" /><i class="fa fa-code fa-fw"></i><asp:Literal runat="server" Text="<%$ Resources:resource, ExportHtml%>"></asp:Literal></asp:LinkButton>                    
                </div>
                <div class="col-md-3">
                    <asp:LinkButton runat="server" ID="btnPreview" CssClass="btn btn-lg btn-info btn-block" OnClick="btnPreview_OnClick"><asp:Literal runat="server" /><i class="fa fa-newspaper-o fa-fw"></i><asp:Literal runat="server" Text="<%$ Resources:resource, Preview%>"></asp:Literal></asp:LinkButton>                    
                </div>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="locNewArticle" Value="<%$ Resources:resource, NewArticle%>" />
        <asp:HiddenField runat="server" ID="locAddCell" Value="<%$ Resources:resource, AddCell%>" />
        <asp:HiddenField runat="server" ID="locDeleteRow" Value="<%$ Resources:resource, DeleteRow%>" />
</asp:Content>