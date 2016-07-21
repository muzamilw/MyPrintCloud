/*
Module with the view model for the My Organization.
*/
define("costcenter/costcenter.viewModel",
["jquery", "amplify", "ko", "costcenter/costcenter.dataservice", "costcenter/costcenter.model", "common/confirmation.viewModel", "common/pagination", "common/sharedNavigation.viewModel", "common/stockItem.viewModel"],
function ($, amplify, ko, dataservice, model, confirmation, pagination, sharedNavigationVM, stockDialog) {
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
            SelectedMatrixVariable = ko.observable(),
            costcenterVariableNodes = ko.observableArray([]),
            variableVariableNodes = ko.observableArray([]),
            variableDropdownList = ko.observableArray([]),
            resourceVariableNodes = ko.observableArray([]),
            questionVariableNodes = ko.observableArray([]),
            matrixVariableNodes = ko.observableArray([]),
            lookupVariableNodes = ko.observableArray([]),
                zoneVariableNodes = ko.observableArray([]),
            selectedCostCenterType = ko.observable(),
            selectedVariableType = ko.observable(),
            SelectedStockVariable = ko.observable(),
            isClickChargeZonesEditorVisible = ko.observable(),
            selectedClickChargeZones = ko.observable(),
            selectedcc = ko.observable(),
            fixedvarIndex = ko.observable(1),
            selectedVariableString = ko.observable(),
            CurrencySymbol = ko.observable(),
            showQuestionVariableChildList = ko.observable(0),
            showZoneVariableChildList = ko.observable(0),
            showMatricesVariableChildList = ko.observable(0),
            // Cost Center Categories
            costCenterCategories = ko.observableArray([]),
            workInstructions = ko.observableArray([]),
            //{ Id: 1, Text: 'Cost Centers' },
            QuestionVariableType = ko.observableArray([
            { Id: 1, Text: 'General' },
            { Id: 2, Text: 'Yes/No' },
            { Id: 3, Text: 'Multiple Options' }            
            ]),
            RowscolCountList = ko.observableArray([
            //{ Id: 1, Text: '1' },
            { Id: 2, Text: '2' },
            { Id: 3, Text: '3' },
            { Id: 4, Text: '4' },
            { Id: 5, Text: '5' },
            { Id: 6, Text: '6' },
            { Id: 7, Text: '7' },
            { Id: 8, Text: '8' },
            { Id: 9, Text: '9' }
            ]),

            variablesTreePrent = ko.observableArray([
            { Id: 2, Text: 'Variables' },
            //{ Id: 4, Text: 'Questions' },
            //{ Id: 5, Text: 'Matrices' },
            //{ Id: 6, Text: 'Lookup' },
            //{ Id: 7, Text: 'Stock Items' }
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
            isZoneVariableType = ko.observable(),
            selectedZoneId = ko.observable(),
            selectedZoneName = ko.observable(),
            selectedZoneVariableId = ko.observable(),
            selectedZonePromptQuestion = ko.observable(""),
            getQuestionsVariableTreeChildItems = function (Selecteddata) {
                if (questionVariableNodes().length > 0) {
                    if (showQuestionVariableChildList() == 1) {
                        showQuestionVariableChildList(0);
                        $("#idQuestionsVariable").removeClass("fa-chevron-circle-down");
                        $("#idQuestionsVariable").addClass("fa-chevron-circle-right");

                    } else {
                        showQuestionVariableChildList(1);
                        $("#idQuestionsVariable").addClass("fa-chevron-circle-down");
                        $("#idQuestionsVariable").removeClass("fa-chevron-circle-right");

                    }

                } else {
                    dataservice.GetTreeListById({
                        id: 4,
                    }, {
                        success: function (data) {
                            questionVariableNodes.removeAll();
                            _.each(data.QuestionVariables, function (item) {
                                var ques = model.QuestionVariable(item);
                                questionVariableNodes.push(ques);
                            });
                            showQuestionVariableChildList(1);
                            $("#idQuestionsVariable").addClass("fa-chevron-circle-down");
                            $("#idQuestionsVariable").removeClass("fa-chevron-circle-right");
                            view.showAddEditQuestionMenu();

                        },
                        error: function () {
                            toastr.error("Failed to load variables tree data.");
                        }
                    });
                }


            },

            getMatricesVariableTreeChildItems = function (Selecteddata) {
                if (matrixVariableNodes().length > 0) {
                    if (showMatricesVariableChildList() == 1) {

                        $("#idMatricesVariable").removeClass("fa-chevron-circle-down");
                        $("#idMatricesVariable").addClass("fa-chevron-circle-right");
                        showMatricesVariableChildList(0);
                    } else {
                        showMatricesVariableChildList(1);
                        $("#idMatricesVariable").addClass("fa-chevron-circle-down");
                        $("#idMatricesVariable").removeClass("fa-chevron-circle-right");
                    }

                } else {

                    dataservice.GetTreeListById({
                        id: 5,
                    }, {
                        success: function (data) {
                            matrixVariableNodes.removeAll();
                            _.each(data.MatricesVariables, function (item) {
                                var ques = model.matrixVariable(item);
                                matrixVariableNodes.push(ques);
                            });
                            showMatricesVariableChildList(1);
                            $("#idMatricesVariable").addClass("fa-chevron-circle-down");
                            $("#idMatricesVariable").removeClass("fa-chevron-circle-right");
                            view.showAddEditMatrixMenu();

                        },
                        error: function () {
                            toastr.error("Failed to load variables tree data.");
                        }
                    });
                }


            },
             onDeletePermanent = function () {
                 confirmation.messageText("WARNING - This item will be removed from the system and you won’t be able to recover.  There is no undo");
                 confirmation.afterProceed(function () {
                     deleteCostCentre(selectedCostCenter().costCentreId());
                 });
                 confirmation.show();
             },
                
               // Delete Company Permanently
                deleteCostCentre = function (id) {
                    dataservice.deleteCostCentre({ CostCentreId: id }, {
                        success: function () {
                            toastr.success("Cost Centre deleted successfully!");

                            closeCostCenterDetail(false);
                            //isEditorVisible(false);
                            if (selectedCostCenter()) {
                                var costCentre = getCostCentreById(selectedCostCenter().costCentreId());
                                if (costCentre) {
                                    costCentersList.remove(costCentre);
                                }
                            }
                           
                        },
                        error: function (response) {
                            toastr.error("Cost Centre is in use and can not delete.");
                        }
                    });
                };

            // Get Company By Id
            getCostCentreById = function (id) {
                return costCentersList.find(function (costcentre) {
                    return costcentre.costCenterId() === id;
                });
            },


            OnEditMatrixVariable = function (oMatrix) {
                if (oMatrix.MatrixId == undefined || oMatrix.MatrixId == null) {
                    var Id = parseInt($('#' + event.currentTarget.parentElement.parentElement.id).data('invokedOn').closest('span').attr('id'));
                    oMatrix = matrixVariableNodes.filter(function (item) { return item.MatrixId() === Id })[0];
                }
                if (oMatrix != null && oMatrix != undefined) {
                    dataservice.getCostCentreAnswerList({
                        MatrixId: oMatrix.MatrixId(),
                    }, {
                        success: function (data) {

                            SelectedMatrixVariable(model.MatrixVariableClientMapper(oMatrix, data));
                            SelectedMatrixVariable().reset();


                        },
                        error: function (response) {
                            toastr.error("Failed to Load . Error: " + response);
                        }

                    });
                }
                view.showCostCentreMatrixDialog();
            },
            addMatrixVariable = function () {

                SelectedMatrixVariable(model.MatrixVariableClientMapper());
                view.showCostCentreMatrixDialog();
                SelectedMatrixVariable().reset();
            },
            DeleteMatrixVariable = function (variable, event) {
                if (event != undefined) {
                    var Id = parseInt($('#' + event.currentTarget.parentElement.parentElement.id).data('invokedOn').closest('span').attr('id'));
                    confirmation.messageText("WARNING - This item will be removed from the system and you won’t be able to recover.  There is no undo");
                    confirmation.afterProceed(function () {
                        dataservice.DeleteMatrixVariable({
                            MatrixId: Id
                        },
                        {
                            success: function (data) {
                                if (data) {
                                    matrixVariableNodes.remove(matrixVariableNodes.filter(function (item) { return item.MatrixId() === Id })[0]);
                                }
                                //view.showAddEditQuestionMenu();


                            },
                            error: function (response) {
                                toastr.error("Failed to Delete Matrix" + response);
                            }
                        });
                    });
                    confirmation.afterCancel(function () {
                        //navigateToUrl(element);
                    });
                    confirmation.show();

                }

            },



            getvariableListItem = function () {
                dataservice.getCostCentreAnswerList({
                    VariableId: 2,
                }, {
                    success: function (data) {
                        if (data != null) {
                            variableDropdownList.removeAll();
                            ko.utils.arrayPushAll(variableDropdownList(), data);
                            variableDropdownList.valueHasMutated();
                        }
                    },
                    error: function (response) {
                        toastr.error("Failed to Load System Variables . Error: " + response);
                    }

                });


            }
            getVariableTreeChildListItems = function (dataRecieved, event) {
                var id = $(event.target).closest('li')[0].id;
                if ($(event.target).closest('li').children('ol').children('li').length > 0) {
                    if ($(event.target).closest('li').children('ol').is(':hidden')) {
                        $(event.target).closest('li').children('ol').show();
                        $("#idVariablesByType").addClass("fa-chevron-circle-down");
                        $("#idVariablesByType").removeClass("fa-chevron-circle-right");
                    } else {
                        $(event.target).closest('li').children('ol').hide();
                        $("#idVariablesByType").removeClass("fa-chevron-circle-down");
                        $("#idVariablesByType").addClass("fa-chevron-circle-right");

                    }
                    return;

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
                                $("#" + id).append('<ol class="dd-list"> <li class="dd-item dd-item-list" id =' + variable.CategoryId + 'vv' + '> <div class="dd-handle-list" data-bind="click: $root.getVariablesByType"><i id="idVariableschildByType"  class="fa fa-chevron-circle-right drop-icon"></i></div><div class="dd-handle"><span style="cursor: not-allowed;">' + variable.Name + '</span><div class="nested-links"></div></div></li></ol>');
                                ko.applyBindings(view.viewModel, $("#" + variable.CategoryId + "vv")[0]);
                            });
                            $("#idVariablesByType").addClass("fa-chevron-circle-down");
                            $("#idVariablesByType").removeClass("fa-chevron-circle-right");
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
                                $("#" + id).append('<ol class="dd-list"> <li class="dd-item dd-item-list" id =' + users.UserName + '> <div class="dd-handle-list"><i class="fa fa-minus-square"></i></div><div class="dd-handle"><span style="cursor: move;z-index: 1000" title="Drag variable to create string" data-bind="drag: $root.dragged">' + users.UserName + '<input type="hidden" id="str" value="' + users.VariableString + '" /></span><div class="nested-links" data-bind="click:$root.addVariableToInputControl"></div></div></li></ol>');
                                ko.applyBindings(view.viewModel, $("#" + users.UserName)[0]);
                            });

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
                                $("#" + id).append('<ol class="dd-list"> <li class="dd-item dd-item-list" id =' + lookup.MethodId + 'lum' + '> <div class="dd-handle-list"><i class="fa fa-minus-square"></i></div><div class="dd-handle"><span >' + lookup.Name + '</span><div class="nested-links"></div></div></li></ol>');
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
                            $("#" + id).append('<ol class="dd-list"> <li class="dd-item dd-item-list" id =' + ccd.CostCentreId + 'cc' + '> <div class="dd-handle-list" data-bind="click: $root.getCostcenterFixedVariables, css: { selectedRow: $data === $root.selectedcc}""><i class="fa fa-minus-square"></i></div><div class="dd-handle"><span >' + ccd.Name + '</span><div class="nested-links"></div></div></li></ol>');
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
                            $("#" + id).append('<ol class="dd-list"> <li class="dd-item dd-item-list" id =' + cfv.Id + '> <div class="dd-handle-list"><i class="fa fa-minus-square"></i></div><div class="dd-handle"><span style="cursor: move;z-index: 1000" data-bind="drag: $root.dragged">' + cfv.Name + '<input type="hidden" id="str" value="' + cfv.VariableString + '" /></span><div class="nested-links"></div></div></li></ol>');
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

                        $("#idVariableschildByType").addClass("fa-chevron-circle-down");
                        $("#idVariableschildByType").removeClass("fa-chevron-circle-right");
                    } else {
                        $(event.target).closest('li').children('ol').hide();
                        $("#idVariableschildByType").removeClass("fa-chevron-circle-down");
                        $("#idVariableschildByType").addClass("fa-chevron-circle-right");
                    }
                    return;
                }
                $("#idVariableschildByType").addClass("fa-chevron-circle-down");
                $("#idVariableschildByType").removeClass("fa-chevron-circle-right");
                _.each(variableVariableNodes(), function (cc) {
                    var sid = id.substring(0, id.length - 2);
                    if (cc.CategoryId == sid) {
                        selectedVariableType(cc);
                        _.each(cc.VariablesList, function (ccd) {
                            $("#" + id).append('<ol class="dd-list"> <li class="dd-item dd-item-list" id =' + ccd.VarId + 'vvd' + '> <div class="dd-handle-list"><i class="fa fa-minus-square"></i></div><div class="dd-handle"><span style="cursor: move;z-index: 1000" title="Drag variable to create string" data-bind="drag: $root.dragged">' + ccd.Name + '<input type="hidden" id="str" value="' + ccd.FixedVariables + '" /></span><div class="nested-links"></div></div></li></ol>');
                            ko.applyBindings(view.viewModel, $("#" + ccd.VarId + "vvd")[0]);
                        });
                    }
                });
            },
            saveMatrixVariable = function (oMatrix) {
                dataservice.saveVariable(model.MatrixVariableServerMapper(oMatrix),
               {
                   success: function (data) {
                       if (oMatrix.MatrixId() > 0) {
                           matrixVariableNodes.filter(function (item) { return item.MatrixId() === oMatrix.MatrixId() })[0].RowsCount(oMatrix.RowsCount());
                           matrixVariableNodes.filter(function (item) { return item.MatrixId() === oMatrix.MatrixId() })[0].ColumnsCount(oMatrix.ColumnsCount());
                           matrixVariableNodes.filter(function (item) { return item.MatrixId() === oMatrix.MatrixId() })[0].Name(oMatrix.Name());
                           matrixVariableNodes.filter(function (item) { return item.MatrixId() === oMatrix.MatrixId() })[0].Description(oMatrix.Description());

                       } else {
                           matrixVariableNodes.push(model.matrixVariable(data));
                       }


                       toastr.success("successfully saved.");
                       view.hideCostCentreMatrixDialog();
                       SelectedMatrixVariable().reset();
                       // view.hidecostcentrequestiondialog();
                   },
                   error: function (response) {
                       toastr.error("failed to save Matrix" + response);
                   }
               });


            }
            UpdateMartix = function (oMatrix) {
                $("#WarningRowColUpdate").html("");
                if (SelectedMatrixVariable().MatrixDetailVariableList().length > 0) {
                    var PreRow = SelectedMatrixVariable().MatrixDetailVariableList().length;
                    var PreCol = SelectedMatrixVariable().MatrixDetailVariableList()[0].length;
                    var colDiff = SelectedMatrixVariable().ColumnsCount() - SelectedMatrixVariable().MatrixDetailVariableList()[0]().length;
                    var RowDiff = SelectedMatrixVariable().RowsCount() - SelectedMatrixVariable().MatrixDetailVariableList().length;
                    if (RowDiff > 0) {
                        for (var c = 0; c < RowDiff; c++) {
                            var rowsTem = ko.observableArray([]);
                            for (var j = 0; j < SelectedMatrixVariable().ColumnsCount() ; j++) {
                                var row = model.MatrixDetailClientMapper();
                                rowsTem.push(row);
                            }
                            SelectedMatrixVariable().MatrixDetailVariableList.push(rowsTem);
                        }
                    } else if (RowDiff < 0) {
                        for (var d = RowDiff; d < 0; d++) {
                            SelectedMatrixVariable().MatrixDetailVariableList.remove(SelectedMatrixVariable().MatrixDetailVariableList()[SelectedMatrixVariable().MatrixDetailVariableList().length - 1])
                        }
                    }
                    if (colDiff > 0) {
                        for (var j = 0 ; j < colDiff; j++) {
                            for (var i = 0; i < oMatrix.RowsCount() ; i++) {
                                SelectedMatrixVariable().MatrixDetailVariableList()[i].push(model.MatrixDetailClientMapper());
                            }
                        }
                    } else if (colDiff < 0) {
                        for (var j = colDiff ; j < 0; j++) {
                            for (var i = 0; i < oMatrix.RowsCount() ; i++) {
                                SelectedMatrixVariable().MatrixDetailVariableList()[i].remove(SelectedMatrixVariable().MatrixDetailVariableList()[i]()[SelectedMatrixVariable().MatrixDetailVariableList()[0]().length - 1]);

                            }
                        }
                    }

                } else if (SelectedMatrixVariable().ColumnsCount() > 0 & SelectedMatrixVariable().RowsCount() == undefined) {
                    $("#WarningRowColUpdate").html("Select Rows");
                } else if (SelectedMatrixVariable().RowsCount() > 0 & SelectedMatrixVariable().ColumnsCount() == undefined) {
                    $("#WarningRowColUpdate").html("Select Columns");
                } else {
                    for (var i = 0; i < SelectedMatrixVariable().RowsCount() ; i++) {
                        var rowsTem = ko.observableArray([]);
                        for (var j = 0; j < SelectedMatrixVariable().ColumnsCount() ; j++) {
                            var row = model.MatrixDetailClientMapper();
                            if (i == 0 && j == 0) {
                                row.Id(-1);
                            }
                            rowsTem.push(row);

                        }
                        SelectedMatrixVariable().MatrixDetailVariableList.push(rowsTem);

                    }
                }






            }
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
                        view.showAddEditQuestionMenu();
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
            draggedVariableString = function (source, event) {
                if (event != undefined) {
                    if (source.$data.VariableString().indexOf("clickchargezone") > -1) {
                        isZoneVariableType("1");
                        selectedZoneId(source.$data.Id());
                        selectedZoneName(source.$data.ZoneName());
                        view.showVariableSelectDialog();
                    } else {
                        return {
                            row: source.$parent,
                            widget: source.$data,
                            html: source.$data.VariableString().replace(/&quot;/g, '"')
                        };
                    }
                    
                }
                return {};
            },
            dragged = function (source, event) {
                if (event != undefined) {
                    return {
                        row: source.$parent,
                        widget: source.$data,
                        html: event.currentTarget.children[0].value
                    };
                }
                return {};
            },
            dropped = function (source, target, event) {
                if (source.html == undefined) {
                    return;
                }
                var vstring = source.html;
                var currentText;
                if (event.target.disabled == false) {
                    currentText = event.target.value + vstring;
                    event.target.value += vstring;
                    selectedCostCenter().strPriceLabourUnParsed(currentText);
                    formatString(currentText);

                }
            },
            formatString = function (val) {
                val.replace('+', '<span class="redcolor">+</span>');
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
                    confirmation.messageText("WARNING - This item will be removed from the system and you won’t be able to recover.  There is no undo");
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
            },
            DeleteQuestionVariable = function (variable, event) {
                if (event != undefined) {
                    var Id = parseInt($('#' + event.currentTarget.parentElement.parentElement.id).data('invokedOn').closest('span').attr('id'));
                    confirmation.messageText("WARNING - This item will be removed from the system and you won’t be able to recover.  There is no undo");
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
                    if (oQuestionData.Type() == "3") {
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

                    if (oQuestion.Type() == "3") {
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
                confirmation.messageText("WARNING - This item will be removed from the system and you won’t be able to recover.  There is no undo");
                confirmation.afterProceed(function () {
                    deleteCostCenter(oCostCenter);
                });
                confirmation.show();
            },
            getCostCenterByFilter = function () {
                pager().reset();
                getCostCenters();
            },
            getCostCenters = function () {
                isLoadingCostCenter(true);

                dataservice.getCostCentersList({
                    SearchString : searchFilter(),
                    PageSize: pager().pageSize(),
                    PageNo: pager().currentPage(),
                    SortBy: sortOn(),
                    IsAsc: sortIsAsc(),
                    CostCenterType: CostCenterType
                }, {
                    success: function (data) {
                        costCentersList.removeAll();
                        if (data != null) {

                            _.each(data.CostCenters, function (item) {
                                var module = model.costCenterListView.Create(item);
                                costCentersList.push(module);
                            });
                            pager().totalCount(data.RowCount);
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

                if (selectedCostCenter().type() == 11) {

                }
                else if (selectedCostCenter().calculationMethodType() == '2') {
                    if (selectedCostCenter().isTimeVariable() == '2') {
                        if (selectedCostCenter().timeQuestionString() == null || selectedCostCenter().timeQuestionString() == undefined || selectedCostCenter().timeQuestionString().length == 0) {
                            errorList.push({ name: "Enter a Valid Question for Number of Hours.", element: selectedCostCenter().timeQuestionString.domElement });

                            flag = false;
                        }
                    }
                }
                else if (selectedCostCenter().calculationMethodType() == '3') {
                    if (selectedCostCenter().isQtyVariable() == '2') {
                        if (selectedCostCenter().quantityQuestionString() == null || selectedCostCenter().quantityQuestionString() == undefined || selectedCostCenter().quantityQuestionString().length == 0) {
                            errorList.push({ name: "Enter a Valid Question for Number of Hours.", element: selectedCostCenter().quantityQuestionString().domElement });
                            flag = false;
                        }
                    }
                }
                if (!selectedCostCenter().isValid() || errorList().length > 0) {
                    selectedCostCenter().errors.showAllMessages();
                    flag = false;
                }
                return flag;
            },

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
                        if (data.IsParsed) {
                            if (selectedCostCenter().type() == CostCenterType) {
                                selectedCostCenter().costCentreId(data.CostCentreId);
                                costCentersList.splice(0, 0, model.costCenterListView(data.CostCentreId, data.Name, data.WebStoreDesc, data.TypeName, selectedCostCenter().calculationMethodType(), data.IsDisabled));

                            }
                             selectedCostCenter().reset();
                            closeCostCenterDetail();
                            //  getCostCenters();
                            toastr.success("Successfully saved.");
                        } else {
                            toastr.error("Formula String is not valid.");
                        }
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
                        if (data.IsParsed) {


                            if (callback && typeof callback === "function") {
                                callback();
                            }
                            if (selectedCostCenter().type() != CostCenterType) {
                                costCentersList.remove(function (item) { return item.costCenterId() == selectedCostCenter().costCentreId() })

                            } else {


                                selectedCostCenter().type(data.TypeName);
                                selectedCostCenter().reset();
                            }
                            closeCostCenterDetail();
                            getCostCenters();
                            toastr.success("Successfully saved.");
                        } else {
                            toastr.error("Formula String is not valid.");
                        }
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
                getCostCentersBaseData();
                //var cc = new model.CostCenter();
                //setDataForNewCostCenter(cc);
                //cc.type(CostCenterType);
                //selectedCostCenter(cc);

                
                // getVariablesTree();
                showCostCenterDetail();
                //selectedCostCenter().type('3');
                sharedNavigationVM.initialize(selectedCostCenter, function (saveCallback) { saveCostCenter(saveCallback); });
                $("#idCostcenterimage").attr("src", "/mis/Content/Images/imageplaceholder.png");
            },
            createDeliveryCostCenter = function () {
                errorList.removeAll();
                var cc = new model.CostCenter();
                cc.setupCost('0');
                cc.minimumCost('0');
                cc.type('11');
                cc.calculationMethodType('1');
                cc.name('Enter Cost Center name');
                cc.isDisabled(true);
                selectedCostCenter(cc);
                $("#idCostcenterimage").attr("src", "/mis/Content/Images/imageplaceholder.png");
                getCostCentersBaseData();
                // getVariablesTree();
                showCostCenterDetail();
                sharedNavigationVM.initialize(selectedCostCenter, function (saveCallback) { saveCostCenter(saveCallback); });
            },
            setDataForNewCostCenter = function (newcostcenter) {
                newcostcenter.costPerUnitQuantity(0);
                newcostcenter.unitQuantity(0);
                newcostcenter.name('Enter Cost Center name');
                newcostcenter.pricePerUnitQuantity(0);
                newcostcenter.perHourPrice(0);
                newcostcenter.setupCost(0);
                newcostcenter.setupSpoilage(0);
                newcostcenter.setupTime(0);
                newcostcenter.minimumCost(0);
                newcostcenter.timePerUnitQuantity(0);
                newcostcenter.runningSpoilage(0);
                newcostcenter.priority(0);
                newcostcenter.isDirectCost(1);
                newcostcenter.isPrintOnJobCard(1);
                newcostcenter.isDisabled(0);
                newcostcenter.isScheduleable(1);
                newcostcenter.sequence(1);
                newcostcenter.strPriceLabourUnParsed('QuotedLabourPrice = 0');
                // newcostcenter.creationDate(moment().toDate().format(ist.utcFormat) + 'Z');
                newcostcenter.costDefaultValue(0);
                newcostcenter.priceDefaultValue(0);
                newcostcenter.quantitySourceType(1);
                newcostcenter.calculationMethodType(2);
                newcostcenter.isQtyVariable(1);
                newcostcenter.isTimeVariable(1);
                newcostcenter.isCalculationMethodEnable(true);
                newcostcenter.type(CostCenterType);
                //if (CostCenterType == "2") {
                //    newcostcenter.type('2');


                //} else if (CostCenterType == "3") {
                //    newcostcenter.type('3');
                //}

                
               


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
            createWorkInstructionChoice = function (oWorkInstruction) {
                var wic = new model.NewInstructionChoice();
                selectedChoice(wic);
                selectedCostCenter().costCenterInstructions().filter(function (item) { return item.instructionId() == oWorkInstruction.instructionId() })[0].workInstructionChoices.splice(0, 0, wic);
                // selectedInstruction().workInstructionChoices.splice(0, 0, wic);
            },
            deleteWorkInstructionChoice = function (choice) {
                selectedCostCenter().costCenterInstructions().filter(function (item) { return item.instructionId() == choice.instructionId() })[0].workInstructionChoices.remove(choice);
                //selectedInstruction().workInstructionChoices.remove(choice);
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

                            $('#idCostcenterimage')
	                            .load(function () { })
	                            .error(function () {
	                                $("#idCostcenterimage").attr("src", "/mis/Content/Images/imageplaceholder.png");
	                            });


                        }
                    },
                    error: function (response) {
                        toastr.error("Failed to load Detail . Error: ");
                    }
                });
            },
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
                view.initializeLabelPopovers();
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
                            
                        } else if (selectedCostCenter() == undefined || selectedCostCenter().type() != 11) {
                            var cc = new model.CostCenter();
                            setDataForNewCostCenter(cc);
                            //cc.type(CostCenterType);
                            selectedCostCenter(cc);
                            $("#idCostcenterimage").attr("src", "/mis/Content/Images/imageplaceholder.png");
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
                    $("#txtQuotedLabourCost").val($("#txtQuotedLabourCost").val() + icoVal);
                    // selectedCostCenter().strPriceLabourUnParsed(selectedCostCenter().strPriceLabourUnParsed() + icoVal);
                }
                if (selectedVariableString() === 'txtLabourActualCost') {
                    selectedCostCenter().strActualCostLabourUnParsed(selectedCostCenter().strActualCostLabourUnParsed() + icoVal);
                }
                if (selectedVariableString() === 'txtEstimateTimeCost') {
                    selectedCostCenter().strTimeUnParsed(selectedCostCenter().strTimeUnParsed() + icoVal);
                }
            },
             gotoElement = function (validation) {
                 view.gotoElement(validation.element);
             },
            AddtoInputControl = function () {
                if (selectedCostCenter().isEditLabourQuote()) {
                    var t = $("#txtQuotedLabourCost").val() + SelectedStockVariable().VariableString().replace(/&quot;/g, '"');
                    $("#txtQuotedLabourCost").val(t);

                }
                view.hideCostCentreStockDialog();
            }
            openStockItemDialog = function (stockCategoryId) {
                stockDialog.show(function (stockItem) {
                    SelectedStockVariable(model.StockItemVariable(stockItem));
                    getvariableListItem();
                    view.showCostCentreStockDialog();

                }, null, true);
            },
            onCopyCostCenter = function () {
                confirmation.messageText("WARNING - Are you sure you want to copy this cost center?");
                confirmation.afterProceed(function () {
                    dataservice.copyCostCenter(model.costCenterServerMapper(selectedCostCenter()), {
                        success: function (data) {
                            closeCostCenterDetail();
                            var cc = model.costCenterListView({});
                            cc.costCenterId(data);
                            getCostCenterById(cc);
                            getCostCenters();
                            
                        },
                        error: function (exceptionMessage, exceptionType) {

                            if (exceptionType === ist.exceptionType.MPCGeneralException) {
                                toastr.error(exceptionMessage);
                            } else {
                                toastr.error("Failed to copy cost center.");
                            }
                        }
                    });
                });
                confirmation.show();
               
            },
            getZonesVariableTreeChildItems = function() {
                if (zoneVariableNodes().length > 0) {
                    if (showZoneVariableChildList() == 1) {
                        showZoneVariableChildList(0);
                        $("#idZonesVariable").removeClass("fa-chevron-circle-down");
                        $("#idZonesVariable").addClass("fa-chevron-circle-right");

                    } else {
                        showZoneVariableChildList(1);
                        $("#idZonesVariable").addClass("fa-chevron-circle-down");
                        $("#idZonesVariable").removeClass("fa-chevron-circle-right");

                    }

                } else {
                    dataservice.GetTreeListById({
                        id: 7,
                    }, {
                        success: function (data) {
                            zoneVariableNodes.removeAll();
                            _.each(data.ClickChargeZones, function (item) {
                                var zone = model.MachineClickChargeZone(item);
                                zoneVariableNodes.push(zone);
                            });
                            showZoneVariableChildList(1);
                            $("#idZonesVariable").addClass("fa-chevron-circle-down");
                            $("#idZonesVariable").removeClass("fa-chevron-circle-right");
                            //view.showAddEditClickChargeZoneMenu();

                        },
                        error: function () {
                            toastr.error("Failed to load click charge zone data.");
                        }
                    });
                }

            },
            addClickChargeZone = function() {
                var newZone = model.MachineClickChargeZone();
                newZone.Id(0);
                isClickChargeZonesEditorVisible(true);
                selectedClickChargeZones(newZone);
                selectedClickChargeZones().reset();
                view.showClickChargeZoneDialog();
            },
            onDeleteClickChargeZone = function() {
                confirmation.messageText("WARNING - Are you sure you want to delete this click charge zone?");
                confirmation.afterProceed(function () {
                    dataservice.deleteClickChargeZone(model.ClickChargeZoneServerMapper(selectedClickChargeZones()), {
                        success: function (data) {
                            if (data == true) {
                                var delZone = zoneVariableNodes.filter(function (item) { return item.Id() === selectedClickChargeZones().Id() })[0];
                                zoneVariableNodes.remove(delZone);
                                view.hideClickChargeZoneDialog();
                            }
                        },
                        error: function (exceptionMessage, exceptionType) {
                            if (exceptionType === ist.exceptionType.MPCGeneralException) {
                                toastr.error(exceptionMessage);
                            } else {
                                toastr.error("Failed to delete click charge zone.");
                            }
                        }
                    });
                });
                confirmation.show();
            },
           OnEditClickChargeZone = function (zone) {
               isClickChargeZonesEditorVisible(true);
               selectedClickChargeZones(zone);
               selectedClickChargeZones().reset();
               view.showClickChargeZoneDialog();

           },
            editZoneByContextMenu = function() {
                var Id = parseInt($('#' + event.currentTarget.parentElement.parentElement.id).data('invokedOn').closest('span').attr('id'));
                var oZoneData = zoneVariableNodes.filter(function (item) { return item.Id() === Id })[0];
                OnEditClickChargeZone(oZoneData);
            },
            onCloseClickChargeZone = function() {
                if(selectedClickChargeZones().isValid())
                    view.hideClickChargeZoneDialog();
            },
            saveClickCharageZone = function () {
                if (!selectedClickChargeZones().isValid())
                    return;
                dataservice.saveClickChargeZone(model.ClickChargeZoneServerMapper(selectedClickChargeZones()), {
                    success: function (data) {
                        if (selectedClickChargeZones().Id() == 0) {
                            var newZone = model.MachineClickChargeZone(data);
                            zoneVariableNodes.push(newZone);
                        } else {
                            var oZoneData = zoneVariableNodes.filter(function (item) { return item.Id() === data.Id })[0];
                            oZoneData.ZoneName(data.ZoneName);
                        }
                        view.hideClickChargeZoneDialog();
                        

                    },
                    error: function (exceptionMessage, exceptionType) {

                        if (exceptionType === ist.exceptionType.MPCGeneralException) {
                            toastr.error(exceptionMessage);
                        } else {
                            toastr.error("Failed to save click charge zone.");
                        }
                    }
                });
            },
             onChangeToValue = function (To) {
                 switch (To) {
                     case '1':
                         selectedClickChargeZones().From2(parseInt(selectedClickChargeZones().To1()) + parseInt(1));
                         if (selectedClickChargeZones().To1() >= selectedClickChargeZones().To2()) {
                             selectedClickChargeZones().To2(parseInt(selectedClickChargeZones().To1()) + parseInt(102));
                         }
                     case '2':
                         selectedClickChargeZones().From3(parseInt(selectedClickChargeZones().To2()) + parseInt(1));
                         if (selectedClickChargeZones().To2() >= selectedClickChargeZones().To3()) {
                             selectedClickChargeZones().To3(parseInt(selectedClickChargeZones().To2()) + parseInt(102));
                         } else {
                             break;
                         }
                     case '3':
                         selectedClickChargeZones().From4(parseInt(selectedClickChargeZones().To3()) + parseInt(1));
                         if (selectedClickChargeZones().To3() >= selectedClickChargeZones().To4()) {
                             selectedClickChargeZones().To4(parseInt(selectedClickChargeZones().To3()) + parseInt(102));
                         } else {
                             break;
                         }
                     case '4':
                         selectedClickChargeZones().From5(parseInt(selectedClickChargeZones().To4()) + parseInt(1));
                         if (selectedClickChargeZones().To4() >= selectedClickChargeZones().To5()) {
                             selectedClickChargeZones().To5(parseInt(selectedClickChargeZones().To4()) + parseInt(102));
                         } else {
                             break;
                         }
                     case '5':
                         selectedClickChargeZones().From6(parseInt(selectedClickChargeZones().To5()) + parseInt(1));
                         if (selectedClickChargeZones().To5() >= selectedClickChargeZones().To6()) {
                             selectedClickChargeZones().To6(parseInt(selectedClickChargeZones().To5()) + parseInt(102));
                         } else {
                             break;
                         }
                     case '6':
                         selectedClickChargeZones().From7(parseInt(selectedClickChargeZones().To6()) + parseInt(1));
                         if (selectedClickChargeZones().To6() >= selectedClickChargeZones().To7()) {
                             selectedClickChargeZones().To7(parseInt(selectedClickChargeZones().To6()) + parseInt(102));
                         } else {
                             break;
                         }
                     case '7':
                         selectedClickChargeZones().From8(parseInt(selectedClickChargeZones().To7()) + parseInt(1));
                         if (selectedClickChargeZones().To7() >= selectedClickChargeZones().To8()) {
                             selectedClickChargeZones().To8(parseInt(selectedClickChargeZones().To7()) + parseInt(102));
                         } else {
                             break;
                         }
                     case '8':
                         selectedClickChargeZones().From9(parseInt(selectedClickChargeZones().To8()) + parseInt(1));
                         if (selectedClickChargeZones().To8() >= selectedClickChargeZones().To9()) {
                             selectedClickChargeZones().To9(parseInt(selectedClickChargeZones().To8()) + parseInt(102));
                         } else {
                             break;
                         }
                     case '9':
                         selectedClickChargeZones().From10(parseInt(selectedClickChargeZones().To9()) + parseInt(1));
                         if (selectedClickChargeZones().To9() >= selectedClickChargeZones().To10()) {
                             selectedClickChargeZones().To10(parseInt(selectedClickChargeZones().To9()) + parseInt(102));
                         } else {
                             break;
                         }
                     case '10':
                         selectedClickChargeZones().From11(parseInt(selectedClickChargeZones().To10()) + parseInt(1));
                         if (selectedClickChargeZones().To10() >= selectedClickChargeZones().To11()) {
                             selectedClickChargeZones().To11(parseInt(selectedClickChargeZones().To10()) + parseInt(102));
                         } else {
                             break;
                         }
                     case '11':
                         selectedClickChargeZones().From12(parseInt(selectedClickChargeZones().To11()) + parseInt(1));
                         if (selectedClickChargeZones().To11() >= selectedClickChargeZones().To12()) {
                             selectedClickChargeZones().To12(parseInt(selectedClickChargeZones().To11()) + parseInt(102));
                         } else {
                             break;
                         }
                     case '12':
                         selectedClickChargeZones().From13(parseInt(selectedClickChargeZones().To12()) + parseInt(1));
                         if (selectedClickChargeZones().To12() >= selectedClickChargeZones().To13()) {
                             selectedClickChargeZones().To13(parseInt(selectedClickChargeZones().To12()) + parseInt(102));
                         } else {
                             break;
                         }
                     case '13':
                         selectedClickChargeZones().From14(parseInt(selectedClickChargeZones().To13()) + parseInt(1));
                         if (selectedClickChargeZones().To13() >= selectedClickChargeZones().To14()) {
                             selectedClickChargeZones().To14(parseInt(selectedClickChargeZones().To13()) + parseInt(102));
                         } else {
                             break;
                         }
                     case '14':
                         selectedClickChargeZones().From15(parseInt(selectedClickChargeZones().To14()) + parseInt(1));
                         if (selectedClickChargeZones().To14() >= selectedClickChargeZones().To15()) {
                             selectedClickChargeZones().To15(parseInt(selectedClickChargeZones().To14()) + parseInt(102));
                         } else {
                             break;
                         }
                     case '15':
                         break;
                 }
             },
            selectClickChargeZoneVariable = function () {
                var question = isZoneVariableType() === "1" ? selectedZoneName() : selectedZonePromptQuestion();
                var varVal = isZoneVariableType() === "1" ? selectedZoneVariableId() : 1;
                var vstring = "{cinput, ID=\"" + selectedZoneId() + "\",question=\"" + question + "\",type=\"2\",InputType=\"" + isZoneVariableType() + "\",value=\"" + varVal + "\"}";
                var currentText = selectedCostCenter().strPriceLabourUnParsed();
                if (selectedCostCenter().isEditLabourQuote()) {
                    currentText += vstring;
                    selectedCostCenter().strPriceLabourUnParsed(currentText);
                    selectedZoneId(undefined);
                    selectedZoneName(undefined);
                    selectedZoneVariableId(undefined);
                }
                view.hideVariableSelectDialog();
            },
            // #region Observables
            // Initialize the view model
           initialize = function (specifiedView) {
               view = specifiedView;
               ko.applyBindings(view.viewModel, view.bindingRoot);
               pager(pagination.Pagination({ PageSize: 10 }, costCentersList, getCostCenters));


               if (CostCenterType == "2") {
                   //$("#createNewCostCenterId").html("Add New Pre Press Cost Center");
                   $("#idcostcentertypename").html("Pre Press Cost Centers");


               } else if (CostCenterType == "3") {
                  // $("#createNewCostCenterId").html("Add New Post Press Cost Center");
                   $("#idcostcentertypename").html("Post Press Cost Centers");
               }
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
                gotoElement: gotoElement,
                sortIsAsc: sortIsAsc,
                pager: pager,
                errorList: errorList,
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
                matrixVariableNodes: matrixVariableNodes,
                createDeliveryCostCenter: createDeliveryCostCenter,
                SelectedQuestionVariable: SelectedQuestionVariable,
                SelectedMatrixVariable: SelectedMatrixVariable,
                AddAnswerofQuestionVariable: AddAnswerofQuestionVariable,
                QuestionVariableType: QuestionVariableType,
                saveQuestionVariable: saveQuestionVariable,
                CurrencySymbol: CurrencySymbol,
                addQuestionVariable: addQuestionVariable,
                DeleteQuestionVariable: DeleteQuestionVariable,
                getQuestionsVariableTreeChildItems: getQuestionsVariableTreeChildItems,
                showQuestionVariableChildList: showQuestionVariableChildList,
                OnEditQuestionVariable: OnEditQuestionVariable,
                OnDeleteAnswerStringofQuestionVariable: OnDeleteAnswerStringofQuestionVariable,
                saveNewQuestionVariable: saveNewQuestionVariable,
                saveEditedQuestionVariable: saveEditedQuestionVariable,
                draggedVariableString: draggedVariableString,
                getMatricesVariableTreeChildItems: getMatricesVariableTreeChildItems,
                showMatricesVariableChildList: showMatricesVariableChildList,
                OnEditMatrixVariable: OnEditMatrixVariable,
                saveMatrixVariable: saveMatrixVariable,
                UpdateMartix: UpdateMartix,
                addMatrixVariable: addMatrixVariable,
                DeleteMatrixVariable: DeleteMatrixVariable,
                SelectedStockVariable: SelectedStockVariable,
                openStockItemDialog: openStockItemDialog,
                //CalculateCostType: CalculateCostType,
                variableDropdownList: variableDropdownList,
                AddtoInputControl: AddtoInputControl,
                RowscolCountList: RowscolCountList,
                getCostCenterByFilter: getCostCenterByFilter,
                onDeletePermanent: onDeletePermanent,
                onCopyCostCenter: onCopyCostCenter,
                getZonesVariableTreeChildItems: getZonesVariableTreeChildItems,
                showZoneVariableChildList: showZoneVariableChildList,
                zoneVariableNodes: zoneVariableNodes,
                OnEditClickChargeZone: OnEditClickChargeZone,
                isClickChargeZonesEditorVisible: isClickChargeZonesEditorVisible,
                selectedClickChargeZones: selectedClickChargeZones,
                saveClickCharageZone: saveClickCharageZone,
                onCloseClickChargeZone: onCloseClickChargeZone,
                onChangeToValue: onChangeToValue,
                addClickChargeZone: addClickChargeZone,
                onDeleteClickChargeZone: onDeleteClickChargeZone,
                editZoneByContextMenu: editZoneByContextMenu,
                isZoneVariableType: isZoneVariableType,
                selectedZoneVariableId: selectedZoneVariableId,
                selectedZonePromptQuestion : selectedZonePromptQuestion,
                selectClickChargeZoneVariable: selectClickChargeZoneVariable

        };
        })()
    };
    return ist.costcenter.viewModel;
});
