import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatSnackBar, MatTableDataSource } from '@angular/material';
import { config } from 'process';
import { MaterialModel } from '../../models/material.model';
import { MaterialService } from '../../services/material.service';

@Component({
  selector: 'app-material-list',
  templateUrl: './material-list.component.html',
  styleUrls: ['./material-list.component.css']
})
export class MaterialListComponent implements OnInit {
  dataSource = new MatTableDataSource();
  displayedColumns: string[] = [
    'nombreMaterial',
    'descripcion',
    'precio',
    'categoria',
    'proveedor',
    'unidadMedida',
    'existencia',
    'action'
  ];

  proveedorFilter = new FormControl();
  categoriaFilter = new FormControl(); 

  filteredValues = {
    categoria: '', proveedor: ''
  };


  constructor(private service: MaterialService,
    private _snackBar: MatSnackBar) { }

  ngOnInit() { 
    this.getMaterial();

    this.proveedorFilter.valueChanges.subscribe((positionFilterValue) => {
      this.filteredValues['proveedor'] = positionFilterValue;
      this.dataSource.filter = JSON.stringify(this.filteredValues);
    });

    this.categoriaFilter.valueChanges.subscribe((nameFilterValue) => {
      this.filteredValues['categoria'] = nameFilterValue;
      this.dataSource.filter = JSON.stringify(this.filteredValues);
    });

  }

  getMaterial(): void {
    this.service.getAll()
      .subscribe(
        data => {
          this.dataSource = new MatTableDataSource(data);
          console.log(data);
          this.dataSource.filterPredicate = this.customFilterPredicate();
        },
        error => { console.log(error); });
  }

  customFilterPredicate() {
    const myFilterPredicate = (data: MaterialModel, filter: string): boolean => { 
      let searchString = JSON.parse(filter);
      return data.proveedor.toString().toLowerCase().trim().indexOf(searchString.proveedor.toLowerCase()) !== -1 &&
        data.categoria.toString().trim().toLowerCase().indexOf(searchString.categoria.toLowerCase()) !== -1;
    }
    return myFilterPredicate;
  }

  deleteRow(id){
    this.service.delete(id)
    .subscribe(
      data => {
        this._snackBar.open('Registro eliminado exitosamente', 'Cerrar', {
          duration: 1000,
        });
        this.getMaterial();
      },
      error => { console.log(error); });
  }
}
