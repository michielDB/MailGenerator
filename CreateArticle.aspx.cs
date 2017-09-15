using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using MailGenerator.DAL;
using MailGenerator.Helper;

namespace MailGenerator
{
    public partial class CreateArticle : System.Web.UI.Page
    {
        private readonly MailGeneratorEntities _db = new MailGeneratorEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                string sId = Request.QueryString["Id"];
                string sNewsletterId = Request.QueryString["NewsletterId"];
                string sRow = Request.QueryString["Row"];
                string sColumn = Request.QueryString["Column"];

                int id = 0;

                if (sId != null)
                    id = Convert.ToInt32(sId);

                if (!IsPostBack)
                {
                    if (id != 0)
                    {
                        LoadArticle(id);
                    }
                    else if (sNewsletterId == null)
                        Response.Redirect("Newsletters.aspx", true);
                    else
                    {
                        txtNewsletterId.Value = sNewsletterId;
                        txtRowNr.Value = sRow;
                        txtColumnNr.Value = sColumn;
                    }
                }
            }
            else
                Response.Redirect("Login.aspx", true);
        }

        protected void btnSaveArticle_OnClick(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsIdentity id = (FormsIdentity) HttpContext.Current.User.Identity;
                int custId = Convert.ToInt32(id.Ticket.Name);

                SaveArticle(custId);

                Response.Redirect("CreateNewsletter.aspx?Id=" + txtNewsletterId.Value, true);
            }

            Response.Redirect("Login.aspx", true);
        }

        private void SaveArticle(int custId)
        {
            try
            {
                int articleId = Request.QueryString["Id"] != null ? Convert.ToInt32(Request.QueryString["Id"]) : 0;

                Article article = new Article();

                if (articleId != 0)
                {
                    article = _db.Article.FirstOrDefault(a => a.Id == articleId);
                }

                if (article != null)
                {
                    article.Title = txtTitle.Text;
                    article.Summary = txtSummary.InnerText;
                    if (fupImage.HasFile)
                    {
                        article.Image =
                            Utils.ResizeImage(
                                Server.MapPath("~/Images/") + Path.GetFileName(fupImage.PostedFile.FileName),
                                fupImage.FileContent);
                    }
                    article.ImageAlt = txtImageAlt.Text;
                    article.Link = txtLink.Text;
                    article.ButtonText = txtButtonText.Text;
                    article.NewsletterId = Convert.ToInt32(txtNewsletterId.Value);
                    article.RowNr = Convert.ToInt32(txtRowNr.Value);
                    article.ColumnNr = Convert.ToInt32(txtColumnNr.Value);
                    article.Deleted = false;

                    if (articleId != 0)
                    {
                        article.Id = articleId;
                        article.DateModified = DateTime.Now;
                        article.CustomerModified = custId;
                    }
                    else
                    {
                        article.DateCreated = DateTime.Now;
                        article.CustomerCreated = custId;
                    }

                    _db.Article.AddOrUpdate(article);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logging.LogException(ex);
            }
        }

        private void LoadArticle(int id)
        {
            try
            {
                Article article = _db.Article.FirstOrDefault(a => a.Id == id);

                if (article != null)
                {
                    txtTitle.Text = article.Title;
                    txtSummary.InnerText = article.Summary;
                    imgImage.ImageUrl = article.Image;
                    txtImageAlt.Text = article.ImageAlt;
                    txtLink.Text = article.Link;
                    txtButtonText.Text = article.ButtonText;
                    txtNewsletterId.Value = article.NewsletterId.ToString();
                    txtRowNr.Value = article.RowNr.ToString();
                    txtColumnNr.Value = article.ColumnNr.ToString();
                }
            }
            catch (Exception ex)
            {
                Logging.LogException(ex);
            }
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("CreateNewsletter.aspx?Id=" + txtNewsletterId.Value, true);
        }
    }
}