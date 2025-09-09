# 基础语法--变量运算符控制流等

## 变量

一个变量声明后就不再改变,是一个好的习惯,在函数式编程语言中常见

# 函数

# 对象

# 数组

# 异步

# DOM(最重要)

- window
	- 是JavaScript的全局对象
	- 代表浏览器窗口
- DOM
	- 每个 HTML 标签、标签内文本均为对象，这些对象构成树状结构（DOM 树），可通过 JavaScript 访问和修改
		- element nodes
		- text nodes
		- etc
	- 获取DOM对象
		- html,body,head标签对应document.documentElement,document.body,document.head
			- 注意document.body可能是null
		- child nodes,descendants
			- document.body.childNodes
			- elem.firstChild,elem.lastChild,elem.hasChildNodes()
			- childNodes是一个collection而不是一个array,通常用for...of遍历,不能使用数组方法,它是只读且动态的
		- siblings,parents
			- elem.previousSibling,elem.nextSibling
			- elem.parentNode
		- element-only navigation(IM)
			- children
			- firstElementChild,lastElementChild
			- previousElementSibling,nextElementSibling
			- parentElement
		- table(IM)
			- `table.rows` – the collection of `<tr>` elements of the table.
			- `table.caption/tHead/tFoot` – references to elements `<caption>`, `<thead>`, `<tfoot>`
			- `table.tBodies` – the collection of `<tbody>` elements
			- 常见API
				- thead/tfoot/tbody.rows
				- tr.cells.sectionRowIndex/.rowIndex
				- td/th.cellIndex
	
	- 操作DOM对象
		- 查找元素
			- querySelectorAll--可以使用css选择器(见[[html,css#css选择器]])
			- querySelector--速度更快
			- elem.matches(css)--返回true或者false
			- elem.closest
			- elemA.contains(elemB)
			- getElementById
			- getElement==s==By*
				- 返回实时的元素集合,文档发生变化自动更新
				- 适用于老代码


# 模块化



