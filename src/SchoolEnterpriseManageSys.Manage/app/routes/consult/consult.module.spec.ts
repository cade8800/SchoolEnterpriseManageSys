import { ConsultModule } from './consult.module';

describe('ConsultModule', () => {
  let consultModule: ConsultModule;

  beforeEach(() => {
    consultModule = new ConsultModule();
  });

  it('should create an instance', () => {
    expect(consultModule).toBeTruthy();
  });
});
