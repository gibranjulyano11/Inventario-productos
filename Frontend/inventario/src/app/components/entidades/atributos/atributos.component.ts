import { Component, OnInit,ViewChild,OnDestroy,AfterViewInit } from '@angular/core';
import Swal from 'sweetalert2';
import { ModalService } from '../modal.service';
import { Atributo } from './atributo';
import { AtributoService } from './atributo.service';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-atributos',
  templateUrl: './atributos.component.html',
  styleUrls: [

  ]
})
export class AtributosComponent implements OnInit {

  data: any;
  @ViewChild(DataTableDirective, {static: false})
  dtElement: DataTableDirective;
  dtOptions: DataTables.Settings = {retrieve: true};
  dtTrigger: Subject<any> = new Subject();


  atributos: Atributo[];
  rootNode: any;

  //NUEVA VARIABLE CREADA DEL TIPO PERSONA DONDE SE VA A ALOJAR LA PERSONA SELECCIONADA PARA MOSTRAR LA INOFMRACIÓN DE LA PERSONA
  atributoSeleccionado: Atributo;

 //SE HA INYECTADO EL MODAL SERVICE CREADO EN LA CARPETA DE ENTIDADES
  constructor(private atributoservice: AtributoService, public modalService: ModalService) { }

  ngOnInit(): void {
    this.data = [];
    this.cargarAtributo();

  }

  cargarAtributo(){
    this.atributoservice.getAtributos().subscribe((data) => {
      this.atributos = data;
      this.dtTrigger.next();
    })

  }


  delete(atributo: Atributo): void {
    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
      },
      buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
      title: 'Estás Seguro?',
      text: `Está seguro que desea eliminar el atributo? ${atributo.name}?`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Si, Eliminar!',
      cancelButtonText: 'No, Cancelar!',
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {
        this.atributoservice.delete(atributo.id).subscribe(
          response => {
            this.atributos = this.atributos.filter(atr => atr!== atributo)
            swalWithBootstrapButtons.fire(
              'Eliminado!',
              'El atributo ha sido eliminado',
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
  abrirModal(atributo: Atributo){
    this.atributoSeleccionado = atributo;
    this.modalService.abrirModal();
  }

  //METODO PARA ASIGNAR LOS DATOS DE LA EMPRESA COMO NUEVO PARA LA CREACION DE PERSONA Y CAMBIA EL ESTADO DEL MODAL
  abrirModalNuevo(){
    this.atributoSeleccionado = new Atributo();
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
