<%@ Page Title="Blog" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Blog.aspx.cs" Inherits="BlogSystem.Web.WebForms.Blog" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center">
        <asp:Label runat="server" ID="blogOwner"></asp:Label>
        <asp:Button runat="server" ID="follow" 
            CssClass="btn btn-success btn-xs" Text="Follow" OnClick="follow_OnClick"></asp:Button>
    </h2>
    
    <div class="row">
        <div class="col-md-9">
            <asp:Repeater runat="server" ID="postsRepeater" >
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# "/" + this.Eval("Author.UserName") + "/" + this.Eval("Slug") %>'>
                        <blockquote>
                            <h2><%# this.Eval("PostTitle") %></h2>
                            <p class="post-content"><%# this.Eval("Content") %></p>
                            <small><%# this.Eval("Author.UserName") %> at <%# this.Eval("DateCreated") %></small>
                        </blockquote>
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        
        <div class="col-md-3">
            <div class="list-group" id="user-followers">
                <span class="list-group-item active"><i class="glyphicon glyphicon-user" aria-hidden="true"></i> Followers</span>
                <asp:Repeater runat="server" ID="followers">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" NavigateUrl='<%# "/" + this.Eval("Username") %>'
                             CssClass="list-group-item" Text='<%# this.Eval("Username") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div class="list-group" id="user-following-users">
                <span class="list-group-item active"><i class="glyphicon glyphicon-user" aria-hidden="true"></i> Following</span>
                <asp:Repeater runat="server" ID="following">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" NavigateUrl='<%# "/" + this.Eval("Username") %>'
                             CssClass="list-group-item" Text='<%# this.Eval("Username") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
