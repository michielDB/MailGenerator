using System;

namespace MailGenerator
{
    public partial class Export : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtHtmlExport.InnerText = Session["HtmlExport"]?.ToString() ?? "No valid export";
        }
    }
}