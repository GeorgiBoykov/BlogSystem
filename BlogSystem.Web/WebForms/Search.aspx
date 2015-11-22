<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" MasterPageFile="../Site.Master" Inherits="BlogSystem.Web.WebForms.Search" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h2>Search results for "<% this.Response.Write(this.RouteData.Values["term"].ToString()); %>"</h2>
    
    <div class="posts-result">
        <h4>Posts: </h4>
        <asp:Repeater runat="server" ID="postsResults">
            <ItemTemplate>
                <a href="<%# string.Format("/{0}/{1}", this.Eval("Author.UserName"), this.Eval("Slug")) %>" class="btn btn-info">
                    <i class="glyphicon glyphicon-edit"></i>
                    <%# this.Eval("PostTitle") %>
                </a>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <div class="users-result">
        <h4>Users: </h4>
        <asp:Repeater runat="server" ID="usersResults">
            <ItemTemplate>
                <a href="/<%# this.Eval("Username") %>" class="btn btn-info">
                    <i class="glyphicon glyphicon-user"></i>
                    <%# this.Eval("Username") %>
                </a>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>