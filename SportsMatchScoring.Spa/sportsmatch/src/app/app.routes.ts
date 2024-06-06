import { Routes } from '@angular/router';
import { ScorerComponent } from './scorer/scorer.component';
import { ScoresHistoryComponent } from './scores-history/scores-history.component';

export const routes: Routes = [
    { path: '', component: ScorerComponent },
    { path: 'history', component: ScoresHistoryComponent }
];

