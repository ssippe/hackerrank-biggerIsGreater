const biggerIsGreater = require('./biggerIsGreater');


test('biggerIsGreater', () => {    
  fullRun("abbb");    
});

const fullRun = (s) => {
  let res = s;
  console.log(res);
  for(let i=0;i<20; i++){
    res = biggerIsGreater.biggerIsGreater(res);    
    //console.log("fullRun="+res);
  }  
}
