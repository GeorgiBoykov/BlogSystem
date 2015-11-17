<%@ Page Title="NewPost" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="NewPost.aspx.cs" Inherits="BlogSystem.Web.NewPost" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
  <%--<link rel="stylesheet" href="//cdn.jsdelivr.net/bootstrap.tagsinput/0.4.2/bootstrap-tagsinput.css" />--%>
  <script src='<%= this.ResolveClientUrl("~/Scripts/bootstrap-tagsinput.min.js") %>' type="text/javascript"></script>
    <div class="new-post-form">
      <fieldset>
        <legend>New Post</legend>
        <div class="form-group">
          <asp:Label runat="server" AssociatedControlID="postTitle" CssClass="col-lg-2 control-label">Title</asp:Label>
          <div class="col-lg-10">
            <asp:TextBox runat="server" CssClass="form-control" ID="postTitle" placeholder="Title"></asp:TextBox>
          </div>
        </div>
        <div class="form-group">
          <asp:Label runat="server" AssociatedControlID="postContent" CssClass="col-lg-2 control-label">Content</asp:Label>
          <div class="col-lg-10">
            <asp:TextBox runat="server" ID="postContent" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
            <span class="help-block">A longer block of help text that breaks onto a new line and may extend beyond one line.</span>
          </div>
        </div>
        <div class="form-group">
          <asp:Label runat="server" AssociatedControlID="postCategory" CssClass="col-lg-2 control-label">Category</asp:Label>
          <div class="col-lg-10">
            <asp:ListBox runat="server" ID="postCategory" CssClass="form-control" SelectionMode="Multiple">
           
            </asp:ListBox>
          </div>
        </div>
        <div class="form-group" id="bootstrapTagsInputForm">
        <asp:Label runat="server" AssociatedControlID="postTags" CssClass="col-lg-2 control-label">Tags</asp:Label>
        <div class="col-xs-8">
            <asp:TextBox runat="server" ID="postTags" name="tags" CssClass="form-control" data-role="tagsinput"></asp:TextBox>
            <span class="help-block">List your tags separated by comma e.g. (tag1, tag2, tag3...)</span>    
        </div>
        </div>
        <div class="form-group">
          <div class="col-lg-10 col-lg-offset-2">
            <asp:Button runat="server" ID="submitPost" CssClass="btn btn-primary" Text="Submit" OnClick="submitPost_OnClick"/>
          </div>
        </div>
      </fieldset>
    </div>
</asp:Content>