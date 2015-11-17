<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Site.Master" CodeBehind="Tag.aspx.cs" Inherits="BlogSystem.Web.Tag" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h2>Posts by tag: <asp:Label runat="server" ID="tagName"></asp:Label></h2>
    
    <asp:Repeater runat="server" ID="postsRepeater" >
        <ItemTemplate>
            <asp:HyperLink runat="server" NavigateUrl='<%# this.Eval("PostTitle", "admin/{0}") %>'>
                <blockquote>
                    <h2><%# this.Eval("PostTitle") %></h2>
                    <p class="post-content"><%# this.Eval("Content") %></p>
                    <small><%# this.Eval("Author.UserName") %> at <%# this.Eval("DateCreated") %></small>
                </blockquote>
            </asp:HyperLink>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>