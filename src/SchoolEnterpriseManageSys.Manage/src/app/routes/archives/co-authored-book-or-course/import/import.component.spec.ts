import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesCoAuthoredBookOrCourseImportComponent } from './import.component';

describe('ArchivesCoAuthoredBookOrCourseImportComponent', () => {
  let component: ArchivesCoAuthoredBookOrCourseImportComponent;
  let fixture: ComponentFixture<ArchivesCoAuthoredBookOrCourseImportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesCoAuthoredBookOrCourseImportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesCoAuthoredBookOrCourseImportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
