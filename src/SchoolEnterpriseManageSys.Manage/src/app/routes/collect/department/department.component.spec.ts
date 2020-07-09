import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CollectDepartmentComponent } from './department.component';

describe('CollectDepartmentComponent', () => {
  let component: CollectDepartmentComponent;
  let fixture: ComponentFixture<CollectDepartmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CollectDepartmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CollectDepartmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
