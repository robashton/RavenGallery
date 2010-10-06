﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>The RavenDB Image Gallery</h2>
    <p>This is an example ASP.NET MVC application using RavenDB, MVC2, StructureMap, Moq and NUnit</p>
        
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
