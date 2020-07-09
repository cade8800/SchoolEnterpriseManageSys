import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesJointlyEstablishedProfessionEditComponent } from './edit.component';

describe('ArchivesJointlyEstablishedProfessionEditComponent', () => {
  let component: ArchivesJointlyEstablishedProfessionEditComponent;
  let fixture: ComponentFixture<ArchivesJointlyEstablishedProfessionEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesJointlyEstablishedProfessionEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesJointlyEstablishedProfessionEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
