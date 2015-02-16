CREATE TABLE [dbo].[ProfileDescriptionLabelsValue] (
    [ValueId]   INT          IDENTITY (1, 1) NOT NULL,
    [valuetext] VARCHAR (50) NULL,
    CONSTRAINT [PK_tbl_profile_description_labels_values] PRIMARY KEY CLUSTERED ([ValueId] ASC)
);

