import { Component, OnInit,ViewChild,OnDestroy,AfterViewInit } from '@angular/core';
import Swal from 'sweetalert2';
import { ModalService } from '../modal.service';
import { Marca } from './marca';
import { MarcaService } from './marca.service';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-marcas',
  templateUrl: './marcas.component.html',
  styleUrls: [

  ]
})
export class MarcasComponent implements OnInit {

  data: any;
  @ViewChild(DataTableDirective, {static: false})
  dtElement: DataTableDirective;
  dtOptions: DataTables.Settings = {retrieve: true};
  dtTrigger: Subject<any> = new Subject();


  marcas: Marca[];
  rootNode: any;

  //NUEVA VARIABLE CREADA DEL TIPO PERSONA DONDE SE VA A ALOJAR LA PERSONA SELECCIONADA PARA MOSTRAR LA INOFMRACIÓN DE LA PERSONA
  marcaSeleccionado: Marca;

 //SE HA INYECTADO EL MODAL SERVICE CREADO EN LA CARPETA DE ENTIDADES
  constructor(private marcaservice: MarcaService, public modalService: ModalService) { }

  ngOnInit(): void {
    this.data = [];
    this.cargarMarca();

  }

  cargarMarca(){
    this.marcaservice.getMarcas().subscribe((data) => {
      this.marcas = data;
      this.dtTrigger.next();
    })

  }


  delete(marca: Marca): void {

    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
      },
      buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
      title: 'Estás Seguro?',
      text: `Está seguro que desea eliminar el producto? ${marca.name}`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Si, Eliminar!',
      cancelButtonText: 'No, Cancelar!',
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {
        console.log(marca.id)
        this.marcaservice.delete(marca.id).subscribe(

          response => {
            this.rerender();
            this.cargarMarca();
            swalWithBootstrapButtons.fire(
              'Eliminado!',
              //'El producto ha sido eliminado',
              `Categoria ${marca.name} eliminada con éxito.`,
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
  abrirModal(marca: Marca){
    this.marcaSeleccionado = marca;
    this.modalService.abrirModal();
  }

  //METODO PARA ASIGNAR LOS DATOS DE LA EMPRESA COMO NUEVO PARA LA CREACION DE PERSONA Y CAMBIA EL ESTADO DEL MODAL
  abrirModalNuevo(){
    this.marcaSeleccionado = new Marca();
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
