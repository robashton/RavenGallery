<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ImageBrowseView>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Browse Images
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Browse Images</h2>

    <label for="searchText">Search</label> <input type="text" id="searchText" name="searchText" />

    <script id="browsing-image-template" type="text/x-jquery-tmpl">
        <div class="browsing-image">
             <h4>${Title}</h4>
             <img src="/Resources/Image/${Filename}" alt={Title}" />
        </div>    
    </script>
    <div id="image-browser">
    </div>

</asp:Content>
