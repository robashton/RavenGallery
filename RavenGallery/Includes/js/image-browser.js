imageBrowser = {

    lastTimer: 0,

    initialize: function () {
        imageBrowser.populateImageBrowser(
            getParameterByName('page'),
            getParameterByName('pageSize'),
            getParameterByName('searchTerm'));

        $("#searchText").autocomplete(
        {
            source: function( request, response ) {
                if(request.term.length < 1) { return []; }
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
            select: function(event, ui){
                    imageBrowser.populateImageBrowser(
                    getParameterByName('page'),
                    getParameterByName('pageSize'),
                    ui.item.value);
            },
            minLength: 0,
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

$(document).ready(imageBrowser.initialize);