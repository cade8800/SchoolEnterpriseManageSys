import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesCampusBaseEditComponent } from './edit.component';

describe('ArchivesCampusBaseEditComponent', () => {
  let component: ArchivesCampusBaseEditComponent;
  let fixture: ComponentFixture<ArchivesCampusBaseEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesCampusBaseEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesCampusBaseEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
