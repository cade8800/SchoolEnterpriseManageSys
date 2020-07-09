import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesExplainComponent } from './explain.component';

describe('ArchivesExplainComponent', () => {
  let component: ArchivesExplainComponent;
  let fixture: ComponentFixture<ArchivesExplainComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesExplainComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesExplainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
