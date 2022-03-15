import { Component, OnInit, Input } from '@angular/core';
import swal from 'sweetalert2';
import { Marca } from './marca';
import { MarcaService } from './marca.service';
import { ModalService } from '../modal.service';
import { MarcasComponent } from './marcas.component';

@Component({
    selector: 'app-formmarca',
    templateUrl: './formmarca.component.html'
  })

  export class FormmarcaComponent implements OnInit {

    //SE INYECTA LA CLASE PRODUCTO

    @Input() marca: Marca;

    //SE HA INYECTADO EL MODAL SERVICE CREADO EN LA CARPETA DE ENTIDADES

    constructor(private marcaService: MarcaService,
      public modalservice: ModalService, private marcacom: MarcasComponent) { }


    //SE HA BORRADO EL METODO CARGAR PERSONA YA QUE NO SE VA A REALIZAR LA BUSQUEDA POR ID SOLO SE VA A EJECUTAR LOS METODOS PARA LLENAR LOS SELECT
        ngOnInit(): void {

          }

          create():void{
            this.marcaService.create(this.marca)
            .subscribe(marca => {
              this.marcacom.cargarMarca();
              this.marcacom.rerender();

              swal.fire('Nueva Marca', `Marca ${this.marca.name} creada con éxito`, 'success')
            })
            this.cerrarModal();
          }

          update():void{
            this.marcaService.update(this.marca)
            .subscribe(marca => {
              this.marcacom.rerender();
              this.marcacom.cargarMarca();
              swal.fire('Marca Actualizada', `Marca ${this.marca.name} actualizada con éxito`, 'success')
            })
            this.cerrarModal();
          }

    //METODO PARA CAMBIA EL ESTADO DEL MODAL
  cerrarModal() {
    this.modalservice.cerrarModal();

  }

  }
