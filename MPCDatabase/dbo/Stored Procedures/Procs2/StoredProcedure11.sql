CREATE PROCEDURE dbo.StoredProcedure11

	

AS
	Declare  @Counter  int
	Set @Counter = 0
	
	While @Counter < 5
		Begin
		print 'one off bilal' + convert(varchar(10),@Counter)
		set @Counter = @Counter + 1
	End
	
	RETURN