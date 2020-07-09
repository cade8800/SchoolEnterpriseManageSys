import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesCampusBaseComponent } from './campus-base.component';

describe('ArchivesCampusBaseComponent', () => {
  let component: ArchivesCampusBaseComponent;
  let fixture: ComponentFixture<ArchivesCampusBaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesCampusBaseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesCampusBaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
