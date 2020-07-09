import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AssessmentDepartmentIndexComponent } from './department-index.component';

describe('AssessmentDepartmentIndexComponent', () => {
  let component: AssessmentDepartmentIndexComponent;
  let fixture: ComponentFixture<AssessmentDepartmentIndexComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssessmentDepartmentIndexComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssessmentDepartmentIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
