import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Categoria } from './categoria';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { catchError } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
  })

  @Injectable()
  export class CategoriaService {

    private urlEndpoint: string = 'http://localhost:5000/api/categories';

    constructor(private http: HttpClient, private router: Router) { }

    private httpHeaders =  new HttpHeaders({'Content-Type':'application/json'})

     getCategorias():Observable<Categoria[]>{

      return this.http.get<Categoria[]>(`${this.urlEndpoint}`).pipe(
        catchError(e=>{
         // this.isNoAutorizado(e);
          return throwError(e);
        })
      );
      //, {headers: this.agregarAutorizationHeader()}
    }
    create(categoria: Categoria) : Observable<any>{
      return this.http.post(`${this.urlEndpoint}`, categoria, {responseType: 'text'}).pipe(
       catchError(e => {
          console.error(e.error.mensaje);
          //if(this.isNoAutorizado(e)){
            //return throwError(e);
          //}
         Swal.fire('Error al crear la categoria',e.error.mensaje,'error');
        return throwError(e);
       })
      );
    }

    getCategoria(id): Observable<Categoria>{
      return this.http.get<Categoria>(`${this.urlEndpoint}/${id}`).pipe(
        catchError(e => {
         // if(this.isNoAutorizado(e)){
            return throwError(e);
         // }
         this.router.navigate(['/categorias']);
          console.error(e.error.mensaje);
          Swal.fire('Error al editar',e.error.mensaje,'error');
          return throwError(e);
        })
      );
    }

    update(categoria: Categoria): Observable<any>{
      return this.http.put(`${this.urlEndpoint}`, categoria, {responseType: 'text'}).pipe(
        catchError(e => {
          console.error(e.error.mensaje);
          //if(this.isNoAutorizado(e)){
            return throwError(e);
          //}
          Swal.fire('Error al eliminar la categoría',e.error.mensaje,'error');
          return throwError(e);
        })
      );
    }

    delete(id : string): Observable<Categoria>{
      return this.http.delete<Categoria>(`${this.urlEndpoint}/${id}`).pipe(
        catchError(e => {
          console.error(e.error.mensaje);
         // if(this.isNoAutorizado(e)){
            return throwError(e);
         // }
          Swal.fire('Error al eliminar la categoría',e.error.mensaje,'error');
          return throwError(e);
        })
      );
    }

  }
