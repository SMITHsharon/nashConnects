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

    vm.message = "Nash NonProfits";

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


    $scope.recommend = (nonprofitId) => {
        $http.get(`/api/NonProfits/${nonprofitId}`)
            .then((getResult) => {
                let thisProfile = getResult.data;
                let likesCount = thisProfile.RecommendCount + 1;

                $http.put(`/api/NonProfits/likes/${nonprofitId}`,
                    {
                        RecommendCount: likesCount,
                        UserName: thisProfile.UserName,
                        FirstName: thisProfile.FirstName,
                        LastName: thisProfile.LastName,
                        Email: thisProfile.Email,
                        Name: thisProfile.Name,
                        WebsiteURL: thisProfile.WebsiteURL,
                        CalendarLink: thisProfile.CalendarLink,
                        Description: thisProfile.Description,
                        Active: true,
                        Id: nonprofitId
                    })
                    .then((putResult) => {
                        console.log("incremented Likes count :: ", putResult);
                        location.reload();
                    })
                    .catch((error) => {
                        console.log("error on Likes count :: ", error);
                    })
            })
            .catch((error) => {
                console.log("error on getNonProfitProfile", error);
            });
    };


}]);