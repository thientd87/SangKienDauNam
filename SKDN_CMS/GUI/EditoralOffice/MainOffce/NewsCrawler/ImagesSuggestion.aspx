<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImagesSuggestion.aspx.cs"
    Inherits="Portal.GUI.EditoralOffice.MainOffce.NewsCrawler.ImagesSuggestion" EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.2/jquery.min.js"></script>--%>
    <style>
        ul { list-style-type: none; margin: 0; padding: 0; }
        #ctn { font-size: 13px; }
        #ctn h3 { float: left; margin: 5px 0; width: 350px; }
        #ctn p { float: left; width: 350px; }
        #images img { width: 150px; margin: 5px; float: left; min-width:150px;min-height:150px}
        #ctn #images { overflow: hidden; float: left; margin-right: 5px; background: url(/images/fbloading.gif) no-repeat scroll 50% 50% transparent; width: 150px; display: block; min-height: 150px; }
        #images a#nextimg { width: 25px; height: 22px; background: transparent url(/images/thumb_control.png) no-repeat scroll -25px 0; display: block; text-indent: 9999px; float: left; }
        #nextimg.disable { background-position: -75px 0 !important; }
        #images a#previmg { width: 25px; height: 22px; background: transparent url(/images/thumb_control.png) no-repeat scroll 0 0; display: block; text-indent: 9999px; float: left; }
        #previmg.disable { background-position: -50px 0 !important; }
        #ctn #buttons{position:absolute;right:10px;bottom:10px}
        #preload{display:none}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 550px; height: 180px" id="ctn">
        <div id="images"></div>
        <h3><asp:Literal ID="ltrTitle" runat="server"></asp:Literal></h3>
        <p><asp:Literal ID="ltrSapo" runat="server"></asp:Literal></p>
        <div id="preload"></div>
        <div id="buttons">
            <input type="button" value="Copy" id="btnCopy"/>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $(function () {
            var _arr = new Array();
            var imgIndex = 0;

            $.getJSON('/ajax/Images.aspx?ID=<%=Request.QueryString["ID"].ToString() %>', function (data) {
                var items = $(data.images);
                $("#images").html('');
                //var img;

                //console.log(items);

                if (items.length > 0) {
                    items.each(function (i, val) {
                        _arr.push(val);
                    });
                }

                //console.log(_arr);
                if (_arr.length > 0) {
                    $("<img/>").attr("src", _arr[imgIndex]).appendTo("#images");
                    $("#images").append('<a href=\'#\' id=\'previmg\'>Prev</a><a href=\'#\' id=\'nextimg\'>Next</a>');
                    bindControlEvents();
                }
                else {
                    $('#images').fadeOut('slow').remove();
                }
            });

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

            checkImages = function () {
                var imgs = $('#preload img');

                imgs.each(function (index, val) {
                    var el = $(this);
                    //console.log(el.width() + ' ' + el.height());

                    if (el.width() < 100 && el.height() < 100)
                        el.remove();
                    else {
                        _arr.push(el.attr('src'));
                    }
                });

                if (_arr.length > 0) {
                    $("<img/>").attr("src", _arr[imgIndex]).appendTo("#images");
                    $("#images").append('<a href=\'#\' id=\'previmg\'>Prev</a><a href=\'#\' id=\'nextimg\'>Next</a>');
                    bindControlEvents();
                }
            };

            $('#btnCopy').click(function () {
                $.ajax({
                    type: 'POST',
                    url: '/Ajax/ActionService.asmx/CopyNews',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{\'news_id\' : \'<%=Request.QueryString["ID"].ToString() %>\', \'news_image\' : \'' + $("#images > img").attr('src') + '\'}',
                    success: function (response) {
                        var result = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                        location.href = result;
                    }
                });
            });

            //only working on the same domain
            getImageFileSize = function (url) {
                var xhr = new XMLHttpRequest();
                xhr.open('HEAD', url, true);
                xhr.onreadystatechange = function () {
                    if (xhr.readyState == 4) {
                        if (xhr.status == 200) {
                            alert('Size in bytes: ' + xhr.getResponseHeader('Content-Length'));

                            return xhr.getResponseHeader('Content-Length');
                        } else {
                            return 'error';
                        }
                    }
                };
                xhr.send(null);
            };
        });
    </script>
</body>
</html>
