import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesOffCampusBaseComponent } from './off-campus-base.component';

describe('ArchivesOffCampusBaseComponent', () => {
  let component: ArchivesOffCampusBaseComponent;
  let fixture: ComponentFixture<ArchivesOffCampusBaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesOffCampusBaseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesOffCampusBaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
