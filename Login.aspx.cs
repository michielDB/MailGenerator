using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using MailGenerator.DAL;
using MailGenerator.Helper;

namespace MailGenerator
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly MailGeneratorEntities _db = new MailGeneratorEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Logout"] == null) return;
            FormsAuthentication.SignOut();
            Response.Redirect("~/Login.aspx");
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            Customer cust = ValidateCredentials(txtEmail.Text, txtPassword.Text);

            if (cust != null)
            {
                FormsAuthenticationTicket tkt =
                    new FormsAuthenticationTicket(cust.Id.ToString(), chkRememberMe.Checked, 60);
                string cookiestr = FormsAuthentication.Encrypt(tkt);
                HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                if (chkRememberMe.Checked)
                    ck.Expires = tkt.Expiration;
                ck.Path = FormsAuthentication.FormsCookiePath;
                Response.Cookies.Add(ck);

                string strRedirect = Request["ReturnUrl"];
                if (strRedirect == null)
                    strRedirect = "Newsletters.aspx";
                Response.Redirect(strRedirect, true);
            }
            else
                Response.Redirect("Login.aspx", true);
        }

        private Customer ValidateCredentials(string email, string password)
        {
            try
            {
                string hashedPass = new PasswordHash().GetSha1HashData(password);
                return _db.Customer.FirstOrDefault(c => c.Email == email && c.Password == hashedPass);
            }
            catch (Exception ex)
            {
                Logging.LogException(ex);
                return null;
            }
        }
    }
}