import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScoresHistoryComponent } from './scores-history.component';

describe('ScoresHistoryComponent', () => {
  let component: ScoresHistoryComponent;
  let fixture: ComponentFixture<ScoresHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScoresHistoryComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ScoresHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
