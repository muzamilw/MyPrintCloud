/*
Module with the view model for the My Organization.
*/
define("costcenter/costcenter.viewModel",
["jquery", "amplify", "ko", "costcenter/costcenter.dataservice", "costcenter/costcenter.model", "common/confirmation.viewModel", "common/pagination", "common/sharedNavigation.viewModel"],
function ($, amplify, ko, dataservice, model, confirmation, pagination, sharedNavigationVM) {
    var ist = window.ist || {};
    ist.costcenter = {
        viewModel: (function () {
            var // the view 
            view,
            // Active
            costCentersList = ko.observableArray([]),
            errorList = ko.observableArray([]),
            // Cost Center Types
            costCenterTypes = ko.observableArray([]),
            // System Users as Resources
            costCenterResources = ko.observableArray([]),
            deliveryCarriers = ko.observableArray([]),
            // Nominal Codes
            nominalCodes = ko.observableArray([]),
            costCenterVariables = ko.observableArray([]),
            // Markups
            markups = ko.observableArray([]),
            SelectedQuestionVariable = ko.observable(),
            costcenterVariableNodes = ko.observableArray([]),
            variableVariableNodes = ko.observableArray([]),
            resourceVariableNodes = ko.observableArray([]),
            questionVariableNodes = ko.observableArray([]),
            matrixVariableNodes = ko.observableArray([]),
            lookupVariableNodes = ko.observableArray([]),
            selectedCostCenterType = ko.observable(),
            selectedVariableType = ko.observable(),
            selectedcc = ko.observable(),
            fixedvarIndex = ko.observable(1),
            selectedVariableString = ko.observable(),
            CurrencySymbol = ko.observable(),
            showQuestionVariableChildList = ko.observable(0),
            // Cost Center Categories
            costCenterCategories = ko.observableArray([]),
            workInstructions = ko.observableArray([]),
            //{ Id: 1, Text: 'Cost Centers' },
            QuestionVariableType = ko.observableArray([
            { Id: 1, Text: 'General' },
            { Id: 2, Text: 'Multiple Options' },
            { Id: 3, Text: 'Yes/No' }
            ]),

            variablesTreePrent = ko.observableArray([
            { Id: 2, Text: 'Variables' },
            { Id: 4, Text: 'Questions' },
            { Id: 5, Text: 'Matrices' },
            { Id: 6, Text: 'Lookup' },
            { Id: 7, Text: 'Stock Items' }
            ]),
            fedexServiceTypes = ko.observableArray([{ Id: 1, Text: 'EUROPE_FIRST_INTERNATIONAL_PRIORITY' },
            { Id: 2, Text: 'FEDEX_1_DAY_FREIGHT' },
            { Id: 3, Text: 'FEDEX_2_DAY' },
            { Id: 4, Text: 'FEDEX_2_DAY_AM' },
            { Id: 5, Text: 'FEDEX_2_DAY_FREIGHT' },
            { Id: 6, Text: 'FEDEX_3_DAY_FREIGHT' },
            { Id: 7, Text: 'FEDEX_DISTANCE_DEFERRED' },
            { Id: 8, Text: 'FEDEX_EXPRESS_SAVER' },
            { Id: 9, Text: 'FEDEX_FIRST_FREIGHT' },
            { Id: 10, Text: 'FEDEX_FREIGHT_ECONOMY' },
            { Id: 11, Text: 'FEDEX_FREIGHT_PRIORITY' },
            { Id: 12, Text: 'FEDEX_GROUND' },
            { Id: 13, Text: 'FEDEX_NEXT_DAY_AFTERNOON' },
            { Id: 14, Text: 'FEDEX_NEXT_DAY_EARLY_MORNING' },
            { Id: 15, Text: 'FEDEX_NEXT_DAY_END_OF_DAY' },
            { Id: 16, Text: 'FEDEX_NEXT_DAY_FREIGHT' },
            { Id: 17, Text: 'FEDEX_NEXT_DAY_MID_MORNING' },
            { Id: 18, Text: 'GROUND_HOME_DELIVERY' },
            { Id: 19, Text: 'FIRST_OVERNIGHT' },
            { Id: 20, Text: 'INTERNATIONAL_ECONOMY' },
            { Id: 21, Text: 'INTERNATIONAL_ECONOMY_FREIGHT' },
            { Id: 22, Text: 'INTERNATIONAL_FIRST' },
            { Id: 23, Text: 'INTERNATIONAL_PRIORITY' },
            { Id: 24, Text: 'INTERNATIONAL_PRIORITY_FREIGHT' },
            { Id: 25, Text: 'PRIORITY_OVERNIGHT' },
            { Id: 26, Text: 'SAME_DAY' },
            { Id: 27, Text: 'SAME_DAY_CITY' },
            { Id: 28, Text: 'SMART_POST' },
            { Id: 29, Text: 'STANDARD_OVERNIGHT' }
            ]),

            getVariableTreeChildItems = function (Selecteddata) {
                if (Selecteddata.Id == 4) {
                    if (questionVariableNodes().length > 0) {
                        if (showQuestionVariableChildList() == 1) {
                            showQuestionVariableChildList(0);
                        } else {
                            showQuestionVariableChildList(1);
                        }

                    } else {
                        dataservice.GetTreeListById({
                            id: Selecteddata.Id,
                        }, {
                            success: function (data) {
                                questionVariableNodes.removeAll();
                                _.each(data.QuestionVariables, function (item) {
                                    var ques = model.QuestionVariable(item);
                                    questionVariableNodes.push(ques);
                                });
                                showQuestionVariableChildList(1);
                                view.showAddEditQuestionMenu();

                            },
                            error: function () {
                                toastr.error("Failed to load variables tree data.");
                            }
                        });
                    }

                }

            },
            getVariableTreeChildListItems = function (dataRecieved, event) {
                var id = $(event.target).closest('li')[0].id;
                if ($(event.target).closest('li').children('ol').children('li').length > 0) {
                    if ($(event.target).closest('li').children('ol').is(':hidden')) {
                        $(event.target).closest('li').children('ol').show();
                    } else {
                        $(event.target).closest('li').children('ol').hide();
                    }
                    return;
                }
                if (id == 1) {
                    dataservice.GetTreeListById({
                        id: id,
                    }, {
                        success: function (data) {
                            //CostCenterVariables
                            costcenterVariableNodes.removeAll();
                            ko.utils.arrayPushAll(costcenterVariableNodes(), data.CostCenterVariables);
                            costcenterVariableNodes.valueHasMutated();

                            _.each(costcenterVariableNodes(), function (ccType) {
                                $("#" + id).append('<ol class="dd-list"> <li class="dd-item dd-item-list"  id =' + ccType.TypeId + '> <div class="dd-handle-list" data-bind="click: $root.getCostcenterByCatId"><i class="fa fa-bars"></i></div><div class="dd-handle"><span >' + ccType.TypeName + '</span><div class="nested-links"></div></div></li></ol>');
                                ko.applyBindings(view.viewModel, $("#" + ccType.TypeId)[0]);
                            });

                        },
                        error: function () {
                            toastr.error("Failed to load variables tree data.");
                        }
                    });

                }
                if (id == 2) {
                    dataservice.GetTreeListById({
                        id: id,
                    }, {
                        success: function (data) {
                            //Variable Variables
                            variableVariableNodes.removeAll();
                            ko.utils.arrayPushAll(variableVariableNodes(), data.VariableVariables);
                            variableVariableNodes.valueHasMutated();

                            _.each(variableVariableNodes(), function (variable) {
                                $("#" + id).append('<ol class="dd-list"> <li class="dd-item dd-item-list" id =' + variable.CategoryId + 'vv' + '> <div class="dd-handle-list" data-bind="click: $root.getVariablesByType"><i class="fa fa-bars"></i></div><div class="dd-handle"><span>' + variable.Name + '</span><div class="nested-links"></div></div></li></ol>');
                                ko.applyBindings(view.viewModel, $("#" + variable.CategoryId + "vv")[0]);
                            });

                        },
                        error: function () {
                            toastr.error("Failed to load variables tree data.");
                        }
                    });

                }
                if (id == 3) {
                    dataservice.GetTreeListById({
                        id: id,
                    }, {
                        success: function (data) {
                            //Resource Variables
                            resourceVariableNodes.removeAll();
                            ko.utils.arrayPushAll(resourceVariableNodes(), data.ResourceVariables);
                            resourceVariableNodes.valueHasMutated();

                            _.each(resourceVariableNodes(), function (users) {
                                $("#" + id).append('<ol class="dd-list"> <li class="dd-item dd-item-list" id =' + users.UserName + '> <div class="dd-handle-list"><i class="fa fa-bars"></i></div><div class="dd-handle"><span style="cursor: move;z-index: 1000" title="Drag variable to create string" data-bind="drag: $root.dragged">' + users.UserName + '<input type="hidden" id="str" value="' + users.VariableString + '" /></span><div class="nested-links" data-bind="click:$root.addVariableToInputControl"></div></div></li></ol>');
                                ko.applyBindings(view.viewModel, $("#" + users.UserName)[0]);
                            });

                        },
                        error: function () {
                            toastr.error("Failed to load variables tree data.");
                        }
                    });

                }
                if (id == 4) {
                    dataservice.GetTreeListById({
                        id: id,
                    }, {
                        success: function (data) {
                            //Questions Variables
                            $("#4").children('ol').remove();
                            questionVariableNodes.removeAll();
                            ko.utils.arrayPushAll(questionVariableNodes(), data.QuestionVariables);
                            questionVariableNodes.valueHasMutated();
                            _.each(questionVariableNodes(), function (question) {
                                $("#" + id).append('<ol class="dd-list"> <li class="dd-item dd-item-list" id =' + question.Id + '> <div class="dd-handle-list"><i class="fa fa-bars"></i></div><div class="dd-handle"><span class="AddEditQuestion" id =' + question.Id + ' style="cursor: move;z-index: 1000" title="Drag variable to create string" data-bind="drag: $root.dragged,click: function() { $root.addVariableToInputControl(&quot;' + question.Id + "," + question.QuestionString + "," + question.Type + "," + question.DefaultAnswer + '&quot;)}">' + question.QuestionString + '<input type="hidden" id="str" value="' + question.VariableString + '" /></span><div class="nested-links" ></div></div></li></ol>');
                                ko.applyBindings(view.viewModel, $("#" + question.Id)[0]);
                            });
                            view.showAddEditQuestionMenu();
                        },
                        error: function () {
                            toastr.error("Failed to load variables tree data.");
                        }
                    });
                }
                if (id == 5) {
                    dataservice.GetTreeListById({
                        id: id,
                    }, {
                        success: function (data) {
                            //Matrix Variables
                            matrixVariableNodes.removeAll();
                            ko.utils.arrayPushAll(matrixVariableNodes(), data.MatricesVariables);
                            matrixVariableNodes.valueHasMutated();
                            _.each(matrixVariableNodes(), function (matrix) {
                                $("#" + id).append('<ol class="dd-list"> <li class="dd-item dd-item-list" id =' + matrix.MatrixId + 'mv' + '> <div class="dd-handle-list"><i class="fa fa-bars"></i></div><div class="dd-handle"><span style="cursor: move;z-index: 1000" title="Drag variable to create string" data-bind="drag: $root.dragged">' + matrix.Name + '<input type="hidden" id="str" value="' + matrix.VariableString + '" /></span><div class="nested-links" ></div></div></li></ol>');
                                ko.applyBindings(view.viewModel, $("#" + matrix.MatrixId + "mv")[0]);
                            });
                        },
                        error: function () {
                            toastr.error("Failed to load variables tree data.");
                        }
                    });
                }
                if (id == 6) {
                    dataservice.GetTreeListById({
                        id: id,
                    }, {
                        success: function (data) {
                            //Lookup Variables
                            lookupVariableNodes.removeAll();
                            ko.utils.arrayPushAll(lookupVariableNodes(), data.LookupVariables);
                            lookupVariableNodes.valueHasMutated();
                            _.each(lookupVariableNodes(), function (lookup) {
                                $("#" + id).append('<ol class="dd-list"> <li class="dd-item dd-item-list" id =' + lookup.MethodId + 'lum' + '> <div class="dd-handle-list"><i class="fa fa-bars"></i></div><div class="dd-handle"><span >' + lookup.Name + '</span><div class="nested-links"></div></div></li></ol>');
                                ko.applyBindings(view.viewModel, $("#" + lookup.MethodId + "lum")[0]);
                            });
                        },
                        error: function () {
                            toastr.error("Failed to load variables tree data.");
                        }
                    });
                }
            },
            getCostcenterByCatId = function (dataRecieved, event) {
                var id = $(event.target).closest('li')[0].id;
                if ($(event.target).closest('li').children('ol').length > 0) {
                    if ($(event.target).closest('li').children('ol').is(':hidden')) {
                        $(event.target).closest('li').children('ol').show();
                    } else {
                        $(event.target).closest('li').children('ol').hide();
                    }
                    return;
                }
                _.each(costcenterVariableNodes(), function (cc) {
                    if (cc.TypeId == id) {
                        selectedCostCenterType(cc);
                        _.each(cc.CostCentres, function (ccd) {
                            //selectedcc(ccd);
                            $("#" + id).append('<ol class="dd-list"> <li class="dd-item dd-item-list" id =' + ccd.CostCentreId + 'cc' + '> <div class="dd-handle-list" data-bind="click: $root.getCostcenterFixedVariables, css: { selectedRow: $data === $root.selectedcc}""><i class="fa fa-bars"></i></div><div class="dd-handle"><span >' + ccd.Name + '</span><div class="nested-links"></div></div></li></ol>');
                            ko.applyBindings(view.viewModel, $("#" + ccd.CostCentreId + "cc")[0]);
                        });
                    }
                });
            },
            getCostcenterFixedVariables = function (dataRecieved, event) {
                var id = $(event.target).closest('li')[0].id;
                if ($(event.target).closest('li').children('ol').length > 0) {
                    if ($(event.target).closest('li').children('ol').is(':hidden')) {
                        $(event.target).closest('li').children('ol').show();
                    } else {
                        $(event.target).closest('li').children('ol').hide();
                    }
                    return;
                }
                var ccid = id.substring(0, id.length - 2);
                _.each(selectedCostCenterType().CostCentres, function (ccv) {
                    if (ccv.CostCentreId == ccid) {
                        _.each(ccv.FixedVariables, function (cfv) {
                            $("#" + id).append('<ol class="dd-list"> <li class="dd-item dd-item-list" id =' + cfv.Id + '> <div class="dd-handle-list"><i class="fa fa-bars"></i></div><div class="dd-handle"><span style="cursor: move;z-index: 1000" data-bind="drag: $root.dragged">' + cfv.Name + '<input type="hidden" id="str" value="' + cfv.VariableString + '" /></span><div class="nested-links"></div></div></li></ol>');
                            ko.applyBindings(view.viewModel, $("#" + cfv.Id)[0]);
                        });
                    }
                });


            },
            getVariablesByType = function (dataRecieved, event) {
                var id = $(event.target).closest('li')[0].id;
                if ($(event.target).closest('li').children('ol').length > 0) {
                    if ($(event.target).closest('li').children('ol').is(':hidden')) {
                        $(event.target).closest('li').children('ol').show();
                    } else {
                        $(event.target).closest('li').children('ol').hide();
                    }
                    return;
                }
                _.each(variableVariableNodes(), function (cc) {
                    var sid = id.substring(0, id.length - 2);
                    if (cc.CategoryId == sid) {
                        selectedVariableType(cc);
                        _.each(cc.VariablesList, function (ccd) {
                            $("#" + id).append('<ol class="dd-list"> <li class="dd-item dd-item-list" id =' + ccd.VarId + 'vvd' + '> <div class="dd-handle-list"><i class="fa fa-bars"></i></div><div class="dd-handle"><span style="cursor: move;z-index: 1000" title="Drag variable to create string" data-bind="drag: $root.dragged">' + ccd.Name + '<input type="hidden" id="str" value="' + ccd.FixedVariables + '" /></span><div class="nested-links"></div></div></li></ol>');
                            ko.applyBindings(view.viewModel, $("#" + ccd.VarId + "vvd")[0]);
                        });
                    }
                });
            },
            saveQuestionVariable = function (oQuestion) {

                if (oQuestion.Id() > 0) {
                    saveEditedQuestionVariable(oQuestion);
                } else {
                    saveNewQuestionVariable(oQuestion);
                }

            }
            saveNewQuestionVariable = function (oQuestion) {
                dataservice.saveNewQuestionVariable(model.QuestionVariableServerMapper(oQuestion),
                {
                    success: function (data) {
                        questionVariableNodes.push(model.QuestionVariable(data));

                        toastr.success("Successfully Saved.");
                        view.hideCostCentreQuestionDialog();
                    },
                    error: function (response) {
                        toastr.error("Failed to Save Question" + response);
                    }
                });


            }
            saveEditedQuestionVariable = function (oQuestion) {
                dataservice.saveQuestionVariable(model.QuestionVariableServerMapper(oQuestion),
                {
                    success: function (data) {
                        questionVariableNodes.filter(function (item) { return item.Id() === oQuestion.Id() })[0].QuestionString(oQuestion.QuestionString());
                        questionVariableNodes.filter(function (item) { return item.Id() === oQuestion.Id() })[0].DefaultAnswer(oQuestion.DefaultAnswer());
                        questionVariableNodes.filter(function (item) { return item.Id() === oQuestion.Id() })[0].Type(oQuestion.Type());
                        toastr.success("Successfully Saved.");
                        view.hideCostCentreQuestionDialog();
                    },
                    error: function (response) {
                        toastr.error("Failed to Save Question" + response);
                    }
                });


            }
            // Returns the item being dragged source.$data.VariableString
            dragged = function (source, event) {
                if (event != undefined) {
                    return {
                        row: source.$parent,
                        widget: source.$data,
                        html: source.$data.VariableString().replace(/&quot;/g, '"') // event.currentTarget.children[0].value
                    };
                }
                return {};
            },
            dropped = function (source, target, event) {
                var vstring = source.html;
                if (event.target.disabled == false) {
                    event.target.value += vstring;
                }
            },
            selectVariableString = function (varstring, e) {
                selectedVariableString(e.currentTarget.id);
                var result = questionVariableNodes.filter(function (item) { return item.Id === 56 });
            },
            AddAnswerofQuestionVariable = function () {
                SelectedQuestionVariable().QuestionVariableMCQsAnswer.push(model.MCQsAnswer());



            },
            OnDeleteAnswerStringofQuestionVariable = function (oAnswer) {
                if (oAnswer.Id() > 0) {
                    confirmation.messageText("Do you want to Detele this Item?");
                    confirmation.afterProceed(function () {
                        dataservice.deleteQuestionVariable({
                            MCQsQuestionAnswerId: oAnswer.Id()
                        },
                        {
                            success: function (data) {
                                if (data) {
                                    SelectedQuestionVariable().QuestionVariableMCQsAnswer.remove(oAnswer);
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to Delete Answer" + response);
                            }
                        });
                    });
                    confirmation.afterCancel(function () {
                        //navigateToUrl(element);
                    });
                    confirmation.show();

                } else {
                    SelectedQuestionVariable().QuestionVariableMCQsAnswer.remove(oAnswer);
                }

            },
            addQuestionVariable = function () {
                SelectedQuestionVariable(model.QuestionVariable());
                view.showCostCentreQuestionDialog();
            }
            DeleteQuestionVariable = function (variable, event) {
                if (event != undefined) {
                    var Id = parseInt($('#' + event.currentTarget.parentElement.parentElement.id).data('invokedOn').closest('span').attr('id'));
                    confirmation.messageText("Do you want to Detele this Item?");
                    confirmation.afterProceed(function () {
                        dataservice.deleteQuestionVariable({
                            QuestionId: Id
                        },
                        {
                            success: function (data) {
                                if (data) {
                                    questionVariableNodes.remove(questionVariableNodes.filter(function (item) { return item.Id() === Id })[0]);
                                }
                                view.showAddEditQuestionMenu();


                            },
                            error: function (response) {
                                toastr.error("Failed to Delete Question" + response);
                            }
                        });
                    });
                    confirmation.afterCancel(function () {
                        //navigateToUrl(element);
                    });
                    confirmation.show();

                }

            },
            addVariableToInputControl = function (variable, event) {
                if (event != undefined) {
                    var Id = $('#' + event.currentTarget.parentElement.parentElement.id).data('invokedOn').closest('span').attr('id')

                    var questionData = questionVariableNodes.filter(function (item) { return item.Id === parseInt(Id) });
                    if (questionData.type == "2") {
                        dataservice.getCostCentreAnswerList({
                            QuestionId: questionData.Id,
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    SelectedQuestionVariable(model.QuestionVariableMapper(questionData, data));
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to Load . Error: " + response);
                            }

                        });
                    } else {
                        SelectedQuestionVariable(model.QuestionVariableMapper(questionData));
                    }
                } else if (variable != null && variable != undefined) {
                    questionData = variable.split(",");
                    if (questionData[2] == "2") {
                        dataservice.getCostCentreAnswerList({
                            QuestionId: questionData[0],
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    SelectedQuestionVariable(model.QuestionVariableMapper(questionData, data));
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to Load . Error: " + response);
                            }

                        });
                    } else {
                        SelectedQuestionVariable(model.QuestionVariableMapper(questionData));
                    }


                } else {
                    SelectedQuestionVariable(model.QuestionVariableMapper());
                }
                view.showCostCentreQuestionDialog();
            },
            OnEditQuestionVariable = function (oQuestion) {
                if (oQuestion.Id == undefined || oQuestion.Id == null) {
                    var Id = parseInt($('#' + event.currentTarget.parentElement.parentElement.id).data('invokedOn').closest('span').attr('id'));

                    var oQuestionData = questionVariableNodes.filter(function (item) { return item.Id() === Id })[0];
                    if (oQuestionData.Type() == "2") {
                        dataservice.getCostCentreAnswerList({
                            QuestionId: oQuestionData.Id(),
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    SelectedQuestionVariable(model.QuestionVariableClientMapper(oQuestionData, data));

                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to Load . Error: " + response);
                            }

                        });
                    } else {
                        SelectedQuestionVariable(model.QuestionVariableClientMapper(oQuestionData));
                    }
                } else if (oQuestion != null && oQuestion != undefined) {

                    if (oQuestion.Type() == "2") {
                        dataservice.getCostCentreAnswerList({
                            QuestionId: oQuestion.Id(),
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    SelectedQuestionVariable(model.QuestionVariableClientMapper(oQuestion, data));
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to Load . Error: " + response);
                            }

                        });
                    } else {
                        SelectedQuestionVariable(model.QuestionVariableClientMapper(oQuestion));
                    }


                } else {
                    SelectedQuestionVariable(model.QuestionVariableMapper());
                }
                view.showCostCentreQuestionDialog();
            },
            // #region Busy Indicators
            isLoadingCostCenter = ko.observable(false),

            // #endregion Busy Indicators
            sortOn = ko.observable(1),
            //Sort In Ascending
            sortIsAsc = ko.observable(true),
            //Pager
            pager = ko.observable(),
            //Search Filter
            searchFilter = ko.observable(),
            isEditorVisible = ko.observable(),
            selectedCostCenter = ko.observable(),
            selectedInstruction = ko.observable(),
            selectedChoice = ko.observable(),
            templateToUse = function (ocostCenter) {
                return (ocostCenter === selectedCostCenter() ? 'editCostCenterTemplate' : 'itemCostCenterTemplate');
            },
            makeEditable = ko.observable(false),
            //Delete Cost Center
            deleteCostCenter = function (oCostCenter) {
                dataservice.deleteCostCenter({
                    CostCentreId: oCostCenter.CostCentreId(),
                }, {
                    success: function (data) {
                        if (data != null) {
                            costCentersList.remove(oCostCenter);
                            toastr.success(" Deleted Successfully !");
                        }
                    },
                    error: function (response) {
                        toastr.error("Failed to Delete . Error: " + response);
                    }
                });
            },
            costcenterImageFilesLoadedCallback = function (file, data) {
                selectedCostCenter().costcentreImageFileBinary(data);
                selectedCostCenter().costcentreImageName(file.name);
            },
            onDeleteCostCenter = function (oCostCenter) {
                if (!oCostCenter.CostCentreId()) {
                    costCentersList.remove(oCostCenter);
                    return;
                }
                // Ask for confirmation
                confirmation.afterProceed(function () {
                    deleteCostCenter(oCostCenter);
                });
                confirmation.show();
            },
            getCostCenters = function () {
                isLoadingCostCenter(true);

                dataservice.getCostCentersList({
                    CostCenterFilterText: searchFilter(),
                    PageSize: pager().pageSize(),
                    PageNo: pager().currentPage(),
                    SortBy: sortOn(),
                    IsAsc: sortIsAsc(),
                    CostCenterType: CostCenterType
                }, {
                    success: function (data) {
                        costCentersList.removeAll();
                        if (data != null) {
                            pager().totalCount(data.RowCount);
                            _.each(data.CostCenters, function (item) {
                                var module = model.costCenterListView.Create(item);
                                costCentersList.push(module);
                            });
                        }
                        isLoadingCostCenter(false);
                    },
                    error: function (response) {
                        isLoadingCostCenter(false);
                        toastr.error("Error: Failed to Load Cost Centers Data." + response);
                    }
                });
            },
            //Do Before Save
            doBeforeSave = function () {
                var flag = true;
                if (!selectedCostCenter().isValid()) {
                    selectedCostCenter().errors.showAllMessages();
                    flag = false;
                }
                return flag;
            },
            AddnewChildItem = function (Item) {
                if (Item.Id == 4) {

                }

            }
            //Save Cost Center
            saveCostCenter = function (callback) {
                errorList.removeAll();
                if (doBeforeSave()) {

                    if (selectedCostCenter().costCentreId() > 0) {
                        saveEdittedCostCenter(callback);
                    } else {
                        saveNewCostCenter(callback);
                    }
                }
            },
            //Save NEW Cost Center
            saveNewCostCenter = function (callback) {
                dataservice.saveNewCostCenter(model.costCenterServerMapper(selectedCostCenter()), {
                    success: function (data) {
                        selectedCostCenter().costCentreId(data.CostCentreId);
                        costCentersList.splice(0, 0, selectedCostCenter());
                        selectedCostCenter().reset();
                        getCostCenters();
                        toastr.success("Successfully saved.");
                    },
                    error: function (response) {
                        toastr.error("Failed to save." + response);
                    }
                });
            },
            //Save EDIT Cost Center
            saveEdittedCostCenter = function (callback) {
                dataservice.saveCostCenter(model.costCenterServerMapper(selectedCostCenter()), {
                    success: function (data) {
                        if (callback && typeof callback === "function") {
                            callback();
                        }
                        selectedCostCenter().reset();
                        getCostCenters();
                        toastr.success("Successfully saved.");
                    },
                    error: function (exceptionMessage, exceptionType) {

                        if (exceptionType === ist.exceptionType.MPCGeneralException) {

                            toastr.error(exceptionMessage);

                        } else {

                            toastr.error("Failed to save.");

                        }

                    }
                });
            },
            createCostCenter = function () {
                errorList.removeAll();
                var cc = new model.CostCenter();
                setDataForNewCostCenter(cc);
                selectedCostCenter(cc);
                getCostCentersBaseData();
                // getVariablesTree();
                showCostCenterDetail();
                sharedNavigationVM.initialize(selectedCostCenter, function (saveCallback) { saveCostCenter(saveCallback); });
            },
            createDeliveryCostCenter = function () {
                errorList.removeAll();
                var cc = new model.CostCenter();
                cc.setupCost('0');
                cc.minimumCost('0');
                cc.type('11');
                selectedCostCenter(cc);
                getCostCentersBaseData();
                // getVariablesTree();
                showCostCenterDetail();
                sharedNavigationVM.initialize(selectedCostCenter, function (saveCallback) { saveCostCenter(saveCallback); });
            },
            setDataForNewCostCenter = function (newcostcenter) {
                newcostcenter.costPerUnitQuantity('0');
                newcostcenter.unitQuantity('0');
                newcostcenter.name('New Cost Center');
                newcostcenter.pricePerUnitQuantity('0');
                newcostcenter.setupCost('0');
                newcostcenter.setupSpoilage('0');
                newcostcenter.setupTime('0');
                newcostcenter.minimumCost('0');
                newcostcenter.timePerUnitQuantity('0');
                newcostcenter.runningSpoilage('0');
                newcostcenter.priority('0');
                newcostcenter.isDirectCost('1');
                newcostcenter.isPrintOnJobCard('1');
                newcostcenter.isDisabled('0');
                newcostcenter.isScheduleable('1');
                newcostcenter.sequence('1');
                // newcostcenter.creationDate(moment().toDate().format(ist.utcFormat) + 'Z');
                newcostcenter.costDefaultValue('0');
                newcostcenter.priceDefaultValue('0');
                newcostcenter.quantitySourceType('1');
                newcostcenter.calculationMethodType('2');
            },
            createWorkInstruction = function () {
                var wi = new model.NewCostCenterInstruction();
                selectedInstruction(wi);
                selectedCostCenter().costCenterInstructions.splice(0, 0, wi);
            },
            deleteWorkInstruction = function (instruction) {
                instruction.workInstructionChoices.removeAll();
                selectedCostCenter().costCenterInstructions.remove(instruction);
            },
            createWorkInstructionChoice = function () {
                var wic = new model.NewInstructionChoice();
                selectedChoice(wic);
                selectedInstruction().workInstructionChoices.splice(0, 0, wic);
            },
            deleteWorkInstructionChoice = function (choice) {
                selectedInstruction().workInstructionChoices.remove(choice);
            },
            //On Edit Click Of Cost Center
            onEditItem = function (oCostCenter) {
                errorList.removeAll();
                getCostCentersBaseData(oCostCenter);
                //getVariablesTree();

            },
            getCostCenterById = function (oCostCenter) {
                dataservice.getCostCentreById({
                    id: oCostCenter.costCenterId(),
                }, {
                    success: function (data) {
                        if (data != null) {
                            selectedCostCenter(model.costCenterClientMapper(data));
                            selectedCostCenter().reset();
                            showCostCenterDetail();
                        }
                    },
                    error: function (response) {
                        toastr.error("Failed to load Detail . Error: ");
                    }
                });
            }
            openEditDialog = function () {
                view.showCostCenterDialog();

            },
            closeEditDialog = function () {
                if (selectedCostCenter() != undefined) {
                    if (selectedCostCenter().costCenterId() > 0) {
                        isEditorVisible(false);
                        view.hideCostCenterDialog();
                    } else {
                        isEditorVisible(false);
                        view.hideCostCenterDialog();
                        costCentersList.remove(selectedCostCenter());
                    }
                    editorViewModel.revertItem();
                }
            },
            // close CostCenter Editor
            closeCostCenterDetail = function () {
                isEditorVisible(false);
            },
            // Show CostCenter Editor
            showCostCenterDetail = function () {
                isEditorVisible(true);

            },

            //Get variables Tree
            getVariablesTree = function (nodeid) {
                dataservice.getVariablesTree({
                    id: nodeid,
                }, {
                    success: function (data) {
                        //CostCenterVariables
                        costcenterVariableNodes.removeAll();
                        ko.utils.arrayPushAll(costcenterVariableNodes(), data.CostCenterVariables);
                        costcenterVariableNodes.valueHasMutated();
                        //Variable Variables
                        variableVariableNodes.removeAll();
                        ko.utils.arrayPushAll(variableVariableNodes(), data.VariableVariables);
                        variableVariableNodes.valueHasMutated();
                        //Resource Variables
                        resourceVariableNodes.removeAll();
                        ko.utils.arrayPushAll(resourceVariableNodes(), data.ResourceVariables);
                        resourceVariableNodes.valueHasMutated();
                        //Questions Variables
                        questionVariableNodes.removeAll();
                        ko.utils.arrayPushAll(questionVariableNodes(), data.QuestionVariables);
                        questionVariableNodes.valueHasMutated();
                        //Matrix Variables
                        matrixVariableNodes.removeAll();
                        ko.utils.arrayPushAll(matrixVariableNodes(), data.MatricesVariables);
                        matrixVariableNodes.valueHasMutated();
                        //Lookup Variables
                        lookupVariableNodes.removeAll();
                        ko.utils.arrayPushAll(lookupVariableNodes(), data.LookupVariables);
                        lookupVariableNodes.valueHasMutated();
                    },
                    error: function () {
                        toastr.error("Failed to load variables tree data.");
                    }
                });
            };
            // Get Base
            getCostCentersBaseData = function (oCostCenter) {
                dataservice.getBaseData({
                    success: function (data) {
                        //costCenter Calculation Types
                        costCenterTypes.removeAll();
                        ko.utils.arrayPushAll(costCenterTypes(), data.CalculationTypes);
                        costCenterTypes.valueHasMutated();
                        //Cost Center Categories
                        costCenterCategories.removeAll();
                        ko.utils.arrayPushAll(costCenterCategories(), data.CostCenterCategories);
                        costCenterCategories.valueHasMutated();
                        //Nominal Codes
                        nominalCodes.removeAll();
                        ko.utils.arrayPushAll(nominalCodes(), data.NominalCodes);
                        nominalCodes.valueHasMutated();
                        //Markups
                        markups.removeAll();
                        ko.utils.arrayPushAll(markups(), data.Markups);
                        markups.valueHasMutated();
                        //Variables
                        costCenterVariables.removeAll();
                        ko.utils.arrayPushAll(costCenterVariables(), data.CostCentreVariables);
                        costCenterVariables.valueHasMutated();
                        //
                        //Cost Center Resources
                        costCenterResources.removeAll();
                        ko.utils.arrayPushAll(costCenterResources(), data.CostCenterResources);
                        costCenterResources.valueHasMutated();
                        //Delivery Carriers
                        deliveryCarriers.removeAll();
                        ko.utils.arrayPushAll(deliveryCarriers(), data.DeliveryCarriers);
                        deliveryCarriers.valueHasMutated();
                        if (oCostCenter != undefined) {
                            getCostCenterById(oCostCenter);
                        }
                        CurrencySymbol(data.CurrencySymbol);

                    },
                    error: function () {
                        toastr.error("Failed to base data.");
                    }
                });
            },
            updateSelectedResources = function () {
                _.each(selectedCostCenter().costCenterResource(), function (resource) {
                    var selectedResource;
                    selectedResource = _.find(costCenterResources(), function (resourceItem) {
                        return resourceItem.costCentreId() === resource.costCentreId();
                    });
                    selectedCostCenter.isSelected(true);
                });
            },
            iconClick = function (iconid, event) {
                var icoId = event.currentTarget.id;
                var icoVal;
                if (icoId == 'icoAdd') { icoVal = "+"; }
                else if (icoId == 'icoSub') { icoVal = "-"; }
                else if (icoId == 'icoMul') { icoVal = "*"; }
                else if (icoId == 'icoDiv') { icoVal = "/"; }
                else if (icoId == 'icoIf') { icoVal = "If ('condition') Then 'Statements' End IF"; }
                else if (icoId == 'icoIfEl') { icoVal = "If ('condition') Then 'Statements' Else 'Statements' End IF"; }
                else if (icoId == 'icoMod') { icoVal = "Mod"; }
                else if (icoId == 'icoPer') { icoVal = "%"; }
                else if (icoId == 'icoBrace1') { icoVal = "("; }
                else if (icoId == 'icoBrace2') { icoVal = ")"; }

                if (selectedVariableString() === 'txtEstimatePlantCost') {
                    document.getElementById('txtEstimatePlantCost').value += icoVal;
                    selectedCostCenter().strCostPlantUnParsed(selectedCostCenter().strCostPlantUnParsed() + icoVal);
                }
                if (selectedVariableString() === 'txtEstimatePlantPrice') {
                    document.getElementById('txtEstimatePlantPrice').value += icoVal;
                    //selectedCostCenter().strPricePlantUnParsed(selectedCostCenter().strPricePlantUnParsed() + icoVal);
                }
                if (selectedVariableString() === 'txtPlantActualCost') {
                    selectedCostCenter().strActualCostPlantUnParsed(selectedCostCenter().strActualCostPlantUnParsed() + icoVal);
                }
                if (selectedVariableString() === 'txtEstimateStockCost') {
                    selectedCostCenter().strCostMaterialUnParsed(selectedCostCenter().strCostMaterialUnParsed() + icoVal);
                }
                if (selectedVariableString() === 'txtQuoteStockCost') {
                    selectedCostCenter().strPriceMaterialUnParsed(selectedCostCenter().strPriceMaterialUnParsed() + icoVal);
                }
                if (selectedVariableString() === 'txtStockActualCost') {
                    selectedCostCenter().strActualCostMaterialUnParsed(selectedCostCenter().strActualCostMaterialUnParsed() + icoVal);
                }
                if (selectedVariableString() === 'txtEstimateLabourCost') {
                    selectedCostCenter().strCostPlantUnParsed(selectedCostCenter().strCostPlantUnParsed() + icoVal);
                }
                if (selectedVariableString() === 'txtQuotedLabourCost') {
                    selectedCostCenter().strPriceLabourUnParsed(selectedCostCenter().strPriceLabourUnParsed() + icoVal);
                }
                if (selectedVariableString() === 'txtLabourActualCost') {
                    selectedCostCenter().strActualCostLabourUnParsed(selectedCostCenter().strActualCostLabourUnParsed() + icoVal);
                }
                if (selectedVariableString() === 'txtEstimateTimeCost') {
                    selectedCostCenter().strTimeUnParsed(selectedCostCenter().strTimeUnParsed() + icoVal);
                }
            },
            // #region Observables
            // Initialize the view model
            initialize = function (specifiedView) {
                view = specifiedView;
                ko.applyBindings(view.viewModel, view.bindingRoot);
                pager(pagination.Pagination({ PageSize: 10 }, costCentersList, getCostCenters));
                getCostCenters();

                // getCostCentersBaseData();
            };

            return {
                // Observables
                costCentersList: costCentersList,
                selectedCostCenter: selectedCostCenter,
                isLoadingCostCenter: isLoadingCostCenter,
                deleteCostCenter: deleteCostCenter,
                onDeleteCostCenter: onDeleteCostCenter,
                sortOn: sortOn,
                sortIsAsc: sortIsAsc,
                pager: pager,
                templateToUse: templateToUse,
                makeEditable: makeEditable,
                getCostCenters: getCostCenters,
                doBeforeSave: doBeforeSave,
                saveCostCenter: saveCostCenter,
                saveNewCostCenter: saveNewCostCenter,
                saveEdittedCostCenter: saveEdittedCostCenter,
                openEditDialog: openEditDialog,
                closeEditDialog: closeEditDialog,
                searchFilter: searchFilter,
                onEditItem: onEditItem,
                initialize: initialize,
                isEditorVisible: isEditorVisible,
                closeCostCenterDetail: closeCostCenterDetail,
                showCostCenterDetail: showCostCenterDetail,
                getCostCentersBaseData: getCostCentersBaseData,
                costCenterTypes: costCenterTypes,
                costCenterCategories: costCenterCategories,
                nominalCodes: nominalCodes,
                markups: markups,
                costCenterResources: costCenterResources,
                costCenterVariables: costCenterVariables,
                deliveryCarriers: deliveryCarriers,
                variablesTreePrent: variablesTreePrent,
                createCostCenter: createCostCenter,
                setDataForNewCostCenter: setDataForNewCostCenter,
                fedexServiceTypes: fedexServiceTypes,
                costcenterImageFilesLoadedCallback: costcenterImageFilesLoadedCallback,
                createWorkInstruction: createWorkInstruction,
                createWorkInstructionChoice: createWorkInstructionChoice,
                deleteWorkInstruction: deleteWorkInstruction,
                deleteWorkInstructionChoice: deleteWorkInstructionChoice,
                //getVariablesTree: getVariablesTree,
                getVariableTreeChildListItems: getVariableTreeChildListItems,
                getCostcenterByCatId: getCostcenterByCatId,
                getVariablesByType: getVariablesByType,
                dragged: dragged,
                addVariableToInputControl: addVariableToInputControl,
                getCostcenterFixedVariables: getCostcenterFixedVariables,
                dropped: dropped,
                selectVariableString: selectVariableString,
                selectedVariableString: selectedVariableString,
                iconClick: iconClick,
                questionVariableNodes: questionVariableNodes,
                createDeliveryCostCenter: createDeliveryCostCenter,
                SelectedQuestionVariable: SelectedQuestionVariable,
                AddAnswerofQuestionVariable: AddAnswerofQuestionVariable,
                AddnewChildItem: AddnewChildItem,
                QuestionVariableType: QuestionVariableType,
                saveQuestionVariable: saveQuestionVariable,
                CurrencySymbol: CurrencySymbol,
                addQuestionVariable: addQuestionVariable,
                DeleteQuestionVariable: DeleteQuestionVariable,
                getVariableTreeChildItems: getVariableTreeChildItems,
                showQuestionVariableChildList: showQuestionVariableChildList,
                OnEditQuestionVariable: OnEditQuestionVariable,
                OnDeleteAnswerStringofQuestionVariable: OnDeleteAnswerStringofQuestionVariable,
                saveNewQuestionVariable: saveNewQuestionVariable,
                saveEditedQuestionVariable: saveEditedQuestionVariable
            };
        })()
    };
    return ist.costcenter.viewModel;
});
