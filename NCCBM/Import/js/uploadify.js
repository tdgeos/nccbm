
$(document).ready(function () {
    $("#uploadify").uploadify({
        'uploader': "js/uploadify.swf",
        'script': 'FujianHandler.ashx',
        'cancelImg': 'js/cancel.png',
        'folder': '../fujian',
        'queueID': 'fileQueue',
        'auto': false,
        'multi': true,
        'buttonImg': 'js/button.png',
        'buttonText': '‰Ø¿¿',
        'width': 60,
        'height': 20
    });
});  