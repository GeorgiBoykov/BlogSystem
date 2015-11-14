<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Post.aspx.cs" MasterPageFile="Site.Master" Inherits="BlogSystem.Web.Post" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="post">
        
        <h2>
            <asp:Label runat="server" ID="postTitle"></asp:Label>
            <asp:Label CssClass="label label-default mini" runat="server" ID="category"></asp:Label>
        </h2>

        <p class="post-content"><asp:Label runat="server" ID="content"></asp:Label></p>
        <div class="attributes">
            <em> — Posted by: 
                <asp:Label runat="server" ID="author"></asp:Label>
                 on 
                <asp:Label runat="server" ID="dateCreated"></asp:Label>
            </em>
        </div>

        <div class="tags">
            <asp:Repeater runat="server" ID="tags">
                <ItemTemplate>
                    <asp:HyperLink runat="server" CssClass="label label-info mini"
                        NavigateUrl='<%# this.Eval("Name", "Tag.aspx?name={0}") %>'
                         Text='<%# this.Eval("Name") %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <div id="add-comment">
            <asp:Label runat="server" AssociatedControlID="addComment">Add comment: </asp:Label>
            
            <asp:TextBox runat="server" ID="commentAuthor" placeholder="Your name" CssClass="form-control"></asp:TextBox>
            <asp:TextBox runat="server" ID="addComment" placeholder="Comment..." CssClass="form-control" TextMode="MultiLine">
            </asp:TextBox>
            <asp:Button runat="server" CssClass="btn-default" ID="addCommentBtn" OnClick="addCommentBtn_OnClick" Text="Submit"/>
        </div>

        <div class="comments">
            <asp:Repeater runat="server" ID="comments">
                <ItemTemplate>
                    <div class="well">
                        <p> <%# this.Eval("Content") %></p>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

    </div>
</asp:Content>
