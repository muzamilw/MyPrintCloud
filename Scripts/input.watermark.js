(function ($) {
    $.fn.extend({
        inputWatermark: function () {
            return this.each(function () {
                // retrieve the value of the ‘placeholder’ attribute
                var watermarkText = $(this).attr('placeholder');
                var $this = $(this);
                if ($this.val() === '') {
                    $this.val(watermarkText);
                    // give the watermark a translucent look
                   // $this.css({ 'opacity': '0.65' });
                }



                $this.blur(function () {
                    if ($this.val() === '') {
                        // If the text is empty put the watermark
                        // back

                        $this.val(watermarkText);
                        // give the watermark a translucent look
                        //$this.css({ 'opacity': '0.65' });
                    }
                });



                $this.focus(function () {
                    if ($this.val() === watermarkText) {
                        $this.val('');
                        //$this.css({ 'opacity': '1.0' });
                    }
                });
            });
        }
    });

})(jQuery);