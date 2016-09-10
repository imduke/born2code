function defer() {
    var deferred = {};
    var promise = new Promise(function(resolve, reject) {
        deferred.resolve = resolve;
        deferred.reject  = reject;
    });
    deferred.promise = promise;
    return deferred;
}

//get all deferred objects
var deferred0 = defer();
var deferred1 = defer();
var deferred2 = defer();
var deferred3 = defer();

//get all promise objects
var promise0 = deferred0.promise;
var promise1 = deferred1.promise;
var promise2 = deferred2.promise;
var promise3 = deferred3.promise;


//attach then to individual promises and Promise.all
Promise.all([promise0,promise1,promise2,promise3]).then(function(data){
console.log('ALL RETURNED');
});
deferred0.promise.then(function(data) {
    console.log('deferred 0 resolved ' + data);
});
deferred1.promise.then(function(data) {
    console.log('deferred 1 resolved ' + data);
});
deferred2.promise.then(function(data) {
    console.log('deferred 2 resolved ' + data);
});
deferred3.promise.then(function(data) {
    console.log('deferred 3 resolved ' + data);
});


console.log('defer created');

setTimeout(function() {
    deferred0.resolve(123);
}, 1000);
setTimeout(function() {
    deferred1.resolve(234);
}, 2000);
setTimeout(function() {
    deferred2.resolve(456);
}, 3000);
setTimeout(function() {
    deferred3.resolve(789);
}, 10000);