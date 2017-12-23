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
            .then(function (result) {
                var freelancerGroupings = result.data;
                $scope.freelancerGroups = freelancerGroupings;

            }).catch(function (error) {
                console.error("error, listing all Freelancers :: ", error);
            });
    };
    getFreelancerList();


    $scope.recommend = (freelancerId) => {
        $http.put(`/api/Freelancers/likes/${freelancerId}`)
            .then((likesAddResult) => {
                location.reload();
            })
            .catch((error) => {
                console.log("error on Likes count :: ", error);
            });
    };

}]);