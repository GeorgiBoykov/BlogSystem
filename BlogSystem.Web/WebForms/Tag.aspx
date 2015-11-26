<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Tag.aspx.cs" Inherits="BlogSystem.Web.WebForms.Tag" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h2>Posts by tag: <asp:Label runat="server" ID="tagName"></asp:Label></h2>
    
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
</asp:Content>