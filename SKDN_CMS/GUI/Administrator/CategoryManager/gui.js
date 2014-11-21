//////////////////////////////////////////////////////////////////////////////
// registry drag-drop element
//////////////////////////////////////////////////////////////////////////////

var Dom = YAHOO.util.Dom;
var Event = YAHOO.util.Event;
var DDM = YAHOO.util.DragDropMgr;

var dragcontainerIdFormat = 'DragAndDropContainer_'; // + index of container => DragAndDropContainer_0
var dragmoduleIdFormat = 'DragableModule_'; // + index of module => DragAndDropContainer_0
var defaultCookieForLayout = ''; // used when user reset layout
var numberOfColumn = 0;
var numberOfTable = 0;

// level of parent column ~ number of tables
function countLevel(parentNode)
{
	var count=0;
	count = getChildsOfParent(parentNode, 'table').length;
	return count;
}
function swapTable(table, upTable)
{
	if (table.nodeName.toLowerCase() != 'table' || upTable.nodeName.toLowerCase() != 'table') return;
	
	var parentNode = table.parentNode;
	
	var tem = table;
	
	parentNode.removeChild(table);
	parentNode.insertBefore(tem, upTable);
}
function moveTableUp(a)
{
	var table = getParentFromChild(a, 'table');
	if (table)
	{
		var i=0;
		var upTable = table.previousSibling;
		while (upTable && upTable.nodeName.toLowerCase() != 'table' && (i++)<2)
			upTable = upTable.previousSibling;
		if (upTable)
		{
			swapTable(table, upTable);
		}
	}
}
function moveTableDown(a)
{
	var table = getParentFromChild(a, 'table');
	if (table)
	{
		var i=0;
		var downTable = table.nextSibling;
		while (downTable && downTable.nodeName.toLowerCase() != 'table' && (i++)<2)
			downTable = downTable.nextSibling;
		if (downTable)
		{
			swapTable(downTable, table);
		}
	}
}
function moveTableLeft(a)
{
	var table = getParentFromChild(a, 'table');
	if (table)
	{
		var td = table.parentNode;
		if (td.nodeName.toLowerCase() != 'td') return;
		
		var tdLeft = td.previousSibling;
		var i = 0;
		while (tdLeft && tdLeft.nodeName.toLowerCase() != 'td' && (i++)<2)
			tdLeft = tdLeft.previousSibling;
		if (!tdLeft) return;
		
		// move table
		var tem = table;
		td.removeChild(table);
		tdLeft.appendChild(tem);
	}
}
function moveTableRight(a)
{
	var table = getParentFromChild(a,'table');
	if (table)
	{
		var td = table.parentNode;
		if (td.nodeName.toLowerCase() != 'td') return;
		
		var tdRight = td.nextSibling;
		var i = 0;
		while (tdRight && tdRight.nodeName.toLowerCase() != 'td' && (i++)<2)
			tdRight = tdRight.nextSibling;
		if (!tdRight) return;
		
		// move table
		var tem = table;
		td.removeChild(table);
		tdRight.appendChild(tem);
	}
}
function moveColumnLeft(childNode)
{
	var td = getParentFromChild(childNode, 'td');
	if (td)
	{
		var tr = td.parentNode;
		var tdLeft = td.previousSibling;
		var i = 0;
		while (tdLeft && tdLeft.nodeName.toLowerCase() != 'td' && (i++)<2)
			tdLeft = tdLeft.previousSibling;
		if (!tdLeft) return;
		
		// swap column
		var tem = td;
		tr.removeChild(td);
		tr.insertBefore(tem, tdLeft);
	}
}
function moveColumnRight(childNode)
{
	var td = getParentFromChild(childNode, 'td');
	if (td)
	{
		var tr = td.parentNode;
		var tdRight = td.nextSibling;
		var i = 0;
		while (tdRight && tdRight.nodeName.toLowerCase() != 'td' && (i++)<2)
			tdRight = tdRight.nextSibling;
		if (!tdRight) return;
		
		// swap column
		var tem = tdRight;
		tr.removeChild(tdRight);
		tr.insertBefore(tem, td);
	}
}
function removeTable(a)
{
	if (!confirm(confirm_delete)) return;
	
	var table = getParentFromChild(a,'table');
	if (table)
	{
		table.parentNode.removeChild(table);
	}
}

// add table to layout
function addTable(childControl)
{
	if (childControl) // add table inside column
	{
		var table = genTable();
		var column = getParentFromChild(childControl, 'td');
		column.appendChild(table);
	}
	else // add global table
	{
		var table = genTable();
		workarea.appendChild(table);
	}
	
	// regis with drag-drop YAHOO
	//YAHOO.example.DDApp.init();
	new YAHOO.util.DDTarget(dragcontainerIdFormat + numberOfColumn);
}

// gen a table for layout
function genTable()
{
	// table
	var table = document.createElement('table');
	table.setAttribute('cellpadding', '0');
	table.style.width = '100%';
	// tbody
	var tbody = document.createElement('tbody');
	// header 
	var trHeader = genHeaderTable();
	tbody.appendChild(trHeader);
	// content
	var trContent = genContentTable();
	
	tbody.appendChild(trContent);
	
	table.appendChild(tbody);
	return table;
}

// generate header control for a table
function genHeaderTable()
{
	// header row
	var tr = document.createElement('tr');
	tr.className = 'header';
	var td = document.createElement('td');
	
	// table name
	var html = '<div style="clear:both; margin:5px;"><div style="float:left;"><span>' + mess_NewTable + '</span></div>';
	numberOfTable++;
	// add column
	html += '<div style="float:right;">';
	
	html += '<a class="arrow editablecolumnwidth_inactive" href="#" title="' + title_unableEditColumnWidth + '" onclick="tonggle_editablecolumnwidth(this); return false;"></a>';
	
	html += '<a class="arrow addcolumn" href="#" title="'+ title_AddColumn + '" onclick="addColumn(this); return false;"></a>';
	// edit table name
	html += '<a class="arrow edit" href="#" title="' + title_EditTableName + '" onclick="editTableName(this); return false;"></a>';
	// move up table
	html += '<a class="arrow moveup" href="#" title="' + title_MoveTableUp + '" onclick="moveTableUp(this); return false;"></a>';
	// move down table
	html += '<a class="arrow movedown" href="#" title="' + title_MoveTableDown + '" onclick="moveTableDown(this); return false;"></a>';
	// move left table
	html += '<a class="arrow moveleft" href="#" title="' + title_MoveTableLeft + '" onclick="moveTableLeft(this); return false;"></a>';
	// move right table
	html += '<a class="arrow moveright" href="#" title="' + title_MoveTableRight + '" onclick="moveTableRight(this); return false;"></a>';
	// remove table
	html += '<a class="arrow remove" href="#" title="' + title_DeleteTable + '" onclick="removeTable(this); return false;"></a>';
	//
	html += '</div></div><br style="clear:both;" />';
	// set style
	td.innerHTML = html;
	
	td.style.backgroundColor = '#303030';
	td.style.color = 'white';
	td.style.width = 'auto';
	
	// column properties
	td.setAttribute('name', mess_NewTable);
	td.setAttribute('level', numberOfTable);
	td.setAttribute('ref', '');
	td.setAttribute('isdragable', true);
	
	tr.appendChild(td);
	
	return tr;
}

// generate content row for a table
function genContentTable()
{
	// module row
	var tr = document.createElement('tr');
	var td = genColumn(0);
	tr.appendChild(td);
	return tr;
}

// edit table name by 'prompt' command
function editTableName(childNode)
{
	var td = getParentFromChild(childNode, 'td');
	if (td)
	{
		var table = getParentFromChild(td, 'table');
		if (table)
		{
			var tableName = td.getAttribute('name');
			var newName = prompt(tableName, tableName);
			if (newName)
			{
				td.setAttribute('name', newName);
				table.setAttribute('name', newName);
				var span = td.getElementsByTagName('span')[0];
				if (span) span.innerHTML = newName;
			}	
		}
	}
}

// add column to layout
function addColumn(chidlNode)
{
	var td = genColumn(0); // new column
	
	var tbody = getParentFromChild(chidlNode, 'tbody');
	var trs = getChildsOfParent(tbody, 'tr');
	
	
	
	// recalculate col span
	var tdHeader = getChildsOfParent(trs[0], 'td')[0];
	if (tdHeader.getAttribute('colspan'))
	{
		setColspan(tdHeader, tdHeader.getAttribute('colspan') + 1);
	}
	else
	{	
		setColspan(tdHeader, 2);
	}
	
	trs[1].appendChild(td);
	
	// regis with drag-drop YAHOO
	//YAHOO.example.DDApp.init();
	new YAHOO.util.DDTarget(dragcontainerIdFormat + numberOfColumn);
}

function setColspan(cell, value)
{
	if (document.all)
		cell.colSpan = value;
	else
		cell.setAttribute('colspan', value);
}

// gen td node standing for column, properties of column assign to td attributes
function genColumn(level)
{
	var td = document.createElement('td');
	// column name
	var html = '<div style="clear:both; margin:5px;"><div style="float:left;"><span>' + mess_NewColumn + '</span></div>';
	numberOfColumn++;
	//
	html += '<div style="float:right;">';
	// insert table inside column
	html += '<a title="' + title_AddTable + '" class="arrow addtable" href="#" onclick="addTable(this); return false;"></a>';
	// edit coumn control
	html += '<a title="' + title_EditColumn + '" class="arrow edit" href="#" onclick="ChangeColumnToEditMode(this); return false;"></a>';
	// move column left
	html += "<a title=\"" + title_MoveColumnLeft + "\" class=\"arrow moveleft\" href=\"#\" title=\"Move column left\" onclick=\"moveColumnLeft(this); return false;\"></a>";
	// move column right
	html += "<a title=\"" + title_MoveColumnRight + "\" class=\"arrow moveright\" href=\"#\" title=\"Move column right\" onclick=\"moveColumnRight(this); return false;\"></a>";
	// remove column control
	html += '<a title="' + title_DeleteColumn + '" class="arrow remove" href="#" onclick="removeColumn(this); return false;"></a>';
	html += '</div></div><br style="clear:both;" />';
	// execute
	td.innerHTML = html;
	
	// drag-drop container
	var ul = document.createElement('ul');
	ul.id = dragcontainerIdFormat + numberOfColumn;
	ul.className = 'draglist';
	ul.innerHTML = vitualSpaceHTML;
	
	td.appendChild(ul);
	// assign module properties
	td.id = 'td' + numberOfColumn;
	td.setAttribute('name', mess_NewColumn);
	td.setAttribute('_style', '');
	td.setAttribute('level', level?0:level);
	td.setAttribute('isdragable', true);
	td.setAttribute('valign', 'top');
	td.className = 'content';
	td.style.backgroundColor = '#ffffff';
	
	return td;
}

// for version 2.0, it's not available now!
function addRow(child)
{
	alert('Does not support!'); return false;
	var tr = document.createElement('tr');
	var td = genColumn();
	var column = getParentFromChild(child, 'td');
	
	// get number of column
	var colspan;
	var colspanValue = 0;
	var tds = column.parentNode.getElementsByTagName('td');
	var temTd = column.parentNode.firstChild;
	while (temTd !=null)
	{
		if (temTd.nodeName.toLowerCase()=='td')
		{
			colspan = temTd.getAttribute('colspan');
			if (colspan)
				colspanValue += colspan;
			else
				colspanValue++;
		}
		temTd = temTd.nextSibling;
	}
	setColspan(td, colspanValue);
	
	tr.appendChild(td);
	var tbody = getParentFromChild(child, 'tbody');
	tbody.insertBefore(tr, column.parentNode.nextSibling);
	
	// regis with drag-drop YAHOO
	YAHOO.example.DDApp.init();
}

// delete column from layout
function removeColumn(a)
{
	if (!confirm(confirm_delete)) return;
	
	var td = getParentFromChild(a, 'td');
	var tr = td.parentNode;
	var tdleng = getChildsOfParent(tr, 'td').length;
	if (tdleng == 1)
	{
		var table = tr.parentNode.parentNode;
		table.parentNode.removeChild(table); // remove table
	}
	else
	{	
		// re-calculate spancolumn
		tr.removeChild(td);
		var tdHeader = getChildsOfParent(getChildsOfParent(tr.parentNode, 'tr')[0], 'td')[0];
		setColspan(tdHeader, tdHeader.getAttribute('colspan') - 1);
	}
	
	// regis with drag-drop YAHOO
	YAHOO.example.DDApp.init();
}

// add new module to layout
function addModule()
{
	
	var DropDownList2 = document.getElementById('DropDownList2');
	if (cboModuleFolder.selectedIndex==0 || cboModuleFolder.selectedIndex==-1 ||  DropDownList2.selectedIndex==0 ||  DropDownList2.selectedIndex==-1)
	{
		alert(alert_missModule);
		return;
	}
	
	var selIndex = cboModuleFolder.selectedIndex;
	var inFolder = cboModuleFolder.options[selIndex].text
	
	
	selIndex = DropDownList2.selectedIndex;
	var moduleName = DropDownList2.options[selIndex].text;
	
	var li = createModule(moduleName, inFolder);
	var ul = getFirstContainer();
	if (ul) 
	{
		// remove virtual space
		var virtualElement = ul.lastChild;
		if (virtualElement.nodeName.toLowerCase() == 'li' && virtualElement.id == '')
			ul.removeChild(virtualElement);
		ul.appendChild(li);
		// regis with drag-drop YAHOO
		YAHOO.example.DDApp.init();
	}
	else
	{
		alert(alert_missTable);
		return;
	}
	
}

// get first ul drag-drop-able in document
function getFirstContainer()
{
	var uls = document.body.getElementsByTagName('ul');
	for (var i = 0; i< uls.length; i++)
	{
		if (uls[i].id.indexOf(dragcontainerIdFormat)==0) return uls[i];
	}
	return null;
}

// create li node standing for module
function createModule(moduleName, inFolder)
{
	var li = document.createElement('li');
	
	// calculate module id
	var i=0;
	
	var module = document.getElementById(dragmoduleIdFormat + i + '$' + inFolder + '$' + moduleName);
	while (module)
		module = document.getElementById(dragmoduleIdFormat + (++i) + '$' + inFolder + '$' + moduleName);
	
	li.id = dragmoduleIdFormat + i + '$' + inFolder + '$' + moduleName;
	li.setAttribute('type', inFolder + '/' + moduleName);
	li.setAttribute('title', moduleName + ' ' + (i+1));
	li.setAttribute('cacheduration', 0);
	
	var html = '<span>' + moduleName + ' ' + (i+1) + '</span>';
	html += ' | <a href="#" onclick="ChangeModuleToEditMode(this); return false;" title="Edit">Edit</a>';
	html += ' | <a href="#" onclick="removeModule(this); return false;" title="Remove">Remove</a>';
	
	li.innerHTML = html;
	return li;
}

// bind column properties to edit form
function ChangeColumnToEditMode(a)
{
	var td = getParentFromChild(a, 'td');
	if (td)
	{
		currentColumn = td;
		// bind data to edit form
		columnRef.innerHTML = td.getAttribute("ref");
		chkIsDragable.checked = td.getAttribute("isdragable")=='True'?true:false;
		txtColumnName.value = td.getAttribute("name");
		txtWidth.value = td.style.width;
		txtStyle.value = td.getAttribute("_style")
		showEditColumnForm();
	}
}

// save column properties to td attributes
function SaveColumn()
{
	if (currentColumn)
	{
		var td = currentColumn;
		td.setAttribute("name", txtColumnName.value);
		td.setAttribute("ref", columnRef.innerHTML);
		td.setAttribute("_style", txtStyle.value);
		td.setAttribute("isdragable", chkIsDragable.checked? 'True': 'False');
		var width = parseWidth(txtWidth.value);
		
		if (!validateWidth(width))
		{
			alert(alert_InvalidWidth);
			width = currentColumn.style.width;
		}
		
		td.style.width = parseWidth(width);
		
		var spans = td.getElementsByTagName('span');
		if (spans.length>0) spans[0].innerHTML = txtColumnName.value;
	}
	hideEditColumnForm();
	
}
function hideEditColumnForm()
{
	overlay.style.display = 'none';
	bgoverlay.style.display = 'none';
	currentColumn = null;
}
function showEditColumnForm()
{
	overlay.style.display = 'block';
	bgoverlay.style.display = 'block';
	bgoverlay.style.height = document.documentElement.scrollHeight + 'px'; // cover whole document
	overlay.style.left = (document.documentElement.scrollWidth-overlay.offsetWidth)/2+'px'; // center of screen
	window.scroll(0,0);
}

// load module properties to edit form
function ChangeModuleToEditMode(a)
{
	var li = getParentFromChild(a,'li');
	currentModule = li;
	var td = getParentFromChild(li,'td');
	// set postback arguments
	pageArg.value = li.getAttribute("ref") + '$' + li.getAttribute("type") + '$' + li.getAttribute("title") + '$' + td.getAttribute("ref");
	
	// bind module properties
	document.getElementById('txtTitle').value = li.getAttribute("title");
	document.getElementById('lblModuleType').innerHTML = li.getAttribute("type");
	
	document.getElementById('txtReference').value = li.getAttribute("ref");
	document.getElementById('txtCacheTime').value = li.getAttribute("cacheduration");
	
	// bind module runtime properties
	RaisePostbackEvent('btnBindModuleEditForm', pageArg.value);
	
}

// save module properties from edit module form to li node
function SaveModule()
{
	if (currentModule)
	{
		var td = getParentFromChild(currentModule,'td');
		currentModule.setAttribute("type", document.getElementById('lblModuleType').innerHTML);
		currentModule.setAttribute("title", document.getElementById('txtTitle').value);
		var span = getChidlNode(currentModule, 'span', 0);
		if (span) span.innerHTML = document.getElementById('txtTitle').value;
		currentModule.setAttribute("ref", document.getElementById('txtReference').value);
		
		var cache = document.getElementById('txtCacheTime').value;
		if (isNaN(cache))
		{
			alert(alert_InvalidCache);
			cache = currentModule.getAttribute("cacheduration");
		}
		currentModule.setAttribute("cacheduration", cache);
		
		pageArg.value = currentModule.getAttribute("ref") + '$' + currentModule.getAttribute("type") + '$' + currentModule.getAttribute("title") + '$' + td.getAttribute("ref");
		// save runtime properties to server
		RaisePostbackEvent('btnSaveModule', pageArg.value);
	}
	hideEditModuleForm();
}
function hideEditModuleForm()
{
	var ref = currentModule.getAttribute("ref");
	if (!ref) setAttribute(currentModule, "ref", document.getElementById('txtReference').value);
	overlay_Editmodule.style.display = 'none';
	bgoverlay.style.display = 'none';
	currentModule = null;
}
function showEditModuleForm()
{	
	bgoverlay.style.display = 'block';
	overlay_Editmodule.style.display = 'block';
	bgoverlay.style.height = document.documentElement.scrollHeight + 'px';
	overlay_Editmodule.style.left = (document.documentElement.scrollWidth-overlay_Editmodule.offsetWidth)/2+'px';
	window.scroll(0,0);
}

// remove li from ul tag
function removeModule(a)
{
	if (!confirm(confirm_delete)) return;
	
	var module = getParentFromChild(a,'li');
	var ul = module.parentNode;
	ul.removeChild(module);
	// add virtual space
	
	if (ul.getElementsByTagName('li').length == 0)
	{
		ul.innerHTML = vitualSpaceHTML;
	}
	
	// regis with drag-drop YAHOO
	YAHOO.example.DDApp.init();
}

// tonggle status of 'editablecolumnwidth' column property
function tonggle_editablecolumnwidth(actionEl)
{
	var td = getParentFromChild(actionEl, 'td');
	if (actionEl.className == 'arrow editablecolumnwidth_active')
	{
		actionEl.className = 'arrow editablecolumnwidth_inactive';
		actionEl.title = title_unableEditColumnWidth;
		td.setAttribute('editablecolumnwidth', 'False');
	}
	else
	{
		actionEl.className = 'arrow editablecolumnwidth_active';
		actionEl.title = title_EditableColumnWidth;
		td.setAttribute('editablecolumnwidth', 'True');
	}
}

function initOverlay()
{
	// set overlay control at center of screen
	overlay.style.left = (document.documentElement.scrollWidth-overlay.offsetWidth)/2+'px';
	overlay_Editmodule.style.left = (document.documentElement.scrollWidth-overlay_Editmodule.offsetWidth)/2+'px';
	bgoverlay.style.height = document.documentElement.scrollHeight + 'px';
}
function preLoadImage()
{
	var preload_image_object = new Image();
	// image for hover hyperlink control
	// set image url
	var image_url = new Array();
	image_url[0] = "/GUI/Front_End/bacth_test5/images/arrow/blue/arrow-up.gif";
	image_url[1] = "/GUI/Front_End/bacth_test5/images/arrow/blue/arrow-down.gif";
	image_url[2] = "/GUI/Front_End/bacth_test5/images/arrow/blue/arrow-left.gif";
	image_url[3] = "/GUI/Front_End/bacth_test5/images/arrow/blue/arrow-right.gif";
	image_url[4] = "/GUI/Front_End/bacth_test5/images/arrow/blue/double-arrow.gif";
	image_url[5] = "/GUI/Front_End/bacth_test5/images/arrow/blue/closed-folder.gif";
	image_url[5] = "/GUI/Front_End/bacth_test5/images/photo2.jpg";

	for(var i=0; i<=5; i++) 
		preload_image_object.src = image_url[i];
}

function countTable()
{
	numberOfTable = workarea.getElementsByTagName('table').length;
	return numberOfTable;
}
function countColumn()
{
	numberOfColumn = workarea.getElementsByTagName('ul').length;
	return numberOfColumn;
}

if (window.addEventListener) // fire fox
{
	window.addEventListener('load', countTable, false);
	window.addEventListener('load', countColumn, false);
	window.addEventListener('load', initOverlay, false);
	window.addEventListener('load', preLoadImage, false);
	window.addEventListener('load', initAjax, false);
}
else // ie
{
	window.attachEvent('onload', countTable);
	window.attachEvent('onload', countColumn);
	window.attachEvent('onload', initOverlay);
	window.attachEvent('onload', preLoadImage);
	window.attachEvent('onload', initAjax);
}
