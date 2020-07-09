import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AssessmentIndexEditComponent } from './index-edit.component';

describe('AssessmentIndexEditComponent', () => {
  let component: AssessmentIndexEditComponent;
  let fixture: ComponentFixture<AssessmentIndexEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssessmentIndexEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssessmentIndexEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
