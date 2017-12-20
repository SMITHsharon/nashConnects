app.config(["$routeProvider", function ($routeProvider) {
    console.log("in routeProvider");
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
            controller: "authController",
            controllerAs: 'vm'
        })
        .when("/register",
        {
            // for user to register as Freelance or Non-Profit
            templateUrl: "/ngApp/Views/register.html",
            controller: "authController",
            controllerAs: 'vm'
        })
        //.when("/nonprofit/register",
        //{
            // for user to register as NonProfit
        //    templateUrl: "/ngApp/Views/npAccount.html",
        //    controller: "npRegisterController",
        //    controllerAs: 'vm'
        //})
        //.when("/nonprofit/approve",
        //{
        //    // reuse non-profit profile screen
        //    //templateUrl: "/ngApp/Views/npAccount.html",
        //    //controller: "xxxController",
        //    //controllerAs: 'vm'
        //})
        .when("/freelancers/list",
        {
            //for user to view a list of Freelancers
            templateUrl: "/ngApp/Views/flList.html",
            controller: "flListController",
            controllerAs: 'vm'
        })
        .when("/freelancers/list/newsletter",
        {
            //for user to view a list of Freelancers
            templateUrl: "/ngApp/Views/flListNewsletters.html",
            controller: "newsletterListController",
            controllerAs: 'vm'
        })
        //.when("/freelance/profile/:id",
        //{
            //for user to view a Freelance profile
        //    templateUrl: "/ngApp/Views/flProfile.html",
        //    controller: "flViewController",
        //    controllerAs: 'vm'
        //})
        .when("/nonprofits/list",
        {
            //for user to view a list of NonProfits
            templateUrl: "/ngApp/Views/npList.html",
            controller: "npListController",
            controllerAs: 'vm'
        })
        //.when("/nonprofit/profile/:id",
        //{
            //for user to view a NonProfit profile
        //    templateUrl: "/ngApp/Views/npProfile.html",
        //    controller: "npViewController",
        //    controllerAs: 'vm'
        //})
        .when("/freelance/account",
        {
            // for user to edit Freelance profile
            templateUrl: "/ngApp/Views/flProfile.html",
            controller: "flEditController",
            controllerAs: 'vm'
        })
        .when("/nonprofit/account",
        {
            // for user to edit NonProfit profile
            templateUrl: "/ngApp/Views/npProfile.html",
            controller: "npEditController",
            controllerAs: 'vm'
        })
        .when("/event/add/:nonprofitid",
        {
            // for NonProfit user to add an Event
            templateUrl: "/ngApp/Views/EventNew.html",
            controller: "eventAddController",
            controllerAs: 'vm'
        })
        .when("/nonprofit/:id/events/list",
        {
            // for user to list Events
            templateUrl: "/ngApp/Views/EventsList.html",
            controller: "eventListController",
            controllerAs: 'vm'
        })
        .when("/event/detail/:id",
        {
            // for user to list Events
            templateUrl: "/ngApp/Views/EventDetail.html",
            controller: "eventViewController",
            controllerAs: 'vm'
        })
        //.when("/category/add",
        //{
        //    //templateUrl: "/ngApp/Views/xxx.html",
        //    //controller: "xxxController",
        //    //controllerAs: 'vm'
        //})
        //.when("/category/edit/:id",
        //{
        //    //templateUrl: "/ngApp/Views/xxx.html",
        //    //controller: "xxxController",
        //    //controllerAs: 'vm'
        //})
        .when("/logout",
        {
            templateUrl: "/ngApp/Views/logout.html",
            controller: "authController",
            controllerAs: 'vm'
        })
        .otherwise("/freelancers/list");
            
}]);

app.run(["$rootScope", "$http", "$location", function ($rootScope, $http, $location) {

    $rootScope.isLoggedIn = function () { return !!sessionStorage.getItem("token") }

    $rootScope.$on("$routeChangeStart", function (event, currRoute) {
        var anonymousPage = true;
        var originalPath = currRoute.originalPath;

        if (originalPath) {
            //anonymousPage = originalPath.indexOf("/login") !== -1;
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