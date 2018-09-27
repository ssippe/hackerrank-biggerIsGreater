'use strict';

// // const fs = require('fs');

// // process.stdin.resume();
// // process.stdin.setEncoding('utf-8');

// let inputString = '';
// let currentLine = 0;

// process.stdin.on('data', inputStdin => {
//     inputString += inputStdin;
// });

// process.stdin.on('end', _ => {
//     inputString = inputString.replace(/\s*$/, '')
//         .split('\n')
//         .map(str => str.replace(/\s*$/, ''));

//     main();
// });

// function readLine() {
//     return inputString[currentLine++];
// }



const sortStr = (w) => w.split('').sort().join("");
const dropChar = (s, i) => s.substring(0, i) + s.substring(i + 1, s.length);
const addOne = (score) => {
    const len = score.length;
    let addOk = false;
    for (var i = len - 2; i >= 0; i--) {
        if (!addOk) {
            const iTotal = score[i] + 1;
            const iMax = len - 1 - i;
            if (iTotal <= iMax) {
                score[i] = iTotal;
                return score;
            }
            else {
                score[i] = 0;
            }
        }
    }
    if (!addOk)
        return null;

    return score;
}


const getStringN = (w, idx) => {
    let wRem = sortStr(w);
    let result = "";
    while (wRem.length > 0) {
        let i = idx[result.length];
        result += wRem[i];
        wRem = dropChar(wRem, i);
    }
    return result;
}

const getScore = (w) => {
    let wOrdered = sortStr(w);
    let score = [];
    for (let i = 0; i < w.length; i++) {
        const n = w.Length - 1 - i;
        let idx = wOrdered.lastIndexOf(w[i]);
        score.push(idx);
        wOrdered = dropChar(wOrdered, idx);

        //var iScore = GetScoreHelper(idx, n);
        //Debug.WriteLine(iScore);
        //sum += iScore;
    }
    return score;
}




// Complete the biggerIsGreater function below.
function biggerIsGreater(w) {
    let score = getScore(w);
    let next = w;
    while (true) {
        
        
        score = addOne(score);
        if (score === null)
            return "no answer";
        next = getStringN(next, score);
        // console.log('biggerIsGreater next='+next);
        const same = w===next;
        console.log('biggerIsGreater score=' + JSON.stringify(score) + ' next=' + next + ' w='+w + ' same='+ same);
        if (next !== w)
            return next;
    }
}

const add = (a, b) => a + b;

module.exports = {
    add,
    getStringN,
    dropChar,
    sortStr,
    getScore,
    addOne,
    biggerIsGreater
};