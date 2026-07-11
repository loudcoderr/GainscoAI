import { Component } from '@angular/core';
import { Api } from '../../services/api';

@Component({
  selector: 'app-upload',
  standalone: true,
  imports: [],
  templateUrl: './upload.html',
  styleUrl: './upload.css'
})
export class Upload {

  selectedFile: File | null = null;

  constructor(private api: Api) {}

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  upload() {

    if (!this.selectedFile) {
      alert('Please select a PDF.');
      return;
    }

    this.api.upload(this.selectedFile).subscribe({
      next: (response) => {
        console.log(response);
        alert('PDF uploaded successfully!');
      },
      error: (err) => {
        console.error(err);
        alert('Upload failed.');
      }
    });
  }
}