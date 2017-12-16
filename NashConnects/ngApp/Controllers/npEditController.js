app.controller("npEditController", ["$rootScope", "$scope", "$http", "$location", function ($rootScope, $scope, $http, $location) {

    //.when("/nonprofit/account",
    //{
          //for user to edit NonProfit profile
    //    templateUrl: "/ngApp/Views/npProfile.html",
    //    controller: "npEditController",
    //    controllerAs: 'vm'
    //})

    console.log("in NonProfits Edit Controller");
    //let vm = this;

    $scope.message = "Non-Profit Profile";
    let userid;

    $scope.thisProfile = {};

    $http.get("/api/NonProfits/current")
        .then(function (result) {
            console.log("result.data", result.data);
            $scope.thisProfile = result.data;
            userid = result.data.Id;
            console.log("userid :: ", userid);
        })
        .catch((error) => {
            console.log("getNonProfitProfile", error);
        });


    $scope.editProfile = () => {
        let userProfile = $scope.thisProfile;
        console.log("editing Profile; userid :: ", userid);
        $http.put(`/api/Nonprofits/${userid}`,
            {
                UserName: userProfile.UserName,
                FirstName: userProfile.FirstName,
                LastName: userProfile.LastName,
                Email: userProfile.Email,
                WebsiteURL: userProfile.WebsiteURL,
                Description: userProfile.Description,
                Active: true,
                Id: userid
            })
            .then((result) => {
                console.log("editNonProfitProfile", result);
            })
            .catch((error) => {
                console.log("editNonProfitProfile", error);
            });
    };

}]);