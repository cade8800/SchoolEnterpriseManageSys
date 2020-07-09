import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesOffCampusBaseEditComponent } from './edit.component';

describe('ArchivesOffCampusBaseEditComponent', () => {
  let component: ArchivesOffCampusBaseEditComponent;
  let fixture: ComponentFixture<ArchivesOffCampusBaseEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesOffCampusBaseEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesOffCampusBaseEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
