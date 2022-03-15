import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Producto } from './producto';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { catchError, map } from 'rxjs/operators';
// import { Producto1 } from './producto1';

@Injectable({
    providedIn: 'root'
  })

  @Injectable()
  export class ProductoService {

    private urlEndpoint: string = 'http://localhost:5000/api/products';
    private httpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' })


    constructor(private http: HttpClient, private router: Router) { }
    //private authService: AuthService

    //private httpHeaders =  new HttpHeaders({'Content-Type':'application/json'})

     getProductos():Observable<Producto[]>{

      return this.http.get<Producto[]>(`${this.urlEndpoint}`,  {headers: this.httpHeaders}).pipe(
        catchError(e=>{
         // this.isNoAutorizado(e);
         console.error(e.error.mensaje);
          return throwError(e);
        })
      );
      //, {headers: this.agregarAutorizationHeader()}
    }

    create(producto: Producto) : Observable<any>{
     return this.http.post(`${this.urlEndpoint}`,producto,{responseType: 'text'}).pipe(
        //return this.http.post<any>(this.urlEndpoint, producto).pipe(
       catchError(e => {
        console.error(e.error.mensaje);
         Swal.fire('Error al crear el producto',e.error.mensaje,'error');
        return throwError(e);
       })
      );
    }

    getProducto(Id): Observable<Producto>{
      return this.http.get<Producto>(`${this.urlEndpoint}/listar/${Id}`,  {headers: this.httpHeaders}).pipe(
        catchError(e => {
         // if(this.isNoAutorizado(e)){
         // }
          console.error(e.error.mensaje);
          Swal.fire('Error al editar',e.error.mensaje,'error');
          return throwError(e);
        })
      );
    }

    update(producto: Producto): Observable<any>{
      return this.http.put(`${this.urlEndpoint}`, producto,  {responseType: 'text'}).pipe(
        catchError(e => {
          console.error(e.error.mensaje);
          //if(this.isNoAutorizado(e)){
          //}
          Swal.fire('Error al actualizar el producto',e.error.mensaje,'error');
          return throwError(e);
        })
      );
    }

    delete(id : string): Observable<any>{
      return this.http.delete<any>(`${this.urlEndpoint}/${id}`,  {headers: this.httpHeaders}).pipe(
        catchError(e => {
          console.error(e.error.mensaje);
          Swal.fire('Error al eliminar el producto',e.error.mensaje,'error');
          return throwError(e);
        })
      );
    }

  }
