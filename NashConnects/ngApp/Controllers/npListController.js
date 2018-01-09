app.controller("npListController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    //.when("/nonprofits/list",
    //{
    //for user to view a list of NonProfits
    //    templateUrl: "/ngApp/Views/npList.html",
    //    controller:  "npListController",
    //    controllerAs: 'vm'
    //})

    let vm = this;

    vm.message = "Nash NonProfits and Other Resources";

    $scope.nonprofits;
    let userid;

    var getNonProfitList = () => {
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

            }).catch((error) => {
                console.log("error, listing all NonProfits :: ", error);
            });
    };
    getNonProfitList();


    $scope.recommend = (nonprofit) => {
        $http.get("api/NonProfits/current")
            .then((getResult) => {
                userid = getResult.data.Id;
                nonprofit.RecommendCount += 1;
                
                $http.put(`/api/NonProfits/likes/${nonprofit.Id}`)
                    .then((likesAddResult) => {
                        /*
                        // posts the Likes relationship in the many-to-many table NPNPRecommendations
                        // not information anyone wd really keep up w
                        // more likely :: FL <=> NP relationships
                        // to track, need to differentiate diff types of users
                        $http.post(`api/NonProfits/likes/${nonprofit.Id}/${userid}`)
                            .then((postLikesRelationshipResult) => {
                            })
                            .catch((error) => {
                                console.error("error on posting nonProfit Likes relationship ", error);
                            });
                    */
                    })
                    .catch((error) => {
                        console.log("error on Likes count :: ", error);
                    });
            })
            .catch((getError) => {
                console.error("error getting NonProfit profile :: ", getError);
            });

       
    };


    $scope.listEvents = (nonprofitId) => {
        $location.url(`/nonprofit/${nonprofitId}/events/list`)
    };
    

}]);