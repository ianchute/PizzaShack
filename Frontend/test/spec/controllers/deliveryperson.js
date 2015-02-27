'use strict';

describe('Controller: DeliverypersonCtrl', function () {

  // load the controller's module
  beforeEach(module('frontendApp'));

  var DeliverypersonCtrl,
    scope;

  // Initialize the controller and a mock scope
  beforeEach(inject(function ($controller, $rootScope) {
    scope = $rootScope.$new();
    DeliverypersonCtrl = $controller('DeliverypersonCtrl', {
      $scope: scope
    });
  }));

  //it('should attach a list of awesomeThings to the scope', function () {
  //  expect(scope.awesomeThings.length).toBe(3);
  //});
});
