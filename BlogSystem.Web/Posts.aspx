<%@ Page Title="Posts" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Posts.aspx.cs" Inherits="BlogSystem.Web.Posts" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Repeater runat="server" ID="postsRepeater" >
        <ItemTemplate>
            <asp:HyperLink runat="server" NavigateUrl='<%# this.Eval("Id", "Post.aspx?id={0}") %>'>
                <blockquote>
                    <h2><%# this.Eval("PostTitle") %></h2>
                    <p class="post-content"><%# this.Eval("Content") %></p>
                    <small><%# this.Eval("Author.UserName") %> at <%# this.Eval("DateCreated") %></small>
                </blockquote>
            </asp:HyperLink>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
