import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ConsultIndexComponent } from './index.component';

describe('ConsultIndexComponent', () => {
  let component: ConsultIndexComponent;
  let fixture: ComponentFixture<ConsultIndexComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConsultIndexComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConsultIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
