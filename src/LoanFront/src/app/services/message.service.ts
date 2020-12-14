import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class MessageService {

    constructor(private toastr: ToastrService) {
    }

    show(message: string, alerta: 'info' | 'warning' | 'success' | 'error' = 'success', duration: number = 5000) {
        if (alerta == 'error')
            this.toastr.error(message, this.firstUpper(alerta), { timeOut: duration, enableHtml: true });
        else if (alerta == 'info')
            this.toastr.info(message, this.firstUpper(alerta), { timeOut: duration, enableHtml: true });
        else if (alerta == 'success')
            this.toastr.success(message, this.firstUpper(alerta), { timeOut: duration, enableHtml: true });
        else if (alerta == 'warning')
            this.toastr.warning(message, this.firstUpper(alerta), { timeOut: duration, enableHtml: true });
    }

    showInfo(message: string, duration: number = 5000){
      this.show(message, 'info', duration);
    }
    showWarning(message: string, duration: number = 5000){
      this.show(message, 'warning', duration);
    }
    showSuccess(message: string, duration: number = 5000){
      this.show(message, 'success', duration);
    }
    showError(message: string, duration: number = 5000){
      this.show(message, 'error', duration);
    }

    private firstUpper(item: string) {
        return item.charAt(0).toUpperCase() + item.slice(1);
    }

}
