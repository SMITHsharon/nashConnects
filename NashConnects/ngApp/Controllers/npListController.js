app.controller("npListController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    //.when("/nonprofits/list",
    //{
          //for user to view a list of NonProfits
    //    templateUrl: "/ngApp/Views/npList.html",
    //    controller:  "npListController",
    //    controllerAs: 'vm'
    //})

    console.log("in NonProfits List Controller");
    let vm = this;

    vm.message = "This is Nash NonProfits List";

    $scope.nonprofits;

    var getNonProfitList = function () {
        $http.get("/api/NonProfits/list")
            .then(function (result) {
                console.log("result, listing all NonProfits :: ", result);
                var dataResults = result.data;
                console.log("NonProfits List result.data :: ", dataResults);
                var listOfNonProfits = [];

                if (dataResults.length > 0) {
                    Object.keys(dataResults).forEach((key) => {
                        dataResults[key].id = key;
                        listOfNonProfits.push(dataResults[key]);
                    });
                }
                $scope.nonprofits = listOfNonProfits;
                console.log($scope.nonprofits);
            }).catch(function (error) {
                console.log("error, listing all NonProfits :: ", error);
            });
    };
    getNonProfitList();

}
]);