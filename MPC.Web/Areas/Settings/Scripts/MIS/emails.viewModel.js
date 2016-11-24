
define("emails/emails.viewModel",
    ["jquery", "amplify", "ko", "emails/emails.dataservice", "emails/emails.model", "common/confirmation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation) {
        var ist = window.ist || {};
        ist.emails = {
            viewModel: (function () {
                var // The view 
                    view,
                    selectedEmail = ko.observable(),
                    selectedSection = ko.observable(),
                    selectedEmailListViewItem = ko.observable(),
                    emailIdCounter = ko.observable(0),
                    ckEditorOpenFrom = ko.observable("Campaign"),
                    emailsList = ko.observableArray([]),
                    campaignSectionFlags = ko.observableArray([]),
                    campaignCompanyTypes = ko.observableArray([]),
                    campaignGroups = ko.observableArray([]),
                    emailCampaignSections = ko.observableArray([]),
                    editedCampaigns = ko.observableArray([]),
                    emailEvents = ko.observableArray([]),
                    isMisEmail = ko.observable(true),
                    getNotificationEmails = function () {
                        dataservice.getCampaigns({
                            success: function (data) {
                                if (data != null) {
                                    emailsList.removeAll();
                                    var mails = [];
                                    _.each(data.OrganisationEmails, function (item) {
                                        var campaign = model.Campaign.Create(item);
                                        _.each(item.CampaignImages, function (campaignImage) {
                                            campaign.campaignImages.push(model.CampaignImage.Create(campaignImage));
                                        });
                                        mails.push(campaign);
                                    });
                                    ko.utils.arrayPushAll(emailsList(), mails);
                                    emailsList.valueHasMutated();
                                    //Email Sections
                                    emailCampaignSections.removeAll();
                                    _.each(data.CampaignSections, function (item) {
                                        var section = model.CampaignSection.Create(item);
                                        _.each(item.CampaignEmailVariables, function (emailVariable) {
                                            section.campaignEmailVariables.push(model.CampaignEmailVariable.Create(emailVariable));
                                        });
                                        emailCampaignSections.push(section);
                                    });
                                    //Email Events
                                    emailEvents.removeAll();
                                    if (data.EmailEvents !== null) {
                                        ko.utils.arrayPushAll(emailEvents(), data.EmailEvents);
                                        emailEvents.valueHasMutated();
                                    }
                                }
                                
                            },
                            error: function () {
                                toastr.error("Error: Failed To load emails ", "", ist.toastrOptions);
                            }
                        });
                        
                    },
                    onEditEmail = function (campaign) {
                        selectedEmailListViewItem(campaign);
                        ckEditorOpenFrom("Campaign");
                        if (campaign.id() < 0) {
                            setCampaignDetail(campaign);
                        } else {
                            var campaignItem = _.find(editedCampaigns(), function (item) {
                                return item.id() === campaign.id();
                            });
                            if (campaignItem) {
                                setCampaignDetail(campaignItem);
                            } else {
                                getCampaignDetail(campaign);
                            }
                        }
                    },
                    dobeforeSaveEmail = function () {
                        var flag = true;
                        if (!selectedEmail().isValid()) {
                            selectedEmail().errors.showAllMessages();
                            flag = false;
                        }
                        return flag;
                    },
                   onSaveEmail = function () {
                       var emailMessage = CKEDITOR.instances.content.getData();
                       selectedEmail().hTMLMessageA(emailMessage);
                       if (dobeforeSaveEmail()) {
                           dataservice.saveEmailCampaign(selectedEmail().convertToServerData(selectedEmail()), {
                               success: function (data) {
                                   if (data != null) {
                                       selectedEmail(undefined);
                                       selectedEmailListViewItem().eventName(data.EventName);
                                       selectedEmailListViewItem().campaignName(data.CampaignName);
                                       selectedEmailListViewItem().isEnabled(data.IsEnabled);
                                       view.hideEmailCamapaignDialog();
                                       toastr.success("Campaign saved successfully.");
                                   }
                               },
                               error: function (response) {
                                   toastr.error("Error: Failed To save campaign " + response, "", ist.toastrOptions);
                               }
                           });
                           //var emailMessage = CKEDITOR.instances.content.getData();
                           //email.hTMLMessageA(emailMessage);
                           //if (email.campaignType() === 3) {
                           //    var flags = null;
                           //    _.each(campaignSectionFlags(), function (item) {
                           //        if (item.isChecked()) {
                           //            if (flags === null) {
                           //                flags = item.id();
                           //            } else {
                           //                flags = flags + "," + item.id();
                           //            }
                           //        }
                           //    });
                           //    email.flagIDs(flags);
                           //    var cTypes = null;
                           //    _.each(campaignCompanyTypes(), function (item) {
                           //        if (item.isChecked()) {
                           //            if (cTypes === null) {
                           //                cTypes = item.id();
                           //            } else {
                           //                cTypes = cTypes + "," + item.id();
                           //            }
                           //        }
                           //    });
                           //    email.customerTypeIDs(flags);
                           //    var groups = "";
                           //    _.each(campaignGroups(), function (item) {
                           //        if (item.isChecked()) {
                           //            if (groups === null) {
                           //                groups = item.id();
                           //            } else {
                           //                groups = groups + "," + item.id();
                           //            }
                           //        }
                           //    });
                           //    email.groupIDs(groups);
                           //}

                           //if (email.emailEventId() !== undefined) {
                           //    _.each(emailEvents(), function (item) {
                           //        if (item.EmailEventId === email.emailEventId()) {
                           //            email.eventName(item.EventName);
                           //        }
                           //    });
                           //}
                           ////New Added
                           //if (email.id() === undefined) {
                           //    email.id(emailIdCounter() - 1);
                           //    emails.splice(0, 0, email);
                           //    newAddedCampaigns.push(email);
                           //    emailIdCounter(emailIdCounter() - 1);
                           //} else {
                           //    //Not save item, edit case
                           //    if (email.id() < 0) {
                           //        var campaign = _.find(newAddedCampaigns(), function (item) {
                           //            return item.id() === email.id();
                           //        });
                           //        if (campaign) {
                           //            newAddedCampaigns.remove(campaign);
                           //            newAddedCampaigns.push(email);
                           //        }
                           //    }
                           //    //Edit Email
                           //    if (email.id() > 0) {
                           //        var campaignItem = _.find(editedCampaigns(), function (item) {
                           //            return item.id() === email.id();
                           //        });
                           //        if (campaignItem) {
                           //            editedCampaigns.remove(campaignItem);
                           //        }
                           //        editedCampaigns.push(email);
                           //    }
                           //    updateCampaignListViewItem(email);
                           //}
                           //email.reset();
                           
                          
                       }
                   },
                    updateCampaignListViewItem = function (email) {
                        selectedEmailListViewItem().eventName(email.eventName());
                        selectedEmailListViewItem().startDateTime(email.startDateTime());
                        selectedEmailListViewItem().isEnabled(email.isEnabled());
                        selectedEmailListViewItem().campaignType(email.campaignType());
                        selectedEmailListViewItem().sendEmailAfterDays(email.sendEmailAfterDays());
                        selectedEmailListViewItem().campaignName(email.campaignName());
                        selectedEmailListViewItem().reset();
                    },
                   setCampaignDetail = function (campaign) {
                       selectedEmail(campaign);
                       selectedEmail().reset();
                       view.showEmailCamapaignDialog();
                       
                       if (selectedEmail().campaignType() === 3) {
                           resetEmailBaseDataArrays();
                           if (campaign.flagIDs() !== null && campaign.flagIDs() !== undefined) {
                               var flagIds = campaign.flagIDs().split(',');
                               for (var i = 0; i < flagIds.length; i++) {
                                   _.each(campaignSectionFlags(), function (item) {
                                       if (parseInt(flagIds[i]) === item.id()) {
                                           item.isChecked(true);
                                       }

                                   });
                               }
                           }
                           if (campaign.customerTypeIDs() !== null && campaign.customerTypeIDs() !== undefined) {
                               var customerTypeIDs = campaign.customerTypeIDs().split(',');
                               for (var j = 0; j < customerTypeIDs.length; j++) {
                                   _.each(campaignCompanyTypes(), function (item) {
                                       if (parseInt(customerTypeIDs[j]) === item.id()) {
                                           item.isChecked(true);
                                       }

                                   });
                               }
                           }
                           if (campaign.groupIDs() !== null && campaign.groupIDs() !== undefined) {
                               var groupIDs = campaign.groupIDs().split(',');
                               for (var k = 0; k < groupIDs.length; k++) {
                                   _.each(campaignGroups(), function (item) {
                                       if (parseInt(groupIDs[k]) === item.id()) {
                                           item.isChecked(true);
                                       }
                                   });
                               }
                           }
                       }
                       makeCkeditorDropable();
                   },
                    resetEmailBaseDataArrays = function () {
                        _.each(campaignSectionFlags(), function (item) {
                            item.isChecked(false);
                        });
                        _.each(campaignCompanyTypes(), function (item) {
                            item.isChecked(false);
                        });

                        _.each(campaignGroups(), function (item) {
                            item.isChecked(false);
                        });
                    },
                     makeCkeditorDropable = function () {
                         setTimeout(
                             function () {
                                 $(CKEDITOR.instances.content.container.find('iframe').$[0]).droppable({
                                     tolerance: 'pointer',
                                     hoverClass: 'dragHover',
                                     activeClass: 'dragActive',
                                     drop: function (evt, ui) {
                                         droppedEmailSection(ui.helper.data('ko.draggable.data'), null, evt);
                                     }
                                 });
                             }, 4000);
                     },
                    selectSection = function (section) {
                        //old menu collapse
                        if (selectedSection() !== undefined) {
                            selectedSection().isExpanded(false);
                        }
                        //new selected section expand
                        section.isExpanded(true);
                        selectedSection(section);
                    },
                    draggedSection = function (source) {

                        return {
                            row: source.$parent,
                            section: source.$data
                        };
                    },
                    draggedImage = function (source) {
                        return {
                            row: source.$parent,
                            image: source.$data
                        };
                    },
                    draggedEmailVaribale = function (source) {
                        return {
                            row: source.$parent,
                            emailVariable: source.$data
                        };
                    },
                    campaignEmailImagesLoadedCallback = function (file, data) {
                        var campaignImage = model.CampaignImage();
                        campaignImage.imageName(file.name);
                        campaignImage.imageSource(data);
                        selectedEmail().campaignImages.push(campaignImage);
                    },
                     droppedEmailSection = function (source, target, event) {
                         var hTMLMessageA = CKEDITOR.instances.content.getData();
                         if (selectedEmail() !== undefined && source !== undefined && source !== null && source.section !== undefined && source.section !== null) {
                             //variableName //sectionName
                             selectedEmail().hTMLMessageA(hTMLMessageA + source.section.sectionName());
                         } else if (selectedEmail() !== undefined && source !== undefined && source !== null && source.emailVariable !== undefined && source.emailVariable !== null) {
                             selectedEmail().hTMLMessageA(hTMLMessageA + source.emailVariable.variableTag());
                         } else if (selectedEmail() !== undefined && source !== undefined && source !== null && source.image !== undefined && source.image !== null) {
                             // var img = "<img  src=" + source.image.imageSource() + "/>";
                             var img = "<img width=\"100px\"  height=\"100px\" src=\"" + source.image.imageSource() + "\"/>";
                             selectedEmail().hTMLMessageA(hTMLMessageA + img);
                             //selectedEmail().hTMLMessageA(); //imageSource
                         }
                     },
                    getCampaignDetail = function (campaign) {
                        dataservice.getCampaignDetailById({
                            campaignId: campaign.id(),
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    var campaignDetail = model.Campaign.Create(data);
                                    _.each(data.CampaignImages, function (campaignImage) {
                                        campaignDetail.campaignImages.push(model.CampaignImage.Create(campaignImage));
                                    });
                                    setCampaignDetail(campaignDetail);
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to load Detail . Error: ");
                            }
                        });
                    },
                    // Delete Email
                    onDeleteEmail = function (email) {
                        // Ask for confirmation
                        confirmation.messageText("WARNING - This item will be archived from the system and you won't be able to use it");
                        confirmation.afterProceed(function () {
                            emails.remove(selectedEmailListViewItem());
                            view.hideEmailCamapaignDialog();
                            if (selectedEmailListViewItem().id() > 0) {
                                deletedCampaigns.push(selectedEmailListViewItem());
                            }
                            removeCampaignFromAddEditList(selectedEmailListViewItem());
                        });
                        confirmation.show();

                    },
                    removeCampaignFromAddEditList = function (email) {
                        var campaign = _.find(newAddedCampaigns(), function (item) {
                            return item.id() === email.id();
                        });
                        if (campaign) {
                            newAddedCampaigns.remove(campaign);
                        }
                        var campaignItem = _.find(editedCampaigns(), function (item) {
                            return item.id() === email.id();
                        });
                        if (campaignItem) {
                            editedCampaigns.remove(campaignItem);
                        }
                    },
                    onCloseCampaignEditor = function() {
                      if (selectedEmail().hasChanges()) {
                          confirmation.messageText("Do you want to save changes?");
                          confirmation.afterProceed(onSaveEmail);
                          confirmation.afterCancel(function () {
                              view.hideEmailCamapaignDialog();
                          });
                          confirmation.show();
                      } else {
                          view.hideEmailCamapaignDialog();
                      }  
                    },
                    
                // Initialize the view model
                 initialize = function (specifiedView) {
                     view = specifiedView;
                     ko.applyBindings(view.viewModel, view.bindingRoot);
                     getNotificationEmails();
                 };

                return {
                    getNotificationEmails: getNotificationEmails,
                    initialize: initialize,
                    onEditEmail: onEditEmail,
                    selectedEmail: selectedEmail,
                    emailsList: emailsList,
                    campaignSectionFlags: campaignSectionFlags,
                    campaignCompanyTypes: campaignCompanyTypes,
                    campaignGroups: campaignGroups,
                    emailCampaignSections: emailCampaignSections,
                    onSaveEmail: onSaveEmail,
                    onDeleteEmail: onDeleteEmail,
                    selectSection: selectSection,
                    droppedEmailSection: droppedEmailSection,
                    draggedSection: draggedSection,
                    draggedImage: draggedImage,
                    draggedEmailVaribale: draggedEmailVaribale,
                    campaignEmailImagesLoadedCallback: campaignEmailImagesLoadedCallback,
                    ckEditorOpenFrom: ckEditorOpenFrom,
                    selectedSection: selectedSection,
                    emailEvents: emailEvents,
                    isMisEmail: isMisEmail,
                    onCloseCampaignEditor: onCloseCampaignEditor
            };
            })()
        };

        return ist.emails.viewModel;
    });

