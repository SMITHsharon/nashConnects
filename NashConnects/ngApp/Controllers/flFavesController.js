app.controller("flFavesController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    //.when("/freelancers/faves/list",
    //    {
    //        for user to view a list of Freelancers s/he has Liked
    //        templateUrl: "/ngApp/Views/flMyFaves.html",
    //        controller:  "flFavesController",
    //        controllerAs: 'vm'
    //    })

    console.log("in flFavesController");

    let vm = this;
    vm.message = "Nash Faves";

    // Faves are Freelancers who this Freelancer has Liked
    // < FAVES / myuserid >

    $scope.likeGroups;
    $scope.freelancer;

    var getFavesList = () => {
        $http.get("/api/Freelancers/current")
            .then((profileResult) => {
                $scope.freelancer = profileResult.data;
                let userid = profileResult.data.Id;

                $http.get(`/api/Freelancers/${userid}/faves`)
                    .then((favesResults) => {
                        console.log("favesResults.data :: ", favesResults.data);
                        // does not return any of the Freelancers who current user has Liked
                        // only those who have Liked him/her
                        // need to access FLFLRecommendations using the other column as Primary Key ???
                        let favesList = favesResults.data;
                    })
                    .catch((favesError) => {
                        console.error("error on getting Faves list", favesError);
                    })
            })
            .catch((getUserProfileError) => {
                console.log("error on getting user profile", getUserProfileError);
            });
    };
    getFavesList();

}]);