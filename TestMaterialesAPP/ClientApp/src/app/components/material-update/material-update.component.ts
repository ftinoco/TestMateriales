import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoriaService } from '../../services/categoria.service';
import { MaterialService } from '../../services/material.service';
import { UnidadMedidaService } from '../../services/unidad-medida.service';

@Component({
  selector: 'app-material-update',
  templateUrl: './material-update.component.html',
  styleUrls: ['./material-update.component.css']
})
export class MaterialUpdateComponent implements OnInit {
  id: number;
  categorias: [] = [];
  materialForm: FormGroup;
  unidadesMedidas: [] = [];

  constructor(private route: ActivatedRoute,
    private fb: FormBuilder,
    private router: Router,
    private service: MaterialService,
    private catService: CategoriaService,
    private umService: UnidadMedidaService,
    private _snackBar: MatSnackBar) { }

  ngOnInit() {
    this.id = +this.route.snapshot.paramMap.get('id');
    this.getById(this.id);
  }

  getById(id){ 
    this.service.get(id)
    .subscribe(
      data => {  
        this.materialForm = this.fb.group({
          nombreMaterial: [data.nombreMaterial, Validators.required],
          descripcion: [data.descripcion, Validators.required],
          precio: [data.precio, Validators.required],
          categoria: [data.idCategoria, Validators.required],
          proveedor: [data.proveedor, Validators.required],
          unidadMedida: [data.idUnidadMedida, Validators.required],
          existencia: [data.existencia, Validators.required]
        });
        this.loadList();
      },
      error => { console.log(error); });
  }

  submit() {
    if (!this.materialForm.valid) return;    

    this.service.update(this.id, {
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
        this.router.navigate(['/']);
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
