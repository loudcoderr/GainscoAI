import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class Api {

  private http = inject(HttpClient);

  private api = 'http://localhost:5080';

  upload(file: File) {

    const formData = new FormData();
    formData.append('file', file);

    return this.http.post(
      `${this.api}/api/document/upload`,
      formData
    );
  }

  ask(question: string) {

    return this.http.get<any>(
      `${this.api}/api/chat/ask?question=${encodeURIComponent(question)}`
    );
  }
}