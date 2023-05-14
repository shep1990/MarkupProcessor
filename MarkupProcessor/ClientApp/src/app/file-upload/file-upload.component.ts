import { APP_BASE_HREF } from "@angular/common";
import { HttpClient } from "@angular/common/http";
import { Component, Inject } from "@angular/core";

@Component({
  selector: 'file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ["./file-upload.component.css"]
})
export class FileUploadComponent {

  fileName = '';

  constructor(private http: HttpClient) {
  }

  onFileSelected(event: any) {

    const file: File = event.target.files[0];

    console.log(file);

    if (file) {

      this.fileName = file.name;

      const formData = new FormData();

      formData.append("thumbnail", file);

      this.http.post("/api/MarkupProcessor/Upload", formData).subscribe();
    }
  }
}
