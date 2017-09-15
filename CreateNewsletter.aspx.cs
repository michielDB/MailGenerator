using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MailGenerator.DAL;
using MailGenerator.Helper;

namespace MailGenerator
{
    public partial class CreateNewsletter : Page, IPostBackEventHandler
    {
        private readonly MailGeneratorEntities _db = new MailGeneratorEntities();

        public void RaisePostBackEvent(string eventArgument)
        {
            //throw new NotImplementedException();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                int newsletterId = Request.QueryString["Id"] != null ? Convert.ToInt32(Request.QueryString["Id"]) : 0;

                var eventtarget = Request.Form["__EVENTTARGET"];
                if (eventtarget != null && eventtarget.ToLower().Contains("addarticle"))
                    AddArticle(eventtarget);
                else if (eventtarget != null && eventtarget.ToLower().Contains("loadarticle"))
                    LoadArticle(eventtarget.Substring(eventtarget.LastIndexOf("_", StringComparison.Ordinal) + 1));
                else if (eventtarget != null && eventtarget.ToLower().Contains("removerow"))
                {
                    RemoveRow(eventtarget);
                    LoadNewsletter(newsletterId);
                }
                if ((newsletterId != 0 && !IsPostBack) || (eventtarget != null && eventtarget.ToLower().Contains("exporthtml")))
                    LoadNewsletter(newsletterId);
            }
            else
                Response.Redirect("Login.aspx", true);
        }

        protected void btnSaveNewsletter_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsIdentity id = (FormsIdentity) HttpContext.Current.User.Identity;
                int custId = Convert.ToInt32(id.Ticket.Name);

                SaveNewsletter(custId);

                Response.Redirect("Newsletters.aspx", true);
            }

            Response.Redirect("Login.aspx", true);
        }

        protected void btnExportHTML_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsIdentity id = (FormsIdentity) HttpContext.Current.User.Identity;
                int custId = Convert.ToInt32(id.Ticket.Name);

                int newsletterId = SaveNewsletter(custId);

                HttpContext.Current.Session["HtmlExport"] = Utils.GenerateHtml(newsletterId);

                HttpContext.Current.Response.Write("<script type='text/javascript'>");
                HttpContext.Current.Response.Write("window.open('Export.aspx','_blank');");
                HttpContext.Current.Response.Write("</script>");
            }
        }

        protected void btnAddArticle_OnClick(object sender, EventArgs e)
        {
            AddArticle(((Button) sender).ID);
        }

        protected void btnLoadArticle_OnClick(object sender, EventArgs e)
        {
            LoadArticle(((LinkButton) sender).ID);
        }

        protected void btnPreview_OnClick(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsIdentity id = (FormsIdentity) HttpContext.Current.User.Identity;
                int custId = Convert.ToInt32(id.Ticket.Name);

                int newsletterId = SaveNewsletter(custId);

                Session["HtmlExport"] = Utils.GenerateHtml(newsletterId);
                Response.Write("<script type='text/javascript'>");
                Response.Write("window.open('Preview.aspx','_blank');");
                Response.Write("</script>");
            }
        }

        private void LoadNewsletter(int id)
        {
            try
            {
                Newsletter newsletter = _db.Newsletter.FirstOrDefault(n => n.Id == id);

                if (newsletter != null)
                {
                    txtName.Text = newsletter.Name;
                    imgHeaderImage.ImageUrl = newsletter.HeaderImage;
                    txtHeaderImageAlt.Text = newsletter.HeaderImageAlt;
                    txtHeaderImageLink.Text = newsletter.HeaderImageLink;
                    txtHeaderTitle.Text = newsletter.HeaderTitle;
                    txtHeaderSubTitle.Text = newsletter.HeaderSubTitle;
                    txtIntroduction.InnerText = newsletter.Introduction;
                    imgFooterImage.ImageUrl = newsletter.FooterImage;
                    txtFooterImageAlt.Text = newsletter.FooterImageAlt;
                    txtFooterText.InnerText = newsletter.FooterText;

                    LoadArticles(id);
                }
            }
            catch (Exception ex)
            {
                Logging.LogException(ex);
            }
        }

        private void LoadArticles(int newsletterId)
        {
            try
            {
                List<Article> articles = _db.Article.Where(a => a.NewsletterId == newsletterId && a.Deleted == false)
                    .OrderBy(a => a.RowNr).ThenBy(a => a.ColumnNr).ToList();

                int lastRow = 1;
                HtmlTableRow row = new HtmlTableRow();
                HtmlTableCell cell;
                LinkButton lbtn;

                foreach (var article in articles)
                {
                    if (article.RowNr == lastRow)
                    {
                        cell = new HtmlTableCell {Height = "51px"};
                        lbtn = new LinkButton
                        {
                            ID = "loadArticle_" + article.Id,
                            Text = article.Title,
                            OnClientClick = "__doPostBack(id);"
                        };

                        cell.Controls.Add(lbtn);
                        row.ID = article.RowNr.ToString();
                        row.Cells.Add(cell);
                    }
                    else
                    {
                        if (row.Cells.Count > 0)
                        {
                            row.ID = "tableAddRow" + lastRow;
                            tableAddRow.Rows.Add(row);
                            tableButtons.Rows.Add(AddButtonsToRow(lastRow));
                        }

                        //table.Rows.Add(row);
                        row = new HtmlTableRow();

                        cell = new HtmlTableCell {Height = "51px"};
                        lbtn = new LinkButton
                        {
                            ID = "loadArticle_" + article.Id,
                            Text = article.Title,
                            OnClientClick = "__doPostBack(id);"
                        };
                        cell.Controls.Add(lbtn);
                        row.ID = article.RowNr.ToString();
                        row.Cells.Add(cell);

                        lastRow = article.RowNr;
                    }
                }

                row.ID = "tableAddRow" + lastRow;
                tableAddRow.Rows.Add(row);
                tableButtons.Rows.Add(AddButtonsToRow(lastRow));
            }
            catch (Exception ex)
            {
                Logging.LogException(ex);
            }
        }

        private void RemoveRow(string senderId)
        {
            try
            {
                int row = Convert.ToInt32(senderId.Substring(senderId.LastIndexOf("w", StringComparison.Ordinal) + 1));

                int newsletterId = Request.QueryString["Id"] != null ? Convert.ToInt32(Request.QueryString["Id"]) : 0;

                List<Article> articlesToDelete =
                    _db.Article.Where(a => a.RowNr == row && a.NewsletterId == newsletterId).ToList();

                foreach (var article in articlesToDelete)
                {
                    article.Deleted = true;
                    _db.Article.AddOrUpdate(article);
                }

                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Logging.LogException(ex);
            }
        }

        private int SaveNewsletter(int custId)
        {
            try
            {
                int newsletterId = Request.QueryString["Id"] != null ? Convert.ToInt32(Request.QueryString["Id"]) : 0;

                Newsletter newsletter = new Newsletter();

                if (newsletterId != 0)
                {
                    newsletter = _db.Newsletter.FirstOrDefault(n => n.Id == newsletterId);
                }

                if (newsletter != null)
                {
                    newsletter.Name = txtName.Text;
                    newsletter.CustomerId = custId;
                    if (fupHeaderImage.HasFile)
                    {
                        newsletter.HeaderImage =
                            Utils.ResizeImage(
                                Server.MapPath("~/Images/") + Path.GetFileName(fupHeaderImage.PostedFile.FileName),
                                fupHeaderImage.FileContent);
                    }
                    newsletter.HeaderImageAlt = txtHeaderImageAlt.Text;
                    newsletter.HeaderImageLink = txtHeaderImageLink.Text;
                    newsletter.HeaderTitle = txtHeaderTitle.Text;
                    newsletter.HeaderSubTitle = txtHeaderSubTitle.Text;
                    newsletter.Introduction = txtIntroduction.InnerText;
                    if (fupFooterImage.HasFile)
                    {
                        newsletter.FooterImage =
                            Utils.ResizeImage(
                                Server.MapPath("~/Images/") + Path.GetFileName(fupFooterImage.PostedFile.FileName),
                                fupFooterImage.FileContent);
                    }
                    newsletter.FooterImageAlt = txtFooterImageAlt.Text;
                    newsletter.FooterText = txtFooterText.InnerText;

                    if (newsletterId != 0)
                    {
                        newsletter.Id = newsletterId;
                        newsletter.DateModified = DateTime.Now;
                        newsletter.CustomerModified = custId;
                    }
                    else
                    {
                        newsletter.DateCreated = DateTime.Now;
                        newsletter.CustomerCreated = custId;
                    }

                    _db.Newsletter.AddOrUpdate(newsletter);
                    _db.SaveChanges();

                    return newsletter.Id;
                }
                return 0;
            }
            catch (Exception ex)
            {
                Logging.LogException(ex);
                return 0;
            }
        }

        private void AddArticle(string senderId)
        {
            try
            {
                if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
                {
                    FormsIdentity id = (FormsIdentity) HttpContext.Current.User.Identity;
                    int custId = Convert.ToInt32(id.Ticket.Name);

                    int newsletterId = SaveNewsletter(custId);

                    //string senderId = ((Button)sender).ID;
                    string[] parts = senderId.Split('_');

                    var row = parts[parts.Length - 2];
                    var column = parts[parts.Length - 1];

                    Response.Redirect(
                        "CreateArticle.aspx?NewsletterId=" + newsletterId + "&Row=" + row + "&Column=" + column, true);
                }

                Response.Redirect("Login.aspx", true);
            }
            catch (Exception ex)
            {
                Logging.LogException(ex);
            }
        }

        private void LoadArticle(string articleId)
        {
            Response.Redirect("CreateArticle.aspx?Id=" + articleId);
        }

        private HtmlTableRow AddButtonsToRow(int lastRow)
        {
            HtmlTableCell cell = new HtmlTableCell();
            HtmlTableRow row = new HtmlTableRow();

            // Add AddCell button
            Button btnAddCell = new Button
            {
                ID = "addCell_" + (lastRow),
                Text = HttpContext.GetGlobalResourceObject("resource", "AddCell")?.ToString(),
                //Text = resources.AddCell,
                CssClass = "btn btn-info"
            };
            btnAddCell.OnClientClick = "addCell(id); return false;";
            //btnAddCell.Click += btnAddCell_OnClick;

            // Add RemoveRow button
            Button btnRemoveRow = new Button
            {
                ID = "removeRow" + (lastRow),
                Text = HttpContext.GetGlobalResourceObject("resource", "DeleteRow")?.ToString(),
                CssClass = "btn btn-danger"
            };
            btnRemoveRow.OnClientClick = "__doPostBack(id,\'\')";
            //btnRemoveRow.Click += btnRemoveRow_OnClick;
            cell.Width = "20%";
            cell.Height = "51px";
            cell.Controls.Add(btnAddCell);
            cell.Controls.Add(btnRemoveRow);
            row.Cells.Add(cell);

            return row;
        }
    }
}