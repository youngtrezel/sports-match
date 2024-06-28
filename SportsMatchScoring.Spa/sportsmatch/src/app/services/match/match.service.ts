import { Injectable, inject, ErrorHandler, } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Match } from '../../../models/match';

@Injectable({
  providedIn: 'root'
})
export class MatchService {

  constructor(private http: HttpClient) { }

  postMatch(matchRequest:Match) : Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }
    return this.http.post('http://localhost:5011/api/score/processgame', matchRequest, httpOptions).pipe();
  }

  getAllMatches() : Observable<any> {
    const headers = new HttpHeaders().append('Content-Type', 'application/json');
    return this.http.get('http://localhost:5011/api/score/getmatchrecords', { headers }).pipe();
  }

  getMatchByTeamName(name: string) : Observable<any> {
    const headers = new HttpHeaders().append('Content-Type', 'application/json');
    const params = new HttpParams().append('name', name);
    return this.http.get('http://localhost:5011/api/score/getmatchbyteam', { headers, params }).pipe();
  }

  getMatchById(id: string) : Observable<any> {
    const headers = new HttpHeaders().append('Content-Type', 'application/json');
    const params = new HttpParams().append('id', id);
    return this.http.get('http://localhost:5011/api/score/getmatchbyid', { headers, params }).pipe();
  }

}
