import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Etiqueta } from './etiqueta';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { catchError } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
  })

  @Injectable()
  export class EtiquetaService {

    private urlEndpoint: string = 'http://localhost:5000/categories';

    constructor(private http: HttpClient, private router: Router) { }

    private httpHeaders =  new HttpHeaders({'Content-Type':'application/json'})

     getEtiquetas():Observable<Etiqueta[]>{

      return this.http.get<Etiqueta[]>(`${this.urlEndpoint}/listar`).pipe(
        catchError(e=>{
         // this.isNoAutorizado(e);
          return throwError(e);
        })
      );
      //, {headers: this.agregarAutorizationHeader()}
    }
    create(etiqueta: Etiqueta) : Observable<any>{
      return this.http.post<any>(`${this.urlEndpoint}`, etiqueta).pipe(
       catchError(e => {
          console.error(e.error.mensaje);
          //if(this.isNoAutorizado(e)){
            //return throwError(e);
          //}
         Swal.fire('Error al crear la etiqueta',e.error.mensaje,'error');
        return throwError(e);
       })
      );
    }

    getEtiqueta(id): Observable<Etiqueta>{
      return this.http.get<Etiqueta>(`${this.urlEndpoint}/buscar/${id}`).pipe(
        catchError(e => {
         // if(this.isNoAutorizado(e)){
            return throwError(e);
         // }
         this.router.navigate(['/etiquetas']);
          console.error(e.error.mensaje);
          Swal.fire('Error al editar',e.error.mensaje,'error');
          return throwError(e);
        })
      );
    }

    update(etiqueta: Etiqueta): Observable<any>{
      return this.http.put<any>(`${this.urlEndpoint}/actualizar/${etiqueta.id}`, etiqueta).pipe(
        catchError(e => {
          console.error(e.error.mensaje);
          //if(this.isNoAutorizado(e)){
            return throwError(e);
          //}
          Swal.fire('Error al eliminar la etiqueta',e.error.mensaje,'error');
          return throwError(e);
        })
      );
    }

    delete(id : number): Observable<Etiqueta>{
      return this.http.delete<Etiqueta>(`${this.urlEndpoint}/eliminar/${id}`).pipe(
        catchError(e => {
          console.error(e.error.mensaje);
         // if(this.isNoAutorizado(e)){
            return throwError(e);
         // }
          Swal.fire('Error al eliminar la etiqueta',e.error.mensaje,'error');
          return throwError(e);
        })
      );
    }

  }
