<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<UserSignInViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Logon
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Logon</h2>
     <%using (Html.BeginForm())
      { %>

      <ul>
        <li><%=Html.LabelFor(x=>x.Username) %> <%= Html.TextBoxFor(x => x.Username) %> <%= Html.ValidationMessageFor(model => Model.Username)%></li>
        <li><%=Html.LabelFor(x => x.Password)%> <%= Html.PasswordFor(x => x.Password) %> <%= Html.ValidationMessageFor(model => Model.Password)%></li>
        <li><%=Html.LabelFor(x => x.StayLoggedIn)%> <%= Html.CheckBoxFor(x => x.StayLoggedIn) %> <%= Html.ValidationMessageFor(model => Model.StayLoggedIn)%></li>
      </ul>    

      <input type="submit" />
      
    <%} %>

</asp:Content>
