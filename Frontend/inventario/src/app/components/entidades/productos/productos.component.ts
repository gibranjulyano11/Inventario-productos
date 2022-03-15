import { Component, OnInit,ViewChild,OnDestroy,AfterViewInit } from '@angular/core';
import Swal from 'sweetalert2';
import { ModalService } from '../modal.service';
import { Producto } from './producto';
import { ProductoService } from './producto.service';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { CategoriaService } from '../categorias/categoria.service';
import { Categoria } from '../categorias/categoria';
import { Marca } from '../marcas/marca';
import { MarcaService } from '../marcas/marca.service';
import { Atributo } from '../atributos/atributo';
import { AtributoService } from '../atributos/atributo.service';
// import { Producto1 } from './producto1';

@Component({
  selector: 'app-productos',
  templateUrl: './productos.component.html',
  styleUrls: [

  ]
})
export class ProductosComponent implements OnInit {

  data: any;
  @ViewChild(DataTableDirective, {static: false})
  dtElement: DataTableDirective;
  dtOptions: DataTables.Settings = {retrieve: true};
  dtTrigger: Subject<any> = new Subject();

  categorias: Categoria[];
  productos: Producto[];
  rootNode: any;
  marcas: Marca[];
  atributos: Atributo[];

  productoSeleccionado: Producto;
  // productoSeleccionado1: Producto1;

 //SE HA INYECTADO EL MODAL SERVICE CREADO EN LA CARPETA DE ENTIDADES
  constructor(private productoService: ProductoService, public modalService: ModalService,
    private categoriaservice: CategoriaService,
    private atributoService: AtributoService,
    private marcaservice: MarcaService) { }

  ngOnInit(): void {
    this.data = [];
    this.cargarProducto();

  }

  cargarProducto(){
    this.productoService.getProductos().subscribe((data) => {
      this.productos = data;
      this.dtTrigger.next();
    });
    this.categoriaservice.getCategorias().subscribe((categoria)=>{
      this.categorias = categoria;
    });
    this.marcaservice.getMarcas().subscribe((marca) =>{
      this.marcas = marca;
    });
    this.atributoService.getAtributos().subscribe((atributo) =>{
      this.atributos = atributo;
    })
  }


  delete(producto: Producto): void {

    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
      },
      buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
      title: 'Estás Seguro?',
      text: `Está seguro que desea eliminar el producto? ${producto.name}`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Si, Eliminar!',
      cancelButtonText: 'No, Cancelar!',
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {
        console.log(producto.id)
        this.productoService.delete(producto.id).subscribe(

          response => {
            this.rerender();
            this.cargarProducto();
            swalWithBootstrapButtons.fire(
              'Eliminado!',
              //'El producto ha sido eliminado',
              `Producto ${producto.name} eliminado con éxito.`,
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


  abrirModal(productos: Producto){
    this.productoSeleccionado = productos;
    console.log(this.productoSeleccionado, 'Este es el producto')
    this.modalService.abrirModal();
  }

  abrirModalNuevo(){
    this.productoSeleccionado = new Producto();
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
