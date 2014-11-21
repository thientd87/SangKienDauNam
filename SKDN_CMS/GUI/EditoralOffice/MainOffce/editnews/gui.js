function saveViewstate()
{
	document.getElementById('customViewstate').value = document.getElementById('panel').innerHTML;
}
function moveupBlock(childControl)
{
	if (!childControl) return;
	
	// find block node
	var blockNode = childControl.parentNode;
	while (blockNode.className != 'block') blockNode = blockNode.parentNode;
	
	// find upper block
	var upperBlockNode = blockNode.previousSibling;
	//while (upperBlockNode && upperBlockNode.nodeName.toLowerCase() == '#text') upperBlockNode = upperBlockNode.previousSibling;
	
	if (upperBlockNode)
	{
		// swap 2 blocks
		var temNode = document.createElement('div');
		temNode = blockNode;
		blockNode.parentNode.removeChild(blockNode);
		upperBlockNode.parentNode.insertBefore(temNode, upperBlockNode);
	}
}
function movedownBlock(childControl)
{
	if (!childControl) return;
	
	// find block node
	var blockNode = childControl.parentNode;
	while (blockNode.className != 'block') blockNode = blockNode.parentNode;
	
	// find lower block
	var lowerBlockNode = blockNode.nextSibling;
	//while (lowerBlockNode && lowerBlockNode.nodeName.toLowerCase() == '#text') lowerBlockNode = lowerBlockNode.nextSibling;
	if (lowerBlockNode)
	{
		// swap 2 blocks
		var temNode = document.createElement('div');
		temNode = lowerBlockNode;
		blockNode.parentNode.removeChild(lowerBlockNode);
		blockNode.parentNode.insertBefore(temNode, blockNode);
	}
}
function set_currentEditBlock(controlNode)
{
	if (!controlNode) return;
	
	var virtualDiv = document.getElementById('virtualDiv');
	if (virtualDiv) virtualDiv.parentNode.removeChild(virtualDiv);
	if (currentEditBlock) currentEditBlock.style.display = '';
	
	currentEditBlock = controlNode.parentNode;
	while (currentEditBlock.className != 'block') currentEditBlock = currentEditBlock.parentNode;
	
}



function extractValueFromStyle(style, attribute)
{
	var re = new RegExp(attribute + ':(.*?);');
	var m = re.exec(style);
	if (m == null) {
		return null;
	} else {
		if (m.length == 2)
		{
			return m[1];
		}
	}
	return null;
}




function editModule(controlNode)
{
	if (!controlNode) return false;
	
	var div = controlNode.parentNode;
	while (div.className != 'block') div = div.parentNode;
	
	var moduleNode, divs = div.getElementsByTagName('div');
	for (var i=0; i<divs.length; i++)
	{
		moduleNode = divs[i];
		if (moduleNode.className == 'module') break;
	}
	//if (editModule2(moduleNode)) showBG();
	editModule2(moduleNode);
}
function editModule2(moduleNode)
{
	if (moduleNode)
	{
		currentEditModule = moduleNode;
		
		var moduleType = moduleNode.getAttribute('_type');
		if (!moduleType) moduleType = '';
		
		var moduleReference = moduleNode.getAttribute('_reference');
		if (!moduleReference) moduleReference = '';
		
		var moduleId = moduleNode.id;
		
		var editControl = 'editModule.aspx?moduleType=' + moduleType + '&moduleReference=' + moduleReference;
		
		if (moduleType && moduleType != '')
		{
			iframeEditForm.src = editControl;
			showEditModuleForm(moduleId);
			return true;
		}
		else
		{
			alert('Block này là "Text only" - Không có module');
			return false;
		}
	}
	return false;
}
function editLastModule()
{
	var div, divs = document.getElementById('panel').getElementsByTagName('div');
	for (var i=divs.length-1; i>-1; i--)
	{
		div = divs[i];
		if (div.className == 'module')
		{
			if (div.getAttribute('_type'))
				editModule2(div);
			else
				editBlock2(div.parentNode);
			break;
		}
	}
}

function showEditModuleForm(moduleId)
{
	// show edit module form
	editModuleForm.style.display = 'block';
	alignCenterObj(editModuleForm);
	// get & bind style
	
	var style = document.getElementById(moduleId).style;
	document.getElementById('Text6').value = style.height;
	document.getElementById('Text7').value = style.width;
	document.getElementById('Text8').value = style.padding;
	document.getElementById('Text9').value = style.margin;
	document.getElementById('Text10').value = style.border;
	
	setValueDropdownlist(document.getElementById('Select2'), style.float);
	
	showBG();
	window.scrollTo(0, 0);
}
function saveModule()
{
	
	var style = '';
	
	if (document.getElementById('Text6').value != '')
		currentEditModule.style.height = trimstr(document.getElementById('Text6').value);
	
	if (document.getElementById('Text7').value != '')
		currentEditModule.style.width = trimstr(document.getElementById('Text7').value);
	
	if (document.getElementById('Text8').value != '')
		currentEditModule.style.padding = trimstr(document.getElementById('Text8').value);
	
	if (document.getElementById('Text9').value != '')
		currentEditModule.style.margin = trimstr(document.getElementById('Text9').value);
	
	if (document.getElementById('Text10').value != '')
		currentEditModule.style.border = trimstr(document.getElementById('Text10').value);
		
	if (document.getElementById('Select2').selectedIndex > 0)
		currentEditModule.style.float = document.getElementById('Select2').options[document.getElementById('Select2').selectedIndex].value;
	
	
	//currentEditModule.setAttribute('style', style);
	
	editModuleForm.style.display = 'none';
	
	
	currentEditModule.innerHTML = frames[0].document.getElementById('presentationContainer').innerHTML;
	
	bgoverlay.style.display = 'none';
	addControlToAllBlock();
}




function cancelEditModule()
{
	editModuleForm.style.display = 'none';
	bgoverlay.style.display = 'none';
	
}
function saveBlock()
{
	// get text content
	var spanContent, spans = currentEditBlock.getElementsByTagName('span');
	for (var i=0; i<spans.length; i++)
	{
		spanContent = spans[i];
		if (spanContent.className == 'content') break;
	}
	spanContent.innerHTML = document.getElementById("idContent"+IDEditblockform1_NewsContent.oName).contentWindow.document.body.innerHTML;
	
	// bind style
	var style = '';
	
	////////////////
	
	if (document.getElementById('Text1').value != '')
		currentEditBlock.style.height =  trimstr(document.getElementById('Text1').value);
	
	if (document.getElementById('Text2').value != '')
		currentEditBlock.style.width = trimstr(document.getElementById('Text2').value);
	
	if (document.getElementById('Text3').value != '')
		currentEditBlock.style.padding = trimstr(document.getElementById('Text3').value);
	
	if (document.getElementById('Text4').value != '')
		currentEditBlock.style.margin = trimstr(document.getElementById('Text4').value);
	
	if (document.getElementById('Text5').value != '')
		currentEditBlock.style.border = trimstr(document.getElementById('Text5').value);
		
	if (document.getElementById('Select1').selectedIndex > 0)
		currentEditBlock.style.float = document.getElementById('Select1').options[document.getElementById('Select1').selectedIndex].value;
	
	//currentEditBlock.setAttribute('style', style);
	
	editBlockForm.style.display = 'none';
	bgoverlay.style.display = 'none';
}
function cancelBlock()
{
	editBlockForm.style.display = 'none';
	bgoverlay.style.display = 'none';
}
function closeBlock(controlNode)
{
	var div = controlNode.parentNode;
	while (div.className != 'block') div = div.parentNode;
	div.parentNode.removeChild(div);
	
	resizeBGLeftAndRight();
}

function showControl(blockNode)
{
	if (!blockNode || blockNode.className != 'block') return false;
	
	blockNode.style.backgroundColor = '#d0d0d0';
		
	var ul, uls = blockNode.getElementsByTagName('ul');
	for (var i=0; i<uls.length; i++)
	{
		ul = uls[i];
		if (ul.className == 'controlContainer')
		{
			ul.style.display = 'block';
			break;
		}
	}
}
function hideControl(blockNode)
{
	if (!blockNode || blockNode.className != 'block') return false;
	
	blockNode.style.backgroundColor = '';
	
	var ul, uls = blockNode.getElementsByTagName('ul');
	for (var i=0; i<uls.length; i++)
	{
		ul = uls[i];
		if (ul.className == 'controlContainer')
		{
			ul.style.display = 'none';
			break;
		}
	}
}

function updateBlock()
{
	document.getElementById('Button1').click();
}
function addControlToAllBlock(parentNode)
{
	if (!parentNode) parentNode = document.getElementById('panel');
	
	// remove control;
	removeControl(parentNode);
	
	// add event handle: onmouseover="showControl(this);" onmouseout="hideControl(this);" for this block
	var html = parentNode.innerHTML;
	html = html.replace(/class=([^"].*?)\b/g, 'class="$1"'); // ie
	html = html.replace(/class="block"/g, 'class="block" onmouseover="showControl(this);" onmouseout="hideControl(this);"');
	
	parentNode.innerHTML = html;
	
	
	// add control: edit bock, edit module, remove block, move up, move down
	divs = parentNode.getElementsByTagName('div');
	for (var i=0; i<divs.length; i++)
	{
		div = divs[i];
		if (div.className == 'block')
		{
			addControlToBlock2(div);
		}
	}
}

function convertHTML_IE2Firefox(iehtml)
{
	
}

function removeControl(container)
{
	// remove control;
	var ul, uls = container.getElementsByTagName('ul');
	for (var i=0; i<uls.length; i++)
	{
		ul = uls[i];
		if (ul.className == 'controlContainer')
		{
			ul.parentNode.removeChild(ul);
		}
	}
}

function addControlToBlock2(blockNode)
{
	if (!blockNode) return false;
	
	
	if (hasModule(blockNode))
		blockNode.innerHTML = '<ul class="controlContainer">' +
									'<li><a onclick="set_currentEditBlock(this); editBlock2(); return false;" title="Edit block"' +
										'href="javascript:void();" class="controlButton edit">Sửa đoạn văn bản </a></li>' +
									'<li><a class="controlButton module" href="javascript:void();" title="Edit module"' +
									'	onclick="module_edit(this); return false;">Sửa module chức năng </a></li>' +
									'<li><a onclick="closeBlock(this); return false;" title="Remove block" href="javascript:void();"' +
									'	class="controlButton remove">Xóa block </a></li>' +
									'<li><a onclick="moveupBlock(this); return false;" title="Move-up block" href="javascript:void();"' +
									'	class="controlButton moveup">Lên trên </a></li>' +
									'<li><a onclick="movedownBlock(this); return false;" title="Move-down block" href="javascript:void();"' +
									'	class="controlButton movedown">Xuống dưới </a></li>' +
								'</ul>' + blockNode.innerHTML;
	else
		blockNode.innerHTML = '<ul class="controlContainer">' +
									'<li><a onclick="set_currentEditBlock(this); editBlock2(); return false;" title="Edit block"' +
									'	href="javascript:void();" class="controlButton edit">Sửa đoạn văn bản </a></li>' +
									'<li class="themModuleChucNang" onmouseover="showListOfModule(this)" onmouseout="hideListOfModules(this)">Thêm module chức năng' + listOfModulesHTML + '</li>' +
									'<li><a onclick="closeBlock(this); return false;" title="Remove block" href="javascript:void();"' +
									'	class="controlButton remove">Xóa block </a></li>' +
									'<li><a onclick="moveupBlock(this); return false;" title="Move-up block" href="javascript:void();"' +
									'	class="controlButton moveup">Lên trên </a></li>' +
									'<li><a onclick="movedownBlock(this); return false;" title="Move-down block" href="javascript:void();"' +
									'	class="controlButton movedown">Xuống dưới </a></li>' +
								'</ul>' + blockNode.innerHTML;
	
	
}
function showListOfModule(control)
{
	//window.scrollTo(0, 0);
	if (!control) return false;
	
	var div = control.parentNode;
	while (div.className != 'block') div = div.parentNode;
	customArg.value = div.id;
	
	currentEditBlock = div;
	
	var divs = div.getElementsByTagName('div');
	for (var i=0; i<divs.length; i++)
	{
		currentEditModule = divs[i];
		if (currentEditModule.className == 'module')
		{
			break;
		}
	}
	
	
	var ul, uls = currentEditBlock.getElementsByTagName('ul');
	for (var i=0; i<uls.length; i++)
	{
		ul = uls[i];
		if (ul.className == 'listOfModules') break;
	}
	
	ul.style.display="block";
//	if (!document.all) // fix for fire fox
//	{
//		ul.style.left = '129px';
//		ul.style.top = '27px';
//		alert('firefox');
//	}

	// fix ie
	if (document.all)
	{
		var a, as = ul.getElementsByTagName('a');
		for (var i=0; i<as.length; i++)
		{
			a = as[i];
			a.style.height = '29px';
			a.style.width = '147px';
		}
	}
	
	control.style.backgroundColor = '#e0e0e0';
	control.style.color = '#303030';
	control.style.border = 'solid 1px #d0d0d0';
	//showBG();
}

function hideListOfModules(control)
{
	var ul, uls = currentEditBlock.getElementsByTagName('ul');
	for (var i=0; i<uls.length; i++)
	{
		ul = uls[i];
		if (ul.className == 'listOfModules') break;
	}
	ul.style.display = 'none';
	
	control.style.backgroundColor = '';
	control.style.color = 'Yellow';
	control.style.border = 'solid 1px red';
}
function hasModule(blockNode)
{
	if (!blockNode) return false;
	
	var div, divs = blockNode.getElementsByTagName('div');
	for (var i=0; i<divs.length; i++)
	{
		div = divs[i];
		if (div.className == 'module')
		{
			if (!div.innerHTML || div.innerHTML == '' || div.innerHTML == '&nbsp;')
				return false;
			else
				return true;
		}
	}
	return false;
}
function hasControl(blockNode)
{
	if (!blockNode) return false;
	
	var divs = blockNode.getElementsByTagName('div');
	for (var i=0; i<divs.length; i++)
	{
		if (divs[i].className == 'controlContainer') return true;				
	}
	return false;
}
function addNewSimpleTextParagrahp()
{
	addNewSimpleTextParagrahp2('Đoạn văn bản mẫu, sửa đoạn văn bản này bằng chức năng "Sửa đoạn văn bản" trên menu. Nội dung văn bản mẫu nội dung văn bản mẫu nội dung văn bản mẫu nội dung văn bản mẫu.');
}

function addNewSimpleTextParagrahp2(content)
{
	document.getElementById('panel').innerHTML += genParagrahp(content);
	
	
	resizeBGLeftAndRight();
	window.scrollTo(0, 2000);
}


function genParagrahp(content)
{
	// count block
	var count = 1, divs = document.getElementById('panel').childNodes;
	for (var i=0; i<divs.length; i++)
	{
		if (divs[i].className == 'block' || (divs[i].id && divs[i].id.indexOf('block') == 0)) count++;
	}
	
	var outerHTML = '<div onmouseover="showControl(this);" onmouseout="hideControl(this);" class="block" id="block' + count + '" _reference="" _type="" style="">';
	outerHTML += '<ul class="controlContainer">' +
					'<li><a onclick="set_currentEditBlock(this); editBlock2(); return false;" title="Edit block"' +
					'	href="javascript:void();" class="controlButton edit">Sửa đoạn văn bản </a></li>' +
					'<li class="themModuleChucNang" onmouseover="showListOfModule(this)" onmouseout="hideListOfModules(this)">Thêm module chức năng' + listOfModulesHTML + '</li>' +
					'<li><a onclick="closeBlock(this); return false;" title="Remove block" href="javascript:void();"' +
					'	class="controlButton remove">Xóa block </a></li>' +
					'<li><a onclick="moveupBlock(this); return false;" title="Move-up block" href="javascript:void();"' +
					'	class="controlButton moveup">Lên trên </a></li>' +
					'<li><a onclick="movedownBlock(this); return false;" title="Move-down block" href="javascript:void();"' +
					'	class="controlButton movedown">Xuống dưới </a></li>' +
				'</ul>';
	outerHTML += '<div style="" _type="" _reference="" id="module' + count + '" class="module"></div><span class="content">' + content + '</span></div>';
	return 	outerHTML;
}
function genParagrahp2(content)
{
	// count block
	var count = 1, divs = document.getElementById('panel').childNodes;
	for (var i=0; i<divs.length; i++)
	{
		if (divs[i].className == 'block' || (divs[i].id && divs[i].id.indexOf('block') == 0)) count++;
	}
	
	var outerHTML = '<div onmouseover="showControl(this);" onmouseout="hideControl(this);" class="block" id="block' + count + '" _reference="" _type="" style="padding:0px;">';
	outerHTML += '<ul class="controlContainer">' +
					'<li><a onclick="set_currentEditBlock(this); editBlock2(); return false;" title="Edit block"' +
					'	href="javascript:void();" class="controlButton edit">Sửa đoạn văn bản </a></li>' +
					'<li class="themModuleChucNang" onmouseover="showListOfModule(this)" onmouseout="hideListOfModules(this)">Thêm module chức năng' + listOfModulesHTML + '</li>' +
					'<li><a onclick="closeBlock(this); return false;" title="Remove block" href="javascript:void();"' +
					'	class="controlButton remove">Xóa block </a></li>' +
					'<li><a onclick="moveupBlock(this); return false;" title="Move-up block" href="javascript:void();"' +
					'	class="controlButton moveup">Lên trên </a></li>' +
					'<li><a onclick="movedownBlock(this); return false;" title="Move-down block" href="javascript:void();"' +
					'	class="controlButton movedown">Xuống dưới </a></li>' +
				'</ul>';
	outerHTML += '<div style="" _type="" _reference="" id="module' + count + '" class="module"></div><span class="content">' + content + '</span></div>';
	return 	outerHTML;
}

function copyContentToEditPage()
{
	var span, spans, module, modules, block, blocks = document.getElementById('panel').getElementsByTagName('div');
	var html = '';
	for (var i=0; i<blocks.length; i++)
	{
		block = blocks[i];
		if (block.className == 'block')
		{
			html += '<div id="' + block.id + '" class="block" style="' + block.getAttribute('style') + '">';
			
			modules = block.getElementsByTagName('div');
			for (var j=0; j<modules.length; j++)
			{
				module = modules[j];
				if (module.className == 'module')
				{
					html += '<div id="' + module.id + '" class="module" style="' + module.getAttribute('style') + '" _type="' + module.getAttribute('_style') + '" _reference="' + module.getAttribute('_reference') + '">' + module.innerHTML + '</div>';
					break;
				}
			}
			
			spans = block.getElementsByTagName('span');
			for (var j=0; j<spans.length; j++)
			{
				span = spans[j];
				if (span.className == 'content')
				{
					html += '<span class="content">' + span.innerHTML + '</span>';
					break;
				}
			}
			
			html += '</div>';
		}
	}
	
	window.top.setEditorValue(html);
}
function copyContentToEditPage2()
{
	if (opener)
	{
		
		
		var arrul = new Array(), uls = document.getElementById('panel').getElementsByTagName('ul');
		for (var i=0; i<uls.length; i++)
		{
			ul = uls[i];
			if (ul.className == 'controlContainer')
			{
				arrul.push(ul);
			}
		}
		for (var i=0; i<arrul.length; i++)
		{
			arrul[i].parentNode.removeChild(arrul[i]);
		}
		
		var html = document.getElementById('panel').innerHTML;
		
		// remove all control
		html = html.replace(/onmouseout="hideControl\(this\);"/g, '');
		html = html.replace(/onmouseover="showControl\(this\);"/g, '');
		html = html.replace(/onmouseout=hideControl\(this\);/g, ''); // ie
		html = html.replace(/onmouseover=showControl\(this\);/g, ''); // ie
		
//		html = html.replace(/<DIV/g, '<div');
//		html = html.replace(/DIV>/g, 'div>');
//		html = html.replace(/<A/g, '<a');
//		html = html.replace(/A>/g, 'a>');
//		html = html.replace(/<SPAN/g, '<span');
//		html = html.replace(/SPAN>/g, 'span>');
//		
//		html = html.replace(/<UL/g, '<ul');
//		html = html.replace(/UL>/g, 'ul>');
//		
//		html = html.replace(/<LI/g, '<li');
//		html = html.replace(/LI>/g, 'li>');
//		
//		html = html.replace(/style="DISPLAY/g, 'style="display');
//		html = html.replace(/class=([^"].*?)\b/g, 'class="$1"');
//		
//		html = html.replace(/<ul\sclass="listOfModules".*?<\/ul>/g, '');
//		
//		
//		html = html.replace(/<ul\sclass="controlContainer".*?<\/ul>/g, '');
//		
//		html = html.replace(/<ul\sstyle="display:\snone;"\sclass="controlContainer".*?<\/ul>/g, '');
//		
		
		opener.setEditorValue(html);
		
		if (typeof(slideShow2_timeOutId) != 'undefined' && slideShow2_timeOutId != null) clearTimeout(slideShow2_timeOutId);
		
		window.close();
	}
}
function loadContent()
{	
	if (opener)
	{
		if (opener.editor && opener.document.getElementById("idContent"+opener.editor.oName))
		{
			var panel = document.getElementById('panel');
			panel.innerHTML = opener.document.getElementById("idContent"+opener.editor.oName).contentWindow.document.body.innerHTML;
			addControlToAllBlock(panel);
			
		}
	}
}
function resizeBGLeftAndRight()
{
	// family
	// document.getElementById('previewdivright').style.height = document.getElementById('previewtdleft').offsetHeight + 'px';
	// document.getElementById('marginleft').style.height = document.getElementById('previewtdleft').offsetHeight + 'px';
}

//function showijCMSControl()
//{
//	ijCMSEditor.style.display = 'block';
//}

//function hideijCMSControl()
//{
//	ijCMSEditor.style.display = 'none';
//}

function showBG()
{
	var height = document.getElementById('__mainDiv').offsetHeight;
	if (height < document.documentElement.scrollHeight) height = document.documentElement.scrollHeight
	bgoverlay.style.height = height + 'px';
	bgoverlay.style.display = 'block';
}

function hideBG()
{
	bgoverlay.style.display = 'none';
}



ie ? window.attachEvent('onload', loadContent) : window.addEventListener('load', loadContent, false);

