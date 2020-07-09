import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesOffCampusBaseImportComponent } from './import.component';

describe('ArchivesOffCampusBaseImportComponent', () => {
  let component: ArchivesOffCampusBaseImportComponent;
  let fixture: ComponentFixture<ArchivesOffCampusBaseImportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesOffCampusBaseImportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesOffCampusBaseImportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
