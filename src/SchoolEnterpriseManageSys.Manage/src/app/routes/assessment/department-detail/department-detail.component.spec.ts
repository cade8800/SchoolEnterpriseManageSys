import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AssessmentDepartmentDetailComponent } from './department-detail.component';

describe('AssessmentDepartmentDetailComponent', () => {
  let component: AssessmentDepartmentDetailComponent;
  let fixture: ComponentFixture<AssessmentDepartmentDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssessmentDepartmentDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssessmentDepartmentDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
