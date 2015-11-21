﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Category.aspx.cs" Inherits="BlogSystem.Web.WebForms.Category" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h2>Posts by category: <asp:Label runat="server" ID="categoryName"></asp:Label></h2>
    
    <asp:Repeater runat="server" ID="postsRepeater" >
        <ItemTemplate>
            <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("../{0}/{1}", this.Eval("Author.UserName"), this.Eval("Slug")) %>'>
                <blockquote>
                    <h2><%# this.Eval("PostTitle") %></h2>
                    <p class="post-content"><%# this.Eval("Content") %></p>
                    <small><%# this.Eval("Author.UserName") %> at <%# this.Eval("DateCreated") %></small>
                </blockquote>
            </asp:HyperLink>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>