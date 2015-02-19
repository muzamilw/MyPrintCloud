<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeBehind="TemplateSelection.aspx.cs"
    Inherits="Web2Print.UI.TemplateSelection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<%@ Register Src="Controls/WhyChooseUs.ascx" TagName="WhyChooseUs" TagPrefix="uc4" %>
<%@ Register Src="Controls/LoginControl.ascx" TagName="LoginControl" TagPrefix="uc2" %>
<%@ Register Src="Controls/MatchingSet.ascx" TagName="MatchingSet" TagPrefix="uc3" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContents" runat="server">
</asp:Content>
<asp:Content ID="ProductDetailsContents" ContentPlaceHolderID="MainContent" runat="server">
    <script src="/Scripts/input.watermark.js" type="text/javascript"></script>
    <script type="text/javascript">
        var pageCount = 0;
        var selectedMatchingSets = null;
        var templateName = "";
        var QTD = null;
        var QtDAta = null;
        $(document).ready(function () {

            $('#Footer').css("display", "none");
            showProgress();
            var globalCategoryName = $('#<%=txtTemplateDesignerMappedCategoryName.ClientID %>').val();
            var CustomerID = $('#<%=txtCustomerID.ClientID %>').val();
            var searchText = $('#<%=txtSearchKeywords.ClientID %>').val();

            if (searchText == "Search Phrase") {

                searchText = "";
            }
            var cmbSelectedIndex = $('#<%=hfSelectedCat.ClientID %>').val();
            var basecolorid = $("#<%=hfBaseColorID.ClientID %>").val();

            Web2Print.UI.Services.WebStoreUtility.GetTemplatesbyMultipleSelection(pageCount, cmbSelectedIndex, globalCategoryName, CustomerID, searchText, basecolorid, OnSuccess);
            pageCount += 1;
            //            setTimeout(function () {
            //                Web2Print.UI.Services.WebStoreUtility.GetTemplatessForTemplateSel(pageCount, cmbSelectedIndex, globalCategoryName, CustomerID, searchText, OnSuccess);
            //            }, 1000)

            if (cmbSelectedIndex != "0") {

                cmbSelectedIndex = cmbSelectedIndex.replace("?", "|");

                var arryOFTags = cmbSelectedIndex.split("|");

                $.each(arryOFTags, function (i, element) {
                    $('input[type=checkbox]').each(function () {
                        var TagId = $(this).attr("data-TagID");
                        if (TagId == element) {
                            $(this).attr('checked', true);
                            $(this).parent().addClass('SquareTickBox');
                            $(this).parent().removeClass('SquareBox1');
                            return;
                        }
                    });
                });
            }

            if (basecolorid != "0") {

                var ColorID = basecolorid.split("|");

                $('input:radio[name=BaseColorsRadioList]').each(function () {
                    var TagId = $(this).attr("data-BaseColorID");
                    if (TagId == ColorID[1]) {
                        $(this).parent().parent().addClass('ShadowToBaseColor'); //.css("background-color", "#f3f3f3");
                        return;
                    }
                });
            }

            if ($('#<%=hfContactID.ClientID %>').val() > 0) {
                $.getJSON("../services/Webstore.svc/FavDesignList?contactID=" + $('#<%=hfContactID.ClientID %>').val(),
		        function (xdata) {
		            selectedMatchingSets = xdata;
		        });
            }

            $("#<%=ToogleBaseColors.ClientID %>").click(function () {
                $("#<%=BaseColorsContainer.ClientID %>").slideToggle();
            });
            $("#<%=ToogleIndustryTypes.ClientID %>").click(function () {
                $('#<%=IndustryElementsContainer.ClientID %>').slideToggle();
            });
            var CheckLogin = '<%=IsLogin %>';
            if (parseInt(CheckLogin) > 0) {
                //return true;
                ContactID = $('#<%=txtContactID.ClientID %>').val();
                Cid = $('#<%=txtQCustomerID.ClientID %>').val();
                $.getJSON("../services/Webstore.svc/getquicktext?Customerid=" + Cid + "&contactid=" + ContactID,
		        function (xdata) {
		            QTD = xdata;
		            QtDAta = xdata;
		            if (QTD.Name == "" || QTD.Name == null) {
		                QTD.Name = "Your Name"
		            }
		            if (QTD.Title == "" || QTD.Title == null) {
		                QTD.Title = "Your Title"
		            }
		            if (QTD.Company == "" || QTD.Company == null) {
		                QTD.Company = "Your Company Name"
		            }
		            if (QTD.CompanyMessage == "" || QTD.CompanyMessage == null) {
		                QTD.CompanyMessage = "Your Company Message"
		            }
		            if (QTD.Address1 == "" || QTD.Address1 == null) {
		                QTD.Address1 = "Address Line 1"
		            }
		            if (QTD.Telephone == "" || QTD.Telephone == null) {
		                QTD.Telephone = "Telephone / Other"
		            }
		            if (QTD.Fax == "" || QTD.Fax == null) {
		                QTD.Fax = "Fax / Other"
		            }
		            if (QTD.Email == "" || QTD.Email == null) {
		                QTD.Email = "Email address / Other"
		            }
		            if (QTD.Website == "" || QTD.Website == null) {
		                QTD.Website = "Website address"
		            }
		            $("#txtQName").val(QTD.Name);
		            $("#txtQTitle").val(QTD.Title);
		            $("#txtQCompanyName").val(QTD.Company);
		            $("#txtQCompanyMessage").val(QTD.CompanyMessage);
		            $("#txtQAddressLine1").val(QTD.Address1);
		            $("#txtQPhone").val(QTD.Telephone);
		            $("#txtQFax").val(QTD.Fax);
		            $("#txtQEmail").val(QTD.Email);
		            $("#txtQWebsite").val(QTD.Website);

		            $("#txtQName").attr("placeholder", QTD.Name);
		            $("#txtQTitle").attr("placeholder", QTD.Title);
		            $("#txtQCompanyName").attr("placeholder", QTD.Company);
		            $("#txtQCompanyMessage").attr("placeholder", QTD.CompanyMessage);
		            $("#txtQAddressLine1").attr("placeholder", QTD.Address1);
		            $("#txtQPhone").attr("placeholder", QTD.Telephone);
		            $("#txtQFax").attr("placeholder", QTD.Fax);
		            $("#txtQEmail").attr("placeholder", QTD.Email);
		            $("#txtQWebsite").attr("placeholder", QTD.Website);

		            var addEvent = function (elem, type, fn) {
		                if (elem.addEventListener) elem.addEventListener(type, fn, false);
		                else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
		            },
        textField = document.getElementById('txtQName'),
        placeholder = QTD.Name;

		            addEvent(textField, 'focus', function () {
		                if (this.value === placeholder) this.value = '';
		            });
		            addEvent(textField, 'blur', function () {
		                if (this.value === '') this.value = placeholder;
		            });

		            var addEvent1 = function (elem, type, fn) {
		                if (elem.addEventListener) elem.addEventListener(type, fn, false);
		                else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
		            },
        textField1 = document.getElementById('txtQTitle'),
        placeholder1 = QTD.Title;

		            addEvent1(textField1, 'focus', function () {
		                if (this.value === placeholder1) this.value = '';
		            });
		            addEvent1(textField1, 'blur', function () {
		                if (this.value === '') this.value = placeholder1;
		            });

		            var addEvent2 = function (elem, type, fn) {
		                if (elem.addEventListener) elem.addEventListener(type, fn, false);
		                else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
		            },
        textField2 = document.getElementById('txtQCompanyName'),
        placeholder2 = QTD.Company;

		            addEvent2(textField2, 'focus', function () {
		                if (this.value === placeholder2) this.value = '';
		            });
		            addEvent2(textField2, 'blur', function () {
		                if (this.value === '') this.value = placeholder2;
		            });


		            var addEvent3 = function (elem, type, fn) {
		                if (elem.addEventListener) elem.addEventListener(type, fn, false);
		                else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
		            },
        textField3 = document.getElementById('txtQCompanyMessage'),
        placeholder3 = QTD.CompanyMessage;

		            addEvent3(textField3, 'focus', function () {
		                if (this.value === placeholder3) this.value = '';
		            });
		            addEvent3(textField3, 'blur', function () {
		                if (this.value === '') this.value = placeholder3;
		            });
		            var addEvent4 = function (elem, type, fn) {
		                if (elem.addEventListener) elem.addEventListener(type, fn, false);
		                else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
		            },
        textField4 = document.getElementById('txtQAddressLine1'),
        placeholder4 = QTD.Address1;

		            addEvent4(textField4, 'focus', function () {
		                if (this.value === placeholder4) this.value = '';
		            });
		            addEvent4(textField4, 'blur', function () {
		                if (this.value === '') this.value = placeholder4;
		            });
		            var addEvent5 = function (elem, type, fn) {
		                if (elem.addEventListener) elem.addEventListener(type, fn, false);
		                else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
		            },
        textField5 = document.getElementById('txtQPhone'),
        placeholder5 = QTD.Telephone;

		            addEvent5(textField5, 'focus', function () {
		                if (this.value === placeholder5) this.value = '';
		            });
		            addEvent5(textField5, 'blur', function () {
		                if (this.value === '') this.value = placeholder5;
		            });
		            var addEvent6 = function (elem, type, fn) {
		                if (elem.addEventListener) elem.addEventListener(type, fn, false);
		                else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
		            },
        textField6 = document.getElementById('txtQFax'),
        placeholder6 = QTD.Fax;

		            addEvent6(textField6, 'focus', function () {
		                if (this.value === placeholder6) this.value = '';
		            });
		            addEvent6(textField6, 'blur', function () {
		                if (this.value === '') this.value = placeholder6;
		            });
		            var addEvent7 = function (elem, type, fn) {
		                if (elem.addEventListener) elem.addEventListener(type, fn, false);
		                else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
		            },
        textField7 = document.getElementById('txtQEmail'),
        placeholder7 = QTD.Email;

		            addEvent7(textField7, 'focus', function () {
		                if (this.value === placeholder7) this.value = '';
		            });
		            addEvent7(textField7, 'blur', function () {
		                if (this.value === '') this.value = placeholder7;
		            });
		            var addEvent8 = function (elem, type, fn) {
		                if (elem.addEventListener) elem.addEventListener(type, fn, false);
		                else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
		            },
        textField8 = document.getElementById('txtQWebsite'),
        placeholder8 = QTD.Website;

		            addEvent8(textField8, 'focus', function () {
		                if (this.value === placeholder8) this.value = '';
		            });
		            addEvent8(textField8, 'blur', function () {
		                if (this.value === '') this.value = placeholder8;
		            });
		        });
            } else {
                var addEvent = function (elem, type, fn) {
                    if (elem.addEventListener) elem.addEventListener(type, fn, false);
                    else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
                },
                    textField = document.getElementById('txtQName'),
                    placeholder = "Your Name";

                addEvent(textField, 'focus', function () {
                    if (this.value === placeholder) this.value = '';
                });
                addEvent(textField, 'blur', function () {
                    if (this.value === '') this.value = placeholder;
                });

                var addEvent1 = function (elem, type, fn) {
                    if (elem.addEventListener) elem.addEventListener(type, fn, false);
                    else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
                },
        textField1 = document.getElementById('txtQTitle'),
        placeholder1 = "Your Title";

                addEvent1(textField1, 'focus', function () {
                    if (this.value === placeholder1) this.value = '';
                });
                addEvent1(textField1, 'blur', function () {
                    if (this.value === '') this.value = placeholder1;
                });

                var addEvent2 = function (elem, type, fn) {
                    if (elem.addEventListener) elem.addEventListener(type, fn, false);
                    else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
                },
        textField2 = document.getElementById('txtQCompanyName'),
        placeholder2 = "Your Company Name";

                addEvent2(textField2, 'focus', function () {
                    if (this.value === placeholder2) this.value = '';
                });
                addEvent2(textField2, 'blur', function () {
                    if (this.value === '') this.value = placeholder2;
                });


                var addEvent3 = function (elem, type, fn) {
                    if (elem.addEventListener) elem.addEventListener(type, fn, false);
                    else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
                },
        textField3 = document.getElementById('txtQCompanyMessage'),
        placeholder3 = "Your Company Message";

                addEvent3(textField3, 'focus', function () {
                    if (this.value === placeholder3) this.value = '';
                });
                addEvent3(textField3, 'blur', function () {
                    if (this.value === '') this.value = placeholder3;
                });
                var addEvent4 = function (elem, type, fn) {
                    if (elem.addEventListener) elem.addEventListener(type, fn, false);
                    else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
                },
        textField4 = document.getElementById('txtQAddressLine1'),
        placeholder4 = "Address Line 1";

                addEvent4(textField4, 'focus', function () {
                    if (this.value === placeholder4) this.value = '';
                });
                addEvent4(textField4, 'blur', function () {
                    if (this.value === '') this.value = placeholder4;
                });
                var addEvent5 = function (elem, type, fn) {
                    if (elem.addEventListener) elem.addEventListener(type, fn, false);
                    else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
                },
        textField5 = document.getElementById('txtQPhone'),
        placeholder5 = "Telephone / Other";

                addEvent5(textField5, 'focus', function () {
                    if (this.value === placeholder5) this.value = '';
                });
                addEvent5(textField5, 'blur', function () {
                    if (this.value === '') this.value = placeholder5;
                });
                var addEvent6 = function (elem, type, fn) {
                    if (elem.addEventListener) elem.addEventListener(type, fn, false);
                    else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
                },
        textField6 = document.getElementById('txtQFax'),
        placeholder6 = "Fax / Other";

                addEvent6(textField6, 'focus', function () {
                    if (this.value === placeholder6) this.value = '';
                });
                addEvent6(textField6, 'blur', function () {
                    if (this.value === '') this.value = placeholder6;
                });
                var addEvent7 = function (elem, type, fn) {
                    if (elem.addEventListener) elem.addEventListener(type, fn, false);
                    else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
                },
        textField7 = document.getElementById('txtQEmail'),
        placeholder7 = "Email address / Other";

                addEvent7(textField7, 'focus', function () {
                    if (this.value === placeholder7) this.value = '';
                });
                addEvent7(textField7, 'blur', function () {
                    if (this.value === '') this.value = placeholder7;
                });
                var addEvent8 = function (elem, type, fn) {
                    if (elem.addEventListener) elem.addEventListener(type, fn, false);
                    else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
                },
        textField8 = document.getElementById('txtQWebsite'),
        placeholder8 = "Website address";

                addEvent8(textField8, 'focus', function () {
                    if (this.value === placeholder8) this.value = '';
                });
                addEvent8(textField8, 'blur', function () {
                    if (this.value === '') this.value = placeholder8;
                });

            }

        });
        var DesignServiceUrl = '<%=TemplateDesignserviceUrl %>';
        var TemplateExist = true;
        function OnSuccess(responce) {
            if (responce != "") {
                $('#<%=hfFTemplateId.ClientID %>').val(responce[0].ProductID);
                $('#<%=hfFTempName.ClientID %>').val(responce[0].ProductName);
                if (responce.length > 3) {
                    var html = "<div>";
                    var classes = "";

                    $.each(responce, function (i, IT) {
                        var src = DesignServiceUrl + "/designer/products/" + IT.ProductID + "/TemplateThumbnail1.jpg";
                        classes += "imgDetailContainer" + IT.ProductID + " ";

                        if (IT.PageOrientation == 1 && (IT.PDFTemplateHeight > IT.PDFTemplateWidth)) {// portrait height > width
                            html += '<div class="ScrollImgContainer1"><img src="' + src + '" alt="' + IT.ProductName + '" class="ScrollerImg LCLB" id="ScrollerImg' + IT.ProductID + '" onclick="ShowTemplateBox(&#39;' + IT.ProductName + '&#39; , ' + IT.ProductID + ', ' + i + ')" /> <div class="ScrollImgTitle">' + IT.ProductName + '</div> </div>';
                        }
                        else if (IT.PageOrientation == 1 && (IT.PDFTemplateHeight <= IT.PDFTemplateWidth)) {// portrait height < width
                            html += '<div class="ScrollImgContainer2"><img src="' + src + '" alt="' + IT.ProductName + '" class="ScrollerImg LCLB" id="ScrollerImg' + IT.ProductID + '" onclick="ShowTemplateBox(&#39;' + IT.ProductName + '&#39; , ' + IT.ProductID + ', ' + i + ')" /> <div class="ScrollImgTitlePort">' + IT.ProductName + '</div> </div>';

                        }
                        else if (IT.PageOrientation == 2 && (IT.PDFTemplateHeight > IT.PDFTemplateWidth)) {// landscap height > width
                            html += '<div class="ScrollImgContainer2"><img src="' + src + '" alt="' + IT.ProductName + '" class="ScrollerImg LCLB" id="ScrollerImg' + IT.ProductID + '" onclick="ShowTemplateBox(&#39;' + IT.ProductName + '&#39; , ' + IT.ProductID + ', ' + i + ')" /> <div class="ScrollImgTitlePort">' + IT.ProductName + '</div> </div>';
                        }
                        else if (IT.PageOrientation == 2 && (IT.PDFTemplateHeight <= IT.PDFTemplateWidth)) {// landscap height > width
                            html += '<div class="ScrollImgContainer1"><img src="' + src + '" alt="' + IT.ProductName + '" class="ScrollerImg LCLB" id="ScrollerImg' + IT.ProductID + '" onclick="ShowTemplateBox(&#39;' + IT.ProductName + '&#39; , ' + IT.ProductID + ', ' + i + ')" /> <div class="ScrollImgTitle">' + IT.ProductName + '</div> </div>';
                        }
                        if ((i + 1) % 3 == 0) {
                            html += "</div><div class='" + classes + "'></div><div>";
                            classes = "";
                        }
                    });
                    var oldhtml = $(".scrollImgContainer").html();
                    html += '</div>';
                    $(".scrollImgContainer").html(oldhtml + html);
                } else {
                    var html = "<div>";
                    var classes = "";
                    $.each(responce, function (i, IT) {
                        var src = DesignServiceUrl + "/designer/products/" + IT.ProductID + "/TemplateThumbnail1.jpg";
                        classes += "imgDetailContainer" + IT.ProductID + " ";

                        if (IT.PageOrientation == 1 && (IT.PDFTemplateHeight > IT.PDFTemplateWidth)) {// portrait height > width
                            html += '<div class="ScrollImgContainer1"><img src="' + src + '" alt="' + IT.ProductName + '" class="ScrollerImg LCLB" id="ScrollerImg' + IT.ProductID + '" onclick="ShowTemplateBox(&#39;' + IT.ProductName + '&#39; , ' + IT.ProductID + ', ' + i + ')" /> <div class="ScrollImgTitle">' + IT.ProductName + '</div> </div>';
                        }
                        else if (IT.PageOrientation == 1 && (IT.PDFTemplateHeight <= IT.PDFTemplateWidth)) {// portrait height < width
                            html += '<div class="ScrollImgContainer2"><img src="' + src + '" alt="' + IT.ProductName + '" class="ScrollerImg LCLB" id="ScrollerImg' + IT.ProductID + '" onclick="ShowTemplateBox(&#39;' + IT.ProductName + '&#39; , ' + IT.ProductID + ', ' + i + ')" /> <div class="ScrollImgTitlePort">' + IT.ProductName + '</div> </div>';

                        }
                        else if (IT.PageOrientation == 2 && (IT.PDFTemplateHeight > IT.PDFTemplateWidth)) {// landscap height > width
                            html += '<div class="ScrollImgContainer2"><img src="' + src + '" alt="' + IT.ProductName + '" class="ScrollerImg LCLB" id="ScrollerImg' + IT.ProductID + '" onclick="ShowTemplateBox(&#39;' + IT.ProductName + '&#39; , ' + IT.ProductID + ', ' + i + ')" /> <div class="ScrollImgTitlePort">' + IT.ProductName + '</div> </div>';
                        }
                        else if (IT.PageOrientation == 2 && (IT.PDFTemplateHeight <= IT.PDFTemplateWidth)) {// landscap height > width
                            html += '<div class="ScrollImgContainer1"><img src="' + src + '" alt="' + IT.ProductName + '" class="ScrollerImg LCLB" id="ScrollerImg' + IT.ProductID + '" onclick="ShowTemplateBox(&#39;' + IT.ProductName + '&#39; , ' + IT.ProductID + ', ' + i + ')" /> <div class="ScrollImgTitle">' + IT.ProductName + '</div> </div>';
                        }


                        if ((i + 1) % responce.length == 0) {
                            html += "</div><div class='" + classes + "'></div><div>";
                            classes = "";
                        }
                    });
                    var oldhtml = $(".scrollImgContainer").html();
                    html += '</div>';
                    $(".scrollImgContainer").html(oldhtml + html);
                }
                var CheckLogin = '<%=IsLogin %>';
                if (parseInt(CheckLogin) > 0) {
                    if (QtDAta != null) {
                        $("#txtQName").val(QtDAta.Name);
                        $("#txtQTitle").val(QtDAta.Title);
                        $("#txtQCompanyName").val(QtDAta.Company);
                        $("#txtQCompanyMessage").val(QtDAta.CompanyMessage);
                        $("#txtQAddressLine1").val(QtDAta.Address1);
                        $("#txtQPhone").val(QtDAta.Telephone);
                        $("#txtQFax").val(QtDAta.Fax);
                        $("#txtQEmail").val(QtDAta.Email);
                        $("#txtQWebsite").val(QtDAta.Website);
                    } else {
                        setTimeout(function () {
                            $("#txtQName").val(QtDAta.Name);
                            $("#txtQTitle").val(QtDAta.Title);
                            $("#txtQCompanyName").val(QtDAta.Company);
                            $("#txtQCompanyMessage").val(QtDAta.CompanyMessage);
                            $("#txtQAddressLine1").val(QtDAta.Address1);
                            $("#txtQPhone").val(QtDAta.Telephone);
                            $("#txtQFax").val(QtDAta.Fax);
                            $("#txtQEmail").val(QtDAta.Email);
                            $("#txtQWebsite").val(QtDAta.Website);
                            // Web2Print.UI.Services.WebStoreUtility.GetTemplatessForTemplateSel(pageCount, cmbSelectedIndex, globalCategoryName, CustomerID, searchText, OnSuccess);
                        }, 2000)
                    }
                }
                hideProgress();
            } else {
                $('#loaderimageDiv').css("display", "none");
                $("#lodingBar").html("All templates loaded, No more template to load ...");
                TemplateExist = false;
                setTimeout(function () { hideProgress() }, 2000);
            }
            $('.ScrollerImg').hover(
            function () {
                $(this).next().css("visibility", "visible");
            },
            function () {
                $(this).next().css("visibility", "hidden");
            }
        );
            $('.passive_star').toggle(function () {
                if (LoginPopUp()) {

                    $(this).removeClass('passive_star').addClass('active_star');
                    $(this).attr('title', 'Click to remove favourite');
                    $(".spanAddFav_TS").text("Remove this design as a favourite");
                    // $("#divFavoriteInd2").css("margin-left", "205px");
                    $(".product_selcection_thumnail_button_container_right_TS").css("width", "260px");
                    //  var templateId = $('#<%=hfProductTemplateID.ClientID %>').val();
                    SavedFarorite($('#<%=hfProductTemplateID.ClientID %>').val(), true);
                    var FavcountValu = $('.FavCount').text();
                    var ReplceFav = FavcountValu.replace("(", " ");
                    var FinalFavCount = ReplceFav.replace(")", " ");
                    var Count = "(" + (parseInt(FinalFavCount, 10) + 1) + ")";
                    $('.FavCount').html(Count);
                }
            }, function () {
                if (LoginPopUp()) {
                    $(this).removeClass('active_star').addClass('passive_star');
                    $(this).attr('title', 'Click to add favourite');
                    $(".spanAddFav_TS").text("Mark this design as a favourite");
                    //  $("#divFavoriteInd2").css("margin-left", "205px");
                    $(".product_selcection_thumnail_button_container_right_TS").css("width", "260px");
                    // var templateId = $('#<%=hfProductTemplateID.ClientID %>').val();
                    SavedFarorite($('#<%=hfProductTemplateID.ClientID %>').val(), false);
                    var FavcountValu = $('.FavCount').text();
                    var ReplceFav = FavcountValu.replace("(", " ");
                    var FinalFavCount = ReplceFav.replace(")", " ");
                    var Count = "(" + (parseInt(FinalFavCount, 10) - 1) + ")";
                    $('.FavCount').html(Count);
                }
            });

            $('.active_star').toggle(function () {
                if (LoginPopUp()) {
                    $(this).removeClass('active_star').addClass('passive_star');
                    $(this).attr('title', 'Click to add favourite');
                    $(".spanAddFav_TS").text("Mark this design as a favourite");
                    //  $("#divFavoriteInd2").css("margin-left", "205px");
                    $(".product_selcection_thumnail_button_container_right_TS").css("width", "240px");
                    //var templateId = $('#<%=hfProductTemplateID.ClientID %>').val();
                    SavedFarorite($('#<%=hfProductTemplateID.ClientID %>').val(), false);
                    var FavcountValu = $('.FavCount').text();
                    var ReplceFav = FavcountValu.replace("(", " ");
                    var FinalFavCount = ReplceFav.replace(")", " ");
                    var Count = "(" + (parseInt(FinalFavCount, 10) - 1) + ")";
                    $('.FavCount').html(Count);
                }
            }, function () {
                if (LoginPopUp()) {
                    $(this).removeClass('passive_star').addClass('active_star');
                    $(this).attr('title', 'Click to remove favourite');
                    $(".spanAddFav_TS").text("Remove this design as a favourite");
                    //  $("#divFavoriteInd2").css("margin-left", "205px");
                    $(".product_selcection_thumnail_button_container_right_TS").css("width", "260px");
                    //  var templateId = $('#<%=hfProductTemplateID.ClientID %>').val();
                    SavedFarorite($('#<%=hfProductTemplateID.ClientID %>').val(), true);
                    var FavcountValu = $('.FavCount').text();
                    var ReplceFav = FavcountValu.replace("(", " ");
                    var FinalFavCount = ReplceFav.replace(")", " ");
                    var Count = "(" + (parseInt(FinalFavCount, 10) + 1) + ")";
                    $('.FavCount').html(Count);
                }
            });
        }

        function showProgressbar() {
            showProgress();
        }

        function showProgress() {

            $('#loaderimageDiv').css("display", "block");
            $("#lodingBar").html("Loading please wait....");
            var shadow = document.getElementById("divShd");
            var bws = getBrowserHeight();
            shadow.style.width = bws.width + "px";
            shadow.style.height = bws.height + "px";
            var left = parseInt((bws.width - 350) / 2);
            var top = parseInt((bws.height - 200) / 2);
            $('#divShd').css("display", "block");
            $('#UpdateProgressUserProfile').css("display", "block");
            return true;
        }
        function hideProgress() {
            $('#divShd').css("display", "none");
            $('#UpdateProgressUserProfile').css("display", "none");
            return true;
        }
        function showTemplateLoading() {
            $('#loaderimageDiv').css("display", "block");
            $("#lodingBar").html("Loading the next set of templates ...");
            var bws = getBrowserHeight();
            var left = parseInt((bws.width - 350) / 2);
            var top = parseInt(($(document).height() - 200) / 2);
            $('#UpdateProgressUserProfile').css("display", "block");
            return true;
        }
        function btnDesignNowClick() {
            $('.pnlTempDetail1').css("display", "none");
            $('.pnlTempDetail2').css("display", "block");
            return false;
        }
        function getBrowserHeight() {
            var intH = 0;
            var intW = 0;
            if (typeof window.innerWidth == 'number') {
                intH = window.innerHeight;
                intW = window.innerWidth;
            }
            else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
                intH = document.documentElement.clientHeight;
                intW = document.documentElement.clientWidth;
            }
            else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
                intH = document.body.clientHeight;
                intW = document.body.clientWidth;
            }
            return { width: parseInt(intW), height: parseInt(intH) };
        }


        var contentHeight = 800;
        var pageHeight = document.documentElement.clientHeight;
        var scrollPosition;
        var intervalObj;
        var ItemID = 0;
        var orderId = 0;
        $(window).scroll(function () {
            if (navigator.appName == "Microsoft Internet Explorer")
                scrollPosition = document.documentElement.scrollTop;
            else
                scrollPosition = window.pageYOffset;

            if ($(window).scrollTop() == $(document).height() - $(window).height()) {
                if (TemplateExist) {
                    pageCount += 1;
                    showTemplateLoading();
                    var globalCategoryName = $('#<%=txtTemplateDesignerMappedCategoryName.ClientID %>').val();
                    var CustomerID = $('#<%=txtCustomerID.ClientID %>').val();
                    var searchText = $('#<%=txtSearchKeywords.ClientID %>').val();
                    if (searchText == "Search Phrase") {
                        searchText = "";
                    }
                    var cmbSelectedIndex = $('#<%=hfSelectedCat.ClientID %>').val();
                    var basecolorid = $("#<%=hfBaseColorID.ClientID %>").val();
                    Web2Print.UI.Services.WebStoreUtility.GetTemplatesOnScroll(pageCount, cmbSelectedIndex, globalCategoryName, CustomerID, searchText, basecolorid, OnSuccess);
                    contentHeight += 800;
                }
            }
        });


        function loadMoreTemplates() {
            
                if (TemplateExist) {
                    pageCount += 1;
                    showTemplateLoading();
                    var globalCategoryName = $('#<%=txtTemplateDesignerMappedCategoryName.ClientID %>').val();
                    var CustomerID = $('#<%=txtCustomerID.ClientID %>').val();
                    var searchText = $('#<%=txtSearchKeywords.ClientID %>').val();
                    if (searchText == "Search Phrase") {
                        searchText = "";
                    }
                    var cmbSelectedIndex = $('#<%=hfSelectedCat.ClientID %>').val();
                    var basecolorid = $("#<%=hfBaseColorID.ClientID %>").val();
                    Web2Print.UI.Services.WebStoreUtility.GetTemplatesOnScroll(pageCount, cmbSelectedIndex, globalCategoryName, CustomerID, searchText, basecolorid, OnSuccess);
                    contentHeight += 800;
                }
        }

        function ShowTemplateBox(templatename, ProductId, i) {
            $(".pnlTempDetail1").css("display", "block");
            $(".pnlTempDetail2").css("display", "none");
            templateName = templatename;

            n = i % 3;
            $("#imgTempPage1").attr('src', '');
            $("#imgTempPageN").attr('src', '');
            $("#imgTempPageNBack").attr('src', '');
            $("#TempPage2").attr('src', '');
            $("#TempPage3").attr('src', '');
            $("#TempPage4").attr('src', '');
            $("#TempPage5").attr('src', '');
            $("#TempPage6").attr('src', '');
            if ($(window).width() >= 961) {
                if (n == 0) {
                    $(".iconSelectedTemplate").css("margin-left", "-487px");//-567
                } else if (n == 1) {
                    $(".iconSelectedTemplate").css("margin-left", "0px");//0
                } else if (n == 2) {
                    $(".iconSelectedTemplate").css("margin-left", "487px");
                }
            } else {
                if (n == 0) {
                    $(".iconSelectedTemplate").css("margin-left", "120px");//-567
                } else if (n == 1) {
                    $(".iconSelectedTemplate").css("margin-left", "385px");//0
                } else if (n == 2) {
                    $(".iconSelectedTemplate").css("margin-left", "567px");
                }
            }
          
            $("#templateDetailContainer").css("display", "block");
            $("#templateDetailContainer").appendTo(".imgDetailContainer" + ProductId);
            $("#divFavoriteInd2").attr("title", "Click to Mark this design as a favourite");
            //  $("#divFavoriteInd2").css("margin-left", "205px");
            $(".product_selcection_thumnail_button_container_right_TS").css("width", "260px");
            $(".spanAddFav_TS").text("Mark this design as a favourite");
            $('#divFavoriteInd2').removeClass('active_star').addClass('passive_star');
            if (selectedMatchingSets != null) {
                $.each(selectedMatchingSets, function (i, IT) {
                    if (IT.TemplateID == ProductId && IT.IsFavorite) {
                        $("#divFavoriteInd2").attr("title", "Click to Remove this design as a favourite");
                        $(".spanAddFav_TS").text("Remove this design as a favourite");
                        // $("#divFavoriteInd2").css("margin-left", "205px");
                        $(".product_selcection_thumnail_button_container_right_TS").css("width", "260px");
                        $('#divFavoriteInd2').removeClass('passive_star').addClass('active_star');
                    }
                });
            }
            ShowTemplate(templatename, ProductId);
            $('html, body').animate({
                scrollTop: $('.imgDetailContainer' + ProductId).offset().top
            },500);
            
            //goToByScroll("#ScrollerImg" + ProductId);
        }
        function goToByScroll(id) {
            $('html,body').animate({
                scrollTop: $(id).offset().top
            }, 1);
        }

        function ShowTemplate(templatename, ProductId) {
            showProgress();
            var DesignSrvceUrl = '<%=TemplateDesignserviceUrl %>';
            var ServiceUrlForTemplates = DesignSrvceUrl + "/designer/products/";
            var Html = null;
            var length = null;
            $('#<%=hfProductTemplateID.ClientID %>').val(ProductId);
            $('#<%=hfTempProductName.ClientID %>').val(templatename);
            $('#<%=lblHeader.ClientID %>').html(templatename);
            $.get("/Services/Webstore.svc/resumeproduct?itemid=" + ItemID + "&orderid=" + orderId + "&templateid=" + ProductId,
                function (data) {
                    $('#<%=hfEditTempType.ClientID %>').val(data);
                });
                Web2Print.UI.Services.WebStoreUtility.GetTemplatePagesbyID(ProductId, OnSuccess);
                function OnSuccess(responce) {
                    if (responce != "") {
                        length = responce.length;

                        if (length > 2) {
                            $('#<%=lrtlMultiFc.ClientID %>').css("display", "block");
                            $('.SecondTemplatediv').css("display", "block");
                            $('#<%=lblTemplatePageName1.ClientID %>').html(responce[0].PageName);
                            $('#<%=lblTemplatePageName2.ClientID %>').html(responce[1].PageName);

                            $("#imgTempPage1").attr('src', ServiceUrlForTemplates + ProductId + '/p' + 1 + '.png');
                            $("#imgTempPageN").attr('src', ServiceUrlForTemplates + ProductId + '/p' + 2 + '.png');
                            $("#imgTempPageNBack").attr('src', ServiceUrlForTemplates + ProductId + '/p' + 1 + '.png');
                            document.getElementById("SubContent").innerHTML = "";
                            if (length >= 6) {
                                for (i = 2; i <= length - 1; i++) {

                                    var src = ServiceUrlForTemplates + ProductId + "/p" + i + ".png";
                                    document.getElementById("SubContent").innerHTML += "<div><div class='TemplesDiv '><div class=pad5><div class=PDTCWB_Temp><div class='PDTC_TemplesDiv FI_TemplesDiv ImageClick' onclick=ChangeImage('" + src + "') ><img id=TempPage" + i + " src =" + src + " class=full_img_ThumbnailPath_Temp_Div /></div></div></div><div class=TemplateNameNCS ><span id=lblPageName" + i + ">" + responce[i].PageName + "</span></div></div></div>";

                                }
                            }
                            else {
                                count = 1;
                                for (i = 2; i <= length; i++) {

                                    var src = ServiceUrlForTemplates + ProductId + "/p" + i + ".png";
                                    document.getElementById("SubContent").innerHTML += "<div><div class='TemplesDiv'><div class=pad5><div class=PDTCWB_Temp><div class='PDTC_TemplesDiv FI_TemplesDiv ImageClick' onclick=ChangeImage('" + src + "') ><img id=TempPage" + i + " src =" + src + " class=full_img_ThumbnailPath_Temp_Div /></div></div></div><div class=TemplateNameNCS ><span id=lblPageName" + i + ">" + responce[count].PageName + "</span></div></div></div>";
                                    count = count + 1;

                                }
                            }
                        }
                        if (length == "1") {
                            $('#<%=lrtlMultiFc.ClientID %>').css("display", "none");
                            $('.SecondTemplatediv').css("display", "none");
                            $('.TemplateName2CSTS').css("display", "none");
                            $('#<%=lblTemplatePageName1.ClientID %>').html(responce[0].PageName);
                            $("#imgTempPage1").attr('src', ServiceUrlForTemplates + ProductId + '/p1' + '.png');
                            $("#imgTempPageNBack").attr('src', ServiceUrlForTemplates + ProductId + '/p1' + '.png');
                            document.getElementById("SubContent").innerHTML = "";
                        }
                        if (length == "2") {
                            $('#<%=lrtlMultiFc.ClientID %>').css("display", "none");
                            $('.SecondTemplatediv').css("display", "block");
                            $('#<%=lblTemplatePageName1.ClientID %>').html(responce[0].PageName);
                            $('#<%=lblTemplatePageName2.ClientID %>').html(responce[1].PageName);
                            $("#imgTempPage1").attr('src', ServiceUrlForTemplates + ProductId + '/p1' + '.png');
                            $("#imgTempPageN").attr('src', ServiceUrlForTemplates + ProductId + '/p2' + '.png');
                            $("#imgTempPageNBack").attr('src', ServiceUrlForTemplates + ProductId + '/p1' + '.png');
                            var src = ServiceUrlForTemplates + ProductId + "/p" + "2" + ".png";
                            document.getElementById("SubContent").innerHTML = "<div><div class='LCLB TemplesDiv MTop15_Temp'><div class=pad5><div class=PDTCWB_Temp><div class='PDTC_TemplesDiv FI_TemplesDiv ImageClick' onclick=ChangeImage('" + src + "') ><img id=TempPage" + "2" + " src =" + src + " class=full_img_ThumbnailPath_Temp_Div /></div></div></div><div class=TemplateNameNCS ><span id=lblPageName2>" + responce[1].PageName + "</span></div></div></div>";
                        }
                        hideProgress();
                    }
                    var catId = $('#<%=hfCategoryId.ClientID %>').val();
                    var ItemId = $('#<%=hfItemId.ClientID %>').val();
                    $("#<%=cntgpShareLink.ClientID%>").attr("onclick", "ShareOnGooglePl('/pd/"+templatename+ ";" + catId + ";" + ItemId + ";UploadDesign;0;" + ProductId + ";0');");
                    $("#<%=cntLinkinShareBtn.ClientID%>").attr("onclick", "ShareOnLinkIn('/pd/" + templatename + ";" + catId + ";" + ItemId + ";UploadDesign;0;" + ProductId + ";0');");
                    $("#<%=cnttwitterSharelnk.ClientID%>").attr("onclick", "OpenNewWindowToSahre('/pd/" + templatename + ";" + catId + ";" + ItemId + ";UploadDesign;0;" + ProductId + ";0');");
                    $("#<%=cntfacebookSharelnk.ClientID%>").attr("onclick", "OpenNewWindowToSahreWithFaceBook('/pd/" + templatename + ";" + catId + ";" + ItemId + ";UploadDesign;0;" + ProductId + ";0');");
                      
                }
            //            $.getJSON(DesignSrvceUrl + "services/TemplatePagesSvc/" + ProductId,
            //	        function (DT) {
            //	            length = DT.length;
            //	            if (length > 2) {
            //	                $('#<%=lrtlMultiFc.ClientID %>').css("display", "block");
            //	                $('.SecondTemplatediv').css("display", "block");
            //	                $('#<%=lblTemplatePageName1.ClientID %>').html(DT[0].PageName);
            //	                $('#<%=lblTemplatePageName2.ClientID %>').html(DT[1].PageName);

            //	                $("#imgTempPage1").attr('src', ServiceUrlForTemplates + ProductId + '/p' + 1 + '.png');
            //	                $("#imgTempPageN").attr('src', ServiceUrlForTemplates + ProductId + '/p' + 2 + '.png');
            //	                $("#imgTempPageNBack").attr('src', ServiceUrlForTemplates + ProductId + '/p' + 1 + '.png');
            //	                document.getElementById("SubContent").innerHTML = "";
            //	                if (length >= 6) {
            //	                    for (i = 2; i <= length - 1; i++) {

            //	                        var src = ServiceUrlForTemplates + ProductId + "/p" + i + ".png";
            //	                        document.getElementById("SubContent").innerHTML += "<div><div class='TemplesDiv '><div class=pad5><div class=PDTCWB_Temp><div class='PDTC_TemplesDiv FI_TemplesDiv ImageClick' onclick=ChangeImage('" + src + "') ><img id=TempPage" + i + " src =" + src + " class=full_img_ThumbnailPath_Temp_Div /></div></div></div><div class=TemplateNameNCS ><span id=lblPageName" + i + ">" + DT[i].PageName + "</span></div></div></div>";

            //	                    }
            //	                }
            //	                else {
            //	                    count = 1;
            //	                    for (i = 2; i <= length; i++) {

            //	                        var src = ServiceUrlForTemplates + ProductId + "/p" + i + ".png";
            //	                        document.getElementById("SubContent").innerHTML += "<div><div class='TemplesDiv'><div class=pad5><div class=PDTCWB_Temp><div class='PDTC_TemplesDiv FI_TemplesDiv ImageClick' onclick=ChangeImage('" + src + "') ><img id=TempPage" + i + " src =" + src + " class=full_img_ThumbnailPath_Temp_Div /></div></div></div><div class=TemplateNameNCS ><span id=lblPageName" + i + ">" + DT[count].PageName + "</span></div></div></div>";
            //	                        count = count + 1;

            //	                    }
            //	                }
            //	            }
            //	            if (length == "1") {
            //	                $('#<%=lrtlMultiFc.ClientID %>').css("display", "none");
            //	                $('.SecondTemplatediv').css("display", "none");
            //	                $('.TemplateName2CSTS').css("display", "none");
            //	                $('#<%=lblTemplatePageName1.ClientID %>').html(DT[0].PageName);
            //	                $("#imgTempPage1").attr('src', ServiceUrlForTemplates + ProductId + '/p1' + '.png');
            //	                $("#imgTempPageNBack").attr('src', ServiceUrlForTemplates + ProductId + '/p1' + '.png');
            //	                document.getElementById("SubContent").innerHTML = "";
            //	            }
            //	            if (length == "2") {
            //	                $('#<%=lrtlMultiFc.ClientID %>').css("display", "none");
            //	                $('.SecondTemplatediv').css("display", "block");
            //	                $('#<%=lblTemplatePageName1.ClientID %>').html(DT[0].PageName);
            //	                $('#<%=lblTemplatePageName2.ClientID %>').html(DT[1].PageName);
            //	                $("#imgTempPage1").attr('src', ServiceUrlForTemplates + ProductId + '/p1' + '.png');
            //	                $("#imgTempPageN").attr('src', ServiceUrlForTemplates + ProductId + '/p2' + '.png');
            //	                $("#imgTempPageNBack").attr('src', ServiceUrlForTemplates + ProductId + '/p1' + '.png');
            //	                var src = DesignSrvceUrl + ProductId + "/p" + "2" + ".png";
            //	                document.getElementById("SubContent").innerHTML = document.getElementById("SubContent").innerHTML = "<div><div class='LCLB TemplesDiv MTop15_Temp'><div class=pad5><div class=PDTCWB_Temp><div class='PDTC_TemplesDiv FI_TemplesDiv ImageClick' onclick=ChangeImage('" + src + "') ><img id=TempPage" + "2" + " src =" + src + " class=full_img_ThumbnailPath_Temp_Div /></div></div></div><div class=TemplateNameNCS ><span id=lblPageName2>" + DT[1].PageName + "</span></div></div></div>";
            //	            }
            //	            hideProgress();
            //	        });

            return false;

        }
        function ChangeImage(src) {
            var srcOfIMage = src;
            $("#imgTempPageN").attr('src', srcOfIMage);
            //document.getElementById("imgTempPageNBack").src = srcOfIMage;

        }
        function SaveQuickTxt() {
            var CheckLogin = '<%=IsLogin %>';
            if (parseInt(CheckLogin) > 0) {
                var CustomerID = $('#<%=txtQCustomerID.ClientID %>').val();
                var ContactID = $('#<%=txtContactID.ClientID %>').val();
                var QuickTxtName = $("#txtQName").val();
                var QuickTxtTitle = $("#txtQTitle").val();
                var QuickTxtCompanyName = $("#txtQCompanyName").val();
                var QuickTxtCompanyMsg = $("#txtQCompanyMessage").val();
                var QuickTxtAddress1 = $("#txtQAddressLine1").val();
                var QuickTxtTel = $("#txtQPhone").val();
                var QuickTxtFax = $("#txtQFax").val();
                var QuickTxtEmail = $("#txtQEmail").val();
                var QuickTxtWebsite = $("#txtQWebsite").val();

                if (QuickTxtCompanyName != undefined || QuickTxtCompanyName != null) {
                    QTD.Company = (QuickTxtCompanyName == "" ? "Your Company Name" : QuickTxtCompanyName);
                } else {
                    QTD.Company = "";
                }
                if (QuickTxtCompanyMsg != undefined || QuickTxtCompanyMsg != null) {
                    QTD.CompanyMessage = QuickTxtCompanyMsg == "" ? "Your Company Message" : QuickTxtCompanyMsg;
                } else {
                    QTD.CompanyMessage = "";
                }
                if (QuickTxtName != undefined || QuickTxtName != null) {
                    QTD.Name = QuickTxtName == "" ? "Your Name" : QuickTxtName;
                } else {
                    QTD.Name = "";
                }
                if (QuickTxtTitle != undefined || QuickTxtTitle != null) {
                    QTD.Title = QuickTxtTitle == "" ? "Your Title" : QuickTxtTitle;
                } else {
                    QTD.Title = "";
                }
                if (QuickTxtAddress1 != undefined || QuickTxtAddress1 != null) {
                    QTD.Address1 = QuickTxtAddress1 == "" ? "Address Line 1" : QuickTxtAddress1;
                } else {
                    QTD.Address1 = "";
                }
                if (QuickTxtTel != undefined || QuickTxtTel != null) {
                    QTD.Telephone = QuickTxtTel == "" ? "Telephone / Other" : QuickTxtTel;
                } else {
                    QTD.Telephone = "";
                }
                if (QuickTxtFax != undefined || QuickTxtFax != null) {
                    QTD.Fax = QuickTxtFax == "" ? "Fax / Other" : QuickTxtFax;
                } else {
                    QTD.Fax = "";
                }
                if (QuickTxtEmail != undefined || QuickTxtEmail != null) {
                    QTD.Email = QuickTxtEmail == "" ? "Email address / Other" : QuickTxtEmail;
                } else {
                    QTD.Email = "";
                }
                if (QuickTxtWebsite != undefined || QuickTxtWebsite != null) {
                    QTD.Website = QuickTxtWebsite == "" ? "Website address" : QuickTxtWebsite;
                } else {
                    QTD.Website = "";
                }
                var jsonObjects = JSON.stringify(QTD, null, 2);
                var to;
                to = "../services/Webstore.svc/update/";
                var options = {
                    type: "POST",
                    url: to,
                    data: jsonObjects,
                    contentType: "text/plain;",
                    dataType: "json",
                    async: false,
                    success: function (response) {
                    },
                    error: function (msg) { alert("Error : " + msg); }
                };
                var returnText = $.ajax(options).responseText;
            }

        }
        function ShowMessage() {
            var type = $('#<%=hfEditTempType.ClientID %>').val();
            if (type == "NoTemplate") {
                showProgress();
                return true;
            } else if (type == "SameTemplate") {
                ShowPopup2("Alert!", "You already have the same template in designer mode Would you like to resume your design or start from scratch ?");
                return false;
            } else if (type == "SameItem") {
                ShowPopup2("Alert!", "Resume editing of your last design for this product or create a new instance based on the template you just selected ?");
                return false;
            }
            return false;

        }
        function HideBar() {
            $("#templateDetailContainer").css("display", "none");
        }
        function LoginPopUp() {
            var CheckLogin = '<%=IsLogin %>';
            if (parseInt(CheckLogin) > 0) {
                return true;
            }
            else {
                var returnUrl = '<%=ReturnUrl %>';
                var message = '<div style="text-align:left;padding-left:90px;">Add to favourites to mark this template as a favourites in your profile, please register or login first</div><br><a href="../Signup.aspx?' + returnUrl + '" style="padding-right:20px;">Register</a><a href="../Login.aspx?' + returnUrl + '">Login</a>';

                ShowPopup('Message', message);
                return false;
            }
        }
        function isUserLogin() {
            var CheckLogin = '<%=IsLogin %>';
            if (parseInt(CheckLogin) > 0) {
                return true;
            }
            else {
                var returnUrl = '<%=ReturnUrl %>';
                var message = '<div style="text-align:left;padding-left:90px;">To submit a marketing brief please login or register.  </div><br><a href="../Signup.aspx?' + returnUrl + '" class="btn_register_files_TS" style="margin-right:10px;">Register</a><a href="../Login.aspx?' + returnUrl + '"  class="btn_register_files_TS" >Login</a>';

                ShowPopup('Message', message);
                return false;
            }
        }
        function SavedFarorite(templateId, isFavorite) {
            var contactId = $('#<%=hfContactID.ClientID %>').val();
            var itemId = $('#<%=hfItemId.ClientID %>').val();
            var categoryId = $('#<%=hfCategoryId.ClientID %>').val();
            if (parseInt(contactId) > 0) {
                Web2Print.UI.Services.WebStoreUtility.AddUpdateFavDesign(templateId, itemId, contactId, categoryId, isFavorite, OnSuccessSavedFav);
            }
        }
        function OnSuccessSavedFav(data) {
            $.getJSON("../services/Webstore.svc/FavDesignList?contactID=" + $('#<%=hfContactID.ClientID %>').val(),
		        function (xdata) {
		            selectedMatchingSets = xdata;
		        });
        }
        function openMS() {
            var templatename = encodeURIComponent(templateName);
            var ProductCategoryId = $('#<%=hfCategoryId.ClientID %>').val();;
            var ItemId = $('#<%=hfItemId.ClientID %>').val();;
            var shadow = document.getElementById("divShd");
            var bws = getBrowserHeight();
            shadow.style.width = bws.width + "px";
            shadow.style.height = bws.height + "px";
            var left = parseInt(($(window).width() - 780) / 2);
            var top = parseInt(($(window).height() - 510) / 2);
            $('#divShd').css("display", "block");
            //shadow = null;
            $('#jqwin').css("position", "fixed");
            $('#jqwin').css("left", left);
            $('#jqwin').css("top", top);
            $('#jqwin').css("background-color", "transparent");
            var html = '<div class="closeBtn" onclick="closeMS()"> </div>';
            $('#jqwin').html(html + '<iframe id="ifrm" style="width:780px; height:510px; padding:5px; padding-bottom:0px; border: none;" class="rounded_corners LCLB"></iframe>').dialog();
            $("#jqwin>#ifrm").attr("src", '../matchingsetpopup.aspx?templatename=' + templatename + "&ProductCategoryId=" + ProductCategoryId + "&ItemID=" + ItemId);
            $('#jqwin').show();
            $(".closeBtn").css("display", "block");
            return false;
        }

        function closeMS() {
            $(".ui-dialog-titlebar-close").click();
            $(".closeBtn").css("display", "none");
            $('#divShd').css("display", "none");
            $('#jqwin').hide();
        }

        function BaseColorOnClick(ID) {
            $("#templateDetailContainer").appendTo("#demotemplateContainer");
            $('input:radio[id=' + ID + ']').attr('checked', true);
            $('input:radio[id=' + ID + ']').parent().parent().addClass('ShadowToBaseColor'); //.css("background-color", "#f3f3f3");
            var BaseClrId = "|";
            BaseClrId += $('input:radio[id=' + ID + ']').attr("data-BaseColorID");

            $("#<%=hfBaseColorID.ClientID %>").val(BaseClrId);
            $('input[type=radio]').each(function () {
                if ($(this).is(':checked')) {
                } else {
                    $(this).parent().parent().removeClass('ShadowToBaseColor'); //.css("background-color", "#ffffff");
                }
            });
            var globalCategoryName = $('#<%=txtTemplateDesignerMappedCategoryName.ClientID %>').val();
            var CustomerID = $('#<%=txtCustomerID.ClientID %>').val();
            var searchText = $('#<%=txtSearchKeywords.ClientID %>').val();
            if (searchText == "Search Phrase") {
                searchText = "";
            }
            var cmbSelectedIndex = $('#<%=hfSelectedCat.ClientID %>').val();
            var basecolorid = $("#<%=hfBaseColorID.ClientID %>").val();

            $("#scrollImgContainer").html("");
            showProgress();
            Web2Print.UI.Services.WebStoreUtility.GetTemplatesbyMultipleSelection("0", cmbSelectedIndex, globalCategoryName, CustomerID, searchText, BaseClrId, OnSuccess);
        }

        function ClearBaseColorSelection() {

            var basecolorid = $("#<%=hfBaseColorID.ClientID %>").val();
            basecolorid = basecolorid.replace("|", "");
            $('input[type=radio]').each(function () {
                var BaseC = $(this).attr("data-BaseColorID");
                if (BaseC == basecolorid) {
                    $(this).parent().parent().removeClass('ShadowToBaseColor');
                    return;
                }
            });
            $("#<%=hfBaseColorID.ClientID %>").val(0);
            var globalCategoryName = $('#<%=txtTemplateDesignerMappedCategoryName.ClientID %>').val();
            var CustomerID = $('#<%=txtCustomerID.ClientID %>').val();
            var searchText = $('#<%=txtSearchKeywords.ClientID %>').val();
            if (searchText == "Search Phrase") {
                searchText = "";
            }
            var cmbSelectedIndex = $('#<%=hfSelectedCat.ClientID %>').val();
            var basecolorid = $("#<%=hfBaseColorID.ClientID %>").val();
            $("#scrollImgContainer").html("");
            showProgress();
            Web2Print.UI.Services.WebStoreUtility.GetTemplatesbyMultipleSelection("0", cmbSelectedIndex, globalCategoryName, CustomerID, searchText, "0", OnSuccess);
        }

        function SetSelectedIndustryTypes() {
            var ids = "";
            $('input[type=checkbox]').each(function () {
                if ($(this).is(':checked')) {
                    if (ids == "") {
                        ids = "?" + $(this).attr("data-TagID");
                    } else {
                        ids += "|" + $(this).attr("data-TagID");
                    }
                }
            });
            $("#<%=hfSelectedCat.ClientID %>").val(ids);
        }
        function CheckBoxOnClick(id) {
            if ($('input:checkbox[id=' + id + ']').is(':checked')) {

                $('input:checkbox[id=' + id + ']').attr('checked', true);
                $('input:checkbox[id=' + id + ']').parent().addClass('SquareTickBox');
                $('input:checkbox[id=' + id + ']').parent().removeClass('SquareBox1');

            } else {
                $('input:checkbox[id=' + id + ']').attr('checked', false);
                $('input:checkbox[id=' + id + ']').parent().addClass('SquareBox1');
                $('input:checkbox[id=' + id + ']').parent().removeClass('SquareTickBox');
            }
            var ids = "";
            $('input[type=checkbox]').each(function () {
                if ($(this).is(':checked')) {
                    if (ids == "") {
                        ids = "?" + $(this).attr("data-TagID");
                    } else {
                        ids += "|" + $(this).attr("data-TagID");
                    }
                }
            });
            $("#<%=hfSelectedCat.ClientID %>").val(ids);
            var globalCategoryName = $('#<%=txtTemplateDesignerMappedCategoryName.ClientID %>').val();
            var CustomerID = $('#<%=txtCustomerID.ClientID %>').val();
            var searchText = $('#<%=txtSearchKeywords.ClientID %>').val();
            if (searchText == "Search Phrase") {
                searchText = "";
            }
            var cmbSelectedIndex = $('#<%=hfSelectedCat.ClientID %>').val();
            var basecolorid = $("#<%=hfBaseColorID.ClientID %>").val();
            $("#scrollImgContainer").html("");
            showProgress();
            Web2Print.UI.Services.WebStoreUtility.GetTemplatesbyMultipleSelection("0", cmbSelectedIndex, globalCategoryName, CustomerID, searchText, basecolorid, OnSuccess);
        }

        function SearchTemplates() {
            showProgress();
            var globalCategoryName = $('#<%=txtTemplateDesignerMappedCategoryName.ClientID %>').val();
            var CustomerID = $('#<%=txtCustomerID.ClientID %>').val();
            var searchText = $('#<%=txtSearchKeywords.ClientID %>').val();
            if (searchText == "Search Phrase") {
                searchText = "";
            }
            var cmbSelectedIndex = $('#<%=hfSelectedCat.ClientID %>').val();
            var basecolorid = $("#<%=hfBaseColorID.ClientID %>").val();

            Web2Print.UI.Services.WebStoreUtility.GetTemplatesbyMultipleSelection(pageCount, cmbSelectedIndex, globalCategoryName, CustomerID, searchText, basecolorid, OnSuccess);
        }
        function RemoveWaterMark() {
            var searchText = $('#<%=txtSearchKeywords.ClientID %>').val();
            if (searchText == "Search Phrase") {
                $('#<%=txtSearchKeywords.ClientID %>').val("");
            }
        }
        function SetWaterMark() {
            var searchText = $('#<%=txtSearchKeywords.ClientID %>').val();
            if (searchText == "") {
                $('#<%=txtSearchKeywords.ClientID %>').val("Search Phrase");
            }
        }
        function SelectAllIndustryTag() {
            var ids = "";
            $('input:checkbox[name=StylesCheckbox]').each(function () {

                $(this).attr('checked', true);
                $(this).parent().addClass('SquareTickBox');
                $(this).parent().removeClass('SquareBox1');

                if (ids == "") {
                    ids = "?" + $(this).attr("data-TagID");
                } else {
                    ids += "|" + $(this).attr("data-TagID");
                }
            });
            showProgress();
            $('#<%=hfSelectedCat.ClientID %>').val(ids);
            var globalCategoryName = $('#<%=txtTemplateDesignerMappedCategoryName.ClientID %>').val();
            var CustomerID = $('#<%=txtCustomerID.ClientID %>').val();
            var searchText = $('#<%=txtSearchKeywords.ClientID %>').val();
            if (searchText == "Search Phrase") {
                searchText = "";
            }
            var cmbSelectedIndex = $('#<%=hfSelectedCat.ClientID %>').val();
            var basecolorid = $("#<%=hfBaseColorID.ClientID %>").val();

            Web2Print.UI.Services.WebStoreUtility.GetTemplatesbyMultipleSelection(pageCount, cmbSelectedIndex, globalCategoryName, CustomerID, searchText, basecolorid, OnSuccess);
        }
        
        function OpenNewWindowToSahre(ShareUrl) {
            var pageUrl = window.location.href;
           
            var DomainName = pageUrl.match(/^http:\/\/[^/]+/);
            ShareUrl = DomainName[0] + ShareUrl;
           
            var WindHref = ShareUrl;
            while (WindHref.indexOf(";") > 0) {
                WindHref = WindHref.replace(";", "%3B");

            }
            WindHref = WindHref.replace(":", "%3A");
            while (WindHref.indexOf("/") > 0) {
                WindHref = WindHref.replace("/", "%2F");

            }
            while (WindHref.indexOf("=") > 0) {
                WindHref = WindHref.replace("=", "%3D");

            }
            while (WindHref.indexOf("&") > 0) {
                WindHref = WindHref.replace("&", "%26");

            }
            WindHref = WindHref.replace("?", "%3F");

            var w = 620;
            var h = 360;
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            window.open("https://twitter.com/intent/tweet?original_referer=" + WindHref + "&related=jasoncosta&text=-%20UK&tw_p=tweetbutton&url=" + WindHref, "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, width=" + w + ", height=" + h + ", top=" + top + ", left=" + left);
            return false;
        }
        
        function OpenNewWindowToSahreWithFaceBook(ShareUrl) {
           
            var pageUrl = window.location.href;

            var DomainName = pageUrl.match(/^http:\/\/[^/]+/);
            ShareUrl = DomainName[0] + ShareUrl;
            var WindHref = ShareUrl;
            while (WindHref.indexOf(";") > 0) {
                WindHref = WindHref.replace(";", "%3B");

            }
            WindHref = WindHref.replace(":", "%3A");
            while (WindHref.indexOf("/") > 0) {
                WindHref = WindHref.replace("/", "%2F");

            }
            while (WindHref.indexOf("=") > 0) {
                WindHref = WindHref.replace("=", "%3D");

            }
            while (WindHref.indexOf("&") > 0) {
                WindHref = WindHref.replace("&", "%26");

            }
            WindHref = WindHref.replace("?", "%3F");
            //var htm = "<div class='clearfix UIShareStage_Preview'><div class='lfloat _ohe'><div class=UIShareStage_Image><img class='UIShareStage_Image img' src=;w=154&amp;h=154&amp;url=https%3A%2F%2Ffbstatic-a.akamaihd.net%2Frsrc.php%2Fv2%2Fy6%2Fr%2FYQEGe6GxI_M.png&amp;cfs=1&amp;upscale alt=></div></div><div class='UIShareStage_ShareContent _42ef'><div class=UIShareStage_Title><span dir=ltr>Social Plugins</span></div><div class=UIShareStage_Subtitle>developers.facebook.com</div><div class=UIShareStage_Summary><p class=UIShareStage_BottomMargin>Social plugins let you see what your friends have liked, commented on or shared on sites across the web.</p></div></div></div>";
            var w = 620;
            var h = 360;
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            window.open("https://www.facebook.com/sharer.php?app_id=1421343758131537&sdk=joey&u=" + WindHref + "&display=popup", "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, width=" + w + ", height=" + h + ", top=" + top + ", left=" + left);
            return false;
        }

        function ShareOnLinkIn(ShareUrl) {
            var pageUrl = window.location.href;

            var DomainName = pageUrl.match(/^http:\/\/[^/]+/);
            ShareUrl = DomainName[0] + ShareUrl;
            var WindHref = ShareUrl;
            while (WindHref.indexOf(";") > 0) {
                WindHref = WindHref.replace(";", "%3B");

            }
            WindHref = WindHref.replace(":", "%3A");
            while (WindHref.indexOf("/") > 0) {
                WindHref = WindHref.replace("/", "%2F");

            }
            while (WindHref.indexOf("=") > 0) {
                WindHref = WindHref.replace("=", "%3D");

            }
            while (WindHref.indexOf("&") > 0) {
                WindHref = WindHref.replace("&", "%26");

            }
            WindHref = WindHref.replace("?", "%3F");
            //var htm = "<div class='clearfix UIShareStage_Preview'><div class='lfloat _ohe'><div class=UIShareStage_Image><img class='UIShareStage_Image img' src=;w=154&amp;h=154&amp;url=https%3A%2F%2Ffbstatic-a.akamaihd.net%2Frsrc.php%2Fv2%2Fy6%2Fr%2FYQEGe6GxI_M.png&amp;cfs=1&amp;upscale alt=></div></div><div class='UIShareStage_ShareContent _42ef'><div class=UIShareStage_Title><span dir=ltr>Social Plugins</span></div><div class=UIShareStage_Subtitle>developers.facebook.com</div><div class=UIShareStage_Summary><p class=UIShareStage_BottomMargin>Social plugins let you see what your friends have liked, commented on or shared on sites across the web.</p></div></div></div>";
            var w = 620;
            var h = 360;
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            window.open("http://www.linkedin.com/cws/share?url=" + WindHref + "&original_referer=" + WindHref + "&token=&isFramed=false&lang=en_US&_ts=1401271443305.631", "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, width=" + w + ", height=" + h + ", top=" + top + ", left=" + left);
            return false;
        }

        function ShareOnGooglePl(ShareUrl) {
          
            var pageUrl = window.location.href;

            var DomainName = pageUrl.match(/^http:\/\/[^/]+/);
            ShareUrl = DomainName[0] + ShareUrl;
            var WindHref = ShareUrl;
            while (WindHref.indexOf(";") > 0) {
                WindHref = WindHref.replace(";", "%3B");

            }
            WindHref = WindHref.replace(":", "%3A");
            while (WindHref.indexOf("/") > 0) {
                WindHref = WindHref.replace("/", "%2F");

            }
            while (WindHref.indexOf("=") > 0) {
                WindHref = WindHref.replace("=", "%3D");

            }
            while (WindHref.indexOf("&") > 0) {
                WindHref = WindHref.replace("&", "%26");

            }
            WindHref = WindHref.replace("?", "%3F");
            //var htm = "<div class='clearfix UIShareStage_Preview'><div class='lfloat _ohe'><div class=UIShareStage_Image><img class='UIShareStage_Image img' src=;w=154&amp;h=154&amp;url=https%3A%2F%2Ffbstatic-a.akamaihd.net%2Frsrc.php%2Fv2%2Fy6%2Fr%2FYQEGe6GxI_M.png&amp;cfs=1&amp;upscale alt=></div></div><div class='UIShareStage_ShareContent _42ef'><div class=UIShareStage_Title><span dir=ltr>Social Plugins</span></div><div class=UIShareStage_Subtitle>developers.facebook.com</div><div class=UIShareStage_Summary><p class=UIShareStage_BottomMargin>Social plugins let you see what your friends have liked, commented on or shared on sites across the web.</p></div></div></div>";
            var w = 620;
            var h = 360;
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            window.open("https://plus.google.com/share?url=" + WindHref, "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, width=" + w + ", height=" + h + ", top=" + top + ", left=" + left);
            return false;
        }

    </script>
    <div id="divShd" class="opaqueLayer">
    </div>
    <div class="container content_area" style="z-index: 1;">
        <div class="row left_right_padding">
            <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" />
            <div class="signin_heading_div_float_left">
                <asp:Label ID="lblMainHeading" runat="server" CssClass="sign_in_heading float_left_simple"></asp:Label>
            </div>
            <div class="template_designing" id="template_designing">
                <div class=" ContainerProductDesc">
                    <div class="get_in_touch_box_ProdDetail rounded_corners">
                        <div class="">
                            <div class="secondDivGetTouchBox GetTouchBoxContainer col-md-3 col-lg-3 col-xs-12 col-sm-6">
                                <p class="GetTouchMainHeading">
                                    <asp:Label ID="Label2" Text="Marketing Tips" runat="server"></asp:Label>
                                </p>
                                <p class="SeperatorTouchBox">
                                    ......................................................................................................................
                                </p>
                                <p class="SpecsContainer">
                                    <asp:Label ID="lblMarketingTipsContainer" Text="Specifications" runat="server"></asp:Label>
                                </p>
                                <asp:Button ID="hrefReadMore" runat="server" Visible="false" Text="Read More" CssClass="paraReadMore"
                                    OnClick="lnkMoreDetail_Click" />
                            </div>
                            <div class="thirdDivGetTouchBox GetTouchBoxContainer col-md-3 col-lg-3 col-xs-12 col-sm-6">
                                <p style="">
                                    <asp:Image ID="WhisperImg" runat="server" CssClass="WhisperImg" />
                                </p>
                                <p class="SpecsContainer paraStartCreatingTS">
                                    Select a design below
                                    <br />
                                    or
                                </p>
                                <asp:Button ID="Button1" runat="server" CssClass="btn_upload_files_TS rounded_corners5"
                                    CausesValidation="False" Text="Upload your design " OnClientClick="showProgressbar();"
                                    OnClick="btnUploadDesign_Click" />
                            </div>
                            <div class="fourthDivGetTouchBox GetTouchBoxContainer col-md-3 col-lg-3 col-xs-12 col-sm-6">
                                <p class="GetTouchMainHeading">
                                    <asp:Label ID="lblPricetxt" Text="Online Prices" runat="server"></asp:Label>
                                    <span id="spanXVatT" runat="server" class="spanXVatTS">ex. VAT</span>
                                </p>
                                <p class="SeperatorTouchBox">
                                    ......................................................................................................................
                                </p>
                                <div class="priceMatrixContainer">
                                    <div id="PricingTblDiv" runat="server">
                                        <table width="70%">
                                            <tr>
                                                <td class="width45p" valign="top">
                                                    <table width="100%" cellpadding="5" cellspacing="0" class="pricetabl">
                                                        <asp:Repeater ID="rptPriceMatrix" runat="server" OnItemDataBound="rptPriceMatrix_ItemDataBound">
                                                            <HeaderTemplate>
                                                                <tr>
                                                                    <td class="product_detail_HeaderCell Fsize12 procu_detail_grid_cell wdthWrp" id="divQuantity"
                                                                        runat="server" style="text-align: left;">
                                                                        <div>
                                                                            Quantity
                                                                        </div>
                                                                    </td>
                                                                    <td id="tdHeadText1" runat="server" class="product_detail_HeaderCell Fsize12 procu_detail_grid_cell wdthWrp"
                                                                        style="text-align: right;">
                                                                        <asp:Label ID="lblHeaderText1" runat="server" Text="" />
                                                                    </td>
                                                                    <div class="clearBoth">
                                                                        &nbsp;
                                                                    </div>
                                                                </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="product_detail_item_cell Fsize12 procu_detail_grid_cell">
                                                                        <div style="width: 95px;">
                                                                            <input type="hidden" id="txtHiddenPriceMatrixID" value='<%# Eval("PriceMatrixID") %>'
                                                                                runat="server" />
                                                                            <input type="hidden" id="txtHiddenIsDiscounted" value='<%# Eval("IsDiscounted") %>'
                                                                                runat="server" />
                                                                            <span runat="server" id="spanFixedQuantity">
                                                                                <%# Eval("Quantity") %></span> <span runat="server" id="spanRangedQuantity">
                                                                                    <%# Eval("QtyRangeFrom")%>
                                                                                    -
                                                                                    <%# Eval("QtyRangeTo")%></span>
                                                                        </div>
                                                                    </td>
                                                                    <td id="td1" runat="server" class="product_detail_item_cell Fsize12 procu_detail_grid_cell"
                                                                        style="text-align: right;">
                                                                        <div id="matrixItemColumn1" runat="server" style="width: 95px;">
                                                                            <span id='Matt_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PricePaperType1")%>'>
                                                                                <asp:Label ID="lblMatt" runat="server" />
                                                                                <asp:Label ID="lblMattDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                                                    runat="server" Text='<%# Eval("PricePaperType1")%>' />
                                                                            </span>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <AlternatingItemTemplate>
                                                                <tr class="product_detail_alt_temp">
                                                                    <td class="product_detail_item_cell Fsize12 procu_detail_grid_cell">
                                                                        <div style="width: 95px;">
                                                                            <input type="hidden" id="txtHiddenPriceMatrixID" value='<%# Eval("PriceMatrixID") %>'
                                                                                runat="server" />
                                                                            <input type="hidden" id="txtHiddenIsDiscounted" value='<%# Eval("IsDiscounted") %>'
                                                                                runat="server" />
                                                                            <span runat="server" id="spanFixedQuantity">
                                                                                <%# Eval("Quantity") %></span> <span runat="server" id="spanRangedQuantity">
                                                                                    <%# Eval("QtyRangeFrom")%>
                                                                                    -
                                                                                    <%# Eval("QtyRangeTo")%></span>
                                                                        </div>
                                                                    </td>
                                                                    <td id="td1" runat="server" class="product_detail_item_cell  Fsize12 procu_detail_grid_cell"
                                                                        style="text-align: right;">
                                                                        <div id="matrixItemColumn1" runat="server" style="width: 95px;">
                                                                            <span id='Matt_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PricePaperType1")%>'>
                                                                                <asp:Label ID="lblMatt" runat="server" />
                                                                                <asp:Label ID="lblMattDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                                                    runat="server" Text='<%# Eval("PricePaperType1")%>' />
                                                                            </span>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </AlternatingItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                    <div class="product_detail_note">
                                                        <asp:Label ID="lblTaxLabel" runat="server" CssClass="lblTxtProdDet"></asp:Label>
                                                    </div>
                                                    <div class="clearBoth">
                                                        &nbsp;
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="firstDivGetTouchBox GetTouchBoxContainer col-md-3 col-lg-3 col-xs-12 col-sm-6">
                                <p class="GetTouchMainHeading">
                                    <asp:Label ID="ltrlOpt1" Text="Specification" runat="server"></asp:Label>
                                </p>
                                <p class="SeperatorTouchBox">
                                    ......................................................................................................................
                                </p>
                                <p class="SpecsContainer">
                                    <asp:Label ID="lblSpecificationContainer" Text="Specifications" runat="server"></asp:Label>
                                </p>
                                <asp:Button ID="lnkMoreDetail" runat="server" Visible="false" Text="Read More" CssClass="paraReadMore"
                                    OnClick="lnkMoreDetail_Click" />
                            </div>
                            <div class="clearBoth">
                                &nbsp;
                            </div>
                        </div>
                        <div>
                        </div>
                    </div>
                     <div class="clearBoth">
                    &nbsp;
                </div>
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
                <asp:Button ID="DefualtBTn" runat="server" Style="display: none;" />
                <asp:Panel ID="pnlSearchBox" runat="server" DefaultButton="DefualtBTn" CssClass="search_box cntSearchbox responsiveSearchCs rounded_corners col-md-2 col-lg-2 col-xs-12 col-sm-4"
                    ClientIDMode="Static">
                    <div class="search_box">
                        <table width="100%">
                            <%-- <tr>
                                <td class="fontSyleBold tdSearchOurDesign" style="white-space: nowrap;">
                                    <asp:Literal ID="ltrlSearchphrase" runat="server" Text="Search our designs"></asp:Literal>
                                </td>
                            </tr>--%>
                            <tr>
                                <td>
                                    <div class="searchBoxContianer_TS">
                                        <asp:HiddenField runat="server" ID="txtTemplateDesignerMappedCategoryName" />
                                        <asp:TextBox ID="txtSearchKeywords" runat="server" class="text_box185_ProdDesgn rounded_corners5"
                                            AutoPostBack="true" onfocus="RemoveWaterMark();" onfocusout="SetWaterMark();" Text="Search Phrase"></asp:TextBox>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Go" OnClientClick="SearchTemplates(); return false;"
                                            CssClass="search_button rounded_corners" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="spanWhiteBar">
                                        &nbsp;
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="Pad5px">
                                    <img id="ToogleBaseColors" runat="server" class="IndsToogleImg H4B" alt="" src="~/images/ToogleSign.PNG" />
                                    <asp:Label ID="lblBaseColors" runat="server" Text="Colour" CssClass="lbltxtIndsType"></asp:Label>
                                    <asp:Button ID="btnClearColors" runat="server" Text="All" OnClientClick="ClearBaseColorSelection(); return false;"
                                        CssClass="clear_button rounded_corners float_right" />
                                </td>
                            </tr>
                            <tr>
                                <td class="Pad5px">
                                    <div id="BaseColorsContainer" runat="server">
                                    </div>
                                    <asp:DropDownList ID="cmbStyles" runat="server" CssClass="dropdown160 rounded_corners5"
                                        Visible="false">
                                        <asp:ListItem>Select Style</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="spanTDGryBar">
                                        &nbsp;
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="Pad5px">
                                    <img id="ToogleIndustryTypes" runat="server" class="IndsToogleImg H4B" alt="" src="~/images/ToogleSign.PNG" />
                                    <asp:Label ID="ltrlIndusrtyType2" runat="server" Text="Style" CssClass="lbltxtIndsType"></asp:Label>
                                    <asp:Button ID="btnAllIndsTags" runat="server" Text="All" OnClientClick="SelectAllIndustryTag(); return false;"
                                        CssClass="clear_button rounded_corners float_right" />
                                </td>
                            </tr>
                            <tr>
                                <td class="Pad5px">
                                    <div id="IndustryElementsContainer" runat="server">
                                    </div>
                                    <%--<asp:DropDownList ID="cmbIndustryTypes" runat="server" CssClass="dropdown160 rounded_corners5"
                                            AutoPostBack="true" OnSelectedIndexChanged="cmbIndustryTypes_SelectedIndexChanged">
                                            <asp:ListItem>Select Industry Type</asp:ListItem>
                                        </asp:DropDownList>--%>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="clearBoth">
                    </div>
                </asp:Panel>
                <div id="scrollImgContainer" class="scrollImgContainer float_left_simple col-md-9 col-lg-9 col-xs-12 col-sm-6" style="padding: 0px !important; text-align:left;">
                </div>
               
                <div class="clearBoth">
                    &nbsp;
                </div>
                <a id="loadMoreTemplates" runat="server" class="loadmoretemplates hidden-lg visible-md visible-sm visible-xs" onclick="loadMoreTemplates();">Load more templates</a>
                <div id="demotemplateContainer"></div>
                <div id="templateDetailContainer" class="templateDetailContainer rounded_corners "
                    style="display: none;">
                    <img src="../App_Themes/S6/Images/iconSelectedTemplate.png" class="iconSelectedTemplate" />
                    <div id="pnlTempDetailSelection" runat="server" clientidmode="Static" class="FileUploaderPopup_Mesgbox  rounded_corners">
                        <div id="MAinContentdiv" class="pop_body_MesgPopUp_Tepmlate innerBodyTemplateCSTS">
                            <%--                                <div class="Width100Percent" style="margin-top: -4px;">
                                    <div class="exit_containerTS">
                                        <div  > Close
                                        </div>
                                    </div>
                                </div>--%>
                            <button onclick="HideBar(); return false;" class="exit_popup4" id="btnCancelMessageBox">
                                Close</button>
                            <div class="pnlTempDetail1">
                                <div class="float_left Width100Percent textAlignLeft divContainerTempSel temSelPnlTxt">
                                    <asp:Label ID="lblHeader" runat="server" CssClass="FileUploadHeaderText_Tepmlate "></asp:Label>
                                </div>
                                <div class="top_sub_section_bottom_space_Pink">
                                </div>
                                <div class="TSNameContainer">
                                    <div class="float_left_simple">
                                       
                                    </div>
                                </div>
                                <div class="clearBoth">
                                    &nbsp;
                                </div>
                                <div class="cntTemplatePages" style="float:left;">
                                    <asp:Label ID="lblTemplatePageName1" runat="server" CssClass="TemplateName1CS"></asp:Label>
                                     <div class="LCLB BD_Temp TSNameContainer">
                                    <div class="pad5">
                                        <div class="PDTCWB_Temp">
                                            <div class="PDTC_Temp FI_Temp">
                                                <img id="imgTempPage1" alt="" class="full_img_ThumbnailPath_Temp" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                </div>
                               <div class="cntTemplatePages" style="float:left;">
                                   <asp:Label ID="lblTemplatePageName2" runat="server" CssClass="TemplateName2CSTS"></asp:Label>
                               
                                <div class="LCLB BD_Temp SecondTemplatediv TSNameContainer">
                                    <div class="pad5">
                                        <div class="PDTCWB_Temp">
                                            <div class="PDTC_Temp FI_Temp">
                                                <img id="imgTempPageN" alt="" class="full_img_ThumbnailPath_Temp" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                   </div>
                                <div class="clearBoth">
                                    &nbsp;
                                </div>
                                <div style="margin-top: 20px;">
                                  <asp:Button ID="Button2" runat="server" Text="Design Now" CssClass="cursor_pointer designNowCS_TS textRighFloating rounded_corners5 btnMakeThisDesign"
                                      OnClick="btnPackThisDesgn_Click"  />
                                    <div class="multiBackAsFav">
                                        <div class="product_selcection_thumnail_button_container_right_TS">
                                            
                                            <div id="divFavoriteInd2" class="passive_star" title="Click to Add as Favorite">
                                                &nbsp;
                                            </div>
                                            <span id="#spanAddFav_TS" class="spanAddFav_TS  ">Mark this design as a favourite</span>
                                            
                                        </div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                    <div id="div1" runat="server">
                                        <ul class="listInstructionsBC" style="margin-top: 7px">
                                            <li>Change background images and colours </li>
                                            <li>Rearrange layout and text </li>
                                        </ul>
                                    </div>
                                    <asp:Button ID="btnMoreAboutThisDesgn" runat="server" Text="More on this design"
                                        CssClass="cursor_pointer MoreAboutDesignCS_TS rounded_corners5" OnClick="btnMoreAboutThisDesgn_Click"
                                        OnClientClick="showProgressbar();" />
                                    <div class="clearBoth">

                                    </div>
                                    <div style="float: right; margin-right: 20px;">
                                        <div id="cntgpShareLink" runat="server" style="float: right !important;" class="ShareGoogPluss">
                                        </div>
                                        <div id="cntLinkinShareBtn" runat="server" class="ShareLinkInBtn" style="float: right;">
                                        </div>
                                        <div id="cnttwitterSharelnk" runat="server" style="float: right; margin-top: 14px;">
                                            <em id="emSeTweet" runat="server" class="emShareTweet"></em>
                                            <a class="TweetsShareBtn" data-related="jasoncosta" data-lang="en" data-size="large" data-count="none"></a>



                                        </div>
                                        <div id="cntfacebookSharelnk" runat="server" style="float: right;" class="ShareFbBtn">
                                        </div>
                                        <!-- Place this tag after the last share tag. -->
                                        <script type="text/javascript">
                                            (function () {
                                                var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
                                                po.src = 'https://apis.google.com/js/platform.js';
                                                var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
                                            })();
                                        </script>
                                        <div>
                                            <script src="//platform.linkedin.com/in.js" type="text/javascript">
            lang: en_US
                                            </script>
                                        </div>

                                    </div>
                                    <div class="clearBoth">

                                    </div>
                                </div>
                                 <div class="clearBoth">

                                    </div>
                                <div id="multibackContainer" runat="server" style="display: none;">
                                    <div class="spacer10pxtop" style="color: #9594ab;">
                                        <asp:Label ID="lrtlMultiFc" CssClass="left_align ltrlMsgMultiback" runat="server"
                                            Text="Multi Backs "></asp:Label>
                                    </div>
                                    <div class="clearBoth">
                                        &nbsp;
                                    </div>
                                    <div id="SubContent" class="multibackContainerSC1">
                                    </div>
                                    <%-- <div id="SubContent2" class="multibackContainerSC2">
                                 
                                    <br />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnMS" runat="server" Text="See Matching Set(s)" CssClass="cursor_pointer MoreAboutDesignCS_TS rounded_corners5"
                                        OnClientClick="javascript:openMS(); return false;" />
                                    <asp:Button ID="btnUploadDesign" runat="server" Text="Upload your design" CssClass="MoreAboutDesignCS_TS textRighFloating rounded_corners5"
                                        OnClick="btnUploadDesign_Click" OnClientClick="return ShowMessage();"  />
                                </div>--%>
                                </div>
                                <div id="StaticDataContainer" runat="server" style="display: none;">
                                    <div class="leftDataContainerTDetail1">
                                        <br />
                                        <span style="color: #F770E1;">Let us design it for you.</span>
                                        <br />
                                        <span>Seen a design concept you like but</span>
                                        <br />
                                        <span>want to discuss with us first? Give us a call. </span>
                                    </div>
                                    <div class="leftDataContainerTDetail">
                                        <div class="IframeCompanyLogoCs MargnRght10">
                                            <img id="imgLogo" runat="server" alt="" style="border: 0px; max-height: 70px; width: 345px; margin-left: 14px;" />
                                        </div>
                                        <div style="width: 200px; margin-left: 2px;">
                                            <div id="ImagePhone" class="TelLogoCs float_left_simple" runat="server">
                                            </div>
                                            <asp:Label ID="lblTel" runat="server" Style="color: #207DB8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-weight: bold; font-style: normal; font-weight: normal; font-size: 15px; line-height: 16px; float: left; margin-top: 4px;" /><br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="pnlTempDetail2" style="display: none;">
                                <div class="float_left Width100Percent textAlignLeft divContainerTempSel temSelPnlTxtBck">
                                    <asp:Label ID="Label5" runat="server" CssClass="FileUploadHeaderText_Tepmlate ">1. Enter your quick text details</asp:Label>
                                    <asp:Label ID="Label6" runat="server" CssClass="FileUploadHeaderText_Tepmlate floatright">2. Modify design</asp:Label>
                                </div>
                                <div class="top_sub_section_bottom_space_Pink">
                                </div>
                                <div class="TSNameContainer">
                                    <div class="float_left_simple">
                                        <asp:Label ID="Label3" runat="server" CssClass="TemplateName1CS"></asp:Label>
                                        <asp:Label ID="Label4" runat="server" CssClass="TemplateName2CSTSs">Front</asp:Label>
                                    </div>
                                </div>
                                <div class="clearBoth">
                                    &nbsp;
                                </div>
                                <div class=" BD_Temp TSNameContainer" style="color:black; float:left;">
                                    <div class="pad5 divQTContainerTS">
                                        <div id="QuickTextItemContainer" class="QuickTextItemContainer">
                                            <div class="panelQuickTextFormRow ">
                                                <input id="txtQName" maxlength="500" class="InputQtxtTS" runat="server" clientidmode="Static"
                                                    placeholder="Your Name" value="Your Name" />
                                            </div>
                                            <div class="panelQuickTextFormRow ">
                                                <input id="txtQTitle" maxlength="500" class="InputQtxtTS" runat="server" clientidmode="Static"
                                                    placeholder="Your Title" value="Your Title" />
                                            </div>
                                            <div class="panelQuickTextFormRow ">
                                                <input id="txtQCompanyName" maxlength="500" class="InputQtxtTS" runat="server" clientidmode="Static"
                                                    placeholder="Your Company Name" value="Your Company Name" />
                                            </div>
                                            <div class="panelQuickTextFormRow ">
                                                <input id="txtQAddressLine1" maxlength="500" class="InputQtxtTS" runat="server" clientidmode="Static"
                                                    placeholder="Address Line 1" value="Address Line 1" />
                                            </div>
                                            <div class="panelQuickTextFormRow ">
                                                <input id="txtQPhone" maxlength="500" class="InputQtxtTS" runat="server" clientidmode="Static"
                                                    placeholder="Telephone / Other" value="Telephone / Other" />
                                            </div>
                                            <div class="panelQuickTextFormRow ">
                                                <input id="txtQFax" maxlength="500" class="InputQtxtTS" runat="server" clientidmode="Static"
                                                    placeholder="Fax / Other" value="Fax / Other" />
                                            </div>
                                            <div class="panelQuickTextFormRow ">
                                                <input id="txtQEmail" maxlength="500" class="InputQtxtTS" runat="server" clientidmode="Static"
                                                    placeholder="Email address / Other" value="Email address / Other" />
                                            </div>
                                            <div class="panelQuickTextFormRow ">
                                                <input id="txtQWebsite" maxlength="500" class="InputQtxtTS" runat="server" clientidmode="Static"
                                                    placeholder="Website address" value="Website address" />
                                            </div>
                                            <div class="panelQuickTextFormRow ">
                                                <input id="txtQCompanyMessage" maxlength="500" class="InputQtxtTS" runat="server"
                                                    clientidmode="Static" placeholder="Your Company Message" value="Your Company Message" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="margin-left:7px; float:left;">

                               <asp:Label ID="Label1" runat="server" CssClass="FileUploadHeaderText_Tepmlate visible-sm visible-xs">2. Modify design</asp:Label>
                                <div class=" BD_Temp TSNameContainer" style="border: 0px;">
                                    <div class="pad5">
                                        <div class="PDTCWB_Temp">
                                            <div class="PDTC_Temp FI_Temp ImgBkTempSel">
                                                <img id="imgTempPageNBack" alt="" class="full_img_ThumbnailPath_Temp" />
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Button ID="btnEditThisDesign" runat="server" Text="Next" CssClass="btnNextCS_TS textRighFloating rounded_corners5 btnMakeThisDesign"
                                        OnClick="btnPackThisDesgn_Click" OnClientClick="SaveQuickTxt(); return ShowMessage(); " />
                                </div>
                                      </div>
                                <div class="clearBoth">
                                    &nbsp;
                                </div>
                                <div id="divBcInstructions" runat="server">
                                    <ul class="listInstructionsBC" style="margin-top: -8px;">
                                        <li>Upload your logo </li>
                                        <li>Change font styles and colours </li>
                                    </ul>
                                </div>
                            </div>
                             <div class="clearBoth">

                                    </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
    </div>
    <asp:HiddenField runat="server" ID="txtCustomerID" Value="0" />
    <asp:HiddenField runat="server" ID="txtContactID" Value="0" />
    <asp:HiddenField runat="server" ID="txtQCustomerID" Value="0" />
    <asp:HiddenField runat="server" ID="hfProductTemplateID" Value="0" />
    <asp:HiddenField runat="server" ID="hfTempProductName" Value=" " />
    <asp:HiddenField ID="hfEditTempType" runat="server" />
    <asp:HiddenField ID="hfState" runat="server" ClientIDMode="Predictable" />
    <asp:HiddenField runat="server" ID="hfTextBoxText" Value="" />
    <asp:HiddenField ID="hfSelectedCat" runat="server" Value="0" />
    <asp:HiddenField ID="hfContactID" runat="server" Value="0" />
    <asp:HiddenField ID="hfCategoryId" runat="server" Value="0" />
    <asp:HiddenField ID="hfItemId" runat="server" Value="0" />
    <asp:HiddenField runat="server" ID="hfFTemplateId" Value="0" />
    <asp:HiddenField runat="server" ID="hfFTempName" Value=" " />
    <asp:HiddenField runat="server" ID="hfItemIDFulPrice" Value="0" />
    <input type="hidden" id="txtIsQuantityRanged" runat="server" />
    <asp:HiddenField ID="hfBrokerMarkup" runat="server" Value="0" />
    <asp:HiddenField ID="hfContactMarkup" runat="server" Value="0" />
    <asp:HiddenField ID="txtHiddenProductCategoryID" runat="server" />
    <asp:HiddenField ID="txtHiddenItemDiscountRate" runat="server" />
    <asp:HiddenField ID="hfBaseColorID" runat="server" Value="0" />
</asp:Content>
