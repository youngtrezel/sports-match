import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ScorerComponent } from './scorer/scorer.component';
import { NavigationComponent } from './navigation/navigation.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, ScorerComponent, NavigationComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'sportsmatch';
}
