import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskUnFinishedComponent } from './task-un-finished.component';

describe('TaskUnFinishedComponent', () => {
  let component: TaskUnFinishedComponent;
  let fixture: ComponentFixture<TaskUnFinishedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskUnFinishedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskUnFinishedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
