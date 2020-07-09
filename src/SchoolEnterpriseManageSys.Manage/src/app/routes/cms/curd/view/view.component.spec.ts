import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CmsCurdViewComponent } from './view.component';

describe('CmsCurdViewComponent', () => {
  let component: CmsCurdViewComponent;
  let fixture: ComponentFixture<CmsCurdViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CmsCurdViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CmsCurdViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
