import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ExtrasUserComponent } from './user.component';

describe('ExtrasUserComponent', () => {
  let component: ExtrasUserComponent;
  let fixture: ComponentFixture<ExtrasUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ExtrasUserComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExtrasUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
