import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { _HttpClient } from '@delon/theme';

@Injectable()
export class HttpService {
  constructor(private http: _HttpClient) {}

  public get(url: string, paramObj: any) {
    return this.http.get(url + this.toQueryString(paramObj)).toPromise();
  }

  public post(url: string, paramObj: any) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/x-www-form-urlencoded',
    });
    return this.http
      .post(url, this.toBodyString(paramObj), undefined, { headers: headers })
      .toPromise();
  }

  public postBody(url: string, paramObj: any) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http
      .post(url, paramObj, undefined, { headers: headers })
      .toPromise();
  }

  public delete(url: string) {
    return this.http.delete(url).toPromise();
  }

  public put(url: string, paramObj: any) {
    return this.http.put(url, paramObj).toPromise();
  }

  //   private handleSuccess(result) {
  //     if (result && !result.success) {
  //       //由于和后台约定好,所有请求均返回一个包含success,msg,data三个属性的对象,所以这里可以这样处理
  //       alert(result.msg); //这里使用ToastController
  //     }
  //     return result;
  //   }

  //   private handleError(error: Response | any) {
  //     let msg = '请求失败';
  //     if (error.status == 0) {
  //       msg = '请求地址错误';
  //     }
  //     if (error.status == 400) {
  //       msg = '请求无效';
  //       console.log('请检查参数类型是否匹配');
  //     }
  //     if (error.status == 404) {
  //       msg = '请求资源不存在';
  //       console.error(msg + '，请检查路径是否正确');
  //     }
  //     console.log(error);
  //     alert(msg); //这里使用ToastController
  //     return { success: false, msg: msg };
  //   }

  /**
   * @param obj　参数对象
   * @return {string}　参数字符串
   * @example
   *  声明: var obj= {'name':'小军',age:23};
   *  调用: toQueryString(obj);
   *  返回: "?name=%E5%B0%8F%E5%86%9B&age=23"
   */
  private toQueryString(obj): string {
    let ret = [];
    for (let key in obj) {
      key = encodeURIComponent(key);
      const values = obj[key];
      if (values && values.constructor === Array) {
        // 数组
        const queryValues = [];
        for (let i = 0, len = values.length, value; i < len; i++) {
          value = values[i];
          queryValues.push(this.toQueryPair(key, value));
        }
        ret = ret.concat(queryValues);
      } else {
        // 字符串
        ret.push(this.toQueryPair(key, values));
      }
    }
    return '?' + ret.join('&');
  }

  /**
   *
   * @param obj
   * @return {string}
   *  声明: var obj= {'name':'小军',age:23};
   *  调用: toQueryString(obj);
   *  返回: "name=%E5%B0%8F%E5%86%9B&age=23"
   */
  private toBodyString(obj): string {
    let ret = [];
    for (let key in obj) {
      key = encodeURIComponent(key);
      const values = obj[key];
      if (values && values.constructor === Array) {
        // 数组
        const queryValues = [];
        for (let i = 0, len = values.length, value; i < len; i++) {
          value = values[i];
          queryValues.push(this.toQueryPair(key, value));
        }
        ret = ret.concat(queryValues);
      } else {
        // 字符串
        ret.push(this.toQueryPair(key, values));
      }
    }
    return ret.join('&');
  }

  private toQueryPair(key, value) {
    if (typeof value === 'undefined') {
      return key;
    }
    return key + '=' + encodeURIComponent(value === null ? '' : String(value));
  }
}
