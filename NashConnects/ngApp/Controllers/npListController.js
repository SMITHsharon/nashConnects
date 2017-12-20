app.controller("npListController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    //.when("/nonprofits/list",
    //{
    //for user to view a list of NonProfits
    //    templateUrl: "/ngApp/Views/npList.html",
    //    controller:  "npListController",
    //    controllerAs: 'vm'
    //})

    let vm = this;

    vm.message = "Nash NonProfits";

    $scope.nonprofits;

    var getNonProfitList = function () {
        $http.get("/api/NonProfits/list")
            .then(function (result) {
                var dataResults = result.data;
                var listOfNonProfits = [];

                if (dataResults.length > 0) {
                    Object.keys(dataResults).forEach((key) => {
                        dataResults[key].id = key;
                        listOfNonProfits.push(dataResults[key]);
                    });
                }
                $scope.nonprofits = listOfNonProfits;
            }).catch(function (error) {
                console.log("error, listing all NonProfits :: ", error);
            });
    };
    getNonProfitList();


    $scope.recommend = (nonprofitId) => {
        $http.put(`/api/NonProfits/likes/${nonprofitId}`)
            .then((likesAddResult) => {
                location.reload();
            })
            .catch((error) => {
                console.log("error on Likes count :: ", error);
            });
    };

    
    $scope.postEvent = (nonprofitId) => {
            $location.url(`/event/add/${nonprofitId}`);
    };
    

}]);