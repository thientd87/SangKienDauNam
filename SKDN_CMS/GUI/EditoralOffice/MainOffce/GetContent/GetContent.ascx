<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GetContent.ascx.cs" Inherits="GafinCMS.GUI.EditoralOffice.MainOffce.GetContent.GetContent" %>
<style>
        ul{list-style-type:none}
        label{float:left;display:block;width:50px;line-height:24px}
        input[type='text'], input[type='password'], textarea { border: 1px solid #B6BFC6; width: 400px; padding: 7px 5px; -moz-border-radius: 3px; -webkit-border-radius: 3px; margin-right: 4px; }
        input[type='text']:hover, input[type='password']:hover, textarea:hover { border-color: #999; outline: 0; -moz-box-shadow: 0 0 3px #999; -webkit-box-shadow: 0 0 3px #999; -khtml-box-shadow: 0 0 3px #999; box-shadow: 0 0 3px #999; }
        input[type='button'], input[type='submit'] { background-position: 0% 0%; padding: 1px 8px 4px; height: 28px; border: 1px solid #ccc; color: #000; -moz-border-radius: 3px; -webkit-border-radius: 3px; border-radius: 3px; white-space: nowrap; vertical-align: middle; cursor: pointer; overflow: visible; outline: 0 none; background-image: -webkit-gradient(linear,left top,left bottom,from(#ffffff),to(#efefef)); background-color: #f6f6f6; background-repeat: repeat; background-attachment: scroll; margin-right: 10px; }
        input[type='button']:hover, input[type='submit']:hover { background-position: 0% 0%; border-color: #999; outline: 0; -moz-box-shadow: 0 0 3px #999; -webkit-box-shadow: 0 0 3px #999; -khtml-box-shadow: 0 0 3px #999; box-shadow: 0 0 3px #999; -ms-filter: "progid:DXImageTransform.Microsoft.Gradient(startColorStr=#FFFFFF, endColorStr=#EFEFEF)"; background: -webkit-gradient(linear,left top,left bottom,from(#ffffff),to(#ebebeb)); background: -moz-linear-gradient(top,  #ffffff,  #ebebeb); background-color: #f3f3f3; background-repeat: repeat; background-attachment: scroll; }
        #_content h3, #_content p{margin:5px 0}
        
        #images img { width: 120px; margin: 5px; float: left; min-width:120px}
        #images { overflow: hidden; float: left; margin-right: 5px; background: url(/images/fbloading.gif) no-repeat scroll 50% 50% transparent; width: 120px; display: block; min-height: 120px; }
        #images a#nextimg { width: 25px; height: 22px; background: transparent url(/images/thumb_control.png) no-repeat scroll -25px 0; display: block; text-indent: 9999px; float: left; }
        #nextimg.disable { background-position: -75px 0 !important; }
        #images a#previmg { width: 25px; height: 22px; background: transparent url(/images/thumb_control.png) no-repeat scroll 0 0; display: block; text-indent: 9999px; float: left; }
        #previmg.disable { background-position: -50px 0 !important; }
        
</style>
<ul>
    <li><asp:TextBox ID="txtLink" runat="server"></asp:TextBox><br /><br /></li>
    <li id="_content"></li>
    <li><label>&nbsp;</label><asp:Label ID="lblMess" runat="server" Text="" ForeColor="Red"></asp:Label></li>
</ul>
<script type="text/javascript">
    $(function () {
        var txt = $('#<%=txtLink.ClientID %>');
        var _arr;
        var imgIndex = 0;

        txt.focus(function () {
            $(this).val('');
        });

        txt.bind('paste', function (e) {
            var el = $(this);

            setTimeout(function () {
                if (validUrl(el.val())) {
                    el.after('<img src=\'/images/loading.gif\' width=\'20px\' id=\'loading\'>');
                    $.ajax({
                        type: 'POST',
                        url: '/Ajax/ActionService.asmx/GetLinkContent',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: '{\'Link\' : \'' + el.val() + '\'}',
                        success: function (response) {
                            var result = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;

                            if (result.error == null) {
                                $('#_content').html('<div id="images"></div><h3>' + result.title + '</h3><p>' + result.sapo + '</p><li><input type="button" value="Copy" id="btnCopy"></li>');
                                _complete();
                            }
                            else
                                $('#_content').html(result.error);

                            $('#loading').fadeOut(300).remove();
                        }
                    });
                }
            }, 100);

        });
        
        _complete = function () {
            _arr = new Array();

            $.ajax({
                type: 'GET',
                url: '/ajax/Images.aspx',
                data: { u: txt.val() },
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    var _items = $(response.images);

                    if (_items.length > 0) {
                        _items.each(function (i, val) {
                            _arr.push(val);
                        });
                    }

                    if (_arr.length > 0) {
                        $("<img/>").attr("src", _arr[imgIndex]).appendTo("#images");
                        $("#images").append('<a href=\'#\' id=\'previmg\'>Prev</a><a href=\'#\' id=\'nextimg\'>Next</a><div style=\'float:left\'><input type=\'checkbox\' id=\'no_image\'/>&nbsp;Không cần ảnh</div>');
                        bindControlEvents();
                    }
                    else
                        $('#images').fadeOut('slow').remove();
                }
            });

            //            $.getJSON('/ajax/Images.aspx', { u: txt.val() }, function (data) {
            //                alert(data);
            //                var items = $(data.images);

            //                if (items.length > 0) {
            //                    items.each(function (i, val) {
            //                        _arr.push(val);
            //                    });
            //                }

            //                if (_arr.length > 0) {
            //                    $("<img/>").attr("src", _arr[imgIndex]).appendTo("#images");
            //                    $("#images").append('<a href=\'#\' id=\'previmg\'>Prev</a><a href=\'#\' id=\'nextimg\'>Next</a><div style=\'float:left\'><input type=\'checkbox\' id=\'no_image\'/>&nbsp;Không cần ảnh</div>');
            //                    bindControlEvents();
            //                }
            //                else
            //                    $('#images').fadeOut('slow').remove();
            //            });

            $('#btnCopy').click(function () {
                $(this).after('<img src=\'/images/loading.gif\' width=\'20px\' id=\'loading\'>');
                var img = $('#no_image').is(":checked") ? '' : $('#images img').attr('src');
                $.ajax({
                    type: 'POST',
                    url: '/Ajax/ActionService.asmx/CopyFromLink',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{\'Link\' : \'' + $('#<%=txtLink.ClientID %>').val() + '\', \'img\': \'' + img + '\' }',
                    success: function (response) {
                        var result = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                        if (result.status == 'ok')
                            location.href = '/office/add,templist/' + result.newsid + '.aspx';
                        else
                            alert(result.message);
                    }
                });
            });
        };

        bindControlEvents = function () {
            $("#images a#previmg").click(function (e) {
                e.preventDefault();
                $("#images").find('a.disable').removeClass('disable');
                imgIndex = (imgIndex == 0) ? imgIndex : imgIndex - 1;

                if (imgIndex == 0)
                    $(this).addClass('disable');

                $("#images > img").attr('src', _arr[imgIndex]);
            });
            $("#images a#nextimg").click(function (e) {
                e.preventDefault();
                $("#images").find('a.disable').removeClass('disable');
                imgIndex = (imgIndex == _arr.length - 1) ? imgIndex : imgIndex + 1;

                if (imgIndex == _arr.length - 1)
                    $(this).addClass('disable');

                $("#images > img").attr('src', _arr[imgIndex]);
            });
        };

        validUrl = function (url) {
            var patt = /http:///g;

            return patt.test(url);
        };

        getQueryString = function (key) {
            key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + key + "=([^&#]*)", 'ig');
            var qs = regex.exec(window.location.href);
            if (qs == null)
                return '';
            else
                return qs[1];
        };

        if (getQueryString('u') != '') {
            txt.val(getQueryString('u'));
            txt.trigger('paste');
        }
    });    
</script>
