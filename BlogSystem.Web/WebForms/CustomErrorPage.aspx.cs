namespace BlogSystem.Web.WebForms
{
    using System;

    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.errorMessage.Text = this.Request.QueryString["ErrorMessage"];
        }
    }
}