﻿<%@ Page Title="NewPost" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="NewPost.aspx.cs" Inherits="BlogSystem.Web.WebForms.NewPost" ValidateRequest="false" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
  <script src='<%= this.ResolveClientUrl("~/Scripts/bootstrap-tagsinput.min.js") %>' type="text/javascript"></script>
  <script src='<%= this.ResolveClientUrl("~/Scripts/ckeditor/ckeditor.js") %>' type="text/javascript"></script>
    <div class="row">
        <div class="col-lg-12">
            <div class="new-post-form">
              <fieldset>
                <legend>New Post</legend>
                
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="postTitle" CssClass="col-lg-2 control-label">Title</asp:Label>
                    <div class="col-lg-10">
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="postTitle" ValidationGroup="NewPostGroup"
                        CssClass="text-danger" ErrorMessage="The title field is required." Display="Dynamic"/>
                        <asp:TextBox runat="server" CssClass="form-control margin-bottom" ID="postTitle" placeholder="Title"></asp:TextBox>
                    </div>
                </div>
        
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="postContent" CssClass="col-lg-2 control-label">Content</asp:Label>
                    <div class="col-lg-10">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="postContent" ValidationGroup="NewPostGroup"
                        CssClass="text-danger" ErrorMessage="The content field is required." Display="Dynamic"/>
                    <asp:TextBox runat="server" TextMode="MultiLine" CssClass="ckeditor margin-bottom" ID="postContent"></asp:TextBox>
                    </div>
                </div>
        
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="postCategory" CssClass="col-lg-2 control-label">Category</asp:Label>
                    <div class="col-lg-10">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="postCategory" ValidationGroup="NewPostGroup"
                        CssClass="text-danger" ErrorMessage="The category field is required." Display="Dynamic"/>
                    <asp:ListBox runat="server" ID="postCategory" CssClass="form-control margin-bottom" SelectionMode="Multiple"></asp:ListBox>
                    </div>
                </div>

                <div class="form-group" id="bootstrapTagsInputForm">
                    <asp:Label runat="server" AssociatedControlID="postTags" CssClass="col-lg-2 control-label">Tags</asp:Label>
                    <div class="col-lg-10">
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
        </div>
    </div>
<%--    <script type="text/javascript">
        CKEDITOR.config.htmlEncodeOutput = true;
    </script>--%>
</asp:Content>