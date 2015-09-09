var undoLimit = 10;
// JavaScript Document
var undoCheck = 0;
var undoArry = [];
var redoArry = [];
var tempJSON;
function buildUndo(target) {
    var currentState = [];
    if (canvas.getActiveGroup()) {
        var groupObjects = canvas.getActiveGroup().getObjects();
        var group = canvas.getActiveGroup();

        var i = 0;
        groupObjects.forEach(function (a) {
            var clone = $.extend(true, {}, a)
            if (a.type == "text" || a.type == "i-text") {
                clone = c0_undo(canvas, a, 0);
            }
            if (a.undoID == 0) {
                a.undoID = Math.floor(Math.random() * 10000);
            }
            a.productPageID = SP;
            currentState[i] = a;
            clone.undoID = Math.floor(Math.random() * 10000);
            clone.left = a.left + group.left;
            clone.top = a.top + group.top;
            clone.setCoords();
            clone.productPageID = SP;
            ////////////////////////////////
            clone.scaleX = clone.scaleX / dfZ1l;
            clone.scaleY = clone.scaleY / dfZ1l;
            clone.left = clone.left / dfZ1l;
            clone.top = clone.top / dfZ1l;
            /////////////////////////////////
            currentState[i + 1] = clone;
            i += 2;

        }, this);




    } else if (canvas.getActiveObject()) {

        var clone = $.extend(true, {}, canvas.getActiveObject());
        if (canvas.getActiveObject().undoID == 0) {
            canvas.getActiveObject().undoID = Math.floor(Math.random() * 10000);
        }
        var a = canvas.getActiveObject();
        a.productPageID = SP;
        currentState[0] = canvas.getActiveObject();
        clone.undoID = Math.floor(Math.random() * 10000);
        clone.productPageID = SP;
        ////////////////////////////////
        clone.scaleX = clone.scaleX / dfZ1l;
        clone.scaleY = clone.scaleY / dfZ1l;
        clone.left = clone.left / dfZ1l;
        clone.top = clone.top / dfZ1l;
        /////////////////////////////////
        currentState[1] = clone;
    }



    pushUndoDouble(currentState);
}

function pushUndoDouble(currentState) {
    if (undoArry.length > undoLimit) {
        undoArry.shift();
    }
    undoArry.push(currentState);
    redoArry = [];
}

function pushUndoSingle(currentState) {
    if (canvas.getActiveObject()) {
        if (undoArry.length > undoLimit) {
            undoArry.shift();
        }
        //undoArry.push(undoArry.length);
        undoArry.push(currentState);
        redoArry = [];
    }
}

function undo() {
    if (undoArry.length <= 0) {
        return;
    }
    var index = 0;
    var currentState = undoArry.pop();
    var redoState = [], redoCC = 0;
    for (var i = 0; i < currentState.length; i += 2) {
        var objectToRemove = currentState[i];
        var objectToAdd = currentState[i + 1];
        for (var n = 0; n < undoArry.length; n++) {
            var checkObj = undoArry[n];
            for (var z = 0; z < checkObj.length; z += 2) {
                var tempObj = checkObj[z];
                if (tempObj.undoID == objectToRemove.undoID) {
                    checkObj[z] = objectToAdd;
                    console.log(index);
                    break;
                }
            }
        }
        var objs = canvas.getObjects();
        for (var z = 0; z < objs.length; z ++) {
            var tempObj = objs[z];
            if (tempObj.ObjectID == objectToRemove.ObjectID) {
                redoState[redoCC] = objectToRemove; 
                redoCC++;
                var clone = $.extend(true, {}, tempObj);
                ////////////////////////////////
                clone.scaleX = clone.scaleX / dfZ1l;
                clone.scaleY = clone.scaleY / dfZ1l;
                clone.left = clone.left / dfZ1l;
                clone.top = clone.top / dfZ1l;
                /////////////////////////////////
                redoState[redoCC] = clone;
                redoCC++;
                index = canvas.getObjects().indexOf(tempObj);
                canvas.remove(tempObj);
                break;
            }
        }
        
        if (objectToAdd) {

            c2_01_del(objectToAdd);
            objectToAdd.scaleX = objectToAdd.scaleX * dfZ1l;
            objectToAdd.scaleY = objectToAdd.scaleY * dfZ1l;
            objectToAdd.left = objectToAdd.left * dfZ1l;
            objectToAdd.top = objectToAdd.top * dfZ1l;
            canvas.add(objectToAdd);
            if (objectToAdd.type == "i-text" || objectToAdd.type == "text") {
                objectToAdd.initialize(objectToAdd.text, { customStyles: objectToAdd.customStyles });
            }
            if (objectToAdd.type == "path-group") {
                if(objectToAdd.customStyles != null)
                {
                    $.each(objectToAdd.customStyles, function (j, IT) {
                        var clr = IT.OriginalColor;
                        if (IT.ModifiedColor != "")
                            clr = IT.ModifiedColor;

                        if (objectToAdd.isSameColor && objectToAdd.isSameColor() || !objectToAdd.paths) {
                            objectToAdd.setFill(clr);
                        }
                        else if (objectToAdd.paths) {
                            for (var i = 0; i < objectToAdd.paths.length; i++) {
                                if (i == j) {
                                    objectToAdd.paths[i].setFill(clr);
                                }
                            }
                        }
                    });
                }
            }
            canvas.moveTo(objectToAdd, index);
        }
    }
    redoArry.push(redoState);
    canvas.deactivateAll();
    canvas.renderAll();
}

function redo() {
    if (redoArry.length <= 0) {
        return;
    }
    var index = 0;
    var currentState = redoArry.pop();
    var redoState = [], redoCC = 0;
    for (var i = 0; i < currentState.length; i += 2) {
        var objectToRemove = currentState[i];
        var objectToAdd = currentState[i + 1];
        for (var n = 0; n < redoArry.length; n++) {
            var checkObj = redoArry[n];
            for (var z = 0; z < checkObj.length; z += 2) {
                var tempObj = checkObj[z];
                if (tempObj.undoID == objectToRemove.undoID) {
                    checkObj[z] = objectToAdd;
                  //  index = canvas.getObjects().indexOf(objectToAdd);
                    break;
                }
            }
        }
        var objs = canvas.getObjects();
        for (var z = 0; z < objs.length; z++) {
            var tempObj = objs[z];
            if (tempObj.ObjectID == objectToRemove.ObjectID) {
                redoState[redoCC] = objectToRemove; 
                redoCC++;
                var clone = $.extend(true, {}, tempObj);
                ////////////////////////////////
                clone.scaleX = clone.scaleX / dfZ1l;
                clone.scaleY = clone.scaleY / dfZ1l;
                clone.left = clone.left / dfZ1l;
                clone.top = clone.top / dfZ1l;
                /////////////////////////////////
                redoState[redoCC] = clone;
                redoCC++;
                index = canvas.getObjects().indexOf(tempObj);
                canvas.remove(tempObj);
                break;
            }
        }
        
        if (objectToAdd) {
            c2_01_del(objectToAdd);
            objectToAdd.scaleX = objectToAdd.scaleX * dfZ1l;
            objectToAdd.scaleY = objectToAdd.scaleY * dfZ1l;
            objectToAdd.left = objectToAdd.left * dfZ1l;
            objectToAdd.top = objectToAdd.top * dfZ1l;
            canvas.add(objectToAdd);
            if (objectToAdd.type == "i-text" || objectToAdd.type == "text") {
                objectToAdd.initialize(objectToAdd.text, { customStyles: objectToAdd.customStyles });
            }
            if (objectToAdd.type == "path-group") {
                if (objectToAdd.customStyles != null) {
                    $.each(objectToAdd.customStyles, function (j, IT) {
                        var clr = IT.OriginalColor;
                        if (IT.ModifiedColor != "")
                            clr = IT.ModifiedColor;

                        if (objectToAdd.isSameColor && objectToAdd.isSameColor() || !objectToAdd.paths) {
                            objectToAdd.setFill(clr);
                        }
                        else if (objectToAdd.paths) {
                            for (var i = 0; i < objectToAdd.paths.length; i++) {
                                if (i == j) {
                                    objectToAdd.paths[i].setFill(clr);
                                }
                            }
                        }
                    });
                }
            }
            canvas.moveTo(objectToAdd, index);
        }
    }
    undoArry.push(redoState);
    canvas.deactivateAll();
    canvas.renderAll();
}
function c2_01_del(CObk) {
    var found = false;
    $.each(TO, function (i, temp) {
        
        if (temp.ObjectID == CObk.ObjectID) {
            found = true;
            return false;
        }
    });
    if (!found) {
        var IT = fabric.util.object.clone(TO[0]);
        IT.ObjectID = CObk.ObjectID;
        IT.MaxWidth = CObk.maxWidth;
        IT.ProductPageId = CObk.productPageID;
        IT.MaxHeight = CObk.maxHeight;
        IT.PositionX = CObk.left - IT.MaxWidth / 2;
        IT.PositionY = CObk.top - IT.MaxHeight / 2;
        if (CObk.type == "text" || CObk.type == "i-text") {
            IT.ContentString = CObk.text;
            var CustomStylesList = [];
            for (var prop in CObk.customStyles) {
                var objStyle = CObk.customStyles[prop];
                if (objStyle != undefined) {
                    var obj = {
                        textCMYK: objStyle['textCMYK'],
                        textColor: objStyle['color'],
                        fontName: objStyle['font-family'],
                        fontSize: objStyle['font-Size'],
                        fontWeight: objStyle['font-Weight'],
                        fontStyle: objStyle['font-Style'],
                        characterIndex: prop
                    }
                    CustomStylesList.push(obj);
                }
            }
            if (CustomStylesList.length != 0) {
                IT.textStyles = JSON.stringify(CustomStylesList, null, 2);
            } else {
                IT.textStyles = null;
            }
        }
        IT.RotationAngle = CObk.getAngle();
        if (CObk.type != "text" && CObk.type != "i-text") {
            IT.MaxWidth = CObk.width * CObk.scaleX;
            IT.CObk = CObk.height * CObk.scaleY;
            if (CObk.type == "ellipse") {
                IT.CircleRadiusX = CObk.getRadiusX();
                IT.CircleRadiusY = CObk.getRadiusY();
                IT.PositionX = CObk.left - CObk.getWidth() / 2;
                IT.PositionY = CObk.top - CObk.getHeight() / 2;
            }
            if (CObk.type == "image") {
                IT.ClippedInfo = CObk.ImageClippedInfo;
            }
            //IT.Tint =parseInt( OPT.getOpacity() * 100);
        }
        else {
            IT.MaxWidth = CObk.maxWidth;
            IT.MaxHeight = CObk.maxHeight;
            IT.LineSpacing = CObk.lineHeight;

        }
        if (CObk.type == "path-group") {
            //IT.textStyles = OPT.toDataURL();
        }
        if (CObk.textAlign == "left")
            IT.Allignment = 1;
        else if (CObk.textAlign == "center")
            IT.Allignment = 2;
        else if (CObk.textAlign == "right")
            IT.Allignment = 3;

        if (CObk.fontFamily != undefined)
            IT.FontName = CObk.fontFamily;
        else
            IT.FontName = "";

        if (CObk.fontSize != undefined)
            IT.FontSize = CObk.fontSize;
        else
            IT.FontSize = 0;

        if (CObk.fontWeight == "bold")
            IT.IsBold = true;
        else
            IT.IsBold = false;

        if (CObk.fontStyle == "italic")
            IT.IsItalic = true;
        else
            IT.IsItalic = false;

        if (CObk.type != "image") {
            IT.ColorHex = CObk.fill;
        }
        if (CObk.type == "path") {
            IT.ExField1 = CObk.strokeWidth;
        }

        IT.Opacity = CObk.opacity;
        IT.ColorC = CObk.C;
        IT.ColorM = CObk.M;
        IT.ColorY = CObk.Y;
        IT.ColorK = CObk.K;
        IT.IsPositionLocked = CObk.IsPositionLocked;
        IT.IsOverlayObject = CObk.IsOverlayObject;
        IT.IsTextEditable = CObk.IsTextEditable;
        IT.AutoShrinkText = CObk.AutoShrinkText;
        IT.IsHidden = CObk.IsHidden;
        IT.IsEditable = CObk.IsEditable;
        TO.push(IT);
        return;
    }
}

function c0_undo(cCanvas, canvasObj,index) {
    var hAlign = canvasObj.get('textAlign');
    var hStyle = canvasObj.get('fontStyle');
    var hWeight = canvasObj.get('fontWeight');
    var textStyles = canvasObj.customStyles;
    var TOL = new fabric.IText(canvasObj.text, {
        left: canvasObj.left,
        top: canvasObj.top,
        fontFamily: canvasObj.fontFamily,
        fontStyle: canvasObj.fontStyle,
        fontWeight: canvasObj.fontWeight,
        lineHeight:canvasObj.lineHeight,
        fontSize: canvasObj.fontSize,
        angle: canvasObj.angle,
        fill: canvasObj.fill,
        scaleX: canvasObj.scaleX,
        scaleY: canvasObj.scaleY,
        maxWidth: canvasObj.maxWidth,
        maxHeight: canvasObj.maxHeight,
        textAlign: canvasObj.textAlign
    });
    TOL.ObjectID = canvasObj.ObjectID;
    if (textStyles != []) {
        TOL.customStyles = (textStyles);
    }
    TOL.C = canvasObj.C;
    TOL.M = canvasObj.M;
    TOL.Y = canvasObj.Y;
    TOL.K = canvasObj.K;
    TOL.charSpacing = canvasObj.charSpacing;
    TOL.IsPositionLocked = canvasObj.IsPositionLocked;
    TOL.IsOverlayObject = canvasObj.IsOverlayObject;
    TOL.IsHidden = canvasObj.IsHidden;
    TOL.IsEditable = canvasObj.IsEditable;
    TOL.IsTextEditable = canvasObj.IsTextEditable;
    TOL.AutoShrinkText = canvasObj.AutoShrinkText;
    TOL.setAngle(canvasObj.getAngle());

    if (canvasObj.IsPositionLocked) {
        TOL.lockMovementX = true;
        TOL.lockMovementY = true;
        TOL.lockScalingX = true;
        TOL.lockScalingY = true;
        TOL.lockRotation = true;
    }
    else {
        TOL.lockMovementX = false;
        TOL.lockMovementY = false;
        TOL.lockScalingX = false;
        TOL.lockScalingY = false;
        TOL.lockRotation = false;
    }
    TOL.set({
        borderColor: 'red',
        cornerColor: 'orange',
        cornersize: 10
    });

    if (canvasObj.IsQuickText == true) {
        TOL.set({
            borderColor: 'green',
            cornerColor: 'green',
            cornersize: 10
        });
        TOL.IsQuickText = true; 
    }
  
    return TOL;

}
