# 安装FastAPI

先安装虚拟环境, 详见[[01-PythonBasics#虚拟环境#安装方法]]

然后安装fastapi

## 第一步

```python
from fastapi import FastAPI

app = FastAPI()


@app.get("/")
async def root():
    return {"message": "Hello World"}
```
在终端运行如下代码
```
fastapi dev main.py
```
运行之后, 获取到服务器和服务器文档(api docs)的url

Server started at http://127.0.0.1:8000  
Documentation at http://127.0.0.1:8000/docs

其中
http://127.0.0.1:8000
127.0.0.1是一个特殊的 IP 地址，被称为**回环地址**，主要用于本地机器的自我通信
8000是**端口号**,在这个网址中，表示对应的服务（比如一个 Web 应用程序）运行在本地计算机的 8000 端口上
对于一个网页来说，它的 “**服务**” 是指网页背后的服务器软件和相关程序所提供的功能和操作

api docs的主要作用:直接测试示例中的`/`接口，点击 "Try it out" 即可发送请求并查看响应结果，无需额外编写测试代码。

api docs会包含一个curl命令,curl命令的主要作用是: 测试API,构造复杂请求,不需要编写代码,以及展示请求的细节,例如请求头中的`Content-Type`、`Authorization`等参数

在fastapi中,所有生成的API都遵循openapi的标准(openapi schema),这其中包括了JSON schema

openapi的schema使用一个JSON文档来表示,地址如下
http://127.0.0.1:8000/openapi.json

所有交互式API文档都是基于openapi

对上面代码的分析
第一步
`from fastapi import FastAPI`
FastAPI是一个class,继承自`Starlette`

第二步
`app = FastAPI()`
构建了FastAPI的一个实例,名为app

https://example.com/items/foo
这是一个url,从第一个`/`开始后边的部分,也就是`/items/foo`
通常叫做path,或者endpoint,以及`route`
文档中用operation代表HTTP method,例如POST/GET/PUT/DELETE/OPTIONS/HEAD/TRACE/PATCH,前四个较常用,分别代表,create/read/update/delete data.
在 HTTP 协议中，你可以使用这些“method”中的一种（或多种）与每个path进行通信。

第三步 定义一个path operation decorator
@app.get("/")告知FastAPI，紧挨着它下面的函数负责处理以下请求： 
路径/ 
使用get操作
在 Python 中，@something语法被称为“decorator”。 你把它放在一个函数的上方。
一个“decorator”接收下面的函数并对其进行一些处理。 在我们的例子中，这个装饰器告诉FastAPI，下面的函数对应于path "/"，并带有一个operation "get"。 它是“path operation decorator”。
get可以替换成其他的operation,例如post,put等等
通俗地说,**当有客户端通过 HTTP 的 GET 方法访问根路径`/`（也就是网站的主页）时，要执行下面定义的函数(被装饰器装饰过的函数)**。

第四步 















# Python类型系统

## 痛点

给变量加上类型后,可以获得智能补全和错误检查
## 基本语法,类型声明

示例
```python
def get_full_name(first_name: str, last_name: str):

    full_name = first_name.title() + " " + last_name.title()

    return full_name
```
在变量后面加冒号, 后面跟上types

### 简单类型

常见的简单type有`str`, `int`, `float`, `bool`, `bytes`

### generic types, type parameters

 有些数据类型的结构中包含了其他值, 且这些值有自己的types, 例如字典, 元组, 列表(`dict`, `list`, `set`, `tuple`)等, 这种类型称为generic types.

新版本的python不需要写`from typing import List`

示例
- list\[str\]
- tuple\[int, int, str\]
- set\[bytes\]
- dict\[str, float\]
- int | str
- str | None = None

### 作为类型的类(classes as types)

类也可以作为类型, 此时传入的参数代表这个类的一个实例

```python
class Person:
    def __init__(self, name: str):
        self.name = name


def get_person_name(one_person: Person):
    return one_person.name
```

fastapi使用 做类型检查, 输入的类型不对会自行纠正




# http协议,请求处理

**HTTP** 是浏览器和服务器之间沟通的“语言”。比如当你在浏览器输入网址，浏览器会发送一个 **HTTP 请求** 到服务器，服务器处理后会返回一个 **HTTP 响应**。

**路由**：定义 URL 路径和代码的对应关系（比如 `/user` 对应显示用户信息）。
**控制器**：处理请求的函数（比如读取数据库并返回结果）。