import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CmsCurdEditComponent } from './edit.component';

describe('CmsCurdEditComponent', () => {
  let component: CmsCurdEditComponent;
  let fixture: ComponentFixture<CmsCurdEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CmsCurdEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CmsCurdEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
