import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent implements OnInit {

  constructor(private router: Router,
    private authService: AuthService) { }

  ngOnInit(): void {
  }

  isInitialPage() {
    return this.router.url.match('^/$');
  }

  isLoginPage() {
    return this.router.url.match('^/login$');
  }

  goToBack(){
    this.router.navigate(['/']);
  }

  logout(){
    this.authService.logout()

    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/login']);
    });
  }
}
