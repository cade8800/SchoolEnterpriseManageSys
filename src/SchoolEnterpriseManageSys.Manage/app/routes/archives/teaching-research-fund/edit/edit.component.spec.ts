import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesTeachingResearchFundEditComponent } from './edit.component';

describe('ArchivesTeachingResearchFundEditComponent', () => {
  let component: ArchivesTeachingResearchFundEditComponent;
  let fixture: ComponentFixture<ArchivesTeachingResearchFundEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesTeachingResearchFundEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesTeachingResearchFundEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
