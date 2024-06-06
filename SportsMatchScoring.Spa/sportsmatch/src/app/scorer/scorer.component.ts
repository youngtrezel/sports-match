import { Component, viewChild } from '@angular/core';
import { ScoresSet } from '../../models/scores-set';
import { JsonPipe, NgFor, NgIf, CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Match } from '../../models/match';
import { Games } from '../../models/games';
import { MatchService } from '../services/match/match.service';

@Component({
  selector: 'app-scorer',
  standalone: true,
  imports: [ NgFor, JsonPipe, ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './scorer.component.html',
  styleUrl: './scorer.component.scss'
})
export class ScorerComponent {

  matchForm!: FormGroup;
  scoreSets: any;
  currentScoreIt: any;
  homeSets: any;
  awaySets: any;
  scoreStrings: any;
  homeTeamName = "";
  awayTeamName = "";
  games = Object.values(Games);
  gameResult = "";
  namesAdded = false;

  winningScore = 15;
  winningScoreDiff = 13;
  suddenHeatScore = 14;
  drawLimit = 20;

  updateScoring() {
    
    if(this.matchForm.value.Game == Games.Volleyball) {
      this.winningScore = 15;
      this.winningScoreDiff = 13;
      this.suddenHeatScore = 14;
      this.drawLimit = 20;
    } else {
      this.winningScore = 11;
      this.winningScoreDiff = 9;
      this.suddenHeatScore = 10;
      this.drawLimit = 18;
    }

    this.resetAllScores() ;
  }


  constructor (private formBuilder: FormBuilder,
    private matchService: MatchService
  ) {
    this.currentScoreIt = new ScoresSet;
    this.scoreSets = [];
    this.homeSets = [];
    this.awaySets= [];
    this.scoreStrings = [];
  }

  ngOnInit() : void {
    this.matchForm = this.formBuilder.group({
      HomeTeamName: new FormControl((''), [Validators.required]),
      AwayTeamName: new FormControl((''), [Validators.required]),
      Game: new FormControl(Games.Volleyball, [Validators.required])
    })
  }

  get homeName(): any { return this.matchForm.get('HomeTeamName'); }
  get awayName(): any { return this.matchForm.get('AwayTeamName'); }

  updateHomeName(event: any) {
    this.homeTeamName = event.target.value;
    this.namesAdded = this.homeTeamName != "" && this.awayTeamName != "";
  }

  updateAwayName(event: any) {
    this.awayTeamName = event.target.value;
    this.namesAdded = this.homeTeamName != "" && this.awayTeamName != "";
  }

  homeScore() {
    this.currentScoreIt.Home++;
    this.currentScoreIt.Score = this.currentScoreIt.Score.concat("1");
    if(this.currentScoreIt.Home >= this.winningScore && this.currentScoreIt.Away <= this.winningScoreDiff) {    
      this.homeSets.push(this.currentScoreIt.Home); 
      this.awaySets.push(this.currentScoreIt.Away); 
      this.roundSettled();
    }
    if(this.currentScoreIt.Home >= this.suddenHeatScore && this.currentScoreIt.Away >= this.suddenHeatScore) {
      if(Math.abs(this.currentScoreIt.Home - this.currentScoreIt.Away) == 2) {
        this.homeSets.push(this.currentScoreIt.Home); 
        this.awaySets.push(this.currentScoreIt.Away); 
        this.roundSettled();
      }

      if(this.currentScoreIt.Home == this.drawLimit && this.currentScoreIt.Away == this.drawLimit) {
        this.roundDrawed();
      }
    }
    this.isSquash();
  }


  awayScore() {
    this.currentScoreIt.Away++;
    this.currentScoreIt.Score = this.currentScoreIt.Score.concat("0");
    if(this.currentScoreIt.Away >= this.winningScore && this.currentScoreIt.Home <= this.winningScoreDiff) {
      this.homeSets.push(this.currentScoreIt.Home); 
      this.awaySets.push(this.currentScoreIt.Away); 
      this.roundSettled();
      this.newScoreSet();
    }
    if(this.currentScoreIt.Home >= this.suddenHeatScore && this.currentScoreIt.Away >= this.suddenHeatScore) {
      if(Math.abs(this.currentScoreIt.Away - this.currentScoreIt.Home) == 2) {
        this.homeSets.push(this.currentScoreIt.Home); 
        this.awaySets.push(this.currentScoreIt.Away); 
        this.roundSettled();
        this.newScoreSet();
      }
      if(this.currentScoreIt.Home == this.drawLimit && this.currentScoreIt.Away == this.drawLimit) {
        this.roundDrawed();
      }
    }

    this.isSquash();
  }

  isSquash() {
    if(this.matchForm.value.Game == Games.Squash) {
      if(Math.abs(this.homeSets.length - this.awaySets.length) == 2 ) {
        this.homeSets.push(this.currentScoreIt.Home); 
        this.awaySets.push(this.currentScoreIt.Away); 
        this.roundSettled();
        this.onSubmit()
      }
    }
  }


  roundDrawed(){
    this.addDrawScore();
    this.resetScore(); 
  }
  
  roundSettled(){
    this.addScoreToArray();
    this.resetScore(); 
  }

  resetScore() {
    this.currentScoreIt.Home = 0;
    this.currentScoreIt.Away = 0;
    this.currentScoreIt.Score = "";
  }

  resetAllScores() {
    this.gameResult = "";
    this.homeTeamName = "";
    this.awayTeamName = "";
    this.homeName.reset();
    this.awayName.reset();
    this.scoreSets.length = 0;
    this.scoreStrings.length = 0;
    this.namesAdded = false;
    this.resetScore();
  }

  newScoreSet() {
    this.currentScoreIt = new ScoresSet;
  }

  addDrawScore() {
    var copy = new ScoresSet; 
    copy.Draw = true;
    copy.Home = this.currentScoreIt.Home; 
    copy.Away = this.currentScoreIt.Away; 
    copy.Score = this.currentScoreIt.Score;

    this.scoreSets.push(copy);
    this.scoreStrings.push(copy.Score);

  }

  addScoreToArray(){
    var copy = new ScoresSet;  
    copy.Home = this.currentScoreIt.Home; 
    copy.Away = this.currentScoreIt.Away; 
    copy.Score = this.currentScoreIt.Score;

    this.scoreSets.push(copy);
    this.scoreStrings.push(copy.Score);
  }

  onSubmit() {

    const matchRequest: Match = {
      HomeTeamName: this.homeTeamName,
      AwayTeamName: this.awayTeamName,
      Game: this.matchForm.value.Game,
      Scores: this.scoreStrings
    };

    this.matchService.postMatch(matchRequest).subscribe({
      next: (data) => {
        this.gameResult = data;
      },
      error: err => console.log(err)
    })

  }

}

