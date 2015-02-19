﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RSSWidget.ascx.cs" Inherits="Web2Print.UI.Controls.RSSWidget" %>
<script type="text/javascript" src="scripts/FeedEk.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#divRss').FeedEk({
            FeedUrl: 'http://blog.pinkcards.com/feed/',
            MaxCount: 5,
            ShowDesc: false
        });


    });    

</script>

<div class="RSSContainer">
<div class="RSSWidgetPRE">
</div>
<div class="RSSWidget">
    <h2 class="rssWidgetHeading">
        Our  <strong>charity blog</strong></h2>
    <div id="divRss">
    </div>
    <div class="clearBoth">
    </div>

    <div class="fbLikeWidget">
    <div id="fb-root"></div>
    <script>    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=633970343333969";
        fjs.parentNode.insertBefore(js, fjs);
    } (document, 'script', 'facebook-jssdk'));</script>
   <div class="fb-like" data-href="https://www.facebook.com/pinkcards" data-width="150" data-layout="button_count" data-action="like" data-show-faces="true" data-share="true"></div>
    </div>

</div>
<div class="RSSWidgetPOST">
</div>
    <div class="clearBoth">

    </div>
</div>