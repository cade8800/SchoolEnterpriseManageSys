import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesCoAuthoredBookOrCourseComponent } from './co-authored-book-or-course.component';

describe('ArchivesCoAuthoredBookOrCourseComponent', () => {
  let component: ArchivesCoAuthoredBookOrCourseComponent;
  let fixture: ComponentFixture<ArchivesCoAuthoredBookOrCourseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesCoAuthoredBookOrCourseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesCoAuthoredBookOrCourseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
