	<!--
	//-------------------------------------------------------------
    // Select all the checkboxes (Hotmail style)
    //-------------------------------------------------------------
    function AssignRelatedNews(elem,val,valtitle,isnew)
    {
         //
        var parentelem=this.opener.document.getElementById(elem);
        var parentelemtitle=this.opener.document.getElementById(elem + "Title");        
        if(parentelem.value=='')
        {
            parentelem.value=val;
            parentelemtitle.value= document.getElementById(valtitle).value;    
                           
         }
        else
        {
            if(val !='')
            {
                parentelem.value = parentelem.value+","+val;
                parentelemtitle.value +=  document.getElementById(valtitle).value;
            }            
        }
    }
    
    
    function AssignNewsToMenu(hid_id,title_id,value_id,value_title)
    {
        var hid_id_object = this.opener.document.getElementById(hid_id);
        var title_id_object = this.opener.document.getElementById(title_id);
        
        
        hid_id_object.value = value_id;
        title_id_object.value =  value_title;
        
    }
    
    
    function ClearRelated(elem)
    {
        var parentelem=this.opener.document.getElementById(elem);
        //alert(elem);
        if(confirm('Bạn có muốn xoá các tin liên quan đã đưa vào trước đó không?'))
            parentelem.value='';
        return false;
    }
    
//    function openpreview_a(sUrl,w,h)
//    {
//        var winX = 0;
//	    var winY = 0;
//	    if (parseInt(navigator.appVersion) >= 4)
//	    {
//		    winX = (screen.availWidth - w)*.5;
//		    winY = (screen.availHeight - h)*.5;
//	    }
//	    popupLoadnWin=window.open(sUrl,'popupLoadnWin_a','scrollbars,resizable=no,status=yes, width=' + w + ',height=' + h + ',left=' + winX + ',top=' + winY);
//    }    
//    
    function openpreviewHiddenStatus(sUrl,w,h)
    {
        var winX = 0;
	    var winY = 0;
	    if (parseInt(navigator.appVersion) >= 4)
	    {
		    winX = (screen.availWidth - w)*.5;
		    winY = (screen.availHeight - h)*.5;
	    }
	    popupLoadnWin=window.open(sUrl,'popupLoadnWin','scrollbars,resizable=no,status=no, width=' + w + ',height=' + h + ',left=' + winX + ',top=' + winY);
    }
    function openpreviewOtherPopup(sUrl,w,h)
    {
        var winX = 0;
	    var winY = 0;
	    if (parseInt(navigator.appVersion) >= 4)
	    {
		    winX = (screen.availWidth - w)*.5;
		    winY = (screen.availHeight - h)*.5;
	    }
	    popupLoadnWin=window.open(sUrl,'otherPopUp','scrollbars,resizable=no,status=no, width=' + w + ',height=' + h + ',left=' + winX + ',top=' + winY);
    }
    function CheckAll()
    {
        var chkAll=document.getElementById('chkAll');
        var xState = chkAll.checked;
        var elm=document.forms[0].elements;
        for(i=0;i<elm.length;i++)
        if(elm[i].type=="checkbox"&&elm[i].id!=chkAll.id)
        {
            if(elm[i].checked!=xState)
            elm[i].click();
        }
    }
    function GoselectAll()
    {
        var chkAll=document.getElementById('chkAll');
        if(!chkAll.checked)
            chkAll.click();
            
        return false;
    }
     function GoUnselectAll()
    {
        var chkAll=document.getElementById('chkAll');
        if(chkAll.checked)
            chkAll.click();
        
        return false;
    }
    function SelectAllCheckboxes(spanChk){
    
    // Added as ASPX uses SPAN for checkbox 
    /*var oItem = spanChk.children;
    var theBox=oItem.item(0)
    xState=theBox.checked;    */
    var xState = spanChk.checked;

        elm=spanChk.form.elements;
        for(i=0;i<elm.length;i++)
        if(elm[i].type=="checkbox" && elm[i].id!=spanChk.id)
            {
            //elm[i].click();
            if(elm[i].checked!=xState)
            elm[i].click();
            //elm[i].checked=xState;
            }
    }
    
    //-------------------------------------------------------------
    //----Select highlish rows when the checkboxes are selected
    //
    // Note: The colors are hardcoded, however you can use 
    //       RegisterClientScript blocks methods to use Grid's
    //       ItemTemplates and SelectTemplates colors.
    //         for ex: grdEmployees.ItemStyle.BackColor OR
    //                 grdEmployees.SelectedItemStyle.BackColor
    //-------------------------------------------------------------
    function HighlightRow(chkB)    {
   /* var oItem = chkB.children;
    xState=oItem.item(0).checked;    */
    var xState = chkB.checked;
    if(xState)
        {chkB.parentElement.parentElement.style.backgroundColor=backgroundColorSel;
           // grdEmployees.SelectedItemStyle.BackColor
         chkB.parentElement.parentElement.style.color=colorSel; 
           // grdEmployees.SelectedItemStyle.ForeColor
        }else 
        {chkB.parentElement.parentElement.style.backgroundColor=backgroundColorDef;
             //grdEmployees.ItemStyle.BackColor
         chkB.parentElement.parentElement.style.color=colorDef; 
             //grdEmployees.ItemStyle.ForeColor
        }
    }
    
    // Check to see if any check box in name chkMultidelete is checked
	// If not then return false
	function isAnySelected()
	{
		var els = document.forms[0].elements;
		for (var i = 0;i < els.length;i++)
		{
			if (els[i].name.indexOf("chkMultiDelete")>=0)
			{
				if (els[i].checked) return true;
			}
		}
		return false;
	}

	// Call isAnySelected() function and show alert message if false
	// and show confirm dialog if true
	function checkSelectedDeleteItems()
	{
		if (!isAnySelected()) {
			alert('You have to select at least 1 item to delete !');
			return false;
		}
		else
			return confirm('Are you sure you want to delete these item ?');
	}

	function RowSelector_SelectAll( parentCheckBox ) {
		
		if ( typeof( document.getElementById ) == "undefined" ) return;	    
		if ( parentCheckBox == null || typeof( parentCheckBox.participants ) == "undefined" ) {
			return;
		}
	    
		var participants = parentCheckBox.participants;	    
		for ( var i=0; i < participants.length; i++ ) {
			var participant = participants[i];
			if ( participant != null ) {				
				participant.checked = parentCheckBox.checked;
				HighlightRow(participant);
			}
		}
    }
	

	function RowSelector_Register( parentName, childName ) {
		if ( typeof( document.getElementById ) == "undefined" ) return;
	    
		var parent = document.getElementById( parentName );
		var child = document.getElementById( childName );
	    
		if ( parent == null || child == null ) {
			return;
		}
	    
		if ( typeof( parent.participants ) == "undefined" ) {
			parent.participants = new Array();
		}
			
		parent.participants[parent.participants.length] = child;
	}

	function RowSelector_CheckChildren( parentName, chkSelectedBox ) {
		if ( typeof( document.getElementById ) == "undefined" ) return;
	    
		var parent = document.getElementById( parentName );
		if ( parent == null || typeof( parent.participants ) == "undefined" ) return;
	    
	    HighlightRow(chkSelectedBox);
	    
		var participants = parent.participants;
		for ( var i=0; i < participants.length; i++ ) {
			var participant = participants[i];
			if ( participant != null && !participant.checked ) {
					parent.checked = false;					
					return;
			}
		}
		
		parent.checked = true;
	}

    // -->