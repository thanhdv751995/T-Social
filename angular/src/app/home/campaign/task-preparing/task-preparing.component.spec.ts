import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskPreparingComponent } from './task-preparing.component';

describe('TaskPreparingComponent', () => {
  let component: TaskPreparingComponent;
  let fixture: ComponentFixture<TaskPreparingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskPreparingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskPreparingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
