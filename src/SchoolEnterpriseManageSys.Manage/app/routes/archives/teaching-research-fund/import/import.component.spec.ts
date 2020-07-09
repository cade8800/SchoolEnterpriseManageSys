import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesTeachingResearchFundImportComponent } from './import.component';

describe('ArchivesTeachingResearchFundImportComponent', () => {
  let component: ArchivesTeachingResearchFundImportComponent;
  let fixture: ComponentFixture<ArchivesTeachingResearchFundImportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesTeachingResearchFundImportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesTeachingResearchFundImportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
