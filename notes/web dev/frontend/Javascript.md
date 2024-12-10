基于Eloquent JavaScript(ELJS)
基于ModernJavaScript(MJS)

## additional tips

### vscode

#### 在vscode运行js代码(涉及CD命令)

1. 安装nodejs
2. 打开vscode终端,用`npm -v`和`node -v`检查是否安装成功,如果vscode已经是打开状态,关掉重启再运行
3. 终端运行`cd + 代码所在的路径`
	1. cd --help查看帮助
	2. cd或cd ~ 快速返回到用户的主目录
	3. cd ..  当前目录的父目录(转到上一级目录)
		- 例如
		- `PS D:\Users\Administrator\Desktop\csharp\frontend_JavaScript> cd ..
		- `PS D:\Users\Administrator\Desktop\csharp`
	4. cd .  表示当前目录(可以转到下一级目录)
		- 例如下面的示例转到下一级目录并运行js代码
		- `PS D:\Users\Administrator\Desktop\csharp> cd ./frontend_JavaScript`
		- `PS D:\Users\Administrator\Desktop\csharp\frontend_JavaScript> node ELJS1.JS`
	5. 使用绝对路径和相对路径
		- `cd D:\Users\Administrator\Desktop\csharp\first`(绝对路径)
		- `D:\Users\Administrator\Desktop\csharp\first> cd ..`(返回上一级目录)
		- `D:\Users\Administrator\Desktop\csharp> cd first`(相对路径)
		- `D:\Users\Administrator\Desktop\csharp\first> `(转到了下一级目录)

#### 在vscode中相同变量的后面插入光标

方法1 Ctrl + Shift + L, 可以一次性选中代码中所有相同的变量并插入光标

方法2 Ctrl + D, 可以逐个选择相同的变量并插入光标

#### 在vscode中注释/取消注释多行代码

快捷键Ctrl + /

# 作者的建议和书的架构(ELJS)

尽量不要只浏览示例，而是要认真阅读并理解它们。 在真正写出可行的解决方案之前，不要认为自己已经理解了它们。 我建议你在实际的 JavaScript 解释器中尝试练习的解决方案。 这样，你就能立即得到反馈，知道你所做的是否有效，而且我希望，你会有尝试和超越练习的欲望。 在浏览器中阅读本书时，你可以通过点击所有示例程序来编辑（和运行）它们。 
本书大致包括三个部分。 前 12 章讨论 JavaScript 语言。 接下来的七章介绍网络浏览器以及 JavaScript 的编程方式。 最后，有两章专门介绍 Node.js，这是另一种 JavaScript 编程环境。
书中有五个项目章节描述了较大的示例程序，让你体验实际编程的乐趣。
本书的语言部分从介绍 JavaScript 语言基本结构的四章开始。 这四章讨论了控制结构（例如你在介绍中看到的 while 字）、函数（编写自己的构建模块）和数据结构。 学完这些内容后，你就能编写基本程序了。
接下来，第 5 章和第 6 章将介绍使用函数和对象编写更抽象代码并控制复杂性的技巧。 在第一个项目章节构建了一个简陋的送货机器人后，本书的语言部分将继续进行，并将介绍错误处理和错误修复、正则表达式（处理文本的重要工具）、模块化（防止复杂性的另一种方法）和异步编程（处理需要时间的事件）等章节。 在第二个项目章节中，我们实现了一种编程语言，为本书的第一部分画上了句号。
本书的第二部分，即第13章至第19章，介绍了浏览器JavaScript可以使用的工具。 你将学习在屏幕上显示内容（第14章和第17章）、响应用户输入（第15章）以及通过网络通信（第18章）。 本部分还有两个项目章节：构建一个平台游戏和一个像素绘画程序。 
第 20 章介绍了 Node.js，第 21 章使用该工具构建了一个小网站。
# language

### 值,类型,运算符

*bit*:通常是0或1,任何离散信息都可以简化为bit表示
处理计算器中的大量bit时,将它们分离成代表信息片段的块。在 JavaScript 环境中，*这些块被称为值value*,不同的值有不同的类型
要创建一个值,只需要调用它的名称,值被存储在某个地方,不用的时候值会消失,留下的bit供其他值使用

#### 数字(Number)

数字使用了64bit来存储,可以表示一个很大的范围,包括负数,小数,可以用科学计数法表示很大的数字(例如2.998e10,2.998乘以10的10次方).
因为只有固定的bit用来存储数字,所以一些数字会失去精确度,需要注意小数只是近似值,其计算并不精确
和数字有关的主要是算术运算,算数运算符有优先级,可以通过括号改变优先级
注意取余数的运算符是"%",英文为*remainder*或*modulo*
在 JavaScript 中，有三个特殊值被视为数字，但它们的行为与普通数字不同。 
前两个是 Infinity 和 -Infinity，分别代表正无穷大和负无穷大。 无穷大-1 仍然是无穷大，以此类推。 不过，不要太相信基于无穷大的计算。 这在数学上并不靠谱，而且很快就会产生下一个特殊数字： NaN。 
NaN 表示 "不是一个数"，尽管它是一个数字类型的值。 例如，当你尝试计算 0 / 0（零除以零）、Infinity - Infinity 或任何其他不产生有意义结果的数值运算时，都会得到这样的结果。

#### 字符串(Strings)

字符串也是由bit构建的,每个字符都有对应的数字,字符串是一串数字,每个字符串元素使用16bit,部分字符串占据了两个字符位置
字符串可以用\`\`, \"\", \'\'包围
使用单引号或双引号编写的字符串的行为非常相似，唯一的区别在于需要在字符串内部转义哪种类型的引号。 反引号字符串(也就是"\`...\`",通常称为*template literals*,模板字面量), 除了可以跨行外，它们还可以嵌入其他值。${}内部的代码将会被计算并转为字符串,例如
\`half of 100 is ${100 / 2}\` 结果是"half of 100 is 50"
转义字符"\\"表示该字符串有特殊含义,适用于引号中带有引号的情况
转义字符"\t"代表制表符,"\n"代表换行
字符串不能做算数运算,+运算符用于连接字符串

#### 运算符

有些运算符使用单词表示, 例如`typeof`,这是一个一元运算符(*unary operators*), 它只对一个值计算, 而一般的运算符对两个值计算, 是二元运算符(*binary operators*),"-"可以是一元或二元运算符

#### 布尔值(boolean)

只有两个值, TRUE和FALSE

产生布尔值的方法
可以通过比较两个值产生布尔值,例如
`console.log("Aardvark" < "Zoroaster")`
常见的比较运算符<,>,>=,<=,!=,==

逻辑运算符产生布尔值,and(&&) or(||) not(!)等等
优先级
||<&&<比较运算符<其他

<blockquote style="background-color: #fff8dc; padding: 15px; border-radius: 5px; border-left: 4px solid #ffd700;"> 
||的表达式可以转换为true时,返回左边的表达式,否则返回右边的表达式<br>
&&的表达式可以转换为false时,返回左边的表达式,否则返回右边的表达式<br>
&&和||运算符只有在必要的时候才会返回右侧的表达式, 例如true || X永远只返回true<br>
操作符"?? "类似于"||"，但只有当左边的值为 null 或undefined时，才会返回右边的值，而不会返回可转换为 false 的其他值。 通常，这比 || 的行为更可取。<br><br>
这种特性被称为短路(short-circuiting)
</blockquote>

有一种三元运算符也返回布尔值
格式:condition ? expression1 : expression2
condition为真则返回expression1否则返回expression2
这是唯一的三元运算符,也叫条件运算符(_conditional operator_)

#### 空值

语言中的许多操作都不会产生有意义的值，产生未定义的值只是因为它们必须产生一些值。
有`null`和`undefined`两个值, 用来表示没有有意义的值。 它们本身也是值，但不携带任何信息。
`undefined`和`null`之间的含义差异是JavaScript设计中的一个意外，在大多数情况下**并不重要**。 如果您确实需要使用这些值，我建议您将它们视为可以互换的值。

#### 类型隐式转换

当两个值相同时，你应该得到 true,**NaN 的情况除外**, 两边是null或者undefined其中之一时也是true。 但当类型不同时，JavaScript 会使用一套复杂而混乱的规则来决定如何处理。

使用"\=\=\="和"!\=\="两个运算符比较两边的值不会进行自动转换, 是作者推荐的用法

#### 逻辑运算符的短路(short-circuiting)

见[[Javascript#language#布尔值(boolean)]]

#### 总结

在本章中，我们介绍了 JavaScript 值的四种类型：数字、字符串、布尔值和未定义值。 这些值通过输入名称（true、null）或值（13、"abc"）来创建。 您可以使用运算符组合和转换值。 我们看到了用于算术（+、-、\*、/ 和 %）、字符串连接（+）、比较（\=\=、!=、\=\=\=、!\=\=、<、>、<=、>=）和逻辑（&&、||、??）的二元运算符，以及几个一元运算符（- 用于否定数字，!用于逻辑否定，typeof 用于查找值的类型）和一个三元运算符（?:），用于根据第三个值从两个值中选择一个。 这些信息足以让你将 JavaScript 用作一个袖珍计算器，但也仅此而已。 下一章将开始把这些表达式整合到基本程序中。

## 程序的结构(program structure)

#### expressions and statements

表达式: 产生一个值(*value*)的代码片段就是expression
例如
单独的字面量的值(value that is written literally): 22, "hello"
括号内的值: (2 + 3)
含有binary operators的expression, binary operators运用于两个expression: 2 + 3
含有unary operators的expression, unary operators运用于一个expression: x++, -5
以上的示例表明, expression中可以嵌套expression以构建任意复杂计算的expression

expression相当于一个句子的**片段**, 而statements相当于**完整的句子**
最简单的statements是一个expression，后面有一个分号。 这就是一个程序：
1;
!false;
这种程序运行后不会有任何事情发生, 但有些statements可能会在屏幕上产生内容, 或者改变机器的状态从而影响后面的statements, 这种变化被称为副作用(*side effects*).
作者建议每个statements结尾都带上分号( ; ), 因为省略分号的规则很复杂, 容易出错

### 绑定, 也叫变量(bindings)

*binding*是为了获取和保存值的, 其中, *keyword*表示将要定义一个binding, 后面跟一个binding的名称, 如果想要赋值, 可以加上"=", 和一个expression

`let caught = 5 * 5;`
`console.log(caught * caught);`

`let`是keyword, caught是binding的名称, 它获取和保存了5\*5的结果
定义binding后, binding的名称可以用作expression

<blockquote style="background-color: #fff8dc; padding: 15px; border-radius: 5px; border-left: 4px solid #ffd700;"> 
binding的名称如果含有多个单词,一般第一个单词全小写,后面的单词第一个字母大写,构造函数除外,构造函数第一个单词的首字母要大写, 示例: let littleKidsAge = 10;
</blockquote>

当binding指向一个值时，并不意味着它永远与该值绑定。 在任何时候都可以对现有binding使用 = 操作符，使其与当前值断开连接，并指向一个新值：
`let caught = 10`
`console.log(caught * caught);`

你应该把binding想象成触角而不是盒子。 它们**不包含值，而是掌握值**--**两个绑定可以引用同一个值**。 **程序只能访问它仍然拥有引用的值**。 当你需要记住某个东西时，你要么长出一根触角来抓住它，要么将你现有的一根触角重新连接到它上面。

当你尝试获取一个empty binding的值, 你会得到`undefined`
`let empty;`
`console.log(empty);`

定义多个值用逗号隔开
`let a = 1, b = 2;`

`var`和`const`和`let`有相似的功能
`var`不常用
`const`代表constant binding, 只要它存在就一直指向同一个值, 这种绑定非常有用

bindings的名称可以是一个或多个字符的序列, 可以包含$和\_,不能用数字开头, 还有一些keywords和一些保留字也不能用(reserved for use)

不需要刻意记这些关键词和保留字, 可以报错之后检查
清单:
`break case catch class const continue debugger default`
`delete do else enum export extends false finally for`
`function if implements import interface in instanceof let`
`new package private protected public return static super`
`switch this throw true try typeof var void while with yield`

### 环境(environment)

在给定时间内存在的bindings及其值的集合称为*environment*。 程序启动时，环境并不是空的。 它总是包含作为语言标准一部分的bindings，而且大多数情况下，它还包含提供与周围系统交互方式的bindings。 例如，在浏览器中，就有与当前加载的网站交互以及读取鼠标和键盘输入的func-tions。

默认环境中提供的很多值都属于函数(*functions*)类型。 函数是用值封装的一段程序(A function is a piece of program wrapped in a value.)。 可以应用这些值来运行被包装的程序(Such values can be _applied_ in order to run the wrapped program.)。 例如，在浏览器环境中，binding `prompt`持有一个function，用于显示一个要求用户输入的小对话框。

执行函数(Executing functions)称为*invoking*, *calling*, or *applying* it。 在产生函数值的表达式后面加上括号，就可以调用函数。 通常，您会直接使用保存了functions的bindings的名称。 括号之间的值将提供给函数内部的程序。 在示例中，提示函数使用我们给它的字符串作为对话框中要显示的文本。 提供给函数的值称为参数(*arguments*)。 不同的functions可能需要不同数量或不同类型的arguments。

console.log在浏览器中的output lands输出结果, F12调出, 它不是一个binding而是一个expression, 这个expression从console binding中获取log property.

functions可以写入对话框,可以写入文本,这是一种副作用
functions也可以产生值,我们称为*return* that value,这种情况下没有副作用函数也是有用的,产生了值的function是expression,说明它可以包含在更大的expression中.([[Javascript#控制流(program structure)#expressions and statements]])

### 控制流(control flow)

程序包含多条statements时,会从上到下顺序执行

#### 条件语句(conditional execution)

结构
`if (条件) {...}`
`if (条件) {...} else {...}`
`if (条件) {...} else if (条件2) {...} ... else if (条件n) {...} else {...}`

{}一般情况会带上, 除非只有一行的statements
{}和()可以把任意数量的statements组合成一个statement, 这称为块(*block*)
()内的条件为true则执行后面的{...}中的block

示例
```JavaScript
let num = Number(prompt("Pick a number"));

if (num < 10) {
  console.log("Small");
} else if (num < 100) {
  console.log("Medium");
} else {
  console.log("Large");
}
```
程序首先会检查 num 是否小于 10。 如果小于 10，程序会选择该分支，显示 "Small"，然后结束。 如果不小于 10，程序会选择 else 分支，该分支本身包含第二个 if。 如果第二个条件（< 100）成立，说明数字至少是 10，但小于 100，则显示 "Medium"。 如果不成立，则选择第二个也是最后一个 else 分支。

#### while和do循环

结构
`while (条件) {...}`
()内的条件为true则执行后面的block, ()中的变量会不断变化并传入{...}中

`do {...} while (条件)`
do...while...语句至少会执行一次, 在第一次执行之后才会开始判断条件是否成立

while和计数器组合是一种常见的做法, 计数器通常从0开始

#### for循环

结构
`for () {...}`
for循环相当于while和计数器的组合,后面的()**必须**包含两个分号 
第一个分号前的部分initialize循环，通常是通过定义一个binding。 第二部分是**检查**循环是否必须继续的表达式。 最后一部分在每次迭代后**更新**循环的状态。 在大多数情况下，这比 while 结构更简洁明了。

示例 计算2的10次方
```js
// 计算2的十次方

let result = 1;
let count = 0;

while (count < 10) {
    result = result * 2;
    count = count + 1
}

console.log(result)

// 计算2的十次方
let result2 = 1;

for (let count = 0; count < 10; count++) {
    result2 = result2 * 2
}

console.log(result2)
```

#### break和continue语句

break语句通常用于终止循环, 它会跳出循环执行后面的代码, 类似"查找**第一个**可以整除7的数"这样的问题可以使用break

continue语句, 在需要跳过某些条件的时候可以使用

```js
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
```

<blockquote style="background-color: #fff8dc; padding: 15px; border-radius: 5px; border-left: 4px solid #ffd700;"> 
counter = counter + 1;<br>
可以写作counter += 1<br>
类似的还有-=,*=等等<br>
counter += 1,counter -= 1<br>
也可以写作counter++, counter-- 
</blockquote>

#### switch语句

结构
`switch () {case ...: ...break;...default:...break;}`
注意不要漏掉break

示例
```js
switch (prompt("What is the weather like?")) {
  case "rainy":
    console.log("Remember to bring an umbrella.");
    break;
  case "sunny":
    console.log("Dress lightly.");
  default:
    console.log("Unknown weather type!");
    break;
}
```

#### 注释

单行注释 
//

多行注释
\/\* 
多行内容
\*\/

### 总结

现在你已经知道，程序是由statements构成的，而statements本身有时又包含更多的statements。 statements往往包含expressions，而expressions本身又可以由更小的expressions组成。 

将statements一个接一个地放在一起，就可以得到一个从上到下执行的程序。 通过使用条件语句（if、else 和 switch）和循环语句（while、do 和 for），可以在控制流中引入干扰。 

bindings可用于将数据片段归档到一个名称下，并可用于跟踪程序中的状态。 environment是bindings定义的集合(The environment is the set of bindings that are defined. )。 JavaScript 系统总是会在环境中加入一些有用的standard bindings。 

functions是封装程序的特殊值。(Functions are special values that encapsulate a piece of program. ) 您可以通过编写 functionName(argument1, argument2) 来调用它们。这种函数调用是一种expression，可以产生一个值。

## 函数

函数的目的是给子程序(subprograms)一个名称, 并使这些子程序相互隔离

### 定义函数

#### 传统的定义形式

函数的结构
```js

const functionName = function(x) {
	// 函数体
};
```
函数的bindings**通常**只是作为程序特定部分的名称。 这种bindings只定义一次，永不更改。 这就很容易混淆函数及其bindings。

但两者是不同的。 函数值(function value)可以做其他值可以做的所有事情--你可以在任意expressions中使用它，而不仅仅是调用它。 你可以将function value存储在一个新的binding，也可以将它作为参数传递给函数，等等。 后续我们将讨论通过将function value传递给其他函数可以实现的有趣功能。

同样，保存函数的binding仍然只是一个普通的binding，如果不是常量，也可以赋予一个新值.

函数由keyword `function`开头

函数可以有一个或多个参数(*parameters*), 也可以没有参数, 传入函数的parameters就像常规的bindings, 但是他们的值是函数的调用者给定的, 而不是函数本身包含的代码给定的, **在JS中, 给函数传入的参数过多, 则多出来的参数会被忽略, 如果传入的参数过少, 缺失的参数会设定为`undefined`**

参数可以是可选的, 称为可选参数(*optional arguments*), 格式如下
`function name(parameter, parameter2 = expression) {...}` 
可选参数在没有给定值的时候, `=`后面的expression将取代参数

==下一章将介绍一种方法，让函数体可以获取传递给它的整个参数列表。 这种方法非常有用，因为它允许函数接受任意数量的参数。 例如，console.log 就能做到这一点==

参数后面跟着函数体(*body*), 包含了函数被调用后会执行的statements, 就算只有一个statements, body也必须用{}封装

函数有return语句时可以返回一个值, 当return后面什么都没有会返回`undefined`, 完全没有return语句会返回一个副作用, 例如console.log(...), 也会返回`undefined`

#### 函数声明(function declaration)

函数的定义还有另一种略微简短的形式,被称作函数声明(_function declaration_), 形式如下

```js
function square(x) {
	return x * x;
}
```
这种function declaration不需要在后面加分号

function declaration**不属于常规的从上到下的控制流**。 从概念上讲，它们被移到其scope([[Javascript#函数#变量和作用域(bindings and scopes)]])的顶部，并可被该scope中的所有代码使用。 这种方法有时很有用，因为它提供了一种自由，可以按照看起来最清晰的方式排列代码，而不必担心在使用之前必须定义所有函数。

```js
console.log("The future says:", future());

let future = function() {
    return "You'll never have flying cars";
};
// => 报错:ReferenceError: Cannot access 'future' before initialization
```
上面的代码改为function declaration则可以运行
```js
console.log("The future says:", future());

function future() {
  return "You'll never have flying cars";
}
// => The future says: You'll never have flying cars
```
#### 箭头函数(arrow functions)

还有一种定义函数的方式叫箭头函数(*arrow functions*)

```js
let functionName = (parameters) => {
	// function body
}
```
当只有一个参数名时，可以省略参数列表周围的括号。参数列表也可以是空的; 

如果函数体是一个单独的表达式，而不是括号中的代码块，函数将返回该表达式。 因此，这两个 `square` 的函数定义做的是同一件事：

```js
const square1 = (x) => { return x * x; };
const square2 = x => x * x;
```


### 变量和作用域(bindings and scopes)

每个bindings都有作用域(scope), scope就是bindings在程序中可见的部分, 对于定义在**函数, blocks, modules**之外的bindings, 可以在程序的任何地方被引用, 这些变量称作全局(global)变量

为函数参数创建的bindings或在函数内部声明的bindings只能在该函数中引用，因此被称为local bindings。 **每次调用函数时，都会创建这些bindings的新instances**。 这就在一定程度上实现了函数之间的隔离.

<blockquote style="background-color: #fff8dc; padding: 15px; border-radius: 5px; border-left: 4px solid #ffd700;"> 
<b>每次调用函数都会创建新的instances, 意味着每次函数执行时，它的参数和内部变量都是全新的，与上一次函数调用时创建的变量是不同的。这也意味着函数内部的变量不会保留上一次函数调用的状态</b>
</blockquote>

使用 `let` 和 `const` 声明的bindings实际上是声明**所在代码块**的local bindings，因此如果在循环中创建bindings，循环前后的代码都无法 "看到 "它。 在 **2015 年以前**的 JavaScript 中，**只有函数**才会创建新的作用域，因此使用 `var` 关键字创建的旧式bindings在其所在的整个函数中都是可见的，如果不在函数中，则在global scope中也是可见的。

```js
const halve = function(n) {
  return n / 2;
};

let n = 10;
console.log(halve(100));
// → 50
console.log(n);
// → 10
```
每个作用域都可以 "向外看 "其周围的作用域, 如果多个绑定具有相同的名称, 只能看到最内层的绑定。 例如，当函数 halve 内的代码引用 n 时，它看到的是自己的 n，而不是全局的 n。

JavaScript 不仅区分global bindings和local bindings。 块和函数可以在其他块和函数内部创建，从而产生多种程度的locality。

当存在嵌套的scope时, 内层的scope可以读取到外层scope中的bindings, 反过来则不成立.

```js
const hummus = function(factor) {
    const ingredient = function(amount, unit, name) {
      let ingredientAmount = amount * factor;
      if (ingredientAmount > 1) {
        unit += "s";
      }
      console.log(`${ingredientAmount} ${unit} ${name}`);
    };
    ingredient(1, "can", "chickpeas");
};

hummus(2) // => 2 cans chickpeas

// factor可以被内层函数ingredient读取到
```

程序块内可见的bindings集合由程序块在程序文本中的位置决定。 每个local scope都能看到包含它的所有local scope，而所有scope都能看到glocal scope。 这种binding visibility的方法称为词法作用域(*lexical scoping*)。

### 调用栈(call stack)

计算机存储调用发生时的上下文的地方就是调用栈(*call stack*)。 每次调用函数时，当前上下文都会存储在栈顶部。 函数返回时，会移除顶部上下文，并使用该上下文继续执行。

存储这个栈需要计算机内存的空间。 如果栈过大，计算机就会出现 "栈空间不足 "或 "递归过多 "等故障信息。

示例
```js
function greet(who) {
    console.log("Hello " + who);
}
greet("Harry");
console.log("Bye");
```
该程序的运行过程大致如下：调用 greet 会导致控制权**跳转**到该函数的起始行（第 2 行）。 该函数调用 console.log，console.log 接管控制，执行其工作，然后将控制**返回到第 2 行**。 在那里，它到达了 greet 函数的末尾，**因此返回到调用它的地方--第 4 行**。 之后的一行再次调用 console.log。 返回后，程序结束。

执行过程可以表示为
not in function
  in greet
    in console.log
  in greet
not in function
  in console.log
not in function

### 闭包(closure)

将函数视为值的功能，加上每次调用函数时都会重新创建local bindings这一事实([[Javascript#变量和作用域(bindings and scopes)]])，带来了一个有趣的问题： 当创建local bindings的函数调用不再有效时，local binding会发生什么变化？

local bindings在每次调用中都是重新创建的，不同的调用不会影响彼此的local bindings。 **这种**引用外层作用域中local bindings的特定实例的**特性被称为closure**。 引用了周围local scopes中的bindings的**函数**称为**a closure**。 这种行为不仅让你不必担心绑定的生命周期，还能以一些创造性的方式使用函数值。

示例
```js
function multiplier(factor) {
	return number => number * factor;
}

let twoTimes = multiplier(2);
let threeTimes = multiplier(3); // 重新创建了factor, 并被内层的number函数引用

console.log(twoTimes(5)); // => 10
console.log(threeTimes(5)); // => 15
```
这样思考程序需要一些练习。 一个好的思维模型是将函数值视为两部分, 一是**函数体中的代码**,二是**创建函数的环境**。 **调用函数时，函数体看到的是创建它的环境**，而不是调用它的环境。

在上面的示例中，调用 multiplier 时会创建一个环境，其中的factor参数绑定为 2和3。 它返回的函数值存储在 twoTimes和threeTimes 中，会记住这个环境，因此调用时会将其参数分别乘以 2和3。

### 递归(recursion)

调用自身的函数称为递归函数.

通常递归比循环慢, 但递归并不总是循环的低效替代品。 有些问题用递归确实比用循环更容易解决。 **这些问题通常需要探索或处理多个 "分支"，而每个 "分支 "又可能分支出更多的 "分支"**。

### Growing Functions--删除代码中的冗余部分

将函数引入程序或多或少有两种自然的方式。

第一种是当你发现自己**多次编写类似的代码**时。 你最好不要这样做，因为代码越多，就意味着隐藏错误的空间越大，也意味着人们在理解程序时需要阅读的材料越多。 因此，**你要把重复的功能，为它找到一个好名字，并把它放到一个函数中**。

第二种方法是，你发现自己需要一些尚未编写的功能，而这些功能听起来应该有自己的函数。 您可以先命名该函数，然后编写其主体。 甚至可能在真正定义函数之前，就已经开始编写使用函数的代码了。

为一个函数找到一个好名字有多难，就能很好地说明你要包装的概念有多清晰。 让我们来看一个例子。

### 函数和副作用(side effects), 以及纯函数(pure functions)

函数可大致分为为副作用而调用的函数和为返回值而调用的函数（当然也可能既有副作用又有返回值）。

与直接执行副作用的函数相比，创建值的函数更容易以新的方式进行组合。

纯函数(*pure functions*)是一种特殊的产生值的函数，**它不仅没有副作用，也不依赖于其他代码的副作用**，例如，它不会读取其值可能会改变的global bindings。 

纯函数有一个令人愉快的特性，即当**调用相同的参数时，它总是产生相同的值**（**而不做其他任何事情**）。 **对这种函数的调用可以用它的返回值来代替，而不会改变代码的含义。** 

当您不确定纯函数是否正常工作时，您可以通过**简单地调用该函数**来测试它，并知道如果它在该上下文中正常工作，那么它在任何上下文中都会正常工作。 非纯函数往往需要更多的脚手架来测试。

示例

纯函数的示例
```js
// 纯函数 
function add(a, b) { 
	return a + b; // 返回值仅依赖于输入参数 
} 

console.log(add(2, 3)); // 输出: 5 
console.log(add(2, 3)); // 输出: 5（相同输入，始终相同输出）
```

非纯函数的示例
```js
let counter = 0; // 不纯函数 

function incrementCounter() { 
	counter += 1; // 修改了外部变量 counter 
	return counter; 
} 

console.log(incrementCounter()); // 输出: 1 
console.log(incrementCounter()); // 输出: 2（每次调用结果不同）
```

## 数组和对象

### 数组

数组是JS用来存储数值序列的数据类型, 其写法是在方括号之间用逗号分隔的值列表。数组的索引从0开始, 用方括号包含索引.

### 属性(properties)

几乎所有 JavaScript 值都有属性。 空值和未定义值是例外。 如果尝试访问这些非值的属性，就会出现错误.

例如, `Math.max`访问的是Math对象的max属性

JavaScript 中访问属性的两种主要方式是使用点和方括号。 value.x 和 value[x] 都可以访问 value 上的一个属性，但不一定是同一个属性。 两者的区别在于如何解释 x。 使用圆点时，圆点后的单词就是属性的字面名称。 使用方括号时，将对括号之间的表达式进行求值，以获得属性名称。 value.x 获取名为 "x "的值的属性，而 value[x] 则**获取名为 x 的变量的值，并将其转换为字符串**作为属性名。

如果你知道你感兴趣的属性叫做 color，你就会说 value.color。 如果要提取由绑定 i 中的值命名的属性，则需要输入 value[i]。 属性名是字符串。 它们可以是任何字符串，但点符号只适用于看起来像有效绑定名称的名称--以字母或下划线开头，且只包含字母、数字和下划线。 如果要访问名为 2 或 John Doe 的属性，必须使用方括号：value[2] 或 value["John Doe"]。

数组中的元素存储为数组的属性，**使用数字作为属性名**。 **由于数字不能使用点符号，而且通常要使用保存索引的绑定，因此必须使用括号符号来获取它们**。

就像字符串一样，数组也有一个`length`属性，告诉我们数组有多少个元素。

### 方法(methods)

除了长度属性外，字符串和数组值都包含一些存放**函数值**的属性。
```js
let doh = "Doh";
console.log(typeof doh.toUpperCase);
// → function
console.log(doh.toUpperCase());
// → DOH
```
例如, 每个字符串都有一个 toUpperCase 属性。 调用时，它会返回一个字符串副本，其中所有字母都已转换为大写。 还有一种 toLowerCase 方法是相反的。
有趣的是，尽管对 toUpperCase 的调用没有传递任何参数，但函数以某种方式访问了字符串 "Doh"，也就是我们调用的属性值。 ==我们将在第 6 章中了解其工作原理。==

**包含函数的属性**一般称为其所属值的方法，如 "`toUpperCase` 是字符串的一个方法"。

数组也有自己的一些方法, 如push, pop等等, 这些是对栈(stack)的常规操作

### 对象(objects)

对象类型的值是属性的任意集合。 创建对象的一种方法是使用大括号作为表达式。

在大括号内，你可以写出一个用逗号分隔的属性列表。 每个属性都有一个名称，后面跟一个冒号和一个值。 当一个对象被写成多行时，按本示例所示缩进有助于提高可读性。 **名称不是有效变量名称或有效数字的属性必须加引号**:

示例
```js
let descriptions = {
  work: "Went to work",
  "touched tree": "Touched a tree"
};
```
这意味着大括号在 JavaScript 中具有两种含义。 在statements的开头，它们表示一个语句块(block of statements)的开始。 在任何其他位置，它们都描述一个对象。 

<blockquote style="background-color: #fff8dc; padding: 15px; border-radius: 5px; border-left: 4px solid #ffd700;"> 
大括号作为代码块的开始,例如if语句、for循环、while循环等结构中
</blockquote>

幸运的是，在statements的开头使用括号(braces)中的对象很少有用，因此这两者之间的歧义问题不大。 **唯一会出现这种情况的是**，当您想从一个arrow function中返回一个对象时--您不能写 n => {prop: n}，**因为大括号会被解释为函数体**。 **相反，你必须在对象周围加上一组圆括号(parenthesis)，以明确它是一个表达式**。

读取不存在的属性会得到`undefined`。

可以使用 = 运算符为属性表达式(property expression)赋值。 如果属性值已经存在，则将替换该属性值；如果不存在，则在对象上创建一个新属性。

对象的属性掌握值, 但其他变量和属性可以有相同的值, **可以把对象想象成一条章鱼, 每根触手上都写着属性的名字**

`delete`是一个一元操作符, 可以从对象中删除属性, 不常用, 格式`delete anObject.aProperty;`这种操作相当于切断了章鱼的触手

`in`可以判断一个属性是否存在于对象, 如果一个属性被设置为`undefined`, 返回`true`, 如果属性被删除, 返回`false`, 这是未定义和不存在的区别

示例
```js
let anObject = {left: 1, right: 2};
console.log(anObject.left);
// → 1
delete anObject.left;
console.log(anObject.left);
// → undefined
console.log("left" in anObject);
// → false
console.log("right" in anObject);
// → true
```
要想知道一个对象有哪些属性，可以使用 Object.keys 函数。 给该函数一个对象，它将返回一个字符串数组--对象的属性名称：
```js
console.log(Object.keys({x: 0, y: 0, z: 2}));
// → ["x", "y", "z"]
```
有一个 Object.assign 函数可以将一个对象的所有属性复制到另一个对象中：
```js
let objectA = {a: 1, b: 2};
Object.assign(objectA, {b: 3, c: 4});
console.log(objectA);
// → {a: 1, b: 3, c: 4}
```
因此，**数组只是一种专门用于存储事物序列的特化的对象(Arrays, then, are just a kind of object specialized for storing sequences of things.)**。 如果对 typeof [] 进行运算，就会产生 "对象"。 你可以把数组想象成扁长的章鱼，所有触角都整齐地排成一行，并用数字标注。

在实际开发中, 对象数组, 也就是数组的元素全部是对象的数组, 是很常见的
例如 商品列表
```js
const products = [
    { id: 101, name: "Product A", price: 19.99, stock: 100 },
    { id: 102, name: "Product B", price: 29.99, stock: 50 },
    // 更多商品...
];
```

### 可变性

我们看到，对象的值是可以修改的。 **前面几章讨论过的数值类型，如数字、字符串和布尔值，都是不可变的--不可能改变这些类型的值**。 你可以将它们组合起来并从中导出新的值，但当你取一个特定的字符串值时，该值将始终保持不变。 字符串中的文本无法更改。 如果您有一个包含 "cat "的字符串，其他代码就不可能更改字符串中的某个字符，使其拼写成 "rat"。

对象的工作方式不同。 您可以更改它们的属性，从而使一个对象的值在不同时间具有不同的内容。

当我们有 120 和 120 这两个数字时，无论它们是否指向相同的物理位，我们都可以认为它们是相同的数字。 对于对象，有两个对同一对象的引用(two references to the same object)与有两个包含相同属性的不同对象(two different objects that contain the same properties)是不同的。 请看下面的代码：

```js
let object1 = {value: 10};
let object2 = object1;
let object3 = {value: 10};

console.log(object1 == object2);
// → true
console.log(object1 == object3);
// → false

object1.value = 15;
console.log(object2.value);
// → 15
console.log(object3.value);
// → 10
```
object1 和 object2 这两个bindings掌握的是同一个对象，这就是为什么改变 object1 也会改变 object2 的值。 可以说它们具有相同的身份。 binding object3 指向一个不同的对象，它最初包含与对象 1 相同的属性，但过着独立的生活。

binding也可以是可变的或恒定的，但这与其值的行为方式是分开的。 尽管数字值不会改变，但您可以使用 let 绑定通过改变绑定所指向的值来跟踪不断变化的数字。 同样，虽然对象的 const 绑定本身不会改变，并且会继续指向同一个对象，但**该对象的内容可能会改变**。

```js
const score = {visitors: 0, home: 0};
// This is okay
score.visitors = 1;
// This isn't allowed
score = {visitors: 1, home: 1};
```
使用 JavaScript 的 == 运算符比较对象时，它是通过同一性进行比较的：只有当两个对象的值完全相同时，它才会返回 true。 比较不同的对象将返回 false，即使它们具有相同的属性。 JavaScript 中并没有内置按内容比较对象的 "深度 "比较操作，但可以自己编写。








