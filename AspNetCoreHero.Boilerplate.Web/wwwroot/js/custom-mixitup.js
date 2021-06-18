(function($){
    
    "use strict";

var config = {
  controls: {
    scope: 'local'
  }
};

$(".mixi").each(function () {
    var $self = $(this);
       
    mixitup($self, config);
});
jQuery( 'ul.post-category li:first-child' ).trigger( 'click' );

}(jQuery));