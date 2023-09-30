using FluentMigrator;

namespace LivroDeReceitas.Infrastructure.Migrations.Versoes;
[Migration((long)NumeroVersoes.CriarTabelaUsuario, "Cria tabela Usuário")]
public class Versao00001 : Migration
{
    public override void Down()
    {
        
    }

    public override void Up()
    {
        var tabela = VersaoBase.InserirColunasPadrao(Create.Table("Usuario"));

        tabela.WithColumn("Nome").AsString(100).NotNullable()
               .WithColumn("Email").AsString().NotNullable()
               .WithColumn("Senha").AsString(2000).NotNullable()
               .WithColumn("Telefone").AsString(16).NotNullable();
    }
}
