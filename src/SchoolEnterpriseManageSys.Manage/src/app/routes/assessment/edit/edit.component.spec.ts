import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AssessmentEditComponent } from './edit.component';

describe('AssessmentEditComponent', () => {
  let component: AssessmentEditComponent;
  let fixture: ComponentFixture<AssessmentEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssessmentEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssessmentEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
