import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BillingService {

  constructor() { }

  concepto = (concepto: number) => {
    switch (concepto) {
      case 1: return 'Productos';
      case 2: return 'Servicios';
      case 3: return 'Productos y servicios';
      default: return 'Otro (' + concepto + ')';
    }
  }

  docTipo = (docTipo: number): string => {
    switch (docTipo) {
      case 80: return 'CUIT';
      case 86: return 'CUIL';
      case 87: return 'CDI';
      case 96: return 'DNI';
      case 99: return 'Sin declarar';
      default: return 'Otro (' + docTipo + ')';
    }
  }

  resultado = (resultado: string): string => {
    switch (resultado) {
      case 'A': return 'Aprobado';
      case 'P': return 'Parcialmente Aprobado';
      case 'R': return 'Rechazado';
      default: return 'Otro (' + resultado + ')';
    }
  }

  cbteTipo = (cbteTipo: number): string => {
    switch (cbteTipo) {
      case 1: return 'Factura A';
      case 6: return 'Factura B';
      default: return 'Otro (' + cbteTipo + ')';
    }
  }

  fecha = (fecha: string): Date | null => {
    if (Number(fecha) && fecha.length === 8) {
      return new Date(Number(fecha.substr(0, 4)), Number(fecha.substr(4, 2)), Number(fecha.substr(6, 2)));
    }
    return null;
  }
}
