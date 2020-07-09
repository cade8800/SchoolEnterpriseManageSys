import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CollectDepartmentUploadComponent } from './upload.component';

describe('CollectDepartmentUploadComponent', () => {
  let component: CollectDepartmentUploadComponent;
  let fixture: ComponentFixture<CollectDepartmentUploadComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CollectDepartmentUploadComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CollectDepartmentUploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
