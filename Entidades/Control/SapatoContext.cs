namespace Entidades.Control
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Entidades.Class;

    public partial class SapatoContext : DbContext
    {
        // Your context has been configured to use a 'SapatoContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'TrabalhoSapatos.Control.SapatoContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'SapatoContext' 
        // connection string in the application configuration file.
        
        static SapatoContext()
        {
            var _ = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
            //var __ = typeof(System.Data.Entity.SqlServerCompact.SqlCeProviderServices);
        }
        
        public SapatoContext()
            : base("name=SapatoContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Pessoa> Pessoas { get; set; }


        public virtual DbSet<Venda> Vendas { get; set; }

        public virtual DbSet<Sapato> Sapatos { get; set; }

        public virtual DbSet<ItensVenda> itensVenda { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}
