CREATE TABLE [dbo].[InkPlateSide] (
    [PlateInkId]          INT           IDENTITY (1, 1) NOT NULL,
    [InkTitle]            VARCHAR (200) NULL,
    [PlateInkDescription] VARCHAR (300) NULL,
    [isDoubleSided]       BIT           CONSTRAINT [DF_tbl_InkPlateSides_isDoubleSided] DEFAULT ((0)) NOT NULL,
    [PlateInkSide1]       INT           CONSTRAINT [DF_tbl_InkPlateSides_PlateInkSide1] DEFAULT ((0)) NOT NULL,
    [PlateInkSide2]       INT           CONSTRAINT [DF_tbl_InkPlateSides_PlateInkSide2] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_tbl_InkPlateSides] PRIMARY KEY CLUSTERED ([PlateInkId] ASC)
);

