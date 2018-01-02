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


    $scope.recommend = (freelancerId) => {
        $http.get("/api/Freelancers/current")
            .then((result) => {
                $scope.thisProfile = result.data;
                userId = result.data.Id;

                // increment the Likes count
                $http.put(`/api/Freelancers/likes/${freelancerId}`)
                    .then((likesAddResult) => {
                        //location.reload();
                        //$scope.$apply();

                        // post the Likes relationship in the many-to-many table FLFLRecommendations
                        $http.post(`api/Freelancers/likes/${freelancerId}/${userId}`)
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
    
    /*
    $scope.recommend = (freelancerId) => {
        userId = getUserId();
        
        console.log("freelancerid :: ", freelancerId);
        $http.put(`/api/Freelancers/likes/${freelancerId}/ ${userId}`)
            .then((likesAddResult) => {
                //location.reload();
                //$scope.$apply();
            })
            .catch((error) => {
                console.log("error on Likes count :: ", error);
            });
        
    };
    */

}]);

/*
.directive('myLikesDirective', function () {
    return {
        restrict: 'A', // A=Attribute; E=Element
        scope: true,
        link: function (scope, element, attrs) {

            function incrementLikesCount() {
                console.log("It worked!");
            }

            //templateURL: 
        }
    };
});
*/