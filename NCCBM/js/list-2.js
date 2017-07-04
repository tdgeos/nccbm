
$(function () {
    $("#GridView2").toSuperTable({ width: "845px", height: "315px", fixedCols: 4, headerRows: 2 })
    .find("tr:even").addClass("altRow");
});

$(function () {
    $("#gridView").toSuperTable({ width: "845px", height: "315px", fixedCols: 4, headerRows: 2 })
    .find("tr:even").addClass("altRow");
});
$(function () {
    $("#gridView_zj").toSuperTable({ width: "845px", height: "315px", fixedCols: 4, headerRows: 2 })
            .find("tr:even").addClass("altRow");
    $("#gridView_xtg").toSuperTable({ width: "845px", height: "291px", fixedCols: 4, headerRows: 1 })
            .find("tr:even").addClass("altRow");
    $("#gridView_gj").toSuperTable({ width: "845px", height: "315px", fixedCols: 4, headerRows: 2 })
            .find("tr:even").addClass("altRow");
    $("#gridView_wj").toSuperTable({ width: "845px", height: "315px", fixedCols: 4, headerRows: 2 })
            .find("tr:even").addClass("altRow");
    $("#gridView_scjwjtjb").toSuperTable({ width: "845px", height: "315px", fixedCols: 4, headerRows: 2 })
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
