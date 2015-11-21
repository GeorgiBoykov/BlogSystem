<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Post.aspx.cs" MasterPageFile="~/Site.Master" Inherits="BlogSystem.Web.Post" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="post">
        
        <h2>
            <asp:Label runat="server" ID="postTitle"></asp:Label>
            <asp:HyperLink runat="server" ID="category" CssClass="label label-default mini"></asp:HyperLink>
        </h2>

        <p class="post-content"><asp:Label runat="server" ID="content"></asp:Label></p>
        <div class="attributes">
            <em> — Posted by: 
                <asp:Label runat="server" ID="authorUsername"></asp:Label>
                 on 
                <asp:Label runat="server" ID="dateCreated"></asp:Label>
            </em>
        </div>
        
        <div class="tags">
            <asp:Repeater runat="server" ID="tags">
                <ItemTemplate>
                    <asp:HyperLink runat="server" CssClass="label label-info mini"
                        NavigateUrl='<%# this.Eval("Slug", "../tags/show/{0}") %>'
                         Text='<%# this.Eval("Name") %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        
        <div class="likes">
            <asp:UpdatePanel runat="server" ID="likesPanel" ChildrenAsTriggers="False" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button runat="server"  CssClass="btn btn-primary btn-xs mini" ID="likeBtn" OnClick="likeBtn_OnClick"/>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="comments">
            <asp:UpdatePanel runat="server" ID="commentsPanel" ChildrenAsTriggers="False" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="add-comment">
                        <asp:Label runat="server" AssociatedControlID="addCommentContent">Add comment: </asp:Label><br/>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="addCommentAuthor"
                        CssClass="text-danger" ErrorMessage="The author field is required." Display="Dynamic" ValidationGroup="CommentsValGroup"/>
                        <asp:TextBox runat="server" ID="addCommentAuthor" placeholder="Your name" CssClass="form-control"></asp:TextBox>
                        
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="addCommentContent"
                        CssClass="text-danger" ErrorMessage="The content field is required." Display="Dynamic" ValidationGroup="CommentsValGroup"/>
                        <asp:TextBox runat="server" ID="addCommentContent" placeholder="Comment..." CssClass="form-control" TextMode="MultiLine">
                        </asp:TextBox>
                        <asp:Button runat="server" CssClass="btn-default" ID="addCommentBtn" OnClick="addCommentBtn_OnClick"  ValidationGroup="CommentsValGroup" Text="Submit"/>
                    </div>

                    <div class="comments">
                        <asp:Repeater runat="server" ID="comments">
                            <ItemTemplate>
                                <div class="well comment">
                                    <p> <%# this.Eval("Content") %></p>
                                    <em class="mini"> — By: 
                                        <span><%# this.Eval("Author") %></span>
                                         on 
                                        <span><%# this.Eval("DateCreated", "{0:d}") %></span>
                                    </em>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                </ContentTemplate>
<%--            <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="addCommentBtn" EventName="Click" />
                </Triggers>--%>
            </asp:UpdatePanel>
        </div>

    </div>
</asp:Content>