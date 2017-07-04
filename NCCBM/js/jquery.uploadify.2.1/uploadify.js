
$(document).ready(function () {
    $("#uploadify").uploadify({
        'uploader': "../js/jquery.uploadify.2.1/uploadify.swf",
        'script': 'ZJHandler.ashx',
        'cancelImg': '../js/jquery.uploadify.2.1/cancel.png',
        'folder': '../fujian',
        'queueID': 'fileQueue',
        'auto': true,
        'multi': true,
        'buttonImg': '../js/jquery.uploadify.2.1/button.png',
        'buttonText': '‰Ø¿¿',
        'width': 60,
        'height': 20
    });
});  