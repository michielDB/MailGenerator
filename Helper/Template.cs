using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using MailGenerator.DAL;

namespace MailGenerator.Helper
{
    public class Template
    {
        private readonly string _templatePath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["TemplatePath"]);

        public string FixedBeginning()
        {
            try
            {
                string template = File.ReadAllText(_templatePath + "fixedbeginning.html");
                return template;
            }
            catch (Exception ex)
            {
                Logging.LogException(ex);
                return null;
            }
        }

        public string Header(string image, string imageLink, string imageAlt, string title, string subtitle)
        {
            try
            {
                string template = File.ReadAllText(_templatePath + "header.html");

                template = template.Replace("[[header.image]]", GetHostPath() + "/" + image);
                template = template.Replace("[[header.imageLink]]", Utils.GetHttpLink(imageLink));
                template = template.Replace("[[header.imageAlt]]", imageAlt);
                template = template.Replace("[[header.title]]", title);
                template = template.Replace("[[header.subtitle]]", subtitle);

                return template;
            }
            catch (Exception ex)
            {
                Logging.LogException(ex);
                return null;
            }
        }

        public string Introduction(string intro)
        {
            try
            {
                if (string.IsNullOrEmpty(intro)) return null;

                string template = File.ReadAllText(_templatePath + "introduction.html");

                template = template.Replace("[[introduction]]", intro);

                return template;
            }
            catch (Exception ex)
            {
                Logging.LogException(ex);
                return null;
            }
        }

        public string MultipleArticles(List<Article> articles, bool imageLeft = false)
        {
            try
            {
                string template = null;

                switch (articles.Count)
                {
                    case 1:
                        template = imageLeft
                            ? File.ReadAllText(_templatePath + "leftimage.html")
                            : File.ReadAllText(_templatePath + "rightimage.html");
                        break;
                    case 2:
                        template = File.ReadAllText(_templatePath + "2columns.html");
                        break;
                    case 3:
                        template = File.ReadAllText(_templatePath + "3columns.html");
                        break;
                }

                for (int i = 0; i < articles.Count; i++)
                {
                    if (template != null)
                    {
                        template = template.Replace("[[article.title" + (i + 1) + "]]", articles[i].Title);
                        template = template.Replace("[[article.summary" + (i + 1) + "]]", articles[i].Summary);
                        template = template.Replace("[[article.image" + (i + 1) + "]]",
                            GetHostPath() + "/" + articles[i].Image);
                        template = template.Replace("[[article.imageAlt" + (i + 1) + "]]", articles[i].ImageAlt);
                        template = template.Replace("[[article.link" + (i + 1) + "]]",
                            Utils.GetHttpLink(articles[i].Link));
                        template = template.Replace("[[article.buttonText" + (i + 1) + "]]", articles[i].ButtonText);
                    }
                }

                return template;
            }
            catch (Exception ex)
            {
                Logging.LogException(ex);
                return null;
            }
        }

        public string Footer(string image, string imageAlt, string text)
        {
            try
            {
                string template = File.ReadAllText(_templatePath + "footer.html");

                template = template.Replace("[[footer.image]]", GetHostPath() + "/" + image);
                template = template.Replace("[[footer.imageAlt]]", imageAlt);
                template = template.Replace("[[footer.text]]", text);

                return template;
            }
            catch (Exception ex)
            {
                Logging.LogException(ex);
                return null;
            }
        }

        public string FixedEnding()
        {
            try
            {
                string template = File.ReadAllText(_templatePath + "fixedending.html");
                return template;
            }
            catch (Exception ex)
            {
                Logging.LogException(ex);
                return null;
            }
        }

        private string GetHostPath()
        {
            return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
        }
    }
}