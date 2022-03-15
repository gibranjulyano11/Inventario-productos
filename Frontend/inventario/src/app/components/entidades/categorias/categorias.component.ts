import { Component, OnInit,ViewChild,OnDestroy,AfterViewInit } from '@angular/core';
import Swal from 'sweetalert2';
import { ModalService } from '../modal.service';
import { Categoria } from './categoria';
import { CategoriaService } from './categoria.service';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-categorias',
  templateUrl: './categorias.component.html',
  styleUrls: [

  ]
})
export class CategoriasComponent implements OnInit {

  data: any;
  @ViewChild(DataTableDirective, {static: false})
  dtElement: DataTableDirective;
  dtOptions: DataTables.Settings = {retrieve: true};
  dtTrigger: Subject<any> = new Subject();


  categorias: Categoria[];
  rootNode: any;

  //NUEVA VARIABLE CREADA DEL TIPO PERSONA DONDE SE VA A ALOJAR LA PERSONA SELECCIONADA PARA MOSTRAR LA INOFMRACIÓN DE LA PERSONA
  categoriaSeleccionado: Categoria;

 //SE HA INYECTADO EL MODAL SERVICE CREADO EN LA CARPETA DE ENTIDADES
  constructor(private categoriaservice: CategoriaService, public modalService: ModalService) { }

  ngOnInit(): void {
    this.data = [];
    this.cargarCategoria();

  }

  cargarCategoria(){
    this.categoriaservice.getCategorias().subscribe((data) => {
      this.categorias = data;
      this.dtTrigger.next();
    })

  }


  delete(categoria: Categoria): void {

    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
      },
      buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
      title: 'Estás Seguro?',
      text: `Está seguro que desea eliminar el producto? ${categoria.name}`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Si, Eliminar!',
      cancelButtonText: 'No, Cancelar!',
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {
        console.log(categoria.id)
        this.categoriaservice.delete(categoria.id).subscribe(

          response => {
            this.rerender();
            this.cargarCategoria();
            swalWithBootstrapButtons.fire(
              'Eliminado!',
              //'El producto ha sido eliminado',
              `Categoria ${categoria.name} eliminada con éxito.`,
              'success'
            )
          }
        )
      } else if (
        /* Read more about handling dismissals below */
        result.dismiss === Swal.DismissReason.cancel
      ) {
        swalWithBootstrapButtons.fire(
          'Cancelado',
          'Se ha cancelado la operación',
          'error'
        )
      }
    })
  }


  //METODO PARA ASIGNAR LOS DATOS DE LA EMPRESA SELECCIONADA Y CAMBIA EL ESTADO DEL MODAL
  abrirModal(categoria: Categoria){
    this.categoriaSeleccionado = categoria;
    this.modalService.abrirModal();
  }

  //METODO PARA ASIGNAR LOS DATOS DE LA EMPRESA COMO NUEVO PARA LA CREACION DE PERSONA Y CAMBIA EL ESTADO DEL MODAL
  abrirModalNuevo(){
    this.categoriaSeleccionado = new Categoria();
    this.modalService.abrirModal();
  }

  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      // Destroy the table first
      dtInstance.destroy();
      // Call the dtTrigger to rerender again
      this.dtTrigger.next();
    });
  }

}
