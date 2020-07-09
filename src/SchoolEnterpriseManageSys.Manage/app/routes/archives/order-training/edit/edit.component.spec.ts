import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesOrderTrainingEditComponent } from './edit.component';

describe('ArchivesOrderTrainingEditComponent', () => {
  let component: ArchivesOrderTrainingEditComponent;
  let fixture: ComponentFixture<ArchivesOrderTrainingEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesOrderTrainingEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesOrderTrainingEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
