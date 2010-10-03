function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results == null)
        return "";
    else
        return decodeURIComponent(results[1].replace(/\+/g, " "));
}

imageBrowser = {

    lastTimer: 0,

    initialize: function () {
        imageBrowser.populateImageBrowser(
            getParameterByName('page'),
            getParameterByName('pageSize'),
            getParameterByName('searchTerm'));

        // $('#tabs').tabs();

        $("#searchText").autocomplete(
        {
            source: function( request, response ) {
				$.ajax({
					url: "/Image/_GetTags",
					dataType: "json",
					data: {
						SearchText: request.term
					},
					success: function( data ) {
						response( $.map( data.Items, function( item ) {
							return {
								label: item.Name,
								value: item.Name
							}
						}));
					}
				});
			},
            search: function () {
                    imageBrowser.populateImageBrowser(
                    getParameterByName('page'),
                    getParameterByName('pageSize'),
                    $(this).val());
                },
                       
        });
    },

    populateImageBrowser: function (page, pageSize, searchText) {
        var query = '?page=' + page
                + '&pageSize=' + pageSize
                + '&searchText=' + searchText;

        $.ajax({
            dataType: "json",
            url: '/Image/_GetBrowseData' + query,
            error: function (xhr, ajaxOptions) {
                alert(xhr.status + ':' + xhr.responseText);
            },
            success: function (data) {
                $('#image-browser').html('');
                $('#browsing-image-template')
                    .tmpl(data.Items)
                    .appendTo('#image-browser');

            }
        });
    }
};

$(document).ready(function(){
    imageBrowser.initialize();
})
