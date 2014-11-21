function treeview_setCurrentNode(path)
{
	var a = null;
	var as = document.getElementById('treeviewFolder').getElementsByTagName('a');
	for (var i=0; i<as.length; i++)
	{
		if (as[i].getAttribute('_path') == path)
		{
			as[i].style.border = 'dotted 1px #303030';
			as[i].style.color = 'red';
			document.getElementById('txtKeyword').value = '';
			document.getElementById('postBackArg').value = path;
			document.getElementById('postBackArg2').value = path;
			a = as[i];
		}
		else
		{
			as[i].style.border = '';
			as[i].style.color = '';
		}
	}
	
	getChidlNode(a.parentNode, 'a', 0).innerHTML = '<img src=\"/images/lines/minus.gif\" alt=\"Thu nhỏ\" />';	
	var ul = getChidlNode(a.parentNode, 'ul', 0)
	if (ul && ul.innerHTML != '') ul.style.display = '';
	
	return a;
}



var myMenuItems = [
	{
		name: 'Thêm thư mục con',
		className: 'newfolder',
			callback: function(e) {
				folder_new(e.element());
			}
		},{
			name: 'Upload file',
			className: 'newfile', 
			disabled: false,
			callback: function(e) {
				folder_upload(e.element());
			}
		},{
		separator: true
		},{
			name: 'Copy',
			className: 'copy', 
			disabled: true,
			callback: function() {
				alert('Copy function called');
			}
		},{
			name: 'Xóa', 
			className: 'deletefolder',
			callback: function(e) {
			if (confirm('Bạn thực sự muốn xóa?'))
			folder_delete(e.element());
		}
	}
]

document.observe('dom:loaded', function(){


new Proto.Menu({
selector: '.folderTreeView',
className: 'menu desktop',
menuItems: myMenuItems
})
new Proto.Menu({
selector: '.folder',
className: 'menu desktop',
menuItems: myMenuItems
})
})








var isShowBG = false;

var folderPath = '';
	
	function folder_new(node)
	{
		document.getElementById('postBackArg').value = node.getAttribute('_path'); // parent folder
		var newName = prompt('Tên thư mục con','NewFolder');
		if (newName && newName != '')
		{
			document.getElementById('postBackArg2').value = newName; // sub folder name
			__doPostBack('btnNewFolder', '');
		}
	}
	function folder_delete(node)
	{
		document.getElementById('postBackArg').value = node.getAttribute('_path'); // parent folder
		__doPostBack('btnDeleteFolder', '');
	}

	function folder_upload(node)
	{
		document.getElementById('postBackArg').value = node.getAttribute('_path'); // parent folder
		upload_show();
	}
	function file_uploadFinish()
	{
		isReload = true;
		browseFolder(document.getElementById('postBackArg').value);
	}
	function browseFolder(path)
	{
		hideModalPopup('ctlPopupView');
		treeview_setCurrentNode(path);
		__doPostBack('btnBrowseFolder', '');
		
	}
	
var copylist = new Array();
	var pastebutton_isDisable = true;
	
	function pastebutton_disable()
	{
		document.getElementById('pastebutton').style.color = '#b0b0b0';
		pastebutton_isDisable = true;
	}
	pastebutton_disable();
	function pastebutton_enable()
	{
		document.getElementById('pastebutton').style.color = '#000000';
		pastebutton_isDisable = false;
	}
	function grid_copy()
	{	
		var chk, chks = document.getElementsByName('chk');
		copylist = new Array();
		for (var i=0; i<chks.length; i++)
		{
			chk = chks[i];
			if (chk.checked && chk.className.indexOf('chk') >= 0)
			{
				copylist.push(chk.value);
			}
		}
		if (copylist.length >= 1) pastebutton_enable();
	}
	function grid_paste()
	{
		if (copylist.length >= 1)
		{
			document.getElementById('postBackArg3').value = copylist.join('\t');
			document.getElementById('txtKeyword').value = '';
			__doPostBack('btnPaste', '');
			pastebutton_disable();
		}
	}
	function grid_selectItem(path, type) {
	    type = type.toLowerCase();
		if (type != 'folder') {
			if (typeof(parentFunction) != 'undefined') {
				if (opener) {
					if (share != 'share') {
						var args = new Array();
						args.push(folder + 'Images/Uploaded/' + path);
						parentFunction(args);
						window.close();
					}
					else
					{
						document.getElementById('postBackArg').value = path;
						__doPostBack('btnSaveToShareFolder', '');
					}
				}
			}
		}
		else
			alert('Không chọn được thư mục');
		
		
	}

	function grid_deleteItem(path, type)
	{
		document.getElementById('postBackArg').value = path;
		document.getElementById('postBackArg2').value = type;
		
		if (type.toLowerCase() == 'folder')
			__doPostBack('btnDeleteFolder', '');
		else
			__doPostBack('btnGrid_DeleteItem', '');
	}
	
	function grid_paging(pageIndex)
	{
		document.getElementById('postBackArg2').value = pageIndex;
		__doPostBack('btnPaging', '');
	}
	
	function grid_selectMultiFile()
	{
	
		
		var chk, chks = document.getElementsByName('chk');
		var arrImagesURL = new Array();
		var arrImagesURL2 = new Array();
		
		for (var i=0; i<chks.length; i++)
		{
			chk = chks[i];
			if (chk.checked && chk.className.indexOf('chk') > -1)
			{
				arrImagesURL.push(folder + 'Images/Uploaded/' + chk.value);
				arrImagesURL2.push(chk.value);
			}
		}
		if (share != 'share')
		{
			if (typeof(parentFunction) != 'undefined')
				parentFunction(arrImagesURL);
			close();
		}
		else
		{
			document.getElementById('postBackArg').value = arrImagesURL2.join(',');
			__doPostBack('btnSaveToShareFolder', '');
		}
	}
	
	function tonggleCHK()
	{
		var chk = document.getElementsByName('chk');
		var checked = document.getElementById('chk').checked;
		for (var i=0; i<chk.length; i++)
		{
			chk[i].checked = checked;
		}
		
	}
	
	var isDelete = false;
	function grid_deleteMultiFile()
	{
		if (confirm('Bạn có chắc chắn muốn xóa?'))
		{
			var chk, chks = document.getElementsByName('chk');
			var arrImagesURL = new Array();
			for (var i=0; i<chks.length; i++)
			{
				chk = chks[i];
				if (chk.checked && chk.className.indexOf('chk') > -1) arrImagesURL.push(chk.value);
			}
			if (arrImagesURL.length>0)
			{
				document.getElementById('postBackArg').value = arrImagesURL.join(',');
				isDelete = true;
				__doPostBack('btnDeleteMultiItem', '');
			}
		}
	}
	
	function grid_previewItem(path, extension, oEvent)
	{
		
		extension = extension.toLowerCase();
		
		if (extension != 'folder')
		{
			showModalPopup('ctlPopupView', false);
			document.getElementById("tdPreview").innerHTML = genPreviewhtml('/Images/Uploaded/' + path);
		}
		else
		{
			browseFolder(path);
		}
		
		
		
		// set active item
		if (extension != 'folder')
		{
			
			var tr = (oEvent.target || oEvent.srcElement).parentNode;
			
			while (tr.nodeName.toLowerCase() != 'tr') tr = tr.parentNode;
			
			var table = tr.parentNode;
			while (table.nodeName.toLowerCase() != 'table') table = table.parentNode;
			
			var trs = table.getElementsByTagName('tr');
			for (var i=0; i<trs.length; i++) trs[i].className = 'inactive';
			
			tr.className = 'active';
			
			//document.getElementById('khungxemtruoc_title').innerHTML = tr.getElementsByTagName('a')[0].innerHTML;
		}
		return false;
	}
//	
//	function present_close()
//	{
//		document.getElementById('present_content').innerHTML = '';
//		document.getElementById('present').style.display = 'none';
//	}
//	
	
//	// centerlize presentation area
//	var winX = 0;
//	var winY = 0;
//	winX = (screen.availWidth - 550)*.5;
//	winY = (screen.availHeight - 600)*.5 - 50;
//	var present = document.getElementById('present');
//	present.style.top = winY + 'px';
//	present.style.left = '200px';
	
	
	
	
	
	
	
	
	
	
	
	
	
	

var postBackElement, isReload = false;
function InitializeRequest(sender, args)
{ 
  if (prm.get_isInAsyncPostBack())
  args.set_cancel(true);
  postBackElement = args.get_postBackElement(); 

  showModalPopup('UpdateProgress1', true, 100);
  
  
  //UpdateProgress1_animate();
  //document.getElementById('grid').className = 'filter';
}
function EndRequest(sender, args)
{ 
  //document.getElementById('grid').className = '';
  if (isDelete)
	document.getElementById('postBackArg').value = document.getElementById('postBackArg2').value;
	
  new Proto.Menu({
          selector: '.folder',
          className: 'menu desktop',
          menuItems: myMenuItems
        })
	//var frameURL = '/Scripts/UploadMultiFile/Default.aspx?currentFolder=' + document.getElementById('postBackArg').value;
	//var frameURL = '/Scripts/UploadFile/Default.aspx?currentFolder=' + document.getElementById('postBackArg').value;
	//if (window.frames[0].location.href.indexOf(frameURL)==-1)
	//	window.frames[0].location = frameURL;
	//window.frames[0].location = "";
		
	hideModalPopup('UpdateProgress1');
}

//var UpdateProgress1_animate_TimeOutId;
//function UpdateProgress1_animate()
//{
//	if ($get('UpdateProgress1').style.top)
//	{
//		var top = parseInt($get('UpdateProgress1').style.top);
//		if (top<150)
//		{
//			$get('UpdateProgress1').style.top = top + 10 + 'px';
//			UpdateProgress1_animate_TimeOutId = setTimeout('UpdateProgress1_animate()', 10)
//		}
//		else if (UpdateProgress1_animate_TimeOutId)
//			clearTimeout(UpdateProgress1_animate_TimeOutId);
//	}
//	
//}

function findPosX(obj)
	{
		var curleft = 0;
		if(obj.offsetParent)
			while(1) 
			{
				curleft += obj.offsetLeft;
				if(!obj.offsetParent)
					break;
				obj = obj.offsetParent;
			}
		else if(obj.x)
			curleft += obj.x;
		return curleft;
	}

function findPosY(obj)
	{
		var curtop = 0;
		if(obj.offsetParent)
		while(1)
		{
			curtop += obj.offsetTop;
			if(!obj.offsetParent)
			break;
			obj = obj.offsetParent;
		}
		else if(obj.y)
			curtop += obj.y;
		return curtop;
	}


//var prm = Sys.WebForms.PageRequestManager.getInstance(); 
function documenntonload()
{
    if (mode != 'multi') {
        //document.getElementById('a_selectMultiFile').style.display = 'none';
    var ctrs = document.getElementsByName("a_selectMultiFile");
       // alert(ctrs.length)
        if (ctrs != undefined && ctrs.length > 0) { 
            for(var i=0;i<ctrs.length;i++)
                ctrs[i].style.display = 'none';
        }
    }
		
	else
	{
		var chk = document.getElementsByName('chk');
		for (var i=0; i<chk.length; i++)
		{
			chk[i].disable = 'disabled';
		}
	}
//	document.getElementById('UploadFileContainer').style.left = '150px';
//	document.getElementById('UploadFileContainer').style.top = '50px';
	
	// select item
	var items = selectedItems;
	var tr;
	if (items != '')
	{
		var chk, chks = document.getElementsByName('chk');
		var value;
		for (var i=0; i<chks.length; i++)
		{
			value = chks[i].value.substr(chks[i].value.lastIndexOf('/')+1);
			if (value && value != '' && items.indexOf(value) >=0)
			{
				chks[i].checked = true;
				tr = chks[i].parentNode;
				while (tr.nodeName.toLowerCase() != 'tr') tr = tr.parentNode;
				tr.getElementsByTagName('td')[0].className = 'active';
				tr.getElementsByTagName('td')[1].className = 'active';
			}
		}
	}	
	
	// colspan tất cả các thư mục
	var treeviewFolder = document.getElementById('treeviewFolder');
	var uls = treeviewFolder.getElementsByTagName('ul');
	for (var i=0; i<uls.length; i++) uls[i].style.display = 'none';
	
	// expand thư mục hiện tại
	var imgs = treeviewFolder.getElementsByTagName('img');
	for (var i=0; i<imgs.length; i++) imgs[i].src = '/images/lines/plus.gif';
	
	var a = null;
	if (typeof(treeview_CurrentNodePath) != 'undefined')
		a = treeview_setCurrentNode(treeview_CurrentNodePath);
		
	var icon = null;
	if (a)
	{
		//a.innerHTML = '<img src=\"/images/lines/minus.gif\" alt=\"Thu nhỏ\" />';
		var parentNode = a.parentNode;
		while (parentNode.nodeName.toLowerCase() != 'td')
		{
			if (parentNode.nodeName.toLowerCase() == 'li')
			{
				icon = getChidlNode(parentNode, 'a', 0);
				if (icon) icon.innerHTML = '<img src=\"/images/lines/minus.gif\" alt=\"Thu nhỏ\" />';
				icon = getChidlNode(parentNode, 'ul', 0);
				if (icon && icon.innerHTML != '') icon.style.display = '';
			}
			parentNode = parentNode.parentNode;
		}
	}
	//prm.add_initializeRequest(InitializeRequest);
	//prm.add_endRequest(EndRequest);
}

function selectThis(a)
{
	var as = document.getElementById('treeviewFolder').getElementsByTagName('a');
	for (var i=0; i<as.length; i++)
	{
		as[i].style.border = '';
		as[i].style.color = '#303030';
	}
		
	a.style.border = 'dotted 1px #303030';
	a.style.color = 'red';
	return true;
}

function upload_show2()
{    
    if (document.getElementById('postBackArg').value != '')
    {
	    if (window.frames[0].location.href.indexOf('currentFolder')==-1 || window.frames[0].location.href.indexOf("UploadFile")  == -1)
		    window.frames[0].location = '/Scripts/UploadFile/Default.aspx?currentFolder=' + document.getElementById('postBackArg').value;
		
	    showModalPopup('UploadFileContainer', true, 50);
    }
	
}

ie ? window.attachEvent('onload', documenntonload) : window.addEventListener('load', documenntonload, false);
function tonggleColspan(a)
{
	if (a.innerHTML.indexOf('plus') > 0)
	{
		a.innerHTML = '<img src=\"/images/lines/minus.gif\" alt=\"Thu nhỏ\" />';
	}
	else
	{
		a.innerHTML = '<img src=\"/images/lines/plus.gif\" alt=\"Mở rộng\" />';
	}
	
	var ul = getChidlNode(a.parentNode, 'ul', 0);
	if (ul && ul.innerHTML != '')
	{
		if (a.innerHTML.indexOf('minus') > 0)
		{
			ul.style.display = '';
		}
		else
		{
			ul.style.display = 'none';
		}
	}
	a.blur();
}
function expand(a)
{
	var ul = getChidlNode(a.parentNode, 'ul', 0);
	if (ul && ul.innerHTML != '')
	{
		a.innerHTML = '<img src=\"/images/lines/minus.gif\" alt=\"Thu nhỏ\" />';
		ul.style.display = '';
	}
	a.blur();
}