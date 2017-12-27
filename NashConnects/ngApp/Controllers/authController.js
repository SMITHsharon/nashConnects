app.controller("authController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    //.when("/register",
    //{
    //    templateUrl: "/ngApp/Views/register.html",
    //    controller:  "authController",
    //    controllerAs: 'vm'
    //})
    //.when("/login",
    //{
    //    templateUrl: "/ngApp/Views/login.html",
    //    controller:  "authController",
    //    controllerAs: 'vm'
    //})
    //.when("/logout",
    //{
    //    templateUrl: "/ngApp/Views/logout.html",
    //    controller:  "authController",
    //    controllerAs: 'vm'
    //})

    console.log("in authController");
    let vm = this;

    vm.loginmessage = "Sign In ...";
    vm.registermessage = "Register ...";
    vm.username = "";
    vm.websiteURL = "";
    vm.password = "";
    vm.confirmPassword = "";

    vm.login = function () {

        console.log(vm.username, vm.password);
        vm.error = "";
        vm.inProgress = true;
        $http({
            method: 'POST',
            url: "/Token",
            //AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj)
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                return str.join("&");
            },
            data: { grant_type: "password", username: vm.username, password: vm.password }
        })
            .then(function (result) {
                sessionStorage.setItem('token', result.data.access_token);
                $http.defaults.headers.common['Authorization'] = `bearer ${result.data.access_token}`;
                $location.path('/freelancers/list');
                console.log("logged in", result);

                vm.inProgress = false;
            })
            .catch((result) => {
                vm.error = result.data.error_description;
                vm.inProgress = false;
            });
    }


    vm.register = function () {

        console.log("in register function");

        console.log(vm.username, vm.password, vm.confirmpassword);
        vm.error = "";
        vm.inProgress = true;
        
        $http({
            method: 'POST',
            url: "/api/Account/Register",
            data:
            {
                UserName: vm.username,
                WebsiteURL: vm.websiteURL,
                Password: vm.password,
                confirmPassword: vm.confirmPassword
            }
        })
            .then((result) => {
                console.log("resultz in Register", result);
                //sessionStorage.setItem('token', result.data.access_token);
                //currentToken = sessionStorage.getItem('token');
                //$rootScope.UserName = result.config.data.UserName;
                //console.log("currentToken :", currentToken);
                console.log("regestration Done welcome :", result.config.data.UserName);
                $scope.authenticate = false;
                //login ... 

                //var id = User.Identity.GetUserId();
                //$location.url('/freelance/account/:id')

                vm.inProgress = false;
            })
            .catch((result) => {
                vm.error = result.data.error_description;
                console.log("error in register :", result.data.Message);
                vm.inProgress = false;
            });
    }

    
    vm.logout = function () {

        console.log("in logout function");

        console.log(vm.username);
        vm.error = "";
        vm.inProgress = true;
        
        $http({
                method: 'POST',
                url: "/api/Account/Logout"
        })

        sessionStorage.removeItem('token');

        $location.path('/freelancers/list');
        }
}]);