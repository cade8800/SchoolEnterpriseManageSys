import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesCampusBaseImportComponent } from './import.component';

describe('ArchivesCampusBaseImportComponent', () => {
  let component: ArchivesCampusBaseImportComponent;
  let fixture: ComponentFixture<ArchivesCampusBaseImportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesCampusBaseImportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesCampusBaseImportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
