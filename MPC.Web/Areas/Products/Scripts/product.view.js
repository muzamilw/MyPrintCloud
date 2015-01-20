/*
    View for the Product. Used to keep the viewmodel clear of UI related logic
*/
define("product/product.view",
    ["jquery", "product/product.viewModel"], function ($, productViewModel) {

        var ist = window.ist || {};

        // View 
        ist.product.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#productBinding")[0],
                // Change View - List/Grid View
                changeView = function (element) {
                    var elementId = element.currentTarget.id;
                    if (elementId === "listViewIcon") {
                        if (viewModel.isListViewVisible()) {
                            return;
                        }

                        viewModel.setListViewActive();
                    }
                    else if (elementId === "gridViewIcon") {
                        if (viewModel.isGridViewVisible()) {
                            return;
                        }

                        viewModel.setGridViewActive();
                    }
                },
                // Show Video the dialog
                showVideoDialog = function () {
                    $("#productVideoDialog").modal("show");
                },
                // Hide Video the dialog
                hideVideoDialog = function () {
                    $("#productVideoDialog").modal("hide");
                },
                // Show RelatedItem the dialog
                showRelatedItemDialog = function () {
                    $("#relatedProductDialog").modal("show");
                },
                // Hide RelatedItem the dialog
                hideRelatedItemDialog = function () {
                    $("#relatedProductDialog").modal("hide");
                },
                // Show StockItem the dialog
                showStockItemDialog = function () {
                    $("#stockDialog").modal("show");
                },
                // Hide StockItem the dialog
                hideStockItemDialog = function () {
                    $("#stockDialog").modal("hide");
                },
                // Show ItemAddonCostCentre the dialog
                showItemAddonCostCentreDialog = function () {
                    $("#itemAddonCostCentreDialog").modal("show");
                },
                // Hide ItemAddonCostCentre the dialog
                hideItemAddonCostCentreDialog = function () {
                    $("#itemAddonCostCentreDialog").modal("hide");
                },
                // Show Product Category dialog
                showProductCategoryDialog = function () {
                    $("#productCategoryDialog").modal("show");
                },
                // Hide Product Category dialog
                hideProductCategoryDialog = function () {
                    $("#productCategoryDialog").modal("hide");
                },
                // Go To Element with Validation Errors
                gotoElement = function (element) {
                    var tab = $(element).closest(".tab-pane");
                    if (!tab) {
                        return;
                    }

                    var liElement = $('a[href=#' + tab.attr('id') + ']');
                    if (!liElement) {
                        return;
                    }

                    liElement.click();

                    // Scroll to Element
                    setTimeout(function () {
                        window.scrollTo($(element).offset().left, $(element).offset().top - 50);
                        // Focus on element
                        $(element).focus();
                    }, 1000);
                },
                // Show/Hide Child Categories
                toggleChildCategories = function (event) {

                    if (!event) {
                        return false;
                    }

                    var targetElement = $(event.target);
                    if (!targetElement) {
                        return false;
                    }

                    var childList = targetElement.closest('li').children('ol');
                    if (childList.length === 0) {
                        return false;
                    }

                    // Toggle Child List
                    if (childList.is(':hidden')) {
                        childList.show();
                    }
                    else {
                        childList.hide();
                    }

                    return true;
                },
                // Get Category Id From li
                getCategoryIdFromliElement = function (categoryliElement) {
                    var categoryliId = categoryliElement.id.split("-");
                    if (!categoryliId || categoryliId.length < 2) {
                        return null;
                    }

                    var categoryId = categoryliId[1];
                    if (!categoryId) {
                        return null;
                    }

                    return parseInt(categoryId);
                },
                // Get Category Id from Binding li Element
                getCategoryIdFromElement = function(event) {
                    if (!event || !event.target) {
                        return null;
                    }

                    var categoryliElement = $(event.target).closest('li')[0];
                    if (!categoryliElement) {
                        return null;
                    }

                    return getCategoryIdFromliElement(categoryliElement);
                },
                // Append Child Category to list
                appendChildCategory = function(event, category) {
                    if (!event) {
                        return;
                    }

                    var targetElement = $(event.target).closest('li');
                    if (!targetElement) {
                        return;
                    }
                    
                    var inputElement = category.isSelected() ?
                        '<input type="checkbox" checked="checked" data-bind="click: $root.updateCheckedStateForCategory"  />' :
                        '<input type="checkbox" data-bind="click: $root.updateCheckedStateForCategory" />';

                    var childCategoryHtml = '<ol class="dd-list"> ' +
                        '<li class="dd-item dd-item-list" id="liElement-' + category.id + '"> ' +
                        '<div class="dd-handle-list" data-bind="click: $root.toggleChildCategories"><i class="fa fa-bars"></i></div>' +
                        '<div class="dd-handle">' +
                        '<span>' + category.name + '</span>' +
                        '<div class="nested-links"> ' +
                        inputElement +
                        '</div></div></li></ol>';

                    targetElement.append(childCategoryHtml);

                    ko.applyBindings(viewModel, $("#liElement-" + category.id)[0]);
                },
                // Update Input Checked States
                updateInputCheckedStates = function() {
                    $.each($("#productCategoryDialogCategories").find("input:checkbox"), function(index, inputElement) {
                        var categoryliElement = $(inputElement).closest('li')[0];
                        if (categoryliElement) {
                            var categoryId = getCategoryIdFromliElement(categoryliElement);
                            if (categoryId) {
                                var category = viewModel.productCategories.find(function (productCategory) {
                                    return productCategory.id === categoryId;
                                });

                                if (category) {
                                    if (category.isSelected()) {
                                        $(inputElement).prop('checked', true);
                                    }
                                    else {
                                        $(inputElement).prop('checked', false);    
                                    }
                                }
                            }
                        }
                    });
                },
                // Initiate Dropzone events 
                initiateDropzoneEvents = function (element, itemId, itemImageType, imageCaption, filePath) {

                    var self = element;
                    self.on("sending", function (file, xhr, formData) {
                        formData.append("itemId", itemId);
                        formData.append("imageFileType", itemImageType);
                    });

                    self.on("removedfile", function () {
                        $.ajax({
                            type: 'post',
                            url: '/Products/Home/DeleteImage?itemId=' + itemId + '&imageFileType=' + itemImageType,
                            success: function () {
                                toastr.success(imageCaption + " removed successfully!");
                            }
                        });
                    });

                    self.on("addedfile", function (file) {
                        var img = $(file.previewTemplate).find("img");
                        img[0].onload = function () {
                            var max = this.width > this.height ? this.width : this.height;
                            var ratio = 100.0 / max;

                            var width = this.width * ratio;
                            var height = this.height * ratio;

                            img.css({
                                width: width + "px",
                                height: height + "px",
                                top: ((100 - height) / 2) + "px",
                                left: ((100 - width) / 2) + "px"
                            });
                        };
                    });

                    if (!filePath) {
                        return;
                    }

                    var mockFile = { name: imageCaption, size: 12345, type: 'image/jpeg' };
                    self.emit('addedfile', mockFile);
                    self.emit('thumbnail', mockFile, filePath);
                },
                // Initialize Dropzones
                initializeDropZones = function () {

                    // Create Dropzone's
                    // "demoUpload1" is the HTML element's ID
                    if (!$("#demoUpload1").dropzone) {
                        $("#demoUpload1").dropzone({
                            paramName: "file", // The name that will be used to transfer the file
                            maxFilesize: 1,
                            addRemoveLinks: true,
                            dictRemoveFile: "Delete",
                            init: function () {
                                initiateDropzoneEvents(this, viewModel.selectedProduct().id(), viewModel.itemFileTypes.thumbnail, "Thumbnail",
                                    viewModel.selectedProduct().thumbnail());
                            }
                        });
                    }

                    if (!$("#demoUpload2").dropzone) {
                        // Image Path
                        $("#demoUpload2").dropzone({
                            paramName: "file", // The name that will be used to transfer the file
                            maxFilesize: 1,
                            addRemoveLinks: true,
                            dictRemoveFile: "Delete",
                            init: function () {
                                initiateDropzoneEvents(this, viewModel.selectedProduct().id(), viewModel.itemFileTypes.imagePath, "Image Path",
                                    viewModel.selectedProduct().imagePath());
                            }
                        });
                    }

                    if (!$("#demoUpload3").dropzone) {
                        // Grid Image
                        $("#demoUpload3").dropzone({
                            paramName: "file", // The name that will be used to transfer the file
                            maxFilesize: 1,
                            addRemoveLinks: true,
                            dictRemoveFile: "Delete",
                            init: function () {
                                initiateDropzoneEvents(this, viewModel.selectedProduct().id(), viewModel.itemFileTypes.grid, "Grid",
                                    viewModel.selectedProduct().gridImage());
                            }
                        });
                    }


                    if (!$("#demoUpload4").dropzone) {
                        // Files
                        $("#demoUpload4").dropzone({
                            paramName: "file", // The name that will be used to transfer the file
                            maxFilesize: 1,
                            addRemoveLinks: true,
                            dictRemoveFile: "Delete",
                            init: function () {
                                initiateDropzoneEvents(this, viewModel.selectedProduct().id(), viewModel.itemFileTypes.file1, "File1",
                                    viewModel.selectedProduct().file1());
                            }
                        });
                    }

                },
                // Show Basic Details Tab when Product Detail Opens up
                showBasicDetailsTab = function() {
                    var liElement = $('a[href=#tab-ProdNameAndImages]');
                    var liDetailsPageandSection = $('a[href=#tab-ProductDetailSection]');
                    if (!liElement || !liDetailsPageandSection) {
                        return;
                    }

                    // Show Basic Details
                    setTimeout(function () {
                        liDetailsPageandSection.click();
                        liElement.click();
                    }, 1000);
                },
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                changeView: changeView,
                showVideoDialog: showVideoDialog,
                hideVideoDialog: hideVideoDialog,
                showRelatedItemDialog: showRelatedItemDialog,
                hideRelatedItemDialog: hideRelatedItemDialog,
                showStockItemDialog: showStockItemDialog,
                hideStockItemDialog: hideStockItemDialog,
                showItemAddonCostCentreDialog: showItemAddonCostCentreDialog,
                hideItemAddonCostCentreDialog: hideItemAddonCostCentreDialog,
                initializeDropZones: initializeDropZones,
                gotoElement: gotoElement,
                toggleChildCategories: toggleChildCategories,
                appendChildCategory: appendChildCategory,
                showProductCategoryDialog: showProductCategoryDialog,
                hideProductCategoryDialog: hideProductCategoryDialog,
                getCategoryIdFromElement: getCategoryIdFromElement,
                updateInputCheckedStates: updateInputCheckedStates,
                showBasicDetailsTab: showBasicDetailsTab
            };
        })(productViewModel);

        // Initialize the view model
        if (ist.product.view.bindingRoot) {
            productViewModel.initialize(ist.product.view);
        }
        return ist.product.view;
    });
