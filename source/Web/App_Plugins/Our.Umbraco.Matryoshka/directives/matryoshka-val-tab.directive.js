function valTab(timeout) {
    return {
        require: ['^^form', '^^valFormManager'],
        restrict: "A",
        link: function (scope, element, attr, ctrs) {

            var valFormManager = ctrs[1];

            var tabAlias = scope.group.alias;
            scope.tabHasError = false;

            //listen for form validation changes
            valFormManager.onValidationStatusChanged(function (evt, args) {

                if (!args.form.$valid) {


                    var tabContent = element.closest(".umb-editor").find("[data-element='group-" + tabAlias + "']");
                    //check if the validation messages are contained inside of this tabs

                    if (tabContent.find(".ng-invalid").length > 0) {
                        scope.tabHasError = true;
                    } else {
                        scope.tabHasError = false;
                    }

                }
                else {
                    scope.tabHasError = false;
                }

            });
        }
    };
}
angular.module('umbraco.directives.validation').directive("matryoshkaValTab", ['$timeout',valTab]);