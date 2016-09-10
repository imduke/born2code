var Calculator = function(start){
var vm = this;

this.add = function(x){
start = start + x;
return vm;
};

this.multiply = function(y){
start = start * y;
return vm;
};

this.equal = function(callback){
callback(start);
return vm;
};

} 

new Calculator(10).add(2).add(3).multiply(2).equal(function(result){
    console.log(result);
});