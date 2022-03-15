import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Marca } from './marca';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { catchError } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
  })

  @Injectable()
  export class MarcaService {

    private urlEndpoint: string = 'http://localhost:5000/api/brands';

    constructor(private http: HttpClient, private router: Router) { }

    private httpHeaders =  new HttpHeaders({'Content-Type':'application/json'})

     getMarcas():Observable<Marca[]>{

      return this.http.get<Marca[]>(`${this.urlEndpoint}`,  {headers: this.httpHeaders}).pipe(
        catchError(e=>{
         // this.isNoAutorizado(e);
          return throwError(e);
        })
      );
      //, {headers: this.agregarAutorizationHeader()}
    }
    create(marca: Marca) : Observable<any>{
      return this.http.post(`${this.urlEndpoint}`, marca,  {responseType: 'text'}).pipe(
       catchError(e => {
          console.error(e.error.mensaje);
          //if(this.isNoAutorizado(e)){
            //return throwError(e);
          //}
         Swal.fire('Error al crear la marca',e.error.mensaje,'error');
        return throwError(e);
       })
      );
    }

    getMarca(id): Observable<Marca>{
      return this.http.get<Marca>(`${this.urlEndpoint}/${id}`,  {headers: this.httpHeaders}).pipe(
        catchError(e => {
         // if(this.isNoAutorizado(e)){
            return throwError(e);
         // }
         this.router.navigate(['/marcas']);
          console.error(e.error.mensaje);
          Swal.fire('Error al editar',e.error.mensaje,'error');
          return throwError(e);
        })
      );
    }

    update(marca: Marca): Observable<any>{
      return this.http.put(`${this.urlEndpoint}`, marca ,  {responseType: 'text'}).pipe(
        catchError(e => {
          console.error(e.error.mensaje);
          //if(this.isNoAutorizado(e)){
            return throwError(e);
          //}
          Swal.fire('Error al eliminar la marca',e.error.mensaje,'error');
          return throwError(e);
        })
      );
    }

    delete(id : number): Observable<Marca>{
      return this.http.delete<Marca>(`${this.urlEndpoint}/${id}`,  {headers: this.httpHeaders}).pipe(
        catchError(e => {
          console.error(e.error.mensaje);
         // if(this.isNoAutorizado(e)){
            return throwError(e);
         // }
          Swal.fire('Error al eliminar la marca',e.error.mensaje,'error');
          return throwError(e);
        })
      );
    }

  }
