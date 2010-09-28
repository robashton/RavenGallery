<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
%>
        Welcome <b><%= Html.Encode(Page.User.Identity.Name) %></b>!
        [ <%= Html.ActionLink("Sign Off", "SignOut", "User") %> ]
<%
    }
    else {
%> 
        [ <%= Html.ActionLink("Sign In", "SignIn", "User")%> ]
<%
    }
%>

