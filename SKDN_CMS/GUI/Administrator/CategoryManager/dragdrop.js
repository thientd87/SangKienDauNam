//////////////////////////////////////////////////////////////////////////////
// bacth: registry drag-drop element
//////////////////////////////////////////////////////////////////////////////


YAHOO.example.DDApp = {
    init: function() {
		
		// registry containers
		var containers = document.documentElement.getElementsByTagName('ul');
		var container;
		for (i=0; i<containers.length; i++)
		{
			container = containers[i];
			if (container.id.indexOf(dragcontainerIdFormat)==0)
			{
				// declare with Yahoo Drag-drop manager
				new YAHOO.util.DDTarget(container.id);
			}
		}
		// registry modules
		var modules = document.documentElement.getElementsByTagName('li');
		var module;
		var dd;
		var headerId;
		for (i=0; i<modules.length; i++)
		{
			module = modules[i];
			if (module.id.indexOf(dragmoduleIdFormat)==0)
			{
				// declare with Yahoo Drag-drop manager
				new YAHOO.example.DDList(module.id);
			}
		}
    }
};

//////////////////////////////////////////////////////////////////////////////
// custom drag and drop implementation
//////////////////////////////////////////////////////////////////////////////

YAHOO.example.DDList = function(id, sGroup, config) {

    YAHOO.example.DDList.superclass.constructor.call(this, id, sGroup, config);

    this.logger = this.logger || YAHOO;
    var el = this.getDragEl();
    Dom.setStyle(el, "opacity", 0.67); // The proxy is slightly transparent

    this.goingUp = false;
    this.lastY = 0;
};

YAHOO.extend(YAHOO.example.DDList, YAHOO.util.DDProxy, {

    startDrag: function(x, y) {
        //this.logger.log(this.id + " startDrag");

        // make the proxy look like the source element
        var dragEl = this.getDragEl();
        var clickEl = this.getEl();
        Dom.setStyle(clickEl, "visibility", "hidden");
		
        dragEl.innerHTML = clickEl.innerHTML;

        Dom.setStyle(dragEl, "color", Dom.getStyle(clickEl, "color"));
        Dom.setStyle(dragEl, "backgroundColor", Dom.getStyle(clickEl, "backgroundColor"));
        Dom.setStyle(dragEl, "border", "2px solid gray");
    },

    endDrag: function(e) {

        var srcEl = this.getEl();
        var proxy = this.getDragEl();

        // Show the proxy element and animate it to the src element's location
        Dom.setStyle(proxy, "visibility", "");
        var a = new YAHOO.util.Motion( 
            proxy, { 
                points: { 
                    to: Dom.getXY(srcEl)
                }
            }, 
            0.2, 
            YAHOO.util.Easing.easeOut 
        )
        var proxyid = proxy.id;
        var thisid = this.id;

        // Hide the proxy and show the source element when finished with the animation
        a.onComplete.subscribe(function() {
                Dom.setStyle(proxyid, "visibility", "hidden");
                Dom.setStyle(thisid, "visibility", "");
                // after module 'fly' to target container, save layout (module position) to cookie
                //saveLayoutToCookie();
                setVirtualSpace();
                document.getElementById(thisid).parentNode.style.border = 'none';
            });
        a.animate();
    },

    onDragDrop: function(e, id) {

        // If there is one drop interaction, the li was dropped either on the list,
        // or it was dropped on the current location of the source element.
        if (DDM.interactionInfo.drop.length === 1) {

            // The position of the cursor at the time of the drop (YAHOO.util.Point)
            var pt = DDM.interactionInfo.point; 

            // The region occupied by the source element at the time of the drop
            var region = DDM.interactionInfo.sourceRegion; 

            // Check to see if we are over the source element's location.  We will
            // append to the bottom of the list once we are sure it was a drop in
            // the negative space (the area of the list without any list items)
            if (!region.intersect(pt)) {
                var destEl = Dom.get(id);
                var destDD = DDM.getDDById(id);
                destEl.appendChild(this.getEl());
                destDD.isEmpty = false;
                DDM.refreshCache();
            }

        }
    },

    onDrag: function(e) {

        // Keep track of the direction of the drag for use during onDragOver
        var y = Event.getPageY(e);

        if (y < this.lastY) {
            this.goingUp = true;
        } else if (y > this.lastY) {
            this.goingUp = false;
        }

        this.lastY = y;
    },

    onDragOver: function(e, id) {
    
        var srcEl = this.getEl();
        var destEl = Dom.get(id);
		var p = destEl.parentNode;
        // We are only concerned with list items, we ignore the dragover
        // notifications for the list.
        if (destEl.nodeName.toLowerCase() == "li") {
            var orig_p = srcEl.parentNode;
            
			
            if (this.goingUp) {
                p.insertBefore(srcEl, destEl); // insert above
            } else {
                p.insertBefore(srcEl, destEl.nextSibling); // insert below
            }
			
            DDM.refreshCache();
        }
        
        // create border for target container
        if (destEl.nodeName.toLowerCase() == "li")
			p.style.border = 'solid 1px red';
		else if (destEl.nodeName.toLowerCase() == "ul") 
			destEl.style.border = 'solid 1px red';
    },
    
    onDragOut: function(e, id) {
		var destEl = Dom.get(id);
		var p = destEl.parentNode;
		
		// clear border container
		if (destEl.nodeName.toLowerCase() == "li")
			p.style.border = 'none';
		else if (destEl.nodeName.toLowerCase() == "ul") 
			destEl.style.border = 'none';
    }
});

// add layout-control to header moveable-module
function SetLayoutControl()
{
	var uls = document.getElementsByTagName('ul');
	var ul;
	var i, j;
	var lis, li;
	
	for (i=0; i<uls.length; i++)
	{
		ul= uls[i];
		if (ul.id.indexOf(dragcontainerIdFormat)==0) // check if this UL has ID in containerId format?
		{
			lis = ul.getElementsByTagName('li');
			for (j=0; j<lis.length; j++)
			{
				li = lis[j];
				if (li.id.indexOf(dragmoduleIdFormat)==0)
				{
					Add_UP_DOWN_Icon(li);
					Add_MOVE_Icon(li);
				}
			}
		}
	}
}

// add move-control to module header
function Add_UP_DOWN_Icon(li)
{
	var className = 'newsfocus_head_right'; // class name of td to specify container of control
	var td, tds = li.getElementsByTagName('td'); // get right column of header module
	for (var i=0; i<tds.length; i++)
	{
		td = tds[i];
		if (td.className == className)
		{
			td.innerHTML += '<div class="control"><br style="clear: both;" />' +
								'<a href="#" class="btn down" title="down" onclick="return move_down(this);">' +
									'&nbsp;</a> <a href="#" class="btn up" title="up" onclick="return move_up(this);">' +
										'&nbsp;</a>' +
							'<br style="clear: both;" /></div>';
							return;
			td.setAttribute('valign', 'bottom');
			td.setAttribute('align', 'right');
		}
	}
}

// add move-control to module header
function Add_MOVE_Icon(li)
{
	var className = 'newsfocus_head'; // class name of td to specify container of control
	var td, tds = li.getElementsByTagName('td'); // get right column of header module
	for (var i=0; i<tds.length; i++)
	{
		td = tds[i];
		if (td.className == className)
		{
			td.innerHTML += '&nbsp;';
			td.setAttribute('id', 'HeaderOfModule_' + li.id);
			td.style.cursor = 'move';
							return;
		}
	}
}

function getHeaderIdOfModule(module)
{
	var control = document.getElementById('HeaderOfModule_' + module.id);
	if (control)
		return control.id;
	else
		return null;
}

// move-up module
function move_up(el)
{
	
	var liModule = el.parentNode;
	var liModuleLower;
	
	// find liModule contain this control, repeat max 12 loop
	var i = 0, j;
	while (liModule.id.indexOf(dragmoduleIdFormat) != 0 && (i++)<12) 
		liModule = liModule.parentNode;
	if (i==12) return; // li not found, return
	
	var ulContainer = liModule.parentNode;
	var liModules = ulContainer.getElementsByTagName('li');
	
	// find index of module in container
	for (i=liModules.length-1; i>-1; i--)
	{
		if (liModules[i].id == liModule.id)
		{
			// find module next to this module
			for (j=i-1; j>-1; j--)
			{
				if (liModules[j].id.indexOf(dragmoduleIdFormat)==0)
				{
					swapModule(liModules[j], liModules[i], ulContainer);
					saveLayoutToCookie();
					YAHOO.example.DDApp.init();
					return false;
				}
			}
		}
	}
	return false;
}
// move-down module
function move_down(el)
{
	
	var liModule = el.parentNode;
	var liModuleLower;
	
	// find liModule, repeat max 12 loop
	var i = 0, j;
	while (liModule.id.indexOf(dragmoduleIdFormat) != 0 && (i++)<12) 
		liModule = liModule.parentNode;
	if (i==12) return; // li not found, return
	
	var ulContainer = liModule.parentNode;
	var liModules = ulContainer.getElementsByTagName('li');
	
	// find index of module in container
	for (i=0; i<liModules.length; i++)
	{
		if (liModules[i].id == liModule.id)
		{
			// find module next to this module
			for (j=i+1; j<liModules.length; j++)
			{
				if (liModules[j].id.indexOf(dragmoduleIdFormat)==0)
				{
					swapModule(liModules[i], liModules[j], ulContainer);
					saveLayoutToCookie();
					YAHOO.example.DDApp.init();
					return false;
				}
			}
		}
	}
	return false;
}



// swap 2 lis in ul container
// liModule is front of liModuleLower
function swapModule(liModule, liModuleLower, ulContainer)
{
	var id = liModuleLower.id;
	var html = liModuleLower.innerHTML;
	ulContainer.removeChild(liModuleLower);
	
	var temModule = document.createElement('li');
	temModule.innerHTML = html;
	temModule.setAttribute('id', id);
	ulContainer.insertBefore(temModule, liModule);
}

// bacth: save layout to cookie
function saveLayoutToCookie()
{
	var cookieName = 'PortalLayout_' + window.location.href; // each page has one record
	var cookieValue = getLayoutInCookieFormat();
	createCookie(cookieName, cookieValue, 30); // 30 days
}

function getLayoutInCookieFormat()
{
	var cookieName = 'PortalLayout_' + window.location.href; // each page has one record
	var cookieValue = '';
	
	
	var containerArr = new Array(); // store ul.id
	var moduleArr; // store li.id
	
	var uls = document.getElementsByTagName('ul');
	var ul;
	var i, j;
	var lis, li;
	var virtualElement;
	
	for (i=0; i<uls.length; i++)
	{
		ul= uls[i];
		if (ul.id.indexOf(dragcontainerIdFormat)==0) // check if this UL has ID in containerId format?
		{
			moduleArr = new Array();
			lis = ul.getElementsByTagName('li');
			for (j=0; j<lis.length; j++)
			{
				li = lis[j];
				if (li.id.indexOf(dragmoduleIdFormat)==0)
				{
					// add module to array
					moduleArr.push(li.id);
				}
			}
			if (moduleArr.length>0)
			{
				// add container to array
				containerArr.push(ul.id + '-' + moduleArr.join(','));
				// remove virtual element
				virtualElement = document.getElementById(ul.id + 'virtualElement');
				if (virtualElement) virtualElement.parentNode.removeChild(virtualElement);
			} else
			{
				// add virtual element
				ul.innerHTML = '<div id="' + ul.id + 'virtualElement" class="virtualElement">Ban co the keo module chuc nang vao day</div>';
			}
			
		}
	}
	cookieValue = containerArr.join('$'); // format: containerId1-moduleId1,moduleId2$containerId2-moduleId3,moduleId4
	return cookieValue;
}

// reset layout like server return
function resetLayout()
{
	var cookieName = 'PortalLayout_' + window.location.href; // each page has one record
	var cookieValue = defaultCookieForLayout;
	createCookie(cookieName, cookieValue, 30); // 30 days
	renderLayoutFromCookie(cookieValue);
}

function setVirtualSpace()
{
	var i, j;
	// find container there are no module to add virtual space
	var hasModule = false;
	var uls = document.getElementsByTagName('ul');
	var ul;
	var lis;
	var li;
	
	for (i=0; i<uls.length; i++)
	{
		ul= uls[i];
		if (ul.id.indexOf(dragcontainerIdFormat)==0)
		{
			hasModule = false;
			lis = ul.getElementsByTagName('li');
			for (j=0; j<lis.length; j++)
			{
				li = lis[j];
				if (li.id.indexOf(dragmoduleIdFormat)==0)
				{
					hasModule = true;
					break;
				}
			}
			// has no module, add virtual space
			if (!hasModule)
				// add virtual element
				ul.innerHTML = vitualSpaceHTML;
			else
			{
				// remove space
				for (j=0; j<lis.length; j++)
				{
					li = lis[j];
					if (li.className=='virtualElement')
					{
						ul.removeChild(li);
						break;
					}
				}
			}
		}
	}
}

// render layout, called on 'window.onload'
function renderLayout()
{
	var cookieName = 'PortalLayout_' + window.location.href;
	var cookieValue = readCookie(cookieName);
	
	// before process, save default layout
	defaultCookieForLayout = getLayoutInCookieFormat();
	
	if (cookieValue)
		renderLayoutFromCookie(cookieValue);
	else
		saveLayoutToCookie();

	// init drag & drop utility
	Event.onDOMReady(YAHOO.example.DDApp.init, YAHOO.example.DDApp, true);
	// add layout-control: move up-down, drag
	SetLayoutControl();
}


