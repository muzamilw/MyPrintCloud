Create Procedure sp_update_templateid_products

@templateidlist varchar(max) ,
@itemidlist varchar(max) 
As
Begin
	DECLARE @temp1 AS TABLE(
		ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
		templateid int
		);
	insert into @temp1(templateid)
	select * from fb_Split_String(@templateidlist,',')
	
	DECLARE @temp2 AS TABLE(
		ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
		itemid int
		);
	insert into @temp2(itemid)
	select * from fb_Split_String(@itemidlist,',')
		
	DECLARE @ItemTemplate AS TABLE(
		ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
		itemid int,
		templateid int
	);
	
	Declare @counter int = 1
	Declare @total int = (select count(*) from @temp1)
	
	While @counter <= @total
	Begin
		insert into @ItemTemplate(itemid,templateid)
		Values( (select itemid from @temp2 where rowid =@counter) , (select templateid from @temp1 where rowid =@counter) )
		set @counter = @counter + 1;
	End
	
	select * from @itemtemplate
	Declare @exists int = 0
	
	Set @counter = 1
	
	while @counter <= @total
	Begin
	set @exists = (select templateid from @itemtemplate where rowid = @counter)
	
	if @exists <> 0
	Begin
		print @exists
		update tbl_items set templateid = (select templateid from @itemtemplate where rowid = @counter) 
		where itemid =  (select itemid from @itemtemplate where rowid = @counter)
	End
	
	set @counter = @counter + 1
	End
End