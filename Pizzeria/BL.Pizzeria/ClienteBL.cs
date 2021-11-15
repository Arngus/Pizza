﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Pizzeria
{
    public class ClienteBL
    {
        Contexto _contexto;

        public BindingList<Cliente> ListaClientes { get; set; }

        public ClienteBL()
        {
            _contexto = new Contexto();
            ListaClientes = new BindingList<Cliente>();
        }

        public BindingList<Cliente> ObtenerClientes()
        {
            _contexto.Clientes.Load();
            ListaClientes = _contexto.Clientes.Local.ToBindingList();

            return ListaClientes;
        }

        public void CancelarCambios()
        {
            foreach (var item in _contexto.ChangeTracker.Entries())
            {
                item.State = EntityState.Unchanged;
                item.Reload();
            }

        }
        public Resultado GuardarCliente(Cliente cliente)
        {
            var resultado = validar(cliente);
            if (resultado.Exitoso == false)
            {
                return resultado;
            }

            _contexto.SaveChanges();

            resultado.Exitoso = true;
            return resultado;
        }

        public void AgregarCliente()
        {
            var nuevoCliente = new Cliente();
            _contexto.Clientes.Add(nuevoCliente);
        }

        public bool EliminarCliente(int id)
        {
            foreach (var cliente in ListaClientes.ToList())
            {
                if (cliente.Id == id)
                {
                    ListaClientes.Remove(cliente);
                    _contexto.SaveChanges();
                    return true;
                }
            }

            return false;
        }


        private Resultado validar(Cliente cliente)
        {
            var resultado = new Resultado();
            resultado.Exitoso = true;

           


            if (cliente == null)
            {
                resultado.Mensaje = "Agruege un cliente para guardarla";
                resultado.Exitoso = false;

                return resultado;
            }


            if (string.IsNullOrEmpty(cliente.nombre) == true)
            {
                resultado.Mensaje = "Ingrese un tipo de pizza";
                resultado.Exitoso = false;
            }

            return resultado;

            
        }
    }

    public class Cliente
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public bool Activo { get; set; }

        public Cliente()
        {
            Activo = true;
        }
    }
}
