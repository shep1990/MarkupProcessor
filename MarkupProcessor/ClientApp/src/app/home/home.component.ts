import { APP_BASE_HREF } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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
  public test: FlowDiagram[] = []

  constructor(private fb: FormBuilder, private apiService: ApiService, private router: Router) {
  }
  ngOnInit() {
    this.apiService.get().subscribe((data) => {
      this.test = Object.values(data)
    });
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
