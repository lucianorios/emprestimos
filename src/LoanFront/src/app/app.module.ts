import { BrowserModule } from '@angular/platform-browser';
import { LOCALE_ID, NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule }   from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import {A11yModule} from '@angular/cdk/a11y';
import {ClipboardModule} from '@angular/cdk/clipboard';
import {DragDropModule} from '@angular/cdk/drag-drop';
import {PortalModule} from '@angular/cdk/portal';
import {ScrollingModule} from '@angular/cdk/scrolling';
import {CdkStepperModule} from '@angular/cdk/stepper';
import {CdkTableModule} from '@angular/cdk/table';
import {CdkTreeModule} from '@angular/cdk/tree';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatBadgeModule} from '@angular/material/badge';
import {MatBottomSheetModule} from '@angular/material/bottom-sheet';
import {MatButtonModule} from '@angular/material/button';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import {MatCardModule} from '@angular/material/card';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatChipsModule} from '@angular/material/chips';
import {MatStepperModule} from '@angular/material/stepper';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatDialogModule, MAT_DIALOG_DEFAULT_OPTIONS} from '@angular/material/dialog';
import {MatDividerModule} from '@angular/material/divider';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import {MatListModule} from '@angular/material/list';
import {MatMenuModule} from '@angular/material/menu';
import {MatNativeDateModule, MatRippleModule, MAT_DATE_LOCALE} from '@angular/material/core';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatRadioModule} from '@angular/material/radio';
import {MatSelectModule} from '@angular/material/select';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatSliderModule} from '@angular/material/slider';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatSortModule} from '@angular/material/sort';
import {MatTableModule} from '@angular/material/table';
import {MatTabsModule} from '@angular/material/tabs';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatTreeModule} from '@angular/material/tree';
import {OverlayModule} from '@angular/cdk/overlay';
import { ToastrModule } from 'ngx-toastr';
import { MessageService } from './services/message.service';
import { MatFabMenuModule } from '@angular-material-extensions/fab-menu';
import { NgxMaskModule, IConfig } from 'ngx-mask';
import { EmprestimoComponent } from './pages/emprestimo/emprestimo.component';

import localePtBr from '@angular/common/locales/pt';
import { registerLocaleData } from '@angular/common';
import { LoadingComponent } from './shared/components/loading/loading.component';
import { FabMenuComponent } from './shared/components/fab-menu/fab-menu.component';
import { PessoaComponent } from './pages/pessoa/pessoa.component';
import { ItemComponent } from './pages/item/item.component';
import { ToolbarComponent } from './shared/components/toolbar/toolbar.component';
import { PessoaDialogComponent } from './pages/pessoa/pessoa-dialog/pessoa-dialog.component';
import { FabMenuComponentEvents } from './shared/components/fab-menu/fab-menu.events';
import { ItemDialogComponent } from './pages/item/item-dialog/item-dialog.component';
import { EmprestimoDialogComponent } from './pages/emprestimo/emprestimo-dialog/emprestimo-dialog.component';
import { LoginComponent } from './pages/login/login.component';
import { AuthGuard } from './shared/auth/auth-guard.service';
import { TokenInterceptorService } from './shared/http/token-interceptor.service';

registerLocaleData(localePtBr);

export const options: Partial<IConfig> | (() => Partial<IConfig>) = null;

@NgModule({
  declarations: [
    AppComponent,
    EmprestimoComponent,
    LoadingComponent,
    FabMenuComponent,
    PessoaComponent,
    ItemComponent,
    ToolbarComponent,
    PessoaDialogComponent,
    ItemDialogComponent,
    EmprestimoDialogComponent,
    LoginComponent
  ],
  exports: [
    LoadingComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    A11yModule,
    ClipboardModule,
    CdkStepperModule,
    CdkTableModule,
    CdkTreeModule,
    DragDropModule,
    MatAutocompleteModule,
    MatBadgeModule,
    MatBottomSheetModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatStepperModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    MatTreeModule,
    OverlayModule,
    PortalModule,
    ScrollingModule,
    HttpClientModule,
    MatFabMenuModule,
    ToastrModule.forRoot({
      timeOut: 5000,
      preventDuplicates: true
    }),
    NgxMaskModule.forRoot()
  ],
  providers: [
    { provide: LOCALE_ID, useValue: "pt-BR" },
    { provide: MAT_DATE_LOCALE, useValue: LOCALE_ID },
    { provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: { hasBackdrop: true } },
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptorService, multi: true },
    MessageService,
    FabMenuComponentEvents,
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
