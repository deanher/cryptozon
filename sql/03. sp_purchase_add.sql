use Cryptozon
go

drop procedure if exists sp_purchase_add
go

create procedure sp_purchase_add
     ( @reference uniqueidentifier,
       @userId    uniqueidentifier,
       @coinId    int,
       @quantity  decimal(18,9),
       @unitPrice decimal(18, 9) )
as
begin
  begin try
    insert dbo.Purchases
         ( Reference,
           UserId,
           CoinId,
           Quantity,
           UnitPrice,
           PurchasedOn )
    select @reference,
           @userId,
           @coinId,
           @quantity,
           @unitPrice,
           getdate()
  end try
  begin catch
    declare @error_number  int           = error_number(),
            @error_message nvarchar(max) = concat(error_message(), ' [sp_purchase_add]'),
            @error_state   int           = error_state();

    throw @error_number, @error_message, @error_state
  end catch
end
go

grant execute on sp_purchase_add to public
go