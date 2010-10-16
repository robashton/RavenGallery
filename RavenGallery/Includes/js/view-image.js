viewImage = {
    initialize: function () {
        var initialImageId = getParameterByName('imageId');
        viewImage.updateView(initialImageId);

        $('#Title').click(viewImage.Title_Clicked);
    },

    updateView: function (imageId) {
        $.ajax({
            dataType: "json",
            url: '/Image/_GetImage?imageId=' + imageId,
            error: function (xhr, ajaxOptions) {
                alert(xhr.status + ':' + xhr.responseText);
            },
            success: function (data) {
                $('#image-placeholder').html('');
                $('#focused-image-template')
                    .tmpl(data)
                    .appendTo('#image-placeholder');

            }
        });
    },

    Title_Clicked: function () {
        
    }
};

$(document).ready(viewImage.initialize);