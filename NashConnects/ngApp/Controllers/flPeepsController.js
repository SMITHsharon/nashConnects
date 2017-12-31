app.controller("flPeepsController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    console.log("in flFavesController");

    let vm = this;
    vm.message = "Nash Peeps";

    // peeps are Freelancers who have posted Likes to this Freelancers
    $scope.peepGroups;
    $scope.peepFreelancers;
    $scope.freelancer = {};


    var getPeepsList = () => {
        $http.get("/api/Freelancers/current")
            .then((profileResult) => {
                console.log("profileResult :: ", profileResult);
                $scope.freelancer = profileResult.data;
                let userid = profileResult.data.Id

                $http.get(`/api/Freelancers/${userid}/peeps`)
                    .then((peepsResults) => {


                        //let likesArray = peepsResults;
                        console.log("userProfile :: ", peepsResults);
                        console.log("userid :: ", userid);
                        
                        //$scope.faveGroups = peepsResults.data.FLFLRecommendations;
                        $scope.peepGroups = peepsResults.data.FLFLRecommendations;
                    })
                    .catch((peepsError) => {
                        console.log("error on getting Peeps list :: ", peepsError);
                    });

                

                //$scope.freelanceGroups = likesArray;
            })
            .catch((getUserProfileError) => {
                console.log("error on getting user profile ::", error);
            });
    };
    getPeepsList();


}]);