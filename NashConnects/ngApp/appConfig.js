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
        .when("/freelancers/faves/list",
        {
            //for user to view a list of Freelancers s/he has Liked
            templateUrl: "/ngApp/Views/flMyFaves.html",
            controller: "flFavesController",
            controllerAs: 'vm'
        })
        .when("/freelancers/peeps/list",
        {
            //for user to view a list of Freelancers who have posted Likes to him/her
            templateUrl: "/ngApp/Views/flMypeeps.html",
            controller: "flPeepsController",
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
        .when("/nonprofit/:nonprofitid/event/:eventid/edit",
        {
            // for NonProfit user to edit an Event
            templateUrl: "/ngApp/Views/EventEdit.html",
            controller: "eventEditController",
            controllerAs: 'vm'
        })
        .when("/nonprofit/:nonprofitid/events/list",
        {
            // for user to list Scheduled Events for Selected NonProfit
            templateUrl: "/ngApp/Views/EventsListSingleNonProfit.html",
            controller: "eventListScheduledController",
            controllerAs: 'vm'
        })
        .when("/events/list/scheduled",
        {
            // for user to list Scheduled Events for All NonProfits
            templateUrl: "/ngApp/Views/EventsListAllNonProfits.html",
            controller: "eventListScheduledController",
            controllerAs: 'vm'
        })
        .when("/events/list/registered",
        {
            // for user to list Events s/he has Registered For
            templateUrl: "/ngApp/Views/EventsListRegistered.html",
            controller: "eventListRegisteredController",
            controllerAs: 'vm'
        })
        .when("/nonprofit/:nonprofitid/event/:eventId/register",
        {
            // for user to list Events
            templateUrl: "/ngApp/Views/EventRegistration.html",
            controller: "eventRegisterController",
            controllerAs: 'vm'
        })
        //.when("/event/detail/:id",
        //{
        //    // for user to list Events
        //    templateUrl: "/ngApp/Views/EventDetail.html",
        //    controller: "eventViewController",
        //    controllerAs: 'vm'
        //})
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


// defined filters
app.filter('isAfter', function () {
    return function (meals, dateAfter) {
        // Using ES6 filter method
        return meals.filter(function (meal) {
            return moment(meal.mealDate).isAfter(dateAfter);
        });
    };
});


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