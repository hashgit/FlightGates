app.controller("GateListCtrl", ["$scope", "$http",
  function ($scope, $http) {
      $http.get("api/gates").success(function (data) {
          $scope.Gates = data;
      });
  }]);
