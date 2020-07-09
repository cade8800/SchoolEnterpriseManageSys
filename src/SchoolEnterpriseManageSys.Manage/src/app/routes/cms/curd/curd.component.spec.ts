import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CmsCurdComponent } from './curd.component';

describe('CmsCurdComponent', () => {
  let component: CmsCurdComponent;
  let fixture: ComponentFixture<CmsCurdComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CmsCurdComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CmsCurdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
