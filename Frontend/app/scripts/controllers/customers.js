'use strict';

/**
 * @ngdoc function
 * @name frontendApp.controller:CustomersCtrl
 * @description
 * # CustomersCtrl
 * Controller of the frontendApp
 */
angular.module('frontendApp')
  .controller('CustomersCtrl', function ($scope) {
    $scope.awesomeThings = [
      'HTML5 Boilerplate',
      'AngularJS',
      'Karma'
    ];
  });
