import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesSocialServiceComponent } from './social-service.component';

describe('ArchivesSocialServiceComponent', () => {
  let component: ArchivesSocialServiceComponent;
  let fixture: ComponentFixture<ArchivesSocialServiceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesSocialServiceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesSocialServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
