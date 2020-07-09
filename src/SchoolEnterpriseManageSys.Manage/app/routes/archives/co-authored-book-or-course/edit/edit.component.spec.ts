import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesCoAuthoredBookOrCourseEditComponent } from './edit.component';

describe('ArchivesCoAuthoredBookOrCourseEditComponent', () => {
  let component: ArchivesCoAuthoredBookOrCourseEditComponent;
  let fixture: ComponentFixture<ArchivesCoAuthoredBookOrCourseEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesCoAuthoredBookOrCourseEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesCoAuthoredBookOrCourseEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
