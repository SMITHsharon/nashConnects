app.controller("flPeepsController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    //.when("/freelancers/peeps/list",
    //    {
    //        for user to view a list of Freelancers who have posted Likes to him/her
    //        templateUrl: "/ngApp/Views/flMypeeps.html",
    //        controller:  "flPeepsController",
    //        controllerAs: 'vm'
    //    })

    console.log("in PeepsController");
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
                console.log("userid :: ", userid);

                $http.get(`/api/Freelancers/${userid}/peeps`)
                    .then((peepsResults) => {
                        console.log("peepsResults :: ", peepsResults);
                        console.log("peepsResults.data :: ", peepsResults.data);
                        //console.log("peepsResults.data.FLFLRecommendations :: ", peepsResults.data.FLFLRecommendations);
                        //$scope.likeGroups = peepsResults.data.FLFLRecommendations;
                        $scope.likeGroups = peepsResults.data;
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