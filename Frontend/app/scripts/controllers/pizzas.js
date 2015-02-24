'use strict';

/**
 * @ngdoc function
 * @name frontendApp.controller:PizzasCtrl
 * @description
 * # PizzasCtrl
 * Controller of the frontendApp
 */
angular.module('frontendApp')
  .controller('PizzasCtrl', function ($scope) {
    $scope.awesomeThings = [
      'HTML5 Boilerplate',
      'AngularJS',
      'Karma'
    ];
  });
