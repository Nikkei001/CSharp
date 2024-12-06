// 编写一个min函数, 接受两个参数并返回最小值
function min(num1, num2) {
    if (num1 <= num2) {
        return num1
    } else {
        return num2
    }
}

console.log(min(1, 3));
console.log(min("string", 2));

// 递归
// 有一种方法判断一个正数是奇数还是偶数
// 0是偶数, 1是奇数, 对于其他数N, 它的奇偶性和N-2一致
// 定义一个函数isEven, 接受一个正整数, 返回TRUE或FALSE
// 例如console.log(isEven(50)); 返回TRUE
// 需要考虑负数
function isEven(positiveNum) {
    if (positiveNum < 0) return isEven(-positiveNum);
    else if (positiveNum = 0) return true;
    else if (positiveNum = 1) return false;
    else return isEven(positiveNum - 2);
}

console.log(isEven(55));
console.log(isEven(-99));

// 写两个函数 
// countB, 要求输入一个字符串计算大写B的个数
// countChar, 要求输入一个字符串和一个字符, 计算给定字符的个数

function countChar(string, stringToFind) {
    const stringLen = string.length;
    let count = 0;
    for (let index = 0; index <= string.length - 1; index++) {
        if (string[index] === stringToFind) count++;
    }
    return count;
}
// 以下是作者的解法, 减小重复
function countBs(string) {
    return countChar(string, "B")
}

console.log(countBs("BOB"));
console.log(countChar("kakkerlak", "k"));






