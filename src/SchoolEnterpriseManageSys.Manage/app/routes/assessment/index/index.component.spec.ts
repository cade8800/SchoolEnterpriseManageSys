import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AssessmentIndexComponent } from './index.component';

describe('AssessmentIndexComponent', () => {
  let component: AssessmentIndexComponent;
  let fixture: ComponentFixture<AssessmentIndexComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssessmentIndexComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssessmentIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
