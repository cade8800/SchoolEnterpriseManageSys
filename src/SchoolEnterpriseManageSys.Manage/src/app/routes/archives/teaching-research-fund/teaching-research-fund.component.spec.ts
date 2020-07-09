import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesTeachingResearchFundComponent } from './teaching-research-fund.component';

describe('ArchivesTeachingResearchFundComponent', () => {
  let component: ArchivesTeachingResearchFundComponent;
  let fixture: ComponentFixture<ArchivesTeachingResearchFundComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesTeachingResearchFundComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesTeachingResearchFundComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
