import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaintThemeComponent } from './paint-theme.component';

describe('PaintThemeComponent', () => {
  let component: PaintThemeComponent;
  let fixture: ComponentFixture<PaintThemeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaintThemeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PaintThemeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
