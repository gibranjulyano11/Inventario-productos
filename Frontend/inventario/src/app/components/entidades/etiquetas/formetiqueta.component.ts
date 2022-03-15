import { Component, OnInit, Input } from '@angular/core';
import swal from 'sweetalert2';
import { Etiqueta } from './etiqueta';
import { EtiquetaService } from './etiqueta.service';
import { ModalService } from '../modal.service';
import { EtiquetasComponent } from './etiquetas.component';

@Component({
    selector: 'app-formetiqueta',
    templateUrl: './formetiqueta.component.html'
  })

  export class FormetiquetaComponent implements OnInit {

    //SE INYECTA LA CLASE PRODUCTO

    @Input() etiqueta: Etiqueta;

    //SE HA INYECTADO EL MODAL SERVICE CREADO EN LA CARPETA DE ENTIDADES

    constructor(private etiquetaService: EtiquetaService,
       public modalservice: ModalService, private etiquetacom: EtiquetasComponent) { }

    //SE HA BORRADO EL METODO CARGAR PERSONA YA QUE NO SE VA A REALIZAR LA BUSQUEDA POR ID SOLO SE VA A EJECUTAR LOS METODOS PARA LLENAR LOS SELECT
        ngOnInit(): void {

          }

          create():void{
            this.etiquetaService.create(this.etiqueta)
            .subscribe(etiqueta => {
              this.etiquetacom.cargarEtiqueta();
              this.etiquetacom.rerender();

              swal.fire('Nueva Etiqueta', `Etiqueta ${this.etiqueta.name} creada con éxito`, 'success')
            })
            this.cerrarModal();
          }

          update():void{
            this.etiquetaService.update(this.etiqueta)
            .subscribe(etiqueta => {
              this.etiquetacom.rerender();
              this.etiquetacom.cargarEtiqueta();
              swal.fire('Etiqueta Actualizada', `Etiqueta ${this.etiqueta.name} actualizada con éxito`, 'success')
            })
            this.cerrarModal();
          }

    //METODO PARA CAMBIA EL ESTADO DEL MODAL
  cerrarModal() {
    this.modalservice.cerrarModal();

  }

  }
