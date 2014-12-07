
function ValidateSearch() {
    if (!require_txt("txtSearchBox", "Bạn chưa nhập từ khóa")) return false;
    key = removeHTMLTags("txtSearchBox");
    window.location = '/Pages/SearchResult.aspx?key=' + key;
    return false;
}
function TDTEnterPressSearch(e) {
    var characterCode;
    if (e && e.which)
    { e = e; characterCode = e.which; }
    else
    { e = window.event; characterCode = e.keyCode; }
    if (characterCode == 13)
    { ValidateSearch(); return false; }
    return true;
}
function removeHTMLTags(ctrID) {

    var strInputCode = document.getElementById(ctrID).value;

    strInputCode = strInputCode.replace(/&(lt|gt);/g, function (strMatch, p1) {
        return (p1 == "lt") ? "<" : ">";
    });
    var strTagStrippedText = strInputCode.replace(/<\/?[^>]+(>|$)/g, "");
    while (strTagStrippedText.indexOf('"') != -1)
        strTagStrippedText = strTagStrippedText.replace('"', '');
    //    while (strTagStrippedText.indexOf('\'') != -1)
    //        strTagStrippedText = strTagStrippedText.replace('\'', '');
    document.getElementById(ctrID).value = strTagStrippedText;
    return strTagStrippedText;



}

function require_txt(control, msg) {
    if (document.getElementById(control).value == "") {
        alert(msg);
        document.getElementById(control).focus();
        return false;
    }
    return true;
}
