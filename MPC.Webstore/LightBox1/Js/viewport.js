// JavaScript Document
var theme_list_open = false;
$(function() {
	$('#iphone').click(function() {
		$("#screen-options li").removeClass("active");
		$(this).addClass("active");
		$('#viewport').attr('class', 'iphone');
		$('#device-detail').attr('class', 'iphone-detail');
	});

	$('#iphone-l').click(function() {
		$("#screen-options li").removeClass("active");
		$(this).addClass("active");
		$('#viewport').attr('class', 'iphone-l');
		$('#device-detail').attr('class', 'iphone-l-detail');
	});

	$('#ipad').click(function() {
		$("#screen-options li").removeClass("active");
		$(this).addClass("active");
		$('#viewport').attr('class', 'ipad');
		$('#device-detail').attr('class', 'ipad-detail');
	});

	$('#ipad-l').click(function() {
		$("#screen-options li").removeClass("active");
		$(this).addClass("active");
		$('#viewport').attr('class', 'ipad-l');
		$('#device-detail').attr('class', 'ipad-l-detail');
	});

	$('#desktop').click(function() {
		$("#screen-options li").removeClass("active");
		$(this).addClass("active");
		$('#viewport').attr('class', 'desktop');
		$('#device-detail').attr('class', 'desktop-detail');
	});
	
	$("#ops_theme_dropdown #ops_theme_list #ops_theme_select").click(function (){
		if (theme_list_open == true) {
			$("#ops_theme_dropdown #ops_theme_list ul").hide();
			theme_list_open = false;
		} else {
		
		$("#ops_theme_dropdown #ops_theme_list ul").show();               
			theme_list_open = true;
		}
		return false;
	});
	
	$('#close').click(function() {
		window.location = $("#iframe").attr("src");
	});
	
	$("#ops_theme_dropdown #ops_theme_list ul li a").click(function (){
		var theme_data = $(this).attr("rel").split(",");
		$("#themePurchase a").attr("href", theme_data[1]);
		$("#themeRemoveFrame a").attr("href", theme_data[0]);
		$("#iframe").attr("src", theme_data[0]);
		$("#ops_theme_dropdown #ops_theme_list #ops_theme_select").text($(this).find('.ops_theme_name').text());
		$("title").text('Preview | ' + $(this).find('.ops_theme_name').text());
		$("#ops_theme_dropdown #ops_theme_list ul").hide();
		theme_list_open = false;
		return false;
	});

});
