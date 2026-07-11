import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class Api {

  private http = inject(HttpClient);

  private api = 'https://gainscoai-api-h6gcaefthacxdxes.centralus-01.azurewebsites.net';

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
