import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesOrderTrainingComponent } from './order-training.component';

describe('ArchivesOrderTrainingComponent', () => {
  let component: ArchivesOrderTrainingComponent;
  let fixture: ComponentFixture<ArchivesOrderTrainingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesOrderTrainingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesOrderTrainingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
