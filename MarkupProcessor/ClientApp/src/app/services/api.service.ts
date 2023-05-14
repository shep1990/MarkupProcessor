import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FlowDiagram } from '../flowDiagram';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  resposne: any;

  constructor(private http: HttpClient) {
  }

  public createFlowDiagram(flowDiagram: FlowDiagram): Observable<FlowDiagram> {
    console.log(flowDiagram)
    this.http.post<FlowDiagram>('api/FlowDiagram/Create', flowDiagram).subscribe(data => {
      this.resposne = data.name;
    })
    console.log(this.resposne)
    return this.resposne
  }
}
