app.controller("flPeepsController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    //.when("/freelancers/peeps/list",
    //    {
    //        for user to view a list of Freelancers who have posted Likes to him/her
    //        templateUrl: "/ngApp/Views/flMypeeps.html",
    //        controller:  "flPeepsController",
    //        controllerAs: 'vm'
    //    })

    let vm = this;
    vm.message = "Nash Peeps";

    // Peeps are Freelancers who have posted Likes to this Freelancers
    // < myuserId / PEEPS >

    $scope.likeGroups;
    $scope.freelancer = {};

    var getPeepsList = () => {
        $http.get("/api/Freelancers/current")
            .then((profileResult) => {
                $scope.freelancer = profileResult.data;
                let userid = profileResult.data.Id;

                $http.get(`/api/Freelancers/${userid}/peeps`)
                    .then((peepsResults) => {
                        $scope.likeGroups = peepsResults.data.FLFLRecommendations;
                    })
                    .catch((peepsError) => {
                        console.error("error on getting Peeps list :: ", peepsError);
                    });
            })
            .catch((getUserProfileError) => {
                console.error("error on getting user profile ::", getUserProfileError);
            });
    };
    getPeepsList();

}]);