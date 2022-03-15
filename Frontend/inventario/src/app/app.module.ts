import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/shared/header/header.component';
import { ContentComponent } from './components/shared/content/content.component';
import { FooterComponent } from './components/shared/footer/footer.component';
import { MenuComponent } from './components/shared/menu/menu.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ProductosComponent } from './components/entidades/productos/productos.component';
import { CategoriasComponent } from './components/entidades/categorias/categorias.component';
import { MarcasComponent } from './components/entidades/marcas/marcas.component';
import { EtiquetasComponent } from './components/entidades/etiquetas/etiquetas.component';
import { AtributosComponent } from './components/entidades/atributos/atributos.component';

import { FormproductoComponent } from './components/entidades/productos/formproducto.component';
import { FormcategoriaComponent } from './components/entidades/categorias/formcategoria.component';
import { FormatributoComponent } from './components/entidades/atributos/formatributo.component';

import { ProductoService } from './components/entidades/productos/producto.service';

import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { DataTablesModule } from 'angular-datatables';
import { InicioComponent } from './components/vistas/inicio/inicio.component';
import { FormmarcaComponent } from './components/entidades/marcas/formmarca.component';
import { FormetiquetaComponent } from './components/entidades/etiquetas/formetiqueta.component';

import { fakeBackendProvider } from '../app/components/entidades/_helpers';

import { JwtInterceptor, ErrorInterceptor } from './components/entidades/_helpers';
import { LoginComponent } from './components/entidades/login';



const routes: Routes = [

  { path: 'productos', component: ProductosComponent},
  { path:'productos/formproductos', component: FormproductoComponent},
  { path:'productos/formproductos/:id', component: FormproductoComponent},
  { path: 'inicio', component: InicioComponent},

  { path: 'categorias', component: CategoriasComponent},
  { path:'categorias/formcategorias', component: FormcategoriaComponent},
  { path:'categorias/formcategorias/:id', component: FormcategoriaComponent},
  { path: 'inicio', component: InicioComponent},

  { path: 'marcas', component: MarcasComponent},
  { path:'marcas/formmarcas', component: FormmarcaComponent},
  { path:'marcas/formmarcas/:id', component: FormmarcaComponent},
  { path: 'inicio', component: InicioComponent},

  { path: 'atributos', component: AtributosComponent},
  { path:'atributos/formatributos', component: FormatributoComponent},
  { path:'atributos/formatributos/:id', component: FormatributoComponent},
  { path: 'inicio', component: InicioComponent},

  { path: 'etiquetas', component: EtiquetasComponent},
  { path:'etiquetas/formetiquetas', component: FormetiquetaComponent},
  { path:'etiquetas/formetiquetas/:id', component: FormetiquetaComponent},
  { path: 'inicio', component: InicioComponent},
  { path: 'login', component: LoginComponent },

];

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    ContentComponent,
    FooterComponent,
    MenuComponent,
    FormproductoComponent,
    FormcategoriaComponent,
    FormmarcaComponent,
    FormatributoComponent,
    FormetiquetaComponent,
    ProductosComponent,
    InicioComponent,
    CategoriasComponent,
    MarcasComponent,
    EtiquetasComponent,
    AtributosComponent,
    LoginComponent,

  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
    FormsModule,
    NoopAnimationsModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatInputModule,
    MatFormFieldModule,
    DataTablesModule,
    [RouterModule.forRoot(routes)],
  ],
  providers: [ProductoService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },

        // provider used to create fake backend
        fakeBackendProvider
     ],
  bootstrap: [AppComponent]
})
export class AppModule { }
