var Book = function(name, price){
var priceChanging = [];
var priceChanged = [];

this.name = function(val){
return name;
};

this.price = function(val){
    if (val !== undefined && val !== price){
        for (var i =0; i<priceChanging.length; i++){
            if (!priceChanging[i](this, val)){
                return price;
            }
        }
        price = val;
        for (var i = 0; i < priceChanged.length; i++){
            priceChanged[i](this);
        }
    }
return price;
};

this.onPriceChanging = function(callback){
    priceChanging.push(callback);
};

this.onPriceChanged = function(callback){
    priceChanged.push(callback);
};

};

book = new Book('Born to code in C',100);
console.log('Book Name: ' + book.name());
console.log('Book Price: ' + book.price());

book.onPriceChanging(function(b, price){
if (price>100){
    console.log('price gone unexpectedly high');
    return false;
}else{
    return true;
}
});
book.onPriceChanged(function(b){
console.log('price successfully changed to ' + b.price());
});

//book.price(50);
book.price(150);