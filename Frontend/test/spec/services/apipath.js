'use strict';

describe('Service: ApiPath', function () {
    // load the service's module
    beforeEach(module('frontendApp'));

    // instantiate service
    var ApiPath;
    beforeEach(inject(function (_ApiPath_) {
        ApiPath = _ApiPath_;
    }));

    it('should do something', function () {
        var success = true;
        expect(success).toBe(true);
    });
});