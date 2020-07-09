import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../../../providers/userService';
import { Router } from '@angular/router';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { NzMessageService, UploadFile } from 'ng-zorro-antd';
import { File_Upload } from 'providers/constants';

@Component({
  selector: 'app-extras-settings',
  templateUrl: './settings.component.html',
  providers: [UserService],
})
export class ExtrasSettingsComponent implements OnInit {
  active = 1;
  profileForm: FormGroup;
  pwd = {
    oldPassword: '',
    newPassword: '',
    confirmNewPassword: '',
  };

  loading = false;
  avatarUrl: string;
  uploadUrl = File_Upload;

  constructor(
    fb: FormBuilder,
    public msg: NzMessageService,
    public userService: UserService,
    private router: Router,
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
  ) {
    this.profileForm = fb.group({
      userName: '',
      nickname: '',
      actualName: '',
      avatarUrl: '',
      mobilephone: '',
      fixedTelephone: '',
      email: '',
      position: '',
    });
  }

  get name() {
    return this.profileForm.get('name');
  }

  profileSave(event, value) {
    this.userService.updateUserInfo(value).then(res => {
      if (!res) return;
      if (!res.success) return;
      this.msg.success('修改资料成功');
    });
  }

  pwdSave() {
    if (!this.pwd.oldPassword) {
      return this.msg.error('请输入旧密码');
    }
    if (!this.pwd.newPassword) {
      return this.msg.error('请输入新密码');
    }
    if (!this.pwd.confirmNewPassword) {
      return this.msg.error('请输入确认新密码');
    }
    if (this.pwd.newPassword != this.pwd.confirmNewPassword) {
      return this.msg.error('两次输入的新密码不一致');
    }
    if (this.pwd.newPassword.length < 8 || this.pwd.newPassword.length > 16) {
      return this.msg.error('请输入8~16位新密码');
    }

    this.userService.updatePassword(this.pwd).then(res => {
      if (!res) return;
      if (!res.success) {
        this.msg.error(res.err.message);
        return;
      }
      this.msg.success('修改密码成功,请重新登录');
      setTimeout(() => {
        this.tokenService.clear();
        this.router.navigateByUrl(this.tokenService.login_url);
      }, 1000);
    });
  }

  ngOnInit() {
    this.userService.getUserInfo().then(res => {
      if (!res) return;
      this.profileForm.patchValue(res['result']);
      this.avatarUrl = res['result'].avatarUrl;
    });
  }

  beforeUpload = (file: File) => {
    const isJPG = file.type === 'image/jpeg';
    if (!isJPG) {
      this.msg.error('仅支持jpg格式头像');
    }
    const isLt2M = file.size / 1024 / 1024 < 2;
    if (!isLt2M) {
      this.msg.error('头像大小不能超过2mb');
    }
    return isJPG && isLt2M;
  }

  private getBase64(img: File, callback: (img: {}) => void): void {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(reader.result));
    reader.readAsDataURL(img);
  }

  handleChange(info: { file: UploadFile }): void {
    if (info.file.status === 'uploading') {
      this.loading = true;
      return;
    }
    if (info.file.status === 'done') {
      this.getBase64(info.file.originFileObj, (img: string) => {
        this.loading = false;
        this.avatarUrl = img;
      });

      if (
        info.file.response.success === true &&
        info.file.response.result &&
        info.file.response.result.length == 1
      ) {
        const fileUrl = info.file.response.result[0].fileUrl;
        if (fileUrl)
          this.userService.updateAvatar(fileUrl);
      }
    }
  }

}
