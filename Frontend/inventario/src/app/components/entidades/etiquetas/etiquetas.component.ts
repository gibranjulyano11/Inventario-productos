import { Component, OnInit,ViewChild,OnDestroy,AfterViewInit } from '@angular/core';
import Swal from 'sweetalert2';
import { ModalService } from '../modal.service';
import { Etiqueta } from './etiqueta';
import { EtiquetaService } from './etiqueta.service';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-etiquetas',
  templateUrl: './etiquetas.component.html',
  styleUrls: [

  ]
})
export class EtiquetasComponent implements OnInit {

  data: any;
  @ViewChild(DataTableDirective, {static: false})
  dtElement: DataTableDirective;
  dtOptions: DataTables.Settings = {retrieve: true};
  dtTrigger: Subject<any> = new Subject();


  etiqueta: Etiqueta[];
  rootNode: any;

  //NUEVA VARIABLE CREADA DEL TIPO PERSONA DONDE SE VA A ALOJAR LA PERSONA SELECCIONADA PARA MOSTRAR LA INOFMRACIÓN DE LA PERSONA
  etiquetaSeleccionado: Etiqueta;

 //SE HA INYECTADO EL MODAL SERVICE CREADO EN LA CARPETA DE ENTIDADES
  constructor(private etiquetaservice: EtiquetaService, public modalService: ModalService) { }

  ngOnInit(): void {
    this.data = [];
    this.cargarEtiqueta();

  }

  cargarEtiqueta(){
    this.etiquetaservice.getEtiquetas().subscribe((data) => {
      this.etiqueta = data;
      this.dtTrigger.next();
    })

  }


  delete(etiqueta: Etiqueta): void {
    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
      },
      buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
      title: 'Estás Seguro?',
      text: `Está seguro que desea eliminar la etiqueta? ${etiqueta.name}?`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Si, Eliminar!',
      cancelButtonText: 'No, Cancelar!',
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {
        this.etiquetaservice.delete(etiqueta.id).subscribe(
          response => {
            this.etiqueta = this.etiqueta.filter(eti => eti!== etiqueta)
            swalWithBootstrapButtons.fire(
              'Eliminado!',
              'La etiqueta ha sido eliminada',
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
  abrirModal(etiqueta: Etiqueta){
    this.etiquetaSeleccionado = etiqueta;
    this.modalService.abrirModal();
  }

  //METODO PARA ASIGNAR LOS DATOS DE LA EMPRESA COMO NUEVO PARA LA CREACION DE PERSONA Y CAMBIA EL ESTADO DEL MODAL
  abrirModalNuevo(){
    this.etiquetaSeleccionado = new Etiqueta();
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
