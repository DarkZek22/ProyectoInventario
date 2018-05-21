using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InventarioUX.Models
{
    public class ConnectionContext : DbContext
    {
        public ConnectionContext()
          : base("MyConnection")
        {

        }
        public DbSet<ALMACEN> ALMACEN { get; set; }
        public DbSet<AREAS> AREAS { get; set; }
        public DbSet<ASIGNACIONES> ASIGNACIONES { get; set; }
        public DbSet<ASIGNACIONES_LISTA> ASIGNACIONES_LISTA { get; set; }
        public DbSet<CAT_PRODUCTOS> CAT_PRODUCTO { get; set; }
        public DbSet<DEPARTAMENTOS> DEPARTAMENTOS { get; set; }
        public DbSet<EMPLEADOS> EMPLEADOS { get; set; }
        public DbSet<MOV_ENTRADA> MOV_ENTRADA { get; set; }
        public DbSet<MOV_ENTRADA_LISTA> MOV_ENTRADA_LISTA { get; set; }
        public DbSet<MOV_SALIDA> MOV_SALIDA { get; set; }
        public DbSet<MOV_SALIDA_LISTA> MOV_SALIDA_LISTA { get; set; }
        public DbSet<PRODUCTOS> PRODUCTOS { get; set; }
        public DbSet<PROVEEDORES> PROVEEDORES { get; set; }
        public DbSet<PUESTOS> PUESTOS { get; set; }
        public DbSet<TIPO_EMPLEADOS> TIPO_EMPLEADO { get; set; }
        public DbSet<TRABAJADORESUX> TRABAJADORESUX { get; set; }
    }
}