CREATE TABLE [dbo].[GroupDetail] (
    [GroupDetailId]     INT      IDENTITY (1, 1) NOT NULL,
    [ContactId]         INT      NOT NULL,
    [IsCustomerContact] SMALLINT CONSTRAINT [DF_tbl_group_detail_IsCustomerContact] DEFAULT (1) NOT NULL,
    [GroupId]           INT      NOT NULL,
    CONSTRAINT [PK_tbl_group_detail] PRIMARY KEY CLUSTERED ([GroupDetailId] ASC),
    CONSTRAINT [FK_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([GroupId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Field To Identify whether the contactID is Customer,PRospect, or Supplier.(1 Customer, 0 Supplier,2 Prospect)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GroupDetail', @level2type = N'COLUMN', @level2name = N'IsCustomerContact';

