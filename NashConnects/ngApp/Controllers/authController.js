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
    vm.password = "";

    vm.login = function () {

        console.log(vm.username, vm.password);
        vm.error = "";
        vm.inProgress = true;
        $http({
            method: 'POST',
            url: "/Token",
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
                $location.path("/");

                vm.inProgress = false;
            }, function (result) {
                vm.error = result.data.error_description;
                vm.inProgress = false;
            });
    }


    vm.register = function () {

        console.log("in register function");

        console.log(vm.username, vm.password, vm.confirmpassword);
        vm.error = "";
        vm.inProgress = true;

        /*
        $http({
            method: 'POST',
            ???
            ???
        })
            .then(function (result) {

                ???

                login ... 

                var id = User.Identity.GetUserId();
                $location.url('/freelance/account/:id')

                vm.inProgress = false;
            }, function (result) {
                vm.error = result.data.error_description;
                vm.inProgress = false;
            });
        */

        
    }


    vm.logout = function () {

        console.log("in logout function");

        console.log(vm.username);
        vm.error = "";
        vm.inProgress = true;

        /*
        $http({
            method: 'POST',
        })
        */
        
        $location.url('/freelancers/list');
    }
}
]);