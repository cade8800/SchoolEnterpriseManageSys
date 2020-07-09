import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesSocialServiceImportComponent } from './import.component';

describe('ArchivesSocialServiceImportComponent', () => {
  let component: ArchivesSocialServiceImportComponent;
  let fixture: ComponentFixture<ArchivesSocialServiceImportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesSocialServiceImportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesSocialServiceImportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
