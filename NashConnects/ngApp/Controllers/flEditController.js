app.controller("flEditController", ["$rootScope", "$scope", "$http", "$location", function ($rootScope, $scope, $http, $location) {

    //.when("/freelance/account,
    //{
    //    for user to edit Freelance profile
    //    templateUrl: "/ngApp/Views/flProfile.html",
    //    controller:  "flEditController",
    //    controllerAs: 'vm'
    //})


    console.log("in Freelancers Edit Controller");
    //let vm = this;

    $scope.delProfile = false;

    $scope.message = "Freelance Profile";

    $scope.thisProfile = {};
    let userid;

    $http.get("/api/Freelancers/current")
        .then((result) => {
            $scope.thisProfile = result.data;
            userid = result.data.Id;
        })
        .catch((error) => {
            console.log("getFreelancerProfile", error);
        });

    
    $scope.editProfile = () => {

        let userProfile = $scope.thisProfile;

        $http.put(`/api/Freelancers/edit/${userid}`,
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
                $location.path('/freelancers/list');
            })
            .catch((error) => {
                console.log("editFreelanceProfile", error);
            });
    };


    $scope.deleteProfile = () => {

        $http.put(`/api/Freelancers/delete/${userid}`)

            .then((deleteResult) => {
                console.log("marked freelancer as Inactive :: ", deleteResult);
                $scope.delProfile = $scope.delProfile === false ? true : false;
                // auto log out?
                $location.path('/freelancers/list');

            })
            .catch((deleteError) => {
                console.error("error on delete freelancer (marking inactive) :: ", deleteError);
            });
    };
        
}]);