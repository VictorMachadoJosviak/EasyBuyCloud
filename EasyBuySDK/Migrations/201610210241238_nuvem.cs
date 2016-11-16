namespace EasyBuySDK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nuvem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tipo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Estabelecimento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Endereco = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Marca = c.String(),
                        Preco = c.Double(nullable: false),
                        Disponivel = c.Boolean(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                        EstabelecimentoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categoria", t => t.CategoriaId, cascadeDelete: true)
                .ForeignKey("dbo.Estabelecimento", t => t.EstabelecimentoId, cascadeDelete: true)
                .Index(t => t.CategoriaId)
                .Index(t => t.EstabelecimentoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produto", "EstabelecimentoId", "dbo.Estabelecimento");
            DropForeignKey("dbo.Produto", "CategoriaId", "dbo.Categoria");
            DropIndex("dbo.Produto", new[] { "EstabelecimentoId" });
            DropIndex("dbo.Produto", new[] { "CategoriaId" });
            DropTable("dbo.Produto");
            DropTable("dbo.Estabelecimento");
            DropTable("dbo.Categoria");
        }
    }
}
