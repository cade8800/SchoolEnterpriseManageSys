import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AssessmentExpertEditComponent } from './expert-edit.component';

describe('AssessmentExpertEditComponent', () => {
  let component: AssessmentExpertEditComponent;
  let fixture: ComponentFixture<AssessmentExpertEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssessmentExpertEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssessmentExpertEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
