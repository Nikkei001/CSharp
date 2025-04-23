# 变量,数据类型,运算符,表达式

## 数字,文字,字符串(相对熟悉)

## 加减乘除等运算符(相对熟悉)

换行符"\n"

## 列表, 字典

### 基本用法
python中的列表和字典对应之前学过的数组和对象

字典中\[\]和.的区别: \[\]访问字典中的值,如果键不存在会报错,更推荐get()方法;"."只能调用字典本身的方法或属性,无法访问字典的键值对

示例代码:
```python
# 创建一个购物清单程序，用列表记录商品，字典记录价格。
goods = ["apple","banana","orange"]
prices = {"apple":5,"banana":3,"orange":4}

print("购物清单:")

for item in goods:
    print("- ", item, ": ", "￥", prices[item])
    
print("总价: ", sum(prices.values()), "元")

# 点号"."获取字典的所有keys
for item in prices.keys():
    print(item)
```
输出结果:
购物清单:
-  apple :  ￥ 5
-  banana :  ￥ 3
-  orange :  ￥ 4
总价:  12 元
apple
banana
orange

### 列表推导式(语法糖)

基本语法如下：
```
new_list = [expression for item in iterable if condition]
new_list = [expression for item1 in iterable1 for item2 in iterable2 ... if condition]
```
示例代码:
```python
# 创建一个包含 0 到 9 的列表
fromZeroToNine = [number for number in range(10)]
print(fromZeroToNine)

# 创建一个包含 0 到 9 的平方的列表
squareOfZeroToNine = [number**2 for number in range(10)]
print(squareOfZeroToNine)

# 创建一个包含 0 到 9 的偶数的列表
evenNumbersOfZeroToNine = [number for number in range(10) if number % 2 == 0]
print(evenNumbersOfZeroToNine)
```
输出
```
[0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
[0, 1, 4, 9, 16, 25, 36, 49, 64, 81]
[0, 2, 4, 6, 8]
```

### 字典合并

在 Python 中，\*\* 是解包操作符，用于将字典中的键值对展开。

## python中的类(class)

class是object的"蓝图",定义了object的属性和方法
```python
class Person:
	species = "human"
    def __init__(self, name, age):
        self.name = name
        self.age = age
	def printAge(self):
		print(self.age)

class Athlete(Person):
	def printAge(self):
		print(str(self.age) + "athlete")
	def running(self):
		print("run")

p = Person("Alice", 25)
print(p.name)  # 输出: Alice
print(p.age)   # 输出: 25
p.printAge()   # 输出: 25
Person.printAge(p) # 输出: 25
print(Person.printAge) # 返回函数本身
print(Person.species) # 输出: human
print(p.species) # 输出: human
a = Athlete("Bill", 25)
a.running()
a.printAge() # 多态
```
class包含一个构造函数,**构造函数用于初始化对象的属性,在创建实例时会自动调用**,`__init__`,在构造函数中,`self`代表当前的示例

实例方法是定义在class中的函数,用于访问实例的**属性和方法**
注意p.printAge()和Person.printAge(p)是等价的

类属性是**类的所有实例共享的属性**，而实例属性是每个实例独有的属性。

继承允许一个类（子类）继承另一个类（父类）的属性和方法。子类可以重写父类的方法.
多态指的是不同类的对象可以对同一方法做出不同的响应.

还没提及的内容: class的封装









# 流程控制

# 异常处理

try, except,else,finally

# 函数

## 普通函数

## 装饰器(decorator)

**装饰器就像一个“外挂”**，可以给现有的函数添加额外的功能，但**不修改原函数的代码**。
**装饰器本质是一个函数**，它接受一个函数作为参数，返回一个新的函数。
**@语法糖**：`@name` 放在函数定义前，自动应用装饰器。
**用途**：添加日志、计时、权限检查、缓存等，无需修改原函数！

### 不带参数的装饰器
举个例子
```python
def addWrappers(func):
    def wrapper():
        print("---begin---")
        func()
        print("---end---")
    return wrapper
    
@addWrappers
def sayHello():
    print("hello")
sayHello()
```
**`@add_before_after` 等价于：`say_hello = add_before_after(say_hello)`**  （把原函数 `say_hello` 传给装饰器，装饰器返回一个新函数 `wrapper`）
当你调用 `say_hello()` 时，实际执行的是 `wrapper()`，它包裹了原函数的功能。

### 带参数的装饰器

**带参数的装饰器**：先根据参数定制一个装饰器，再用它装饰函数。
举个例子
```python
def repeat(times):
    def decorator(func):
        def wrapper():
            for i in range(times):
                func()
        return wrapper
    return decorator
  
@repeat(5)
def sayHola():
    print("hola")
sayHola()
```
分步解释
1. **`repeat(3)` 的分解**：
    - 调用 `repeat(3)` → 返回 `decorator` 函数（此时 `times=3` 已被固定）
    - 然后对 `say_hello` 应用装饰器：`decorator(say_hello)` → 返回 `wrapper` 函数
2. **调用 `say_hello()` 时**：
    - 实际执行的是 `wrapper()` 函数，循环 3 次调用原函数

# 文件操作

with语句常用于文件操作,确保文件操作中即使发生异常也可以正确关闭
open()函数由三个参数
基本语法
```
file_object = open(file_name, mode, encoding=None)
```
file_name:文件名称,可以是相对路径或者绝对路径
mode:打开文件的模式,常用的"r"是只读模式,"w"是写入模式
# 模块,包

基本用法
from 模块名 import 对象名1, 对象名2,... as 别名
**模块名**：要导入的模块的名称。
**对象名**：模块中定义的函数、类或变量的名称。
# 面向对象

# 常用标准库

# 虚拟环境

## 理由

在 Python 项目中创建 **虚拟环境（Virtual Environment）** 是开发的最佳实践，主要原因如下：

---
### **1. 隔离项目依赖**

- **避免版本冲突**：不同项目可能依赖同一库的不同版本（如 `numpy 1.20` vs `numpy 2.0`）。虚拟环境为每个项目创建独立的空间，防止全局安装的包互相干扰。
    
- **保持系统环境干净**：不污染操作系统全局的 Python 环境，避免因安装/卸载包导致系统工具或脚本崩溃。
---
### **2. 便于依赖管理**

- **精确控制依赖**：通过 `pip freeze > requirements.txt` 生成项目依赖清单，其他人可通过 `pip install -r requirements.txt` 一键复现环境。
    
- **协作开发**：确保团队成员、生产服务器和开发机使用完全一致的依赖版本，避免“在我机器上能跑”的问题。
---
### **3. 支持多项目并行开发**

- 如果你同时开发多个项目（如一个用 Django 3.0，另一个用 Django 4.0），虚拟环境能隔离它们的依赖，避免手动切换版本的麻烦。
---
### **4. 安全性和权限控制**

- **避免权限问题**：在虚拟环境中安装包不需要管理员权限（如 `sudo`），降低误操作系统级文件的风险。
- **实验性尝试**：可随意安装测试新库，即使导致环境损坏，删除虚拟环境重建即可，不影响其他项目。
---
### **5. 标准化工具链**

- 虚拟环境是 Python 生态的标准实践，配合工具如：
    - `venv`（Python 3 内置）
    - `virtualenv`（更灵活的第三方工具）
    - `pipenv`（整合虚拟环境和依赖管理）
    - `poetry`（现代依赖与包管理工具）
---
### **🌰 举个栗子**

假设你的电脑全局安装了 Python 3.8：
1. **项目A**：需要 `requests 2.25`
2. **项目B**：需要 `requests 3.0`（不兼容旧版）
如果直接在全局安装，两个项目无法共存。通过虚拟环境，每个项目独立安装自己所需的版本。


## 安装方法

cd到项目所在的文件夹,例如
```
cd fastAPI_Practice
```
然后在terminal输入
```
python -m venv .venv
```
文件夹下面会多出来几个子文件夹

cd到.venv文件夹下,然后激活虚拟环境(选择**powershell终端**)
```
Scripts\Activate.ps1
```
文档中写的`source .venv/Scripts/activate`中的source其实就是当前所在的虚拟环境的目录,只需要粘贴source后面的部分
**需要注意每次打开一个新的terminal都要输入激活命令**
```
Scripts\Activate.ps1
```
官方建议每次安装新的package时都输入一遍激活命令,防止package安装到全局,从而出现不兼容的问题.

检查虚拟环境是否激活
```
Get-Command python
```
如果source的路径是当前虚拟环境的路径,则说明激活成功
terminal显示了(.venv)也说明激活成功

升级pip
```
python -m pip install --upgrade pip
```
更改.gitignore文件
```
echo "*" > .gitignore
```
echo "\*"：将在终端中 "打印 "文本 （下一部分会稍作修改）
">"左边的命令打印到终端的任何内容都不应打印，而应写入">"右边的文件中。
.gitignore：应写入文本的文件名
对于 Git 来说，"\*"意味着 "一切"。 因此，它会忽略 .venv 目录中的所有内容。

该命令将创建一个 .gitignore 文件，内容如下
```
*
```

重要:为什么我运行了echo "\*" > .venv/.gitignore之后,还是.venv文件夹下的很多文件还是显示untracked状态
.gitignore文件应该放在根目录下面,而不是.venv文件夹内部

`python -m venv .venv`的解释以及主要文件和文件夹的解释

<blockquote style="background-color: #fff8dc; padding: 15px; border-radius: 5px; border-left: 4px solid #ffd700;"> 
python => 调用python解释器<br>
-m => 是 Python 的模块运行标志，表示运行指定的模块<br>
venv => 是 Python 的一个内置模块，用于创建虚拟环境<br>
.venv => 这是要创建的虚拟环境的目录名称。你可以将其替换为任何你想要的名称<br>
当你运行 `python -m venv .venv` 时，Python 会创建一个名为 `.venv` 的目录，并在其中设置一个独立的 Python 运行环境。这个环境与全局的 Python 环境隔离，允许你为不同的项目安装特定的依赖包，而不会互相干扰。<br><br>

创建虚拟环境后，`.venv` 目录中会包含以下主要文件和文件夹：<br>
- bin（在 Windows 上是 `Scripts`）：包含激活环境的脚本和可执行文件。<br>
- lib：包含虚拟环境的 Python 解释器和标准库。<br>
- include：包含 Python 头文件。<br>
- pyvenv.cfg：配置文件，存储虚拟环境的配置信息。
</blockquote>


