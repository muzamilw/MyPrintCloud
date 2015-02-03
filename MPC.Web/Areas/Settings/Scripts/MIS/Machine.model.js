define(["ko", "underscore", "underscore-ko"], function (ko) {
    var machineList = function () {
        var
            self,
            MachineId = ko.observable(),
            MachineCatId = ko.observable(),
            MachineName = ko.observable(),
            Description = ko.observable(),
            ImageSource = ko.observable(),
            LookupMethodName=ko.observable(),
            maximumsheetwidth = ko.observable(),
            maximumsheetheight = ko.observable(),
            minimumsheetwidth = ko.observable(),
            minimumsheetheight = ko.observable(),
            errors = ko.validation.group({
                name: MachineName,
                type: MachineCatId,
            }),
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),
            dirtyFlag = new ko.dirtyFlag({
                name: MachineName,
                type: MachineCatId,
            }),
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),

            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            MachineId : MachineId,
            MachineCatId : MachineCatId,
            MachineName : MachineName,
            Description: Description,
            ImageSource : ImageSource,
            dirtyFlag: dirtyFlag,
            errors: errors,
            isValid: isValid,
            hasChanges: hasChanges,
            maximumsheetwidth:maximumsheetwidth,
            maximumsheetheight: maximumsheetheight,
            minimumsheetwidth: minimumsheetwidth,
            minimumsheetheight: minimumsheetheight,
            LookupMethodName:LookupMethodName,
            reset: reset
        };
        return self;
    };
    var machine = function () {
        var
            self,
            MachineId = ko.observable(),
            MachineName = ko.observable(),
            MachineCatId = ko.observable(),
            ColourHeads = ko.observable(),
            isPerfecting = ko.observable(),
            SetupCharge = ko.observable(),
            WashupPrice = ko.observable(),
            WashupCost = ko.observable(),
            MinInkDuctqty = ko.observable(),
            worknturncharge = ko.observable(),
            MakeReadyCost = ko.observable(),
            DefaultFilmId = ko.observable(),
            DefaultPlateId = ko.observable(),
            DefaultPaperId = ko.observable(),
            isfilmused = ko.observable(),
            isplateused = ko.observable(),
            ismakereadyused = ko.observable(),
            iswashupused = ko.observable(),
            maximumsheetweight = ko.observable(),
            maximumsheetheight = ko.observable(),
            maximumsheetwidth = ko.observable(),
            minimumsheetheight = ko.observable(),
            minimumsheetwidth = ko.observable(),
            gripdepth = ko.observable(),
            gripsideorientaion = ko.observable(),
            gutterdepth = ko.observable(),
            headdepth = ko.observable(),
            Va = ko.observable(),
            PressSizeRatio = ko.observable(),
            Description = ko.observable(),
            Priority = ko.observable(),
            DirectCost = ko.observable(),
            Image = ko.observable(),
            MinimumCharge = ko.observable(),
            CostPerCut = ko.observable(),
            PricePerCut = ko.observable(),
            IsAdditionalOption = ko.observable(),
            IsDisabled = ko.observable(),
            LockedBy = ko.observable(),
            CylinderSizeId = ko.observable(),
            MaxItemAcrossCylinder = ko.observable(),
            Web1MRCost = ko.observable(),
            Web1MRPrice = ko.observable(),
            Web2MRCost = ko.observable(),
            Web2MRPrice = ko.observable(),
            ReelMRCost = ko.observable(),
            ReelMRPrice = ko.observable(),
            IsMaxColorLimit = ko.observable(),
            PressUtilization = ko.observable(),
            MakeReadyPrice = ko.observable(),
            InkChargeForUniqueColors = ko.observable(),
            CompanyId = ko.observable(),
            FlagId = ko.observable(),
            IsScheduleable = ko.observable(),
            SystemSiteId = ko.observable(),
            SpoilageType = ko.observable(),
            SetupTime = ko.observable(),
            TimePerCut = ko.observable(),
            MakeReadyTime = ko.observable(),
            WashupTime = ko.observable(),
            ReelMakereadyTime = ko.observable(),
            Maximumsheetweight = ko.observable(),
            Maximumsheetheight = ko.observable(),
            Maximumsheetwidth = ko.observable(),
            Minimumsheetheight = ko.observable(),
            Minimumsheetwidth = ko.observable(),
            LookupMethodId = ko.observable(),
            errors = ko.validation.group({
                name: MachineName,
                type: MachineCatId,
            }),
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),
            dirtyFlag = new ko.dirtyFlag({
                name: MachineName,
                type: MachineCatId,
            }),
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),

            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            MachineId:MachineId,
            MachineName :MachineName ,
            MachineCatId :MachineCatId ,
            ColourHeads : ColourHeads,
            isPerfecting :isPerfecting ,
            SetupCharge :SetupCharge ,
            WashupPrice :WashupPrice ,
            WashupCost :WashupCost ,
            MinInkDuctqty : MinInkDuctqty,
            worknturncharge : worknturncharge,
            MakeReadyCost : MakeReadyCost,
            DefaultFilmId : DefaultFilmId,
            DefaultPlateId : DefaultPlateId,
            DefaultPaperId :DefaultPaperId ,
            isfilmused :isfilmused ,
            isplateused : isplateused,
            ismakereadyused : ismakereadyused,
            iswashupused : iswashupused,
            maximumsheetweight : maximumsheetweight,
            maximumsheetheight :maximumsheetheight ,
            maximumsheetwidth : maximumsheetwidth,
            minimumsheetheight : minimumsheetheight,
            minimumsheetwidth : minimumsheetwidth,
            gripdepth : gripdepth,
            gripsideorientaion : gripsideorientaion,
            gutterdepth : gutterdepth,
            headdepth : headdepth,
            Va : Va,
            PressSizeRatio : PressSizeRatio,
            Description : Description,
            Priority : Priority,
            DirectCost : DirectCost,
            Image : Image,
            MinimumCharge : MinimumCharge,
            CostPerCut : CostPerCut,
            PricePerCut : PricePerCut,
            IsAdditionalOption : IsAdditionalOption,
            IsDisabled : IsDisabled,
            LockedBy : LockedBy,
            CylinderSizeId : CylinderSizeId,
            MaxItemAcrossCylinder : MaxItemAcrossCylinder,
            Web1MRCost : Web1MRCost,
            Web1MRPrice : Web1MRPrice,
            Web2MRCost : Web2MRCost,
            Web2MRPrice : Web2MRPrice,
            ReelMRCost : ReelMRCost,
            ReelMRPrice : ReelMRPrice,
            IsMaxColorLimit : IsMaxColorLimit,
            PressUtilization : PressUtilization,
            MakeReadyPrice : MakeReadyPrice,
            InkChargeForUniqueColors : InkChargeForUniqueColors,
            CompanyId : CompanyId,
            FlagId : FlagId,
            IsScheduleable :IsScheduleable ,
            SystemSiteId : SystemSiteId,
            SpoilageType : SpoilageType,
            SetupTime : SetupTime,
            TimePerCut : TimePerCut,
            MakeReadyTime : MakeReadyTime,
            WashupTime : WashupTime,
            ReelMakereadyTime  : ReelMakereadyTime,    
            Maximumsheetweight : Maximumsheetweight,
            Maximumsheetheight : Maximumsheetheight,
            Maximumsheetwidth : Maximumsheetwidth,
            Minimumsheetheight : Minimumsheetheight,
            Minimumsheetwidth : Minimumsheetwidth,
            LookupMethodId: LookupMethodId,
            dirtyFlag: dirtyFlag,
            errors: errors,
            isValid: isValid,
            hasChanges: hasChanges,
            reset: reset
        };
        return self;
    };

    var machineListClientMapper = function (source) {

        var omachineList = new machineList();
        omachineList.Description(source.Description);
        omachineList.MachineId(source.MachineId);
        omachineList.MachineCatId(source.MachineCatId);
        omachineList.MachineName(source.MachineName);
        omachineList.ImageSource(source.ImageSource);
        omachineList.maximumsheetwidth(source.maximumsheetwidth);
        omachineList.maximumsheetheight(source.maximumsheetheight);
        omachineList.minimumsheetwidth(source.minimumsheetwidth);
        omachineList.minimumsheetheight(source.minimumsheetheight);
        omachineList.LookupMethodName(source.LookupMethodName);
        return omachineList;

    };

    var machineListServerMapper = function (source) {
        var result = {};
        result.Description= source.description;
        result.MachineId= source.MachineId;
        result.MachineCatId= source.MachineCatId;
        result.MachineName= source.MachineName;
        result.ImageSource = source.ImageSource;
        result.maximumsheetwidth = source.maximumsheetwidth;
        result.maximumsheetheight = source.maximumsheetheight;
        result.minimumsheetwidth = source.minimumsheetwidth;
        result.minimumsheetheight = source.minimumsheetheight;
        resultLookupMethodName = source.LookupMethodName;
        return result;
    };
    var machineClientMapper = function (source) {
        var omachine = new machine();
        omachine.MachineId(source.MachineId);
        omachine.MachineName(source.MachineName);
        omachine.MachineCatId(source.MachineCatId);
        omachine.ColourHeads(source.ColourHeads);
        omachine.isPerfecting(source.isPerfecting);
        omachine.SetupCharge(source.SetupCharge);
        omachine.WashupPrice(source.WashupPrice);
        omachine.WashupCost(source.WashupCost);
        omachine.MinInkDuctqty(source.MinInkDuctqty);
        omachine.worknturncharge(source.worknturncharge);
        omachine.MakeReadyCost(source.MakeReadyCost);
        omachine.DefaultFilmId(source.DefaultFilmId);
        omachine.DefaultPlateId(source.DefaultPlateId);
        omachine.DefaultPaperId(source.DefaultPaperId);
        omachine.isfilmused(source.isfilmused);
        omachine.isplateused(source.isplateused);
        omachine.ismakereadyused(source.ismakereadyused);
        omachine.iswashupused(source.iswashupused);
        omachine.maximumsheetweight(source.maximumsheetweight);
        omachine.maximumsheetheight(source.maximumsheetheight);
        omachine.maximumsheetwidth(source.maximumsheetwidth);
        omachine.minimumsheetheight(source.minimumsheetheight);
        omachine.minimumsheetwidth(source.minimumsheetwidth);
        omachine.gripdepth(source.gripdepth);
        omachine.gripsideorientaion(source.gripsideorientaion);
        omachine.gutterdepth(source.gutterdepth);
        omachine.headdepth(source.headdepth);
        omachine.Va(source.Va);
        omachine.PressSizeRatio(source.PressSizeRatio);
        omachine.Description(source.Description);
        omachine.Priority(source.Priority);
        omachine.DirectCost(source.DirectCost);
        omachine.Image(source.Image);
        omachine.MinimumCharge(source.MinimumCharge);
        omachine.CostPerCut(source.CostPerCut);
        omachine.PricePerCut(source.PricePerCut);
        omachine.IsAdditionalOption(source.IsAdditionalOption);
        omachine.IsDisabled(source.IsDisabled);
        omachine.LockedBy(source.LockedBy);
        omachine.CylinderSizeId(source.CylinderSizeId);
        omachine.MaxItemAcrossCylinder(source.MaxItemAcrossCylinder);
        omachine.Web1MRCost(source.Web1MRCost);
        omachine.Web1MRPrice(source.Web1MRPrice);
        omachine.Web2MRCost(source.Web2MRCost);
        omachine.Web2MRPrice(source.Web2MRPrice);
        omachine.ReelMRCost(source.ReelMRCost);
        omachine.ReelMRPrice(source.ReelMRPrice);
        omachine.IsMaxColorLimit(source.IsMaxColorLimit);
        omachine.PressUtilization(source.PressUtilization);
        omachine.MakeReadyPrice(source.MakeReadyPrice);
        omachine.InkChargeForUniqueColors(source.InkChargeForUniqueColors);
        omachine.CompanyId(source.CompanyId);
        omachine.FlagId(source.FlagId);
        omachine.IsScheduleable(source.IsScheduleable);
        omachine.SystemSiteId(source.SystemSiteId);
        omachine.SpoilageType(source.SpoilageType);
        omachine.SetupTime(source.SetupTime);
        omachine.TimePerCut(source.TimePerCut);
        omachine.MakeReadyTime(source.MakeReadyTime);
        omachine.WashupTime(source.WashupTime);
        omachine.ReelMakereadyTime(source.ReelMakereadyTime);
        omachine.Maximumsheetweight(source.Maximumsheetweight);
        omachine.Maximumsheetheight(source.Maximumsheetheight);
        omachine.Maximumsheetwidth(source.Maximumsheetwidth);
        omachine.Minimumsheetheight(source.Minimumsheetheight);
        omachine.Minimumsheetwidth(source.Minimumsheetwidth);
        omachine.LookupMethodId(source.LookupMethodId);
        return omachine;
    };
    return {
        machineList: machineList,
        machineListClientMapper: machineListClientMapper,
        machineListServerMapper: machineListServerMapper,
        machine:machine,
        machineClientMapper: machineClientMapper
    };
});