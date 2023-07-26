import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FlowDiagram } from '../flowDiagram';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  constructor(private http: HttpClient) {
  }

  public createFlowDiagram(flowDiagram: FlowDiagram): Observable<FlowDiagram> {
    return this.http.post<FlowDiagram>('api/FlowDiagram/Create', flowDiagram);
  }
}
