angular.module('umbraco.services').config([
    '$httpProvider',
    function ($httpProvider) {

        $httpProvider.interceptors.push(function ($q) {
            return {
                'response': function (response) {

                    if (response.config.url.includes("views/content/apps/content/content.html") || response.config.url.includes("doctypegrideditor.dialog.html")) {
                        response.data = response.data.replace("umb-tabbed-content", "matryoshka-tabbed-content");
                    }

                    else if (response.config.url.includes("views/member/apps/content/content.html")) {
                        response.data = "<div class=\"form-horizontal\" ng-controller=\"Umbraco.Editors.Member.Apps.ContentController as vm\"><matryoshka-tabbed-content ng-if=\"!loading\" content=\"content\"></matryoshka-tabbed-content></div>";
                    }

                    else if (response.config.url.includes("views/media/apps/content/content.html")) {
                        response.data = "<div class=\"form-horizontal\" ng-controller=\"Umbraco.Editors.Media.Apps.ContentController as vm\"><matryoshka-tabbed-content ng-if=\"!loading\" content=\"content\"></matryoshka-tabbed-content></div>";
                    }

                    return response;
                }
            };
        });

    }]);
