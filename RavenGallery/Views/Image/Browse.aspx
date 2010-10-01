<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ImageBrowseView>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Browse
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Browse Images</h2>
    <p>Page <%: Model.Page %>, showing <%: Model.PageSize %> items per page</p>
    
    <div class="image-browser">
        <%foreach(var item in Model.Items){ %>
        <div class="browsing-image">
                <h4><%: item.Title %></h4>
                <img src="<%= this.ResolveUrl(String.Format("/Resources/Image/{0}", Url.Encode(item.Filename))) %>" alt="<%: item.Title %>" />
        </div>
        <%} %>
    </div>

</asp:Content>
