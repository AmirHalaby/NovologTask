import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";

@Injectable({
    providedIn:'root'
})
export class DisplayDoctorsService{
    private url: string = environment.apiBaseurl;    
    constructor(private _http: HttpClient){}

    GetDoctors(): Observable<any>{
        return this._http.get(this.url + '/DisplayDoctors/GetDoctors/')
    }
    contactUs(data: any): Observable<any>{
        return this._http.post(`${this.url}/DisplayDoctors/ContactUs`,data,{responseType: 'text'});
    }

}