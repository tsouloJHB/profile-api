import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaythemeComponent } from './playtheme.component';

describe('PlaythemeComponent', () => {
  let component: PlaythemeComponent;
  let fixture: ComponentFixture<PlaythemeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlaythemeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PlaythemeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
