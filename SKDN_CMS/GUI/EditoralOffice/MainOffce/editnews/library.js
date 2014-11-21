function alignCenterObj(obj)
	{
		var scrollYX = getScrollXY();
		obj.style.left = scrollYX[0] + (document.documentElement.offsetWidth - obj.offsetWidth)/2 + 'px';
		obj.style.top = scrollYX[1] + 50 + 'px';
	}

function alignCenterWindow(obj)
	{
		
	}
	
function getScrollXY()
	{
		var scrOfX = 0, scrOfY = 0;
		if( typeof( window.pageYOffset ) == 'number' ) {
		//Netscape compliant
		scrOfY = window.pageYOffset;
		scrOfX = window.pageXOffset;
		} else if( document.body && ( document.body.scrollLeft || document.body.scrollTop ) ) {
		//DOM compliant
		scrOfY = document.body.scrollTop;
		scrOfX = document.body.scrollLeft;
		} else if( document.documentElement && ( document.documentElement.scrollLeft || document.documentElement.scrollTop ) ) {
		//IE6 standards compliant mode
		scrOfY = document.documentElement.scrollTop;
		scrOfX = document.documentElement.scrollLeft;
		}
		return [ scrOfX, scrOfY ];
	}
	
function alignRightObj(obj)
	{
		obj.style.left = document.documentElement.offsetWidth - obj.offsetWidth + 'px';
	}

function alignTopObj(obj)
	{
		obj.style.top = '10px';
	}

function setValueDropdownlist(cbo, value)
	{
	
		if (cbo && value)
		{
			value = trimstr(value);
			for (var i=0; i<cbo.options.length; i++)
			{
				if (cbo.options[i].value == value) cbo.selectedIndex = i;
			}
		}
	}



function checkStyleValue(valueToCheck, valueDefault, element)
{
	if (element.value == valueToCheck) element.value = valueDefault;
}








