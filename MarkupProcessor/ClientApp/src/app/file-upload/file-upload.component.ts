import { APP_BASE_HREF } from "@angular/common";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { MDContents } from "../mdContents";
import { ApiService } from "../services/api.service";

@Component({
  selector: 'file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ["./file-upload.component.css"]
})
export class FileUploadComponent implements OnInit {
  fileName = '';
  public flowDiagramData = new Object;
  public mdContents: MDContents[] = []
  public formData = new FormData();

  constructor(private http: HttpClient, private apiService: ApiService, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.apiService.GetMdContentsListData(this.route.snapshot.paramMap.get('id')!).subscribe((data) => {
      this.mdContents = Object.values(data)
    });
  }

  onFileSelected(event: any) {
    let files: FileList = event.target.files;
    const params = new HttpParams().set('flowDiagramId', this.route.snapshot.paramMap.get('id')!)
     
    for (var i = 0; i < files.length; i++) {
      this.formData.append("flowDiagramData", files[i]);
    }
    this.http.post("api/MarkupProcessor/Upload", this.formData, { params: params }).subscribe(data => {
      this.flowDiagramData = data as FlowDiagramData
    });
  }
}

interface FlowDiagramData {
  payload: string
}
  

