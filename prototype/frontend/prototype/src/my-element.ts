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
