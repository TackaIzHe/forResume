CREATE TABLE [dbo].[Groups] (
    [id]           INT            IDENTITY (1, 1) NOT NULL,
    [name]         NVARCHAR (MAX) NULL,
    [nameStarosta] NVARCHAR (MAX) NULL,
    [idFacul]      INT            NOT NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED ([id] ASC)
);

