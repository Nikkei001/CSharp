- 安装node,下载x64版本  
- 在vscode终端打开cmd终端,node -v,npm -v检查是否安装  
- 安装vite并创建ts模版,npm create vite@latest typescript -- --template vanilla-ts  
- 项目中各个文件的大致作用.以下是这个项目中各文件作用的通俗解释：  
	- public 文件夹  
		 - vite.svg：是放在 `public` 文件夹下的一个图标文件 ，一般用于存放一些不会被构建工具处理，可直接通过相对路径引用的静态资源，比如网站的 logo 等，在网页中可直接访问调用。  
	- src 文件夹  
		 - counter.ts：用 TypeScript 编写的文件 ，很可能定义了一个计数器相关的功能模块，比如实现计数逻辑、相关操作函数等，具体功能要看里面代码实现。  
		 - main.ts ：项目的主入口文件 ，一般在这里进行项目初始化，引入各种模块，挂载应用到页面等关键操作，像前端项目里可能会在这里初始化 Vue、React 等框架实例。  
		 - style.css ：用来编写项目样式的文件 ，通过 CSS 规则定义网页元素的外观，比如颜色、大小、布局等。  
		 - typescript.svg ：是 `src` 目录下的一个图标文件 ，可能是项目中某个功能模块相关的展示图标，会在构建过程中被处理后引用到网页中。  
		 - vite-env.d.ts ：TypeScript 的类型声明文件 ，用于为 Vite 相关环境变量声明类型，帮助 TypeScript 编译器识别和检查代码中 Vite 环境变量使用的类型是否正确。  
	- 根目录文件  
		 - .gitignore ：告诉 Git 哪些文件和文件夹不需要被跟踪 ，比如项目运行产生的临时文件、构建产物等，防止把不必要的文件提交到代码仓库。  
		 - index.html ：网页的基础框架文件 ，定义了网页的结构，包含网页标题、引入外部资源（如 JavaScript、CSS 文件）等，是浏览器加载项目的起始点。  
		 - package.json ：项目的配置文件 ，记录了项目的基本信息，比如名称、版本，还定义了项目依赖的包、运行脚本（如启动、构建命令）等，`npm` 或 `yarn` 等包管理器会依据它来管理项目依赖和执行相关命令。  
		 - tsconfig.json ：TypeScript 编译器的配置文件 ，用来指定 TypeScript 编译的各项选项，比如编译目标、是否严格检查类型、哪些文件要编译、哪些要排除等。  
- 进入到typescript文件夹也就是项目文件夹,打开终端,运行npm install安装dependencies.  
- 运行npm run dev命令启动项目,package.json中有一个dev脚本--在scripts中,这就是我们刚执行的命令  
- npm run build会编译我们写的ts/tsx代码,编译后的js文件存放在新出现的dist文件夹中  
- npm run dev,即使代码中有类型错误,项目也不会报错,改用npm run build则会报错  
- 运行npm run dev后,需要按ctrl+c终止当前运行的进程,然后再输入npm run build则会观察到报错 
- npm run dev是本地运行,npm run build是部署项目  
- union type, literal value type, 陌生的是literal value type, 也就是字面量值类型, 可以和union type结合, 例如
```typescript
let status: "success" | "failed";  
status = "random" // 报错  
```

- 关于typescript的静态类型检查  
```typescript
const books = ['1984', 'Brave New World', 'Fahrenheit 451'];  
  
let foundbook:string;  
for (const book of books) {  
    if (book === "1984") {  
         foundbook = book;  
         break;  
    }  
}  
  
console.log(foundbook.length); 
```
类型系统只知道 books 是一个 string[] 类型的数组，而不知道数组里具体包含哪些字符串。当你在循环中查找 "1984" 时，类型系统也无法确定 foundbook 是否会被赋值。因此，在调用 foundbook.length 时，类型系统可能会提示 foundbook 可能为 undefined 的错误，因为它无法保证 foundbook 一定被赋值。  
所以需要改写如下 
```typescript
const books = ['1984', 'Brave New World', 'Fahrenheit 451'];  
  
let foundbook:string | undefined;  
for (const book of books) {  
    if (book === "1984") {  
         foundbook = book;  
         break;  
    }  
}  
  
console.log(foundbook?.length);  
```
其中?.的意思是如果左边是null/undefined则会直接返回这两个,如果不是则可以当做不存在  
??会把null/undefined替换成默认值
- object:
	- 基本格式: const obj : {name: string; age: number};
	- 可选参数: const obj : {name: string; age?: number};
		- ?:代表某个属性是可选的
		- 可选属性的type必须和定义的type一致
	- 只读属性: const obj : {name: string; readonly age: number};
		- 属性前加上readonly代表初始化object之后该属性不能被修改
- array
	- 基本格式: let testarr: string\[\];
	- union types let testarr: (string | number)\[\];
	- 元素为对象的数组 let testarr: {name: string; age?: number}\[\];
- functions
	- 参数: 如果不指定参数的types会报错,建议定义函数时指定参数的类型,不要设置any或者更改tsconfig的设置
	- 返回值(return)

