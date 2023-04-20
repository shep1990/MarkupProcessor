import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";

@Component({
  selector: 'file-upload',
  templateUrl: './home.component.html',
  styleUrls: ["./home.component.css"]
})
export class HomeComponent {

  fileName = '';

  constructor(private http: HttpClient) { }

  onFileSelected(event: any) {

    const file: File = event.target.files[0];

    console.log(file);

    if (file) {

      this.fileName = file.name;

      const formData = new FormData();

      formData.append("thumbnail", file);

      console.log(formData)

      const upload$ = this.http.post("https://localhost:7291/api/MarkupProcessor/UploadFile", formData);

      upload$.subscribe();
    }
  }
}
