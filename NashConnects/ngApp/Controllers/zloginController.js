app.controller("loginController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    //.when("/login",
    //{
    //    templateUrl: "/ngApp/Views/login.html",
    //    controller:  "loginController",
    //    controllerAs: 'vm'
    //})

    let vm = this;

    vm.message = "This is login";
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
}
]);