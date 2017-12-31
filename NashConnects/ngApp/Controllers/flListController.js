app.controller("flListController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    //.when("/freelancers/list",
    //{
    //for user to view a list of Freelancers
    //    templateUrl: "/ngApp/Views/flList.html",
    //    controller:  "flListController",
    //    controllerAs: 'vm'
    //})

    let vm = this;
    vm.message = "Nash Freelancers";

    $scope.freelancerGroups = [];

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
        console.log("in IncremntCount");
        $http.put(`/api/Freelancers/likes/${freelancerId}`)
            .then((likesAddResult) => {
                //location.reload();
                //$scope.$apply();
            })
            .catch((error) => {
                console.log("error on Likes count :: ", error);
            });
        
    };

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