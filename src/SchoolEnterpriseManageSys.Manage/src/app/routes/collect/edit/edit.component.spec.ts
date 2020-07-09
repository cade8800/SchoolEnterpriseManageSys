import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CollectEditComponent } from './edit.component';

describe('CollectEditComponent', () => {
  let component: CollectEditComponent;
  let fixture: ComponentFixture<CollectEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CollectEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CollectEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
