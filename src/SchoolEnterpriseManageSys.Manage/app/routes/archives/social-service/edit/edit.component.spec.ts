import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ArchivesSocialServiceEditComponent } from './edit.component';

describe('ArchivesSocialServiceEditComponent', () => {
  let component: ArchivesSocialServiceEditComponent;
  let fixture: ComponentFixture<ArchivesSocialServiceEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesSocialServiceEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesSocialServiceEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
