'use strict';

describe('Controller: BaseDataCtrl', function () {

  // load the controller's module
  beforeEach(module('frontendApp'));

  var BasedataCtrl,
    scope;

  // Initialize the controller and a mock scope
  beforeEach(inject(function ($controller, $rootScope) {
    scope = $rootScope.$new();
    BasedataCtrl = $controller('BaseDataCtrl', {
      $scope: scope
    });
  }));

  it('should attach a list of awesomeThings to the scope', function () {
    expect(scope.awesomeThings.length).toBe(3);
  });
});
