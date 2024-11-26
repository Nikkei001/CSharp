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

函数的结构
```js
function name(params) {
	// 函数体
};
```
函数由keyword `function`开头

函数可以有一个或多个参数(*parameters*), 也可以没有参数

参数后面跟着函数体(*body*), 包含了函数被调用后会执行的statements, 就算只有一个statements, body也必须用{}封装

函数可以把名字写在前面或者后面

函数有return语句时可以返回一个值, 当return后面什么都没有会返回`undefined`, 完全没有return语句会返回一个副作用, 例如console.log(...), 也会返回`undefined`

传入函数的parameters就像常规的bindings, 但是他们的值是函数的调用者给定的, 而不是函数本身包含的代码给定的

### 绑定和作用域(bindings and scopes)






