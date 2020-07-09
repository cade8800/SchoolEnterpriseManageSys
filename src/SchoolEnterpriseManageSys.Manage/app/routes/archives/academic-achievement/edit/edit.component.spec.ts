import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesAcademicAchievementEditComponent } from './edit.component';

describe('ArchivesAcademicAchievementEditComponent', () => {
  let component: ArchivesAcademicAchievementEditComponent;
  let fixture: ComponentFixture<ArchivesAcademicAchievementEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesAcademicAchievementEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesAcademicAchievementEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
