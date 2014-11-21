/**
 * COMMON DHTML FUNCTIONS
 * These are handy functions I use all the time.
 *
 * By Seth Banks (webmaster at subimage dot com)
 * http://www.subimage.com/
 *
 * Up to date code can be found at http://www.subimage.com/dhtml/
 *
 * This code is free for you to use anywhere, just keep this comment block.
 */

/**
 * X-browser event handler attachment and detachment
 * TH: Switched first true to false per http://www.onlinetools.org/articles/unobtrusivejavascript/chapter4.html
 *
 * @argument obj - the object to attach event to
 * @argument evType - name of the event - DONT ADD "on", pass only "mouseover", etc
 * @argument fn - function to call
 */
function addEvent(obj, evType, fn){
 if (obj.addEventListener){
    obj.addEventListener(evType, fn, false);
    return true;
 } else if (obj.attachEvent){
    var r = obj.attachEvent("on"+evType, fn);
    return r;
 } else {
    return false;
 }
}
function removeEvent(obj, evType, fn, useCapture){
  if (obj.removeEventListener){
    obj.removeEventListener(evType, fn, useCapture);
    return true;
  } else if (obj.detachEvent){
    var r = obj.detachEvent("on"+evType, fn);
    return r;
  } else {
    alert("Handler could not be removed");
  }
}

/**
 * Code below taken from - http://www.evolt.org/article/document_body_doctype_switching_and_more/17/30655/
 *
 * Modified 4/22/04 to work with Opera/Moz (by webmaster at subimage dot com)
 *
 * Gets the full width/height because it's different for most browsers.
 */
function getViewportHeight() {
	if (window.innerHeight!=window.undefined) return window.innerHeight;
	if (document.compatMode=='CSS1Compat') return document.documentElement.clientHeight;
	if (document.body) return document.body.clientHeight; 

	return window.undefined; 
}
function getViewportWidth() {
	var offset = 17;
	var width = null;
	if (window.innerWidth!=window.undefined) return window.innerWidth; 
	if (document.compatMode=='CSS1Compat') return document.documentElement.clientWidth; 
	if (document.body) return document.body.clientWidth; 
}

/**
 * Gets the real scroll top
 */
function getScrollTop() {
	if (self.pageYOffset) // all except Explorer
	{
		return self.pageYOffset;
	}
	else if (document.documentElement && document.documentElement.scrollTop)
		// Explorer 6 Strict
	{
		return document.documentElement.scrollTop;
	}
	else if (document.body) // all other Explorers
	{
		return document.body.scrollTop;
	}
}
function getScrollLeft() {
	if (self.pageXOffset) // all except Explorer
	{
		return self.pageXOffset;
	}
	else if (document.documentElement && document.documentElement.scrollLeft)
		// Explorer 6 Strict
	{
		return document.documentElement.scrollLeft;
	}
	else if (document.body) // all other Explorers
	{
		return document.body.scrollLeft;
	}
}


/**
 * SUBMODAL v1.6
 * Used for displaying DHTML only popups instead of using buggy modal windows.
 *
 * By Subimage LLC
 * http://www.subimage.com
 *
 * Contributions by:
 * 	Eric Angel - tab index code
 * 	Scott - hiding/showing selects for IE users
 *	Todd Huss - inserting modal dynamically and anchor classes
 *
 * Up to date code can be found at http://submodal.googlecode.com
 */

// Popup code
var gPopupADMINMask = null;
var gPopupADMINContainer = null;
var gPopFrameADMIN = null;
var gReturnFuncADMIN;
var gPopupADMINIsShown = false;
var gDefaultPageADMIN = "/loading.html";
var gHideSelectsADMIN = false;
var gReturnValADMIN = null;

var gTabIndexesADMIN = new Array();
// Pre-defined list of tags we want to disable/enable tabbing into
var gTabbableTagsADMIN = new Array("A","BUTTON","TEXTAREA","INPUT","IFRAME");	

// If using Mozilla or Firefox, use Tab-key trap.
if (!document.all) {
	document.onkeypress = keyDownHandlerADMIN;
}

/**
 * Initializes popup code on load.	
 */
function initPopUpADMIN() {
	// Add the HTML to the body
	theBody = document.getElementsByTagName('BODY')[0];
	popmask = document.createElement('div');
	popmask.id = 'popupADMINMask';
	popcont = document.createElement('div');
	popcont.id = 'popupADMINContainer';
	popcont.innerHTML = '' +
		'<div id="popupADMINInner">' +
			'<div id="popupADMINTitleBar">' +
				'<div id="popupADMINTitle"></div>' +
				'<div id="popupADMINControls">' +
					//'<img src="/Layout/images/Close_Popup.gif" onclick="hidePopWinADMIN(false);" id="popCloseBox" />' +
				'</div>' +
			'</div>' +
			
			'<div class="ceifdialog" id="chh-dialog-admin" style="overflow: visible;">' +
			'<table cellspacing="0" cellpadding="0" border="0">' +
			'<tbody>' +
			'<tr>' +
			'<td class="ceifdialogtl">' +
			'<img width="1" height="35" border="0" src="/images/1x1.gif"/>' +
			'</td>' +
			'<td class="ceifdialogtop">' +
			'<div class="ceifdialogtitletext" id="chh-dialog-title-admin"></div>' +
			'<a class="btnClose" href="javascript:hidePopWinADMIN(false)" titlebar="Close" title="Close"/>' +
			'</td>' +
			'<td class="ceifdialogtr"/>' +
			'</tr>' +
			'<tr>' +
			'<td class="ceifdialogleftbar">' +
			'<img width="15" height="1" border="0" src="/images/1x1.gif"/>' +
			'</td>' +
			'<td class="ceifdialogcenter">' +
			'<iframe src="'+ gDefaultPageADMIN +'" style="width:100%;height:100%;background-color:transparent;" scrolling="auto" frameborder="0" allowtransparency="true" id="popupADMINFrame" name="popupADMINFrame" width="100%" height="100%"></iframe>' +
			'</td>' +
			'<td class="ceifdialogrightbar">' +
			'<img width="15" height="1" border="0" src="/images/1x1.gif"/>' +
			'</td>' +
			'</tr>' +
			'<tr>' +
			'<td class="ceifdialogbottomleft"/>' +
			'<td class="ceifdialogbottom"/>' +
			'<td class="ceifdialogbottomright"/>' +
			'</tr>' +
			'</tbody>' +
			'</table>' +
			'</div>' +
			
			
		'</div>';
	theBody.appendChild(popmask);
	theBody.appendChild(popcont);
	
	gPopupADMINMask = document.getElementById("popupADMINMask");
	gPopupADMINContainer = document.getElementById("popupADMINContainer");
	gPopFrameADMIN = document.getElementById("popupADMINFrame");	
	
	//$("#chh-dialog-admin").draggable({ containment: '#popupADMINMask' });
	
	// check to see if this is IE version 6 or lower. hide select boxes if so
	// maybe they'll fix this in version 7?
	var brsVersion = parseInt(window.navigator.appVersion.charAt(0), 10);
	if (brsVersion <= 6 && window.navigator.userAgent.indexOf("MSIE") > -1) {
		gHideSelectsADMIN = true;
	}
	
	// Add onclick handlers to 'a' elements of class submodal or submodal-width-height
	var elms = document.getElementsByTagName('a');
	for (i = 0; i < elms.length; i++) {
		if (elms[i].className.indexOf("submodal") == 0) { 
			// var onclick = 'function (){showPopWinADMIN(\''+elms[i].href+'\','+width+', '+height+', null);return false;};';
			// elms[i].onclick = eval(onclick);
			elms[i].onclick = function(){
				// default width and height
				var width = 400;
				var height = 200;
				// Parse out optional width and height from className
				params = this.className.split('-');
				if (params.length == 3) {
					width = parseInt(params[1]);
					height = parseInt(params[2]);
				}
				showPopWinADMIN(this.href,width,height,null); return false;
			}
		}
	}
}
addEvent(window, "load", initPopUpADMIN);

 /**
	* @argument width - int in pixels
	* @argument height - int in pixels
	* @argument url - url to display
	* @argument returnFunc - function to call when returning true from the window.
	* @argument showCloseBox - show the close box - default true
	*/
function showPopWinADMIN(url, title, width, height, beforePopUpFunc, returnFunc) {
	// show or hide the window close widget
	//var showCloseBox = true
//	if (showCloseBox == null || showCloseBox == true) {
//		document.getElementById("popCloseBox").style.display = "block";
//	} else {
//		document.getElementById("popCloseBox").style.display = "none";
//	}
	document.getElementById("chh-dialog-title-admin").innerHTML = title;
	gPopupADMINIsShown = true;
	disableTabIndexesADMIN();
	gPopupADMINMask.style.display = "block";
	gPopupADMINContainer.style.display = "block";
	// calculate where to place the window on screen
	centerPopWinADMIN(width, height);
	
	var titleBarHeight = parseInt(document.getElementById("popupADMINTitleBar").offsetHeight, 10);


	gPopupADMINContainer.style.width = width + "px";
	gPopupADMINContainer.style.height = (height+titleBarHeight) + "px";
	
	setMaskSizeADMIN();

	// need to set the width of the iframe to the title bar width because of the dropshadow
	// some oddness was occuring and causing the frame to poke outside the border in IE6
	gPopFrameADMIN.style.width = parseInt(document.getElementById("popupADMINTitleBar").offsetWidth, 10) + "px";
	gPopFrameADMIN.style.height = (height) + "px";
	
	// set the url
	gPopFrameADMIN.src = url;
	
	gReturnFuncADMIN = returnFunc;
	// for IE
	if (gHideSelectsADMIN == true) {
		hideSelectBoxes();
	}
	
	window.setTimeout("setPopTitleADMIN();", 600);
}

//
var gi = 0;
function centerPopWinADMIN(width, height) {
	if (gPopupADMINIsShown == true) {
		if (width == null || isNaN(width)) {
			width = gPopupADMINContainer.offsetWidth;
		}
		if (height == null) {
			height = gPopupADMINContainer.offsetHeight;
		}
		
		//var theBody = document.documentElement;
		var theBody = document.getElementsByTagName("BODY")[0];
		//theBody.style.overflow = "hidden";
		var scTop = parseInt(getScrollTop(),10);
		var scLeft = parseInt(theBody.scrollLeft,10);
	
		setMaskSizeADMIN();
		
		//window.status = gPopupADMINMask.style.top + " " + gPopupADMINMask.style.left + " " + gi++;
		
		var titleBarHeight = parseInt(document.getElementById("popupADMINTitleBar").offsetHeight, 10);
		
		var fullHeight = getViewportHeight();
		var fullWidth = getViewportWidth();
		
		gPopupADMINContainer.style.top = (scTop + ((fullHeight - (height+titleBarHeight)) / 2)) + "px";
		gPopupADMINContainer.style.left =  (scLeft + ((fullWidth - width) / 2)) + "px";
		//alert(fullWidth + " " + width + " " + gPopupADMINContainer.style.left);
	}
}
//addEvent(window, "resize", centerPopWinADMIN);
//addEvent(window, "scroll", centerPopWinADMIN);
//window.onscroll = centerPopWinADMIN;


/**
 * Sets the size of the popup mask.
 *
 */
function setMaskSizeADMIN() {
	var theBody = document.getElementsByTagName("BODY")[0];
			
	var fullHeight = getViewportHeight();
	var fullWidth = getViewportWidth();
	
	// Determine what's bigger, scrollHeight or fullHeight / width
	if (fullHeight > theBody.scrollHeight) {
		popHeight = fullHeight;
	} else {
		popHeight = theBody.scrollHeight;
	}
	
	if (fullWidth > theBody.scrollWidth) {
		popWidth = fullWidth;
	} else {
		popWidth = theBody.scrollWidth;
	}
	
	gPopupADMINMask.style.height = popHeight + "px";
	gPopupADMINMask.style.width = popWidth + "px";
}

/**
 * @argument callReturnFunc - bool - determines if we call the return function specified
 * @argument returnVal - anything - return value 
 */
function hidePopWinADMIN(callReturnFunc) {
	gPopupADMINIsShown = false;
	var theBody = document.getElementsByTagName("BODY")[0];
	theBody.style.overflow = "";
	restoreTabIndexesADMIN();
	if (gPopupADMINMask == null) {
		return;
	}
	gPopupADMINMask.style.display = "none";
	gPopupADMINContainer.style.display = "none";
	if (callReturnFunc == true && gReturnFuncADMIN != null) {
		// Set the return code to run in a timeout.
		// Was having issues using with an Ajax.Request();
		gReturnValADMIN = window.frames["popupADMINFrame"].returnVal;
		window.setTimeout('gReturnFuncADMIN(gReturnValADMIN);', 1);
	}
	gPopFrameADMIN.src = gDefaultPageADMIN;
	// display all select boxes
	if (gHideSelectsADMIN == true) {
		displaySelectBoxes();
	}
}

/**
 * Sets the popup title based on the title of the html document it contains.
 * Uses a timeout to keep checking until the title is valid.
 */
function setPopTitleADMIN() {
	return;
	if (window.frames["popupADMINFrame"].document.title == null) {
		window.setTimeout("setPopTitleADMIN();", 10);
	} else {
		document.getElementById("popupADMINTitle").innerHTML = window.frames["popupADMINFrame"].document.title;
	}
}

// Tab key trap. iff popup is shown and key was [TAB], suppress it.
// @argument e - event - keyboard event that caused this function to be called.
function keyDownHandlerADMIN(e) {
    if (gPopupADMINIsShown && e.keyCode == 9)  return false;
}

// For IE.  Go through predefined tags and disable tabbing into them.
function disableTabIndexesADMIN() {
	if (document.all) {
		var i = 0;
		for (var j = 0; j < gTabbableTagsADMIN.length; j++) {
			var tagElements = document.getElementsByTagName(gTabbableTagsADMIN[j]);
			for (var k = 0 ; k < tagElements.length; k++) {
				gTabIndexesADMIN[i] = tagElements[k].tabIndex;
				tagElements[k].tabIndex="-1";
				i++;
			}
		}
	}
}

// For IE. Restore tab-indexes.
function restoreTabIndexesADMIN() {
	if (document.all) {
		var i = 0;
		for (var j = 0; j < gTabbableTagsADMIN.length; j++) {
			var tagElements = document.getElementsByTagName(gTabbableTagsADMIN[j]);
			for (var k = 0 ; k < tagElements.length; k++) {
				tagElements[k].tabIndex = gTabIndexesADMIN[i];
				tagElements[k].tabEnabled = true;
				i++;
			}
		}
	}
}


/**
 * Hides all drop down form select boxes on the screen so they do not appear above the mask layer.
 * IE has a problem with wanted select form tags to always be the topmost z-index or layer
 *
 * Thanks for the code Scott!
 */
function hideSelectBoxes() {
  var x = document.getElementsByTagName("SELECT");

  for (i=0;x && i < x.length; i++) {
    x[i].style.visibility = "hidden";
  }
}

/**
 * Makes all drop down form select boxes on the screen visible so they do not 
 * reappear after the dialog is closed.
 * 
 * IE has a problem with wanting select form tags to always be the 
 * topmost z-index or layer.
 */
function displaySelectBoxes() {
  var x = document.getElementsByTagName("SELECT");

  for (i=0;x && i < x.length; i++){
    x[i].style.visibility = "visible";
  }
}
/*Hàm refresh lại trang khi close modalwindow*/
function onClose_Refresh(returnVal){
	//alert("close");
    location.href = location.href;
}
function Form_Refresh(returnVal){
	//alert("close");
    location.href = location.href;
}
