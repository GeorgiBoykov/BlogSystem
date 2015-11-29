<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Category.aspx.cs" Inherits="BlogSystem.Web.WebForms.Category" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h2>Posts by category: <asp:Label runat="server" ID="categoryName"></asp:Label></h2>
    
     <div class="row">
        <div class="col-md-8">
            <asp:Repeater runat="server" ID="postsRepeater" >
                <ItemTemplate>
                    <blockquote>
                        <asp:HyperLink runat="server" NavigateUrl='<%# "/" + this.Eval("Author.UserName") + "/" + this.Eval("Slug") %>'>
                                <h2><%# this.Eval("PostTitle") %></h2>
                                <p class="post-content"><%# this.Eval("Content") %></p>
                                <small><%# this.Eval("Author.UserName") %> at <%# this.Eval("DateCreated") %></small>
                        </asp:HyperLink>
                    </blockquote>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    
    <div class="row">
        <div class="col-md-12 text-center">
            <ul class="pagination pagination-sm">
                <li><a href="?page=1">&laquo;</a></li>
                <% for (int i = this.CurrentPage - 2 > 0 ? this.CurrentPage - 2 : 1;
                          i <= (this.CurrentPage + 2 > this.PagesCount ? this.PagesCount : this.CurrentPage + 2);
                          i++)
                    {
                        if (this.CurrentPage == i)
                        {
                            this.Response.Write("<li class='active'><a href='?page="+i+"'>"+i+"</a></li>");
                        }
                        else
                        {
                            this.Response.Write("<li><a href='?page="+i+"'>"+i+"</a></li>");
                        }
                    } %>
                <li><a href="?page=<% this.Response.Write(this.PagesCount); %>">&raquo;</a></li>
            </ul>
        </div>
    </div>

</asp:Content>