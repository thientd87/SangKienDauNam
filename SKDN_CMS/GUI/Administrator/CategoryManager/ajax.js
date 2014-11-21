function RaisePostbackEvent(controlId, arg)
{
   __doPostBack(controlId, arg);
}
function onCallbackComplete() {}
function onCallbackError() {}

function doClick(buttonId, e)
{
	//the purpose of this function is to allow the enter key to 

	//point to the correct button to click.
	var key;

	if(window.event)
		key = window.event.keyCode;     //IE
	else
		key = e.which;     //firefox

	if (key == 13)
	{
		//Get the button the user wants to have clicked

		var btn = document.getElementById(buttonId);
		if (btn != null)
		{ //If we find the button click it
			btn.click();
			
		}
		return false;
	} else return true;
}

var _postBackElement;
function initializeRequest(sender, e)
{
	_postBackElement = e.get_postBackElement();
	if (_postBackElement.id.indexOf('btnBindModuleEditForm') > -1)
	{
		bgoverlay.style.height = document.documentElement.scrollHeight + 'px';
		bgoverlay.style.display = 'block';
	}
}

function endRequest(sender, e)
{
	if (_postBackElement.id.indexOf('btnBindModuleEditForm') > -1)
	{
		// check whether has picture properties
		var picturePosition = $findByProperty('value', 'pictureposition', 'input');
		if (picturePosition)
		{
			// show picture layout
			var tables = overlay_Editmodule.getElementsByTagName('table');
			var pictureTableLayout = tables[tables.length-1];
			pictureTableLayout.style.display = 'block';
			overlay_Editmodule.style.width = '800px';
			
			// calculate picture layout width
			if (currentModule)
			{
				var td = getParentFromChild(currentModule,'td');
				pictureTableLayout.style.width = td.offsetWidth + 'px';
			}
			
			// add event handle for position dropdownlist
			var cboPicturePosition = picturePosition.parentNode.getElementsByTagName('select')[0];
			cboPicturePosition.setAttribute( 'onchange', 'bindPictureLayout();');
			
			var picturedimension = $findByProperty('value', 'picturedimension', 'input').previousSibling;
			while (picturedimension.nodeName.toLowerCase() != 'input') picturedimension = picturedimension.previousSibling;
			picturedimension.setAttribute('onchange', "bindPictureLayout();");
			
			// bind picture layout
			bindPictureLayout();
		} else
		{
			// hide picture layout
			var tables = overlay_Editmodule.getElementsByTagName('table');
			if (tables.length>0) tables[tables.length-1].style.display = 'none';
			// resize edit form width
			overlay_Editmodule.style.width = '500px';
			
		}
		showEditModuleForm();
	}
}

function bindPictureLayout()
{
	var picturePosition = $findByProperty('value', 'pictureposition', 'input');
	if (picturePosition)
	{
		var cboPicturePosition = picturePosition.parentNode.getElementsByTagName('select')[0];
		var selIndex = cboPicturePosition.selectedIndex;
		var position = cboPicturePosition.options[selIndex].text;
		
		var titleLinkLayout = document.getElementById('titleLinkLayout');
		var imageLinkLayout = document.getElementById('imageLinkLayout');
		
		var tdTintieudiem = titleLinkLayout.parentNode;
		
		var tem1 = titleLinkLayout;
		var tem2 = document.createElement('a');
		tem2 = imageLinkLayout.parentNode;
		var image = tem2.getElementsByTagName('img')[0];
		
		switch (position)
		{
			case '':
				imageLinkLayout.parentNode.style.display = 'none';
				break;
			case 'TOP-LEFT':
				tdTintieudiem.removeChild(imageLinkLayout.parentNode);
				image.setAttribute('style', 'float:left; margin-right: 10px;');
				tem2.style.display = 'block';
				tdTintieudiem.insertBefore(tem2, titleLinkLayout);
				break;
			case 'TOP-RIGHT':
				tdTintieudiem.removeChild(imageLinkLayout.parentNode);
				image.setAttribute('style', 'float:right; margin-left: 10px;');
				tem2.style.display = 'block';
				tdTintieudiem.insertBefore(tem2, titleLinkLayout);
				break;
			case 'MIDDLE-LEFT':
				tdTintieudiem.removeChild(imageLinkLayout.parentNode);
				image.setAttribute('style', 'float:left; margin-right: 10px;');
				tem2.style.display = 'block';
				tdTintieudiem.insertBefore(tem2, titleLinkLayout.nextSibling);
				break;
			case 'MIDDLE-RIGHT':
				tdTintieudiem.removeChild(imageLinkLayout.parentNode);
				image.setAttribute('style', 'float:right; margin-left: 10px;');
				tem2.style.display = 'block';
				tdTintieudiem.insertBefore(tem2, titleLinkLayout.nextSibling);
				break;
		}
		
		// image size
		var txtSize = $findByProperty('value', 'picturedimension', 'input').previousSibling;
		while (txtSize.nodeName.toLowerCase() != 'input') txtSize = txtSize.previousSibling;
		if (txtSize)
		{
			// parse width, heigh
			var strSize = txtSize.value;
			var arrSize = strSize.split(',');
			// width
			if (arrSize[0] != '' && Number(arrSize[0]) != 'NaN')
				image.setAttribute('width', arrSize[0]);
			else
				image.removeAttribute('width');
			// height
			if (arrSize.length==2 && arrSize[1] != '' && Number(arrSize[1]) != 'NaN')
				image.setAttribute('height', arrSize[1]);
			else
				image.removeAttribute('height');
		}
	}
}

 function initAjax()
 {
	var prm = Sys.WebForms.PageRequestManager.getInstance();
	if (prm)
	{
		prm.add_initializeRequest(initializeRequest);
		prm.add_endRequest(endRequest);
	}
 }
 function $findByProperty(propertyName, propertyValue, tagName)
 {
	var elements = document.documentElement.getElementsByTagName(tagName);
	for (var i=0; i<elements.length; i++)
	{
		if (elements[i].getAttribute(propertyName) == propertyValue)
		{
			return elements[i];
		}
	}
	return null;
 }