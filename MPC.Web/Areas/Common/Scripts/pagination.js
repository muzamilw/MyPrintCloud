define(["ko", "underscore", "underscore-ko"], function (ko) {

    var
    // Pagination
    // Default Options - Page Size, Pager Limit    
    // ReSharper disable InconsistentNaming
    Pagination = function (defaultOptions, observableList, callback) {
            // ReSharper restore InconsistentNaming
        var // Reference to this object
            // list
            list = observableList,
            currentPage = ko.observable(1),
            // products page size
            pageSize = ko.observable(defaultOptions && defaultOptions.PageSize || 5),
            // Total Count
            totalCount = ko.observable(0),
            // All products Pager Limit
            pagerLimit = ko.observable(defaultOptions && defaultOptions.PagerLimit || 6),
            // Total number of products pages
            totalPages = ko.computed(function() {
                return Math.ceil(totalCount() / pageSize());
            }),
            // List of objects for each page of products
            pages = ko.computed(function() {
                var result = ko.observableArray([]);
                for (var i = 0; i < totalPages(); i++) {
                    result.push({
                        page: i + 1,
                        clicked: function() {
                            if (currentPage() !== this.page) {
                                currentPage(this.page);
                                if (callback && typeof callback === "function") {
                                    callback();
                                }
                            }
                        }
                    });
                }
                return result();
            }),
            // List of products with pager limit
            pagerWithLimit = ko.computed(function() {
                // Return Sliced Records i.e. max 5
                var startIndex = 0;
                var endIndex = pagerLimit() - 1;
                var pageNo = currentPage() - 1;
                if (pageNo < endIndex) {
                    startIndex = 0;
                    endIndex = pagerLimit() - 1;
                } else if (pagerWithLimit().length > 0) {
                    if (pageNo === pagerWithLimit()[pagerWithLimit().length - 1].page) {
                        startIndex = pageNo;
                        endIndex = (pageNo + pagerLimit()) - 1;
                    } else if (pageNo < pagerWithLimit()[pagerWithLimit().length - 1].page &&
                        currentPage() > pagerWithLimit()[0].page - 1) {
                        startIndex = pagerWithLimit()[0].page - 1;
                        endIndex = pagerWithLimit()[pagerWithLimit().length - 1].page;
                    } else if (pageNo < pagerWithLimit()[0].page - 1) {
                        endIndex = pageNo + 1;
                        startIndex = endIndex - (pagerLimit() - 1);
                    }
                } else if (pages().length === 0) {
                    return pages();
                }
                return pages().slice(startIndex, endIndex);
            }),
            // Show Back Page Link
            showBackPageDotLink = ko.computed(function() {
                return currentPage() > pagerLimit() - 1;
            }),
            // Show Next Page Link
            showNextPageDotLink = ko.computed(function() {
                if (pagerWithLimit().length > 0) {
                    return pagerWithLimit()[pagerWithLimit().length - 1].page < totalPages();
                }
                return false;
            }),
            // Go to the next page of products
            nextPage = function() {
                if (currentPage() < totalPages()) {
                    currentPage(currentPage() + 1);
                    if (callback && typeof callback === "function") {
                        callback();
                    }
                }
            },
            // Go to the previous page of products
            previousPage = function() {
                if (currentPage() > 1) {
                    currentPage(currentPage() - 1);
                    if (callback && typeof callback === "function") {
                        callback();
                    }
                }
            },
            // Pages Right
            shiftRight = function() {
                var prevIndex = pagerWithLimit()[0].page - 1;
                currentPage(prevIndex);
                if (callback && typeof callback === "function") {
                    callback();
                }
            },
            // Pages Left
            shiftLeft = function() {
                var nextIndex = pagerWithLimit()[pagerWithLimit().length - 1].page + 1;
                currentPage(nextIndex);
                if (callback && typeof callback === "function") {
                    callback();
                }
            },
            incrementTotalCount = function() {
                totalCount(totalCount() + 1);
            },
            decrementTotalCount = function() {
                totalCount(totalCount() - 1);
            },
            //Total page Count
            totalPageListCount = 0,
            //for showing current page and total result found counter below paging 
            filteredTotalContents = ko.computed(function() {
                var length = list().length;
                var startIndex = (currentPage() - 1) * pageSize();
                var total = startIndex + length;
                if (totalCount() > 0) {
                    startIndex += 1;
                }
                var totalCountValue = totalCount() == 0 ? 0 : totalCount();
                totalCount(totalCountValue);
                if (total > 0) {
                    return ist.resourceText.showing + startIndex + " - " + total + " " + ist.resourceText.of + " " + +(totalCountValue);
                } else {
                    return ist.resourceText.showing + "0" + " - " + total + " " + ist.resourceText.of + " 0";
                }
                //return ist.resourceText.showing + startIndex + " - " + total + " " + ist.resourceText.of + " " + +(totalCountValue);
            }),
            //Update Total Count
            updateTotalCount = ko.computed(function () {
                var length = list().length;
                var startIndex = (currentPage() - 1) * pageSize();
                var total = startIndex + length;
                if (totalPageListCount < length) {
                    incrementTotalCount();
                } else if (totalPageListCount > length) {
                    decrementTotalCount();
                }
                totalPageListCount = length;
            }),
            // Reset
            reset = function() {
                totalCount(0);
                currentPage(1);
            },
            // Is Valid Page - Return true if valid
            isPageValid = function(page) {
                return page && page <= totalPages();
            };

            return {
                showBackPageDotLink: showBackPageDotLink,
                currentPage: currentPage,
                pageSize: pageSize,
                pagerWithLimit: pagerWithLimit,
                showNextPageDotLink: showNextPageDotLink,
                nextPage: nextPage,
                previousPage: previousPage,
                shiftRight: shiftRight,
                shiftLeft: shiftLeft,
                filteredTotalContents: filteredTotalContents,
                pagerLimit: pagerLimit,
                totalCount: totalCount,
                totalPages: totalPages,
                isPageValid: isPageValid,
                incrementTotalCount: incrementTotalCount,
                decrementTotalCount: decrementTotalCount,
                list: list,
                reset: reset
            };
        };

    
    return {
        Pagination: Pagination
    };
});