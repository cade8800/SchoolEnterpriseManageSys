import { AppointmentModule } from './appointment.module';

describe('AppointmentModule', () => {
  let appointmentModule: AppointmentModule;

  beforeEach(() => {
    appointmentModule = new AppointmentModule();
  });

  it('should create an instance', () => {
    expect(appointmentModule).toBeTruthy();
  });
});
