import { APP_BASE_HREF } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FlowDiagram } from '../flowDiagram';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  flowDiagram = new FlowDiagram();

  constructor(private fb: FormBuilder, private apiService: ApiService, private router: Router) {
  }

  flowChartForm = this.fb.group({
    nameField: new FormControl(''),
  });

  onSubmit() {
    this.flowDiagram.name = this.flowChartForm.value.nameField?.toString()
    this.apiService.createFlowDiagram(this.flowDiagram).subscribe((result: FlowDiagram) => {
      this.router.navigate(['/file-upload', result.id]);
    });
  }
}
