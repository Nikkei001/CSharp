import {LitElement, html} from 'lit';
import {customElement, property} from 'lit/decorators.js';


@customElement('my-element') export class MyElement extends LitElement {  
  @property()
  name: string = "Nikkei";

  render() {  
    return html`    
      <h1>Hello world! From ${this.name}.</h1>
    `;
  }
}
