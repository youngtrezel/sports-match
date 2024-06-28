import { Component } from '@angular/core';
import { JsonPipe, NgFor, NgIf, CommonModule, formatDate } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatchService } from '../services/match/match.service';
import { ScoresResponse } from '../../models/scores-response';

@Component({
  selector: 'app-scores-history',
  standalone: true,
  imports: [NgFor, JsonPipe, ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './scores-history.component.html',
  styleUrl: './scores-history.component.scss'
})
export class ScoresHistoryComponent {
  allForm!: FormGroup;
  nameForm!: FormGroup;
  idForm!: FormGroup;
  allRecords: ScoresResponse[] = [];
  populated = false;
  unpopulated = false;

  constructor (private formBuilder: FormBuilder,
    private matchService: MatchService
  ) {
    this.allRecords = [];

  }

  ngOnInit(): void {
    this.nameForm = this.formBuilder.group({
      NameSearch: new FormControl((''), [Validators.required])
    });
    this.idForm = this.formBuilder.group({
      IdSearch: new FormControl((''), [Validators.required])
    })
    this.populated = false;
    this.unpopulated = false;
  }

  get nameSearch(): any { return this.nameForm.get('NameSearch'); }
  get idSearch(): any { return this.idForm.get('IdSearch'); }

  getAll() {
    this.allRecords = [];
    this.matchService.getAllMatches().subscribe({
      next: (data) => {
        
        if(data.length > 0) {
          this.allRecords = data;
          this.unpopulated = false;
          this.populated = true;
        } else {
          this.unpopulated = true;
          this.populated = false;
        }
         
      },
      error: err => console.log(err)
    })
  }

  clearfields() {
    this.nameSearch.reset();
    this.idSearch.reset();
  }

  getByName() {
    var name = this.nameForm.value.NameSearch;
    this.allRecords = [];
    this.matchService.getMatchByTeamName(name).subscribe({
      next: (data) => {

        if(data.length > 0) {
          this.allRecords = data;
          this.unpopulated = false;
          this.populated = true;
          
        } else {
          this.unpopulated = true;
          this.populated = false;
        }
        
      },
      error: err => console.log(err)
    })

    this.clearfields();
  }

  getById() {
    var id = this.idForm.value.IdSearch;
    this.allRecords = [];
    this.matchService.getMatchById(id).subscribe({
      next: (data) => {       

        if(data.length > 0) {
          this.allRecords = data;
          this.unpopulated = false;
          this.populated = true;
        } else {         
          this.unpopulated = true;  
          this.populated = false;      
        }
      },
      error: err => console.log(err)
    })

    this.clearfields();
  }
}
