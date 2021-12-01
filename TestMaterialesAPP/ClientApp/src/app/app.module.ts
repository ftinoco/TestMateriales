import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav.menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { DxReportViewerModule, DxReportDesignerModule } from 'devexpress-reporting-angular';
import { ReportViewerComponent } from './reportviewer/report-viewer';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialListComponent } from './components/material-list/material-list.component';
import { MaterialCreateComponent } from './components/material-create/material-create.component';
import { MaterialUpdateComponent } from './components/material-update/material-update.component';

import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';
import {MatTableModule} from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRadioModule, MatSelectModule } from '@angular/material';
import {MatSnackBarModule} from '@angular/material/snack-bar';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ReportViewerComponent,
    MaterialListComponent,
    MaterialCreateComponent,
    MaterialUpdateComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    /* material modules */
    MatToolbarModule,
    MatIconModule,
    MatTableModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatCheckboxModule,
    MatRadioModule,
    MatSelectModule,
    MatSnackBarModule,

    FormsModule,
    ReactiveFormsModule,
    DxReportViewerModule,
    RouterModule.forRoot([
      { path: '', component: MaterialListComponent, pathMatch: 'full' },
      { path: 'create', component: MaterialCreateComponent },
      { path: 'edit/:id', component: MaterialUpdateComponent },
      { path: 'viewer', component: ReportViewerComponent }
    ]),
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }