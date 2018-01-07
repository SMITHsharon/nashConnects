app.controller("npEditController", ["$rootScope", "$scope", "$http", "$location", function ($rootScope, $scope, $http, $location) {

    //.when("/nonprofit/account",
    //{
          //for user to edit NonProfit profile
    //    templateUrl: "/ngApp/Views/npProfile.html",
    //    controller: "npEditController",
    //    controllerAs: 'vm'
    //})

    //let vm = this;

    $scope.delProfile = false;

    $scope.message = "Non-Profit Profile";
    let userid;

    $scope.thisProfile = {};

    $http.get("/api/NonProfits/current")
        .then(function (result) {
            $scope.thisProfile = result.data;
            userid = result.data.Id;
        })
        .catch((error) => {
            console.log("getNonProfitProfile", error);
        });


    $scope.editProfile = () => {
        let userProfile = $scope.thisProfile;
        $http.put(`/api/NonProfits/${userid}`,
            {
                UserName: userProfile.UserName,
                FirstName: userProfile.FirstName,
                LastName: userProfile.LastName,
                Email: userProfile.Email,
                Name: userProfile.Name,
                WebsiteURL: userProfile.WebsiteURL,
                CalendarLink: userProfile.CalendarLink,
                Description: userProfile.Description,
                Active: true,
                Id: userid
            })
            .then((result) => {
                $location.path('/NonProfits/list');
            })
            .catch((error) => {
                console.log("editNonProfitProfile", error);
            });
    };


    $scope.deleteProfile = () => {

        $http.put(`api/Nonprofits/delete/${userid}`)

            .then((result) => {
                $scope.delProfile = $scope.delProfile === false ? true : false;
                // auto log out?
                $location.path('/nonprofits/list');
            })
            .catch((deleteError) => {
                console.error("error on deleting profile", deleteError);
            });
    };


}]);