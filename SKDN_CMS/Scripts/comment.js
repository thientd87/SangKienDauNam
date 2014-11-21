var grvCommentsID = null;
var btnSaveID = null;
var btnSaveAndApproveID = null;
var txtContentID = null;
var txtUserSendID = null;
var txtEmailID = null;
var hdCommentIDID = null;
var hdNewsIDID = null;
var chkCommentHayID = null;
var grvCommentsID = null;



function sethaystyle()
{
	if (document.getElementById(grvCommentsID) != null)
	{
		var chk, chks = document.getElementById(grvCommentsID).getElementsByTagName('input');
		
		var i = 0; count = chks.length;
		var td;
		for (i=0; i<count; i++)
		{
			chk = chks[i];
			if (chk.type == 'checkbox' && chk.id.indexOf('chkIsInNewDetails') >= 0)
			{
				td = chk.parentNode;
				while (td.nodeName.toLowerCase() != 'td') td = td.parentNode;
				if (chk.checked)
					td.className = 'active';
				else
					td.className = '';
			}
		}
	}
}

function commentsave()
{
	action = 'save';
	__doPostBack(document.getElementById(btnSaveID).name,'');
	document.getElementById('editform').style.display = 'none';
	
}
function commentapprove()
{
	if (confirm('Lưu và duyệt?'))
	{
		action = 'approve';
		__doPostBack(document.getElementById(btnSaveAndApproveID).name,'');
		document.getElementById('editform').style.display = 'none';
	}
}
function CheckDuyet()
{
	var count = countCheckItem(grvCommentsID, 'chkFirstColumn');
	if (count==0)
	{
		alert('Bạn chưa chọn comment');
		return false;
	}
	else
	{
		return confirm('Bạn có chắc chắn muốn duyệt những comment được đánh dấu?');
	}
}
function CheckKhongDuyet() {
    var count = countCheckItem(grvCommentsID, 'chkFirstColumn');
    if (count == 0) {
        alert('Bạn chưa chọn comment');
        return false;
    }
    else {
        return confirm('Bạn có chắc chắn muốn bỏ qua những comment được đánh dấu?');
    }
}
function CheckXoa()
{
	var count = countCheckItem(grvCommentsID, 'chkFirstColumn');
	if (count==0)
	{
		alert('Bạn chưa chọn comment');
		return false;
	}
	else
	{
		return confirm('Bạn có chắc chắn muốn xóa không?');
	}
}


var chkUpdateCommentHay = null;
var action = null;

function UpdateCommentHay(hay, commentID)
{
	return; // khong postback nua - xu ly theo lo
	document.getElementById(hdUpdateCommentHayID).value = hay + ',' + commentID;
	chkUpdateCommentHay = btnUpdateCommentHayID;
	__doPostBack(document.getElementById(btnUpdateCommentHayID).name,'');
	action = 'hay';
}

function showeditform(e)
{
	showModalPopup('editform');
}
function hideeditform() {
   
	hideModalPopup('editform');
}

var tr = null
function loadeditform(e)
{
	tr = e;
	
	document.getElementById(txtContentID).value = removeemtag(trimstr(tr.getElementsByTagName('p')[0].innerHTML.replace(/<br>/ig,'\n')));
	document.getElementById(txtUserSendID).value = trimstr(tr.getElementsByTagName('span')[0].innerHTML);
	document.getElementById(txtEmailID).value = trimstr(tr.getElementsByTagName('span')[1].innerHTML);
	document.getElementById(hdCommentIDID).value = trimstr(tr.getElementsByTagName('input')[0].value);
	document.getElementById(hdNewsIDID).value = trimstr(tr.getElementsByTagName('input')[1].value);
	//document.getElementById(chkCommentHayID).checked = getCheckBoxCommentHay(tr).checked;
}

function removeemtag(html)
{
	var re = new RegExp('<em>(.*?)</em>', 'gi');
	return html.replace(re, '$1');
}

function getCheckBoxCommentHay(tr)
{
	var input, inputs = tr.getElementsByTagName('input');
	var i = 0, count = inputs.length;
	for (i = count - 1; i >= 0; i--)
	{
		input = inputs[i];
		if (input.type = 'checkbox') return input;
	}
	return null;
}

function bindPresentation()
{
	tr.getElementsByTagName('span')[0].innerHTML = trimstr(document.getElementById(txtUserSendID).value);
	tr.getElementsByTagName('span')[1].innerHTML = trimstr(document.getElementById(txtEmailID).value);
	getCheckBoxCommentHay(tr).checked = document.getElementById(chkCommentHayID).checked;
	sethaystyle();
}
// ajax extension
function comment_EndRequest(sender, eventArgs)
{
    if (eventArgs.get_error() != undefined && 
        eventArgs.get_error().httpStatusCode == '500')
    {
        var errorMessage = eventArgs.get_error().message;
        eventArgs.set_errorHandled(true);
        //alert(errorMessage);
        alert('Đã có lỗi xảy ra, cập nhật không thành công!');
        
        switch (action)
        {
			case 'hay':	chkUpdateCommentHay.check = !chkUpdateCommentHay.checked; sethaystyle(); break;
		}
		return;
    }
    else if (action == 'save')
    {   
		hideeditform();
        bindPresentation();
        
    }
    else if (action == 'hay')
    {   
        hideeditform();
    }
    else if (action == 'approve')
    {
		hideeditform();
		tr.style.display = 'none';
        bindPresentation();
    }
    else
		animate_scrollToTop();
    action = null;
    sethaystyle();

    btnApprove.style.display = (cboViewType.selectedIndex == 0 || cboViewType.selectedIndex == 2) ? '' : 'none';
    btnNotApprove.style.display = (cboViewType.selectedIndex == 0  || cboViewType.selectedIndex == 1) ? '' : 'none';
}




function listcomment_onload()
{
	initvalue();
	document.getElementById('editform').style.display = 'none';
	sethaystyle();
	Sys.WebForms.PageRequestManager.getInstance().add_endRequest(comment_EndRequest);
}

function commentheight(tblID)
{

}

function hovertr(tr, evt)
{
	if (tr.className != 'active2') tr.className = 'active';
}
function outtr(tr, evt)
{
	if (tr.className != 'active2') tr.className = 'inactive';
}
function clicktr(tr, evt)
{
	evt = evt || window.event;
	var tar = evt.target || evt.srcElement;
	
	
	if (tar.nodeName.toLowerCase() == 'td')
	{
		var chks = tar.getElementsByTagName('input');
		var isHasCheckbox = false;
		for (var i=0; i<chks.length; i++)
			if (chks[i].type == 'checkbox')
			{
				chks[i].checked = !chks[i].checked;
				isHasCheckbox = true;
				if (chks[i].id.indexOf('chkIsInNewDetails') == -1)
				{
					tr.className = chks[i].checked ? 'active2' : 'inactive';
				}
				else
				{
					tar.className = chks[i].checked ? 'active' : '';
				}
			}
		if (!isHasCheckbox)
		{
			showeditform(tr); loadeditform(tr);
		}
	} 
	else if (tar.id.indexOf('chkIsInNewDetails') == -1)
	{
		if (tar.nodeName.toLowerCase() == 'input')
		{
			tr.className = tar.checked ? 'active2' : 'inactive';
		}
		else
		{
			showeditform(tr); loadeditform(tr);
		}
	}
	else
	{
		tar.parentNode.className = tar.checked ? 'active' : '';
	}
}
function countCheckboxInside(td)
{
	
	var count = 0;
	
}
ie ?  window.attachEvent('onload', listcomment_onload) : window.addEventListener('load', listcomment_onload, false);
