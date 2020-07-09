import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AssessmentDepartmentEditComponent } from './department-edit.component';

describe('AssessmentDepartmentEditComponent', () => {
  let component: AssessmentDepartmentEditComponent;
  let fixture: ComponentFixture<AssessmentDepartmentEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssessmentDepartmentEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssessmentDepartmentEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
