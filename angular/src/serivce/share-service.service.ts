import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
@Injectable({
  providedIn: 'root',
})
export class ShareServiceService {

  public REST_API_SERVER = environment.apis.default.url;
  public API_ACCOUNT = environment.AccountApi.accountApi;
  public API_ACTIVITY = environment.apiActivity.default.url;
  public httpOptions = {
    headers: new HttpHeaders({
      // 'Content-Type': 'application/json',
      Authorization: `Bearer ${this.getToken()}`
    }),
  };

  constructor(private httpClient: HttpClient) {}

  setRequestHeader() {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${this.getToken()}`,
    });
  }
  public returnHttpClient(url: string) {
    return this.httpClient
      .get<any>(url)
      .pipe(catchError((err) => this.handleError(err)));
  }

  public returnHttpClientGet(url: string) {
    return this.httpClient
      .get<any>(url)
      .pipe(catchError((err) => this.handleError(err)));
  }

  public deleteHttpClient(url: string) {
    return this.httpClient
      .delete<any>(url)
      .pipe(catchError((err) => this.handleError(err)));
  }

  public deleteHttpClientParam(url: string, id: string[]) {
    return this.httpClient
      .delete<any>(url, { body: { id } })
      .pipe(catchError((err) => this.handleError(err)));
  }
  public deleteHttpClientBody(url: string, data : any) {
    return this.httpClient
      .delete<any>(url, { body: { data } })
      .pipe(catchError((err) => this.handleError(err)));
  }
  public deleteClientBelongToProfile(url: string, id: any) {
    return this.httpClient
      .delete<any>(url, { body:id })
      .pipe(catchError((err) => this.handleError(err)));
  }
  public postHttpClient(url: string, data: any) {
    return this.httpClient
      .post<any>(url, data)
      .pipe(catchError((err) => this.handleError(err)));
  }
  public postWithURl(url: string) {
    return this.httpClient
      .post<any>(url, null)
      .pipe(catchError((err) => this.handleError(err)));
  }
  public postHttpClientAnonymous(url: string, data: any) {
    return this.httpClient
      .post<any>(url, data)
      .pipe(catchError((err) => this.handleError(err)));
  }

  public putHttpClient(url: string, data: any) {
    return this.httpClient
      .put(url, data)
      .pipe(catchError((err) => this.handleError(err)));
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.log('An error occurred:', error.error.message);
    } else {
      console.log('status: ', error.status, error.message, error);

      // if(error.status == 403) {
      //   localStorage.clear();
      //   this.router.navigate(['sign-in']);
      // }
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.log('error', error.error);
      console.error(
        `Backend returned code ${error.status}, ` + `body was: ${error.error}`
      );
    }
    // Return an observable with a user-facing error message.
    // this.spinnerService.requestSpinner();
    return throwError(error.error);
  }

  writeLocalData(accessToken: string, refreshToken: string, userData: any) {
    localStorage.setItem('accessToken', accessToken);
    localStorage.setItem('refreshToken', refreshToken);
    localStorage.setItem('userData', userData);
    // //   {
    //   headers: new HttpHeaders({
    //     Authorization: `Bearer ${this.getToken()}`,
    //   })
    // } = {
    // //   headers: new HttpHeaders({
    // //     Authorization: `Bearer ${this.getToken()}`,
    // //   })
    // // };
  }

  deleteLocalData() {
    localStorage.clear();
  }
  getToken() {
    return localStorage.getItem('accessToken');
  }
}
