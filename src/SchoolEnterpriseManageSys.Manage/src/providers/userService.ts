import { Injectable } from '@angular/core';
import {
  User_Login,
  User_Update_Password,
  User_Info,
  User_Update_Info,
  User_Enterprise_Regist,
  User_GetUsers,
  User_Update_State,
  User_Reset_Password,
  User_Insert_Campust_User,
  User_Update_Avatar,
} from './constants';
import { HttpService } from './httpService';

@Injectable()
export class UserService {
  constructor(public http: HttpService) { }

  login(paramObj: any) {
    return this.http.post(User_Login, paramObj);
  }
  updatePassword(paramObj: any) {
    return this.http.put(User_Update_Password, paramObj);
  }
  getUserInfo() {
    return this.http.get(User_Info, {});
  }
  updateUserInfo(paramObj: any) {
    return this.http.put(User_Update_Info, paramObj);
  }
  enterpriseRegist(paramObj: any) {
    return this.http.postBody(User_Enterprise_Regist, paramObj);
  }
  getUsers(paramObj: any) {
    return this.http.postBody(User_GetUsers, paramObj);
  }
  updateUserState(userId: string) {
    return this.http.put(User_Update_State + userId, {});
  }
  resetPassword(userId: string) {
    return this.http.post(User_Reset_Password + userId, {});
  }
  insertUser(paramObj: any) {
    return this.http.postBody(User_Insert_Campust_User, paramObj);
  }

  updateAvatar(url: string) {
    return this.http.put(User_Update_Avatar, { url: url });
  }
}
