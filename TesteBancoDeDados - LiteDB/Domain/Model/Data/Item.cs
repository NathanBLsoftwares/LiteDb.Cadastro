﻿using LiteDB;
using TesteBancoDeDados___LiteDB.Mappers;
using TesteBancoDeDadosLiteDB.Domain.Model;

namespace TesteBancoDeDados___LiteDB.Domain.Model.Data;

internal class Item
{
    [BsonId]
    public int Id { get; set; }
    public string Nome { get; set; }

    [BsonRef($"{MapDataBase.ItemLinha}")]
    public ItemDaLinha ItemLinha { get; set; }

    //[BsonRef($"{MapDataBase.DiametroTipo}")]
    public EDiametros Diametro { get; set; }

    [BsonRef($"{MapDataBase.Bloco}")]
    public Bloco Blocos { get; set; }
    public override string ToString()
    {
        return Nome;
    }
}
