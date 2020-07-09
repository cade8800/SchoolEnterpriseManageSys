import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesAcademicAchievementComponent } from './academic-achievement.component';

describe('ArchivesAcademicAchievementComponent', () => {
  let component: ArchivesAcademicAchievementComponent;
  let fixture: ComponentFixture<ArchivesAcademicAchievementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesAcademicAchievementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesAcademicAchievementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
