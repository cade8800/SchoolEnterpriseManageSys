import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesJointlyEstablishedProfessionComponent } from './jointly-established-profession.component';

describe('ArchivesJointlyEstablishedProfessionComponent', () => {
  let component: ArchivesJointlyEstablishedProfessionComponent;
  let fixture: ComponentFixture<ArchivesJointlyEstablishedProfessionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesJointlyEstablishedProfessionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesJointlyEstablishedProfessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
