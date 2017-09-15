using System;
using System.Web.UI;

namespace MailGenerator
{
    public partial class Preview : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            previewContent.Controls.Add(new LiteralControl(Session["HtmlExport"]?.ToString() ?? "No valid export"));
        }
    }
}