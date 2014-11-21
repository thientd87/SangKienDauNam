; (function($) {
    $.fn.autosave = function(options) {
        var opts = $.extend({}, $.fn.autosave.options, options);
        var i = 0;

        setInterval(function() {
            $.fn.autosave.go();
        }, $.fn.autosave.options.interval);
    };

    $.fn.autosave.options = {
        'interval': 30000
    };

    $.fn.autosave.go = function() {
        var content = document.getElementById('idContentoEdit_tab$ctl15$ctl00$NewsContent').contentWindow.document.body.innerHTML;
        var title = $('#' + prefix + '_txtTitle').val();
        var sapo = $('#' + prefix + '_txtInit').val();
        var image = $('#' + prefix + '_txtSelectedFile').val();
        var catid = $('#' + prefix + '_lstCat').val();
        var newsid = $('#' + prefix + '_hidNewsID').val();

        $('.info_div').html('Saving...');
        $('.info_div').slideDown("slow");

        setTimeout(function() {
            $('.info_div').slideUp("slow");
        }, 4000);

        $.post('/Ajax/AutoSave.ashx', { news_id: newsid, cat_id: catid, news_title: title, news_content: content, sapo: sapo, image: image },
        function(data) {
            $('.info_div').html(data);
            $('.info_div').slideDown("slow");

            setTimeout(function() {
                $('.info_div').slideUp("slow");
            }, 4000);

        });

    };
})(jQuery);
