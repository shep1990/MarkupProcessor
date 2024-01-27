import { APP_BASE_HREF } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FlowDiagram } from '../flowDiagram';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  flowDiagram = new FlowDiagram();
  public flowDiagramList: FlowDiagram[] = []

  constructor(private fb: FormBuilder, private apiService: ApiService, private router: Router) {
  }
  ngOnInit() {
    this.apiService.GetFlowDiagramList().subscribe((data) => {
      this.flowDiagramList = Object.values(data)
    });
  }

  routeToComponent(id: any) {
    this.router.navigate(['/file-upload/', id]);
  };

  flowChartForm = this.fb.group({
    nameField: new FormControl(''),
  });

  onSubmit() {
    this.flowDiagram.flowDiagramName = this.flowChartForm.value.nameField?.toString()
    this.apiService.CreateFlowDiagram(this.flowDiagram).subscribe((result: FlowDiagram) => {
      this.router.navigate(['/file-upload/', result.id]);
    });
  }
}
