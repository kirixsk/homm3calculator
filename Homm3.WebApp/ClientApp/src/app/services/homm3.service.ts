import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CalculatedMonsterValues, GuessedObjectResult, ReferenceData } from '../model/model';

@Injectable({
  providedIn: 'root'
})
export class Homm3Service {

  constructor(
    private http: HttpClient,
    @Inject('BASE_API_URL') private baseUrl: string) { }

  getReferenceData(): Observable<ReferenceData> {
    return this.http.get<ReferenceData>(this.baseUrl + 'referencedata');
  }

  getMonsterValue(requestBody: any): Observable<CalculatedMonsterValues> {
    return this.http.post<CalculatedMonsterValues>(this.baseUrl + 'Calculator/GetMonsterValue', requestBody)
  }

  guessMapObject(requestBody: any): Observable<GuessedObjectResult[]> {
    return this.http.post<GuessedObjectResult[]>(this.baseUrl + 'Calculator/GuessObject', requestBody)
  }

  saveFormValue(requestBody: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'MiniUrl', { value: JSON.stringify(requestBody) })
  }

  getFormValue(url: any): Observable<any> {
    return this.http.get<any>(this.baseUrl + 'MiniUrl/' + url)
  }
}


