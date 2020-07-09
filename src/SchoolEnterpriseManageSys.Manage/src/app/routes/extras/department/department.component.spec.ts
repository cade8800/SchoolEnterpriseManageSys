import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ExtrasDepartmentComponent } from './department.component';

describe('CmsListComponent', () => {
  let component: ExtrasDepartmentComponent;
  let fixture: ComponentFixture<ExtrasDepartmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExtrasDepartmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExtrasDepartmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
