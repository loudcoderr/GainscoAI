import { Component, ChangeDetectorRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Api } from '../../services/api';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './chat.html',
  styleUrl: './chat.css'
})
export class Chat {

  question = '';

  answer = '';

  loading = false;

  constructor(
  private api: Api,
  private cdr: ChangeDetectorRef
) {}

  ask() {

  if (!this.question.trim()) {
    return;
  }

  this.loading = true;
  this.answer = '';

  this.api.ask(this.question).subscribe({
    next: (res: any) => {
      console.log('Response:', res);

      this.answer = res.answer;
      this.loading = false;
      this.cdr.detectChanges();
      console.log('Answer:', this.answer);
      console.log('Loading:', this.loading);
    },
    error: (err) => {
      console.error('Error:', err);
      this.loading = false;
    },
    complete: () => {
      console.log('Request completed');
    }
  });
}
}