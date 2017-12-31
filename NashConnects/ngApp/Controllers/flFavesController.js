app.controller("flFavesController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    console.log("in flFavesController");

    let vm = this;
    vm.message = "Nash Freelancers";

    $scope.faveGroups;
    $scope.faveFreelancers;
    $scope.freelancer = {};


    var getFavesList = () => {
        $http.get("/api/Freelancers/current")
            .then((profileResult) => {
                console.log("profileResult :: ", profileResult);
                $scope.freelancer = profileResult.data;
                let userid = profileResult.data.Id

                $http.get(`/api/Freelancers/${userid}/faves`)
                    .then((favesResults) => {


                        //let likesArray = favesResults;
                        console.log("userProfile :: ", favesResults);
                        console.log("userid :: ", userid);
                        
                        //$scope.faveGroups = favesResults.data.FLFLRecommendations;
                        $scope.faveGroups = favesResults.data.FLFLRecommendations;
                    })
                    .catch((favesError) => {
                        console.log("error on getting Faves list :: ", favesError);
                    });

                

                //$scope.freelanceGroups = likesArray;
            })
            .catch((getUserProfileError) => {
                console.log("error on getting user profile ::", error);
            });
    };
    getFavesList();


}]);