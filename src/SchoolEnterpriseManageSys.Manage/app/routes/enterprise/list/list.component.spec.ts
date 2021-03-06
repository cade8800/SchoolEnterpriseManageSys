import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { EnterpriseListComponent } from './list.component';

describe('EnterpriseListComponent', () => {
  let component: EnterpriseListComponent;
  let fixture: ComponentFixture<EnterpriseListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnterpriseListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnterpriseListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
