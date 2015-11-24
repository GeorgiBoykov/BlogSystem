<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" MasterPageFile="../Site.Master" Inherits="BlogSystem.Web.WebForms.Home" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="jumbotron">
        <h3>Welcome <%# this.User.Identity.Name %></h3>
    </div>

    <div class="row">
        <div class="col-md-4">
            <div class="list-group" id="posts-feed">
                <span class="list-group-item active">Posts Feed</span>
                <asp:Repeater runat="server" ID="postsFeed">
                    <ItemTemplate>
                        <a href="<%# string.Format("/{0}/{1}", this.Eval("Author.UserName"), this.Eval("Slug")) %>" class="list-group-item">
                            <i class="glyphicon glyphicon-edit"></i>
                            <%# this.Eval("PostTitle") %>
                            <br/>
                            <em class="mini"> —  
                                <span><%# this.Eval("Author.UserName") %></span>
                                    at 
                                <span><%# this.Eval("DateCreated", "{0:d}") %></span>
                            </em>
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
                            <br/>
                            <em class="mini"> —  
                                <span><%# this.Eval("Author") %></span>
                                    at 
                                <span><%# this.Eval("DateCreated", "{0:d}") %></span>
                            </em>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="col-md-4">
            <div class="panel panel-primary">
              <div class="panel-heading">
                <span class="panel-title">Famous Tags</span>
              </div>
              <div class="panel-body">
                <asp:Repeater runat="server" ID="famousTags">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" CssClass="btn btn-info btn-xs margin-bottom"
                            NavigateUrl='<%# this.Eval("Slug", "../tags/show/{0}") %>'
                             Text='<%# this.Eval("Name") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:Repeater>
              </div>
            </div>
        </div>

    </div>
</asp:Content>