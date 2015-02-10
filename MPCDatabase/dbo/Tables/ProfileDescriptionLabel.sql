CREATE TABLE [dbo].[ProfileDescriptionLabel] (
    [Id]        BIGINT       IDENTITY (1, 1) NOT NULL,
    [Title]     VARCHAR (50) NULL,
    [ValueId]   INT          NULL,
    [ProfileId] BIGINT       NULL,
    CONSTRAINT [PK_tbl_profile_description_labels] PRIMARY KEY CLUSTERED ([Id] ASC)
);

