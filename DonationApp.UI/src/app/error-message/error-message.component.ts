import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-error-message',
    imports: [CommonModule],
    standalone: true,
    templateUrl: './error-message.component.html',
    styleUrl: './error-message.component.css'
})
export class ErrorMessageComponent {
  @Input() errorMessage: string = '';
}
