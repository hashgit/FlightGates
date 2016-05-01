var app = angular.module("FlightGatesApp", ["ngRoute"]);

app.config(["$routeProvider",
  function ($routeProvider) {
      $routeProvider.
        when("/gates", {
            templateUrl: "template/gatelist",
            controller: "GateListCtrl"
        }).
        when("/gates/:gateId", {
            templateUrl: "template/gatedetail",
            controller: "GateDetailCtrl"
        }).
        when("/flights/:flightId", {
            templateUrl: "template/flightdetail",
            controller: "FlightCtrl"
        }).
        when("/flights/create/:gateId", {
            templateUrl: "template/flightcreate",
            controller: "FlightCreateCtrl"
        }).
        otherwise({
            redirectTo: "/gates"
        });
  }]);