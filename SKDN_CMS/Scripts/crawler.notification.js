/*
 * Jquery crawler notification for Gafin
 *
 * Author: @tuanva
 *
 */
(function ($) {
    jQuery.fn.reverse = function () {
        return this.pushStack(this.get().reverse(), arguments);
    };
    $.fn.extend({
        notification: function (options) {
            var defaults = {
                enable: true,
                interval: 30000
            }
            var hide = false, _f = 1, _container;
            options = $.extend(defaults, options);

            init = function () {
                _container = $('#crawler').length > 0 ? $('#crawler') : $('body').append('<div id="crawler"><h3></h3><div id="btn"><a href="/office/crawler.aspx" title="Xem toàn bộ"><img src="/images/application_view_list.png" border="0"></a><a href="#" title="Ẩn/hiện box" id="showhide"></a></div><ul></ul></div>').find('#crawler');

                $('#showhide').click(function (e) {
                    e.preventDefault();
                    if (hide) {
                        _container.animate({
                            'height': '50px'
                        }, 'slow');
                        hide = false;
                        $(this).removeClass('hide');
                    }
                    else {
                        _container.animate({
                            'height': '310px'
                        }, 'slow');

                        $(this).toggleClass('hide');
                        hide = true;
                    }
                });

                getData();
                _f = 0;

                _container.animate({
                    opacity: 'show'
                });

            };

            getData = function () {
                var _c = $('#crawler ul');

                $.ajax({
                    type: 'POST',
                    url: "/ajax/ActionService.asmx/LatestCrawlerNews",
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (response) {
                        var result = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;

                        var _items = (_f == 1) ? $(result.rows) : $(result.rows).reverse();

                        _items.each(function (index, val) {
                            var _currentItem = '#' + val.cols[0].ID + '';

                            //console.log(val.cols[1].Title + ' - ' + index);
                            
                            if ($(_currentItem).length == 0) {
                                if (_f == 1)
                                    _c.append('<li><a href="/office/get-web-content.aspx?u=' + val.cols[2].News_Source + '" id="' + val.cols[0].ID + '" target="_blank">' + val.cols[1].Title + '</a>&nbsp;<a href="' + val.cols[2].News_Source + '" target="_blank" title="Xem trang gốc"><img src="http://images.gafin.vn/Images/url_icon.gif" border="0"></a></li>');
                                else {
                                    //console.log(val.cols[1].Title + ' - ' + index);
                                    _c.prepend('<li><a href="/office/get-web-content.aspx?u=' + val.cols[2].News_Source + '" id="' + val.cols[0].ID + '" target="_blank">' + val.cols[1].Title + '</a>&nbsp;<a href="' + val.cols[2].News_Source + '" target="_blank" title="Xem trang gốc"><img src="http://images.gafin.vn/Images/url_icon.gif" border="0"></a></li>');
                                }
                            }
                            if (index == (_items.length - 1))
                                _container.find('h3').html('<a href="/office/get-web-content.aspx?u=' + val.cols[2].News_Source + '" id="' + val.cols[0].ID + '" target="_blank">' + val.cols[1].Title + '</a>&nbsp;<a href="' + val.cols[2].News_Source + '" target="_blank" title="Xem trang gốc"><img src="http://images.gafin.vn/Images/url_icon.gif" border="0"></a>');
                        });
                    }
                });
            };

            return this.each(function () {
                init();
                var o = options;

                setInterval(function () {
                    getData();
                }, o.interval);
            });
        }
    });
})(jQuery);
