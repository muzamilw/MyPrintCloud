CREATE PROCEDURE dbo.sp_journalLadger_AddVoucherDetail 
(	 
@VoucherID int,
@Credit float,
@CreditAccount int,
@Debit float,
@DebitAccount int,
@Description varchar(255) ,
@DepartmentID int
	)
AS
	INSERT INTO tbl_voucherdetail (VoucherID, DebitAccount, Debit, Credit, Description, CreditAccount,DepartmentID) Values
	(@VoucherID, @DebitAccount, @Debit, @Credit, @Description, @CreditAccount,@DepartmentID)

RETURN