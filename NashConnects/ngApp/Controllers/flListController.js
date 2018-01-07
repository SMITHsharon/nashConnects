app.controller("flListController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    //.when("/freelancers/list",
    //{
    //    for user to view a list of Freelancers
    //    templateUrl: "/ngApp/Views/flList.html",
    //    controller:  "flListController",
    //    controllerAs: 'vm'
    //})

    let vm = this;
    vm.message = "Nash Freelancers";

    $scope.freelancerGroups = [];
    let userProfile = {};
    let userid;

    var getFreelancerList = function () {
        $http.get("/api/Freelancers/list")
            .then((result) => {
                var freelancerGroupings = result.data;
                $scope.freelancerGroups = freelancerGroupings;

            }).catch((error) => {
                console.error("error, listing all Freelancers :: ", error);
            });
    };
    getFreelancerList();


    $scope.recommend = (freelancer) => {
        $http.get("/api/Freelancers/current")
            .then((result) => {
                $scope.thisProfile = result.data;
                userId = result.data.Id;
                freelancer.RecommendCount += 1;
                // increment the Likes count
                $http.put(`/api/Freelancers/likes/${freelancer.Id}`)
                    .then((likesAddResult) => {
                        //location.reload();
                        //$scope.$apply();

                        // post the Likes relationship in the many-to-many table FLFLRecommendations
                        $http.post(`api/Freelancers/likes/${freelancer.Id}/${userId}`)
                            .then((postLikesRelationshipResult) => {

                            })
                            .catch((error) => {
                                console.log("error on posting Likes Relationship :: ", error);
                            });
                    })
                    .catch((error) => {
                        console.log("error on Likes count :: ", error);
                    });
            })
            .catch((error) => {
                console.log("getFreelancerProfile", error);
            });
    };


}]);