namespace BlogSystem.Web.WebForms
{
    using System;
    using System.Web.UI;

    public partial class ErrorPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.errorMessage.Text = this.Request.QueryString["ErrorMessage"];
        }
    }
}