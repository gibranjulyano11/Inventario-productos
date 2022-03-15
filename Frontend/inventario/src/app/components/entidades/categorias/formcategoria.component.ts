import { Component, OnInit, Input } from '@angular/core';
import swal from 'sweetalert2';
import { Categoria } from './categoria';
import { CategoriaService } from './categoria.service';
import { ModalService } from '../modal.service';
import { CategoriasComponent } from './categorias.component';

@Component({
    selector: 'app-formcategoria',
    templateUrl: './formcategoria.component.html'
  })

  export class FormcategoriaComponent implements OnInit {

    //SE INYECTA LA CLASE PRODUCTO

    @Input() categoria: Categoria;

    //SE HA INYECTADO EL MODAL SERVICE CREADO EN LA CARPETA DE ENTIDADES

    constructor(private categoriaService: CategoriaService,
       public modalservice: ModalService, private categoriacom: CategoriasComponent) { }

    //SE HA BORRADO EL METODO CARGAR PERSONA YA QUE NO SE VA A REALIZAR LA BUSQUEDA POR ID SOLO SE VA A EJECUTAR LOS METODOS PARA LLENAR LOS SELECT
        ngOnInit(): void {

          }

          create():void{
            this.categoriaService.create(this.categoria)
            .subscribe(categoria => {
              this.categoriacom.cargarCategoria();
              this.categoriacom.rerender();

              swal.fire('Nueva Categoría', `Categoría ${this.categoria.name} creada con éxito`, 'success')
            })
            this.cerrarModal();
          }

          update():void{
            this.categoriaService.update(this.categoria)
            .subscribe(categoria => {
              this.categoriacom.rerender();
              this.categoriacom.cargarCategoria();
              swal.fire('Categoría Actualizada', `Categoría ${this.categoria.name} actualizada con éxito`, 'success')
            })
            this.cerrarModal();
          }

    //METODO PARA CAMBIA EL ESTADO DEL MODAL
  cerrarModal() {
    this.modalservice.cerrarModal();

  }

  }
