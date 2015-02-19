﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TwitterWidget.ascx.cs"
    Inherits="Web2Print.UI.Controls.TwitterWidget" %>
<%@ Import Namespace="LinqToTwitter" %>
<%
    
    try
    {


        SingleUserAuthorizer auth = new SingleUserAuthorizer
        {
            Credentials = new SingleUserInMemoryCredentials
            {
                ConsumerKey = "oPOzveUpzpxyGfRo7bqA",
                ConsumerSecret = "Qf6bKqB1Y0dxP4iBhMVkSVqgINvB8NXBicuRTUE",
                TwitterAccessToken = "2214277634-tLE2kknUz6dV4EBMcNpvJfbmM6dlRMRIfyUWObX",
                TwitterAccessTokenSecret = "L2eoX2zjTqjrr7AgpcCgUvfhAy7yVSlbwhaXuAZXAP3zX"
            }
        };

        var twitterCtx = new TwitterContext(auth);



        var myMentions = (
             from tweeets in twitterCtx.Status
             where tweeets.Type == StatusType.Home && tweeets.Count == 1
             select tweeets).SingleOrDefault();


        Literal1.Text = GetFormattedTweet(myMentions.Text);

    }
    catch (Exception)
    {

        Literal1.Text = "Error loading tweets, try again in few moments";
    }   
    
    //foreach (var Status in myMentions)
    //{
    //    Response.Write(Status.Text);

    //}
    
    
    
    
    
        
%>
<section class="twitter-feed"><blockquote><p><asp:Literal ID="Literal1" runat="server"></asp:Literal></p></blockquote><cite>
              @mypinkcards</cite>
              <iframe title="Twitter Follow Button" class="twitter-follow-button twitter-follow-button" id="twitter-widget-0" src="http://platform.twitter.com/widgets/follow_button.1384994725.html#_=1385726659254&amp;id=twitter-widget-0&amp;lang=en&amp;screen_name=mypinkcards&amp;show_count=false&amp;show_screen_name=true&amp;size=m" frameborder="0" scrolling="no" style="width: 162px; height: 20px;" allowtransparency="true" data-twttr-rendered="true"></iframe><script>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        !function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "//platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } } (document, "script", "twitter-wjs");</script>
    </section>
