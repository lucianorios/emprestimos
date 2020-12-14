import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserModel } from 'src/app/models/auth/user.model';
import { AuthService } from 'src/app/services/auth/auth.service';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public userModel: UserModel;
  public isLoading: boolean = false;

  constructor(private messageService: MessageService,
    private router: Router,
    private authService: AuthService) { }

  ngOnInit(): void {
    this.userModel = new UserModel();
  }

  login(){
    if(this.userModel.isValid()){
      this.authService.login(this.userModel).subscribe(
        response => {
          console.log(response);
          if(response.success){
            this.authService.store(response.data.token, response.data.userName);
            this.router.navigate(['']);
          } else {
            this.messageService.showError(response.messages.join('<br>'));
          }
          this.isLoading = false;
        },
        err => {
          this.messageService.showError(err.message);
          this.isLoading = false;
        }
      )
    } else {
      this.messageService.showWarning(this.userModel.notifications.join('<br>'));
      this.userModel.clearNorifications();    }
  }
}
