CREATE TABLE [Notas] (
    [Id] int NOT NULL IDENTITY,
    [NomePagador] nvarchar(150) NOT NULL,
    [NumeroIdentificacao] nvarchar(300) NOT NULL,
    [DataEmissaoNota] datetime2 NULL,
    [DataCobranca] datetime2 NULL,
    [DataPagamento] datetime2 NULL,
    [Valor] decimal(14,2) NOT NULL,
    [NotaFiscal] nvarchar(max) NOT NULL,
    [BoletoBancario] nvarchar(max) NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Notas] PRIMARY KEY ([Id])
);