import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CollectDepartmentDetailComponent } from './department-detail.component';

describe('CollectDepartmentDetailComponent', () => {
  let component: CollectDepartmentDetailComponent;
  let fixture: ComponentFixture<CollectDepartmentDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CollectDepartmentDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CollectDepartmentDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
