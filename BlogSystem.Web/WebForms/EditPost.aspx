<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPost.aspx.cs" MasterPageFile="/Site.Master" Inherits="BlogSystem.Web.WebForms.EditPost" ValidateRequest="false"%>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
  <script src='<%= this.ResolveClientUrl("~/Scripts/bootstrap-tagsinput.min.js") %>' type="text/javascript"></script>
  <script src='<%= this.ResolveClientUrl("~/Scripts/ckeditor/ckeditor.js") %>' type="text/javascript"></script>
    <div class="row">
        <div class="col-lg-11">
            <div class="new-post-form">
              <fieldset>
                <legend>Edit Post</legend>
                
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="postTitle" CssClass="col-lg-2 control-label">Title</asp:Label>
                    <div class="col-lg-10">
                        <asp:TextBox runat="server" Visible="False" ID="postId"></asp:TextBox>
                        <asp:TextBox runat="server" CssClass="form-control margin-bottom" ID="postTitle"></asp:TextBox>
                    </div>
                </div>
        
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="postContent" CssClass="col-lg-2 control-label">Content</asp:Label>
                    <div class="col-lg-10">
                    <asp:TextBox runat="server" TextMode="MultiLine" CssClass="ckeditor margin-bottom" ID="postContent"></asp:TextBox>
                    </div>
                </div>
        
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="postCategory" CssClass="col-lg-2 control-label">Category</asp:Label>
                    <div class="col-lg-10">
                        <asp:TextBox runat="server" CssClass="form-control margin-bottom" ID="postCategory" disabled=""></asp:TextBox>
                    </div>
                </div>

                <div class="form-group" id="bootstrapTagsInputForm">
                    <asp:Label runat="server" AssociatedControlID="postTags" CssClass="col-lg-2 control-label">Tags</asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox runat="server" ID="postTags" name="tags" CssClass="form-control" data-role="tagsinput" disabled=""></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                  <div class="col-lg-10 col-lg-offset-2">
                    <asp:Button runat="server" ID="submitChanges" CssClass="btn btn-primary" Text="Submit" OnClick="submitChanges_OnClick"/>
                  </div>
                </div>
              </fieldset>
            </div>
        </div>
    </div>
</asp:Content>