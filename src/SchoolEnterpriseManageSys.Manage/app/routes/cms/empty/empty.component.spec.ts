import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CmsEmptyComponent } from './empty.component';

describe('CmsEmptyComponent', () => {
  let component: CmsEmptyComponent;
  let fixture: ComponentFixture<CmsEmptyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CmsEmptyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CmsEmptyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
