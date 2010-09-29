<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ImageNewViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	New
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Upload Image</h2>
    <form method="post" enctype="multipart/form-data">

        <ul>
            <li><%=Html.LabelFor(x=>x.Title) %> <%= Html.TextBoxFor(x=>x.Title) %> <%=Html.ValidationMessageFor(x=>x.Title) %></li>
            <li><%=Html.LabelFor(x=>x.Tags) %> <%= Html.TextBox("Tags", string.Join(", ", Model.Tags)) %> <%=Html.ValidationMessageFor(x=>x.Tags) %></li>
            <li><%=Html.Label("File") %> <input type="file" name="File" /> <%=Html.ValidationMessage("File") %> </li>
        </ul>      

        <input type="submit" />
    </form>

</asp:Content>
