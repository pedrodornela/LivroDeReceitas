﻿using FluentMigrator;

namespace LivroDeReceitas.Infrastructure.Migrations.Versoes;

[Migration((long)NumeroVersoes.AlterarTabelaReceitas, "Adicionando coluna Tempo para o preparo")]
public class Versao00003 : Migration
{
    public override void Down()
    {
        
    }

    public override void Up()
    {
        Alter.Table("receitas").AddColumn("TempoPreparo").AsInt32().NotNullable().WithDefaultValue(0);
    }
}
