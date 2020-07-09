import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesAcademicAchievementImportComponent } from './import.component';

describe('ArchivesAcademicAchievementImportComponent', () => {
  let component: ArchivesAcademicAchievementImportComponent;
  let fixture: ComponentFixture<ArchivesAcademicAchievementImportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesAcademicAchievementImportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesAcademicAchievementImportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
