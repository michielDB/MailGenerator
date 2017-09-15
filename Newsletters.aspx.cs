using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MailGenerator.DAL;
using MailGenerator.Helper;

namespace MailGenerator
{
    public partial class Newsletters : Page
    {
        private MailGeneratorEntities db = new MailGeneratorEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
            }
            else
            {
                Response.Redirect("Login.aspx", true);
            }
        }

        public IQueryable<Newsletter> gdvNewsletters_GetData()
        {
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsIdentity id = (FormsIdentity) HttpContext.Current.User.Identity;

                int custId = Convert.ToInt32(id.Ticket.Name);
                var query = db.Newsletter.Where(n => n.CustomerId == custId).Include(c => c.Customer)
                    .OrderByDescending(n => n.DateCreated);
                return query;
            }

            return null;
        }

        protected void btnCreateNewsletter_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateNewsletter.aspx", true);
        }

        protected void gdvNewsletters_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(e.CommandArgument);

                switch (e.CommandName)
                {
                    case "Edit":
                        Response.Redirect("CreateNewsletter.aspx?Id=" + id, true);
                        break;
                    case "Export":
                        string export = Utils.GenerateHtml(id);
                        Session["HtmlExport"] = export;
                        Response.Write("<script type='text/javascript'>");
                        Response.Write("window.open('Export.aspx','_blank');");
                        Response.Write("</script>");
                        break;
                    case "Save":
                        string newsletterName = db.Newsletter.FirstOrDefault(n => n.Id == id)?.Name;
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ContentType = "text/plain";
                        Response.AddHeader("Content-Disposition",
                            "attachment;filename=export_" + newsletterName?.Replace(" ", "_") + ".html");
                        Response.Write(Utils.GenerateHtml(id));
                        Response.End();
                        break;
                    case "Preview":
                        Session["HtmlExport"] = Utils.GenerateHtml(id);
                        Response.Write("<script type='text/javascript'>");
                        Response.Write("window.open('Preview.aspx','_blank');");
                        Response.Write("</script>");
                        break;
                    default:
                        return;
                }
            }
            catch (Exception ex)
            {
                Logging.LogException(ex);
            }
        }

        protected void gdvNewsletters_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(gdvNewsletters, "Edit$" + DataBinder.Eval(e.Row.DataItem, "Id"));
                e.Row.Attributes["style"] = "cursor:pointer";
            }
        }
    }
}