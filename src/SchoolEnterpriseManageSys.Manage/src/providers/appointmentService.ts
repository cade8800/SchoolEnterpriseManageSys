import { Injectable } from '@angular/core';
import { HttpService } from './httpService';
import { Appointment_Get, Appointment_Edit, Appointment_Get_Enterprise, Appointment_Delete, Appointment_Confirm, Appointment_Send } from './constants';

@Injectable()
export class AppointmentService {
    constructor(public http: HttpService) { }

    getAppointment(id: string) {
        return this.http.postBody(Appointment_Get, { id: id });
    }
    editAppointment(paramObj: any) {
        return this.http.postBody(Appointment_Edit, paramObj);
    }

    getEnterpriseAppointment(paramObj: any) {
        return this.http.postBody(Appointment_Get_Enterprise, paramObj);
    }

    deleteAppointment(id: string) {
        return this.http.delete(Appointment_Delete + id);
    }

    confirmAppointment(id: string) {
        return this.http.post(Appointment_Confirm + id, {});
    }

    sendToDepartment(paramObj: any) {
        return this.http.postBody(Appointment_Send, paramObj);
    }
}