# 基础

## 重点理解的概念

### event listener

### web components基础

### 装饰器

### 类

## 定义一个component

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

## 响应式属性以及模板
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

- `property`: 装饰器，用于定义响应式属性,需要和模板字面量配合使用,此处是在MyElement class中定义了一个名为message的属性,类型为字符串
- 响应式属性: 当name属性值改变时,组件会自动重新渲染(调用render方法)
- 可以通过HTML属性设置这个值,设置之后会覆盖掉ts文件中定义的值,例如`<my-element message = "gagaga"></my-element>`,这时网页会显示`gagaga`而不是之前定义的`Hello again`
- 






