'use strict';

describe('Service: FlavorRepository', function () {

  // load the service's module
  beforeEach(module('frontendApp'));

  // instantiate service
  var FlavorRepository;
  beforeEach(inject(function (_FlavorRepository_) {
    FlavorRepository = _FlavorRepository_;
  }));

  it('should do something', function () {
    expect(!!FlavorRepository).toBe(true);
  });

});
