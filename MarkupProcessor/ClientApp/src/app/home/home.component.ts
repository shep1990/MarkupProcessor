import { APP_BASE_HREF } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { FlowDiagram } from '../flowDiagram';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  flowDiagram = new FlowDiagram();

  constructor(private fb: FormBuilder, private apiService: ApiService) {
  }

  flowChartForm = this.fb.group({
    nameField: new FormControl(''),
  });

  onSubmit() {
    this.flowDiagram.name = this.flowChartForm.value.nameField?.toString()
    console.log(this.flowChartForm)
    this.apiService.createFlowDiagram(this.flowDiagram).subscribe(data => {
      console.log(data)
    });
  }
}
