import { Component, OnInit, Input } from '@angular/core';
import swal from 'sweetalert2';
import { Atributo } from './atributo';
import { AtributoService } from './atributo.service';
import { ModalService } from '../modal.service';
import { AtributosComponent } from './atributos.component';

@Component({
    selector: 'app-formatributo',
    templateUrl: './formatributo.component.html'
  })

  export class FormatributoComponent implements OnInit {

    //SE INYECTA LA CLASE PRODUCTO

    @Input() atributo: Atributo;

    //SE HA INYECTADO EL MODAL SERVICE CREADO EN LA CARPETA DE ENTIDADES

    constructor(private atributoService: AtributoService,
       public modalservice: ModalService, private atributocom: AtributosComponent) { }

    //SE HA BORRADO EL METODO CARGAR PERSONA YA QUE NO SE VA A REALIZAR LA BUSQUEDA POR ID SOLO SE VA A EJECUTAR LOS METODOS PARA LLENAR LOS SELECT
        ngOnInit(): void {

          }

          create():void{
            this.atributoService.create(this.atributo)
            .subscribe(atributo => {
              this.atributocom.cargarAtributo();
              this.atributocom.rerender();

              swal.fire('Nuevo Atributo', `Atributo ${this.atributo.name} creado con éxito`, 'success')
            })
            this.cerrarModal();
          }

          update():void{
            this.atributoService.update(this.atributo)
            .subscribe(atributo => {
              this.atributocom.rerender();
              this.atributocom.cargarAtributo();
              swal.fire('Atributo Actualizado', `Atributo ${this.atributo.name} actualizado con éxito`, 'success')
            })
            this.cerrarModal();
          }

    //METODO PARA CAMBIA EL ESTADO DEL MODAL
  cerrarModal() {
    this.modalservice.cerrarModal();

  }

  }
