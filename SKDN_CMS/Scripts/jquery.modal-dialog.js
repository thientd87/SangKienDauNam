(function($) {
  $.fn.dialog = function() {
        var id = "#modalwindow";
        $('#modalwindow h1').html("Copy bài");
        // Determine browser windows dimensions. 
        var maskHeight = $(document).height();
        var maskWidth = $(window).width();
        
        // Set dimensions for the mask to opaque the screen when the modal  window is displayed.
        $('#mask').css({ 'width': maskWidth, 'height': maskHeight });
        
        // Make the Window Opaque
        $('#mask').fadeIn("fast");
        $('#mask').fadeTo("slow", 0.8);

        //Get the window height and width
        var winH = $(window).height();
        var winW = $(window).width();

        //$(id).css('top', winH / 2 - $(id).height() / 2);
        $(id).css('top', $(id).offset().top + 99) ;
        $(id).css('left', winW / 2 - $(id).width() / 2);
        
        $('#frameEditor').attr('src', "/PublishToOtherSites.aspx?NewsID=" + hdNewsID.value);
        
        // Show the Modal Window
        $(id).fadeIn("fast"); 
  }
})(jQuery);