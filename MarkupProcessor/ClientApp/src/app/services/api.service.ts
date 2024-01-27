import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FlowDiagram } from '../flowDiagram';
import { Observable, throwError } from 'rxjs';
import { catchError, map, retry } from 'rxjs/operators';
import { FlowDiagramResponse } from '../flowDiagramResponse';
import { MDContents } from '../mdContents';
import { MDContentsResponse } from '../mdContentsResponse';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  constructor(private http: HttpClient) {
  }

  public CreateFlowDiagram(flowDiagram: FlowDiagram): Observable<FlowDiagram> {
    return this.http.post<FlowDiagram>('api/FlowDiagram/Create', flowDiagram);
  }

  public GetMdContentsListData(flowDiagramId: string): Observable<MDContents[]> {
    const params = new HttpParams()
      .set('flowChartId', flowDiagramId)
    return this.http.get<MDContentsResponse>('api/MarkupProcessor', { params }).pipe(map(response => response.data));
  }

  public GetFlowDiagramList(): Observable<FlowDiagram[]> {
    return this.http.get<FlowDiagramResponse>('api/FlowDiagram').pipe(map(response => response.data));
  }
}
