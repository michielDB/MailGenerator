<%@ Page Language="C#" MasterPageFile="~/MailGenerator.Master" AutoEventWireup="true" CodeBehind="CreateArticle.aspx.cs" Inherits="MailGenerator.CreateArticle" validateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <title><asp:Literal runat="server" Text="<%$ Resources:resource, CreateArticle%>" /></title>
    <script type="text/javascript" src="scripts/CreateArticle.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <h2><asp:Literal runat="server" Text="<%$ Resources:resource, CreateArticle%>" /></h2>
        <div class="container">
            <asp:HiddenField runat="server" id="txtNewsletterId" />
            <asp:HiddenField runat="server" id="txtRowNr" />
            <asp:HiddenField runat="server" id="txtColumnNr" />
            <div class="row top-buffer">
                <div class="col-md-2">
                    <asp:Label runat="server" Text="<%$ Resources:resource, Title %>"></asp:Label>
                </div>
                <div class="col-md-10">
                    <asp:TextBox runat="server" id="txtTitle"></asp:TextBox>
                </div>
            </div>
            <div class="row top-buffer">
                <div class="col-md-2">
                    <asp:Label runat="server" Text="<%$ Resources:resource, Summary %>"></asp:Label>
                </div>
                <div class="col-md-10">
                    <textarea runat="server" id="txtSummary" class="editable"></textarea>
                </div>
            </div>
            <div class="row top-buffer">
                <div class="col-md-2">
                    <asp:Label runat="server" Text="<%$ Resources:resource, Image %>"></asp:Label>
                </div>
                <div class="col-md-10">
                    <asp:Image ID="imgImage" runat="server" CssClass="img-preview" />
                    <asp:FileUpload ID="fupImage" runat="server" accept="image/*" multiple="false" onChange="previewImage(this)" />
                </div>
            </div>
            <div class="row top-buffer">
                <div class="col-md-2">
                    <asp:Label runat="server" Text="<%$ Resources:resource, ImageAlt %>"></asp:Label>
                </div>
                <div class="col-md-10">
                    <asp:TextBox runat="server" id="txtImageAlt"></asp:TextBox>
                </div>
            </div>
            <div class="row top-buffer">
                <div class="col-md-2">
                    <asp:Label runat="server" Text="<%$ Resources:resource, Link %>"></asp:Label>
                </div>
                <div class="col-md-10">
                    <asp:TextBox runat="server" id="txtLink"></asp:TextBox>
                </div>
            </div>
            <div class="row top-buffer">
                <div class="col-md-2">
                    <asp:Label runat="server" Text="<%$ Resources:resource, ButtonText %>"></asp:Label>
                </div>
                <div class="col-md-10">
                    <asp:TextBox runat="server" id="txtButtonText"></asp:TextBox>
                </div>
            </div>
            <div class="row top-buffer">
                <div class="col-md-2"></div>
                <div class="col-md-6">
                    <asp:LinkButton runat="server" ID="btnSaveArticle" CssClass="btn btn-lg btn-success btn-block" OnClick="btnSaveArticle_OnClick"><i class="fa fa-save fa-fw"></i><asp:Literal runat="server" Text="<%$ Resources:resource, SaveArticle %>"></asp:Literal></asp:LinkButton>
                </div>
                <div class="col-md-4">
                    <asp:LinkButton runat="server" id="btnBack" CssClass="btn btn-lg btn-default btn-block" OnClick="btnBack_OnClick"><i class="fa fa-undo fa-fw"></i><asp:Literal runat="server" Text="<%$ Resources:resource, Back %>"></asp:Literal></asp:LinkButton>
                </div>
            </div>
        </div>
</asp:Content>






<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><asp:Literal runat="server" Text="<%$ Resources:resource, CreateArticle%>" /></title>
    <script src="scripts/jquery/jquery-1.12.4.js"></script>
    <script src="scripts/jquery/jquery-ui-1.12.1.js"></script>
    <script src="scripts/bootstrap/bootstrap.js"></script>
    <link href="Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/css/MailGenerator.css" rel="stylesheet" />
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    
    <!-- include tinymce css/js-->
    <script type="text/javascript" src="scripts/tinymce/tinymce.min.js"></script>
    <script type="text/javascript" src="scripts/CreateArticle.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <h2><asp:Literal runat="server" Text="<%$ Resources:resource, CreateArticle%>" /></h2>
        <div class="container">
            <asp:HiddenField runat="server" id="txtNewsletterId" />
            <asp:HiddenField runat="server" id="txtRowNr" />
            <asp:HiddenField runat="server" id="txtColumnNr" />
            <div class="row top-buffer">
                <div class="col-md-2">
                    <asp:Label runat="server" Text="<%$ Resources:resource, Title %>"></asp:Label>
                </div>
                <div class="col-md-10">
                    <asp:TextBox runat="server" id="txtTitle"></asp:TextBox>
                </div>
            </div>
            <div class="row top-buffer">
                <div class="col-md-2">
                    <asp:Label runat="server" Text="<%$ Resources:resource, Summary %>"></asp:Label>
                </div>
                <div class="col-md-10">
                    <textarea runat="server" id="txtSummary" class="editable"></textarea>
                </div>
            </div>
            <div class="row top-buffer">
                <div class="col-md-2">
                    <asp:Label runat="server" Text="<%$ Resources:resource, Image %>"></asp:Label>
                </div>
                <div class="col-md-10">
                    <asp:Image ID="imgImage" runat="server" CssClass="img-preview" />
                    <asp:FileUpload ID="fupImage" runat="server" accept="image/*" multiple="false" onChange="previewImage(this)" />
                </div>
            </div>
            <div class="row top-buffer">
                <div class="col-md-2">
                    <asp:Label runat="server" Text="<%$ Resources:resource, ImageAlt %>"></asp:Label>
                </div>
                <div class="col-md-10">
                    <asp:TextBox runat="server" id="txtImageAlt"></asp:TextBox>
                </div>
            </div>
            <div class="row top-buffer">
                <div class="col-md-2">
                    <asp:Label runat="server" Text="<%$ Resources:resource, Link %>"></asp:Label>
                </div>
                <div class="col-md-10">
                    <asp:TextBox runat="server" id="txtLink"></asp:TextBox>
                </div>
            </div>
            <div class="row top-buffer">
                <div class="col-md-2">
                    <asp:Label runat="server" Text="<%$ Resources:resource, ButtonText %>"></asp:Label>
                </div>
                <div class="col-md-10">
                    <asp:TextBox runat="server" id="txtButtonText"></asp:TextBox>
                </div>
            </div>
            <div class="row top-buffer">
                <div class="col-md-8">
                    <asp:LinkButton runat="server" ID="btnSaveArticle" CssClass="btn btn-lg btn-primary btn-block" OnClick="btnSaveArticle_OnClick" Text="<%$ Resources:resource, SaveArticle %>"></asp:LinkButton>
                </div>
                <div class="col-md-4">
                    <asp:LinkButton runat="server" id="btnBack" CssClass="btn btn-lg btn-primary btn-block" OnClick="btnBack_OnClick" Text="<%$ Resources:resource, Back %>"></asp:LinkButton>
                </div>
            </div>
        </div>
    </form>
</body>
</html>--%>
