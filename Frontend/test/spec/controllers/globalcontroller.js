'use strict';

describe('Controller: GlobalcontrollerCtrl', function () {

  // load the controller's module
  beforeEach(module('frontendApp'));

  var GlobalcontrollerCtrl,
    scope;

  // Initialize the controller and a mock scope
  beforeEach(inject(function ($controller, $rootScope) {
    scope = $rootScope.$new();
    GlobalcontrollerCtrl = $controller('GlobalcontrollerCtrl', {
      $scope: scope
    });
  }));

  //it('should attach a list of awesomeThings to the scope', function () {
  //  expect(scope.awesomeThings.length).toBe(3);
  //});
});
