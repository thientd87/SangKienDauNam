/****************************************************************
*
*	bacth, March 27, 2008
*
****************************************************************/

//****************************************************************************************
//***** library **************************************************************************
var ie = window.navigator.appName == 'Microsoft Internet Explorer';

// get lowest parent node of child node
function getParentFromChild(childNode, parentTag)
{
	var i = 0;
	while (childNode.nodeName.toLowerCase() != parentTag.toLowerCase() && (i++)<10) childNode = childNode.parentNode;
	return childNode;
}

// set selected value of dropdownlist
function selectDropDownList(cbo, selectedValue)
{
	for (var i=0; i<cbo.options.length; i++)
	{
		if (cbo.options[i].value == selectedValue)
		{
			cbo.options[i].selected = true;
			return;
		}
	}
}

// set attribute for a node, cross-browser
function setAttribute(element, attributeName, attributeValue)
{
	// firefox
	element.setAttribute(attributeName, attributeValue);
	// ie
	for (var i=0;i<element.attributes.length; i++)
	{
		if (element.attributes[i].name.toLowerCase()==attributeName.toLowerCase() && attributeName != 'style')
		{
			element.attributes[i].value = attributeValue;
			break;
		}
	}
	
}

// convert number input => width style
function parseWidth(width)
{
	if (width == null || width == '' || width == 'auto' || width == '?') return 'auto';
	
	if (width.indexOf('%')==-1 && width.indexOf('px')==-1)
		return width + 'px';
	else
	{
		return width;
	}
}

// validate width for style
// right format: 56%, 56px, 56, auto
function validateWidth(width)
{
	if (width == null || width == '' || width == 'auto') return true;
	
	width = width.replace('%', '');
	width = width.replace('px', '');
	
	return !isNaN(width);
}

// trap enter key
// ev: event
// el: element fired event
function doBlur(ev, el)
{
	var key;

	if(window.event)
		key = window.event.keyCode;     //IE
	else
		key = ev.which;     //firefox

	if (key == 13)
	{
		el.blur();
		return false;
	}
}

//
function setFloat(element, floatValue)
{
	if (ie)
		element.style.styleFloat = floatValue; 
	else
		element.style.cssFloat = floatValue;
}


// 
function getChidlNode(parentNode, childTag, index)
{
	var childs = parentNode.childNodes;
	var count = 0;
	for (var i=0; i<childs.length; i++)
	{
		if (childs[i].nodeName.toLowerCase() == childTag)
		{
			if (count == index) return childs[i];
			count++;
		}
	}
}

//
function  getChildsOfParent(parentNode, childTag)
{
	var toReturn = new Array();
	var childs = parentNode.childNodes;
	for (var i=0; i<childs.length; i++)
	{
		if (childs[i].nodeName.toLowerCase() == childTag)
		{
			toReturn.push(childs[i]);
		}
	}
	return toReturn;
}