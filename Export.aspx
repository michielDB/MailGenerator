<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Export.aspx.cs" Inherits="MailGenerator.Export" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> <asp:Literal runat="server" Text="<%$ Resources:resource, ExportHtml%>" /></title>
    <style type="text/css">
        html, body, form {
            height: 100%;
        }
        body {
            overflow: hidden;
        }
        textarea {
            height: 100%;
            width: 100%;
        }
    </style>
    <script type="text/javascript">
        function copyToClipboard() {
            var message = document.getElementById("txtHtmlExport");
            message.select();
            document.execCommand('copy');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input type="button" runat="server" id="btnCopyToClipboard" value="<%$ Resources:resource, CopyToClipboard %>" onclick="copyToClipboard();" />
        <textarea runat="server" id="txtHtmlExport"></textarea>
    </form>
</body>
</html>
