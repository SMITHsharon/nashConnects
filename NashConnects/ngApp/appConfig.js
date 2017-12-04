app.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when("/",
        {
            templateUrl: "/ngApp/Views/home.html",
            controller: "homeController",
            controllerAs: 'vm'
        })
        .when("/login",
        {
            templateUrl: "/ngApp/Views/login.html",
            controller: "loginController",
            controllerAs: 'vm'
        })
        .when("/profile/freelance",
        {
            templateUrl: "/ngApp/Views/FLprofile.html",
            controller: "FLAccountController",
            controllerAs: 'vm'
        })
        .when("/profile/nonprofit",
        {
            //templateUrl: "/ngApp/Views/xxx.html",
            //controller: "xxxController",
            //controllerAs: 'vm'
        })
        .when("/nonprofit/approve",
        {
            // reuse non-profit profile screen
            //templateUrl: "/ngApp/Views/xxx.html",
            //controller: "xxxController",
            //controllerAs: 'vm'
        })
        .when("/profile/online",
        {
            //templateUrl: "/ngApp/Views/xxx.html",
            //controller: "xxxController",
            //controllerAs: 'vm'
        })
        .when("/profile/nonprofit",
        {
            //templateUrl: "/ngApp/Views/xxx.html",
            //controller: "xxxController",
            //controllerAs: 'vm'
        })
        .when("/nonprofits/list",
        {
            //templateUrl: "/ngApp/Views/xxx.html",
            //controller: "xxxController",
            //controllerAs: 'vm'
        })
        .when("/onlines/list",
        {
            //templateUrl: "/ngApp/Views/xxx.html",
            //controller: "xxxController",
            //controllerAs: 'vm'
        })
        .when("/freelancers/list",
        {
            //templateUrl: "/ngApp/Views/xxx.html",
            //controller: "xxxController",
            //controllerAs: 'vm'
        })
        .when("/event/add",
        {
            //templateUrl: "/ngApp/Views/xxx.html",
            //controller: "xxxController",
            //controllerAs: 'vm'
        })
        .when("/events/list",
        {
            //templateUrl: "/ngApp/Views/xxx.html",
            //controller: "xxxController",
            //controllerAs: 'vm'
        })
        .when("/event/detail/:id",
        {
            //templateUrl: "/ngApp/Views/xxx.html",
            //controller: "xxxController",
            //controllerAs: 'vm'
        })
        .when("/category/add",
        {
            //templateUrl: "/ngApp/Views/xxx.html",
            //controller: "xxxController",
            //controllerAs: 'vm'
        })
        .when("/category/edit/:id",
        {
            //templateUrl: "/ngApp/Views/xxx.html",
            //controller: "xxxController",
            //controllerAs: 'vm'
        });
        //.othersise("/");
}]);

app.run(["$rootScope", "$http", "$location", function ($rootScope, $http, $location) {

    $rootScope.isLoggedIn = function () { return !!sessionStorage.getItem("token") }

    $rootScope.$on("$routeChangeStart", function (event, currRoute) {
        var anonymousPage = false;
        var originalPath = currRoute.originalPath;

        if (originalPath) {
            anonymousPage = originalPath.indexOf("/login") !== -1;
        }

        if (!anonymousPage && !$rootScope.isLoggedIn()) {
            event.preventDefault();
            $location.path("/login");
        }
    });

    var token = sessionStorage.getItem("token");

    if (token)
        $http.defaults.headers.common["Authorization"] = `bearer ${token}`;
}])