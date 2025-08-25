# 基础

## 重点理解的概念

### event listener
```草稿
event对象
- event.target - 触发事件的元素（这里是输入框）
- event.type - 事件类型（这里是"input"）
- event.timeStamp - 事件发生的时间
event handler method
```




### web components基础

### 装饰器



### 类

## 定义一个component

### 基本格式

```typescript
import {LitElement, html} from 'lit';
import {customElement} from 'lit/decorators.js';

@customElement('my-element') 
export class MyElement extends LitElement {
	render() {
		return html `
			
		`
	}
}
```
- `LitElement`: Lit框架的基础类，用于创建Web Components
- `html`: 模板字面量函数，用于定义HTML模板
- `customElement` : 装饰器，用于注册自定义元素
	- `@customElement('my-element')`: 将这个类注册为名为my-element的自定义HTML元素

### 响应式属性以及模板

```typescript
import {LitElement, html} from 'lit';
import {customElement, property} from 'lit/decorators.js';

@customElement('my-element')
export class MyElement extends LitElement {
// TODO: Add a reactive property
  @property()
  message: string = 'Hello again.';
  render() {
    return html`
      <p>${this.message}</p>
    `;
  }
}
```

- `@property`: 装饰器，用于**定义响应式属性**,需要和**模板字符串**配合使用,此处是在MyElement class中定义了一个名为message的属性,类型为字符串
- 响应式属性: 当`message`属性值改变时,**组件会自动重新渲染(调用render方法)**
- `${}` 是模板字符串的插值语法,`${this.message}` 引用组件的 `message` 属性,叫做**数据绑定**
- 可以通过HTML属性设置这个值,设置之后会覆盖掉ts文件中定义的值,例如`<my-element message = "gagaga"></my-element>`,这时网页会显示`gagaga`而不是之前定义的`Hello again`

### 声明式事件监听器

```typescript
import {LitElement, html} from 'lit';
import {customElement, property} from 'lit/decorators.js';


@customElement('my-element') export class MyElement extends LitElement {  
  @property()
  name: string = "Nikkei";

  render() {  
    return html`    
      <h1>Hello world! From ${this.name}.</h1>
      <input placeholder="Enter your name" @input=${this.changeName}>
    `;
  }

  changeName(event: Event) {
    const input = event.target as HTMLInputElement;
    this.name = input.value;
  }
}
```
- `@input=${this.changeName}` - 关键部分 ：绑定输入事件,`@input` 是Lit框架的事件监听语法，监听 input 事件
	- 相当于原生js的`inputElement.addEventListener('input', this.changeName.bind(this))`
	- **input 事件在用户每次输入字符时都会触发**
	- `${this.changeName}` 指定当事件发生时要调用的方法,也就是**event handler method**
	- 当你在输入框中输入任何字符时：
		- 浏览器检测到输入 - 每次键盘输入、粘贴、删除都会触发
		- 浏览器**创建Event对象** - 包含所有事件相关信息
		- 浏览器**调用事件监听器** - **自动执行**绑定的函数
- changeName(event: Event) 接收一个Event类型的参数，包含事件的所有信息
- 获取输入元素 ： `const input = event.target as HTMLInputElement`;  `event.target` 是触发事件的元素（即那个输入框）,  `as HTMLInputElement` 是TypeScript类型断言，告诉编译器这是一个输入框元素
- lit检测到`this.name`改变了,调用`render`方法重新渲染component,因为name是一个响应式属性

### Lit中的表达式(expressions)

- Lit 模板可以包含称为表达式的动态值。表达式可以是任何 JavaScript 表达式。
- 表达式一般用在五个常见的地方
	- child nodes
	- attributes
	- boolean attributes
	- property
	- event listener
- 示例如下
```html
<!-- Child nodes -->
<h1>${this.pageTitle}</h1>

<!-- Attribute -->
<div class=${this.myTheme}></div>

<!-- Boolean attribute -->
<p ?hidden=${this.isHidden}>I may be in hiding.</p>

<!-- Property -->
<input .value=${this.value}>

<!-- Event listener -->
<button @click=${() => {console.log("You clicked a button.")}}>...</button>
```
- 解释boolean attributes
	- 当右边的值为true, 添加这个属性
	- 否则移除这个属性

示例代码
```typescript
import {LitElement, html} from 'lit';
import {customElement, property} from 'lit/decorators.js';

@customElement('my-element') 
export class MyElement extends LitElement {  
  @property()
  name: string = "Nikkei";
  
  @property({ type: Boolean })
  checked: boolean = false;

  render() {  
    return html`    
      <h1>Hello world! From ${this.name}.</h1>
      
      <!-- 添加复选框来控制输入框状态 -->
      <label>
        <input 
          type="checkbox" 
          .checked=${this.checked}
          @change=${this.toggleChecked}>
        启用输入框
      </label>
      
      <br><br>
      
      <input 
        placeholder="Enter your name" 
        @input=${this.changeName}
        ?disabled=${!this.checked}>
    `;
  }

  changeName(event: Event) {
    const input = event.target as HTMLInputElement;
    this.name = input.value;
  }
  
  toggleChecked(event: Event) {
    const checkbox = event.target as HTMLInputElement;
    this.checked = checkbox.checked;
  }
}
```
这段代码的运行过程
```
1. 用户点击复选框
   ↓
2. 浏览器切换复选框状态 (unchecked → checked)
   ↓
3. 浏览器触发 'change' 事件
   ↓
4. 浏览器调用：toggleChecked(eventObject)
   ↓
5. toggleChecked方法执行：
   - const checkbox = event.target  // 获取复选框元素
   - this.checked = checkbox.checked  // 读取新状态并更新组件属性
   ↓
6. Lit检测到 this.checked 属性变化
   ↓
7. Lit触发重新渲染
   ↓
8. 模板重新执行：.checked=${this.checked}
   ↓
9. 其他依赖 this.checked 的元素也会更新
   (比如 ?disabled=${!this.checked} 的输入框)
```
为什么要使用类型断言










