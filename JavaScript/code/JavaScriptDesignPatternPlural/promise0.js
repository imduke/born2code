function defer() {
    var deferred = {};
    var promise = new Promise(function(resolve, reject) {
        deferred.resolve = resolve;
        deferred.reject  = reject;
    });
    deferred.promise = promise;
    return deferred;
}

var deferred = defer();

deferred.promise.then(function(data) {
    console.log('After deferred.resolved invoking then of promise with resolved data ' + data);
});

console.log('defer created');

setTimeout(function() {
    deferred.resolve(123);
}, 5000);

//in console
//var deferred = {};
//var promise = new Promise(function(resolve,reject){deferred.resolve = resolve; deferred.reject = reject; }));
//deferred.promise = promise;
//promise.then(function(result){console.log(result*100)});
//deferred.resolve(10);