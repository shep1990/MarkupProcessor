import { APP_BASE_HREF } from "@angular/common";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Component, Inject, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { FlowDiagram } from "../flowDiagram";
import { MDContents } from "../mdContents";
import { ApiService } from "../services/api.service";

@Component({
  selector: 'file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ["./file-upload.component.css"]
})
export class FileUploadComponent implements OnInit {
  fileName = '';
  hello: string = ''
  public forecasts: any;
  public test: MDContents[] = []

  constructor(private http: HttpClient, private apiService: ApiService, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.apiService.getFlowCharts(this.route.snapshot.paramMap.get('id')!).subscribe((data) => {
      this.test = Object.values(data)
      console.log(this.test)
    });
  }

  onFileSelected(event: any) {
    let files: FileList = event.target.files;
    let formData = new FormData();
    const params = new HttpParams().set('flowDiagramId', this.route.snapshot.paramMap.get('id')!)
     
    for (var i = 0; i < files.length; i++) {
      formData.append("flowDiagramData", files[i]);
    }
    this.http.post("api/MarkupProcessor/Upload", formData, { params: params }).subscribe(data => {
      this.forecasts = data as FlowDiagramData
    });
  }
}

interface FlowDiagramData {
  payload: string
}
  

