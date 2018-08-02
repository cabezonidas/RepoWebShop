import { CateringModule } from './catering.module';

describe('CateringModule', () => {
  let cateringModule: CateringModule;

  beforeEach(() => {
    cateringModule = new CateringModule();
  });

  it('should create an instance', () => {
    expect(cateringModule).toBeTruthy();
  });
});
