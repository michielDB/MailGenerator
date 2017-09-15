using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using MailGenerator.DAL;

namespace MailGenerator.Helper
{
    public class Utils
    {
        private static MailGeneratorEntities db = new MailGeneratorEntities();

        public static void FocusControlOnPageLoad(string clientId, System.Web.UI.Page page)
        {
            page?.RegisterClientScriptBlock("CtrlFocus",
                @"<script> 
      function ScrollView()
      {
         var el = document.getElementById('" + clientId + @"')
         if (el != null)
         {        
            el.scrollIntoView();
            el.focus();
         }
      }
      window.onload = ScrollView;
      </script>");
        }

        public static string GetHttpLink(string link)
        {
            try
            {
                string httpLink = "";

                if (!link.StartsWith("http") || !link.StartsWith("https"))
                    httpLink = "http://" + link;

                return httpLink;
            }
            catch (Exception ex)
            {
                Logging.LogException(ex);
                return null;
            }
        }

        public static string ResizeImage(string dir, Stream fileContent)
        {
            try
            {
                // Create a bitmap of the content of the fileUpload control in memory
                Bitmap originalBmp = new Bitmap(fileContent);

                // Calculate the new image dimensions
                double origWidth = originalBmp.Width;
                double origHeight = originalBmp.Height;
                double sngRatio = origWidth / origHeight;
                int newWidth = 760;
                int newHeight = Convert.ToInt32(newWidth / sngRatio);

                // Create a new bitmap which will hold the previous resized bitmap
                Bitmap newBmp = new Bitmap(originalBmp, newWidth, newHeight);

                // Create a graphic based on the new bitmap
                Graphics oGraphics = Graphics.FromImage(newBmp);

                // Set the properties for the new graphic file
                oGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Draw the new graphic based on the resized bitmap
                oGraphics.DrawImage(originalBmp, 0, 0, newWidth, newHeight);

                Logging.LogMessage("Add timestamp");
                // Save the new graphic file to the server
                dir = AddTimestampToFilename(dir, "yyyyMMdd_HHmmss");
                Logging.LogMessage("Directory: " + dir);
                newBmp.Save(dir);

                // Once finished with the bitmap objects, we deallocate them.
                originalBmp.Dispose();
                newBmp.Dispose();
                oGraphics.Dispose();

                return GetShortFilePath(dir);
            }
            catch (Exception ex)
            {
                Logging.LogException(ex);
                return null;
            }
        }

        private static string AddTimestampToFilename(string originalDir, string format)
        {
            return originalDir.Insert(originalDir.LastIndexOf('.'), "_" + DateTime.Now.ToString(format));
        }

        private static string GetShortFilePath(string originalDir)
        {
            var shortPath = originalDir.Substring(originalDir.IndexOf("\\Images\\", StringComparison.Ordinal) + 1).Replace(@"\", "/");
            return shortPath;
        }

        public static string GenerateHtml(int newsletterId)
        {
            try
            {
                Newsletter nl = db.Newsletter.FirstOrDefault(n => n.Id == newsletterId);
                bool imageLeft = true;

                if (nl == null)
                    return null;

                Template template = new Template();

                string export = template.FixedBeginning();
                export += template.Header(nl.HeaderImage, nl.HeaderImageLink, nl.HeaderImageAlt, nl.HeaderTitle,
                    nl.HeaderSubTitle);
                export += template.Introduction(nl.Introduction);

                if (nl.Article != null && nl.Article.Count > 0)
                {
                    for (int i = 1;
                        i <= nl.Article.OrderBy(a => a.RowNr).ThenBy(a => a.ColumnNr).LastOrDefault()?.RowNr;
                        i++)
                    {
                        var articlesPerRow = nl.Article.Where(a => a.RowNr == i).ToList();

                        if (articlesPerRow.Count == 1)
                        {
                            export += template.MultipleArticles(articlesPerRow, imageLeft);
                            imageLeft = !imageLeft;
                        }
                        else if (articlesPerRow.Count > 0)
                            export += template.MultipleArticles(articlesPerRow);
                    }
                }

                export += template.Footer(nl.FooterImage, nl.FooterImageAlt, nl.FooterText);
                export += template.FixedEnding();

                return export;
            }
            catch (Exception ex)
            {
                Logging.LogException(ex);
                return null;
            }
        }
    }
}