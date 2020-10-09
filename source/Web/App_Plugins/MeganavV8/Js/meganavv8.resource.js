angular.module("umbraco.resources").factory("meganavV8Resource", function ($http, iconHelper) {
    return {
        getById: function (id, culture) {

            return $http.get("backoffice/MeganavV8/MeganavV8EntityApi/GetById?id=" + id + "&culture=" + culture)
                .then(function (response) {
                    var item = response.data;
                    item.icon = iconHelper.convertFromLegacyIcon(item.icon);
                    return response;
                }
            );
        }
    }
});