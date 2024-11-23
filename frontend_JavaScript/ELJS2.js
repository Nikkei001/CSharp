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
