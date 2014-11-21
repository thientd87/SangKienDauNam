function showLoading() {
    $("#imgloading").show();
    //        $("#bgFilter").show();
}
function hideLoading() {
    setTimeout(function () {
        $("#imgloading").hide();
        //            $("#bgFilter").hide();
    }, 100);
}

var ie = window.navigator.appName == 'Microsoft Internet Explorer';
var endRequest = null;
var filterEnable = true;
// get lowest parent node of child node
function getParentFromChild(childNode, parentTag) {
    var i = 0;
    while (childNode.nodeName.toLowerCase() != parentTag.toLowerCase() && (i++) < 10) childNode = childNode.parentNode;
    return childNode;
}

// set selected value of dropdownlist
function selectDropDownList(cbo, selectedValue) {
    for (var i = 0; i < cbo.options.length; i++) {
        if (cbo.options[i].value == selectedValue) {
            cbo.options[i].selected = true;
            return;
        }
    }
}

// set attribute for a node, cross-browser
function setAttribute(element, attributeName, attributeValue) {
    // firefox
    element.setAttribute(attributeName, attributeValue);
    // ie
    for (var i = 0; i < element.attributes.length; i++) {
        if (element.attributes[i].name.toLowerCase() == attributeName.toLowerCase() && attributeName != 'style') {
            element.attributes[i].value = attributeValue;
            break;
        }
    }

}

// convert number input => width style
function parseWidth(width) {
    if (width == null || width == '' || width == 'auto' || width == '?') return 'auto';

    if (width.indexOf('%') == -1 && width.indexOf('px') == -1)
        return width + 'px';
    else {
        return width;
    }
}

// validate width for style
// right format: 56%, 56px, 56, auto
function validateWidth(width) {
    if (width == null || width == '' || width == 'auto') return true;

    width = width.replace('%', '');
    width = width.replace('px', '');

    return !isNaN(width);
}

// trap enter key
// ev: event
// el: element fired event
function doBlur(ev, el) {
    var key;

    if (window.event)
        key = window.event.keyCode;     //IE
    else
        key = ev.which;     //firefox

    if (key == 13) {
        el.blur();
        return false;
    }
}

//
function setFloat(element, floatValue) {
    if (ie)
        element.style.styleFloat = floatValue;
    else
        element.style.cssFloat = floatValue;
}


// 
function getChidlNode(parentNode, childTag, index) {
    var childs = parentNode.childNodes;
    var count = 0;
    for (var i = 0; i < childs.length; i++) {
        if (childs[i].nodeName.toLowerCase() == childTag) {
            if (count == index) return childs[i];
            count++;
        }
    }
}

//
function getChildsOfParent(parentNode, childTag) {
    var toReturn = new Array();
    var childs = parentNode.childNodes;
    for (var i = 0; i < childs.length; i++) {
        if (childs[i].nodeName.toLowerCase() == childTag) {
            toReturn.push(childs[i]);
        }
    }
    return toReturn;
}
function getOffsetLeft(el) {
    var offset = 0;
    while (el != document.documentElement) {
        offset += el.offsetLeft;
        el = el.parentNode;
    }
    return offset;
}
function getOffsetTop(el) {
    var offset = 0;
    while (el != document.documentElement) {
        offset += el.offsetTop;
        if (el.nodeName.toLowerCase() == 'td')
            el = el.parentNode;
        el = el.parentNode;
    }
    return offset;
}
function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function eraseCookie(name) {
    createCookie(name, "", -1);
}
function findPosX(obj) {
    var curleft = 0;
    if (obj.offsetParent)
        while (1) {
            curleft += obj.offsetLeft;
            if (!obj.offsetParent)
                break;
            obj = obj.offsetParent;
        }
    else if (obj.x)
        curleft += obj.x;
    return curleft;
}

function findPosY(obj) {
    var curtop = 0;
    if (obj.offsetParent)
        while (1) {
            curtop += obj.offsetTop;
            if (!obj.offsetParent)
                break;
            obj = obj.offsetParent;
        }
    else if (obj.y)
        curtop += obj.y;
    return curtop;
}


function trimstr(str) { return str.replace(/^\s+|\s+$/g, ''); }

function openpreview(sUrl, w, h) {
    var winX = 0;
    var winY = 0;
    if (parseInt(navigator.appVersion) >= 4) {
        winX = (screen.availWidth - w) * .5;
        winY = (screen.availHeight - h) * .5;
    }
    var newWindow = window.open(sUrl, '', 'scrollbars,resizable=yes,status=yes,addressbar=no, width=' + w + ',height=' + h + ',left=' + winX + ',top=' + winY);
}
function getNodeByClass(parentNode, xpath) {
    var tagName = xpath.split('.')[0];
    var className = xpath.split('.')[1];

    var node, nodes = parentNode.getElementsByTagName(tagName);
    for (var i = 0; i < nodes.length; i++) {
        node = nodes[i];
        if (node.className.toLowerCase() == className.toLowerCase()) return node;
    }
    return null;
}

function setValueDropdownlist(cbo, value) {

    if (cbo && value) {
        value = trimstr(value);
        value = value.split(' ')[0];
        for (var i = 0; i < cbo.options.length; i++) {
            if (cbo.options[i].value == value) cbo.selectedIndex = i;
        }
    }
}

function hideAllDropDown() {
    var i = 0;
    var elm = document.getElementsByTagName('select')
    for (i = 0; i < elm.length; i++) {
        if (elm[i].type.toLowerCase() == 'select-one') {
            elm[i].style.display = 'none';
        }
    }
}
function showAllDropDown() {
    var i = 0;
    var elm = document.forms[0].elements;
    for (i = 0; i < elm.length; i++) {
        if (elm[i].type && elm[i].type.toLowerCase() == 'select-one') {
            elm[i].style.display = '';
        }
    }
}

/*Back End*/
function input_init() {
    if (typeof (Sys) != 'undefined') {
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(global_BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(global_EndRequestHandler);
    }
}
function trapEnterKey(e, id) {
    if (!e) e = window.event;

    if (e && e.keyCode == 13) {
        document.getElementById(id).click();
        return false;
    }
    return true;
}
ie ? window.attachEvent('onload', input_init) : window.addEventListener('load', input_init, false);

function global_BeginRequestHandler(sender, args) {
    if (filterEnable) {
        showFilter();
        hideAllDropDown();
    }

    //var loading = document.getElementById("imgloading");
    //var top = 0;
    //var left = 0;
    //left = (document.documentElement && document.documentElement.scrollLeft) ? document.documentElement.scrollLeft : document.body.scrollLeft;
    //top = (document.documentElement && document.documentElement.scrollTop) ? document.documentElement.scrollTop : document.body.scrollTop;
    //loading.style.display = 'block';
    //loading.style.top = top + 'px';
    //loading.style.left = left + 'px';

}
function global_EndRequestHandler(sender, args) {
    if (filterEnable) {
        hideFilter();
        showAllDropDown();
    }
    if (endRequest) {
        eval(endRequest);
    }
    endRequest = null;
    filterEnable = true;

    //var loading = document.getElementById("imgloading");
    //loading.style.display = 'none';
    showAllDropDown();
}
/*het back end*/
/* Common */
var ie = window.navigator.appName == 'Microsoft Internet Explorer';

var PageHost = 'http://vnexpress.net';
function AddHeader(Name, Header, Buttons, Symbol, AddChildTable) {
    document.writeln('<div class=BreakLine id="IDM_', Name, '">');
    if (typeof (AddChildTable) == 'undefined') {
        document.writeln('<table class="OutsiderBox" align=center width="100%" cellspacing=0 cellpadding=0 border=0>');
        LastChild = 1;
    }
    else {
        LastChild = 0;
    }
    return true;
}

function AddFooter() {
    if (LastChild) {
        document.writeln('</table></div>');
    }
    else {
        document.writeln('</div>');
    }
}
function setEditorValue(html) {
    if (!ie) {
        oUtil.obj.saveForUndo();
        var obj = oUtil.obj;
        var oEditor = oUtil.oEditor;
        var sHTML = html;
        sHTML = sHTML.replace(/>\s+</gi, "><"); //replace space between tag
        sHTML = sHTML.replace(/\r/gi, ""); //replace space between tag
        sHTML = sHTML.replace(/(<br>)\s+/gi, "$1"); //replace space between BR and text
        sHTML = sHTML.replace(/(<br\s*\/>)\s+/gi, "$1"); //replace space between <BR/> and text. spasi antara <br /> menyebebkan content menggeser kekanan saat di apply
        sHTML = sHTML.replace(/\s+/gi, " "); //replace spaces with space
        oEditor.document.body.innerHTML = obj.docType + sHTML;
        obj.cleanDeprecated();
    }
    else {
        oUtil.obj.saveForUndo();
        var obj = oUtil.obj;
        var sBodyContent = html;
        sHTML = obj.docType + sBodyContent;
        obj.putHTML(sHTML);
    }
}
function ItemMinMax(Name, ControlButton) {
    var MItem = document.all(Name);
    var ImgItem = document.all(ControlButton);
    if (MItem.style.display == '') {
        MItem.style.display = 'none';
        ImgItem.src = 'Images/Weather/min.gif';
    }
    else {
        MItem.style.display = '';
        ImgItem.src = 'Images/Weather/max.gif';
    }
}

function opnwd(url) {
    popupWin = window.open(url, 'new_page', 'toolbar=no,location=no,menubar=no,scrollbars=yes,width=500,height=460,top=40,left=130,resizeable=yes,status=no');
}

function OpenNewWindow(url, wndName, attr) {
    /*var winX = (screen.availWidth - w)*.5;
    var winY = (screen.availHeight - h)*.5;*/
    var _newWindow = window.open(url, wndName, attr);
    return _newWindow;
}

//Ham dung de phong to anh
function openImageNews(vLink, ImgSrc) {
    var sLink = (typeof (vLink.href) == 'undefined') ? vLink : vLink.href;
    var newImg, vHeight = 0, vWidth = 0;
    if (sLink == '') {
        return false;
    }
    newImg = new Image();
    newImg.src = ImgSrc;
    vWidth = newImg.width;
    vHeight = newImg.height;
    newImg = null;

    if (vWidth == 0 || vHeight == 0) return false;

    winDef = 'status=no,resizable=no,scrollbars=no,toolbar=no,location=no,fullscreen=no,titlebar=yes,height='.concat(vHeight).concat(',').concat('width=').concat(vWidth).concat(',');
    winDef = winDef.concat('top=').concat((screen.height - vHeight) / 2).concat(',');
    winDef = winDef.concat('left=').concat((screen.width - vWidth) / 2);
    newwin = open('', '_blank', winDef);

    newwin.document.writeln('<title>qdnd.vn</title><body topmargin="0" leftmargin="0" marginheight="0" marginwidth="0">');
    newwin.document.writeln('<a href="" onClick="window.close(); return false;"><img src="/NQL/', ImgSrc, '" alt="', 'Dong lai', '" border=0></a>');
    newwin.document.writeln('</body>');

    if (typeof (vLink.href) != 'undefined') {
        return false;
    }
}

// Used by Paging Section
function changePage(form) {
    form.submit();
}

// Use POST method to exec a link
// form : form id, this form
// ItemID : Hidden element ID
// ID : ID Value
function FollowLink(form, ItemID, ID) {
    ItemID.value = ID;
    form.submit();
}

// Use POST method to exec a link with two Parameter
// form : form id, this form
// ItemID : Hidden element ID
// ID : ID Value
// SecondItemID :Second Hidden element ID
// SecondID : Second ID Value
function FollowLink2P(form, ItemID, SecondItemID, ID, SecondID) {
    ItemID.value = ID;
    SecondItemID.value = SecondID;
    form.submit();
}

function openInfo(url, width, height) {
    var attri;
    attri = 'width=' + width + ',height=' + height + ',resize=yes ,scrollbars=yes,status=yes,toolbar=yes,top=0,left=0';
    var popwin = window.open(url, 'Page_' + width, attri);
    return;
}


/************************************************
Makes all Yes/No entries on the form visible
*/
function showAllComboBox() {
    var elements = document.getElementsByTagName("select");
    var i;
    for (i = 0; i < elements.length; i++) {
        elements[i].style.visibility = "visible";
    }
}



//ham sho va hide
function show(id) {
    var _objDiv = document.all[id];
    if (_objDiv.className == "Hidde") {

        _objDiv.className = "Show";

    }
}

function hidde(id) {
    var _objDiv = document.getElementById(id);

    if (_objDiv != null && _objDiv.className != null && _objDiv.className == "Show") {

        _objDiv.className = "Hidde";

    }
}

function changetheImage(sPath, sImage) {
    var Iimage = document.all["Picture"];
    Iimage.src = sPath + sImage;
    var intHeight, intWidth, intScale;
    intWidth = Iimage.width;
    intHeight = Iimage.height;
    intScale = intWidth / intHeight;
    if (intWidth > 300) {
        Iimage.width = 300;
        Iimage.height = 300 * intScale;
    }
}

function changeImage(sPath, sImage) {
    var objArr = sImage.split(".");
    var ext = objArr[objArr.length - 1];
    if ((ext == "jpg") || (ext == "gif") || (ext == "png")) {
        show("imgArea");
        hidde("flashArea");
        var Iimage = document.all["Picture"];
        Iimage.src = "getImage.aspx?filename=" + sPath + sImage + "&width=200";
    }
    if (ext == "swf") {
        show("flashArea");
        hidde("imgArea");
        var _objDiv = document.all("flashArea");
        _objDiv.innerHTML = "<embed src=" + sPath + sImage + " width=200>";
    }
}

function getFileOnly(sValue) {
    var sResult = "";
    var i = sValue.lastIndexOf('/') + 1;
    if (sValue != '')
        sResult = sValue.substring(i);
    document.all["PictureName"].innerText = sResult;
    return sResult;
}
function setHiddenField(sValue) {
    document.all["PictureName"].innerText = getFileOnly(sValue);
    document.all["hddPicture"].value = sValue;
}

function SelectImage(aFolder) {
    var objSelect = document.getElementsByTagName("select");
    var sValue = "";

    if (objSelect != null)
        sValue = objSelect[1].options[objSelect[1].selectedIndex].value;
    window.opener.document.getElementById("hddPicture").value = "Images/" + aFolder + "/" + sValue;
    window.opener.document.getElementById("Picture").src = "Images/" + aFolder + "/" + sValue;
    window.opener.document.getElementById("PictureName").innerText = sValue;
    window.close();
}

function ShowWindow(sUrl, w, h) {
    var winX = 0;
    var winY = 0;
    if (parseInt(navigator.appVersion) >= 4) {
        winX = (screen.availWidth - w) * .5;
        winY = (screen.availHeight - h) * .5;
    }
    popupLoadnWin = window.open(sUrl, 'popupLoadnWin', 'scrollbars,resizable=no,status=yes, width=' + w + ',height=' + h + ',left=' + winX + ',top=' + winY);

}

function Height_Width(obj) {

    if (obj.value != "") {
        var Iimage = document.all["Picture"];
        var img = new Image();
        img.src = obj.value;
        document.all["Height"].value = img.height;
        document.all["Width"].value = img.width;

        Iimage.src = obj.value;
    }

}

function Filechange(obj) {
    var Iimage = document.all["Picture"];
    Iimage.src = obj.value;

}

var _blnShow = false;
function comment() {
    var _objDiv = document.all["comment"];
    if (_objDiv.className == "commentHidden") {

        _objDiv.className = "commentVisible";

    }
    else {
        _objDiv.className = "commentHidden";
    }
}

function ChildImage(_strAuthor) {
    var objSelect = document.getElementsByTagName("select");
    var sValue = "";
    if (objSelect != null)
        sValue = objSelect[1].options[objSelect[1].selectedIndex].text;

    var fulparth = window.location.href;
    var hostname = fulparth.substring(0, fulparth.indexOf("MediaHelper.aspx"));
    window.opener.document.getElementById("inpImgURL").value = hostname + "images/" + _strAuthor + "/" + sValue;
    self.close();
}

function ShowListCat(sName, sNameStyle) {
    var _objDiv = document.all[sName];
    _objDiv.className = sNameStyle;
}
function CheckAccount(oldpass, hiddenPass, newpass) {
    var result;
    if (oldpass == "") {
        alert('Bạn phải nhập vào ');
        result = false;
    }
    if (oldpass != hiddenPass) {
        alert('Bạn đã nhập sai password cữ, xin mời nhập lại');
        result = false;
    }
    if (newpass == "") {
        alert('Mời bạn nhập vào password mới');
        result = false;
    }
    if (oldpass != newpass) {
        alert('Mật khẩu chưa được xác nhận đúng, mời nhập lại');
        result = false;
    }
    return result;
}

function doGetModuleTitle() {
    var res = DFISYS.Ultility.ShowInvidualModule.GetModuleTitle();

    p = res.value;
    alert(p);
}

function doGetModule(tabref, moduleref, callback) {
    var res = DFISYS.Ultility.ShowInvidualModule.LoadModuleContent(tabref, moduleref);

    var p = res.value;

    alert(p);
}

function ShowModule_CallBack(response) {
    if (response.error != null) {
        alert("Error : " + response.error);
        return;
    }
    alert("Value : " + response.value);
}
function popup(wURL, wTitle, wFeature) {
    mywin = window.open(wURL, wTitle, wFeature);
}
function slow(ele) {
    ele.scrollAmount = 1;
}
function fast(ele) {
    ele.scrollAmount = 2;
}
// window media
function Playfile(sfile) {
    document.forms[0].mediaplayer.URL = sfile;
}
function PlayformUrl() {
    var strUrl = window.location.href;
    if (strUrl.indexOf("#mms") > 0) {
        var strMms = strUrl.split("#")[1];
        //alert("url="+strUrl+"mms="+strMms);
        Playfile(strMms);
    }
}
function uMouseMove() {
    document.forms[0].test.value = "B&#7845;m &#273;úp &#273;&#7875; xem toàn màn hình.";
}

function uMouseOut() {
    document.forms[0].test.value = "T&#7843;i ph&#7847;n m&#7873;m xem ch&#432;&#417;ng trình.";
}

function uMouseDown() {
    document.forms[0].test.hideFocus = true;
}

/*voi votes*/
function doVote(id, frm) {
    /*var ml=document.vote;
    ml.action="site/usercontrols/vote_result.aspx?id="+id;
    ml.submit();*/
    var val = document.forms[0].R1;
    var Rval = getCheckedValue(val);
    OpenNewWindow('Ultility/ResultSelect.aspx?edittype=' + id + '&pollid=' + Rval, 'myWind', 'width=500, height=350, resize=0, scrollbars=1');
    //javascript:window.open('site/usercontrols/vote_result.aspx?id='+id+'&R1='+Rval, '', 'toolbar=0,scrollbars=0,location=0,statusbar=0,menubar=0,resizable=0,top=200,left=200,width=400,height=300');	
}
function getCheckedValue(radioObj) {
    if (!radioObj)
        return "";
    var radioLength = radioObj.length;
    if (radioLength == undefined)
        if (radioObj.checked)
            return radioObj.value;
        else
            return "";
    for (var i = 0; i < radioLength; i++) {
        if (radioObj[i].checked) {
            return radioObj[i].value;
        }
    }
    return "";
}

/*doSearch*/
function doSearch(txtSearch) {
    var text = document.all[txtSearch].value;
    //alert('obj='+text);
    window.location.href = 'searching.aspx?s=&q=' + escape(text) + '&m=0';
}

/*Duongna scripts*/
String.prototype.trim = function () {
    return this.replace(/^\s+|\s+$/g, "");
}
String.prototype.ltrim = function () {
    return this.replace(/^\s+/, "");
}
String.prototype.rtrim = function () {
    return this.replace(/\s+$/, "");
}
// Get control bằng tên control
function GetControlByName(name) {
    return document.getElementById(document.getElementById('hidIdPrefix').value + name);
}
//Kiểm tra giá trị cần
function CheckRequire(controlToCheck, nameOfControl) {
    var controlToValidate;
    controlToValidate = GetControlByName(controlToCheck);
    if (controlToValidate.value.trim().length == 0) {
        alert(nameOfControl + " không được để trống!");
        controlToValidate.focus();
        return false;
    }
    else {
        return true;
    }
}
//Kiểm tra Email
function EmailCheck(controlName) {
    var emailStr = GetControlByName(controlName).value;
    if (emailStr.trim().length == 0)
        return true;
    var emailPat = /^(.+)@(.+)$/
    var specialChars = "\\(\\)<>@,;:\\\\\\\"\\.\\[\\]"
    var validChars = "\[^\\s" + specialChars + "\]"
    var quotedUser = "(\"[^\"]*\")"
    var ipDomainPat = /^\[(\d{1,3})\.(\d{1,3})\.(\d{1,3})\.(\d{1,3})\]$/
    var atom = validChars + '+'
    var word = "(" + atom + "|" + quotedUser + ")"
    var userPat = new RegExp("^" + word + "(\\." + word + ")*$")
    var domainPat = new RegExp("^" + atom + "(\\." + atom + ")*$")
    var matchArray = emailStr.match(emailPat)
    if (matchArray == null) {
        alert("Email address seems incorrect (check @ and .'s)");
        GetControlByName(controlName).focus();
        return false
    }
    var user = matchArray[1]
    var domain = matchArray[2]

    // See if "user" is valid 
    if (user.match(userPat) == null) {
        // user is not valid
        alert("The username doesn't seem to be valid.");
        GetControlByName(controlName).focus();
        return false
    }

    /* if the e-mail address is at an IP address (as opposed to a symbolic
    host name) make sure the IP address is valid. */
    var IPArray = domain.match(ipDomainPat)
    if (IPArray != null) {
        // this is an IP address
        for (var i = 1; i <= 4; i++) {
            if (IPArray[i] > 255) {
                alert("Destination IP address is invalid!");
                GetControlByName(controlName).focus();
                return false
            }
        }
        return true
    }

    // Domain is symbolic name
    var domainArray = domain.match(domainPat)
    if (domainArray == null) {
        alert("The domain name doesn't seem to be valid.")
        GetControlByName(controlName).focus();
        return false
    }

    var atomPat = new RegExp(atom, "g")
    var domArr = domain.match(atomPat)
    var len = domArr.length
    if (domArr[domArr.length - 1].length < 2 ||
    domArr[domArr.length - 1].length > 3) {
        // the address must end in a two letter or three letter word.
        alert("The address must end in a three-letter domain, or two letter country.")
        GetControlByName(controlName).focus();
        return false
    }

    // Make sure there's a host name preceding the domain.
    if (len < 2) {
        var errStr = "This address is missing a hostname!"
        alert(errStr)
        return false
    }

    // If we've gotten this far, everything's valid!
    return true;
}
//Chỉ nhập số
function NumberBox(e) {
    var keynum;
    e = e ? e : window.event;
    keynum = e.keyCode;

    if (keynum < 48 || keynum > 57) {
        e.cancelBlur = false;
        e.returnValue = false;
    }
}
//không cho phép nhập 1 số ký tự đặc biệt
function CheckUserName(e) {
    var keynum;
    e = e ? e : window.event;
    keynum = e.keyCode;

    if (!((keynum >= 48 && keynum <= 57) || (keynum >= 97 && keynum <= 122) || (keynum >= 65 && keynum <= 90) || keynum == 45 || keynum == 95)) {
        e.cancelBlur = false;
        e.returnValue = false;
    }
}

function GetQueryString(value) {
    hu = window.location.search.substring(1);
    gy = hu.split("&");
    for (i = 0; i < gy.length; i++) {
        ft = gy[i].split("=");
        if (ft[0] == value) {
            return ft[1];
        }
    }
}

// vote module
function vote_Ketqua(id) {
    openpreview('/ViewVote.aspx?vid=' + id, 400, 300);
}
function vote_Vote(node, id) {
    var table = node.parentNode.parentNode.getElementsByTagName('table')[0];
    var radios = table.getElementsByTagName('input');
    var strId = '';
    for (var i = 0; i < radios.length; i++)
        if (radios[i].checked) strId += ',' + radios[i].value;

    openpreview('/ViewVote.aspx?vid=' + id + '&aid=' + strId, 400, 300);
}

function showPopup(controlID, gotoTop) {

    var control = document.getElementById(controlID);
    if (control) {
        control.style.display = 'block';
        var top = (window.screen.availHeight - control.offsetHeight) / 2 - 100;
        var left = (document.documentElement.offsetWidth - control.offsetWidth) / 2;
        left += (document.documentElement && document.documentElement.scrollLeft) ? document.documentElement.scrollLeft : document.body.scrollLeft;
        top += (document.documentElement && document.documentElement.scrollTop) ? document.documentElement.scrollTop : document.body.scrollTop;

        control.style.left = left + 'px';

        if (typeof (gotoTop) != 'undefined') top -= gotoTop;
        control.style.top = top + 'px';

        return true;
    }
    return false;
}
function showFilter() {
    var bg = document.getElementById('bgFilter');
    if (bg) {
        bg.style.display = 'block';
        var height = document.documentElement.scrollHeight;
        height = height > window.screen.height ? height : window.screen.height;
        bg.style.height = height + 'px';
    }
}
function hideFilter() {
    var bg = document.getElementById('bgFilter');
    if (bg) {
        bg.style.display = 'none';
    }
}
function showModalPopup(controlID, isShowBG, gotoTop) {
    var b = false;
    if (typeof (gotoTop) == 'undefined')
        b = showPopup(controlID);
    else
        b = showPopup(controlID, gotoTop);


    if (typeof (isShowBG) == 'undefined')
        var isShowBG = true;

    if (b && isShowBG) {
        var bg = document.getElementById('bgFilter');
        if (bg) {
            //hideAllDropDown();
            bg.style.display = 'block';
            var tbl = document.getElementById('maintable');
            var height = window.screen.availHeight;
            if (tbl) {
                height = height > tbl.offsetHeight ? height : tbl.offsetHeight;
            }

            bg.style.height = height + 'px';
        }
    }
}
function hideModalPopup(controlID) {
    document.getElementById(controlID).style.display = 'none';
    var bgFilter = document.getElementById('bgFilter');
    if (bgFilter) bgFilter.style.display = 'none';
    showAllDropDown();
}

function countCheckItem(parentid, name) {
    var chks = document.getElementById(parentid).getElementsByTagName('input');

    var count = 0;
    for (var i = 1; i < chks.length; i++) {
        if (chks[i].type.toLowerCase() == 'checkbox' && chks[i].id.indexOf(name) >= 0 && chks[i].checked) {
            count++;
        }
    }
    return count;
}

function tonggle(parentID, checked, name) {
    var grid = document.getElementById(parentID);
    var chks = grid.getElementsByTagName('input')

    var tr, inactiveClass;
    for (var i = 1; i < chks.length; i++) {
        if (chks[i].type.toLowerCase() == 'checkbox' && chks[i].id.indexOf(name) > -1) {
            chks[i].checked = checked;
            tr = chks[i].parentNode;
            while (tr.nodeName.toLowerCase() != 'tr') tr = tr.parentNode;
            setClassTr(tr, checked);
        }
    }
}
function setClassTr(tr, checked) {
    while (tr.nodeName.toLowerCase() != 'tr') tr = tr.parentNode;
    var inactiveClass = tr.getAttribute('_inactive');
    tr.className = checked ? 'active2' : (inactiveClass ? inactiveClass : '');
}
function selectRow(e) {
    var tr = e.parentNode.parentNode;
    tr.className = e.checked ? 'active2' : tr.getAttribute('_inactive');
}
function animate_scrollToTop() {
    window.scrollTo(0, 0);
}

function genPreviewhtml(path) {
    var dotIndex = path.lastIndexOf('.');
    var extension = path.substr(dotIndex + 1).toLowerCase();

    var html = '';

    if (extension == 'gif' || extension == 'jpg' || extension == 'bmp' || extension == 'png') {
        html = '<img src="' + path + '" />';
    }
    else if (extension == 'swf') {
        html = '<object codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000">' +
					'<param value="sameDomain" name="allowScriptAccess"/>' +
					'<param value="' + path + '" name="movie"/>' +
					'<param value="high" name="quality"/>' +
					'<embed pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" allowscriptaccess="sameDomain" quality="high" src="' + path + '"/>' +
					'</object>';
    }
    else if (extension == 'flv') {
        html = '<embed type="application/x-shockwave-flash" src="/AssetManager/CustomObjects/mediaplayer.swf" style="" id="single" name="single" quality="high" allowfullscreen="true" flashvars="file=' + path + '&amp;width=365&amp;height=255&amp;autostart=false&amp;wmode=window&amp;logo=logoK14.png" height="255" width="365">';
    }
    else if (extension == 'mp3' || extension == 'wmv' || extension == 'wma') {

        var WMP7;
        var videoHeight = ' height="300" ';
        var videoWidth = 'width="300" ';
        var link = path;
        try {
            if (navigator.appName != "Netscape") {
                WMP7 = new ActiveXObject('WMPlayer.OCX');
            }
        }
        catch (error) {
            ;
        }


        // Windows Media Player 7 Code
        if (WMP7) {
            html += ('<OBJECT ' + videoHeight + ' ' + videoWidth + ' classid="clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6" VIEWASTEXT>');
            html += ('<PARAM NAME="URL" VALUE="' + link + '">');
            html += ('<param name="autostart" value="true">');
            html += ('<param name="showcontrols" value="true">');
            html += ('<param name="playcount" value="9999">');
            html += ('<param name="autorewind" value="1"><param name="wmode" value="opaque" />');
            html += ('<PARAM NAME="Volume" VALUE="100">');
            html += ('</OBJECT>');
        }

        // Windows Media Player 6.4 Code
        else {
            html += ('<OBJECT  classid="CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95" ');
            html += ('codebase="http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,4,5,715" ');
            html += (' ' + videoHeight + ' ' + videoWidth + ' ');
            html += ('standby="Loading Microsoft Windows Media Player components..." ');
            html += ('type="application/x-oleobject" VIEWASTEXT> ');
            html += ('<PARAM NAME="FileName"           VALUE="' + link + '">');
            html += ('<PARAM NAME="TransparentAtStart" Value="false">');
            html += ('<PARAM NAME="AutoStart"          Value="true">');
            html += ('<PARAM NAME="AnimationatStart"   Value="false">');
            html += ('<PARAM NAME="ShowControls"       Value="false">');
            html += ('<PARAM NAME="ShowDisplay"	 value ="false">');
            html += ('<PARAM NAME="playCount" VALUE="999">');
            html += ('<PARAM NAME="displaySize" 	 Value="0"><param name="wmode" value="opaque" />');
            html += ('<PARAM NAME="Volume" VALUE="100">');
            html += ('<Embed type="application/x-mplayer2" ');
            html += ('pluginspage= ');
            html += ('"http://www.microsoft.com/Windows/MediaPlayer/" ');
            html += ('src="' + link + '" ');
            html += ('Name=MediaPlayer  wmode="transparent"');
            html += ('transparentAtStart=0 ');
            html += ('autostart=1 ');
            html += ('playcount=999 ');
            html += ('volume=100');
            html += ('animationAtStart=0 ');
            html += (' ' + videoHeight + ' ' + videoWidth + ' ');
            html += ('displaySize=0></embed> ');
            html += ('</OBJECT> ');
        }

    }
    return html;
}

function server_getElementById(id) {
    if (typeof (prefix) != 'undefined') {
        return document.getElementById(prefix + '_' + id);
    }
    else {
        return document.getElementById(id);
    }
}
/*Het common*/


function menu_logHref(href) {
    if (href.indexOf('.aspx') == -1) href += '.aspx';
    createCookie('ChannelVN.menu', href, 1);
}
function menu_onload() {
    // neu box khong co item thi an di
    var box, boxes = document.getElementsByTagName('li');
    for (var i = 0; i < boxes.length; i++) {
        box = boxes[i];
        if (box.className == 'box') {
            if (box.getElementsByTagName('li').length == 0)
                box.style.display = 'none';
        }
    }


    if (window.location.href.indexOf('office.aspx') == -1) {
        var href = readCookie('ChannelVN.menu');
        if (href && href != '') {
            var a, as, ul, uls = document.getElementsByTagName('ul');
            for (var i = 0; i < uls.length; i++) {
                ul = uls[i];
                if (ul.className == 'menu') {
                    as = ul.getElementsByTagName('a');
                    if (overLoadMenuItem != '') {
                        overLoadMenuItem += '.aspx';
                        for (var j = 0; j < as.length; j++) {
                            a = as[j];
                            if (a.href.indexOf(overLoadMenuItem) > 0) {
                                a.className = 'menuactive';
                                return;
                            }
                        }
                    }
                    for (var j = 0; j < as.length; j++) {
                        a = as[j];
                        if (a.href == window.location.href) {
                            a.className = 'menuactive';
                            return;
                        }
                    }
                    for (var j = 0; j < as.length; j++) {
                        a = as[j];
                        if (a.href.indexOf(href) > 0) {
                            a.className = 'menuactive';
                            return;
                        }
                    }
                }
            }
        }
    }
}
ie ? window.attachEvent('onload', menu_onload) : window.addEventListener('load', menu_onload, false);

function chooseFileMNG(type, txtID) {
    txtID = document.getElementById(txtID).value;
    ShowFileMNG('/GUI/EditoralOffice/MainOffce/FileManager/default.aspx?function=' + type + '_loadValue&mode=single&share=share&i=' + encodeURIComponent(txtID), 900, 700);
}


function ShowFileMNG(sUrl, w, h) {
    var winX = 0;
    var winY = 0;
    if (parseInt(navigator.appVersion) >= 4) {
        winX = (screen.availWidth - w) * .5;
        winY = (screen.availHeight - h) * .5;
    }
    var newWindow = window.open(sUrl, '', 'scrollbars,resizable=yes,status=yes, width=' + w + ',height=' + h + ',left=' + winX + ',top=' + winY);
}

/*
function insertMultipleImage_loadValue(arrImagesURL) {
var html = '';

if (document.all) {
editor.insertHTML(html);
}
else {
editor.insertHTML(html);
}
}*/
/**
* A Javascript object to encode and/or decode html characters using HTML or Numeric entities that handles double or partial encoding
* Author: R Reid
* source: http://www.strictly-software.com/htmlencode
* Licences: GPL, The MIT License (MIT)
* Copyright: (c) 2011 Robert Reid - Strictly-Software.com
*
* Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
* 
* Revision:
*  2011-07-14, Jacques-Yves Bleau: 
*       - fixed conversion error with capitalized accentuated characters
*       + converted arr1 and arr2 to object property to remove redundancy
*/

Encoder = {

    // When encoding do we convert characters into html or numerical entities
    EncodeType: "entity",  // entity OR numerical

    isEmpty: function (val) {
        if (val) {
            return ((val === null) || val.length == 0 || /^\s+$/.test(val));
        } else {
            return true;
        }
    },
    arr1: new Array('&nbsp;', '&iexcl;', '&cent;', '&pound;', '&curren;', '&yen;', '&brvbar;', '&sect;', '&uml;', '&copy;', '&ordf;', '&laquo;', '&not;', '&shy;', '&reg;', '&macr;', '&deg;', '&plusmn;', '&sup2;', '&sup3;', '&acute;', '&micro;', '&para;', '&middot;', '&cedil;', '&sup1;', '&ordm;', '&raquo;', '&frac14;', '&frac12;', '&frac34;', '&iquest;', '&Agrave;', '&Aacute;', '&Acirc;', '&Atilde;', '&Auml;', '&Aring;', '&Aelig;', '&Ccedil;', '&Egrave;', '&Eacute;', '&Ecirc;', '&Euml;', '&Igrave;', '&Iacute;', '&Icirc;', '&Iuml;', '&ETH;', '&Ntilde;', '&Ograve;', '&Oacute;', '&Ocirc;', '&Otilde;', '&Ouml;', '&times;', '&Oslash;', '&Ugrave;', '&Uacute;', '&Ucirc;', '&Uuml;', '&Yacute;', '&THORN;', '&szlig;', '&agrave;', '&aacute;', '&acirc;', '&atilde;', '&auml;', '&aring;', '&aelig;', '&ccedil;', '&egrave;', '&eacute;', '&ecirc;', '&euml;', '&igrave;', '&iacute;', '&icirc;', '&iuml;', '&eth;', '&ntilde;', '&ograve;', '&oacute;', '&ocirc;', '&otilde;', '&ouml;', '&divide;', '&Oslash;', '&ugrave;', '&uacute;', '&ucirc;', '&uuml;', '&yacute;', '&thorn;', '&yuml;', '&quot;', '&amp;', '&lt;', '&gt;', '&oelig;', '&oelig;', '&scaron;', '&scaron;', '&yuml;', '&circ;', '&tilde;', '&ensp;', '&emsp;', '&thinsp;', '&zwnj;', '&zwj;', '&lrm;', '&rlm;', '&ndash;', '&mdash;', '&lsquo;', '&rsquo;', '&sbquo;', '&ldquo;', '&rdquo;', '&bdquo;', '&dagger;', '&dagger;', '&permil;', '&lsaquo;', '&rsaquo;', '&euro;', '&fnof;', '&alpha;', '&beta;', '&gamma;', '&delta;', '&epsilon;', '&zeta;', '&eta;', '&theta;', '&iota;', '&kappa;', '&lambda;', '&mu;', '&nu;', '&xi;', '&omicron;', '&pi;', '&rho;', '&sigma;', '&tau;', '&upsilon;', '&phi;', '&chi;', '&psi;', '&omega;', '&alpha;', '&beta;', '&gamma;', '&delta;', '&epsilon;', '&zeta;', '&eta;', '&theta;', '&iota;', '&kappa;', '&lambda;', '&mu;', '&nu;', '&xi;', '&omicron;', '&pi;', '&rho;', '&sigmaf;', '&sigma;', '&tau;', '&upsilon;', '&phi;', '&chi;', '&psi;', '&omega;', '&thetasym;', '&upsih;', '&piv;', '&bull;', '&hellip;', '&prime;', '&prime;', '&oline;', '&frasl;', '&weierp;', '&image;', '&real;', '&trade;', '&alefsym;', '&larr;', '&uarr;', '&rarr;', '&darr;', '&harr;', '&crarr;', '&larr;', '&uarr;', '&rarr;', '&darr;', '&harr;', '&forall;', '&part;', '&exist;', '&empty;', '&nabla;', '&isin;', '&notin;', '&ni;', '&prod;', '&sum;', '&minus;', '&lowast;', '&radic;', '&prop;', '&infin;', '&ang;', '&and;', '&or;', '&cap;', '&cup;', '&int;', '&there4;', '&sim;', '&cong;', '&asymp;', '&ne;', '&equiv;', '&le;', '&ge;', '&sub;', '&sup;', '&nsub;', '&sube;', '&supe;', '&oplus;', '&otimes;', '&perp;', '&sdot;', '&lceil;', '&rceil;', '&lfloor;', '&rfloor;', '&lang;', '&rang;', '&loz;', '&spades;', '&clubs;', '&hearts;', '&diams;'),
    arr2: new Array('&#160;', '&#161;', '&#162;', '&#163;', '&#164;', '&#165;', '&#166;', '&#167;', '&#168;', '&#169;', '&#170;', '&#171;', '&#172;', '&#173;', '&#174;', '&#175;', '&#176;', '&#177;', '&#178;', '&#179;', '&#180;', '&#181;', '&#182;', '&#183;', '&#184;', '&#185;', '&#186;', '&#187;', '&#188;', '&#189;', '&#190;', '&#191;', '&#192;', '&#193;', '&#194;', '&#195;', '&#196;', '&#197;', '&#198;', '&#199;', '&#200;', '&#201;', '&#202;', '&#203;', '&#204;', '&#205;', '&#206;', '&#207;', '&#208;', '&#209;', '&#210;', '&#211;', '&#212;', '&#213;', '&#214;', '&#215;', '&#216;', '&#217;', '&#218;', '&#219;', '&#220;', '&#221;', '&#222;', '&#223;', '&#224;', '&#225;', '&#226;', '&#227;', '&#228;', '&#229;', '&#230;', '&#231;', '&#232;', '&#233;', '&#234;', '&#235;', '&#236;', '&#237;', '&#238;', '&#239;', '&#240;', '&#241;', '&#242;', '&#243;', '&#244;', '&#245;', '&#246;', '&#247;', '&#248;', '&#249;', '&#250;', '&#251;', '&#252;', '&#253;', '&#254;', '&#255;', '&#34;', '&#38;', '&#60;', '&#62;', '&#338;', '&#339;', '&#352;', '&#353;', '&#376;', '&#710;', '&#732;', '&#8194;', '&#8195;', '&#8201;', '&#8204;', '&#8205;', '&#8206;', '&#8207;', '&#8211;', '&#8212;', '&#8216;', '&#8217;', '&#8218;', '&#8220;', '&#8221;', '&#8222;', '&#8224;', '&#8225;', '&#8240;', '&#8249;', '&#8250;', '&#8364;', '&#402;', '&#913;', '&#914;', '&#915;', '&#916;', '&#917;', '&#918;', '&#919;', '&#920;', '&#921;', '&#922;', '&#923;', '&#924;', '&#925;', '&#926;', '&#927;', '&#928;', '&#929;', '&#931;', '&#932;', '&#933;', '&#934;', '&#935;', '&#936;', '&#937;', '&#945;', '&#946;', '&#947;', '&#948;', '&#949;', '&#950;', '&#951;', '&#952;', '&#953;', '&#954;', '&#955;', '&#956;', '&#957;', '&#958;', '&#959;', '&#960;', '&#961;', '&#962;', '&#963;', '&#964;', '&#965;', '&#966;', '&#967;', '&#968;', '&#969;', '&#977;', '&#978;', '&#982;', '&#8226;', '&#8230;', '&#8242;', '&#8243;', '&#8254;', '&#8260;', '&#8472;', '&#8465;', '&#8476;', '&#8482;', '&#8501;', '&#8592;', '&#8593;', '&#8594;', '&#8595;', '&#8596;', '&#8629;', '&#8656;', '&#8657;', '&#8658;', '&#8659;', '&#8660;', '&#8704;', '&#8706;', '&#8707;', '&#8709;', '&#8711;', '&#8712;', '&#8713;', '&#8715;', '&#8719;', '&#8721;', '&#8722;', '&#8727;', '&#8730;', '&#8733;', '&#8734;', '&#8736;', '&#8743;', '&#8744;', '&#8745;', '&#8746;', '&#8747;', '&#8756;', '&#8764;', '&#8773;', '&#8776;', '&#8800;', '&#8801;', '&#8804;', '&#8805;', '&#8834;', '&#8835;', '&#8836;', '&#8838;', '&#8839;', '&#8853;', '&#8855;', '&#8869;', '&#8901;', '&#8968;', '&#8969;', '&#8970;', '&#8971;', '&#9001;', '&#9002;', '&#9674;', '&#9824;', '&#9827;', '&#9829;', '&#9830;'),

    // Convert HTML entities into numerical entities
    HTML2Numerical: function (s) {
        return this.swapArrayVals(s, this.arr1, this.arr2);
    },

    // Convert Numerical entities into HTML entities
    NumericalToHTML: function (s) {
        return this.swapArrayVals(s, this.arr2, this.arr1);
    },


    // Numerically encodes all unicode characters
    numEncode: function (s) {

        if (this.isEmpty(s)) return "";

        var e = "";
        for (var i = 0; i < s.length; i++) {
            var c = s.charAt(i);
            if (c < " " || c > "~") {
                c = "&#" + c.charCodeAt() + ";";
            }
            e += c;
        }
        return e;
    },

    // HTML Decode numerical and HTML entities back to original values
    htmlDecode: function (s) {

        var c, m, d = s;

        if (this.isEmpty(d)) return "";

        // convert HTML entites back to numerical entites first
        d = this.HTML2Numerical(d);

        // look for numerical entities &#34;
        arr = d.match(/&#[0-9]{1,5};/g);

        // if no matches found in string then skip
        if (arr != null) {
            for (var x = 0; x < arr.length; x++) {
                m = arr[x];
                c = m.substring(2, m.length - 1); //get numeric part which is refernce to unicode character
                // if its a valid number we can decode
                if (c >= -32768 && c <= 65535) {
                    // decode every single match within string
                    d = d.replace(m, String.fromCharCode(c));
                } else {
                    d = d.replace(m, ""); //invalid so replace with nada
                }
            }
        }

        return d;
    },

    // encode an input string into either numerical or HTML entities
    htmlEncode: function (s, dbl) {

        if (this.isEmpty(s)) return "";

        // do we allow double encoding? E.g will &amp; be turned into &amp;amp;
        dbl = dbl || false; //default to prevent double encoding

        // if allowing double encoding we do ampersands first
        if (dbl) {
            if (this.EncodeType == "numerical") {
                s = s.replace(/&/g, "&#38;");
            } else {
                s = s.replace(/&/g, "&amp;");
            }
        }

        // convert the xss chars to numerical entities ' " < >
        s = this.XSSEncode(s, false);

        if (this.EncodeType == "numerical" || !dbl) {
            // Now call function that will convert any HTML entities to numerical codes
            s = this.HTML2Numerical(s);
        }

        // Now encode all chars above 127 e.g unicode
        s = this.numEncode(s);

        // now we know anything that needs to be encoded has been converted to numerical entities we
        // can encode any ampersands & that are not part of encoded entities
        // to handle the fact that I need to do a negative check and handle multiple ampersands &&&
        // I am going to use a placeholder

        // if we don't want double encoded entities we ignore the & in existing entities
        if (!dbl) {
            s = s.replace(/&#/g, "##AMPHASH##");

            if (this.EncodeType == "numerical") {
                s = s.replace(/&/g, "&#38;");
            } else {
                s = s.replace(/&/g, "&amp;");
            }

            s = s.replace(/##AMPHASH##/g, "&#");
        }

        // replace any malformed entities
        s = s.replace(/&#\d*([^\d;]|$)/g, "$1");

        if (!dbl) {
            // safety check to correct any double encoded &amp;
            s = this.correctEncoding(s);
        }

        // now do we need to convert our numerical encoded string into entities
        if (this.EncodeType == "entity") {
            s = this.NumericalToHTML(s);
        }

        return s;
    },

    // Encodes the basic 4 characters used to malform HTML in XSS hacks
    XSSEncode: function (s, en) {
        if (!this.isEmpty(s)) {
            en = en || true;
            // do we convert to numerical or html entity?
            if (en) {
                s = s.replace(/\'/g, "&#39;"); //no HTML equivalent as &apos is not cross browser supported
                s = s.replace(/\"/g, "&quot;");
                s = s.replace(/</g, "&lt;");
                s = s.replace(/>/g, "&gt;");
            } else {
                s = s.replace(/\'/g, "&#39;"); //no HTML equivalent as &apos is not cross browser supported
                s = s.replace(/\"/g, "&#34;");
                s = s.replace(/</g, "&#60;");
                s = s.replace(/>/g, "&#62;");
            }
            return s;
        } else {
            return "";
        }
    },

    // returns true if a string contains html or numerical encoded entities
    hasEncoded: function (s) {
        if (/&#[0-9]{1,5};/g.test(s)) {
            return true;
        } else if (/&[A-Z]{2,6};/gi.test(s)) {
            return true;
        } else {
            return false;
        }
    },

    // will remove any unicode characters
    stripUnicode: function (s) {
        return s.replace(/[^\x20-\x7E]/g, "");

    },

    // corrects any double encoded &amp; entities e.g &amp;amp;
    correctEncoding: function (s) {
        return s.replace(/(&amp;)(amp;)+/, "$1");
    },


    // Function to loop through an array swaping each item with the value from another array e.g swap HTML entities with Numericals
    swapArrayVals: function (s, arr1, arr2) {
        if (this.isEmpty(s)) return "";
        var re;
        if (arr1 && arr2) {
            //ShowDebug("in swapArrayVals arr1.length = " + arr1.length + " arr2.length = " + arr2.length)
            // array lengths must match
            if (arr1.length == arr2.length) {
                for (var x = 0, i = arr1.length; x < i; x++) {
                    re = new RegExp(arr1[x], 'g');
                    s = s.replace(re, arr2[x]); //swap arr1 item with matching item from arr2	
                }
            }
        }
        return s;
    },

    inArray: function (item, arr) {
        for (var i = 0, x = arr.length; i < x; i++) {
            if (arr[i] === item) {
                return i;
            }
        }
        return -1;
    }

}

//example of using the html encode object

//// set the type of encoding to numerical entities e.g & instead of &
//Encoder.EncodeType = "numerical";

//// or to set it to encode to html entities e.g & instead of &
//Encoder.EncodeType = "entity";

//// HTML encode text from an input element
//// This will prevent double encoding.
//var encoded = Encoder.htmlEncode(document.getElementById('input'));

//// To encode but to allow double encoding which means any existing entities such as
//// &amp; will be converted to &amp;amp;
//var dblEncoded = Encoder.htmlEncode(document.getElementById('input'),true);

//// Decode the now encoded text
//var decoded = Encoder.htmlDecode(encoded);

//// Check whether the text still contains HTML/Numerical entities
//var containsEncoded = Encoder.hasEncoded(decoded);
/**
*
*  Base64 encode / decode
*  http://www.webtoolkit.info/
*
**/

var Base64 = {

    // private property
    _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",

    // public method for encoding
    encode: function (input) {
        var output = "";
        var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
        var i = 0;

        input = Base64._utf8_encode(input);

        while (i < input.length) {

            chr1 = input.charCodeAt(i++);
            chr2 = input.charCodeAt(i++);
            chr3 = input.charCodeAt(i++);

            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;

            if (isNaN(chr2)) {
                enc3 = enc4 = 64;
            } else if (isNaN(chr3)) {
                enc4 = 64;
            }

            output = output +
			this._keyStr.charAt(enc1) + this._keyStr.charAt(enc2) +
			this._keyStr.charAt(enc3) + this._keyStr.charAt(enc4);

        }

        return output;
    },

    // public method for decoding
    decode: function (input) {
        var output = "";
        var chr1, chr2, chr3;
        var enc1, enc2, enc3, enc4;
        var i = 0;

        input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

        while (i < input.length) {

            enc1 = this._keyStr.indexOf(input.charAt(i++));
            enc2 = this._keyStr.indexOf(input.charAt(i++));
            enc3 = this._keyStr.indexOf(input.charAt(i++));
            enc4 = this._keyStr.indexOf(input.charAt(i++));

            chr1 = (enc1 << 2) | (enc2 >> 4);
            chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
            chr3 = ((enc3 & 3) << 6) | enc4;

            output = output + String.fromCharCode(chr1);

            if (enc3 != 64) {
                output = output + String.fromCharCode(chr2);
            }
            if (enc4 != 64) {
                output = output + String.fromCharCode(chr3);
            }

        }

        output = Base64._utf8_decode(output);

        return output;

    },

    // private method for UTF-8 encoding
    _utf8_encode: function (string) {
        string = string.replace(/\r\n/g, "\n");
        var utftext = "";

        for (var n = 0; n < string.length; n++) {

            var c = string.charCodeAt(n);

            if (c < 128) {
                utftext += String.fromCharCode(c);
            }
            else if ((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            }
            else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }

        }

        return utftext;
    },

    // private method for UTF-8 decoding
    _utf8_decode: function (utftext) {
        var string = "";
        var i = 0;
        var c = c1 = c2 = 0;

        while (i < utftext.length) {

            c = utftext.charCodeAt(i);

            if (c < 128) {
                string += String.fromCharCode(c);
                i++;
            }
            else if ((c > 191) && (c < 224)) {
                c2 = utftext.charCodeAt(i + 1);
                string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                i += 2;
            }
            else {
                c2 = utftext.charCodeAt(i + 1);
                c3 = utftext.charCodeAt(i + 2);
                string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                i += 3;
            }

        }

        return string;
    }

}