// 计算2的十次方
let result = 1;
let count = 0;

while (count < 10) {
    result = result * 2;
    count = count + 1
}

console.log(result) // => 1024

// 计算2的十次方
let result2 = 1;
for (let count = 0; count < 10; count++) {
    result2 = result2 * 2
}

console.log(result2) 

// 输出20之后第一个可以被7整除的数
for (let test = 20; ; test++) {
    if (test % 7 === 0) {
        console.log(test)
        break
    }
}
// => 21

// 输出1到4的序列,跳过2
for (let num = 1; num <= 4; num++) {
    if (num === 2) continue
    console.log(num)
} // => 1, 3, 4

console.log("=".repeat(100));

// 画一个三角形
// my solution
let string = "#"
for (let count = 0; count <= 7; count++) {
    console.log(string.repeat(count));
}
// my solution2
for (let string2 = "#"; string2.length <= 7; string2 = string2 + "#") {
    console.log(string2);
}
// 作者的解法string2 += "#"

console.log("=".repeat(100));

// 打印1到100, 能被3整除的数字打印"Fizz",能被5整除的数字打印"Buzz"
// 打印1到100, 能同时被3和5整除的数字打印"FizzBuzz"
for (let index2 = 1; index2 <= 100; index2++) {
    if (index2 % 3 === 0 && index2 % 5 === 0) console.log("FizzBuzz")
    else if (index2 % 3 === 0) console.log("Fizz")
    else if (index2 % 5 === 0) console.log("Buzz")
    else console.log(index2)
}

console.log("=".repeat(100));

// 作者的解法(借鉴学习)
for (let n = 1; n <= 100; n++) {
    let output = "";
    if (n % 3 == 0) output += "Fizz";
    if (n % 5 == 0) output += "Buzz";
    console.log(output || n);
}

console.log("=".repeat(100));

// 输出一个棋盘,找规律,注意运算符的优先级
for (let row = 1; row <= 8; row++) {
    let rst = "";
    for (let column = 1; column <= 8; column++) {
        if ((row + column) % 2 === 0) rst += " ";
        if ((row + column) % 2 !== 0) rst += "#";
    }
    rst += "\n";
    console.log(rst);
}





