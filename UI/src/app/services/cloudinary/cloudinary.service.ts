import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Cloudinary } from 'cloudinary-core';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class CloudinaryService {

  private cloudinary: Cloudinary;

  constructor(private http: HttpClient) {
    this.cloudinary = new Cloudinary({
      cloud: {
        cloudName: environment.cloudinary.cloudName,
      }
    });
  }

  uploadImage(file: File) {
    const formData = new FormData();
    formData.append('file', file);
    formData.append('upload_preset', environment.cloudinary.uploadPreset);

    return this.http.post(`https://api.cloudinary.com/v1_1/${environment.cloudinary.cloudName}/image/upload`, formData);
  }
}
