import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesSummaryComponent } from './summary.component';

describe('ArchivesSummaryComponent', () => {
  let component: ArchivesSummaryComponent;
  let fixture: ComponentFixture<ArchivesSummaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesSummaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
