$(function () {
    $("#GridView2").toSuperTable({ width: "845px", height: "610px", fixedCols: 4, headerRows: 4 })
            .find("tr:even").addClass("altRow");
});
function getpagebar() {
    var trs = document.getElementsByTagName("tr");

    for (var i = 0; i < trs.length; i++) {
        var t = trs[i];
        if (t.className == "PagerBar1") {
            document.getElementById("pagebottom").appendChild(t.childNodes[1].childNodes[0]);
            if (t.childNodes[1].childNodes[0] != null) t.childNodes[1].childNodes[0].setAttribute("width", 200);
            if (t.childNodes[1].childNodes[0] != null) t.childNodes[1].childNodes[0].setAttribute("style", "width:200");
            break;
        }
    }
}