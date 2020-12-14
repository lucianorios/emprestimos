export class UserModel{
  login: string;
  password: string;

  notifications: string[] = [];

  constructor(){
    this.notifications = [];
  }

  isValid(){
    if(!this.login)
      this.notifications.push('Informe o login');

    if(!this.password)
      this.notifications.push('Informe o password');

    return this.notifications.length == 0;
  }

  clearNorifications(){
    this.notifications = [];
  }
}
