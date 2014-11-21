

var firefox = document.getElementById&&!document.all;
var isdrag = false;
var x0 = 0, y0 = 0, tx = 0, ty = 0;
var anchoc = 0, anchot = 0;
var objon = null;
var objTable = null;

getTopPos = function(inputObj)
{
	var returnValue = inputObj.offsetTop;
	while((inputObj = inputObj.offsetParent) != null){
		if(inputObj.tagName!='HTML')returnValue += inputObj.offsetTop;
	}
	return returnValue;
}   

getLeftPos = function(inputObj)
{
	var returnValue = inputObj.offsetLeft;

	while((inputObj = inputObj.offsetParent) != null){
		if(inputObj.tagName!='HTML')returnValue += inputObj.offsetLeft;
	}
	return returnValue;
}

function tblOnMouseDown(e){

	if(!firefox)e = event;

	objon = (!firefox)?e.srcElement:e.target;

	if(objon.className == "arrastrable")
	{
		objTable = getParentFromChild(objon, 'table');
		isdrag = true;
		x0 = e.clientX; y0 = e.clientY;
		anchoc = objon.offsetWidth;
		anchot = objTable.offsetWidth;
	}
}

function tblOnMouseMove(e){
	if(!firefox)e = event;

	if(isdrag){
		ic=e.clientX-x0+anchoc;
		it=e.clientX-x0+anchot;
		objon.style.width = ic + "px";

		objTable = getParentFromChild(objon, 'table');
		objTable.style.width = it + "px";
		
		
	}

}

function tblOnMouseUp(e){
	isdrag = false;
	
	// update module column size
	if (objTable)
	{
		var tbody = getChildsOfParent(objTable, 'tbody')[0];
		var trs = getChildsOfParent(tbody, 'tr');
		var trModule = trs[1];
		var trResize = trs[2];
		var tdModules = getChildsOfParent(trModule, 'td');
		var tdResizes = getChildsOfParent(trResize, 'td');
		
		for (var i=0; i<tdResizes.length; i++)
		{
			tdModules[i].style.width = tdResizes[i].style.width;
		}
	}
}
//document.onmouseup = tblOnMouseUp;
//document.onmousemove = tblOnMouseMove;
//document.onmousedown = tblOnMouseDown;


