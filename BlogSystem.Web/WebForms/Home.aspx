<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" MasterPageFile="../Site.Master" Inherits="BlogSystem.Web.WebForms.Home" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="jumbotron">
        <h2>Welcome <%# this.User.Identity.Name %></h2>
    </div>

    <div class="row">
        <div class="col-md-4">
            <div class="list-group" id="latest-posts">
                <span class="list-group-item active">Latest Posts</span>
                <asp:Repeater runat="server" ID="latestPosts">
                    <ItemTemplate>
                        <a href="<%# string.Format("/{0}/{1}", this.Eval("Author.UserName"), this.Eval("Slug")) %>" class="list-group-item">
                            <i class="glyphicon glyphicon-edit"></i>
                            <%# this.Eval("PostTitle") %>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="col-md-4">
            <div class="list-group" id="latest-comments">
                <span class="list-group-item active">Latest Comments</span>
                <asp:Repeater runat="server" ID="latestComments">
                    <ItemTemplate>
                        <a href="<%# string.Format("/{0}/{1}", this.Eval("Post.Author.UserName"), this.Eval("Post.Slug")) %>" class="list-group-item">
                            <i class="glyphicon glyphicon-edit"></i>
                            <%# this.Eval("Content") %>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="col-md-4">
            <div class="panel panel-primary">
              <div class="panel-heading">
                <h3 class="panel-title">Famous Tags</h3>
              </div>
              <div class="panel-body">
                <asp:Repeater runat="server" ID="famousTags">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" CssClass="btn btn-info btn-xs"
                            NavigateUrl='<%# this.Eval("Slug", "../tags/show/{0}") %>'
                             Text='<%# this.Eval("Name") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:Repeater>
              </div>
            </div>
        </div>

    </div>
</asp:Content>