angular.module("umbraco").controller("Matryoshka.GroupSeparator.Controller", [

    "$scope",
    "$timeout",
    "$element",
    "editorState",

    function ($scope, $timeout, $element, editorState) {
        var isNew = editorState.getCurrent().id == 0;

        var separator = $element.closest(".umb-nested-content-property-container");
        if (separator.length == 0) {
            separator = $element.closest(".umb-property");
        }
        
        $timeout(function() {
            separator.addClass("our-matryoshka-group-separator-container");
        });

        $scope.toggleCollapse = function() {
            $timeout(function() {
                separator.toggleClass("our-matryoshka-group-separator--collapsed");
                separator.nextUntil(".our-matryoshka-group-separator-container").toggleClass("our-matryoshka-group-separator--collapsed");
            });
        }

        $scope.collapsible = $scope.model.config.collapsible && $scope.model.config.collapsible.indexOf("collapsible") == 0;

        if (($scope.model.config.collapsible == "collapsibleOpenOnCreation" && !isNew) || $scope.model.config.collapsible == "collapsibleClosedOnLoad") {
            $scope.toggleCollapse();
        }

    }
]);
