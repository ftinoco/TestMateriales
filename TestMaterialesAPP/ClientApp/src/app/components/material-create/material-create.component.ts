import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CategoriaService } from '../../services/categoria.service';
import { MaterialService } from '../../services/material.service';
import { UnidadMedidaService } from '../../services/unidad-medida.service';

@Component({
  selector: 'app-material-create',
  templateUrl: './material-create.component.html',
  styleUrls: ['./material-create.component.css']
})
export class MaterialCreateComponent implements OnInit {
  materialForm: FormGroup;
  categorias: [] = [];
  unidadesMedidas: [] = [];

  constructor(private fb: FormBuilder,
    private service: MaterialService,
    private catService: CategoriaService,
    private umService: UnidadMedidaService,
    private _snackBar: MatSnackBar,
    private route: Router
  ) { }

  ngOnInit() {
    this.materialForm = this.fb.group({
      nombreMaterial: [null, Validators.required],
      descripcion: [null, Validators.required],
      precio: [null, Validators.required],
      categoria: [null, Validators.required],
      proveedor: [null, Validators.required],
      unidadMedida: [null, Validators.required],
      existencia: [null, Validators.required]
    });
    this.loadList();
  }

  submit() {
    if (!this.materialForm.valid) {
      return;
    } 
    this.service.create({
      NombreMaterial: this.materialForm.controls['nombreMaterial'].value,
      Descripcion: this.materialForm.controls['descripcion'].value,
      Precio: this.materialForm.controls['precio'].value,
      IdCategoria: this.materialForm.controls['categoria'].value,
      Proveedor: this.materialForm.controls['proveedor'].value,
      IdUnidadMedida: this.materialForm.controls['unidadMedida'].value,
      Existencia: this.materialForm.controls['existencia'].value
    }).subscribe(
      data => { 
        this._snackBar.open(data.message, 'Cerrar', {
          duration: 1000,
        });
        this.route.navigate(['/']);
      }, 
      error => { console.log(error); });
  }

  loadList() {
    this.catService.getAll().subscribe(
      data => { this.categorias = data; },
      error => { console.log(error); });

    this.umService.getAll().subscribe(
      data => { this.unidadesMedidas = data; },
      error => { console.log(error); });
  }
}
