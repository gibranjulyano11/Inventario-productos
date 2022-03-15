import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Atributo } from './atributo';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { catchError } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
  })

  @Injectable()
  export class AtributoService {

    private urlEndpoint: string = 'http://localhost:5000/api/attributes';

    constructor(private http: HttpClient, private router: Router) { }

    private httpHeaders =  new HttpHeaders({'Content-Type':'application/json'})

     getAtributos():Observable<Atributo[]>{

      return this.http.get<Atributo[]>(`${this.urlEndpoint}`).pipe(
        catchError(e=>{
         // this.isNoAutorizado(e);
          return throwError(e);
        })
      );
      //, {headers: this.agregarAutorizationHeader()}
    }
    create(atributo: Atributo) : Observable<any>{
      return this.http.post(`${this.urlEndpoint}`, atributo, {responseType: 'text'}).pipe(
       catchError(e => {
          console.error(e.error.mensaje);
          //if(this.isNoAutorizado(e)){
            //return throwError(e);
          //}
         Swal.fire('Error al crear el atributo',e.error.mensaje,'error');
        return throwError(e);
       })
      );
    }

    getAtributo(id): Observable<Atributo>{
      return this.http.get<Atributo>(`${this.urlEndpoint}/buscar/${id}`).pipe(
        catchError(e => {
         // if(this.isNoAutorizado(e)){
            return throwError(e);
         // }
         this.router.navigate(['/atributos']);
          console.error(e.error.mensaje);
          Swal.fire('Error al editar',e.error.mensaje,'error');
          return throwError(e);
        })
      );
    }

    update(atributo: Atributo): Observable<any>{
      return this.http.put(`${this.urlEndpoint}`, atributo, {responseType: 'text'}).pipe(
        catchError(e => {
          console.error(e.error.mensaje);
          //if(this.isNoAutorizado(e)){
            return throwError(e);
          //}
          Swal.fire('Error al eliminar el atributo',e.error.mensaje,'error');
          return throwError(e);
        })
      );
    }

    delete(id : string): Observable<any>{
      return this.http.delete<any>(`${this.urlEndpoint}/${id}`,  {headers: this.httpHeaders}).pipe(
        catchError(e => {
          console.error(e.error.mensaje);
         // if(this.isNoAutorizado(e)){
            return throwError(e);
         // }
          Swal.fire('Error al eliminar el atributo',e.error.mensaje,'error');
          return throwError(e);
        })
      );
    }

  }
