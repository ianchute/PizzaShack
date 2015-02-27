'use strict';

describe('Controller: FlavorsCtrl', function () {

  // load the controller's module
  beforeEach(module('frontendApp'));

  var FlavorsCtrl,
    scope;

  // Initialize the controller and a mock scope
  beforeEach(inject(function ($controller, $rootScope) {
    scope = $rootScope.$new();
    FlavorsCtrl = $controller('FlavorsCtrl', {
      $scope: scope
    });
  }));

  //it('should attach a list of awesomeThings to the scope', function () {
  //  expect(scope.awesomeThings.length).toBe(3);
  //});
});
