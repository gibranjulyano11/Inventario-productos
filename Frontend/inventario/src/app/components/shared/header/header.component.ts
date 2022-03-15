import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { AuthenticationService } from '../../entidades/_services/authentication.service';
//import { AuthService } from '../../vistas/login/auth.service';



@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
})
export class HeaderComponent implements OnInit {
  authenticationService: any;



  constructor(private router: Router,
    authenticationService: AuthenticationService,
    //public authService: AuthService,
    ) { }

  logout():void{
    let userName = this.authenticationService.currentUser;
    this.authenticationService.logout();
    Swal.fire('Login', `Hola ${userName}, ya haz iniciado sesión anteriormente!`, 'info');
    this.router.navigate(['/login']);
  }

  ngOnInit(): void {
  }

}
