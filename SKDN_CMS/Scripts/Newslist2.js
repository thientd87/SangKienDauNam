// bacth, 10:30 AM 7/11/2008
var contextMenuInterval = null;
var linkpreview = '/preview/default.aspx?news='; 
function newslist_cboIsHot_selectedIndexChange(e) {
    var newsID = 0;
    var tr = e.parentNode;
    while (tr.nodeName.toLowerCase() != 'tr') tr = tr.parentNode;
    newsID = tr.getElementsByTagName('input')[0].value;
    hdArgs.value = newsID + ',' + e.options[e.selectedIndex].value;
    commandName = 'setloaitin';
    __doPostBack(btnSetLoaiTin.name, '');
}
function newslist_chkIsFocus_CheckedChanged(e) {
    var newsID = 0;
    var tr = e.parentNode;
    while (tr.nodeName.toLowerCase() != 'tr') tr = tr.parentNode;
    newsID = tr.getElementsByTagName('input')[0].value;
    hdArgs.value = newsID + ',' + e.checked;
    commandName = 'settieudiem';

    __doPostBack(btnSetTieuDiem.name, '');
}
function newslist_setstyleforcheckbox() {
    /*var chk, chks =  document.getElementById(grdListNewsID).getElementsByTagName('input');
    var i = 0, count = chks.length;
    var td;
    for (i=1; i<count; i++)
    {
    chk = chks[i];
    if (chk.type.toLowerCase() == 'checkbox')
    {
    td = chk.parentNode; while (td.nodeName.toLowerCase() != 'td') td = td.parentNode;
    if (chk.checked)
    td.className = 'active'; 
    else
    td.className = '';
			
    }
    }*/
}

function newslist_removeActiveRow(sender, eventArgs) {
    if (eventArgs.get_error() != undefined && eventArgs.get_error().httpStatusCode == '500') {
        var errorMessage = eventArgs.get_error().message;
        eventArgs.set_errorHandled(true);
        alert(errorMessage);
        return;
    }
    document.getElementById('contextmenu').style.display = 'none';
    switch (commandName) {
        case 'xoatam':
            tr.parentNode.removeChild(tr); break;
        case 'xuatban':
            tr.parentNode.removeChild(tr); break;
        case 'xoathat':
            tr.parentNode.removeChild(tr); break;
        case 'guilen':
            tr.parentNode.removeChild(tr); break;
        case 'trave':
            tr.parentNode.removeChild(tr); break;
        case 'gobo':
            tr.parentNode.removeChild(tr); break;
        case 'selectedRow':
            var chks = document.getElementById(grdListNewsID).getElementsByTagName('input');
            var trActive = null;
            var trs = new Array();
            var i = 0;
            for (i = 1; i < chks.length; i++) {
                if (chks[i].type.toLowerCase() == 'checkbox' && chks[i].id.indexOf('chkSelect') >= 0 && chks[i].checked) {
                    trActive = chks[i].parentNode;
                    while (trActive.nodeName.toLowerCase() != 'tr') trActive = trActive.parentNode;
                    trs.push(trActive);
                }
            }
            if (trs.length > 0) {
                var trBody = trs[0].parentNode;
                for (i = 0; i < trs.length; i++) trBody.removeChild(trs[i]);
            }
            break;
        case 'settieudiem': break;
        case 'setloaitin': break;
        case 'chamnhuanbut': break;
        //default: animate_scrollToTop();  
            break;
    }
    commandName = null;
}


function newslist_grid_clickitem(ev, a, newsID, new_mode) {
    hdNewsID.value = newsID;
    var hidNewsMode = document.getElementById('hidNewsMode');
    if (hidNewsMode) hidNewsMode.value = new_mode;
    var contextmenu = document.getElementById('contextmenu');
    contextmenu.style.display = 'block';
    contextMenuInterval = setTimeout('document.getElementById(\'contextmenu\').style.display = \'none\';', 5000);
    $(".xemtruoc").attr("href", linkpreview + newsID);
    
    var left = findPosX(a) + a.offsetWidth - 2 * contextmenu.offsetWidth;
    var top = findPosY(a) + a.offsetHeight - 50;

    contextmenu.style.top = top + 'px';
    contextmenu.style.left = left + 'px';

    tr = a.parentNode;
    while (tr.nodeName.toLowerCase() != 'tr') tr = tr.parentNode;
}

function newslist_setstylefordropdownlist() {
   
}
function checkMultiAction(action) {
    var chks = document.getElementById(grdListNewsID).getElementsByTagName('input');

    commandName = 'selectedRow';

    var newsIDs = new Array();

    for (var i = 1; i < chks.length; i++) {
        if (chks[i].type.toLowerCase() == 'checkbox' && chks[i].id.indexOf('chkSelect') >= 0 && chks[i].checked) {
            newsIDs.push(chks[i].value);
        }
    }

    if (newsIDs.length == 0) {
        alert('Bạn chưa chọn tin');
        return false;
    }

    hdNewsID.value = newsIDs.join(',');
    hdArgs.value = newsIDs.join(',');

    switch (action) {
        case 'send': return confirm('Bạn có muốn gửi tin đã chọn hay không?'); break;
        case 'sendback': return confirm('Bạn có muốn trả lại bài viết đã chọn hay không?'); break;
        case 'approved': return confirm('Bạn có muốn xuất bản tin đã chọn hay không?'); break;
        case 'disapproved': return confirm('Bạn có muốn gỡ tin đã chọn hay không?'); break;
        case 'delete': return confirm('Bạn có muốn xóa tin đã chọn hay không?'); break;
    }
    return false;
}

function setInactiveClass(tblID) {
    var trs = document.getElementById(grdListNewsID).getElementsByTagName('tr');
    for (var i = 1; i < trs.length; i++) {
        if (trs[i].className) trs[i].setAttribute('_inactive', trs[i].className);
    }
}

function CloseComment() {
    document.getElementById('divShowComment').style.display = 'none';
    document.getElementById("bgFilter").style.display = 'none';
}
function ClosePop() {
    document.getElementById('loginForm').style.display = 'none';
    document.getElementById("bgFilter").style.display = 'none';
}
function newslist_mouseup(evt) {
    document.getElementById('contextmenu').style.display = 'none';

    evt = evt || window.event;
    var tar = evt.target || evt.srcElement;

    if (tar.nodeName.toLowerCase() == 'td' && tar.parentNode.parentNode.parentNode.id == grdListNewsID) {
        var chks = tar.getElementsByTagName('input');
        for (var i = 0; i < chks.length; i++) {
            if (chks[i].type.toLowerCase() == 'checkbox') {
                chks[i].checked = !chks[i].checked;
                if (chks[i].id.indexOf('chkSelect') >= 0)
                    tar.parentNode.className = chks[i].checked ? 'active2' : tar.parentNode.getAttribute('_inactive'); // select row
                else if (chks[i].id.indexOf('chkIsFocus') >= 0)
                    newslist_chkIsFocus_CheckedChanged(chks[i]); // set tieu diem
            }
        }
    }
    if (contextMenuInterval) {
        clearTimeout(contextMenuInterval);
        contextMenuInterval = null;
    }
    return false;
}



function editnews(news_id) {
    hdNewsID.value = news_id
    suanoidung();
    return false;
}
function suanoidung() {
    var hr = window.location.href.substring(window.location.href.lastIndexOf("/") + 1, window.location.href.length).replace(".aspx", "").replace('#a', '');
    hr = hr.replace('#', '');
    if (hr.indexOf('sendlist') >= 0)
        return;
    var Page = "add," + hr + "/" + hdNewsID.value + ".aspx?source=" + window.location.href;
    if (hr.indexOf('office') >= 0)
        Page = hr + "/add/" + hdNewsID.value + ".aspx?source=" + window.location.href;
    window.location = Page;
}
//*****************************************************************************************//
// context menu for news list
function copynews() {
    if (hdNewsID != null && !isNaN(Number(hdNewsID.value)))
        openpreview(linkpreview + hdNewsID.value, 900, 700);
}

function xemtruoc() {
    if (hdNewsID != null && !isNaN(Number(hdNewsID.value)))
        openpreview(linkpreview + hdNewsID.value, 900, 700);
}

 
function gobo() {
    if (confirm('Bạn có muốn gỡ bỏ bài này?')) {
        hdArgs.value = hdNewsID.value + ',7';
        __doPostBack(btnUpdateStatus.name, '');
        var hidNewsMode = document.getElementById('hidNewsMode');
        if (hidNewsMode && hidNewsMode.value == "1") { alert('Bạn vừa gỡ bài nổi bật mục vì vậy bạn phải chọn 1 bài nổi bật mục khác thay thế bài vừa gỡ'); return; }
        commandName = 'gobo';
    }
}
function xuatban() {
    hdArgs.value = hdNewsID.value + ',3';
    __doPostBack(btnUpdateStatus.name, '');
    commandName = 'xuatban';
}
function xemthongtinbandocgui() {
    openpreview('/ThongTinBanDocGui.aspx?NewsID=' + hdNewsID.value, 400, 300);
}
function xemnhanxet() {
    openpreview('/CommentNewsReturn.aspx?News_ID=' + hdNewsID.value, 600, 230);
}
function trave() {
    showModalPopup('feedbackform');
    hdID.value = hdNewsID.value;
    commandName = 'trave';
}
function xoatam() {

    if (confirm('Bạn có muốn xóa tạm tin đã chọn hay không?')) {
        hdArgs.value = hdNewsID.value + ',6';
        __doPostBack(btnUpdateStatus.name, '');
        commandName = 'xoatam';
    }
}
function xoathat() {
    if (confirm('Bạn có muốn xóa tin đã chọn hay không?')) {
        hdArgs.value = hdNewsID.value
        __doPostBack(btnDeletePermanently.name, '');
        commandName = 'xoathat';
    }
}
function guilen() {
    hdArgs.value = hdNewsID.value + ',' + (isSendDirectly ? 2 : 1); // neu duoc gui truc tiep thi => cho duyet, neu ko => bien tap
    commandName = 'guilen';
    __doPostBack(btnUpdateStatus.name, '');
}
function init_contextmenu() {
    var url = window.location.href;
    if (url.indexOf('publishedlist.aspx') > 0)
        showcontextmenu(new Array(0, 1, 2, 3, 10));
    else if (url.indexOf('removedlist.aspx') > 0)
        showcontextmenu(new Array(0, 1, 5, 7, 8));
    else if (url.indexOf('templist.aspx') > 0)
        showcontextmenu(new Array(0, 1, 5, 6));
    else if (url.indexOf('editwaitlist.aspx') > 0)
        showcontextmenu(new Array(0, 1, 5, 7, 6));
    else if (url.indexOf('editinglist.aspx') > 0)
        showcontextmenu(new Array(0, 1, 5, 7, 6));
    else if (url.indexOf('approvalwaitlist.aspx') > 0)
        showcontextmenu(new Array(0, 1, 5, 7, 6));
    else if (url.indexOf('dellist.aspx') > 0)
        showcontextmenu(new Array(0, 1, 9));
    else if (url.indexOf('backlist.aspx') > 0)
        showcontextmenu(new Array(0, 1, 4, 5, 6));
    else if (url.indexOf('approvinglist.aspx') > 0)
        showcontextmenu(new Array(0, 1, 5, 7, 6));
    else if (url.indexOf('sendlist.aspx') > 0 || url.indexOf('sendapprovallist.aspx') > 0) {
        var tem = new Array(1); tem[0] = 0;
        showcontextmenu(tem);
    }

    if (published == 'false') {
        hideContextMenu(8);
    }

}
function showcontextmenu(index) {
    var i = 0, count = index.length;
    var lis = document.getElementById('contextmenu').getElementsByTagName('li');
    for (i = 0; i < count; i++)
        lis[index[i]].style.display = 'block';
}

function hideContextMenu(index) {
    var lis = document.getElementById('contextmenu').getElementsByTagName('li');
    lis[index].style.display = 'none';
}

ie ? window.attachEvent('onload', init_contextmenu) : window.addEventListener('load', init_contextmenu, false);
//*****************************************************************************************//

ie ? window.attachEvent('onload', newslist_init) : window.addEventListener('load', newslist_init, false);
ie ? document.attachEvent('onmouseup', newslist_mouseup) : document.addEventListener('mouseup', newslist_mouseup, false);


/*$().ready(function() {
$("#tab_ctl16_ctl02_txtKeyword").autocomplete(oc, {
minChars: 3,
delay: 10,
width: 350,
matchContains: true,						
autoFill: false,
formatItem: function(row) {
return row.m + "@" + row.o;
//return row.m + "@" + row.o;
},
formatResult: function(row) {
return row.m;
//return row.m;
}
});
});*/

