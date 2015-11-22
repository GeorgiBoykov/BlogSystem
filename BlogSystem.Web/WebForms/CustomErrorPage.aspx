<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomErrorPage.aspx.cs" MasterPageFile="~/Site.Master" Inherits="BlogSystem.Web.WebForms.ErrorPage" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h1>Ooops! An error occured:</h1>
    <h2>Error Message: <% this.Response.Write(this.Request.QueryString["ErrorMessage"]); %></h2>
</asp:Content>