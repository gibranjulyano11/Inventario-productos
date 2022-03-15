import { Component, OnInit, Input } from '@angular/core';
import swal from 'sweetalert2';
import { Producto } from './producto';
import { ProductoService } from './producto.service';
import { ModalService } from '../modal.service';
import { ProductosComponent } from './productos.component';
import { Router, ActivatedRoute } from '@angular/router'
import { CategoriaService } from '../categorias/categoria.service';
import { Categoria } from '../categorias/categoria';
import { MarcaService } from '../marcas/marca.service';
import { Marca } from '../marcas/marca';
import { AtributoService } from '../atributos/atributo.service';
import { Atributo } from '../atributos/atributo';


@Component({
    selector: 'app-formproducto',
    templateUrl: './formproducto.component.html'
  })

  export class FormproductoComponent implements OnInit {

    //SE INYECTA LA CLASE PRODUCTO


    @Input() producto: Producto;
    categoria: Categoria[];
    marca: Marca[];
    atributo: Atributo[];



    //SE HA INYECTADO EL MODAL SERVICE CREADO EN LA CARPETA DE ENTIDADES

    constructor(private productoService: ProductoService,
      public activatedRoute: ActivatedRoute,
      public modalservice: ModalService, private productocom: ProductosComponent,
      private categoriaservice: CategoriaService,
      private atributoService: AtributoService,
      private marcaservice: MarcaService) { }

    //SE HA BORRADO EL METODO CARGAR PERSONA YA QUE NO SE VA A REALIZAR LA BUSQUEDA POR ID SOLO SE VA A EJECUTAR LOS METODOS PARA LLENAR LOS SELECT
        ngOnInit(): void {
          this.cargarProducto()

          }

          cargarProducto(): void{
            this.activatedRoute.params.subscribe(params =>{
              let id = params['id']
              if(id){
                this.productoService.getProducto(id).subscribe( (producto) => this.producto = producto)
              }
            });
             this.categoriaservice.getCategorias().subscribe((categoria)=>{
              this.categoria = categoria;
            });
            this.marcaservice.getMarcas().subscribe((marca) =>{
              this.marca = marca;
            });
            this.atributoService.getAtributos().subscribe((atributo) =>{
              this.atributo = atributo;
            })
          }

          create():void{
            console.log(this.producto, 'Esta es la data')
            this.productoService.create(this.producto)
            .subscribe(producto => {
              this.productocom.cargarProducto();
              this.productocom.rerender();

              swal.fire('Nuevo Producto', `Producto ${this.producto.name} creado con éxito`, 'success')
            })
            this.cerrarModal();
          }

          update():void{
            this.productoService.update(this.producto)
            .subscribe(producto => {
              this.productocom.cargarProducto();
              this.productocom.rerender();
              swal.fire('Producto Actualizado', `Producto ${this.producto.name} actualizado con éxito`, 'success')
            })
            this.cerrarModal();
          }

    //METODO PARA CAMBIA EL ESTADO DEL MODAL
  cerrarModal() {
    this.modalservice.cerrarModal();

  }
}
