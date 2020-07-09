import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesOrderTrainingImportComponent } from './import.component';

describe('ArchivesOrderTrainingImportComponent', () => {
  let component: ArchivesOrderTrainingImportComponent;
  let fixture: ComponentFixture<ArchivesOrderTrainingImportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesOrderTrainingImportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesOrderTrainingImportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
