import { Component, Inject, OnInit } from '@angular/core';
import { ControlContainer, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PessoaModel } from 'src/app/models/pessoa/pessoa.model';
import { MessageService } from 'src/app/services/message.service';
import { PessoaService } from 'src/app/services/pessoa.service';

@Component({
  selector: 'app-pessoa-dialog',
  templateUrl: './pessoa-dialog.component.html',
  styleUrls: ['./pessoa-dialog.component.scss']
})
export class PessoaDialogComponent implements OnInit {

  public isLoading: boolean = false;
  public pessoaModel: PessoaModel;

  constructor(private dialogRef: MatDialogRef<PessoaDialogComponent>,
    private pessoaService: PessoaService,
    private messageService: MessageService,
    @Inject(MAT_DIALOG_DATA) public data: { pessoaId: number, pessoa: PessoaModel }) { }

  ngOnInit(): void {
    this.pessoaModel = new PessoaModel();

    if(this.data.pessoa)
      this.pessoaModel = this.data.pessoa;
  }

  salvar(){

    if(this.pessoaModel.isValid()){
      this.isLoading = true;
      this.pessoaService.salvar(this.pessoaModel).subscribe(
        response => {
          if(response.success){
            this.messageService.showSuccess('Pessoa cadastrada com sucesso');
            this.dialogRef.close(true);
          } else {
            this.messageService.showError(response.messages.join('<br>'));
          }
          this.isLoading = false;
        },
        err => {
          this.messageService.showError(err.message);
          this.isLoading = false;
        }
      );
    } else {
      this.messageService.showWarning(this.pessoaModel.notifications.join('<br>'));
      this.pessoaModel.clearNorifications();
    }
  }
}
