﻿app.controller("flEditController", ["$rootScope", "$scope", "$http", "$location", function ($rootScope, $scope, $http, $location) {

    //.when("/freelance/account,
    //{
    //for user to edit Freelance profile
    //    templateUrl: "/ngApp/Views/flProfile.html",
    //    controller: "flEditController",
    //    controllerAs: 'vm'
    //})


    console.log("in Freelancers Edit Controller");
    //let vm = this;

    $scope.message = "Freelance Profile";
    let userid;

    $scope.thisProfile = {};

    $http.get("/api/Freelancers/current")
        .then(function (result) {
            console.log("result.data", result.data);
            $scope.thisProfile = result.data;
            userid = result.data.Id;
            console.log("userid :: ", userid);
        })
        .catch((error) => {
            console.log("getFreelancerProfile", error);
        });

    
    $scope.editProfile = () => {
        let userProfile = $scope.thisProfile;
        console.log("editing Profile; userid :: ", userid);
        $http.put(`/api/Freelancers/${userid}`,
            {
                UserName: userProfile.UserName,
                FirstName: userProfile.FirstName,
                LastName: userProfile.LastName,
                Email: userProfile.Email,
                WebsiteURL: userProfile.WebsiteURL,
                Category: userProfile.Category,
                Description: userProfile.Description,
                Newsletter: userProfile.Newsletter,
                PublicReveal: userProfile.PublicReveal,
                Active: true,
                Id: userid
            })
            .then((result) => {
                console.log("editFreelanceProfile", result);
            })
            .catch((error) => {
                console.log("editFreelanceProfile", error);
            });
        };
        
}]);